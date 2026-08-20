using GlyCombo;
using GlyComboCLI;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.NamingConventionBinder;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using static GlyComboCLI.MonosaccharideDefinition;


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
    public decimal massError { get; set; }
    public string? massErrorType { get; set; }
    public string? file { get; set; }
    public string? adducts { get; set; }
    public bool? offByOne { get; set; }
    public bool noGlyTouCan { get; set; }
    public string? outputPath { get; set; }

}
class Program
{
    static void PrintBanner()
    {
        Console.WriteLine();
        Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                     GlyComboCLI  v1.1                        ║");
        Console.WriteLine("║       Monosaccharide Combinatorial Assignment for MS         ║");
        Console.WriteLine("╠══════════════════════════════════════════════════════════════╣");
        Console.WriteLine("║  Rapidly assigns monosaccharide combinations to observed     ║");
        Console.WriteLine("║  and fragmented precursors in mass spectrometry experiments. ║");
        Console.WriteLine("╠══════════════════════════════════════════════════════════════╣");
        Console.WriteLine("║  Supported input formats:  .mzML  |  .txt                    ║");
        Console.WriteLine("║  Supported derivatisations: Native | Permethylated |         ║");
        Console.WriteLine("║    Peracetylated                                             ║");
        Console.WriteLine("║  Supported reducing ends:   Free | Reduced | InstantPC |     ║");
        Console.WriteLine("║    Rapifluor-MS | 2AA | 2AB | Procainamide | Girard |        ║");
        Console.WriteLine("║    Custom                                                    ║");
        Console.WriteLine("║  Supported adducts (+):     MH+ | MNa+ | MNH4+ | MK+         ║");
        Console.WriteLine("║  Supported adducts (-):     MH- | MFA- | MAA- | MTFA-        ║");
        Console.WriteLine("║  Other adducts:             Neutral | Custom                 ║");
        Console.WriteLine("╠══════════════════════════════════════════════════════════════╣");
        Console.WriteLine("║  Example:                                                    ║");
        Console.WriteLine("║  GlyComboCLI.exe -F=\".\\example.mzML\" -hMin=1 -hMax=12        ║");
        Console.WriteLine("║    -nMin=2 -nMax=8 -sMin=0 -sMax=2 -fMin=0 -fMax=3           ║");
        Console.WriteLine("║    -gMin=0 -gMax=2 -D=\"Native\" -R=\"Reduced\" -T=Da -E=0.6     ║");
        Console.WriteLine("╠══════════════════════════════════════════════════════════════╣");
        Console.WriteLine("║  Questions, comments and bug reports:                        ║");
        Console.WriteLine("║    https://github.com/Protea-Glycosciences/GlyComboCLI       ║");
        Console.WriteLine("║    chris@proteaglyco.com                                     ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
        Console.WriteLine();
    }

    static void Main(string[] args)
    {
        decimal errorTol;
        string solutionProcess;
        string solutions;
        string solutionMultiples = "";
        int targetsToAdd;
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
        decimal dneugc = 0;
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
        bool mzmlFile = false;
        string filePath = "";
        string filePath1 = "";
        string filePath2 = "";


        List<decimal> numbers = new List<decimal>();
        List<decimal> scans = new List<decimal>();
        List<int> charges = new List<int>();
        List<decimal> retentionTimes = new List<decimal>();
        List<decimal> TICs = new List<decimal>();
        List<string> files = new List<string>();
        List<int> targetIndex = new List<int>();
        List<decimal> targets = new List<decimal>();
        List<string> targetStrings = new List<string>();
        List<MonosaccharideDefinition> catalog = new();
        List<CompositionResult> results = new();
        long iterations = 0;


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
        new Option<int>(new[] {"--customReducingCCount", "-cRC"}, "Carbon count for the reducing end"),
        new Option<int>(new[] {"--customReducingHCount", "-cRH"}, "Hydrogen count for the reducing end"),
        new Option<int>(new[] {"--customReducingNCount", "-cRN"}, "Nitrogen count for the reducing end"),
        new Option<int>(new[] {"--customReducingOCount", "-cRO"}, "Oxygen count for the reducing end"),
        new Option<string>(new[] {"--customAdductPolarity", "-cAP"}, "Positive or Negative"),
        new Option<decimal>(new[] {"--customAdductMass", "-cAM"}, "Mass of custom adduct"),

        new Option<string>(new[] {"--derivatisation", "-D" }, "Native, Permethylated or Peracetylated derivatisation"),
        new Option<string>(new[] {"--reducedEnd", "-R" }, "Free, Reduced, InstantPC, Rapifluor-MS (rapifluor), 2-aminobenzoic acid (2aa), 2-aminobenzamide (2ab), Procainamide, Girard's reagent P (girard), and Custom reducing end formats. E.g. \"Reduced\""),
        new Option<string>(new[] {"--adducts", "-A" }, "Neutral, MH+, MNa+, MNH4+, MH-, MFA-, MAA-, MTFA-"),
        new Option<decimal>(new[] {"--massError", "-E" }, "Mass error value, e.g. \"30\" or \"0.6\""),
        new Option<string>(new[] {"--massErrorType", "-T" }, "Mass error type can either be Da or ppm"),
        new Option<bool>(new[] {"--offByOne", "-O" }, "if set to true, enables off-by-one searching for cases of incorrect monoisotopic precursor determination"),
        new Option<bool>(new[] {"--noGlyTouCan", "-N" }, "Disable GlyTouCan base-composition accession annotation in output files"),
        new Option<string>(new[] {"--file", "-F" }, "Path to the input file, either .mzml, or .txt/.dat (mass list)"), // File upload option
        new Option<string>(new[] {"--outputPath"}, "Path for the output files (optional)"),

    };
        PrintBanner();
        rootCommand.Description = "A CLI for GlyCombo, allowing rapid assignment of monosaccharide combinations to observed and fragmented precursors in mass spectrometry experiments" + Environment.NewLine + Environment.NewLine + "Example command: GlyComboCLI.exe -F=\".\\example.mzML\" -hMin=1 -hMax=12 -nMin=2 -nMax=8 -sMin=0 -sMax=2 -fMin=0 -fMax=3 -gMin=0 -gMax=2 -D=\"Native\" -R=\"Reduced\" -T=Da -E=\"0.6\"" + Environment.NewLine + Environment.NewLine + "Questions, comments and bug reports:" + Environment.NewLine + "https://github.com/Protea-Glycosciences/GlyComboCLI" + Environment.NewLine + "chris@proteaglyco.com" + Environment.NewLine + "GlyComboCLI release: v0.0";
        rootCommand.Handler = CommandHandler.Create<CommandOptions>(options =>
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            if (string.IsNullOrWhiteSpace(options.file) || !File.Exists(options.file))
            {
                Console.WriteLine("No valid file path provided or file does not exist. Search terminated.");
                return;
            }

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
                options.reducedEnd = options.reducedEnd.Trim().ToLower();
                Console.WriteLine($"The reduced end is {options.reducedEnd}");
                switch (options.reducedEnd)
                {
                    case "free":
                    case "reduced":
                    case "instantpc":
                    case "rapifluor":
                    case "2aa":
                    case "2ab":
                    case "procainamide":
                    case "girard":
                    case "custom":
                        break;
                    default:
                        Console.WriteLine($"{options.reducedEnd} is not a valid option. GlyCombo supports Free, Reduced, InstantPC, Rapifluor-MS (rapifluor), 2-aminobenzoic acid (2aa), 2-aminobenzamide (2ab), Procainamide, Girard's reagent P (girard), and Custom. Search terminated.");
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
                string ext = Path.GetExtension(options.file).ToLower();
                bool isXmlDat = ext == ".dat" && File.ReadLines(options.file).First().Contains("xml");
                mzmlFile = ext == ".mzml" || isXmlDat;

                if (mzmlFile)
                {
                    List<Ms2ScanRecord> ms2Records;
                    try
                    {
                        ms2Records = MzmlMs2Parser.Parse(options.file);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to parse mzML file '{options.file}': {ex.Message}");
                        return;
                    }

                    if (ms2Records.Count == 0)
                    {
                        Console.WriteLine("No MS2 found in the given mzML file. Please confirm the selected file has MS2 scans, or select a different file.");
                        return;
                    }

                    foreach (var r in ms2Records)
                    {
                        neutralPrecursorListmzml += Convert.ToString(r.NeutralPrecursorMass) + Environment.NewLine;
                        if (decimal.TryParse(r.ScanNumber, System.Globalization.NumberStyles.Float,
                                              System.Globalization.CultureInfo.InvariantCulture, out decimal scanNum))
                            scans.Add(scanNum);
                        else
                            scans.Add(0);
                        charges.Add(r.Charge);
                        retentionTimes.Add(r.RetentionTimeMinutes);
                        TICs.Add(r.TotalIonCurrent);
                        files.Add(r.FileName);
                    }

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

            async Task ProcessingSteps()
            {
                try
                {
                    results.Clear();

                    catalog = MonosaccharideCatalog.BuildActive(options);
                    numbers = catalog.Select(d => d.Mass).ToList();
                    currentMonosaccharideSelection = MonosaccharideCatalog.FormatParameterReport(catalog);

                    bool monoCustom1 = options.customMono1Max > 0;
                    bool monoCustom2 = options.customMono2Max > 0;
                    bool monoCustom3 = options.customMono3Max > 0;
                    bool monoCustom4 = options.customMono4Max > 0;
                    bool monoCustom5 = options.customMono5Max > 0;


                    // Process for multiple targets conditionally based on text box or mzml input
                    string fileExtension = Path.GetExtension(options.file);
                    if (fileExtension.ToLower() == ".txt")
                    {
                        Console.WriteLine("Processing text file input.");
                        targetString = ReadMassFileWithSeparator(options.file, Environment.NewLine);
                    }
                    else if (fileExtension.ToLower() == ".mzml")
                    {
                        mzmlFile = true;
                        targetString = neutralPrecursorListmzml;
                    }
                    // Functionality for Galaxy .dat files
                    else if (fileExtension.ToLower() == ".dat")
                    {
                        Console.WriteLine("Processing dat file input.");
                        string firstline = File.ReadLines(options.file).First();
                        if (firstline.Contains("xml"))
                        {
                            mzmlFile = true;
                            targetString = neutralPrecursorListmzml;
                        }
                        else
                        {
                            targetString = ReadMassFileWithSeparator(options.file, Environment.NewLine);
                        }
                    }
                    else
                    {
                        Console.WriteLine("The file extension " + fileExtension + " is not supported.");
                        return;
                    }

                    if (options.outputPath != null)
                    {
                        if (mzmlFile == false)
                        {
                            filePath1 = Path.Combine(
                                options.outputPath + Path.GetFileNameWithoutExtension(options.file) + "_result" + ".csv");
                        }
                        else
                        {
                            filePath1 = Path.Combine(
                                options.outputPath + Path.GetFileNameWithoutExtension(options.file) + "_SkylineImport.csv");
                        }
                        filePath2 = Path.Combine(
                            options.outputPath + Path.GetFileNameWithoutExtension(options.file) + "_parameters.txt");
                    }
                    else
                    {
                        filePath = Path.Combine(
                            Path.GetDirectoryName(options.file),
                            Path.GetFileNameWithoutExtension(options.file));
                        if (mzmlFile == false)
                        {
                            filePath1 = Path.Combine(
                                filePath + "_result" + ".csv");
                        }
                        else
                        {
                            filePath1 = Path.Combine(
                                filePath + "_SkylineImport.csv");
                        }
                        filePath2 = Path.Combine(
                            filePath + "_parameters.txt");
                    }

                    // Turn that input into a list of masses
                    targetStrings = new(
                    targetString.Split(new string[] { "\n" },
                    StringSplitOptions.RemoveEmptyEntries));
                    targets = targetStrings
                    .Select(s => s.Trim())
                    .Where(s => !string.IsNullOrEmpty(s))
                    .Select(decimal.Parse)
                    .ToList();

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
                        if (mzmlFile == true)
                        {

                            // This all needs to be revised to find if the options.adducts CONTAINS the adduct text, rather than ==. This is because people can submit more than one adduct.
                            // Subtracting H- from all targets and saving that as a new list
                            if (options.adducts.Split(",").Select(a => a.Trim()).Contains("MH-") ||
                            options.adducts.Split(",").Select(a => a.Trim()).Contains("Neutral") ||
                            options.adducts.Split(",").Select(a => a.Trim()).Contains("MH+"))
                            {
                                searchRepeats += 1;
                                targetsToAdd = targetAdductsProcessing.Count;
                                for (int o = 0; o < targetsToAdd; o++)
                                {
                                    targetAdducts.Add(targetAdductsProcessing[o]);
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
                            if (options.adducts.Split(',').Select(a => a.Trim()).Contains("Neutral"))
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
                        Console.WriteLine("TargetAdducts count: " + targetAdducts.Count);
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
                            case "instantpc":
                                reducedEnd = "instantpc";
                                targets = targets.Select(z => z - (18.010555m + 261.14773m)).ToList();
                                break;
                            case "rapifluor":
                                reducedEnd = "rapifluor";
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
                            case "girard":
                                reducedEnd = "girard";
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
                    targets.Clear();
                    numbers.Clear();
                }
            }

            (ElementalFormula formula, decimal observed, decimal theoretical) ApplyReducingEnd(ElementalFormula monosaccharideFormula, decimal sum, decimal target)
            {
                ElementalFormula f = monosaccharideFormula;
                decimal obs = observedMass;
                decimal theo = theoreticalMass;

                switch (options.derivatisation)
                {
                    case "native":
                        switch (options.reducedEnd)
                        {
                            case "free": f.Hydrogen += 2; f.Oxygen += 1; obs = sum + 18.010565m; theo = target + 18.010565m; break;
                            case "reduced": f.Hydrogen += 4; f.Oxygen += 1; obs = sum + 20.026195m; theo = target + 20.026195m; break;
                            case "instantpc": f.Carbon += 14; f.Hydrogen += 21; f.Nitrogen += 3; f.Oxygen += 3; obs = sum + 18.010565m + 261.1477m; theo = target + 18.010565m + 261.1477m; break;
                            case "rapifluor": f.Carbon += 17; f.Hydrogen += 23; f.Nitrogen += 5; f.Oxygen += 2; obs = sum + 18.010565m + 311.17461m; theo = target + 18.010565m + 311.17461m; break;
                            case "2aa": f.Carbon += 7; f.Hydrogen += 9; f.Nitrogen += 1; f.Oxygen += 2; obs = sum + 18.010565m + 121.052774m; theo = target + 18.010565m + 121.052774m; break;
                            case "2ab": f.Carbon += 7; f.Hydrogen += 10; f.Nitrogen += 2; f.Oxygen += 1; obs = sum + 18.010565m + 120.068758m; theo = target + 18.010565m + 120.068758m; break;
                            case "procainamide": f.Carbon += 13; f.Hydrogen += 23; f.Nitrogen += 3; f.Oxygen += 1; obs = sum + 18.010565m + 219.1735574m; theo = target + 18.010565m + 219.1735574m; break;
                            case "girard": f.Carbon += 7; f.Hydrogen += 10; f.Nitrogen += 3; f.Oxygen += 1; obs = sum + 18.010565m + 134.06405m; theo = target + 18.010565m + 134.06405m; break;
                            case "custom": f.Carbon += options.customReducingCCount; f.Hydrogen += options.customReducingHCount; f.Nitrogen += options.customReducingNCount; f.Oxygen += options.customReducingOCount; obs = sum + 18.010565m + options.customReducingMass; theo = target + 18.010565m + options.customReducingMass; break;
                        }
                        break;

                    case "permethylated":
                        switch (options.reducedEnd)
                        {
                            case "free": f.Carbon += 2; f.Hydrogen += 6; f.Oxygen += 1; obs = sum + 18.010565m + 28.031300m; theo = target + 18.010565m + 28.031300m; break;
                            case "reduced": f.Carbon += 3; f.Hydrogen += 10; f.Oxygen += 1; obs = sum + 20.026195m + 42.046950m; theo = target + 20.026195m + 42.046950m; break;
                            case "custom": f.Carbon += options.customReducingCCount; f.Hydrogen += options.customReducingHCount; f.Nitrogen += options.customReducingNCount; f.Oxygen += options.customReducingOCount; obs = sum + 18.010565m + options.customReducingMass; theo = target + 18.010565m + options.customReducingMass; break;
                        }
                        break;

                    case "peracetylated":
                        switch (options.reducedEnd)
                        {
                            case "free": f.Carbon += 4; f.Hydrogen += 6; f.Oxygen += 3; obs = sum + 18.010565m + 84.021129m; theo = target + 18.010565m + 84.021129m; break;
                            case "reduced": f.Carbon += 6; f.Hydrogen += 10; f.Oxygen += 4; obs = sum + 20.026195m + 126.031694m; theo = target + 20.026195m + 126.031694m; break;
                            case "custom": f.Carbon += options.customReducingCCount; f.Hydrogen += options.customReducingHCount; f.Nitrogen += options.customReducingNCount; f.Oxygen += options.customReducingOCount; obs = sum + 18.010565m + options.customReducingMass; theo = target + 18.010565m + options.customReducingMass; break;
                        }
                        break;
                }

                return (f, obs, theo);
            }

            string LookUpGlyTouCanAccession(IReadOnlyDictionary<MonosaccharideDefinition, int> counts)
            {
                if (options.noGlyTouCan)
                {
                    return "";
                }

                var glyTouCanMappableKinds = new HashSet<string>
                {
                    "Hex", "HexNAc", "dHex", "HexA", "HexN", "Pent", "KDN",
                    "Neu5Ac", "Neu5Gc", "dHexNAc", "Phos", "Sulf", "Acetyl"
                };

                bool hasUnmappedComposition = counts.Any(kvp =>
                    kvp.Value > 0 && !glyTouCanMappableKinds.Contains(kvp.Key.Kind));

                if (hasUnmappedComposition)
                {
                    return "";
                }

                int CountOf(string kind) => counts
                    .Where(kvp => kvp.Key.Kind == kind)
                    .Sum(kvp => kvp.Value);

                return GlyTouCanBaseCompositionLookup.FindAccession(
                    CountOf("Hex"), CountOf("HexNAc"), CountOf("dHex"), CountOf("HexA"),
                    CountOf("HexN"), CountOf("Pent"), CountOf("KDN"), CountOf("Neu5Ac"),
                    CountOf("Neu5Gc"), CountOf("dHexNAc"), CountOf("Phos"), CountOf("Sulf"),
                    CountOf("Acetyl"));
            }

            void glyComboProcess()
            {
                iterations = 0;
                Sum_up(catalog, targets, options);
                Console.WriteLine("GlyComboCLI has finished running." + Environment.NewLine + results.Count + " monosaccharide combinations identified over " + iterations + " iterations." + Environment.NewLine);
            }

            // Process to match glycan compositions by sum_up_recursive
            void Sum_up(List<MonosaccharideDefinition> catalog, List<decimal> targets, CommandOptions options)
            {
                Console.WriteLine("Search started.");
                Console.WriteLine("Processing " + targets.Count + " precursors.");

                CompositionSearch.SumUp(
                    catalog,
                    targets,
                    options.massError,
                    options.massErrorType,
                    onResultFound: (raw, targetIndex) =>
                    {
                        var (formula, obs, theo) = ApplyReducingEnd(raw.Formula, raw.ObservedMass, raw.TheoreticalMass);

                        if (!CompositionSearch.AllCountsInBounds(raw.Counts, catalog))
                        {
                            return;
                        }

                        string accession = LookUpGlyTouCanAccession(raw.Counts);

                        Ms2ScanRecord? ms2 = null;
                        if (mzmlFile)
                        {
                            int index = (int)targetIndex % scans.Count;
                            ms2 = new Ms2ScanRecord
                            {
                                ScanNumber = scans.ElementAt(index).ToString(),
                                Charge = charges.ElementAt(index),
                                RetentionTimeMinutes = retentionTimes.ElementAt(index),
                                TotalIonCurrent = TICs.ElementAt(index),
                                FileName = files.ElementAt(index),
                            };
                        }

                        results.Add(new CompositionResult(
                            raw.Counts, theo, obs, obs - theo, formula, accession, ms2, options.derivatisation));
                    },
                    iterations: ref iterations);

                string separator = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator;
                bool includeGlyTouCan = !options.noGlyTouCan;
                string glyTouCanHeader = includeGlyTouCan ? separator + "GlyTouCan Base Composition" : "";

                if (mzmlFile)
                {
                    string header = "Molecule List Name" + separator + "Molecule Name" + separator + "Observed mass" + separator + "Theoretical mass" + separator + "Molecular Formula" + separator + "Mass error" + separator + "Scan number" + separator + "Precursor Charge" + separator + "Retention Time" + separator + "TIC" + separator + "Molecule Note" + glyTouCanHeader;
                    string body = string.Join(Environment.NewLine, results.Select(r => r.ToSkylineCsvRow(separator, includeGlyTouCan)));
                    File.WriteAllText(filePath1, header + Environment.NewLine + body);
                }
                else
                {
                    string header = "Composition" + separator + "Observed mass" + separator + "Theoretical mass" + separator + "ChemicalFormula" + separator + "Mass error" + glyTouCanHeader;
                    string body = string.Join(Environment.NewLine, results.Select(r => r.ToPlainCsvRow(separator, includeGlyTouCan)));
                    File.WriteAllText(filePath1, header + Environment.NewLine + body);
                }

                Console.WriteLine("File processing complete. Output written to: " + filePath1);

                string submitOutput = "GlyComboCLI (v1.1) search output" + Environment.NewLine;
                submitOutput += "<Input file> " + Path.GetFileName(options.file) + Environment.NewLine;
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
                    submitOutput += "<OffByOne enabled> " + Environment.NewLine;
                }
                submitOutput += "<GlyTouCan base-composition annotation> " + (options.noGlyTouCan ? "Disabled" : "Enabled") + Environment.NewLine;
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
                if (!string.IsNullOrWhiteSpace(options.adducts))
                {
                    submitOutput += options.adducts + Environment.NewLine;
                }
                else if (mzmlFile)
                {
                    bool positiveMode = charges.Any(c => c > 0);
                    bool negativeMode = charges.Any(c => c < 0);

                    if (positiveMode && negativeMode)
                    {
                        submitOutput += "M+xH, M-xH (inferred from mzML polarity)" + Environment.NewLine;
                    }
                    else if (positiveMode)
                    {
                        submitOutput += "M+xH (inferred from mzML positive polarity)" + Environment.NewLine;
                    }
                    else if (negativeMode)
                    {
                        submitOutput += "M-xH (inferred from mzML negative polarity)" + Environment.NewLine;
                    }
                    else
                    {
                        submitOutput += "None" + Environment.NewLine;
                    }
                }
                else
                {
                    submitOutput += "None" + Environment.NewLine;
                }
                File.WriteAllText(
                    filePath2,
                    submitOutput
                    + Environment.NewLine
                    + "<Precursor targets>"
                    + Environment.NewLine
                    + targetString
                );
                Console.WriteLine(submitOutput);

                stopwatch.Stop();

                Console.WriteLine(
                    "Total execution time: " +
                    stopwatch.Elapsed.ToString(@"hh\:mm\:ss\.fff")
                );
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