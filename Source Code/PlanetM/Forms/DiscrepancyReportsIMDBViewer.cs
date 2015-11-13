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
    public partial class DiscrepancyReportsIMDBViewer : Form
    {
        DBOperations dbObj = new DBOperations();
        List<DiscrepancyReport> reports = new List<DiscrepancyReport>();
        DiscrepancyReport selectedReport;
        public DiscrepancyReportsIMDBViewer()
        {
            InitializeComponent();
            reports.Add(new DiscrepancyReport(1, "Movies rated in PlanetM and can be rated on IMDB Site.", "uspReportForIMoviesRatedInPlanetM"));
            reports.Add(new DiscrepancyReport(2, "Movies which are seen but not rated on IMDB Site.", "uspReportForIMoviesWatchedNotRated"));
            reports.Add(new DiscrepancyReport(3, "Movies which not rated on IMDB Site.", "uspReportForIMoviesNotRated"));
            reports.Add(new DiscrepancyReport(4, "Movies which rated much different than IMDB.", "uspReportForIMoviesWithMyRatingsImdbRatingsMismatch"));
            reports.Add(new DiscrepancyReport(5, "Movies without IMDB Rating.", "uspReportForIMoviesWithoutImdbRating"));
            reports.Add(new DiscrepancyReport(6, "Movies without a director.", "uspReportForIMoviesWithoutDirector"));
            reports.Add(new DiscrepancyReport(7, "Movies with repeated Top250.", "uspReportForIMoviesWithRepeatedTop250"));
            reports.Add(new DiscrepancyReport(8, "Movies with outdated data and should be refreshed.", "uspReportForIMoviesWithOutdatedData"));
            reports.Add(new DiscrepancyReport(9, "Movies without a Writer.", "uspReportForIMoviesWithoutWriter"));
            reports.Add(new DiscrepancyReport(10, "Movies without any poster.", "uspReportForIMoviesWithoutPoster"));
            reports.Add(new DiscrepancyReport(11, "Movies without any genre.", "uspReportForIMoviesWithoutGenre"));
            reports.Add(new DiscrepancyReport(12, "Movies with a year and release date mismatch.", "uspReportForIMoviesWithReleaseDateYearMismatch"));
            reports.Add(new DiscrepancyReport(13, "Movies without sufficient cast.", "uspReportForIMoviesWithoutSufficientCast"));
            reports.Add(new DiscrepancyReport(14, "Movies without sufficient stars.", "uspReportForIMoviesWithoutSufficientStars"));
            reports.Add(new DiscrepancyReport(15, "Movies without a year.", "uspReportForIMoviesWithoutYear"));
            
            comboBoxDiscReportType.Items.Add("--- SELECT REPORT ---");
            foreach (var report in reports)
            {
                comboBoxDiscReportType.Items.Add(report.ReportName);
            }
            comboBoxDiscReportType.SelectedIndex = 0;
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
                MessageBox.Show("Please select one Movie from the available Movies !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void comboBoxDiscReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxDiscReportType.SelectedIndex != 0)
            {
                selectedReport = reports.Find(rpt => rpt.ReportName == comboBoxDiscReportType.SelectedItem.ToString());
                LoadMovies();
            }
        }
        private void LoadMovies()
        {
            if (selectedReport != null)
            {
                DataTable movies = dbObj.GetDiscrepancyReport(selectedReport).Tables[0];
                this.labelDiscrepancyCount.Text = "Discrepancies Found : " + movies.Rows.Count.ToString();
                dgMovies.Rows.Clear();
                int iterator = 0;
                foreach (DataRow drMovie in movies.AsEnumerable())
                {
                    dgMovies.Rows.Add();
                    dgMovies.Rows[iterator].Cells["Language"].Value = drMovie["Language"];
                    dgMovies.Rows[iterator].Cells["Title"].Value = drMovie["Title"];
                    dgMovies.Rows[iterator].Cells["Year"].Value = drMovie["Year"];
                    dgMovies.Rows[iterator].Cells["Rating"].Value = drMovie["Rating"];
                    dgMovies.Rows[iterator].Cells["Oscars"].Value = drMovie["Oscars"];
                    dgMovies.Rows[iterator].Cells["Genre"].Value = drMovie["Genre"];
                    dgMovies.Rows[iterator].Cells["ImdbID"].Value = drMovie["IMDBID"];
                    dgMovies.Rows[iterator].DefaultCellStyle.BackColor = Color.PaleTurquoise;
                    iterator++;
                }
            }
        }

        private void DiscrepancyReportsIMDBViewer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
