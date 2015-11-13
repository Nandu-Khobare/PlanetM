using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PlanetM_Utility;
using AbstractLayer;

namespace PlanetM_LicenseRequestor
{
    public partial class RequestLicenseForm : Form
    {
        PCInfoRetriever pcInfoRetriever = new PCInfoRetriever();
        public RequestLicenseForm()
        {
            InitializeComponent();
        }

        private void btnGenerateInfo_Click(object sender, EventArgs e)
        {
            PCInfo pcInfo;
            string error;
            if (pcInfoRetriever.GetPCInfo(out pcInfo,out error))
            {
                StringBuilder sbPCInfo=new StringBuilder();
                sbPCInfo.AppendLine(pcInfo.ProcessorID);
                sbPCInfo.AppendLine(pcInfo.HardDiskID);
                sbPCInfo.AppendLine(pcInfo.MotherboardID);
                sbPCInfo.AppendLine(pcInfo.MACID);
                rtMachineInfo.Text = sbPCInfo.ToString();
            }
            else
            {
                MessageHandler.ShowError(error);
            }
        }

        private void btnCopyToClipboard_Click(object sender, EventArgs e)
        {

        }

        private void btnSendViaGMail_Click(object sender, EventArgs e)
        {

        }
    }
}
