using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PlanetM_Utility
{
    public static class MessageHandler
    {
        public static void ShowError(Exception ex)
        {
            MessageBox.Show("Error occurred in Application!" + Environment.NewLine + "Source : " + ex.Source + Environment.NewLine + "Error Message : " + ex.Message + Environment.NewLine + "Error StackTrace : " + ex.StackTrace, "PlanetM : Error in " + ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void ShowError(string strErrorMsg)
        {
            MessageBox.Show(strErrorMsg, Application.ProductName + " - Version : " + Application.ProductVersion, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void ShowError(string strErrorMsg, string caption)
        {
            MessageBox.Show(strErrorMsg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void ShowInfo(string info)
        {
            MessageBox.Show(info, Application.ProductName + " - Version : " + Application.ProductVersion, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void ShowInfo(string info, string caption)
        {
            MessageBox.Show(info, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void ShowWarning(string warning)
        {
            MessageBox.Show(warning, Application.ProductName + " - Version : " + Application.ProductVersion, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void ShowWarning(string warning, string caption)
        {
            MessageBox.Show(warning, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static DialogResult AskQuestion(string question)
        {
            return MessageBox.Show(question, Application.ProductName + " - Version : " + Application.ProductVersion, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
    }
}
