using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;

namespace MidwestDevOps_Hub
{
    public static class Utility
    {
        public static string GetConnectionString()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"connection.json");

            return BusinessLogicLayer.TextHasher.Decrypt(JsonConvert.DeserializeObject<HubModels.Connection>(File.ReadAllText(path)).DatabaseConnection);
        }

        public static string GetIPAddress()
        {
            string ipAddress = new WebClient().DownloadString("http://icanhazip.com");

            return ipAddress;
        }

        public static void RestartApplication(System.Windows.Forms.Form mainHub)
        {
            System.Diagnostics.Process.Start(System.Windows.Forms.Application.ExecutablePath); // to start new instance of application
            mainHub.Close(); //to turn off current app
        }

        [DllImport("User32.dll")]
        private static extern bool
        GetLastInputInfo(ref LASTINPUTINFO plii);

        internal struct LASTINPUTINFO
        {
            public uint cbSize;

            public uint dwTime;
        }

        public static uint GetInactiveMilliseconds()
        {
            LASTINPUTINFO lastInPut = new LASTINPUTINFO();
            lastInPut.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(lastInPut);
            GetLastInputInfo(ref lastInPut);

            return ((uint)Environment.TickCount - lastInPut.dwTime);
        }
    }
}
