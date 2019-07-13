using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace MidwestDevOps_Hub
{
    public static class Utility
    {
        public static string GetConnectionString()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"connection.json");

            return BusinessLogicLayer.TextHasher.Decrypt(JsonConvert.DeserializeObject<HubModels.Connection>(File.ReadAllText(path)).DatabaseConnection);
        }
    }
}
