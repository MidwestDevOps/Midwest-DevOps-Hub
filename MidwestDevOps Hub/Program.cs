using BusinessLogicLayer;
using HubModels;
using MidwestDevOps_Hub.Forms.FirstTimeSetup;
using MidwestDevOps_Hub.Forms.Login;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MidwestDevOps_Hub
{
    static class Program
    {
        static bool isFirstTime = false;

        [STAThread]
        static void Main(string[] args)
        {
            Initialize();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new ThreadExceptionEventHandler(MyCommonExceptionHandlingMethod);

            if (isFirstTime || args.Where(x => x == "isFirstTime").Count() > 0)
                Application.Run(new FirstTimeSetUp());
            else
                Application.Run(new Login());
        }

        private static void MyCommonExceptionHandlingMethod(object sender, ThreadExceptionEventArgs t)
        {
            MessageBox.Show(t.Exception.ToString(), "An Error Has Occured");

            BusinessLogicLayer.Logging.SaveLog(new BusinessLogicLayer.Log() { time = DateTime.Now, exception = t.Exception });
        }

        internal static void CreateDirectory(string path)
        {
            if (Directory.Exists(path)) { 

            }
            else
            {
                Directory.CreateDirectory(path);
            }
        }

        internal static void CreateFile(string path)
        {
            if (File.Exists(path)) { 

            }
            else
            {
                File.Create(path);
            }
        }

        static void Initialize()
        {

            String path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            CreateDirectory(Path.Combine(path, "MidwestDevOps"));

            //Connections
            CreateDirectory(Path.Combine(path, "MidwestDevOps", "Connections"));
            if (!File.Exists(Path.Combine(path, "MidwestDevOps", "Connections", "Connections.json")) || String.IsNullOrEmpty(File.ReadAllText(Path.Combine(path, "MidwestDevOps", "Connections", "Connections.json"))))
                isFirstTime = true;
            CreateFile(Path.Combine(path, "MidwestDevOps", "Connections", "Connections.json"));

            //Logging
            CreateDirectory(Path.Combine(path, "MidwestDevOps", "Logging"));
            CreateFile(Path.Combine(path, "MidwestDevOps", "Logging", "log.txt"));

            BusinessLogicLayer.Logging.LogPath = Path.Combine(path, "MidwestDevOps", "Logging", "log.txt");
        }
    }
}
