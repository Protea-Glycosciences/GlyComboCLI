namespace GlyComboCLI.Tests;

/// Fixture files (highman.txt, glycan_dummy_data.txt, glycan_dummy_deriv.txt, and the five
/// .mzML files) are identical to Galaxy GlyCombo files
public class CompositionSearchTests
{
    // ---- Tests 1-8: monosaccharides, modifications, reducing ends, and adducts ----

    [Fact(DisplayName = "#1 Free & Neutral (highman.txt)")]
    public void Test01_FreeAndNeutral()
    {
        var result = GlyComboCliRunner.Run("highman.txt", new Dictionary<string, string>
        {
            ["derivatisation"] = "native",
            ["reducedEnd"] = "free",
            ["massError"] = "0.6",
            ["massErrorType"] = "Da",
            ["adducts"] = "Neutral",
            ["hexMin"] = "0",
            ["hexMax"] = "9",
            ["hexNAcMin"] = "0",
            ["hexNAcMax"] = "4",
        });

        Assert.Equal(0, result.ExitCode);
        string csv = result.ReadResultCsv();
        Assert.False(string.IsNullOrWhiteSpace(csv));
    }

    [Fact(DisplayName = "#2 Reduced & MTFA- (glycan_dummy_data.txt)")]
    public void Test02_ReducedAndMtfa()
    {
        var result = GlyComboCliRunner.Run("glycan_dummy_data.txt", new Dictionary<string, string>
        {
            ["derivatisation"] = "native",
            ["reducedEnd"] = "reduced",
            ["massError"] = "0.6",
            ["massErrorType"] = "Da",
            ["adducts"] = "MTFA-",
            ["hexMin"] = "0",
            ["hexMax"] = "2",
            ["hexAMin"] = "0",
            ["hexAMax"] = "3",
            ["hexNMin"] = "0",
            ["hexNMax"] = "2",
            ["pentMin"] = "0",
            ["pentMax"] = "3",
            ["acetylMin"] = "0",
            ["acetylMax"] = "2",
        });

        string csv = result.ReadResultCsv();
        Assert.Contains("(HexA)2 (Hex)2 (Acetyl)1", csv);
        Assert.Contains("(HexA)3 (Pent)3", csv);
        Assert.Contains("(HexN)2 (Acetyl)2", csv);
        Assert.Contains("(HexA)1", csv);
        Assert.Contains("(HexN)1 (Hex)1 (Acetyl)1", csv);
    }

    [Fact(DisplayName = "#3 InstantPC & MFA- (glycan_dummy_data.txt)")]
    public void Test03_InstantPcAndMfa()
    {
        var result = GlyComboCliRunner.Run("glycan_dummy_data.txt", new Dictionary<string, string>
        {
            ["derivatisation"] = "native",
            ["reducedEnd"] = "instantpc",
            ["massError"] = "0.6",
            ["massErrorType"] = "Da",
            ["adducts"] = "MFA-",
            ["hexMax"] = "3",
            ["lNeuAcMax"] = "2",
            ["dNeuAcMax"] = "2",
            ["eeNeuAcMax"] = "3",
        });

        string csv = result.ReadResultCsv();
        Assert.Contains("(Hex)3 (lNeuAc)1 (dNeuAc)1", csv);
        Assert.Contains("(eNeuAc)1", csv);
        Assert.Contains("(Hex)1 (lNeuAc)2", csv);
        Assert.Contains("(lNeuAc)1 (eNeuAc)3 (dNeuAc)1", csv);
        Assert.Contains("(Hex)2 (dNeuAc)2", csv);
    }

