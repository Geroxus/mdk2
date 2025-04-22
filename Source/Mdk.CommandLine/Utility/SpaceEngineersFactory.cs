using System;
using System.Runtime.InteropServices;
using Mdk.CommandLine.CommandLine;

namespace Mdk.CommandLine.Utility;

public static class SpaceEngineersFactory
{
    public static ISpaceEngineers Get()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return new SpaceEngineers();
        } else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return new SpaceEngineersLinux();
        }
        throw new CommandLineException(-1, $"Unable to get IngameScripts path on platform: ({Environment.OSVersion.Platform.ToString()})");
    }
}