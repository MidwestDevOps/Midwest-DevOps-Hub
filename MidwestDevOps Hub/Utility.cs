using Newtonsoft.Json;
using System.IO;

namespace MidwestDevOps_Hub
{
    public static class Utility
    {
        public static string GetConnectionString()
        {
            return JsonConvert.DeserializeObject<HubModels.Connection>(File.ReadAllText(@"C:\Users\Mark\source\repos\MidwestDevOps Hub\Midwest-DevOps-Hub\MidwestDevOps Hub\connection.json")).DatabaseConnection;
        }
    }
}
