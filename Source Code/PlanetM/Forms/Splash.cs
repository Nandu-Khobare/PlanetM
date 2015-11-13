using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PlanetM.Forms
{
    public partial class Splash : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        public Splash()
        {
            InitializeComponent();
            pictureBoxLoading.Image = Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"Images\Loading.gif"));
            SetForegroundWindow(this.Handle);
        }
    }
}
