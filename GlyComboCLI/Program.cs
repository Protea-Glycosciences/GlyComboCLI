using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.CommandLine.NamingConventionBinder;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.InteropServices;
using System.Security.AccessControl;

public class CommandOptions
{
    public int HexMin { get; set; }
    public int HexMax { get; set; }
    public int HexNAcMin { get; set; }
    public int HexNAcMax { get; set; }
    public int dHexMin { get; set; }
    public int dHexMax { get; set; }
    public int HexAMin { get; set; }
    public int HexAMax { get; set; }
    public int HexNMin { get; set; }
    public int HexNMax { get; set; }
    public int PentMin { get; set; }
    public int PentMax { get; set; }
    public int KDNMin { get; set; }
    public int KDNMax { get; set; }
    public int Neu5AcMin { get; set; }
    public int Neu5AcMax { get; set; }
    public int Neu5GcMin { get; set; }
    public int Neu5GcMax { get; set; }
    public int PhosMin { get; set; }
    public int PhosMax { get; set; }
    public int SulfMin { get; set; }
    public int SulfMax { get; set; }
    public int dHexNAcMin { get; set; }
    public int dHexNAcMax { get; set; }
    public int lNeuAcMin { get; set; }
    public int lNeuAcMax { get; set; }
    public int eeNeuAcMin { get; set; }
    public int eeNeuAcMax { get; set; }
    public int dNeuAcMin { get; set; }
    public int dNeuAcMax { get; set; }
    public int amNeuAcMin { get; set; }
    public int amNeuAcMax { get; set; }
    public int acetylMin { get; set; }
    public int acetylMax { get; set; }
    public int lNeuGcMin { get; set; }
    public int lNeuGcMax { get; set; }
    public int eeNeuGcMin { get; set; }
    public int eeNeuGcMax { get; set; }
    public int dNeuGcMin { get; set; }
    public int dNeuGcMax { get; set; }
    public int amNeuGcMin { get; set; }
    public int amNeuGcMax { get; set; }
    // Custom monosaccharide 1
    public string? customMono1Name { get; set; }
    public int customMono1CCount { get; set; }
    public int customMono1HCount { get; set; }
    public int customMono1NCount { get; set; }
    public int customMono1OCount { get; set; }
    public decimal customMono1Mass { get; set; }
    public int customMono1Min { get; set; }
    public int customMono1Max { get; set; }

    // custom monosaccharide 2
    public string? customMono2Name { get; set; }
    public int customMono2CCount { get; set; }
    public int customMono2HCount { get; set; }
    public int customMono2NCount { get; set; }
    public int customMono2OCount { get; set; }
    public decimal customMono2Mass { get; set; }
    public int customMono2Min { get; set; }
    public int customMono2Max { get; set; }

    // custom monosaccharide 3
    public string? customMono3Name { get; set; }
    public int customMono3CCount { get; set; }
    public int customMono3HCount { get; set; }
    public int customMono3NCount { get; set; }
    public int customMono3OCount { get; set; }
    public decimal customMono3Mass { get; set; }
    public int customMono3Min { get; set; }
    public int customMono3Max { get; set; }

    // custom monosaccharide 4
    public string? customMono4Name { get; set; }
    public int customMono4CCount { get; set; }
    public int customMono4HCount { get; set; }
    public int customMono4NCount { get; set; }
    public int customMono4OCount { get; set; }
    public decimal customMono4Mass { get; set; }
    public int customMono4Min { get; set; }
    public int customMono4Max { get; set; }

