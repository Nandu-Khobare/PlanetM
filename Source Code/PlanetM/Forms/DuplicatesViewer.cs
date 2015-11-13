using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PlanetM_Ops;
namespace PlanetM.Forms
{
    public partial class DuplicatesViewer : Form
    {
        DBOperations dbObj = new DBOperations();
        public DuplicatesViewer()
        {
            InitializeComponent();

            DataTable movies = dbObj.GetAllMovieDuplicates().Tables[0];
            dgMovies.Rows.Clear();
            int iterator = 0;
            foreach (DataRow drMovie in movies.AsEnumerable())
            {
                dgMovies.Rows.Add();
                dgMovies.Rows[iterator].Cells["Language"].Value = drMovie["Language"];
                dgMovies.Rows[iterator].Cells["MovieName"].Value = drMovie["Name"];
                dgMovies.Rows[iterator].Cells["Size"].Value = drMovie["Size"];
                dgMovies.Rows[iterator].Cells["Checksum"].Value = drMovie["Checksum"];
                dgMovies.Rows[iterator].Cells["Location"].Value = drMovie["Location"];
                dgMovies.Rows[iterator].DefaultCellStyle.BackColor = Color.Bisque;
                iterator++;
            }
            //for (int i = dgMovies.Rows.Count; i > -1;i-- )
            //{

            //}
        }

        private void DuplicatesViewer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
