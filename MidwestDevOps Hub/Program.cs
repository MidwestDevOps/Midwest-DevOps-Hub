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
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Initialize();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new ThreadExceptionEventHandler(MyCommonExceptionHandlingMethod);
            Application.Run(new Login());
        }

        private static void MyCommonExceptionHandlingMethod(object sender, ThreadExceptionEventArgs t)
        {
            MessageBox.Show(t.Exception.ToString(), "An Error Has Occured");

            BusinessLogicLayer.Logging.SaveLog(new BusinessLogicLayer.Log() { time = DateTime.Now, exception = t.Exception });
        }

        static void Initialize()
        {

            //Logging
            String path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (Directory.Exists(Path.Combine(path, "MidwestDevOps")))
            {

            }
            else
            {
                Directory.CreateDirectory(Path.Combine(path, "MidwestDevOps"));
            }

            if (Directory.Exists(Path.Combine(path, "MidwestDevOps", "Logging")))
            {

            }
            else
            {
                Directory.CreateDirectory(Path.Combine(path, "MidwestDevOps", "Logging"));
            }

            if (File.Exists(Path.Combine(path, "MidwestDevOps", "Logging", "log.txt")))
            {

            }
            else
            {
                File.Create(Path.Combine(path, "MidwestDevOps", "Logging", "log.txt"));
            }

            BusinessLogicLayer.Logging.LogPath = Path.Combine(path, "MidwestDevOps", "Logging", "log.txt");
        }
    }
}
