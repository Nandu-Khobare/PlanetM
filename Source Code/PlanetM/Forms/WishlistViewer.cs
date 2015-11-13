using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PlanetM_Ops;
using PlanetM_Utility;
namespace PlanetM.Forms
{
    public partial class WishlistViewer : Form
    {
        DBOperations dbObj = new DBOperations();
        public WishlistViewer()
        {
            InitializeComponent();
            LoadMovies();
        }

        private void LoadMovies()
        {
            DataTable movies = dbObj.GetMoviesForWishlistFromIMDB().Tables[0];
            this.Text = "My Wishlist :: Movies Count : " + movies.Rows.Count.ToString();
            dgMovies.Rows.Clear();
            int iterator = 0;
            foreach (DataRow drMovie in movies.AsEnumerable())
            {
                dgMovies.Rows.Add();
                dgMovies.Rows[iterator].Cells["Language"].Value = drMovie["Language"];
                dgMovies.Rows[iterator].Cells["Title"].Value = drMovie["Title"];
                dgMovies.Rows[iterator].Cells["Year"].Value = drMovie["Year"];
                dgMovies.Rows[iterator].Cells["Rating"].Value = drMovie["Rating"];
                dgMovies.Rows[iterator].Cells["Top250"].Value = drMovie["Top250"];
                dgMovies.Rows[iterator].Cells["Oscars"].Value = drMovie["Oscars"];
                dgMovies.Rows[iterator].Cells["Genre"].Value = drMovie["Genre"];
                dgMovies.Rows[iterator].Cells["ImdbID"].Value = drMovie["IMDBID"];
                dgMovies.Rows[iterator].DefaultCellStyle.BackColor = Color.Azure;
                iterator++;
            }
        }

        private void dgMovies_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < dgMovies.RowCount && e.RowIndex > -1)
            {
                MovieIMDBDetails fmMovieImdb = new MovieIMDBDetails((string)dgMovies.Rows[e.RowIndex].Cells["ImdbID"].Value);
                fmMovieImdb.Show();
            }
        }

        private void openMovieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgMovies.SelectedRows.Count == 1)
            {
                MovieIMDBDetails fmMovieImdb = new MovieIMDBDetails((string)dgMovies.SelectedRows[0].Cells["ImdbID"].Value);
                fmMovieImdb.Show();
            }
            else
                MessageBox.Show("Please select one Movie from the available Movies !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void deleteMoviesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSelectedMovie();
        }

        private void DeleteSelectedMovie()
        {
            if (dgMovies.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Do you want to delete Movie : " + dgMovies.SelectedRows[0].Cells["Title"].Value + " from MiniIMDB ?", "Delete Movie Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (dbObj.DeleteMovieFromIMDB((string)dgMovies.SelectedRows[0].Cells["ImdbID"].Value))
                    {
                        MessageBox.Show("Movie details has been deleted from Database", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadMovies();
                    }
                    else
                        MessageBox.Show("Error while deleting movie details from Database !", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageHandler.ShowWarning("Please select one Movie from the available Movies !", "Warning");
        }

        private void markAsNonWishlistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgMovies.SelectedRows.Count > 0)
            {
                string strErrorMsg = string.Empty;
                foreach (DataGridViewRow dgMov in dgMovies.SelectedRows)
                {
                    if (!dbObj.UpdateMovieImdbAsWishlist((string)dgMov.Cells["ImdbID"].Value, false, ref strErrorMsg) && !string.IsNullOrEmpty(strErrorMsg))
                        MessageHandler.ShowError(strErrorMsg, "Failure!");
                }
                LoadMovies();
            }
            else
                MessageHandler.ShowWarning("Please select few Movies from the available Movies !", "Warning");
        }

        private void WishlistViewer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