    // custom monosaccharide 5
    public string? customMono5Name { get; set; }
    public int customMono5CCount { get; set; }
    public int customMono5HCount { get; set; }
    public int customMono5NCount { get; set; }
    public int customMono5OCount { get; set; }
    public decimal customMono5Mass { get; set; }
    public int customMono5Min { get; set; }
    public int customMono5Max { get; set; }
    // Custom Reducing
    public int customReducingCCount { get; set; }
    public int customReducingHCount { get; set; }
    public int customReducingNCount { get; set; }
    public int customReducingOCount { get; set; }
    public decimal customReducingMass { get; set; }
    public string? customReducingName { get; set; }
    public string? customReducedMassOutput { get; set; }
    public string? customAdductPolarity { get; set; }
    public decimal customAdductMass { get; set; }
    // Other Input
    public string? derivatisation { get; set; } 
    public string? reducedEnd { get; set; }
    public decimal massError {  get; set; }
    public string? massErrorType {  get; set; }
    public string? file { get; set; }
    public string? adducts { get; set; }
    public bool? offByOne { get; set; }

}
class Program
{
    static void Main(string[] args)
    {
        decimal errorTol;
        string solutionProcess;
        string solutions;
        string solutionMultiples = "";
        int targetsToAdd;
        int iterations;
        decimal targetLow = 0;
        decimal targetHigh = 0;
        string reducedEnd = "";
        decimal observedMass = 0;
        decimal theoreticalMass = 0;
        decimal error;

        // Adducts
        int searchRepeats = 0;
        List<decimal> targetAdducts;
        List<decimal> targetAdductsProcessing;

        // Monosaccharides
        // Need to initialize these to 0 or another number?
        decimal dhex = 0;
        decimal hex = 0;
        decimal hexnac = 0;
        decimal hexn = 0;
        decimal hexa = 0;
        decimal dhexnac = 0;
        decimal pent = 0;
        decimal kdn = 0;
        decimal neuac = 0;
        decimal neugc = 0;
        decimal phos = 0;
        decimal lneuac = 0;
        decimal eeneuac = 0;
        decimal dneuac = 0;
        decimal amneuac = 0;
        decimal acetyl = 0;
        decimal lneugc = 0;
        decimal eeneugc = 0;
        decimal dneugc = 9;
        decimal amneugc = 0;
        decimal sulf = 0;

        // Parameter report variables
        bool monoCustom1 = false;
        bool monoCustom2 = false;
        bool monoCustom3 = false;
        bool monoCustom4 = false;
        bool monoCustom5 = false;

        decimal precursor = 0;
        string line;
        string[] precursorLine;
        string[] chargeLine;
        int charge = 0;
        string[] RTLine;
        decimal retentionTime = 0;
        string neutralPrecursorListmzml = "";
        string targetString = "";
        string scanNumber = "";
        string[] scanLine;
        decimal TIC = 0;
        string[] TICLine;
        string currentMonosaccharideSelection = "";


        List<decimal> numbers = new List<decimal>();
        List<decimal> scans = new List<decimal>();
        List<int> charges = new List<int>();
        List<decimal> retentionTimes = new List<decimal>();
        List<decimal> TICs = new List<decimal>();
        List<string> files = new List<string>();
        List<int> targetIndex = new List<int>();
        List<decimal> targets = new List<decimal>();
        List<string> targetStrings = new List<string>();


        var rootCommand = new RootCommand
    {

        // Regular monosaccharide flags
        new Option<int>(new[] {"--hexMin", "-hMin" }, "Minimum hexose count (default:0)"),
        new Option<int>(new[] {"--hexMax", "-hMax" }, "Maximum hexose count (default:0)"),
        new Option<int>(new[] {"--hexNAcMin", "-nMin" }, "Minimum N-acetyl hexosamine count (default:0)"),
        new Option<int>(new[] {"--hexNAcMax", "-nMax" }, "Maximum N-acetyl hexosamine count (default:0)"),
        new Option<int>(new[] {"--dHexMin", "-fMin" }, "Minimum deoxyhexose count (default:0)"),
        new Option<int>(new[] {"--dHexMax", "-fMax" }, "Maximum deoxyhexose count (default:0)"),
        new Option<int>(new[] {"--hexAMin", "-aMin" }, "Minimum hexuronic acid count (default:0)"),
        new Option<int>(new[] {"--hexAMax", "-aMax" }, "Maximum hexuronic acid count (default:0)"),
        new Option<int>(new[] {"--hexNMin", "-xMin" }, "Minimum hexosamine count (default:0)"),
        new Option<int>(new[] {"--hexNMax", "-xMax" }, "Maximum hexosamine count (default:0)"),
        new Option<int>(new[] {"--pentMin", "-pMin" }, "Minimum pentose count (default:0)"),
        new Option<int>(new[] {"--pentMax", "-pMax" }, "Maximum pentose count (default:0)"),
        new Option<int>(new[] {"--kdnMin", "-kMin" }, "Minimum KDN count (default:0)"),
        new Option<int>(new[] {"--kdnMax", "-kMax" }, "Maximum KDN count (default:0)"),
        new Option<int>(new[] {"--neu5AcMin", "-sMin" }, "Minimum N-acetyl-neuraminic acid count (default:0)"),
        new Option<int>(new[] {"--neu5AcMax", "-sMax" }, "Maximum N-acetyl-neuraminic acid count (default:0)"),
        new Option<int>(new[] {"--neu5GcMin", "-gMin" }, "Minimum N-glycolyl-neuraminic acid count (default:0)"),
        new Option<int>(new[] {"--neu5GcMax", "-gMax" }, "Maximum N-glycolyl-neuraminic acid count (default:0)"),
        new Option<int>("--phosMin", "Minimum phosphate count (default:0)"),
        new Option<int>("--phosMax", "Maximum phosphate count (default:0)"),
        new Option<int>("--sulfMin", "Minimum sulfate count (default:0)"),
        new Option<int>("--sulfMax", "Maximum sulfate count (default:0)"),
        new Option<int>("--dHexNAcMin", "Minimum N-acetyl deoxyhexose count (default:0)"),
        new Option<int>("--dHexNAcMax", "Maximum N-acetyl deoxyhexose count (default:0)"),
        new Option<int>("--lNeuAcMin", "Minimum lactonised N-acetyl-neuraminic acid (a2,3) count (default:0)"),
        new Option<int>("--lNeuAcMax", "Maximum lactonised N-acetyl-neuraminic acid (a2,3) count (default:0)"),
        new Option<int>("--eeNeuAcMin", "Minimum ethyl esterified N-acetyl-neuraminic acid (a2,6) count (default:0)"),
        new Option<int>("--eeNeuAcMax", "Maximum ethyl esterified N-acetyl-neuraminic acid (a2,6) count (default:0)"),
        new Option<int>("--dNeuAcMin", "Minimum dimethylamidated N-acetyl-neuraminic acid (a2,6) count (default:0)"),
        new Option<int>("--dNeuAcMax", "Maximum dimethylamidated N-acetyl-neuraminic acid (a2,6) count (default:0)"),
        new Option<int>("--amNeuAcMin", "Minimum ammonia amidated N-acetyl-neuraminic acid (a2,3) count (default:0)"),
        new Option<int>("--amNeuAcMax", "Maximum ammonia amidated N-acetyl-neuraminic acid (a2,3) count (default:0)"),
        new Option<int>("--acetylMin", "Minimum acetylation count (default:0)"),
        new Option<int>("--acetylMax", "Maximum acetylation count (default:0)"),
        new Option<int>("--lNeuGcMin", "Minimum lactonised N-acetyl-neuraminic acid (a2,3) count (default:0)"),
        new Option<int>("--lNeuGcMax", "Maximum lactonised N-acetyl-neuraminic acid (a2,3) count (default:0)"),
        new Option<int>("--eeNeuGcMin", "Minimum ethyl esterified N-glycolyl-neuraminic acid (a2,6) count (default:0)"),
        new Option<int>("--eeNeuGcMax", "Maximum ethyl esterified N-glycolyl-neuraminic acid (a2,6) count (default:0)"),
        new Option<int>("--dNeuGcMin", "Minimum dimethylamidated N-glycolyl-neuraminic acid (a2,6) count (default:0)"),
        new Option<int>("--dNeuGcMax", "Maximum dimethylamidated N-glycolyl-neuraminic acid (a2,6) count (default:0)"),
        new Option<int>("--amNeuGcMin", "Minimum ammonia amidated N-glycolyl-neuraminic acid (a2,3) count (default:0)"),
        new Option<int>("--amNeuGcMax", "Maximum ammonia amidated N-glycolyl-neuraminic acid (a2,3) count (default:0)"),

        // Custom monosaccharide flags
        new Option<string>(new[] {"--customMono1Name", "-c1Name"}, "First custom monosaccharide name"),
        new Option<int>(new[] {"--customMono1CCount", "-c1C"}, "Carbon count of the first custom monosaccharide"),
        new Option<int>(new[] {"--customMono1HCount", "-c1H"}, "Hydrogen count of the first custom monosaccharide"),
        new Option<int>(new[] {"--customMono1NCount", "-c1N"}, "Nitrogen count of the first custom monosaccharide"),
        new Option<int>(new[] {"--customMono1OCount", "-c1O"}, "Oxygen count of the first custom monosaccharide"),
        new Option<decimal>(new[] {"--customMono1Mass", "-c1M"}, "Mass of the first custom monosaccharide"),
        new Option<int>(new[] {"--customMono1Min", "-c1Min"}, "Minimum value for the first custom monosaccharide"),
        new Option<int>(new[] {"--customMono1Max", "-c1Max"}, "Maximum value for the first custom monosaccharide"),

        new Option<string>(new[] {"--customMono2Name", "-c2Name"}, "Second custom monosaccharide name"),
        new Option<int>(new[] {"--customMono2CCount", "-c2C"}, "Carbon count of the second custom monosaccharide"),
        new Option<int>(new[] {"--customMono2HCount", "-c2H"}, "Hydrogen count of the second custom monosaccharide"),
        new Option<int>(new[] {"--customMono2NCount", "-c2N"}, "Nitrogen count of the second custom monosaccharide"),
        new Option<int>(new[] {"--customMono2OCount", "-c2O"}, "Oxygen count of the second custom monosaccharide"),
        new Option<decimal>(new[] {"--customMono2Mass", "-c2M"}, "Mass of the second custom monosaccharide"),
        new Option<int>(new[] {"--customMono2Min", "-c2Min"}, "Minimum value for the second custom monosaccharide"),
        new Option<int>(new[] {"--customMono2Max", "-c2Max"}, "Maximum value for the second custom monosaccharide"),

        new Option<string>(new[] {"--customMono3Name", "-c3Name"}, "Third custom monosaccharide name"),
        new Option<int>(new[] {"--customMono3CCount", "-c3C"}, "Carbon count of the third custom monosaccharide"),
        new Option<int>(new[] {"--customMono3HCount", "-c3H"}, "Hydrogen count of the third custom monosaccharide"),
        new Option<int>(new[] {"--customMono3NCount", "-c3N"}, "Nitrogen count of the third custom monosaccharide"),
        new Option<int>(new[] {"--customMono3OCount", "-c3O"}, "Oxygen count of the third custom monosaccharide"),
        new Option<decimal>(new[] {"--customMono3Mass", "-c3M"}, "Mass of the third custom monosaccharide"),
        new Option<int>(new[] {"--customMono3Min", "-c3Min"}, "Minimum value for the third custom monosaccharide"),
        new Option<int>(new[] {"--customMono3Max", "-c3Max"}, "Maximum value for the third custom monosaccharide"),

        new Option<string>(new[] {"--customMono4Name", "-c4Name"}, "Fourth custom monosaccharide name"),
        new Option<int>(new[] {"--customMono4CCount", "-c4C"}, "Carbon count of the fourth custom monosaccharide"),
        new Option<int>(new[] {"--customMono4HCount", "-c4H"}, "Hydrogen count of the fourth custom monosaccharide"),
        new Option<int>(new[] {"--customMono4NCount", "-c4N"}, "Nitrogen count of the fourth custom monosaccharide"),
        new Option<int>(new[] {"--customMono4OCount", "-c4O"}, "Oxygen count of the fourth custom monosaccharide"),
        new Option<decimal>(new[] {"--customMono4Mass", "-c4M"}, "Mass of the fourth custom monosaccharide"),
        new Option<int>(new[] {"--customMono4Min", "-c4Min"}, "Minimum value for the fourth custom monosaccharide"),
        new Option<int>(new[] {"--customMono4Max", "-c4Max"}, "Maximum value for the fourth custom monosaccharide"),

        new Option<string>(new[] {"--customMono5Name", "-c5Name"}, "Fifth custom monosaccharide name"),
        new Option<int>(new[] {"--customMono5CCount", "-c5C"}, "Carbon count of the fifth custom monosaccharide"),
        new Option<int>(new[] {"--customMono5HCount", "-c5H"}, "Hydrogen count of the fifth custom monosaccharide"),
        new Option<int>(new[] {"--customMono5NCount", "-c5N"}, "Nitrogen count of the fifth custom monosaccharide"),
        new Option<int>(new[] {"--customMono5OCount", "-c5O"}, "Oxygen count of the fifth custom monosaccharide"),
        new Option<decimal>(new[] {"--customMono5Mass", "-c5M"}, "Mass of the fifth custom monosaccharide"),
        new Option<int>(new[] {"--customMono5Min", "-c5Min"}, "Minimum value for the fifth custom monosaccharide"),
        new Option<int>(new[] {"--customMono5Max", "-c5Max"}, "Maximum value for the fifth custom monosaccharide"),

        // Additional options
        new Option<decimal>(new[] {"--customReducingMass", "-cRM"}, "Mass of the custom reducing end"),
        new Option<string>(new[] {"--customReducingName", "-cRName"}, "Name of the custom reducing end"),
        new Option<string>(new[] {"--customReducedMassOutput", "-cROut"}, "Output for the reduced mass"),
        new Option<string>(new[] {"--customReducingCCount", "-cRC"}, "Carbon count for the reducing end"),
        new Option<string>(new[] {"--customReducingHCount", "-cRH"}, "Hydrogen count for the reducing end"),
        new Option<string>(new[] {"--customReducingNCount", "-cRN"}, "Nitrogen count for the reducing end"),
        new Option<string>(new[] {"--customReducingOCount", "-cRO"}, "Oxygen count for the reducing end"),
        new Option<string>(new[] {"--customAdductPolarity", "-cAP"}, "Positive or Negative"),
        new Option<decimal>(new[] {"--customAdductMass", "-cAM"}, "Mass of custom adduct"),

        new Option<string>(new[] {"--derivatisation", "-D" }, "Native, Permethylated or Peracetylated derivatisation"),
        new Option<string>(new[] {"--reducedEnd", "-R" }, "Free, Reduced, InstantPC, Rapifluor-MS, 2-aminobenzoic acid, 2-aminobenzamide, Procainamide, Girard's reagent P, and Custom reducing end formats. E.g. \"Reduced\""),
        new Option<string>(new[] {"--adducts", "-A" }, "Neutral, MH+, MNa+, MNH4+, MH-, MFA-, MAA-, MTFA-"),
        new Option<decimal>(new[] {"--massError", "-E" }, "Mass error value, e.g. \"30\" or \"0.6\""),
        new Option<string>(new[] {"--massErrorType", "-T" }, "Mass error type can either be Da or ppm"),
        new Option<bool>(new[] {"--offByOne", "-O" }, "if set to true, enables off-by-one searching for cases of incorrect monoisotopic precursor determination"),
        new Option<string>(new[] {"--file", "-F" }, "Path to the input file, either .mzml, or .txt/.dat (mass list)"), // File upload option
    };

        rootCommand.Description = "A CLI for GlyCombo, allowing rapid assignment of monosaccharide combinations to observed and fragmented precursors in mass spectrometry experiments" + Environment.NewLine + Environment.NewLine + "Example command: GlyComboCLI.exe -F=\".\\example.mzML\" -hMin=1 -hMax=12 -nMin=2 -nMax=8 -sMin=0 -sMax=2 -fMin=0 -fMax=3 -gMin=0 -gMax=2 -D=\"Native\" -R=\"Reduced\" -T=Da -E=\"0.6\"" + Environment.NewLine + Environment.NewLine + "Questions, comments and bug reports:" + Environment.NewLine + "https://github.com/Protea-Glycosciences/GlyComboCLI" + Environment.NewLine + "chris@proteaglyco.com" + Environment.NewLine + "GlyComboCLI release: v0.0";
        rootCommand.Handler = CommandHandler.Create<CommandOptions>(options =>
        {
            if (options.derivatisation != null)
            {
                // Derivatisation
                options.derivatisation = options.derivatisation.ToLower();
                switch (options.derivatisation)
                {
                    case "native":
                    case "permethylated":
                    case "peracetylated":
                        Console.WriteLine($"The derivatisation is {options.derivatisation}");
                        break;

                    default:
                        Console.WriteLine($"{options.derivatisation} is not a valid derivatisation. GlyCombo supports Native, Permethylated and Peracetylated derivitisations. Search terminated.");
                        return;
                }
            }

            if (options.reducedEnd != null)
            {
                // Reducing End
                options.reducedEnd = options.reducedEnd.ToLower();
                switch (options.reducedEnd)
                {
                    case "free":
                    case "reduced":
                    case "instantPC":
                    case "rapifluor-MS":
                    case "2-aminobenzoic acid":
                    case "2-aminobenzamide":
                    case "procainamide":
                    case "girard's reagent p":
                        Console.WriteLine($"The reduced end is {options.reducedEnd}");
                        break;

                    case "custom":
                        Console.WriteLine($"The reduced end is {options.reducedEnd}");
                        break;

                    default:
                        Console.WriteLine($"{options.reducedEnd} is not a valid option. GlyCombo supports Free, Reduced, InstantPC, Rapifluor-MS, 2-aminobenzoic acid, 2-aminobenzamide, Procainamide, Girard's reagent P, and Custom. Search terminated.");
                        return;
                }
            }

            if (options.massErrorType != null)
            {
                // Mass Error Type
                options.massErrorType = options.massErrorType.ToLower();
                if (options.massErrorType == "da" || options.massErrorType == "ppm")
                {
                    Console.WriteLine($"The mass error is {options.massError} {options.massErrorType}");
                }
                else
                {
                    Console.WriteLine($"Mass error and mass error type must be selected. {options.massErrorType} is not a valid option.");
                    return;
                }
            }

            async Task mzMLProcess()
            {
                List<string> fileNames = [];
                List<int> fileScans = [];
                string polarity = "";
                List<decimal> precursors = [];
                // Read each line from the given file
                StreamReader sr = new(options.file);

                // Parse each line of the mzml to extract important information from MS2 scans of the mzML (polarity, precursor m/z, charge state, scan # for given MS2)
                for (line = sr.ReadLine(); line != null; line = sr.ReadLine())
                {
                    // Problem: Bruker and Thermo mzmls have all lines in different positions
                    // Thermo order: Spectrum index (including scan#), then "ms level" value="2", then "negative", then "selected ion m/z", then "charge state"
                    // Bruker order: Spectrum index (including scan#), then "negative scan", then "ms level" value ="2", then "charge state", then "selected ion m/z"
                    // Sciex doesn't use scan #, so we've adapted cycle and experiment number to make (X.Y) representation instead
                    // Code modified to perform this per spectrum, rather than trying to hard code by line positions

                    // find lines containing positive or negative mode, all vendors follow this requirement
                    if (line.Contains("MS:1000129"))
                    {
                        polarity = "negative";
                    }
                    if (line.Contains("MS:1000130"))
                    {
                        polarity = "positive";
                    }

                    // find lines containing retention time, to minute time scale
                    if (line.Contains("MS:1000016"))
                    {
                        // Bruker records retention time by the second
                        if (line.Contains("unitName=\"second\""))
                        {
                            // split the line containing this by "
                            RTLine = line.Split("\"");
                            // After the 7th ", that's where the charge can be found, so convert it from string array into int
                            retentionTime = decimal.Parse(RTLine[7]) / 60;
                        }
                        else
                        // whereas Thermo/Sciex/Waters/Agilent records RT by the minute
                        {
                            // split the line containing this by "
                            RTLine = line.Split("\"");
                            // After the 7th ", that's where the charge can be found, so convert it from string array into int
                            retentionTime = decimal.Parse(RTLine[7]);
                        }
                    }

                    // find lines containing total ion chromatogram intensity for that scan, only supported by Agilent, Thermo, Sciex, and Waters
                    if (line.Contains("MS:1000285"))
                    {
                        // split the line containing this by "
                        TICLine = line.Split("\"");
                        // After the 7th ", that's where the charge can be found, so convert it from string array into int
                        TIC = decimal.Parse(TICLine[7], System.Globalization.NumberStyles.AllowExponent | System.Globalization.NumberStyles.AllowDecimalPoint);
                    }

                    // find lines containing the precursor
                    if (line.Contains("\"selected ion m/z\""))
                    {
                        // split the line containing this by "
                        precursorLine = line.Split("\"");
                        // After the 7th ", that's where the precursor m/z can be found, so convert it from string array into decimal for accuracy
                        precursor = Math.Round(decimal.Parse(precursorLine[7]), 6);
                    }
                    // find lines containing the charge. Some vendors need this to be added via MSConvert as they don't automatically provide a charge state
                    if (line.Contains("\"charge state\""))
                    {
                        // split the line containing this by "
                        chargeLine = line.Split("\"");
                        // After the 7th ", that's where the charge can be found, so convert it from string array into int
                        charge = int.Parse(chargeLine[7]);
                    }
                    // Waters lines sometimes contain offsets with scan numbers outside of the spectra sections, so this is to ignore those lines
                    if (line.Contains("offset idRef="))
                    {
                        continue;
                    }

                    // find lines containing the scan number
                    if (line.Contains("scan=")
                        // To ensure we don't pick up the Thermo spectrum title
                        && line.Contains("defaultArrayLength")
                        // To ensure we don't pick up the Waters ms scans
                        && !line.Contains("function=")
                        // To ensure we don't pick up the Agilent ms scans
                        && !line.Contains("id=\"scanId"))
                    {
                        // split the line containing this by "
                        scanLine = line.Split("\"");
                        // After the 3rd ", that's where the scan # can be found. This is the Thermo-specific extraction:
                        if (line.Contains("controller"))
                        {
                            scanNumber = scanLine[3].Replace("controllerType=0 controllerNumber=1 scan=", "");
                        }
                        // And this is for Bruker
                        else
                        {
                            scanNumber = scanLine[3].Replace("scan=", "");
                        }
                    }

                    // Agilent specific scan number interpreation, since they use structure of: id="scanId=4620"
                    if (line.Contains("id=\"scanId")
                    && line.Contains("defaultArrayLength"))
                    {
                        //split the line containing scan number by =
                        scanLine = line.Split("=");
                        // Scan # is after the 3rd "="
                        scanNumber = scanLine[3].Replace("\" defaultArrayLength", "");
                    }

                    // Waters specific scan number interpretation, sometimes merged scans occur so we need to account for those as well
                    if (line.Contains("function=")
                    && line.Contains(" process=")
                    && line.Contains("defaultArrayLength"))
                    {
                        //split the line containing scan number by =
                        scanLine = line.Split("=");
                        // If we convert without scan summing (e.g. MSConvert), we only have individual "spectrum" lists rather than combined "spectra". This selects for MSConvert output
                        if (!line.Contains(" spectrum=")
                            && !line.Contains(" spectra="))
                        {
                            // Scan # is after the 5th "="
                            scanNumber = scanLine[5].Replace("\" defaultArrayLength", "");
                        }
                        else
                        // This is for Waters DataConnect output where spectra can be merged (or unmerged).
                        {
                            if (line.Contains("merged"))
                            {
                                // Scan # is after the 7th "="
                                scanNumber = scanLine[7].Replace("\" defaultArrayLength", "");
                            }
                            else
                            {
                                // Scan # is after the 6th "=" as it is a single spectrum
                                scanNumber = scanLine[6].Replace("\" defaultArrayLength", "");
                            }
                        }

                    }

                    // Sciex specific scan number interpretation
                    if (line.Contains(" cycle=")
                        && line.Contains(" experiment=")
                        && line.Contains("defaultArrayLength"))
                    {
                        //split the line containing cycle and experiment number by =
                        scanLine = line.Split("=");
                        // Cycle # is after the 5th "="
                        string cycleScan = scanLine[5].Replace(" experiment", ".");
                        string experimentScan = scanLine[6].Replace("\" defaultArrayLength", "");
                        scanNumber = cycleScan + experimentScan;
                    }

                    // Wrapping everything together to form a neutral precursor mass, only if requirements are met
                    
                    if (line.Contains("</spectrum>"))
                    {
                        if (precursor != 0 && charge != 0)
                        {
                            // Convert precursor m/z and z to obtain neutral precursor mass
                            if (polarity == "negative") {
                                neutralPrecursorListmzml += Convert.ToString(charge * precursor + (charge * 1.007276m)) + Environment.NewLine;
                                charge = -charge;
                            }
                            else if (polarity == "positive") {
                                neutralPrecursorListmzml += Convert.ToString(charge * precursor - (charge * 1.007276m)) + Environment.NewLine;
                            }
                            // Put the scan value into a list of scan numbers that feature MS2.
                            scans.Add(decimal.Parse(scanNumber));
                            // Adds charge to a list for the end report
                            charges.Add(charge);
                            // Add RT to a list for the end report
                            retentionTimes.Add(retentionTime);
                            // Add TIC to a list for the end report
                            TICs.Add(TIC);
                            // Add stripped file name to the index
                            string fileName = options.file.Substring(options.file.LastIndexOf('\\') + 1);
                            files.Add(fileName);
                        }
                        precursor = 0;
                        charge = 0;
                        scanNumber = "";
                        polarity = "";
                        retentionTime = 0;
                        TIC = 0;
                    }
                    string fileNameOutput = options.file.Substring(options.file.LastIndexOf('\\') + 1);
                }
                if (scans.Count == 0 && Path.GetExtension(options.file) == ".mzml")
                {
                    Console.WriteLine("No MS2 found in the given mzML file. Please confirm the selected file has MS2 scans, or select a different file.");
                    return;
                }
                else
                {
                    // Provide number of scans for each filename
                    Console.WriteLine("File " + options.file + " has completed uploading with a total number of " + scans.Count + " MS2 scans identified.");
                }
                ProcessingSteps().GetAwaiter().GetResult();
            }

    static string ReadMassFileWithSeparator(string filePath, string separator)
            {
                // Processes the neutral mass input with separators
                string[] lines = File.ReadAllLines(filePath);
                if (separator == ",")
                {
                    for (int i = 0; i < lines.Length; i++)
                    {
                        lines[i] = lines[i].Replace(",", Environment.NewLine);
                    }
                }
                return string.Join(separator, lines);
            }


            mzMLProcess().GetAwaiter().GetResult();

            async Task ProcessingSteps() {
                try
                {
                    solutionMultiples = "";
                    // Define the components in the combinatorial analysis: native, permethylated, peracetylated
                    if (options.derivatisation == "native")
                    {
                        // Native
                        dhex = 146.057908m; // permethylated mass = 174.089210 chemical formula = C8H14O4
                        hex = 162.052823m; // permethylated mass = 204.099775 chemical formula = C9H16O5
                        hexnac = 203.079372m; // permethylated mass = 245.126324 chemical formula = C11H19NO5
                        hexn = 161.068808m; // permethylated mass = 217.131409 chemical formula = C10H19NO4
                        hexa = 176.032088m; // permethylated mass = 218.079040 chemical formula = C9H14O6
                        dhexnac = 187.084458m; // permethylated mass = 215.115759 chemical formula = C10H17N1O4
                        pent = 132.042258m; // permethylated mass = 160.073560 chemical formula = C7H12O4
                        kdn = 250.068867m; // permethylated mass = 320.147120 chemical formula = C14H24O8
                        neuac = 291.095416m; // permethylated mass = 361.173669 chemical formula = C16H27NO8
                        neugc = 307.090331m; // permethylated mass = 391.184234 chemical formula = C17H29NO9
                        phos = 79.966331m; // permethylated mass = 93.981983 chemical formula = CH3O3P
                        lneuac = 273.0848518m;
                        eeneuac = 319.1267166m;
                        dneuac = 318.1427011m;
                        amneuac = 290.1114009m;
                        acetyl = 42.010565m;
                        lneugc = 289.0797664m;
                        eeneugc = 335.1216313m;
                        dneugc = 306.1063155m;
                        amneugc = 334.1376157m;
                        sulf = 79.956815m; // SO3
                    }
                    if (options.derivatisation == "permethylated")
                    {
                        // Permethylated
                        dhex = 174.089210m; // chemical formula = C8H14O4
                        hex = 204.099775m; //  chemical formula = C9H16O5
                        hexnac = 245.126324m; //  chemical formula = C11H19NO5
                        hexn = 203.115758m; //  chemical formula = C9H17NO4
                        hexa = 218.079040m; //  chemical formula = C9H14O6
                        dhexnac = 215.115758m; //  chemical formula = C10H17N1O4
                        pent = 160.073560m; //  chemical formula = C7H12O4
                        kdn = 320.147120m; // chemical formula = C14H24O8
                        neuac = 361.173669m; // chemical formula = C16H27NO8
                        neugc = 391.184234m; // chemical formula = C17H29NO9
                        phos = 93.981980m; // chemical formula = PO3H3C1
                        sulf = 65.941165m; // chemical formula = SO3C-1H-2
                    }
                    if (options.derivatisation == "peracetylated")
                    {
                        // Peracetylated
                        dhex = 230.079038m; // chemical formula = C10H14O6
                        hex = 288.084517m; // chemical formula = C12H16O8
                        hexnac = 287.100501m; // chemical formula = C12H17NO7
                        hexn = 287.100501m; // chemical formula = C12H17NO7
                        hexa = 260.053217m; // chemical formula = C10H12O8
                        dhexnac = 247.105587m; // chemical formula = C10H17NO6
                        pent = 216.063388m; // chemical formula = C9H12O6
                        kdn = 376.100561m; // chemical formula = C15H20O11
                        neuac = 417.127110m; // chemical formula = C17H23NO11
                        neugc = 475.132593m; // chemical formula = C19H25NO13
                        phos = 37.955765m; // chemical formula = PO2C-2H-1
                        sulf = 37.946250m; // chemical formula = SO2C-2H-2
                    }

                    // Add the components to combinatorial analysis based on which monosaccharides the user chooses to include
                    if (options.HexMax > 0)
                    {
                        currentMonosaccharideSelection += "Hex(" + options.HexMin + "-" + options.HexMax + "), ";
                        numbers.Add(hex);
                    }

                    if (options.HexAMax > 0)
                    {
                        currentMonosaccharideSelection += "HexA(" + options.HexAMin + "-" + options.HexAMax + "), ";
                        numbers.Add(hexa);
                    }

                    if (options.dHexMax > 0)
                    {
                        currentMonosaccharideSelection += "dHex(" + options.dHexMin + "-" + options.dHexMax + "), ";
                        numbers.Add(dhex);
                    }

                    if (options.HexNAcMax > 0)
                    {
                        currentMonosaccharideSelection += "HexNAc(" + options.HexNAcMin + "-" + options.HexNAcMax + "), ";
                        numbers.Add(hexnac);
                    }

                    if (options.HexNMax > 0)
                    {
                        currentMonosaccharideSelection += "HexN(" + options.HexNMin + "-" + options.HexNMax + "), ";
                        numbers.Add(hexn);
                    }

                    if (options.dHexNAcMax > 0)
                    {
                        currentMonosaccharideSelection += "dHexNAc(" + options.dHexNAcMin + "-" + options.dHexNAcMax + "), ";
                        numbers.Add(dhexnac);
                    }

                    if (options.PentMax > 0)
                    {
                        currentMonosaccharideSelection += "Pent(" + options.PentMin + "-" + options.PentMax + "), ";
                        numbers.Add(pent);
                    }

                    if (options.KDNMax > 0)
                    {
                        currentMonosaccharideSelection += "KDN(" + options.KDNMin + "-" + options.KDNMax + "), ";
                        numbers.Add(kdn);
                    }

                    if (options.Neu5AcMax > 0)
                    {
                        currentMonosaccharideSelection += "Neu5Ac(" + options.Neu5AcMin + "-" + options.Neu5AcMax + "), ";
                        numbers.Add(neuac);
                    }

                    if (options.Neu5GcMax > 0)
                    {
                        currentMonosaccharideSelection += "Neu5Gc(" + options.Neu5GcMin + "-" + options.Neu5GcMax + "), ";
                        numbers.Add(neugc);
                    }

                    if (options.PhosMax > 0)
                    {
                        currentMonosaccharideSelection += "Phos(" + options.PhosMin + "-" + options.PhosMax + "), ";
                        numbers.Add(phos);
                    }

                    if (options.SulfMax > 0)
                    {
                        currentMonosaccharideSelection += "Sulf(" + options.SulfMin + "-" + options.SulfMax + "), ";
                        numbers.Add(sulf);
                    }

                    if (options.lNeuAcMax > 0)
                    {
                        currentMonosaccharideSelection += "lNeuAc(" + options.lNeuAcMin + "-" + options.lNeuAcMax + "), ";
                        numbers.Add(lneuac);
                    }

                    if (options.eeNeuAcMax > 0)
                    {
                        currentMonosaccharideSelection += "eeNeuAc(" + options.eeNeuAcMin + "-" + options.eeNeuAcMax + "), ";
                        numbers.Add(eeneuac);
                    }

                    if (options.dNeuAcMax > 0)
                    {
                        currentMonosaccharideSelection += "dNeuAc(" + options.dNeuAcMin + "-" + options.dNeuAcMax + "), ";
                        numbers.Add(dneuac);
                    }

                    if (options.amNeuAcMax > 0)
                    {
                        currentMonosaccharideSelection += "amNeuAc(" + options.amNeuAcMin + "-" + options.amNeuAcMax + "), ";
                        numbers.Add(amneuac);
                    }

                    if (options.acetylMax > 0)
                    {
                        currentMonosaccharideSelection += "Acetyl(" + options.acetylMin + "-" + options.acetylMax + "), ";
                        numbers.Add(acetyl);
                    }

                    if (options.lNeuGcMax > 0)
                    {
                        currentMonosaccharideSelection += "lNeuGc(" + options.lNeuGcMin + "-" + options.lNeuGcMax + "), ";
                        numbers.Add(lneugc);
                    }

                    if (options.eeNeuGcMax > 0)
                    {
                        currentMonosaccharideSelection += "eeNeuGc(" + options.eeNeuGcMin + "-" + options.eeNeuGcMax + "), ";
                        numbers.Add(eeneugc);
                    }

                    if (options.dNeuGcMax > 0)
                    {
                        currentMonosaccharideSelection += "dNeuGc(" + options.dNeuGcMin + "-" + options.dNeuGcMax + "), ";
                        numbers.Add(dneugc);
                    }

                    if (options.amNeuGcMax > 0)
                    {
                        currentMonosaccharideSelection += "amNeuGc(" + options.amNeuGcMin + "-" + options.amNeuGcMax + "), ";
                        numbers.Add(amneugc);
                    }

                    if (options.customMono1Max > 0)
                    {
                        currentMonosaccharideSelection += options.customMono1Name + "(" + options.customMono1Min + "-" + options.customMono1Max + "), ";
                        numbers.Add(options.customMono1Mass);
                        monoCustom1 = true;
                    }

                    if (options.customMono2Max > 0)
                    {
                        currentMonosaccharideSelection += options.customMono2Name + "(" + options.customMono2Min + "-" + options.customMono2Max + "), ";
                        numbers.Add(options.customMono2Mass);
                        monoCustom2 = true;
                    }

                    if (options.customMono3Max > 0)
                    {
                        currentMonosaccharideSelection += options.customMono3Name + "(" + options.customMono3Min + "-" + options.customMono3Max + "), ";
                        numbers.Add(options.customMono3Mass);
                        monoCustom3 = true;
                    }

                    if (options.customMono4Max > 0)
                    {
                        currentMonosaccharideSelection += options.customMono4Name + "(" + options.customMono4Min + "-" + options.customMono4Max + "), ";
                        numbers.Add(options.customMono4Mass);
                        monoCustom4 = true;
                    }

                    if (options.customMono5Max > 0)
                    {
                        currentMonosaccharideSelection += options.customMono5Name + "(" + options.customMono5Min + "-" + options.customMono5Max + "), ";
                        numbers.Add(options.customMono5Mass);
                        monoCustom5 = true;
                    }


                    // Process for multiple targets conditionally based on text box or mzml input
                    string fileExtension = Path.GetExtension(options.file);
                    if (fileExtension.ToLower() == ".txt" || fileExtension.ToLower() == ".dat")
                    {
                        targetString = ReadMassFileWithSeparator(options.file, Environment.NewLine);
                    }
                    else if (fileExtension.ToLower() == ".mzml")
                    {
                        targetString = neutralPrecursorListmzml;
                    }
                    else
                    {
                        Console.WriteLine("The file extension " + fileExtension + " is not supported.");
                        return;
                    }

                    // Turn that input into a list of masses
                    targetStrings = new(
                    targetString.Split(new string[] { "\n" },
                    StringSplitOptions.RemoveEmptyEntries));
                    targets = targetStrings.ConvertAll(decimal.Parse);

                    // Adduct calculation
                    // This can result in huge combinatorial searches but it's there for the user as an option
                    // if mzml input used, force M+H and M-H, then let the user add on other adducts (problem with this is that positive mode will have negative adducts etc)

                    // Making a separate list to then be used for target building
                    targetAdductsProcessing = targets;
                    targetAdducts = new List<decimal>();

                    // Only trigger this if something other than M is selected
                    if (options.adducts != null)
                    {
                        // mzML input has been processed as de / protonated to generate a neutral mass list, so adducts offset is +/- 1 Da for the respective negative/positive adducts
                        // We also don't bother with doing M, M+H, and M-H because they are all the same after mzML processing (M+H and M-H become M)
                        if (fileExtension == ".mzML")
                        {

                            // This all needs to be revised to find if the options.adducts CONTAINS the adduct text, rather than ==. This is because people can submit more than one adduct.
                            // Subtracting H- from all targets and saving that as a new list
                            if (options.adducts.Split(",").Select(a => a.Trim()).Contains("MH-") ||
                            options.adducts.Split(",").Select(a => a.Trim()).Contains("Mneutral") ||
                            options.adducts.Split(",").Select(a => a.Trim()).Contains("MH+"))
                            {
                                searchRepeats += 1;
                                targetsToAdd = targetAdductsProcessing.Count;
                                for (int o = 0; o < targetsToAdd; o++)
                                {
                                    targetAdducts.Add(targetAdductsProcessing[o]);
                                }
                            }
                            // M+COOH adduct calculation
                            if (options.adducts.Split(",").Select(a => a.Trim()).Contains("MFA+"))
                            {
                                searchRepeats += 1;
                                targetsToAdd = targetAdductsProcessing.Count;
                                for (int o = 0; o < targetsToAdd; o++)
                                {
                                    targetAdducts.Add(targetAdductsProcessing[o] - (decimal)44.998201 - (decimal)1.007276);
                                }
                            }
                            // M+acetic acid adduct calculation
                            if (options.adducts.Split(",").Select(a => a.Trim()).Contains("MAA+"))
                            {
                                searchRepeats += 1;
                                targetsToAdd = targetAdductsProcessing.Count;
                                for (int o = 0; o < targetsToAdd; o++)
                                {
                                    targetAdducts.Add(targetAdductsProcessing[o] - (decimal)59.013851 - (decimal)1.007276);
                                }
                            }
                            // M+TFA adduct calculation
                            if (options.adducts.Split(",").Select(a => a.Trim()).Contains("MTFA+"))
                            {
                                searchRepeats += 1;
                                targetsToAdd = targetAdductsProcessing.Count;
                                for (int o = 0; o < targetsToAdd; o++)
                                {
                                    targetAdducts.Add(targetAdductsProcessing[o] - (decimal)112.985586 - (decimal)1.007276);
                                }
                            }
                            // M+Na adduct calculation
                            if (options.adducts.Split(",").Select(a => a.Trim()).Contains("MNa+"))
                            {
                                searchRepeats += 1;
                                targetsToAdd = targetAdductsProcessing.Count;
                                // This runs to completion
                                for (int o = 0; o < targetsToAdd; o++)
                                {
                                    targetAdducts.Add(targetAdductsProcessing[o] - (decimal)22.989218 + (decimal)1.007276);
                                }
                            }
                            // M+K adduct calculation
                            if (options.adducts.Split(",").Select(a => a.Trim()).Contains("MK+"))
                            {
                                searchRepeats += 1;
                                targetsToAdd = targetAdductsProcessing.Count;
                                for (int o = 0; o < targetsToAdd; o++)
                                {
                                    targetAdducts.Add(targetAdductsProcessing[o] - (decimal)38.963158 + (decimal)1.007276);
                                }
                            }
                            // M+NH4 adduct calculation
                            if (options.adducts.Split(",").Select(a => a.Trim()).Contains("MNH4+"))
                            {
                                searchRepeats += 1;
                                targetsToAdd = targetAdductsProcessing.Count;
                                for (int o = 0; o < targetsToAdd; o++)
                                {
                                    targetAdducts.Add(targetAdductsProcessing[o] - (decimal)18.033823 + (decimal)1.007276);
                                }
                            }
                            // Custom adduct calculcation
                            if (options.customAdductMass > 0)
                            {
                                searchRepeats += 1;
                                decimal adductCustom;
                                targetsToAdd = targetAdductsProcessing.Count;
                                // Processing of customAdductMassText to account for mzML assuming a protonated/deprotonated precursor
                                if (options.customAdductPolarity == "positive") // Protonated
                                {
                                    adductCustom = options.customAdductMass - (decimal)1.007276;
                                }
                                else if (options.customAdductPolarity == "negative")// Deprotonated
                                {
                                    adductCustom = options.customAdductMass + (decimal)1.007276;
                                }
                                else
                                {
                                    return;
                                }
                                for (int o = 0; o < targetsToAdd; o++)
                                {
                                    targetAdducts.Add(targetAdductsProcessing[o] - adductCustom);
                                }
                            }
                        }
                        // Text input is singly charged m/z values that are observed via experiments like MALDI-MS of permethylated glycans so no modification of mass is needed.
                        if (fileExtension == ".txt" || fileExtension.ToLower() == ".dat")
                        {
                            // Subtracting H- from all targets and saving that as a new list
                            if (options.adducts.Split(',').Select(a => a.Trim()).Contains("MH-"))
                            {
                                targetsToAdd = targetAdductsProcessing.Count;
                                for (int o = 0; o < targetsToAdd; o++)
                                {
                                    targetAdducts.Add(targetAdductsProcessing[o] + (decimal)1.007276);
                                }
                            }
                            // Appending the list with the original text if the user has M selected
                            // Fix this later, just the adduct M will be found incorrectly with any M, e.g. MH+
                            if (options.adducts.Split(',').Select(a => a.Trim()).Contains("MNeutral"))
                            {
                                targetsToAdd = targetAdductsProcessing.Count;
                                for (int o = 0; o < targetsToAdd; o++)
                                {
                                    targetAdducts.Add(targetAdductsProcessing[o]);
                                }
                            }
                            // M+COOH adduct calculation
                            if (options.adducts.Split(',').Select(a => a.Trim()).Contains("MFA-"))
                            {
                                targetsToAdd = targetAdductsProcessing.Count;
                                for (int o = 0; o < targetsToAdd; o++)
                                {
                                    targetAdducts.Add(targetAdductsProcessing[o] - (decimal)44.998201);
                                }
                            }
                            // M+acetic acid adduct calculation
                            if (options.adducts.Split(',').Select(a => a.Trim()).Contains("MAA-"))
                            {
                                targetsToAdd = targetAdductsProcessing.Count;
                                for (int o = 0; o < targetsToAdd; o++)
                                {
                                    targetAdducts.Add(targetAdductsProcessing[o] - (decimal)59.013851);
                                }
                            }
                            // M+TFA adduct calculation
                            if (options.adducts.Split(',').Select(a => a.Trim()).Contains("MTFA-"))
                            {
                                targetsToAdd = targetAdductsProcessing.Count;
                                for (int o = 0; o < targetsToAdd; o++)
                                {
                                    targetAdducts.Add(targetAdductsProcessing[o] - (decimal)112.985586);
                                }
                            }
                            // M+H adduct calculation
                            if (options.adducts.Split(',').Select(a => a.Trim()).Contains("MH+"))
                            {
                                targetsToAdd = targetAdductsProcessing.Count;
                                for (int o = 0; o < targetsToAdd; o++)
                                {
                                    targetAdducts.Add(targetAdductsProcessing[o] - (decimal)1.007276);
                                }
                            }
                            // M+Na adduct calculation
                            if (options.adducts.Split(',').Select(a => a.Trim()).Contains("MNa+"))
                            {
                                targetsToAdd = targetAdductsProcessing.Count;
                                for (int o = 0; o < targetsToAdd; o++)
                                {
                                    targetAdducts.Add(targetAdductsProcessing[o] - (decimal)22.989218);
                                }
                            }
                            // M+K adduct calculation
                            if (options.adducts.Split(',').Select(a => a.Trim()).Contains("MK+"))
                            {
                                targetsToAdd = targetAdductsProcessing.Count;
                                for (int o = 0; o < targetsToAdd; o++)
                                {
                                    targetAdducts.Add(targetAdductsProcessing[o] - (decimal)38.963158);
                                }
                            }
                            // M+NH4 adduct calculation
                            if (options.adducts.Split(',').Select(a => a.Trim()).Contains("MNH4+"))
                            {
                                targetsToAdd = targetAdductsProcessing.Count;
                                for (int o = 0; o < targetsToAdd; o++)
                                {
                                    targetAdducts.Add(targetAdductsProcessing[o] - (decimal)18.033823);
                                }
                            }
                            // Custom adduct calculcation
                            if (options.customAdductMass > 0)
                            {
                                targetsToAdd = targetAdductsProcessing.Count;
                                for (int o = 0; o < targetsToAdd; o++)
                                {
                                    targetAdducts.Add(targetAdductsProcessing[o] - options.customAdductMass);
                                }
                            }
                        }

                        targets = targetAdducts;
                    }
                    // If the user doesn't specify adducts, add straight to the list for searching 
                    else
                    {
                        Console.WriteLine("No adducts specified, using defaults");
                        searchRepeats += 1;
                        targetsToAdd = targetAdductsProcessing.Count;
                        for (int o = 0; o < targetsToAdd; o++)
                        {
                            targetAdducts.Add(targetAdductsProcessing[o]);
                        }
                        targets = targetAdducts;
                    }

                    // For enabling off-by-one errors. Thermo is pretty good at correcting the selected ion m/z when it picks an isotopic distribution, but might be useful for others
                    if (options.offByOne == true)
                    {
                        searchRepeats += 1;
                        // For each target in the list, remove one hydrogen to account for the C13 isotope being picked instead of monoisotopic (negative mode only)
                        targetsToAdd = targets.Count;
                        for (int o = 0; o < targetsToAdd; o++)
                        {
                            targets.Add(targets[o] - (decimal)1.007276);
                        }
                    }

                    // Early processing of target list, breaking it down so that the reducing ends are removed
                    if (options.derivatisation == "native")
                    {
                        // Assuming `options.reducingEnd` is a string with values like "Free", "Reduced", etc.
                        switch (options.reducedEnd)
                        {
                            case "free":
                                reducedEnd = "free";
                                targets = targets.Select(z => z - 18.010555m).ToList();
                                break;
                            case "reduced":
                                reducedEnd = "reduced";
                                targets = targets.Select(z => z - 20.026195m).ToList();
                                break;
                            case "instantPC":
                                reducedEnd = "instantpc";
                                targets = targets.Select(z => z - (18.010555m + 261.14773m)).ToList();
                                break;
                            case "rapifluor-ms":
                                reducedEnd = "rapifluor-ms";
                                targets = targets.Select(z => z - (18.010555m + 311.17461m)).ToList();
                                break;
                            case "2aa":
                                reducedEnd = "2aa";
                                targets = targets.Select(z => z - (18.010555m + 121.052774m)).ToList();
                                break;
                            case "2ab":
                                reducedEnd = "2ab";
                                targets = targets.Select(z => z - (18.010555m + 120.068758m)).ToList();
                                break;
                            case "procainamide":
                                reducedEnd = "procainamide";
                                targets = targets.Select(z => z - (18.010555m + 219.173557m)).ToList();
                                break;
                            case "girp":
                                reducedEnd = "girp";
                                targets = targets.Select(z => z - (18.010555m + 134.07182m)).ToList();
                                break;
                            case "custom":
                                reducedEnd = "custom";
                                targets = targets.Select(z => z - (18.010555m + options.customReducingMass)).ToList();
                                break;
                            default:
                                throw new ArgumentException($"Invalid reducing end type: {options.reducedEnd}");
                        }

                    }
                    else if (options.derivatisation == "permethylated")
                    {
                        switch (options.reducedEnd)
                        {
                            case "free":
                                targets = targets.Select(z => z - (18.010555m + 28.031300m)).ToList();
                                break;
                            case "reduced":
                                targets = targets.Select(z => z - (20.026195m + 42.046950m)).ToList();
                                break;
                            case "custom":
                                targets = targets.Select(z => z - (18.010555m + options.customReducingMass)).ToList();
                                break;
                            default:
                                break;
                        }
                    }
                    else if (options.derivatisation == "peracetylated")
                    {
                        switch (options.reducedEnd)
                        {
                            case "free":
                                targets = targets.Select(z => z - (18.010555m + 84.021129m)).ToList();
                                break;
                            case "reduced":
                                targets = targets.Select(z => z - (20.026195m + 126.031694m)).ToList();
                                break;
                            case "custom":
                                targets = targets.Select(z => z - (18.010555m + options.customReducingMass)).ToList();
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Derivatization of " + options.derivatisation + " is not accepted.");
                        return;
                    }

                    // Define the upper and lower error tolerances for search
                    if (options.massErrorType == "da" || options.massErrorType == "ppm")
                    {
                        errorTol = options.massError;
                    }
                    else
                    {
                        Console.WriteLine("Mass error type of " + options.massErrorType + " is not accepted.");
                        return;
                    }
                    Console.WriteLine("Beginning processing...");

                    await Task.Run(() => glyComboProcess());
                }
                finally
                {
                    solutionProcess = "";
                    targets.Clear();
                    numbers.Clear();
                    solutions = "";
                }
            }

            void glyComboProcess()
            {
                iterations = 0;
                Sum_up(numbers, targets, options);
                solutions = "";
                // Pop-up to let the user know the search has finished
                new Thread(() => { Console.WriteLine("GlyCombo has finished running." + Environment.NewLine + ((solutionMultiples.Length - solutionMultiples.Replace(Environment.NewLine, string.Empty).Length) / 2) + " monosaccharide combinations identified over " + iterations + " iterations." + Environment.NewLine); }).Start();
                solutionProcess = "";
            }

            // Process to match glycan compositions by sum_up_recursive
            void Sum_up(List<decimal> numbers, List<decimal> targets, CommandOptions options)
            {
                // let the user know the search is running
                Console.WriteLine("Search started.");
                Console.WriteLine("Processing " + targets.Count + " precursors.");

                // For each target in the list
                for (int i = 0; i < targets.Count; i++)
                {
                    // Updates the progress, every 20% of targets
                    int currentPercentage = (int)Math.Floor((double)i / targets.Count * 100);
                    int currentStep = currentPercentage / 20;
                    int previousStep = (int)Math.Floor((double)(i - 1) / targets.Count * 100 / 20);
                    if (currentStep != previousStep && currentPercentage < 100)
                    {
                        Console.WriteLine(DateTime.Now + " Progress: " + currentPercentage + "%");
                    }
                    bool targetFound = false;
                    // Define the upper and lower error tolerances for search
                    if (options.massErrorType == "da")
                    {
                        targetLow = targets[i] - options.massError;
                        targetHigh = targets[i] + options.massError;
                    }
                    else if (options.massErrorType == "ppm")
                    {
                        targetLow = targets[i] - (targets[i] * (options.massError / 1000000));
                        targetHigh = targets[i] + (targets[i] * (options.massError / 1000000));
                    }
                    decimal target = targets[i];
                    Sum_up_recursive(numbers, target, [], targetFound, i, options);
                };

                // Write processed data to csv file
                string solutionHeader = "";
                string skylineSolutionHeader = "";
                string skylineSolutionMultiplesPreTrim = "";
                string skylineSolutionMultiples = "";
                string fileExtension = Path.GetExtension(options.file);
                string outputFilePath = Path.Combine(
                    Path.GetDirectoryName(options.file),
                    Path.GetFileNameWithoutExtension(options.file) + "_result" + ".csv"
                );

                if (fileExtension == ".mzML")
                {
                    solutionHeader = "Composition,Observed mass,Theoretical mass,Molecular Formula,Mass error,Scan number,Precursor Charge,Retention Time,TIC,File Name";
                    skylineSolutionHeader = "Molecule List Name,Molecule Name,Observed mass,Theoretical mass,Molecular Formula,Mass error,Scan number,Precursor Charge,Retention Time,TIC,Molecule Note";
                    // Process the SolutionMultiples string in a way that generates an output compatible with Skyline with no user intervention
                    skylineSolutionMultiplesPreTrim = (solutionMultiples.Insert(0, Environment.NewLine)).Replace(Environment.NewLine, Environment.NewLine + "GlyCombo,");
                    skylineSolutionMultiples = skylineSolutionMultiplesPreTrim.Substring(0, skylineSolutionMultiplesPreTrim.Length - 10);
                    File.WriteAllText(Path.Combine(
                        Path.GetDirectoryName(outputFilePath),
                        Path.GetFileNameWithoutExtension(outputFilePath) + "_SkylineImport.csv"),
                        skylineSolutionHeader + skylineSolutionMultiples
                    );
                }
                else
                {
                    solutionHeader = "Composition,Observed mass,Theoretical mass,ChemicalFormula,Mass error";
                    File.WriteAllText(outputFilePath, solutionHeader + Environment.NewLine + solutionMultiples);
                }

                Console.WriteLine("File processing complete. Output written to: " + outputFilePath);

                // Converting precursor list to series of strings for subsequent confirmation
                string combinedTargets = string.Join(Environment.NewLine, targets.ToArray());
                string submitOutput = Environment.NewLine + "GlyCombo search output" + Environment.NewLine;
                submitOutput += "<Error tolerance> " + options.massError + "," + options.massErrorType + Environment.NewLine;
                submitOutput += "<Reducing end> " + options.reducedEnd + Environment.NewLine;
                if (options.reducedEnd.ToString() == "Custom")
                {
                    submitOutput += "Custom reducing end: Name, Mass, #C, #H, #N, #O";
                    submitOutput += "<Custom reducing end> " + options.customReducingName + "," + options.customReducedMassOutput + "," + options.customReducingCCount + "," + options.customReducingHCount + "," + options.customReducingOCount + "," + options.customReducingNCount;
                }
                submitOutput += "<Derivatisation> " + options.derivatisation + Environment.NewLine;
                if (options.offByOne == true)
                {
                    submitOutput += "<OffByOne enabled> " +  Environment.NewLine;
                }
                submitOutput += "## Monosaccharides: Monosaccharide1(Min-Max), Monosaccharide2(Min-Max)" + Environment.NewLine;
                submitOutput += currentMonosaccharideSelection + Environment.NewLine;
                if (options.customMono1Max > 0 || options.customMono2Max > 0 || options.customMono3Max > 0 || options.customMono4Max > 0 || options.customMono5Max > 0)
                {
                    submitOutput += "## CustomMono#: Name, Mass, #C, #H, #N, #O, Min., Max." + Environment.NewLine;
                    if (options.customMono1Max > 0)
                    {
                        submitOutput += "<CustomMono1> " + options.customMono1Name + "," + options.customMono1Mass + "," + options.customMono1CCount + "," + options.customMono1HCount + "," + options.customMono1NCount + "," + options.customMono1OCount + "," + options.customMono1Min + "," + options.customMono1Max + Environment.NewLine;
                    }

                    if (options.customMono2Max > 0)
                    {
                        submitOutput += "<CustomMono2> " + options.customMono2Name + "," + options.customMono2Mass + "," + options.customMono2CCount + "," + options.customMono2HCount + "," + options.customMono2NCount + "," + options.customMono2OCount + "," + options.customMono2Min + "," + options.customMono2Max + Environment.NewLine;
                    }

                    if (options.customMono3Max > 0)
                    {
                        submitOutput += "<CustomMono3> " + options.customMono3Name + "," + options.customMono3Mass + "," + options.customMono3CCount + "," + options.customMono3HCount + "," + options.customMono3NCount + "," + options.customMono3OCount + "," + options.customMono3Min + "," + options.customMono3Max + Environment.NewLine;
                    }

                    if (options.customMono4Max > 0)
                    {
                        submitOutput += "<CustomMono4> " + options.customMono4Name + "," + options.customMono4Mass + "," + options.customMono4CCount + "," + options.customMono4HCount + "," + options.customMono4NCount + "," + options.customMono4OCount + "," + options.customMono4Min + "," + options.customMono4Max + Environment.NewLine;
                    }

                    if (options.customMono5Max > 0)
                    {
                        submitOutput += "<CustomMono5> " + options.customMono5Name + "," + options.customMono5Mass + "," + options.customMono5CCount + "," + options.customMono5HCount + "," + options.customMono5NCount + "," + options.customMono5OCount + "," + options.customMono5Min + "," + options.customMono5Max + Environment.NewLine;
                    }

                }
                submitOutput += "## Adducts: Adduct1, Adduct2" + Environment.NewLine;
                submitOutput += options.adducts + Environment.NewLine;
                File.WriteAllText(
                    Path.Combine(
                        Path.GetDirectoryName(outputFilePath),
                        Path.GetFileNameWithoutExtension(outputFilePath) + "_parameters.txt"),
                    submitOutput
                    + Environment.NewLine
                    + "<Precursor targets>"
                    + Environment.NewLine
                    + targetString
                );
                Console.WriteLine(submitOutput);

            }

            void Sum_up_recursive(List<decimal> numbers, decimal target, List<decimal> partial, bool targetFound, int i, CommandOptions options)
            {
                decimal s = 0;
                solutionProcess = "";
                solutions = "";

                // Count the number of times we have done this calculation and add more sugars to a given composition
                iterations += 1;
                foreach (decimal x in partial) s += x;

                // Once s is between the required mass range, write a line into solutions that contains all identified compositions
                if (s >= targetLow && s <= targetHigh)
                {
                    // Combines each of the solutions for the given mass
                    solutions = string.Join("", partial.ToArray());

                    // This replaces repeated monosaccharide names with 1 monosaccharide name and the number of the occurences
                    string solutionsUpdate = "";
                    int chemicalFormulaeC = 0;
                    int chemicalFormulaeH = 0;
                    int chemicalFormulaeO = 0;
                    int chemicalFormulaeN = 0;
                    int chemicalFormulaeP = 0;
                    int chemicalFormulaeS = 0;
                    int dHexCount = 0;
                    int HexACount = 0;
                    int HexNCount = 0;
                    int PentCount = 0;
                    int KDNCount = 0;
                    int hexCount = 0;
                    int neuAcCount = 0;
                    int neuGcCount = 0;
                    int hexNAcCount = 0;
                    int phosCount = 0;
                    int sulfCount = 0;
                    int dhexnacCount = 0;
                    int lNeuAcCount = 0;
                    int eeNeuAcCount = 0;
                    int dNeuAcCount = 0;
                    int amNeuAcCount = 0;
                    int acetylCount = 0;
                    int lNeuGcCount = 0;
                    int eeNeuGcCount = 0;
                    int dNeuGcCount = 0;
                    int amNeuGcCount = 0;

                    // This replaces all the masses with their respective monosaccharide identities
                    switch (options.derivatisation)
                    {
                        case "native":
                            solutions = solutions.Replace("146.057908", "dHex ").Replace("162.052823", "Hex ").Replace("291.095416", "Neu5Ac ").Replace("307.090331", "Neu5Gc ").Replace("203.079372", "HexNAc ").Replace("79.966331", "Phos ").Replace("79.956815", "Sulf ").Replace(",", "").Replace("161.068808", "HexN ").Replace("176.032088", "HexA ").Replace("187.084458", "dHexNAc ").Replace("132.042258", "Pent ").Replace("250.068867", "KDN ").Replace("273.0848518", "lneuac ").Replace("319.1267166", "eeneuac ").Replace("318.1427011", "dneuac ").Replace("290.1114009", "amneuac ").Replace("42.010565", "acetyl ").Replace("289.0797664", "lneugc ").Replace("335.1216313", "eeneugc ").Replace("306.1063155", "dneugc ").Replace("334.1376157", "amneugc ").Replace(options.customMono1Mass.ToString(), options.customMono1Name + " ").Replace(options.customMono2Mass.ToString(), options.customMono2Name + " ").Replace(options.customMono3Mass.ToString(), options.customMono3Name + " ").Replace(options.customMono4Mass.ToString(), options.customMono4Name + " ").Replace(options.customMono5Mass.ToString(), options.customMono5Name + " ");

                            // Chemical formulae for native
                            dHexCount = Regex.Matches(solutions, "dHex ").Count;
                            if (dHexCount > 0)
                            {
                                chemicalFormulaeC += (dHexCount * 6);
                                chemicalFormulaeH += (dHexCount * 10);
                                chemicalFormulaeO += (dHexCount * 4);
                                solutionsUpdate = solutionsUpdate + "(dHex)" + Convert.ToString(dHexCount) + " ";
                            }
                            HexACount = Regex.Matches(solutions, "HexA ").Count;
                            if (HexACount > 0)
                            {
                                chemicalFormulaeC += (HexACount * 6);
                                chemicalFormulaeH += (HexACount * 8);
                                chemicalFormulaeO += (HexACount * 6);
                                solutionsUpdate = solutionsUpdate + "(HexA)" + Convert.ToString(HexACount) + " ";
                            }
                            HexNCount = Regex.Matches(solutions, "HexN ").Count;
                            if (HexNCount > 0)
                            {
                                chemicalFormulaeC += (HexNCount * 6);
                                chemicalFormulaeH += (HexNCount * 11);
                                chemicalFormulaeO += (HexNCount * 4);
                                chemicalFormulaeN += (HexNCount);
                                solutionsUpdate = solutionsUpdate + "(HexN)" + Convert.ToString(HexNCount) + " ";
                            }
                            PentCount = Regex.Matches(solutions, "Pent ").Count;
                            if (PentCount > 0)
                            {
                                chemicalFormulaeC += (PentCount * 5);
                                chemicalFormulaeH += (PentCount * 8);
                                chemicalFormulaeO += (PentCount * 4);
                                solutionsUpdate = solutionsUpdate + "(Pent)" + Convert.ToString(PentCount) + " ";
                            }
                            KDNCount = Regex.Matches(solutions, "KDN ").Count;
                            if (KDNCount > 0)
                            {
                                chemicalFormulaeC += (KDNCount * 9);
                                chemicalFormulaeH += (KDNCount * 14);
                                chemicalFormulaeO += (KDNCount * 8);
                                solutionsUpdate = solutionsUpdate + "(KDN)" + Convert.ToString(KDNCount) + " ";
                            }
                            hexCount = Regex.Matches(solutions, "Hex ").Count - Regex.Matches(solutions, "dHex ").Count;
                            if (hexCount > 0)
                            {
                                chemicalFormulaeC += (hexCount * 6);
                                chemicalFormulaeH += (hexCount * 10);
                                chemicalFormulaeO += (hexCount * 5);
                                solutionsUpdate = solutionsUpdate + "(Hex)" + Convert.ToString(hexCount) + " ";
                            }
                            neuAcCount = Regex.Matches(solutions, "Neu5Ac ").Count;
                            if (neuAcCount > 0)
                            {
                                chemicalFormulaeC += (neuAcCount * 11);
                                chemicalFormulaeH += (neuAcCount * 17);
                                chemicalFormulaeN += (neuAcCount);
                                chemicalFormulaeO += (neuAcCount * 8);
                                solutionsUpdate = solutionsUpdate + "(NeuAc)" + Convert.ToString(neuAcCount) + " ";
                            }
                            neuGcCount = Regex.Matches(solutions, "Neu5Gc ").Count;
                            if (neuGcCount > 0)
                            {
                                chemicalFormulaeC += (neuGcCount * 11);
                                chemicalFormulaeH += (neuGcCount * 17);
                                chemicalFormulaeN += (neuGcCount);
                                chemicalFormulaeO += (neuGcCount * 9);
                                solutionsUpdate = solutionsUpdate + "(NeuGc)" + Convert.ToString(neuGcCount) + " ";
                            }
                            hexNAcCount = Regex.Matches(solutions, "HexNAc ").Count - Regex.Matches(solutions, "dHexNAc ").Count;
                            if (hexNAcCount > 0)
                            {
                                chemicalFormulaeC += (hexNAcCount * 8);
                                chemicalFormulaeH += (hexNAcCount * 13);
                                chemicalFormulaeN += (hexNAcCount);
                                chemicalFormulaeO += (hexNAcCount * 5);
                                solutionsUpdate = solutionsUpdate + "(HexNAc)" + Convert.ToString(hexNAcCount) + " ";
                            }
                            phosCount = Regex.Matches(solutions, "Phos ").Count;
                            if (phosCount > 0)
                            {
                                chemicalFormulaeH += (phosCount);
                                chemicalFormulaeO += (phosCount * 3);
                                chemicalFormulaeP += (phosCount);
                                solutionsUpdate = solutionsUpdate + "(Phos)" + Convert.ToString(phosCount) + " ";
                            }
                            sulfCount = Regex.Matches(solutions, "Sulf ").Count;
                            if (sulfCount > 0)
                            {
                                chemicalFormulaeO += (sulfCount * 3);
                                chemicalFormulaeS += (sulfCount);
                                solutionsUpdate = solutionsUpdate + "(Sulf)" + Convert.ToString(sulfCount) + " ";
                            }
                            dhexnacCount = Regex.Matches(solutions, "dHexNAc ").Count;
                            if (dhexnacCount > 0)
                            {
                                chemicalFormulaeC += (dhexnacCount * 8);
                                chemicalFormulaeH += (dhexnacCount * 13);
                                chemicalFormulaeN += (dhexnacCount);
                                chemicalFormulaeO += (dhexnacCount * 4);
                                solutionsUpdate = solutionsUpdate + "(dHexNAc)" + Convert.ToString(dhexnacCount) + " ";
                            }
                            lNeuAcCount = Regex.Matches(solutions, "lneuac ").Count;
                            if (lNeuAcCount > 0)
                            {
                                chemicalFormulaeC += (lNeuAcCount * 11);
                                chemicalFormulaeH += (lNeuAcCount * 15);
                                chemicalFormulaeN += (lNeuAcCount);
                                chemicalFormulaeO += (lNeuAcCount * 7);
                                solutionsUpdate = solutionsUpdate + "(lNeuAc)" + Convert.ToString(lNeuAcCount) + " ";
                            }
                            eeNeuAcCount = Regex.Matches(solutions, "eeneuac ").Count;
                            if (eeNeuAcCount > 0)
                            {
                                chemicalFormulaeC += (eeNeuAcCount * 13);
                                chemicalFormulaeH += (eeNeuAcCount * 21);
                                chemicalFormulaeN += (eeNeuAcCount);
                                chemicalFormulaeO += (eeNeuAcCount * 8);
                                solutionsUpdate = solutionsUpdate + "(eNeuAc)" + Convert.ToString(eeNeuAcCount) + " ";
                            }
                            dNeuAcCount = Regex.Matches(solutions, "dneuac ").Count;
                            if (dNeuAcCount > 0)
                            {
                                chemicalFormulaeC += (dNeuAcCount * 13);
                                chemicalFormulaeH += (dNeuAcCount * 22);
                                chemicalFormulaeN += (dNeuAcCount * 2);
                                chemicalFormulaeO += (dNeuAcCount * 7);
                                solutionsUpdate = solutionsUpdate + "(dNeuAc)" + Convert.ToString(dNeuAcCount) + " ";
                            }
                            amNeuAcCount = Regex.Matches(solutions, "amneuac ").Count;
                            if (amNeuAcCount > 0)
                            {
                                chemicalFormulaeC += (amNeuAcCount * 11);
                                chemicalFormulaeH += (amNeuAcCount * 18);
                                chemicalFormulaeN += (amNeuAcCount * 2);
                                chemicalFormulaeO += (amNeuAcCount * 7);
                                solutionsUpdate = solutionsUpdate + "(amNeuAc)" + Convert.ToString(amNeuAcCount) + " ";
                            }
                            acetylCount = Regex.Matches(solutions, "acetyl ").Count;
                            if (acetylCount > 0)
                            {
                                chemicalFormulaeC += (acetylCount * 2);
                                chemicalFormulaeH += (acetylCount * 2);
                                chemicalFormulaeO += (acetylCount * 1);
                                solutionsUpdate = solutionsUpdate + "(Acetyl)" + Convert.ToString(acetylCount) + " ";
                            }
                            lNeuGcCount = Regex.Matches(solutions, "lneugc ").Count;
                            if (lNeuGcCount > 0)
                            {
                                chemicalFormulaeC += (lNeuGcCount * 11);
                                chemicalFormulaeH += (lNeuGcCount * 15);
                                chemicalFormulaeN += (lNeuGcCount);
                                chemicalFormulaeO += (lNeuGcCount * 8);
                                solutionsUpdate = solutionsUpdate + "(lNeuGc)" + Convert.ToString(lNeuGcCount) + " ";
                            }
                            eeNeuGcCount = Regex.Matches(solutions, "eeneugc ").Count;
                            if (eeNeuGcCount > 0)
                            {
                                chemicalFormulaeC += (eeNeuGcCount * 13);
                                chemicalFormulaeH += (eeNeuGcCount * 21);
                                chemicalFormulaeN += (eeNeuGcCount);
                                chemicalFormulaeO += (eeNeuGcCount * 9);
                                solutionsUpdate = solutionsUpdate + "(eNeuGc)" + Convert.ToString(eeNeuGcCount) + " ";
                            }
                            dNeuGcCount = Regex.Matches(solutions, "dneugc ").Count;
                            if (dNeuGcCount > 0)
                            {
                                chemicalFormulaeC += (dNeuGcCount * 13);
                                chemicalFormulaeH += (dNeuGcCount * 22);
                                chemicalFormulaeN += (dNeuGcCount * 2);
                                chemicalFormulaeO += (dNeuGcCount * 8);
                                solutionsUpdate = solutionsUpdate + "(dNeuGc)" + Convert.ToString(dNeuGcCount) + " ";
                            }
                            amNeuGcCount = Regex.Matches(solutions, "amneugc ").Count;
                            if (amNeuGcCount > 0)
                            {
                                chemicalFormulaeC += (amNeuGcCount * 11);
                                chemicalFormulaeH += (amNeuGcCount * 18);
                                chemicalFormulaeN += (amNeuGcCount * 2);
                                chemicalFormulaeO += (amNeuGcCount * 8);
                                solutionsUpdate = solutionsUpdate + "(amNeuGc)" + Convert.ToString(amNeuGcCount) + " ";
                            }

                            switch (options.reducedEnd)
                            {
                                case "free":
                                    chemicalFormulaeH += 2;
                                    chemicalFormulaeO += 1;
                                    break;
                                case "reduced":
                                    chemicalFormulaeH += 4;
                                    chemicalFormulaeO += 1;
                                    break;
                                case "instantpc":
                                    chemicalFormulaeC += 14;
                                    chemicalFormulaeH += 21;
                                    chemicalFormulaeN += 3;
                                    chemicalFormulaeO += 3;
                                    break;
                                case "rapifluor-ms":
                                    chemicalFormulaeC += 17;
                                    chemicalFormulaeH += 23;
                                    chemicalFormulaeN += 5;
                                    chemicalFormulaeO += 2;
                                    break;
                                case "2aa":
                                    chemicalFormulaeC += 7;
                                    chemicalFormulaeH += 9;
                                    chemicalFormulaeN += 1;
                                    chemicalFormulaeO += 2;
                                    break;
                                case "2ab":
                                    chemicalFormulaeC += 7;
                                    chemicalFormulaeH += 10;
                                    chemicalFormulaeN += 2;
                                    chemicalFormulaeO += 1;
                                    break;
                                case "procainamide":
                                    chemicalFormulaeC += 13;
                                    chemicalFormulaeH += 23;
                                    chemicalFormulaeN += 3;
                                    chemicalFormulaeO += 1;
                                    break;
                                case "girp":
                                    chemicalFormulaeC += 7;
                                    chemicalFormulaeH += 10;
                                    chemicalFormulaeN += 3;
                                    chemicalFormulaeO += 1;
                                    break;
                                case "custom":
                                    chemicalFormulaeC += options.customReducingCCount;
                                    chemicalFormulaeH += options.customReducingHCount;
                                    chemicalFormulaeN += options.customReducingNCount;
                                    chemicalFormulaeO += options.customReducingOCount;
                                    break;
                                default:
                                    break;
                            }
                            switch (options.reducedEnd)
                            {
                                case "free":
                                    observedMass = s + 18.010565m;
                                    theoreticalMass = target + 18.010565m;
                                    break;
                                case "reduced":
                                    observedMass = s + 20.026195m;
                                    theoreticalMass = target + 20.026195m;
                                    break;
                                case "instantpc":
                                    observedMass = s + 18.010565m + 261.1477m;
                                    theoreticalMass = target + 18.010565m + 261.1477m;
                                    break;
                                case "rapifluor-ms":
                                    observedMass = s + 18.010565m + 311.17461m;
                                    theoreticalMass = target + 18.010565m + 311.17461m;
                                    break;
                                case "2aa":
                                    observedMass = s + 18.010565m + 121.052774m;
                                    theoreticalMass = target + 18.010565m + 121.052774m;
                                    break;
                                case "2ab":
                                    observedMass = s + 18.010565m + 120.068758m;
                                    theoreticalMass = target + 18.010565m + 120.068758m;
                                    break;
                                case "procainamide":
                                    observedMass = s + 18.010565m + 219.1735574m;
                                    theoreticalMass = target + 18.010565m + 219.1735574m;
                                    break;
                                case "girp":
                                    observedMass = s + 18.010565m + 134.06405m;
                                    theoreticalMass = target + 18.010565m + 134.06405m;
                                    break;
                                case "custom":
                                    observedMass = s + 18.010565m + options.customReducingMass;
                                    theoreticalMass = target + 18.010565m + options.customReducingMass;
                                    break;
                                default:
                                    break;
                            }
                            break;

                        case "permethylated":
                            solutions = solutions.Replace("174.089210", "dHex ").Replace("204.099775", "Hex ").Replace("361.173669", "Neu5Ac ").Replace("391.184234", "Neu5Gc ").Replace("245.126324", "HexNAc ").Replace("93.981983", "Phos ").Replace("79.956815", "Sulf ").Replace(",", "").Replace("203.115758", "HexN ").Replace("218.079040", "HexA ").Replace("215.115759", "dHexNAc ").Replace("160.073560", "Pent ").Replace("320.147120", "KDN ").Replace(options.customMono1Mass.ToString(), options.customMono1Name + " ").Replace(options.customMono2Mass.ToString(), options.customMono2Name + " ").Replace(options.customMono3Mass.ToString(), options.customMono3Name + " ").Replace(options.customMono4Mass.ToString(), options.customMono4Name + " ").Replace(options.customMono5Mass.ToString(), options.customMono5Name + " ");

                            // Chemical formulae for permethylated
                            dHexCount = Regex.Matches(solutions, "dHex ").Count;
                            if (dHexCount > 0)
                            {
                                chemicalFormulaeC += (dHexCount * 8);
                                chemicalFormulaeH += (dHexCount * 14);
                                chemicalFormulaeO += (dHexCount * 4);
                                solutionsUpdate = solutionsUpdate + "(dHex)" + Convert.ToString(dHexCount) + " ";
                            }
                            HexACount = Regex.Matches(solutions, "HexA ").Count;
                            if (HexACount > 0)
                            {
                                chemicalFormulaeC += (HexACount * 9);
                                chemicalFormulaeH += (HexACount * 14);
                                chemicalFormulaeO += (HexACount * 6);
                                solutionsUpdate = solutionsUpdate + "(HexA)" + Convert.ToString(HexACount) + " ";
                            }
                            HexNCount = Regex.Matches(solutions, "HexN ").Count;
                            if (HexNCount > 0)
                            {
                                chemicalFormulaeC += (HexNCount * 9);
                                chemicalFormulaeH += (HexNCount * 17);
                                chemicalFormulaeO += (HexNCount * 4);
                                chemicalFormulaeN += (HexNCount);
                                solutionsUpdate = solutionsUpdate + "(HexN)" + Convert.ToString(HexNCount) + " ";
                            }
                            PentCount = Regex.Matches(solutions, "Pent ").Count;
                            if (PentCount > 0)
                            {
                                chemicalFormulaeC += (PentCount * 7);
                                chemicalFormulaeH += (PentCount * 12);
                                chemicalFormulaeO += (PentCount * 4);
                                solutionsUpdate = solutionsUpdate + "(Pent)" + Convert.ToString(PentCount) + " ";
                            }
                            KDNCount = Regex.Matches(solutions, "KDN ").Count;
                            if (KDNCount > 0)
                            {
                                chemicalFormulaeC += (KDNCount * 14);
                                chemicalFormulaeH += (KDNCount * 24);
                                chemicalFormulaeO += (KDNCount * 8);
                                solutionsUpdate = solutionsUpdate + "(KDN)" + Convert.ToString(KDNCount) + " ";
                            }
                            hexCount = Regex.Matches(solutions, "Hex ").Count - Regex.Matches(solutions, "dHex ").Count;
                            if (hexCount > 0)
                            {
                                chemicalFormulaeC += (hexCount * 9);
                                chemicalFormulaeH += (hexCount * 16);
                                chemicalFormulaeO += (hexCount * 5);
                                solutionsUpdate = solutionsUpdate + "(Hex)" + Convert.ToString(hexCount) + " ";
                            }
                            neuAcCount = Regex.Matches(solutions, "Neu5Ac ").Count;
                            if (neuAcCount > 0)
                            {
                                chemicalFormulaeC += (neuAcCount * 16);
                                chemicalFormulaeH += (neuAcCount * 27);
                                chemicalFormulaeN += (neuAcCount);
                                chemicalFormulaeO += (neuAcCount * 8);
                                solutionsUpdate = solutionsUpdate + "(NeuAc)" + Convert.ToString(neuAcCount) + " ";
                            }
                            neuGcCount = Regex.Matches(solutions, "Neu5Gc ").Count;
                            if (neuGcCount > 0)
                            {
                                chemicalFormulaeC += (neuGcCount * 17);
                                chemicalFormulaeH += (neuGcCount * 29);
                                chemicalFormulaeN += (neuGcCount);
                                chemicalFormulaeO += (neuGcCount * 9);
                                solutionsUpdate = solutionsUpdate + "(NeuGc)" + Convert.ToString(neuGcCount) + " ";
                            }
                            hexNAcCount = Regex.Matches(solutions, "HexNAc ").Count - Regex.Matches(solutions, "dHexNAc ").Count;
                            if (hexNAcCount > 0)
                            {
                                chemicalFormulaeC += (hexNAcCount * 11);
                                chemicalFormulaeH += (hexNAcCount * 19);
                                chemicalFormulaeN += (hexNAcCount);
                                chemicalFormulaeO += (hexNAcCount * 5);
                                solutionsUpdate = solutionsUpdate + "(HexNAc)" + Convert.ToString(hexNAcCount) + " ";
                            }
                            phosCount = Regex.Matches(solutions, "Phos ").Count;
                            if (phosCount > 0)
                            {
                                chemicalFormulaeC += (phosCount);
                                chemicalFormulaeH += (phosCount * 3);
                                chemicalFormulaeO += (phosCount * 3);
                                chemicalFormulaeP += (phosCount);
                                solutionsUpdate = solutionsUpdate + "(Phos)" + Convert.ToString(phosCount) + " ";
                            }
                            dhexnacCount = Regex.Matches(solutions, "dHexNAc ").Count;
                            if (dhexnacCount > 0)
                            {
                                chemicalFormulaeC += (dhexnacCount * 10);
                                chemicalFormulaeH += (dhexnacCount * 17);
                                chemicalFormulaeN += (dhexnacCount);
                                chemicalFormulaeO += (dhexnacCount * 4);
                                solutionsUpdate = solutionsUpdate + "(dHexNAc)" + Convert.ToString(dhexnacCount) + " ";
                            }
                            sulfCount = Regex.Matches(solutions, "Sulf ").Count;
                            if (sulfCount > 0)
                            {
                                chemicalFormulaeC += (sulfCount * -1);
                                chemicalFormulaeH += (sulfCount * -2);
                                chemicalFormulaeO += (sulfCount * 3);
                                chemicalFormulaeS += (sulfCount);
                                solutionsUpdate = solutionsUpdate + "(Sulf)" + Convert.ToString(sulfCount) + " ";
                            }
                            switch (reducedEnd)
                            {
                                case "free":
                                    chemicalFormulaeC += 2;
                                    chemicalFormulaeH += 6;
                                    chemicalFormulaeO += 1;
                                    break;
                                case "reduced":
                                    chemicalFormulaeC += 3;
                                    chemicalFormulaeH += 10;
                                    chemicalFormulaeO += 1;
                                    break;
                                case "custom":
                                    chemicalFormulaeC += options.customReducingCCount;
                                    chemicalFormulaeH += options.customReducingHCount;
                                    chemicalFormulaeN += options.customReducingNCount;
                                    chemicalFormulaeO += options.customReducingOCount;
                                    break;
                                default:
                                    break;
                            }
                            // Permethylated
                            switch (reducedEnd)
                            {
                                case "free":
                                    observedMass = s + 18.010565m + 28.031300m;
                                    theoreticalMass = target + 18.010565m + 28.031300m;
                                    break;
                                case "reduced":
                                    observedMass = s + 20.026195m + 42.046950m;
                                    theoreticalMass = target + 20.026195m + 42.046950m;
                                    break;
                                case "custom":
                                    observedMass = s + 18.010565m + options.customReducingMass;
                                    theoreticalMass = target + 18.010565m + options.customReducingMass;
                                    break;
                                default:
                                    break;
                            }
                            break;

                        case "peracetylated":
                            solutions = solutions.Replace("230.079038", "dHex ").Replace("288.084517", "Hex ").Replace("417.127110", "Neu5Ac ").Replace("475.132593", "Neu5Gc ").Replace("287.100501", "HexNAc ").Replace("93.981983", "Phos ").Replace("79.956815", "Sulf ").Replace(",", "").Replace("287.100501", "HexN ").Replace("260.053217", "HexA ").Replace("247.105587", "dHexNAc ").Replace("216.063388", "Pent ").Replace("376.100561", "KDN ").Replace(options.customMono1Mass.ToString(), options.customMono1Name + " ").Replace(options.customMono2Mass.ToString(), options.customMono2Name + " ").Replace(options.customMono3Mass.ToString(), options.customMono3Name + " ").Replace(options.customMono4Mass.ToString(), options.customMono4Name + " ").Replace(options.customMono5Mass.ToString(), options.customMono5Name + " ");
                            // peracetylated processing
                            // Chemical formulae for peracetylated
                            dHexCount = Regex.Matches(solutions, "dHex ").Count;
                            if (dHexCount > 0)
                            {
                                chemicalFormulaeC += (dHexCount * 10);
                                chemicalFormulaeH += (dHexCount * 14);
                                chemicalFormulaeO += (dHexCount * 6);
                                solutionsUpdate = solutionsUpdate + "(dHex)" + Convert.ToString(dHexCount) + " ";
                            }
                            HexACount = Regex.Matches(solutions, "HexA ").Count;
                            if (HexACount > 0)
                            {
                                chemicalFormulaeC += (HexACount * 10);
                                chemicalFormulaeH += (HexACount * 12);
                                chemicalFormulaeO += (HexACount * 8);
                                solutionsUpdate = solutionsUpdate + "(HexA)" + Convert.ToString(HexACount) + " ";
                            }
                            HexNCount = Regex.Matches(solutions, "HexN ").Count;
                            if (HexNCount > 0)
                            {
                                chemicalFormulaeC += (HexNCount * 12);
                                chemicalFormulaeH += (HexNCount * 17);
                                chemicalFormulaeO += (HexNCount * 7);
                                chemicalFormulaeN += (HexNCount);
                                solutionsUpdate = solutionsUpdate + "(HexN)" + Convert.ToString(HexNCount) + " ";
                            }
                            PentCount = Regex.Matches(solutions, "Pent ").Count;
                            if (PentCount > 0)
                            {
                                chemicalFormulaeC += (PentCount * 9);
                                chemicalFormulaeH += (PentCount * 12);
                                chemicalFormulaeO += (PentCount * 6);
                                solutionsUpdate = solutionsUpdate + "(Pent)" + Convert.ToString(PentCount) + " ";
                            }
                            KDNCount = Regex.Matches(solutions, "KDN ").Count;
                            if (KDNCount > 0)
                            {
                                chemicalFormulaeC += (KDNCount * 15);
                                chemicalFormulaeH += (KDNCount * 28);
                                chemicalFormulaeO += (KDNCount * 11);
                                solutionsUpdate = solutionsUpdate + "(KDN)" + Convert.ToString(KDNCount) + " ";
                            }
                            hexCount = Regex.Matches(solutions, "Hex ").Count - Regex.Matches(solutions, "dHex ").Count;
                            if (hexCount > 0)
                            {
                                chemicalFormulaeC += (hexCount * 12);
                                chemicalFormulaeH += (hexCount * 16);
                                chemicalFormulaeO += (hexCount * 8);
                                solutionsUpdate = solutionsUpdate + "(Hex)" + Convert.ToString(hexCount) + " ";
                            }
                            neuAcCount = Regex.Matches(solutions, "Neu5Ac ").Count;
                            if (neuAcCount > 0)
                            {
                                chemicalFormulaeC += (neuAcCount * 17);
                                chemicalFormulaeH += (neuAcCount * 23);
                                chemicalFormulaeN += (neuAcCount);
                                chemicalFormulaeO += (neuAcCount * 11);
                                solutionsUpdate = solutionsUpdate + "(NeuAc)" + Convert.ToString(neuAcCount) + " ";
                            }
                            neuGcCount = Regex.Matches(solutions, "Neu5Gc ").Count;
                            if (neuGcCount > 0)
                            {
                                chemicalFormulaeC += (neuGcCount * 19);
                                chemicalFormulaeH += (neuGcCount * 25);
                                chemicalFormulaeN += (neuGcCount);
                                chemicalFormulaeO += (neuGcCount * 13);
                                solutionsUpdate = solutionsUpdate + "(NeuGc)" + Convert.ToString(neuGcCount) + " ";
                            }
                            hexNAcCount = Regex.Matches(solutions, "HexNAc ").Count - Regex.Matches(solutions, "dHexNAc ").Count;
                            if (hexNAcCount > 0)
                            {
                                chemicalFormulaeC += (hexNAcCount * 12);
                                chemicalFormulaeH += (hexNAcCount * 17);
                                chemicalFormulaeN += (hexNAcCount);
                                chemicalFormulaeO += (hexNAcCount * 7);
                                solutionsUpdate = solutionsUpdate + "(HexNAc)" + Convert.ToString(hexNAcCount) + " ";
                            }
                            phosCount = Regex.Matches(solutions, "Phos ").Count;
                            if (phosCount > 0)
                            {
                                chemicalFormulaeC += (phosCount * -2);
                                chemicalFormulaeH += (phosCount * -1);
                                chemicalFormulaeO += (phosCount * 2);
                                chemicalFormulaeP += (phosCount);
                                solutionsUpdate = solutionsUpdate + "(Phos)" + Convert.ToString(phosCount) + " ";
                            }
                            dhexnacCount = Regex.Matches(solutions, "dHexNAc ").Count;
                            if (dhexnacCount > 0)
                            {
                                chemicalFormulaeC += (dhexnacCount * 10);
                                chemicalFormulaeH += (dhexnacCount * 17);
                                chemicalFormulaeN += (dhexnacCount);
                                chemicalFormulaeO += (dhexnacCount * 6);
                                solutionsUpdate = solutionsUpdate + "(dHexNAc)" + Convert.ToString(dhexnacCount) + " ";
                            }
                            sulfCount = Regex.Matches(solutions, "Sulf ").Count;
                            if (sulfCount > 0)
                            {
                                chemicalFormulaeC += (sulfCount * -2);
                                chemicalFormulaeH += (sulfCount * -2);
                                chemicalFormulaeO += (sulfCount * 2);
                                chemicalFormulaeS += (sulfCount);
                                solutionsUpdate = solutionsUpdate + "(Sulf)" + Convert.ToString(sulfCount) + " ";
                            }
                            switch (reducedEnd)
                            {
                                case "free":
                                    chemicalFormulaeC += 4;
                                    chemicalFormulaeH += 6;
                                    chemicalFormulaeO += 3;
                                    break;
                                case "reduced":
                                    chemicalFormulaeC += 6;
                                    chemicalFormulaeH += 10;
                                    chemicalFormulaeO += 4;
                                    break;
                                case "custom":
                                    chemicalFormulaeC += options.customReducingCCount;
                                    chemicalFormulaeH += options.customReducingHCount;
                                    chemicalFormulaeN += options.customReducingNCount;
                                    chemicalFormulaeO += options.customReducingOCount;
                                    break;
                                default:
                                    break;
                            }
                            // Peracetylated
                            switch (reducedEnd)
                            {
                                case "free":
                                    observedMass = s + 18.010565m + 84.021129m;
                                    theoreticalMass = target + 18.010565m + 84.021129m;
                                    break;
                                case "reduced":
                                    observedMass = s + 20.026195m + 126.031694m;
                                    theoreticalMass = target + 20.026195m + 126.031694m;
                                    break;
                                case "custom":
                                    observedMass = s + 18.010565m + options.customReducingMass;
                                    theoreticalMass = target + 18.010565m + options.customReducingMass;
                                    break;
                                default:
                                    break;
                            }
                            break;

                        default:
                            break;
                    }

                    // Custom monosaccharides are independent of derivatisation status
                    // This is incorrectly finding customMonos despite none being set
                    int customMono1Count = Regex.Matches(solutions, options.customMono1Name + " ").Count;
                    if (customMono1Count > 0 && monoCustom1 == true)
                    {
                        chemicalFormulaeC += (customMono1Count * options.customMono1CCount);
                        chemicalFormulaeH += (customMono1Count * options.customMono1HCount);
                        chemicalFormulaeN += (customMono1Count * options.customMono1NCount);
                        chemicalFormulaeO += (customMono1Count * options.customMono1OCount);
                        solutionsUpdate += "(" + options.customMono1Name + ")" + customMono1Count + " ";
                    }

                    int customMono2Count = Regex.Matches(solutions, options.customMono2Name + " ").Count;
                    if (customMono2Count > 0 && monoCustom2 == true)
                    {
                        chemicalFormulaeC += (customMono2Count * options.customMono2CCount);
                        chemicalFormulaeH += (customMono2Count * options.customMono2HCount);
                        chemicalFormulaeN += (customMono2Count * options.customMono2NCount);
                        chemicalFormulaeO += (customMono2Count * options.customMono2OCount);
                        solutionsUpdate += "(" + options.customMono2Name + ")" + customMono2Count + " ";
                    }

                    int customMono3Count = Regex.Matches(solutions, options.customMono3Name + " ").Count;
                    if (customMono3Count > 0 && monoCustom3 == true)
                    {
                        chemicalFormulaeC += (customMono3Count * options.customMono3CCount);
                        chemicalFormulaeH += (customMono3Count * options.customMono3HCount);
                        chemicalFormulaeN += (customMono3Count * options.customMono3NCount);
                        chemicalFormulaeO += (customMono3Count * options.customMono3OCount);
                        solutionsUpdate += "(" + options.customMono3Name + ")" + customMono3Count + " ";
                    }

                    int customMono4Count = Regex.Matches(solutions, options.customMono4Name + " ").Count;
                    if (customMono4Count > 0 && monoCustom4 == true)
                    {
                        chemicalFormulaeC += (customMono4Count * options.customMono4CCount);
                        chemicalFormulaeH += (customMono4Count * options.customMono4HCount);
                        chemicalFormulaeN += (customMono4Count * options.customMono4NCount);
                        chemicalFormulaeO += (customMono4Count * options.customMono4OCount);
                        solutionsUpdate += "(" + options.customMono4Name + ")" + customMono4Count + " ";
                    }

                    int customMono5Count = Regex.Matches(solutions, options.customMono5Name + " ").Count;
                    if (customMono5Count > 0 && monoCustom5 == true)
                    {
                        chemicalFormulaeC += (customMono5Count * options.customMono5CCount);
                        chemicalFormulaeH += (customMono5Count * options.customMono5HCount);
                        chemicalFormulaeN += (customMono5Count * options.customMono5NCount);
                        chemicalFormulaeO += (customMono5Count * options.customMono5OCount);
                        solutionsUpdate += "(" + options.customMono5Name + ")" + customMono5Count + " ";
                    }

                    // Preparation to export a chemical formulae in a format compatible with Skyline
                    string chemicalFormula = "C" + chemicalFormulaeC + "H" + chemicalFormulaeH + "N" + chemicalFormulaeN + "O" + chemicalFormulaeO + "P" + chemicalFormulaeP + "S" + chemicalFormulaeS;
                    chemicalFormula = chemicalFormula.Replace("N0", "").Replace("P0", "").Replace("S0", "");

                    // Calculation for mass error
                    error = observedMass - theoreticalMass;

                    // Calculation of scan number and charge state to be represented later
                    targetIndex.Add(i);
                    string fileExtension = Path.GetExtension(options.file);
                    if (fileExtension.ToLower() == ".mzml")
                    {
                        string scanNumberForOutput = "";
                        string chargeForOutput = "";
                        string retentionTimeForOutput = "";
                        string TICForOutput = "";
                        string FileForOutput = "";

                        // mzml input therefore output needs to be include scan #, charge, RT and TIC values.
                        // Adducts multiply the target list, this step ensures that we can assign metadata to all of the targets (otherwise it looks for targets that aren't there)
                        for (int z = 0; z < searchRepeats; z++)
                        {
                            int index = i % scans.Count;
                            scanNumberForOutput = Convert.ToString(scans.ElementAt(index));
                            chargeForOutput = Convert.ToString(charges.ElementAt(index));
                            retentionTimeForOutput = Convert.ToString(retentionTimes.ElementAt(index));
                            TICForOutput = Convert.ToString(TICs.ElementAt(index));
                            FileForOutput = Convert.ToString(files.ElementAt(index));
                        }

                        // Adding of each string component to output
                        solutionProcess += solutionsUpdate + System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator + theoreticalMass + System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator + observedMass + System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator + chemicalFormula + System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator + error + System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator + scanNumberForOutput + System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator + chargeForOutput + System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator + retentionTimeForOutput + System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator + TICForOutput + System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator + FileForOutput + Environment.NewLine;
                    }
                    else
                    {
                        // just text input, so no charge state, scan number, RT, or TIC info
                        solutionProcess += solutionsUpdate + System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator + theoreticalMass + System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator + observedMass + System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator + chemicalFormula + System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator + error + System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator + Environment.NewLine;
                    }


                    // Method to remove all compositions outside of user-set bounds
                    int outOfBounds = 0;

                    if (hexCount < options.HexMin || hexCount > options.HexMax)
                    {
                        outOfBounds += 1;
                    }
                    if (hexNAcCount < options.HexNAcMin || hexNAcCount > options.HexNAcMax)
                    {
                        outOfBounds += 1;
                    }
                    if (dHexCount < options.dHexMin || dHexCount > options.dHexMax)
                    {
                        outOfBounds += 1;
                    }
                    if (HexACount < options.HexAMin || HexACount > options.HexAMax)
                    {
                        outOfBounds += 1;
                    }
                    if (HexNCount < options.HexNMin || HexNCount > options.HexNMax)
                    {
                        outOfBounds += 1;
                    }
                    if (PentCount < options.PentMin || PentCount > options.PentMax)
                    {
                        outOfBounds += 1;
                    }
                    if (KDNCount < options.KDNMin || KDNCount > options.KDNMax)
                    {
                        outOfBounds += 1;
                    }
                    if (neuAcCount < options.Neu5AcMin || neuAcCount > options.Neu5AcMax)
                    {
                        outOfBounds += 1;
                    }
                    if (neuGcCount < options.Neu5GcMin || neuGcCount > options.Neu5GcMax)
                    {
                        outOfBounds += 1;
                    }
                    if (phosCount < options.PhosMin || phosCount > options.PhosMax)
                    {
                        outOfBounds += 1;
                    }
                    if (sulfCount < options.SulfMin || sulfCount > options.SulfMax)
                    {
                        outOfBounds += 1;
                    }
                    if (dhexnacCount < options.dHexNAcMin || dhexnacCount > options.dHexNAcMax)
                    {
                        outOfBounds += 1;
                    }
                    if (lNeuAcCount < options.lNeuAcMin || lNeuAcCount > options.lNeuAcMax)
                    {
                        outOfBounds += 1;
                    }
                    if (eeNeuAcCount < options.eeNeuAcMin || eeNeuAcCount > options.eeNeuAcMax)
                    {
                        outOfBounds += 1;
                    }
                    if (dNeuAcCount < options.dNeuAcMin || dNeuAcCount > options.dNeuAcMax)
                    {
                        outOfBounds += 1;
                    }
                    if (amNeuAcCount < options.amNeuAcMin || amNeuAcCount > options.amNeuAcMax)
                    {
                        outOfBounds += 1;
                    }
                    if (acetylCount < options.acetylMin || acetylCount > options.acetylMax)
                    {
                        outOfBounds += 1;
                    }
                    if (lNeuGcCount < options.lNeuGcMin || lNeuGcCount > options.lNeuGcMax)
                    {
                        outOfBounds += 1;
                    }
                    if (eeNeuGcCount < options.eeNeuGcMin || eeNeuGcCount > options.eeNeuGcMax)
                    {
                        outOfBounds += 1;
                    }
                    if (dNeuGcCount < options.dNeuGcMin || dNeuGcCount > options.dNeuGcMax)
                    {
                        outOfBounds += 1;
                    }
                    if (amNeuGcCount < options.amNeuGcMin || amNeuGcCount > options.amNeuGcMax)
                    {
                        outOfBounds += 1;
                    }

                    if (customMono1Count < options.customMono1Min
                        || customMono1Count > options.customMono1Max
                        && monoCustom1 == true)
                    {
                        outOfBounds += 1;
                    }
                    if (customMono2Count < options.customMono2Min
                        || customMono2Count > options.customMono2Max
                        && monoCustom2 == true)
                    {
                        outOfBounds += 1;
                    }
                    if (customMono3Count < options.customMono3Min
                        || customMono3Count > options.customMono3Max
                        && monoCustom3 == true)
                    {
                        outOfBounds += 1;
                    }
                    if (customMono4Count < options.customMono4Min
                        || customMono4Count > options.customMono4Max
                        && monoCustom4 == true)
                    {
                        outOfBounds += 1;
                    }
                    if (customMono5Count < options.customMono5Min
                        || customMono5Count > options.customMono5Max
                        && monoCustom5 == true)
                    {
                        outOfBounds += 1;
                    }

                    // The only solutions that get reported are those that do not fall outside of any specified monosaccharide ranges
                    if (outOfBounds == 0)
                    {
                        solutionMultiples += solutionProcess.ToString();
                    }
                }

                // Give up if the mass is too high
                if (s >= targetHigh)
                {
                    return;
                }

                // Keep adding monosaccharides until remainder resets
                // Starting from current index k, each subset of numbers is considered only once
                // By starting the loop at k, each combination is built by progressively adding monosaccharides, avoiding different combinations of the same numbers
                for (int k = 0; k < numbers.Count; k++)
                {
                    List<decimal> remaining = [];
                    decimal n = numbers[k];
                    for (int j = k; j < numbers.Count; j++) remaining.Add(numbers[j]);
                    // Combinations are built in a consistent order, avoiding permutations of the same set of monosaccharides
                    List<decimal> partial_rec = new(partial)
            {
                n
            };
                    Sum_up_recursive(remaining, target, partial_rec, targetFound, i, options);
                }
            }

            if (!string.IsNullOrWhiteSpace(options.file) && File.Exists(options.file))
            {
                using var reader = new StreamReader(options.file);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                }
            }
            else
            {
                Console.WriteLine("No valid file path provided or file does not exist.");
            }
        });
        rootCommand.InvokeAsync(args).Wait();
    }
}
