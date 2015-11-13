using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PlanetM_Ops;
using AbstractLayer;
using PlanetM_Utility;
namespace PlanetM.Forms
{
    public partial class WatchlistViewer : Form
    {
        DBOperations dbObj = new DBOperations();
        public WatchlistViewer()
        {
            InitializeComponent();
            LoadMovies();
        }

        private void dgMovies_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < dgMovies.RowCount && e.RowIndex > -1)
            {
                OpenSelectedMovie((string)dgMovies.Rows[e.RowIndex].Cells["MovieName"].Value, (string)dgMovies.Rows[e.RowIndex].Cells["Language"].Value);
            }
        }

        private void openMovieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgMovies.SelectedRows.Count == 1)
            {
                OpenSelectedMovie((string)dgMovies.SelectedRows[0].Cells["MovieName"].Value, (string)dgMovies.SelectedRows[0].Cells["Language"].Value);
            }
            else
                MessageBox.Show("Please select one Movie from the available Movies !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void deleteMoviesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSelectedMovie();
        }

        private void LoadMovies()
        {
            DataTable movies = dbObj.GetWatchlistMovies().Tables[0];
            this.Text = "My Watchlist :: Movies Count : " + movies.Rows.Count.ToString();
            dgMovies.Rows.Clear();
            int iterator = 0;
            foreach (DataRow drMovie in movies.AsEnumerable())
            {
                dgMovies.Rows.Add();
                dgMovies.Rows[iterator].Cells["MovieName"].Value = drMovie["Name"];
                dgMovies.Rows[iterator].Cells["Language"].Value = drMovie["Language"];
                dgMovies.Rows[iterator].Cells["Year"].Value = drMovie["Year"];
                dgMovies.Rows[iterator].Cells["ImdbRating"].Value = drMovie["ImdbRating"];
                dgMovies.Rows[iterator].Cells["Genre"].Value = drMovie["Genre"];
                dgMovies.Rows[iterator].Cells["AddedDate"].Value = drMovie["AddedDate"];
                dgMovies.Rows[iterator].DefaultCellStyle.BackColor = Color.Azure;
                iterator++;
            }
        }

        private void OpenSelectedMovie(string Name, string Language)
        {
            DataSet dtStMovie;
            Movie tmpMov = new Movie();
            tmpMov.Name = Name;
            tmpMov.Language = Language;
            dtStMovie = dbObj.GetMovieDetails(tmpMov.Name, tmpMov.Language);
            if (dtStMovie.Tables[0].Rows.Count != 0)
            {
                if (!dtStMovie.Tables[0].Rows[0].IsNull("Year"))
                    tmpMov.Year = (int)dtStMovie.Tables[0].Rows[0]["Year"];
                tmpMov.IsWatched = (bool)dtStMovie.Tables[0].Rows[0]["Seen"];
                if (!dtStMovie.Tables[0].Rows[0].IsNull("SeenDate"))
                    tmpMov.WhenSeen = (DateTime)dtStMovie.Tables[0].Rows[0]["SeenDate"];
                tmpMov.Size = (float)(double)dtStMovie.Tables[0].Rows[0]["Size"];
                if (!dtStMovie.Tables[0].Rows[0].IsNull("MyRating"))
                    tmpMov.MyRating = (int)dtStMovie.Tables[0].Rows[0]["MyRating"];
                if (!dtStMovie.Tables[0].Rows[0].IsNull("IMDBRating"))
                    tmpMov.IMDBRating = (float)Convert.ToDouble(dtStMovie.Tables[0].Rows[0]["IMDBRating"]);
                if (!dtStMovie.Tables[0].Rows[0].IsNull("IMDBID"))
                    tmpMov.IMDBID = (string)dtStMovie.Tables[0].Rows[0]["IMDBID"];
                if (!dtStMovie.Tables[0].Rows[0].IsNull("Quality"))
                    tmpMov.Quality = (string)dtStMovie.Tables[0].Rows[0]["Quality"];
                foreach (string genre in dtStMovie.Tables[0].Rows[0]["Genre"].ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                {
                    tmpMov.Genre.Add(genre);
                }
                foreach (DataRow dr in dtStMovie.Tables[0].Rows)
                {
                    tmpMov.Locations.Add(dr.Field<string>("Location"));
                    tmpMov.Checksums.Add(dr.Field<string>("Checksum"));
                    if (dr.Field<object>("LocationSize") != null)
                        tmpMov.LocationSize.Add((float)dr.Field<double>("LocationSize"));
                }

                MovieDetails fmmovie = new MovieDetails(tmpMov);
                fmmovie.Show();
            }
            else
                MessageHandler.ShowError("Refresh and try again!", "Error");
        }

        private void DeleteSelectedMovie()
        {
            if (dgMovies.SelectedRows.Count == 1)
            {
                Movie mov = new Movie();
                mov.Name = (string)dgMovies.SelectedRows[0].Cells["MovieName"].Value;
                mov.Language = (string)dgMovies.SelectedRows[0].Cells["Language"].Value;
                if (MessageBox.Show("Do you want to delete Movie : " + mov.Name + " from PlanetM ?", "Delete Movie Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (dbObj.DeleteMovie(mov))
                    {
                        MessageBox.Show("Movie details has been deleted from Database", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadMovies();
                    }
                    else
                        MessageBox.Show("Error while deleting movie details from Database !", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Please select one Movie from the available Movies !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void markAsSeenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgMovies.SelectedRows.Count > 0)
            {
                string strErrorMsg = string.Empty;
                foreach (DataGridViewRow dgMov in dgMovies.SelectedRows)
                {
                    if (!dbObj.UpdateMovieAsSeenUnseen((string)dgMov.Cells["MovieName"].Value,
                                                       (string)dgMov.Cells["Language"].Value, true, ref strErrorMsg) && !string.IsNullOrEmpty(strErrorMsg))
                        MessageHandler.ShowError(strErrorMsg, "Failure!");
                }
                LoadMovies();
            }
            else
                MessageBox.Show("Please select few Movies from the available Movies !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void WatchlistViewer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
