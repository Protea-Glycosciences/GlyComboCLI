using GlyComboCLI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace GlyCombo
{

    public sealed class Ms2ScanRecord
    {
        public string ScanNumber { get; set; } = "";
        public int Charge { get; set; }
        public decimal RetentionTimeMinutes { get; set; }
        public decimal TotalIonCurrent { get; set; }
        public decimal PrecursorMz { get; set; }
        public string Polarity { get; set; } = ""; // "positive" or "negative"
        public decimal NeutralPrecursorMass { get; set; }
        public string FileName { get; set; } = "";
    }

    public static class MzmlMs2Parser
    {
        private const decimal ProtonMass = 1.007276m;

        private const string ACC_MS_LEVEL = "MS:1000511";
        private const string ACC_NEGATIVE_SCAN = "MS:1000129";
        private const string ACC_POSITIVE_SCAN = "MS:1000130";
        private const string ACC_SCAN_START_TIME = "MS:1000016";
        private const string ACC_TOTAL_ION_CURRENT = "MS:1000285";
        private const string ACC_SELECTED_ION_MZ = "MS:1000744";
        private const string ACC_CHARGE_STATE = "MS:1000041";

        private static readonly Regex SciexScanPattern =
            new(@"cycle=(?<cycle>\d+).*?experiment=(?<experiment>\d+)", RegexOptions.Compiled);
        private static readonly Regex AgilentScanPattern =
            new(@"scanId[=:](?<scan>\d+)", RegexOptions.Compiled);
        private static readonly Regex GenericScanPattern =
            new(@"(?<![A-Za-z])scan=(?<scan>\d+)", RegexOptions.Compiled);

        public static List<Ms2ScanRecord> Parse(string filePath)
        {
            var records = new List<Ms2ScanRecord>();
            string fileName = Path.GetFileName(filePath);

            var settings = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Ignore,
                IgnoreWhitespace = true,
                IgnoreComments = true,
                XmlResolver = null // never resolve external entities/DTDs
            };

            using var fileStream = File.OpenRead(filePath);
            using var reader = XmlReader.Create(fileStream, settings);

            while (reader.Read())
            {
                if (reader.NodeType != XmlNodeType.Element || reader.LocalName != "spectrum")
                    continue;

                using var subtreeReader = reader.ReadSubtree();
                subtreeReader.Read();
                var spectrumElement = (XElement)XNode.ReadFrom(subtreeReader);

                var record = TryParseSpectrum(spectrumElement, fileName);
                if (record != null)
                    records.Add(record);

            }

            return records;
        }

        private static Ms2ScanRecord? TryParseSpectrum(XElement spectrum, string fileName)
        {
            string? msLevel = CvValue(spectrum, ACC_MS_LEVEL);
            if (msLevel != "2")
                return null;

            string polarity = spectrum.Descendants()
                .Where(IsCvParam)
                .Any(e => Accession(e) == ACC_NEGATIVE_SCAN)
                ? "negative"
                : spectrum.Descendants().Where(IsCvParam).Any(e => Accession(e) == ACC_POSITIVE_SCAN)
                    ? "positive"
                    : "";

            decimal? precursorMz = ParseDecimal(CvValueAnywhere(spectrum, ACC_SELECTED_ION_MZ));
            int? charge = ParseInt(CvValueAnywhere(spectrum, ACC_CHARGE_STATE));

            if (polarity == "" || precursorMz is null || charge is null || charge == 0)
            {
                // Without polarity, precursor m/z, and charge we cannot compute a neutral
                // mass for this spectrum mirrors the original "precursor != 0 && charge != 0"
                // gate before a spectrum was added to the result set.
                return null;
            }

            decimal retentionTime = ParseRetentionTimeMinutes(spectrum);
            decimal tic = ParseDecimal(CvValueAnywhere(spectrum, ACC_TOTAL_ION_CURRENT)) ?? 0m;
            string scanNumber = ExtractScanIdentifier(spectrum.Attribute("id")?.Value ?? "");

            int signedCharge = charge.Value;
            decimal neutralMass;
            if (polarity == "negative")
            {
                neutralMass = signedCharge * precursorMz.Value + (signedCharge * ProtonMass);
                signedCharge = -signedCharge;
            }
            else
            {
                neutralMass = signedCharge * precursorMz.Value - (signedCharge * ProtonMass);
            }

            return new Ms2ScanRecord
            {
                ScanNumber = scanNumber,
                Charge = signedCharge,
                RetentionTimeMinutes = retentionTime,
                TotalIonCurrent = tic,
                PrecursorMz = precursorMz.Value,
                Polarity = polarity,
                NeutralPrecursorMass = neutralMass,
                FileName = fileName
            };
        }

        private static decimal ParseRetentionTimeMinutes(XElement spectrum)
        {
            XElement? rtParam = spectrum.Descendants()
                .Where(IsCvParam)
                .FirstOrDefault(e => Accession(e) == ACC_SCAN_START_TIME);

            if (rtParam == null)
                return 0m;

            decimal value = ParseDecimal(rtParam.Attribute("value")?.Value) ?? 0m;
            string unit = rtParam.Attribute("unitName")?.Value ?? "minute";

            return unit.Equals("second", StringComparison.OrdinalIgnoreCase)
                ? value / 60m
                : value; // minute (or unspecified - assume minute, matching prior default path)
        }

        internal static string ExtractScanIdentifier(string spectrumId)
        {
            if (string.IsNullOrEmpty(spectrumId))
                return "";

            // Sciex: "cycle=X experiment=Y" -> represented as "X.Y" (no native scan number)
            var sciexMatch = SciexScanPattern.Match(spectrumId);
            if (sciexMatch.Success)
                return $"{sciexMatch.Groups["cycle"].Value}.{sciexMatch.Groups["experiment"].Value}";

            // Agilent: "scanId=NNNN"
            var agilentMatch = AgilentScanPattern.Match(spectrumId);
            if (agilentMatch.Success)
                return agilentMatch.Groups["scan"].Value;

            // Thermo ("controllerType=0 controllerNumber=1 scan=NNNN"), Bruker ("scan=NNNN"),
            // and Waters ("function=X process=Y scan=NNNN", merged or unmerged) all expose the
            // scan number via a "scan=" token; take the last match in case of nested contexts.
            var genericMatches = GenericScanPattern.Matches(spectrumId);
            if (genericMatches.Count > 0)
                return genericMatches[^1].Groups["scan"].Value;

            return spectrumId;
        }

        private static bool IsCvParam(XElement e) => e.Name.LocalName == "cvParam";

        private static string? Accession(XElement cvParam) => cvParam.Attribute("accession")?.Value;
        private static string? CvValue(XElement parent, string accession)
        {
            return parent.Elements()
                .Where(IsCvParam)
                .FirstOrDefault(e => Accession(e) == accession)
                ?.Attribute("value")?.Value;
        }

        private static string? CvValueAnywhere(XElement parent, string accession)
        {
            return parent.Descendants()
                .Where(IsCvParam)
                .FirstOrDefault(e => Accession(e) == accession)
                ?.Attribute("value")?.Value;
        }

        private static decimal? ParseDecimal(string? s)
        {
            if (string.IsNullOrEmpty(s))
                return null;
            return decimal.TryParse(
                s,
                NumberStyles.Float | NumberStyles.AllowExponent,
                CultureInfo.InvariantCulture,
                out decimal result)
                ? result
                : null;
        }

        private static int? ParseInt(string? s)
        {
            if (string.IsNullOrEmpty(s))
                return null;
            return int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out int result)
                ? result
                : null;
        }
    }
}