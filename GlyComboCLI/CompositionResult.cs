using GlyComboCLI;
using System.Globalization;
using static GlyComboCLI.MonosaccharideDefinition;

namespace GlyCombo;

internal sealed record CompositionResult(
    IReadOnlyDictionary<MonosaccharideDefinition, int> Counts,
    decimal TheoreticalMass,
    decimal ObservedMass,
    decimal MassError,
    ElementalFormula Formula,
    string GlyTouCanAccession,
    Ms2ScanRecord? Ms2Scan, // null for plain text-list input; present for mzML input
    string Derivatisation   // "native" | "permethylated" | "peracetylated"  selects display order
)
{
    private static readonly string[] NativeAndPermethylatedDisplayOrder =
    {
        "dHex", "HexA", "HexN", "Pent", "KDN", "Hex", "NeuAc", "NeuGc", "HexNAc",
        "Phos", "Sulf", "dHexNAc", "lNeuAc", "eNeuAc", "dNeuAc", "amNeuAc", "Acetyl",
        "lNeuGc", "eNeuGc", "dNeuGc", "amNeuGc"
    };

    private static readonly string[] PeracetylatedDisplayOrder =
    {
        "dHex", "HexA", "Pent", "KDN", "Hex", "NeuAc", "NeuGc",
        "HexNAc", "HexN", "HexNAc/HexN", "Phos", "dHexNAc", "Sulf"
    };

    public string FormatLabel()
    {
        string[] order = Derivatisation == "peracetylated"
            ? PeracetylatedDisplayOrder
            : NativeAndPermethylatedDisplayOrder;

        var byLabel = Counts
            .GroupBy(kvp => kvp.Key.DisplayLabel)
            .Select(g => (Label: g.Key, Count: g.Sum(kvp => kvp.Value)))
            .Where(x => x.Count > 0)
            .OrderBy(x =>
            {
                int idx = Array.IndexOf(order, x.Label);
                return idx >= 0 ? idx : int.MaxValue;
            });

        return string.Join(" ", byLabel.Select(x => $"({x.Label}){x.Count}"));
    }

    public string ToPlainCsvRow(string separator, bool includeGlyTouCan)
    {
        var fields = new List<string>
        {
            FormatLabel(),
            TheoreticalMass.ToString(CultureInfo.InvariantCulture),
            ObservedMass.ToString(CultureInfo.InvariantCulture),
            Formula.ToString(),
            MassError.ToString(CultureInfo.InvariantCulture),
        };
        if (includeGlyTouCan)
        {
            fields.Add(GlyTouCanAccession);
        }
        return string.Join(separator, fields);
    }

    public string ToSkylineCsvRow(string separator, bool includeGlyTouCan)
    {
        if (Ms2Scan is null)
        {
            throw new InvalidOperationException(
                "ToSkylineCsvRow requires Ms2Scan metadata; this result came from text-list input.");
        }

        var fields = new List<string>
        {
            "GlyCombo",
            FormatLabel(),
            TheoreticalMass.ToString(CultureInfo.InvariantCulture),
            ObservedMass.ToString(CultureInfo.InvariantCulture),
            Formula.ToString(),
            MassError.ToString(CultureInfo.InvariantCulture),
            Ms2Scan.ScanNumber,
            Ms2Scan.Charge.ToString(CultureInfo.InvariantCulture),
            Ms2Scan.RetentionTimeMinutes.ToString(CultureInfo.InvariantCulture),
            Ms2Scan.TotalIonCurrent.ToString(CultureInfo.InvariantCulture),
            Ms2Scan.FileName,
        };
        if (includeGlyTouCan)
        {
            fields.Add(GlyTouCanAccession);
        }
        return string.Join(separator, fields);
    }
}
