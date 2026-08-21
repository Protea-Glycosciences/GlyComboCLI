using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace GlyComboCLI.Tests;

public sealed record CliResult(int ExitCode, string StdOut, string StdErr, string OutputDir, string InputBaseName)
{
    public string ReadResultCsv()
    {
        string path = Path.Combine(OutputDir, $"{InputBaseName}_result.csv");
        Assert.True(File.Exists(path), $"Expected result CSV not found at {path}.\nSTDOUT:\n{StdOut}\nSTDERR:\n{StdErr}");
        return File.ReadAllText(path);
    }

    /// <summary>mzML-input runs write "{basename}_SkylineImport.csv".</summary>
    public string ReadSkylineImportCsv()
    {
        string path = Path.Combine(OutputDir, $"{InputBaseName}_SkylineImport.csv");
        Assert.True(File.Exists(path), $"Expected SkylineImport CSV not found at {path}.\nSTDOUT:\n{StdOut}\nSTDERR:\n{StdErr}");
        return File.ReadAllText(path);
    }
}

/// Point GLYCOMBO_CLI_PATH at a published build, e.g.:
///   dotnet publish -c Release -o ./publish
///   export GLYCOMBO_CLI_PATH=$(pwd)/publish/GlyComboCLI

public static class GlyComboCliRunner
{
    private static readonly string CliExecutablePath =
        Environment.GetEnvironmentVariable("GLYCOMBO_CLI_PATH")
        ?? throw new InvalidOperationException(
            "Set the GLYCOMBO_CLI_PATH environment variable to a built GlyComboCLI executable " +
            "before running these tests (e.g. the output of `dotnet publish -c Release`).");

    private static readonly string TestDataDir =
        Path.Combine(AppContext.BaseDirectory, "TestData");

    public static CliResult Run(string inputFileName, IReadOnlyDictionary<string, string> options)
    {
        string inputPath = Path.Combine(TestDataDir, inputFileName);
        Assert.True(File.Exists(inputPath),
            $"Missing test fixture '{inputFileName}' - copy it from your Galaxy test-data/ " +
            $"directory into {TestDataDir}");

        string outputDir = Directory.CreateDirectory(
            Path.Combine(Path.GetTempPath(), "GlyComboCLI.Tests", Guid.NewGuid().ToString("N"))).FullName;

        var args = new List<string>
        {
            $"--file={inputPath}",
            $"--outputPath={outputDir}{Path.DirectorySeparatorChar}"
        };
        foreach (var (key, value) in options)
            args.Add($"--{key}={value}");

        var psi = new ProcessStartInfo(CliExecutablePath)
        {
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
        };
        foreach (var a in args)
            psi.ArgumentList.Add(a);

        using var process = Process.Start(psi)
            ?? throw new InvalidOperationException($"Failed to start {CliExecutablePath}");

        string stdOut = process.StandardOutput.ReadToEnd();
        string stdErr = process.StandardError.ReadToEnd();
        bool exited = process.WaitForExit(TimeSpan.FromMinutes(5));
        if (!exited)
        {
            process.Kill(entireProcessTree: true);
            throw new TimeoutException($"GlyComboCLI did not exit within 5 minutes.\nSTDOUT:\n{stdOut}");
        }

        string baseName = Path.GetFileNameWithoutExtension(inputFileName);
        return new CliResult(process.ExitCode, stdOut, stdErr, outputDir, baseName);
    }
}