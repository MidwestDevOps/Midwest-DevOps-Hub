using BusinessLogicLayer;
using HubModels;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MidwestDevOps_Hub
{
    public static class Utility
    {
        public static string HubMainConnectionString = "JLq9DKzWp4tM0kQOwyPTulP36bAIeQrrI5GzwfOSx/H8I0k0UshhqppmJxYRtLrJtRDUXr5Yqg+AutbwMz3UVibF7y7rfqY6+NZSOFD+HkyPbHfDRJeygre5X+VxQkUKT4Dpo0n8bD9JojHKuw9UqyjdJwqWBNfQHuCQ+KPkXKukUsKJOsUzwDkRVih2cjF3GnvyTJnCLO6/XbVJ+k4bbuvoB3z+ioKE2CbyWlPIk/Uhq7WjUK4np7+hLLbbr3Irb7omkHUJqkdOPuEYrqUHi+N+N4PnWhpoFKQm/4VjK/kCvuLl+hwSaGXnMyM4JJSByXPmOaEoY9nfi9AKSgyGg/BxYhfh0h60pquztKLDk2g=";

        public static string connectionString
        {
            get; set;
        }

        public static string connectionPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MidwestDevOps", "Connections", "Connections.json");

        public static string GetConnectionString()
        {
            return connectionString;
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

        public static void CloseApplication(System.Windows.Forms.Form mainHub)
        {
            Application.Exit();
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
