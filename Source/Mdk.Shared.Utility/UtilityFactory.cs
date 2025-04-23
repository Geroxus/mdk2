using System;
using System.Runtime.InteropServices;

namespace Mdk2.Shared.Utility
{
    public static class UtilityFactory
    {
        public static ISpaceEngineers MakeSpaceEngineers()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return new SpaceEngineersWindows();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return new SpaceEngineersLinux();
            }
            throw new Exception($"Unable to get IngameScripts path on platform: ({Environment.OSVersion.Platform.ToString()})");
        }
    }
}