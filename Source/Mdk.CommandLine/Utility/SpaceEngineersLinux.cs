using System;
using System.Diagnostics;
using System.Runtime.Versioning;

namespace Mdk.CommandLine.Utility;

[SupportedOSPlatform("linux")]
internal class SpaceEngineersLinux : ISpaceEngineers
{
    public string? GetInstallPath(params string[]? subfolders) => GetOnLinux("SpaceEngineers/Bin64");

    public string GetDataPath(params string[]? subfolders) => GetOnLinux("SpaceEngineers/IngameScripts/local");

    private static string GetOnLinux(string findPath)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "find",
            Arguments = $"/home -path \"*/{findPath}\"",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        using (var process = Process.Start(startInfo))
        {
            if (process == null) throw new Exception("Failed to find IngameScripts folder.");
            process.WaitForExit();
            return process.StandardOutput.ReadToEnd().Trim();
        }
    }
}