    [Fact(DisplayName = "#4 Rapifluor & MAA- (glycan_dummy_data.txt)")]
    public void Test04_RapifluorAndMaa()
    {
        var result = GlyComboCliRunner.Run("glycan_dummy_data.txt", new Dictionary<string, string>
        {
            ["derivatisation"] = "native",
            ["reducedEnd"] = "rapifluor",
            ["massError"] = "0.6",
            ["massErrorType"] = "Da",
            ["adducts"] = "MAA-",
            ["hexMax"] = "3",
            ["amNeuAcMax"] = "3",
            ["lNeuGcMax"] = "2",
            ["eeNeuGcMax"] = "3",
            ["dNeuGcMax"] = "2",
        });

        string csv = result.ReadResultCsv();
        Assert.Contains("(Hex)2 (amNeuAc)2 (eNeuGc)3", csv);
        Assert.Contains("(lNeuGc)2 (eNeuGc)3", csv);
        Assert.Contains("(Hex)3 (lNeuGc)1 (dNeuGc)2", csv);
        Assert.Contains("(amNeuAc)1 (lNeuGc)2 (dNeuGc)1", csv);
        Assert.Contains("(Hex)1 (amNeuAc)3 (eNeuGc)1 (dNeuGc)1", csv);
    }

    [Fact(DisplayName = "#5 2AA & MNH4+ (glycan_dummy_data.txt)")]
    public void Test05_2aaAndMnh4()
    {
        var result = GlyComboCliRunner.Run("glycan_dummy_data.txt", new Dictionary<string, string>
        {
            ["derivatisation"] = "native",
            ["reducedEnd"] = "2aa",
            ["massError"] = "0.6",
            ["massErrorType"] = "Da",
            ["adducts"] = "MNH4+",
            ["hexMin"] = "1",
            ["hexMax"] = "3",
            ["amNeuGcMin"] = "0",
            ["amNeuGcMax"] = "2",
            ["phosMin"] = "0",
            ["phosMax"] = "3",
        });

        string csv = result.ReadResultCsv();
        Assert.Contains("(Hex)1 (amNeuGc)1", csv);
        Assert.Contains("(Hex)2 (Phos)1", csv);
        Assert.Contains("(Hex)3 (Phos)1 (amNeuGc)2", csv);
        Assert.Contains("(Hex)2 (Phos)3", csv);
    }

    [Fact(DisplayName = "#6 2AB & MK+ (glycan_dummy_data.txt)")]
    public void Test06_2abAndMk()
    {
        var result = GlyComboCliRunner.Run("glycan_dummy_data.txt", new Dictionary<string, string>
        {
            ["derivatisation"] = "native",
            ["reducedEnd"] = "2ab",
            ["massError"] = "0.6",
            ["massErrorType"] = "Da",
            ["adducts"] = "MK+",
            ["hexMin"] = "1",
            ["hexMax"] = "3",
            ["sulfMin"] = "0",
            ["sulfMax"] = "2",
            ["dHexNAcMin"] = "0",
            ["dHexNAcMax"] = "3",
        });

        string csv = result.ReadResultCsv();
        Assert.Contains("(Hex)1 (Sulf)1", csv);
        Assert.Contains("(Hex)2 (dHexNAc)3", csv);
        Assert.Contains("(Hex)3 (Sulf)2", csv);
        Assert.Contains("(Hex)2 (dHexNAc)2", csv);
    }

    [Fact(DisplayName = "#7 Procainamide & MNa+ (glycan_dummy_data.txt)")]
    public void Test07_ProcainamideAndMna()
    {
        var result = GlyComboCliRunner.Run("glycan_dummy_data.txt", new Dictionary<string, string>
        {
            ["derivatisation"] = "native",
            ["reducedEnd"] = "procainamide",
            ["massError"] = "0.6",
            ["massErrorType"] = "Da",
            ["adducts"] = "MNa+",
            ["hexMin"] = "0",
            ["hexMax"] = "4",
            ["hexNAcMin"] = "0",
            ["hexNAcMax"] = "3",
            ["neu5GcMin"] = "0",
            ["neu5GcMax"] = "3",
        });

        string csv = result.ReadResultCsv();
        Assert.Contains("(Hex)2 (NeuGc)1", csv);
        Assert.Contains("(HexNAc)3", csv);
        Assert.Contains("(Hex)1 (NeuGc)3 (HexNAc)2", csv);
        Assert.Contains("(HexNAc)2", csv);
        Assert.Contains("(Hex)4 (NeuGc)3", csv);
    }

