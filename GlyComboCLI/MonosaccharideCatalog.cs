using GlyComboCLI;

namespace GlyCombo;

internal static class MonosaccharideCatalog
{

    public static List<MonosaccharideDefinition> BuildActive(CommandOptions options)
    {
        var active = new List<MonosaccharideDefinition>();

        foreach (MonosaccharideDefinition def in BuildAllForDerivatisation(options))
        {
            if (def.Max > 0)
            {
                active.Add(def);
            }
        }

        active.AddRange(BuildActiveCustomMonosaccharides(options));

        return active;
    }

    /// Formats the parameter-report line ("Hex(0-9), HexNAc(0-4), ...") from the active

    public static string FormatParameterReport(IEnumerable<MonosaccharideDefinition> active)
    {
        return string.Join("", active.Select(d => $"{d.DisplayLabel}({d.Min}-{d.Max}), "));
    }

    private static IEnumerable<MonosaccharideDefinition> BuildAllForDerivatisation(CommandOptions o)
    {
        return o.derivatisation switch
        {
            "native" => Native(o),
            "permethylated" => Permethylated(o),
            "peracetylated" => Peracetylated(o),
            _ => Enumerable.Empty<MonosaccharideDefinition>(),
        };
    }

    private static IEnumerable<MonosaccharideDefinition> Native(CommandOptions o)
    {
        // Name, Name, Mass, C, H, N, O, P, S, Min, Max
        yield return new("Hex", "Hex", 162.052823m, 6, 10, 0, 5, 0, 0, o.HexMin, o.HexMax);
        yield return new("HexA", "HexA", 176.032088m, 6, 8, 0, 6, 0, 0, o.HexAMin, o.HexAMax);
        yield return new("dHex", "dHex", 146.057908m, 6, 10, 0, 4, 0, 0, o.dHexMin, o.dHexMax);
        yield return new("HexNAc", "HexNAc", 203.079372m, 8, 13, 1, 5, 0, 0, o.HexNAcMin, o.HexNAcMax);
        yield return new("HexN", "HexN", 161.068808m, 6, 11, 1, 4, 0, 0, o.HexNMin, o.HexNMax);
        yield return new("dHexNAc", "dHexNAc", 187.084458m, 8, 13, 1, 4, 0, 0, o.dHexNAcMin, o.dHexNAcMax);
        yield return new("Pent", "Pent", 132.042258m, 5, 8, 0, 4, 0, 0, o.PentMin, o.PentMax);
        yield return new("KDN", "KDN", 250.068867m, 9, 14, 0, 8, 0, 0, o.KDNMin, o.KDNMax);
        yield return new("Neu5Ac", "Neu5Ac", 291.095416m, 11, 17, 1, 8, 0, 0, o.Neu5AcMin, o.Neu5AcMax);
        yield return new("Neu5Gc", "Neu5Gc", 307.090331m, 11, 17, 1, 9, 0, 0, o.Neu5GcMin, o.Neu5GcMax);
        yield return new("Phos", "Phos", 79.966331m, 0, 1, 0, 3, 1, 0, o.PhosMin, o.PhosMax);
        yield return new("Sulf", "Sulf", 79.956815m, 0, 0, 0, 3, 0, 1, o.SulfMin, o.SulfMax);
        yield return new("lNeuAc", "lNeuAc", 273.0848518m, 11, 15, 1, 7, 0, 0, o.lNeuAcMin, o.lNeuAcMax);
        yield return new("eeNeuAc", "eNeuAc", 319.1267166m, 13, 21, 1, 8, 0, 0, o.eeNeuAcMin, o.eeNeuAcMax);
        yield return new("dNeuAc", "dNeuAc", 318.1427011m, 13, 22, 2, 7, 0, 0, o.dNeuAcMin, o.dNeuAcMax);
        yield return new("amNeuAc", "amNeuAc", 290.1114009m, 11, 18, 2, 7, 0, 0, o.amNeuAcMin, o.amNeuAcMax);
        yield return new("Acetyl", "Acetyl", 42.010565m, 2, 2, 0, 1, 0, 0, o.acetylMin, o.acetylMax);
        yield return new("lNeuGc", "lNeuGc", 289.0797664m, 11, 15, 1, 8, 0, 0, o.lNeuGcMin, o.lNeuGcMax);
        yield return new("eeNeuGc", "eNeuGc", 335.1216313m, 13, 21, 1, 9, 0, 0, o.eeNeuGcMin, o.eeNeuGcMax);
        yield return new("dNeuGc", "dNeuGc", 306.1063155m, 13, 22, 2, 8, 0, 0, o.dNeuGcMin, o.dNeuGcMax);
        yield return new("amNeuGc", "amNeuGc", 334.1376157m, 11, 18, 2, 8, 0, 0, o.amNeuGcMin, o.amNeuGcMax);
    }

