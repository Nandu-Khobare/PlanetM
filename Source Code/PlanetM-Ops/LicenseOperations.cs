using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlanetM_Utility;
using System.IO;
using System.Windows.Forms;

namespace PlanetM_Ops
{
    public class LicenseOperations
    {
        public bool ValidateLicense()
        {
            return true;
            bool isValid = false;

            string licenseFilePath = string.Format("{0}\\PlanetM.knj", Application.StartupPath);
            if (!File.Exists(licenseFilePath))
            {
                if (MessageHandler.AskQuestion("License not installed! Do you want to install now?") == DialogResult.Yes)
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog
                    {
                        InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer),
                        Filter = "PlanetM License files (*.knj)|*.knj|All files (*.*)|*.*",
                        FilterIndex = 1,
                        RestoreDirectory = true,
                        Title = "Select license file"
                    };

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        ValidateLicenseFile(openFileDialog.FileName);
                    }
                }
            }
            else
            {
                ValidateLicenseFile(licenseFilePath);
            }
            //Thread.Sleep(2000);
            //CryptoAlgoritms c = (CryptoAlgoritms)Enum.Parse(typeof(CryptoAlgoritms), AlgorithmsComboBoxE.Text, true);
            return isValid;
        }

        private void ValidateLicenseFile(string licenseFile)
        {
        }
    }
}