    [Fact(DisplayName = "#8 Girard's reagent P & Neutral (glycan_dummy_data.txt)")]
    public void Test08_GirardAndNeutral()
    {
        var result = GlyComboCliRunner.Run("glycan_dummy_data.txt", new Dictionary<string, string>
        {
            ["derivatisation"] = "native",
            ["reducedEnd"] = "girard",
            ["massError"] = "0.6",
            ["massErrorType"] = "Da",
            ["adducts"] = "Neutral",
            ["hexMin"] = "0",
            ["hexMax"] = "4",
            ["hexNAcMin"] = "0",
            ["hexNAcMax"] = "3",
            ["neu5AcMin"] = "0",
            ["neu5AcMax"] = "1",
        });

        string csv = result.ReadResultCsv();
        Assert.Contains("(Hex)3 (NeuAc)1 (HexNAc)1", csv);
        Assert.Contains("(HexNAc)3", csv);
        Assert.Contains("(Hex)4", csv);
        Assert.Contains("(HexNAc)1", csv);
        Assert.Contains("(Hex)1 (NeuAc)1", csv);
    }

    // ---- Tests 9-12: derivatisation, and the peracetylated HexN/HexNAc mass-degeneracy cases ----

    [Fact(DisplayName = "#9 Permethylated (glycan_dummy_deriv.txt)")]
    public void Test09_Permethylated()
    {
        var result = GlyComboCliRunner.Run("glycan_dummy_deriv.txt", new Dictionary<string, string>
        {
            ["derivatisation"] = "permethylated",
            ["reducedEnd"] = "free",
            ["massError"] = "0.6",
            ["massErrorType"] = "Da",
            ["adducts"] = "Neutral",
            ["hexMin"] = "0",
            ["hexMax"] = "1",
            ["dHexMin"] = "0",
            ["dHexMax"] = "1",
            ["hexNMin"] = "0",
            ["hexNMax"] = "1",
            ["hexAMin"] = "0",
            ["hexAMax"] = "1",
            ["pentMin"] = "0",
            ["pentMax"] = "1",
            ["hexNAcMin"] = "0",
            ["hexNAcMax"] = "1",
            ["kdnMin"] = "0",
            ["kdnMax"] = "1",
            ["neu5AcMin"] = "0",
            ["neu5AcMax"] = "1",
            ["neu5GcMin"] = "0",
            ["neu5GcMax"] = "1",
            ["phosMin"] = "0",
            ["phosMax"] = "1",
            ["sulfMin"] = "0",
            ["sulfMax"] = "1",
        });

        string csv = result.ReadResultCsv();
        Assert.Contains("(dHex)1 (HexN)1 (KDN)1 (Hex)1", csv);
        Assert.Contains("(HexA)1 (Pent)1", csv);
        Assert.Contains("(NeuAc)1 (NeuGc)1 (HexNAc)1", csv);
        Assert.Contains("(Hex)1 (HexNAc)1 (Phos)1", csv);
        Assert.Contains("(Hex)1 (HexNAc)1 (Sulf)1", csv);
    }

    [Fact(DisplayName = "#10 Peracetylated, HexN and HexNAc both enabled -> combined label (glycan_dummy_deriv.txt)")]
    public void Test10_PeracetylatedCombinedHexNHexNAc()
    {
        var result = GlyComboCliRunner.Run("glycan_dummy_deriv.txt", new Dictionary<string, string>
        {
            ["derivatisation"] = "peracetylated",
            ["reducedEnd"] = "free",
            ["massError"] = "0.6",
            ["massErrorType"] = "Da",
            ["adducts"] = "Neutral",
            ["hexMin"] = "0",
            ["hexMax"] = "1",
            ["dHexMin"] = "0",
            ["dHexMax"] = "1",
            ["hexNMin"] = "0",
            ["hexNMax"] = "1",
            ["hexAMin"] = "0",
            ["hexAMax"] = "1",
            ["pentMin"] = "0",
            ["pentMax"] = "1",
            ["hexNAcMin"] = "0",
            ["hexNAcMax"] = "1",
            ["kdnMin"] = "0",
            ["kdnMax"] = "1",
            ["neu5AcMin"] = "0",
            ["neu5AcMax"] = "1",
            ["neu5GcMin"] = "0",
            ["neu5GcMax"] = "1",
            ["phosMin"] = "0",
            ["phosMax"] = "1",
            ["sulfMin"] = "0",
            ["sulfMax"] = "1",
        });

        string csv = result.ReadResultCsv();
        Assert.Contains("(dHex)1 (KDN)1 (Hex)1 (HexNAc/HexN)1", csv);
        Assert.Contains("(HexA)1 (Pent)1", csv);
        Assert.Contains("(NeuAc)1 (NeuGc)1 (HexNAc/HexN)1", csv);
        Assert.Contains("(Hex)1 (HexNAc/HexN)1 (Phos)1", csv);
        Assert.Contains("(Hex)1 (HexNAc/HexN)1 (Sulf)1", csv);
    }

