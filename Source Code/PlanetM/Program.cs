using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PlanetM_Utility;

namespace PlanetM
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            LogWrapper.LogInfo("* * * * * * * * * INITIALIZATION * * * * * * * * *");
            LogWrapper.LogApplicationDetails();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DashboardPlanetM());
            LogWrapper.LogInfo("* * * * * * * * * EXIT * * * * * * * * *");
            //Application.Run(new SearchIMDB());
            //Application.Run(new DashboardMiniIMDB());
        }
    }
}
