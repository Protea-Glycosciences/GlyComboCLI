using GlyComboCLI;
using static GlyComboCLI.MonosaccharideDefinition;

namespace GlyCombo;

internal static class CompositionSearch
{
    public delegate void ResultHandler(CompositionResult result, decimal targetIndex);

    public static void SumUp(
        List<MonosaccharideDefinition> catalog,
        List<decimal> targets,
        decimal massError,
        string massErrorType,
        ResultHandler onResultFound,
        ref long iterations)
    {
        for (int i = 0; i < targets.Count; i++)
        {
            decimal target = targets[i];
            decimal targetLow, targetHigh;
            if (massErrorType == "da")
            {
                targetLow = target - massError;
                targetHigh = target + massError;
            }
            else
            {
                targetLow = target - target * (massError / 1_000_000m);
                targetHigh = target + target * (massError / 1_000_000m);
            }

            SumUpRecursive(
                catalog, target, targetLow, targetHigh,
                partial: new List<MonosaccharideDefinition>(),
                startIndex: 0,
                i, onResultFound, ref iterations);
        }
    }

    private static void SumUpRecursive(
        List<MonosaccharideDefinition> remaining,
        decimal target,
        decimal targetLow,
        decimal targetHigh,
        List<MonosaccharideDefinition> partial,
        int startIndex,
        decimal targetIndex,
        ResultHandler onResultFound,
        ref long iterations)
    {
        iterations++;
        decimal sum = partial.Sum(d => d.Mass);

        if (sum >= targetLow && sum <= targetHigh)
        {

            Dictionary<MonosaccharideDefinition, int> counts = partial
                .GroupBy(d => d)
                .ToDictionary(g => g.Key, g => g.Count());

            ElementalFormula formula = new();
            foreach ((MonosaccharideDefinition def, int count) in counts)
            {
                for (int n = 0; n < count; n++)
                {
                    formula += def;
                }
            }

            onResultFound(
                new CompositionResult(
                    counts,
                    TheoreticalMass: target, // caller adjusts for reducing end/adduct
                    ObservedMass: sum,       // caller adjusts for reducing end/adduct
                    MassError: 0,            // caller computes after adjustment
                    formula,
                    GlyTouCanAccession: "",  // caller fills in after bounds-checking
                    Ms2Scan: null,           // caller attaches if this is mzML input
                    Derivatisation: ""),     // unused here - caller builds its own result with the real value
                targetIndex);
        }

        if (sum >= targetHigh)
        {
            return;
        }

        for (int k = startIndex; k < remaining.Count; k++)
        {
            partial.Add(remaining[k]);
            SumUpRecursive(remaining, target, targetLow, targetHigh, partial, k, targetIndex, onResultFound, ref iterations);
            partial.RemoveAt(partial.Count - 1);
        }
    }

    public static bool AllCountsInBounds(
        IReadOnlyDictionary<MonosaccharideDefinition, int> counts,
        IEnumerable<MonosaccharideDefinition> catalog)
    {
        foreach (MonosaccharideDefinition def in catalog)
        {
            int count = counts.TryGetValue(def, out int c) ? c : 0;
            if (!def.CountIsInBounds(count))
            {
                return false;
            }
        }
        return true;
    }
}