    private static IEnumerable<MonosaccharideDefinition> Permethylated(CommandOptions o)
    {
        yield return new("Hex", "Hex", 204.099775m, 9, 16, 0, 5, 0, 0, o.HexMin, o.HexMax);
        yield return new("HexA", "HexA", 218.079040m, 9, 14, 0, 6, 0, 0, o.HexAMin, o.HexAMax);
        yield return new("dHex", "dHex", 174.089210m, 8, 14, 0, 4, 0, 0, o.dHexMin, o.dHexMax);
        yield return new("HexNAc", "HexNAc", 245.126324m, 11, 19, 1, 5, 0, 0, o.HexNAcMin, o.HexNAcMax);
        yield return new("HexN", "HexN", 203.115758m, 10, 19, 1, 4, 0, 0, o.HexNMin, o.HexNMax);
        yield return new("dHexNAc", "dHexNAc", 215.115759m, 10, 17, 1, 4, 0, 0, o.dHexNAcMin, o.dHexNAcMax);
        yield return new("Pent", "Pent", 160.073560m, 7, 12, 0, 4, 0, 0, o.PentMin, o.PentMax);
        yield return new("KDN", "KDN", 320.147120m, 14, 24, 0, 8, 0, 0, o.KDNMin, o.KDNMax);
        yield return new("Neu5Ac", "Neu5Ac", 361.173669m, 16, 27, 1, 8, 0, 0, o.Neu5AcMin, o.Neu5AcMax);
        yield return new("Neu5Gc", "Neu5Gc", 391.184234m, 17, 29, 1, 9, 0, 0, o.Neu5GcMin, o.Neu5GcMax);
        yield return new("Phos", "Phos", 93.981980m, 1, 3, 0, 3, 1, 0, o.PhosMin, o.PhosMax);
        yield return new("Sulf", "Sulf", 65.941165m, -1, -2, 0, 3, 0, 1, o.SulfMin, o.SulfMax);
    }

    private static IEnumerable<MonosaccharideDefinition> Peracetylated(CommandOptions o)
    {
        yield return new("Hex", "Hex", 288.084517m, 12, 16, 0, 8, 0, 0, o.HexMin, o.HexMax);
        yield return new("HexA", "HexA", 260.053217m, 10, 12, 0, 8, 0, 0, o.HexAMin, o.HexAMax);
        yield return new("dHex", "dHex", 230.079038m, 10, 14, 0, 6, 0, 0, o.dHexMin, o.dHexMax);
        yield return new("dHexNAc", "dHexNAc", 247.105587m, 10, 17, 1, 6, 0, 0, o.dHexNAcMin, o.dHexNAcMax);
        yield return new("Pent", "Pent", 216.063388m, 9, 12, 0, 6, 0, 0, o.PentMin, o.PentMax);
        yield return new("KDN", "KDN", 376.100561m, 15, 20, 0, 11, 0, 0, o.KDNMin, o.KDNMax);
        yield return new("Neu5Ac", "Neu5Ac", 417.127110m, 17, 23, 1, 11, 0, 0, o.Neu5AcMin, o.Neu5AcMax);
        yield return new("Neu5Gc", "Neu5Gc", 475.132593m, 19, 25, 1, 13, 0, 0, o.Neu5GcMin, o.Neu5GcMax);
        yield return new("Phos", "Phos", 37.955765m, -2, -1, 0, 2, 1, 0, o.PhosMin, o.PhosMax);
        yield return new("Sulf", "Sulf", 37.946250m, -2, -2, 0, 2, 0, 1, o.SulfMin, o.SulfMax);

        // Peracetylated HexN and HexNAc are exact same mass
        if (o.HexNAcMax > 0 && o.HexNMax > 0)
        {
            yield return new("HexNAc(degenerate)", "HexNAc/HexN", 287.100501m, 12, 17, 1, 7, 0, 0, o.HexNAcMin, o.HexNAcMax);
            yield return new("HexN(degenerate)", "HexNAc/HexN", 287.100501m, 12, 17, 1, 7, 0, 0, o.HexNMin, o.HexNMax);
        }
        else if (o.HexNAcMax > 0)
        {
            yield return new("HexNAc", "HexNAc", 287.100501m, 12, 17, 1, 7, 0, 0, o.HexNAcMin, o.HexNAcMax);
        }
        else if (o.HexNMax > 0)
        {
            yield return new("HexN", "HexN", 287.100501m, 12, 17, 1, 7, 0, 0, o.HexNMin, o.HexNMax);
        }
    }

    private static IEnumerable<MonosaccharideDefinition> BuildActiveCustomMonosaccharides(CommandOptions o)
    {
        if (o.customMono1Max > 0 && o.customMono1Name is not null)
            yield return new(o.customMono1Name, o.customMono1Name, o.customMono1Mass,
                o.customMono1CCount, o.customMono1HCount, o.customMono1NCount, o.customMono1OCount, 0, 0,
                o.customMono1Min, o.customMono1Max);

        if (o.customMono2Max > 0 && o.customMono2Name is not null)
            yield return new(o.customMono2Name, o.customMono2Name, o.customMono2Mass,
                o.customMono2CCount, o.customMono2HCount, o.customMono2NCount, o.customMono2OCount, 0, 0,
                o.customMono2Min, o.customMono2Max);

        if (o.customMono3Max > 0 && o.customMono3Name is not null)
            yield return new(o.customMono3Name, o.customMono3Name, o.customMono3Mass,
                o.customMono3CCount, o.customMono3HCount, o.customMono3NCount, o.customMono3OCount, 0, 0,
                o.customMono3Min, o.customMono3Max);

        if (o.customMono4Max > 0 && o.customMono4Name is not null)
            yield return new(o.customMono4Name, o.customMono4Name, o.customMono4Mass,
                o.customMono4CCount, o.customMono4HCount, o.customMono4NCount, o.customMono4OCount, 0, 0,
                o.customMono4Min, o.customMono4Max);

        if (o.customMono5Max > 0 && o.customMono5Name is not null)
            yield return new(o.customMono5Name, o.customMono5Name, o.customMono5Mass,
                o.customMono5CCount, o.customMono5HCount, o.customMono5NCount, o.customMono5OCount, 0, 0,
                o.customMono5Min, o.customMono5Max);
    }
}