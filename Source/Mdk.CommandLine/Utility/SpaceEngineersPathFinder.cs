using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Mdk.CommandLine.Utility
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
}