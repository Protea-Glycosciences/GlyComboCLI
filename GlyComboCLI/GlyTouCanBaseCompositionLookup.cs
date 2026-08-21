using System.Collections.Frozen;
using System.Globalization;
using System.Reflection;

/// <summary>
/// Maps GlyComboCLI monosaccharide counts to GlyTouCan base-composition accessions.
///
/// The lookup is a pinned, offline snapshot derived from the GlyCosmos validated
/// GlyTouCan dataset. The TSV is parsed once, then stored as a FrozenDictionary
/// keyed by a value-type composition key for fast repeated lookups.
/// </summary>
internal static class GlyTouCanBaseCompositionLookup
{
    private const string ResourceName = "GlyComboCLI.Data.glytoucan_base_composition_lookup.tsv";

    private readonly record struct CompositionKey(
        int Hex,
        int HexNAc,
        int DHex,
        int HexA,
        int HexN,
        int Pent,
        int KDN,
        int Neu5Ac,
        int Neu5Gc,
        int DHexNAc,
        int P,
        int S,
        int Ac
    );

    private static readonly Lazy<FrozenDictionary<CompositionKey, string>> Lookup =
        new(LoadLookup, isThreadSafe: true);

    /// <summary>
    /// Returns the matching GlyTouCan base-composition accession, or an empty
    /// string when no registered compatible base composition exists.
    ///
    /// Unsupported or ambiguous composition types must be rejected by the caller
    /// before invoking this method so that they do not query the dictionary.
    /// </summary>
    internal static string FindAccession(
        int hex,
        int hexNAc,
        int dHex,
        int hexA,
        int hexN,
        int pent,
        int kdn,
        int neu5Ac,
        int neu5Gc,
        int dHexNAc,
        int phosphate,
        int sulfate,
        int acetyl)
    {
        CompositionKey key = new(
            hex,
            hexNAc,
            dHex,
            hexA,
            hexN,
            pent,
            kdn,
            neu5Ac,
            neu5Gc,
            dHexNAc,
            phosphate,
            sulfate,
            acetyl
        );

        return Lookup.Value.TryGetValue(key, out string? accession)
            ? accession
            : string.Empty;
    }

    private static FrozenDictionary<CompositionKey, string> LoadLookup()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        using Stream? stream = assembly.GetManifestResourceStream(ResourceName);
        if (stream is null)
        {
            throw new InvalidOperationException(
                $"Embedded GlyTouCan lookup resource '{ResourceName}' could not be found.");
        }

        using StreamReader reader = new(stream);
        Dictionary<CompositionKey, string> lookup = new(capacity: 4096);
        string? line;

        while ((line = reader.ReadLine()) is not null)
        {
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith('#'))
            {
                continue;
            }

            int tabIndex = line.IndexOf('\t');
            if (tabIndex <= 0)
            {
                continue;
            }

            ReadOnlySpan<char> compositionText = line.AsSpan(0, tabIndex);
            ReadOnlySpan<char> accessionText = line.AsSpan(tabIndex + 1);

            if (compositionText.SequenceEqual("CompositionKey".AsSpan()))
            {
                continue;
            }

            if (!TryParseCompositionKey(compositionText, out CompositionKey key))
            {
                continue;
            }

            lookup[key] = accessionText.ToString();
        }

        return lookup.ToFrozenDictionary();
    }

    private static bool TryParseCompositionKey(
        ReadOnlySpan<char> compositionText,
        out CompositionKey key)
    {
        Span<int> counts = stackalloc int[13];
        int countIndex = 0;
        int tokenStart = 0;

        while (tokenStart <= compositionText.Length && countIndex < counts.Length)
        {
            int separatorOffset = compositionText[tokenStart..].IndexOf(';');
            int tokenLength = separatorOffset >= 0
                ? separatorOffset
                : compositionText.Length - tokenStart;

            ReadOnlySpan<char> token = compositionText.Slice(tokenStart, tokenLength);
            int equalsIndex = token.IndexOf('=');

            if (equalsIndex < 0 ||
                !int.TryParse(
                    token[(equalsIndex + 1)..],
                    NumberStyles.Integer,
                    CultureInfo.InvariantCulture,
                    out counts[countIndex]))
            {
                key = default;
                return false;
            }

            countIndex++;

            if (separatorOffset < 0)
            {
                break;
            }

            tokenStart += tokenLength + 1;
        }

        if (countIndex != counts.Length)
        {
            key = default;
            return false;
        }

        key = new CompositionKey(
            counts[0],
            counts[1],
            counts[2],
            counts[3],
            counts[4],
            counts[5],
            counts[6],
            counts[7],
            counts[8],
            counts[9],
            counts[10],
            counts[11],
            counts[12]
        );

        return true;
    }
}
