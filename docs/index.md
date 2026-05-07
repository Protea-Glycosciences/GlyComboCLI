# GlyComboCLI

GlyComboCLI is a command-line tool for the rapid assignment of monosaccharide combinations to observed and fragmented precursors in mass spectrometry experiments.

Given a list of observed precursor masses (from a plain text file or precursors selected for MS2 in an mzML file), GlyComboCLI performs a combinatorial search across user-defined monosaccharide building blocks and reports all compositions that fall within the specified mass error tolerance.

## Key features

- Accepts plain text mass lists (`.txt`) and vendor-neutral mzML files from Bruker, Thermo, Agilent, Waters, and Sciex instruments
- Supports native, permethylated, and peracetylated derivatisations
- Supports a wide range of reducing end labels including Free, Reduced, InstantPC, Rapifluor-MS, 2-AA, 2-AB, Procainamide, and Girard's reagent P, as well as custom reducing ends
- Supports common adducts in both positive and negative ion mode
- Outputs results as a CSV file, with an additional Skyline-compatible import file when mzML input is used
- Supports up to five user-defined custom monosaccharides
- Off-by-one isotope correction for cases of incorrect monoisotopic precursor selection

## Quick example

```
GlyComboCLI.exe -F=".\masses.txt" -D=Native -R=Reduced -T=Da -E=0.6 -A=Neutral -hMax=9 -nMin=2 -nMax=10
```

## Getting help

- [GitHub repository](https://github.com/Protea-Glycosciences/GlyComboCLI)
- Bug reports and questions: chris@proteaglyco.com