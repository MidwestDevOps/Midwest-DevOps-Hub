using Newtonsoft.Json;
using System.IO;
using System.Reflection;

namespace MidwestDevOps_Hub
{
    public static class Utility
    {
        public static string GetConnectionString()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"connection.json");

            return JsonConvert.DeserializeObject<HubModels.Connection>(File.ReadAllText(path)).DatabaseConnection;
        }
    }
}
