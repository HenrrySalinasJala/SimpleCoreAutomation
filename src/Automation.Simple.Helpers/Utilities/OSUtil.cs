namespace Automation.Simple.Helpers.Utilities
{
    using Microsoft.Win32;
    using System;
    using System.Linq;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;

    /// <summary>
    /// Operating system info utility class.
    /// </summary>
    public static class OSUtil
    {
        /// <summary>
        /// Get OS name.
        /// </summary>
        /// <returns>Represents the operating system name.</returns>
        public static string GetOSName()
        {
            RegistryKey key = Registry.LocalMachine;
            const string windowsCurrentVersionSubkey = @"SOFTWARE\Wow6432Node\Microsoft\Windows NT\CurrentVersion";
            RegistryKey subkey = key.OpenSubKey(windowsCurrentVersionSubkey);
            const string productNameSubKey = "ProductName";
            string os = subkey.GetValue(productNameSubKey).ToString();
            if (!String.IsNullOrEmpty(os))
            {
                return os;
            }

            return "Unknown";
        }

        /// <summary>
        /// Gets the local IP.
        /// </summary>
        /// <returns>The IP address.</returns>
        public static string GetLocalIpAddress()
        {
            foreach (var netI in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (netI.NetworkInterfaceType != NetworkInterfaceType.Wireless80211 &&
                    (netI.NetworkInterfaceType != NetworkInterfaceType.Ethernet ||
                     netI.OperationalStatus != OperationalStatus.Up)) continue;
                foreach (var uniIpAddrInfo in netI.GetIPProperties()
                                                  .UnicastAddresses
                                                  .Where(x => netI.GetIPProperties().GatewayAddresses.Count > 0))
                {

                    if (uniIpAddrInfo.Address.AddressFamily == AddressFamily.InterNetwork &&
                        uniIpAddrInfo.AddressPreferredLifetime != uint.MaxValue)
                        return uniIpAddrInfo.Address.ToString();
                }
            }

            return "Unknown";
        }

    }
}
