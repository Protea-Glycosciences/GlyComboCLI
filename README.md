# GlyComboCLI
![GitHub License](https://img.shields.io/github/license/Protea-Glycosciences/GlyComboCLI)

GlyComboCLI is an open source command line interface for combinatorial glycan composition determination to identify glycans in MS acquisitions of glycan-containing samples in text or mzML formats.
This application enables rapid extraction of precursor *m/z* values from mzML files, a vendor-neutral format that ensures cross-platform compatibility. For the GUI version, please see [GlyCombo](https://github.com/Protea-Glycosciences/GlyCombo).

---

## Features

- Accepts plain text mass lists (`.txt`) and vendor-neutral mzML files from Bruker, Thermo, Agilent, Waters, and Sciex instruments
- Supports native, permethylated, and peracetylated derivatisations
- Supports a wide range of reducing end labels including Free, Reduced, InstantPC, Rapifluor-MS, 2-AA, 2-AB, Procainamide, and Girard's reagent P, as well as custom reducing ends
- Supports common adducts in both positive and negative ion mode
- Outputs results as a CSV file, with an additional Skyline-compatible import file when mzML input is used
- Supports up to five user-defined custom monosaccharides
- Off-by-one isotope correction for cases of incorrect monoisotopic precursor selection

---

## Quick start

```
GlyComboCLI.exe -F=".\masses.txt" -D=Native -R=Reduced -T=Da -E=0.6 -A=Neutral -hMax=9 -nMin=2 -nMax=10
```

---

## Documentation

Full documentation is available at [glycombocli.readthedocs.io](https://glycombocli.readthedocs.io).

---

## Installation

Pre-built binaries for Windows and Linux are available on the [releases page](https://github.com/Protea-Glycosciences/GlyComboCLI/releases).

Full installation instructions are available in the [documentation](https://glycombocli.readthedocs.io/en/latest/installation).

---


## Citation

If you use GlyComboCLI in your research, please cite:

> Citation placeholder — DOI: [to be added]

---

## Contact

Questions, comments and bug reports:
- GitHub: [https://github.com/Protea-Glycosciences/GlyComboCLI/issues](https://github.com/Protea-Glycosciences/GlyComboCLI/issues)
- Email: chris@proteaglyco.com

---

## Licence

See [LICENSE](LICENSE) for details.

---

# Functions

<details>
<summary>Spectral analysis, Sequence composition calculation</summary>

```yaml
# biotools-function
operation:
- term: Spectral analysis
  uri: http://edamontology.org/operation_3214
- term: Sequence composition calculation
  uri: http://edamontology.org/operation_0236
input:
- data:
    term: Mass spectrum
    uri: http://edamontology.org/data_0943
  format:
  - term: mzML
    uri: http://edamontology.org/format_3244
output:
- data:
    term: Carbohydrate accession
    uri: http://edamontology.org/data_2900
  format:
  - term: CSV
    uri: http://edamontology.org/format_3752
note: Requires MS2 scans with precursor m/z annotated if using the mzml approach
cmd: GlyComboCLI.exe -F=".\masses.txt" -D=Native -R=Reduced -T=Da -E=0.6 -A=Neutral
  -hMax=9 -nMin=2 -nMax=10

```

</details>
