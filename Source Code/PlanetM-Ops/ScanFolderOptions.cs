using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PlanetM_Ops
{
    public partial class ScanFolderOptions : Form
    {
        FolderBrowserDialog browseFolder;
        public ScanFolderOptions()
        {
            InitializeComponent();
            string[] Languages = PlanetM_Utility.Configuration.GetConfigurationValues("Languages");
            foreach (string lang in Languages)
                comboBoxLanguage.Items.Add(lang);
            comboBoxLanguage.Text = "English";
            textBoxFolderPath.Enabled = btnOK.Enabled = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public string SelectedFolderPath
        {
            get { return browseFolder.SelectedPath; }
        }
        public string SelectedMovieLanguage
        {
            get { return comboBoxLanguage.SelectedItem.ToString(); }
        }
        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            browseFolder = new FolderBrowserDialog();
            browseFolder.RootFolder = Environment.SpecialFolder.MyComputer;
            browseFolder.ShowNewFolderButton = false;
            if (browseFolder.ShowDialog() == DialogResult.OK)
            {
                btnOK.Enabled = true;
                textBoxFolderPath.Text = browseFolder.SelectedPath;
            }
        }
    }
}