using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Mdk2.References.Utility;

namespace Mdk2.References.Utility
{
    public static class SpaceEngineersPathFinder
    {
        public static string DataPath()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var se = new SpaceEngineers();
                return se.GetDataPath();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return GetOnLinux("SpaceEngineers/IngameScripts/local");
            }
            throw new Exception($"Unable to get IngameScripts path on platform: ({Environment.OSVersion.Platform.ToString()})");
        }

        public static string BinaryPath()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var se = new SpaceEngineers();
                if (!se.TryGetInstallPath("Bin64", out var installPath))
                    throw new Exception($"Unable to find Space Engineers Binary path.");
                return installPath;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return GetOnLinux("SpaceEngineers/Bin64");
            }
            throw new Exception($"Unable to get IngameScripts path on platform: ({Environment.OSVersion.Platform.ToString()})");
        }

        private static string GetOnLinux(string findPath)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "find",
                Arguments = $"/home/{{$USER}} -path \"*/{findPath}\"",
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
}