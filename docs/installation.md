# Installation

GlyComboCLI runs on **Windows** and **Linux**. Pre-built binaries are available on the [GitHub releases page](https://github.com/Protea-Glycosciences/GlyComboCLI/releases).

---

## Windows

1. Download `GlyComboCLI-win-x64.zip` from the latest release.
2. Extract the zip to a folder of your choice, e.g. `C:\Tools\GlyComboCLI`.
3. Open a terminal (Command Prompt or PowerShell) in that folder and run:

```
GlyComboCLI.exe --help
```

To run GlyComboCLI from any directory, add the folder to your `PATH` environment variable:

- Open **Settings** → **System** → **About** → **Advanced system settings** → **Environment Variables**
- Under **System variables**, select `Path` and click **Edit**
- Add the folder path and click **OK**

---

## Linux

1. Download `GlyComboCLI-linux-x64.zip` from the latest release.
2. Extract, make executable, and run:

```bash
unzip GlyComboCLI-linux-x64.zip
chmod +x GlyComboCLI-linux-x64
./GlyComboCLI-linux-x64 --help
```

To install system-wide:

```bash
sudo mv GlyComboCLI-linux-x64 /usr/local/bin/GlyComboCLI
```

---

## Verifying the installation

Run the following to confirm GlyComboCLI is working:

```
GlyComboCLI --help
```

You should see the version number and a list of available options.