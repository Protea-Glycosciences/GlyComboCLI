using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlyComboCLI
{
    internal sealed record MonosaccharideDefinition(
    string Kind,
    string DisplayLabel,
    decimal Mass,
    int CarbonCount,
    int HydrogenCount,
    int NitrogenCount,
    int OxygenCount,
    int PhosphorusCount,
    int SulfurCount,
    int Min,
    int Max)
    {
        public bool CountIsInBounds(int count) => count >= Min && count <= Max;

        internal struct ElementalFormula
        {
            public int Carbon;
            public int Hydrogen;
            public int Nitrogen;
            public int Oxygen;
            public int Phosphorus;
            public int Sulfur;

            public static ElementalFormula operator +(ElementalFormula a, MonosaccharideDefinition d) => new()
            {
                Carbon = a.Carbon + d.CarbonCount,
                Hydrogen = a.Hydrogen + d.HydrogenCount,
                Nitrogen = a.Nitrogen + d.NitrogenCount,
                Oxygen = a.Oxygen + d.OxygenCount,
                Phosphorus = a.Phosphorus + d.PhosphorusCount,
                Sulfur = a.Sulfur + d.SulfurCount,
            };

            /// Skyline compatible formula string, e.g. "C24H38N2O16" (zero-count elements omitted).
            public override string ToString()
            {
                var sb = new System.Text.StringBuilder();
                sb.Append('C').Append(Carbon).Append('H').Append(Hydrogen);
                if (Nitrogen != 0) sb.Append('N').Append(Nitrogen);
                sb.Append('O').Append(Oxygen);
                if (Phosphorus != 0) sb.Append('P').Append(Phosphorus);
                if (Sulfur != 0) sb.Append('S').Append(Sulfur);
                return sb.ToString();
            }
        }

    }

}