    [Fact(DisplayName = "#11 Peracetylated, only HexNAc enabled -> HexNAc label (glycan_dummy_deriv.txt)")]
    public void Test11_PeracetylatedHexNAcOnly()
    {
        var result = GlyComboCliRunner.Run("glycan_dummy_deriv.txt", new Dictionary<string, string>
        {
            ["derivatisation"] = "peracetylated",
            ["reducedEnd"] = "free",
            ["massError"] = "0.6",
            ["massErrorType"] = "Da",
            ["adducts"] = "Neutral",
            ["hexMin"] = "0",
            ["hexMax"] = "1",
            ["dHexMin"] = "0",
            ["dHexMax"] = "1",
            ["hexAMin"] = "0",
            ["hexAMax"] = "1",
            ["pentMin"] = "0",
            ["pentMax"] = "1",
            ["hexNAcMin"] = "0",
            ["hexNAcMax"] = "1",
            ["kdnMin"] = "0",
            ["kdnMax"] = "1",
            ["neu5AcMin"] = "0",
            ["neu5AcMax"] = "1",
            ["neu5GcMin"] = "0",
            ["neu5GcMax"] = "1",
            ["phosMin"] = "0",
            ["phosMax"] = "1",
            ["sulfMin"] = "0",
            ["sulfMax"] = "1",
            // hexNMin/hexNMax intentionally omitted (default 0) - HexN branch disabled
        });

        string csv = result.ReadResultCsv();
        Assert.Contains("(dHex)1 (KDN)1 (Hex)1 (HexNAc)1", csv);
        Assert.Contains("(HexA)1 (Pent)1", csv);
        Assert.Contains("(NeuAc)1 (NeuGc)1 (HexNAc)1", csv);
        Assert.Contains("(Hex)1 (HexNAc)1 (Phos)1", csv);
        Assert.Contains("(Hex)1 (HexNAc)1 (Sulf)1", csv);
    }

    [Fact(DisplayName = "#12 Peracetylated, only HexN enabled -> HexN label (glycan_dummy_deriv.txt)")]
    public void Test12_PeracetylatedHexNOnly()
    {
        var result = GlyComboCliRunner.Run("glycan_dummy_deriv.txt", new Dictionary<string, string>
        {
            ["derivatisation"] = "peracetylated",
            ["reducedEnd"] = "free",
            ["massError"] = "0.6",
            ["massErrorType"] = "Da",
            ["adducts"] = "Neutral",
            ["hexMin"] = "0",
            ["hexMax"] = "1",
            ["dHexMin"] = "0",
            ["dHexMax"] = "1",
            ["hexNMin"] = "0",
            ["hexNMax"] = "1",
            ["hexAMin"] = "0",
            ["hexAMax"] = "1",
            ["pentMin"] = "0",
            ["pentMax"] = "1",
            ["kdnMin"] = "0",
            ["kdnMax"] = "1",
            ["neu5AcMin"] = "0",
            ["neu5AcMax"] = "1",
            ["neu5GcMin"] = "0",
            ["neu5GcMax"] = "1",
            ["phosMin"] = "0",
            ["phosMax"] = "1",
            ["sulfMin"] = "0",
            ["sulfMax"] = "1",
            // hexNAcMin/hexNAcMax intentionally omitted (default 0) - HexNAc branch disabled
        });

        string csv = result.ReadResultCsv();
        Assert.Contains("(dHex)1 (KDN)1 (Hex)1 (HexN)1", csv);
        Assert.Contains("(HexA)1 (Pent)1", csv);
        Assert.Contains("(NeuAc)1 (NeuGc)1 (HexN)1", csv);
        Assert.Contains("(Hex)1 (HexN)1 (Phos)1", csv);
        Assert.Contains("(Hex)1 (HexN)1 (Sulf)1", csv);
    }

