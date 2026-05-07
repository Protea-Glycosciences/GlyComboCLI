# Usage

## Basic syntax

```
GlyComboCLI.exe [options]
```

All options can be passed in either long form (`--hexMax`) or short form (`-hMax`) where available.

---

## Required options

| Option | Short | Description |
|---|---|---|
| `--file` | `-F` | Path to the input file (`.mzML` or `.txt`) |
| `--derivatisation` | `-D` | `Native`, `Permethylated`, or `Peracetylated` |
| `--reducedEnd` | `-R` | Reducing end label (see [Reducing ends](#reducing-ends)) |
| `--massError` | `-E` | Mass error value, e.g. `0.6` or `20` |
| `--massErrorType` | `-T` | `Da` or `ppm` |

---

## Monosaccharide options

Each monosaccharide has a minimum and maximum count. Set `--*Max` to `0` to exclude a monosaccharide from the search entirely. GlyCombo minimums and maximums do not need to be defined unless they are greater than 0.

| Monosaccharide | Min flag | Max flag |
|---|---|---|
| Hexose (Hex) | `-hMin` | `-hMax` |
| N-acetyl hexosamine (HexNAc) | `-nMin` | `-nMax` |
| Deoxyhexose (dHex) | `-fMin` | `-fMax` |
| Hexuronic acid (HexA) | `-aMin` | `-aMax` |
| Hexosamine (HexN) | `-xMin` | `-xMax` |
| Pentose (Pent) | `-pMin` | `-pMax` |
| KDN | `-kMin` | `-kMax` |
| Neu5Ac (NeuAc) | `-sMin` | `-sMax` |
| Neu5Gc (NeuGc) | `-gMin` | `-gMax` |
| Phosphate (Phos) | `--phosMin` | `--phosMax` |
| Sulfate (Sulf) | `--sulfMin` | `--sulfMax` |
| N-acetyl deoxyhexose (dHexNAc) | `--dHexNAcMin` | `--dHexNAcMax` |
| Acetylation | `--acetylMin` | `--acetylMax` |

### Modified sialic acids (native derivatisation only)

| Monosaccharide | Min flag | Max flag |
|---|---|---|
| Lactonised NeuAc (a2,3) | `--lNeuAcMin` | `--lNeuAcMax` |
| Ethyl esterified NeuAc (a2,6) | `--eeNeuAcMin` | `--eeNeuAcMax` |
| Dimethylamidated NeuAc (a2,6) | `--dNeuAcMin` | `--dNeuAcMax` |
| Ammonia amidated NeuAc (a2,3) | `--amNeuAcMin` | `--amNeuAcMax` |
| Lactonised NeuGc (a2,3) | `--lNeuGcMin` | `--lNeuGcMax` |
| Ethyl esterified NeuGc (a2,6) | `--eeNeuGcMin` | `--eeNeuGcMax` |
| Dimethylamidated NeuGc (a2,6) | `--dNeuGcMin` | `--dNeuGcMax` |
| Ammonia amidated NeuGc (a2,3) | `--amNeuGcMin` | `--amNeuGcMax` |

---

## Reducing ends

Pass the value to `-R` / `--reducedEnd`:

| Value | Label |
|---|---|
| `Free` | Free reducing end |
| `Reduced` | Reduced (alditol) |
| `InstantPC` | InstantPC |
| `Rapifluor` | Rapifluor-MS |
| `2aa` | 2-aminobenzoic acid |
| `2ab` | 2-aminobenzamide |
| `Procainamide` | Procainamide |
| `Girard` | Girard's reagent P |
| `Custom` | User-defined (see [Custom reducing end](#custom-reducing-end)) |

---

## Adducts
Represents the monoisotopic mass plus the addition of a given adduct, and either in negative mode or positive mode (e.g. MFA- is M + Formic Acid in negative mode). 

Pass a single adduct or a comma-separated list to `-A` / `--adducts`:

**Positive mode:** `MH+`, `MNa+`, `MNH4+`, `MK+`

**Negative mode:** `MH-`, `MFA-`, `MAA-`, `MTFA-`

**Other:** `Neutral`

Example using multiple adducts:

```
-A="MH+,MNa+"
```

---

## Input file formats

### Text files (`.txt`)

A plain list of observed masses, one per line. Values are treated as singly charged m/z observations (e.g. from MALDI-MS).

```
1299.46
1461.51
1623.57
```

### mzML files (`.mzML`)

Standard mzML files from any vendor supported by MSConvert (Bruker, Thermo, Agilent, Waters, Sciex). GlyComboCLI extracts MS2 precursor masses, charge states, retention times, and TIC values automatically.

When an mzML file is used, the output includes a Skyline-compatible import CSV in addition to the standard results CSV.

---

## Output files

| File | When produced | Contents |
|---|---|---|
| `<input>_result.csv` | Text input | Composition, observed mass, theoretical mass, molecular formula, mass error |
| `<input>_SkylineImport.csv` | mzML input | As above, plus scan number, charge, retention time, TIC, and file name |
| `<input>_parameters.txt` | Always | Full record of the search parameters and target mass list |

By default output files are written to the same directory as the input file. Use `--outputPath` to specify a different location:

```
--outputPath="C:\Results\"
```

---

## Custom reducing end

Provide the name, monoisotopic mass, and elemental composition of your unique reducing end label:

```
-R=Custom -cRName="MyLabel" -cRM=150.068 -cRC=8 -cRH=10 -cRN=1 -cRO=2
```

| Flag | Description |
|---|---|
| `-cRName` | Name of the custom reducing end |
| `-cRM` | Monoisotopic mass |
| `-cRC` | Carbon count |
| `-cRH` | Hydrogen count |
| `-cRN` | Nitrogen count |
| `-cRO` | Oxygen count |

---

## Custom monosaccharides

Up to five custom monosaccharides can be defined. Replace `1` with `2`–`5` for additional entries:

```
-c1Name="MyMono" -c1M=150.053 -c1C=6 -c1H=10 -c1N=0 -c1O=5 -c1Min=0 -c1Max=3
```

| Flag | Description |
|---|---|
| `-c1Name` | Name |
| `-c1M` | Monoisotopic mass |
| `-c1C` | Carbon count |
| `-c1H` | Hydrogen count |
| `-c1N` | Nitrogen count |
| `-c1O` | Oxygen count |
| `-c1Min` | Minimum count |
| `-c1Max` | Maximum count |

---

## Off-by-one correction

Enables searching with a mass shifted by one isotope (~1.003 Da) to account for cases where the instrument selected an M+1a non-monoisotopic precursor instead of the monoisotopic:

```
-O=true
```

---

## Examples

**Native, reduced, text input:**
```
GlyComboCLI.exe -F=".\masses.txt" -D=Native -R=Reduced -T=Da -E=0.6 -A=Neutral -hMax=9 -nMin=2 -nMax=10
```

**Native, free, mzML input, ppm error:**
```
GlyComboCLI.exe -F=".\data.mzML" -D=Native -R=Free -T=ppm -E=20 -A=MH- -hMin=1 -hMax=5 -nMin=2 -nMax=10 -sMax=4 -gMax=4 -fMax=2
```

**Permethylated with phosphate and sulfate:**
```
GlyComboCLI.exe -F=".\perme.txt" -D=Permethylated -R=Free -T=ppm -E=10 -A=MH+ -hMax=12 -nMin=2 -nMax=4 --phosMax=2 --sulfMax=2
```