    // ---- Tests 13-17: mzML input across vendors (writes *_SkylineImport.csv) ----

    [Fact(DisplayName = "#13 Bruker mzML")]
    public void Test13_Bruker()
    {
        var result = GlyComboCliRunner.Run("Bruker_Reduced.mzML", new Dictionary<string, string>
        {
            ["derivatisation"] = "native",
            ["reducedEnd"] = "reduced",
            ["massError"] = "1.0",
            ["massErrorType"] = "Da",
            ["hexMin"] = "0",
            ["hexMax"] = "5",
            ["hexNAcMin"] = "0",
            ["hexNAcMax"] = "4",
            ["dHexMin"] = "0",
            ["dHexMax"] = "1",
            ["neu5AcMin"] = "0",
            ["neu5AcMax"] = "2",
            // adducts intentionally omitted - CLI falls back to its own default handling
        });

        string csv = result.ReadSkylineImportCsv();
        Assert.Contains("(dHex)1 (Hex)5 (NeuAc)2 (HexNAc)4", csv);
    }

    [Fact(DisplayName = "#14 Agilent mzML")]
    public void Test14_Agilent()
    {
        var result = GlyComboCliRunner.Run("Agilent_Free.mzML", new Dictionary<string, string>
        {
            ["derivatisation"] = "native",
            ["reducedEnd"] = "free",
            ["massError"] = "20",
            ["massErrorType"] = "ppm",
            ["adducts"] = "MH+",
            ["hexMin"] = "5",
            ["hexMax"] = "5",
            ["hexNAcMin"] = "4",
            ["hexNAcMax"] = "4",
            ["neu5AcMin"] = "1",
            ["neu5AcMax"] = "1",
        });

        string csv = result.ReadSkylineImportCsv();
        Assert.Contains("(Hex)5 (NeuAc)1 (HexNAc)4", csv);
    }

    [Fact(DisplayName = "#15 Thermo mzML")]
    public void Test15_Thermo()
    {
        var result = GlyComboCliRunner.Run("Thermo_Reduced.mzML", new Dictionary<string, string>
        {
            ["derivatisation"] = "native",
            ["reducedEnd"] = "reduced",
            ["massError"] = "0.6",
            ["massErrorType"] = "Da",
            ["adducts"] = "MH+",
            ["hexMin"] = "1",
            ["hexMax"] = "2",
            ["hexNAcMin"] = "2",
            ["hexNAcMax"] = "2",
        });

        string csv = result.ReadSkylineImportCsv();
        Assert.Contains("(Hex)2 (HexNAc)2", csv);
    }

    [Fact(DisplayName = "#16 Waters mzML")]
    public void Test16_Waters()
    {
        var result = GlyComboCliRunner.Run("Waters_ProA.mzML", new Dictionary<string, string>
        {
            ["derivatisation"] = "native",
            ["reducedEnd"] = "procainamide",
            ["massError"] = "50",
            ["massErrorType"] = "ppm",
            ["adducts"] = "Neutral",
            ["hexMin"] = "2",
            ["hexMax"] = "2",
            ["neu5AcMin"] = "1",
            ["neu5AcMax"] = "1",
        });

        string csv = result.ReadSkylineImportCsv();
        Assert.Contains("(Hex)2 (NeuAc)1", csv);
    }

    [Fact(DisplayName = "#17 Sciex mzML")]
    public void Test17_Sciex()
    {
        var result = GlyComboCliRunner.Run("Sciex_ZenoTOF.mzML", new Dictionary<string, string>
        {
            ["derivatisation"] = "native",
            ["reducedEnd"] = "free",
            ["massError"] = "20",
            ["massErrorType"] = "ppm",
            ["adducts"] = "Neutral",
            ["hexMin"] = "5",
            ["hexMax"] = "5",
            ["hexNAcMin"] = "4",
            ["hexNAcMax"] = "4",
            ["neu5AcMin"] = "2",
            ["neu5AcMax"] = "2",
        });

        string csv = result.ReadSkylineImportCsv();
        Assert.Contains("(Hex)5 (NeuAc)2 (HexNAc)4", csv);
    }
}