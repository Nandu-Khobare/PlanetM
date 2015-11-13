using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using AbstractLayer;
using PlanetM_Ops;
using PlanetM_Utility;
using System.IO;

namespace PlanetM
{
    public partial class Search : Form
    {
        #region Initialization

        DBOperations dbObj = new DBOperations();
        AbstractLayer.MovieSearchCriteria criteria;
        public Search()
        {
            InitializeComponent();
            LoadControls();
            ConfigureGridview();
        }
        #region [Function ConfigureGridview]
        /// <FunctionName> ConfigureGridview</FunctionName>
        /// <param />
        /// <returns/>
        /// <summary>This function will configure the Grid View</summary>
        /// <exception cref=" "/>
        /// <history>
        ///
        /// </history>
        #endregion
        private void ConfigureGridview()
        {
            try
            {
                dgMovies.Columns.Clear();
                dgMovies.AutoGenerateColumns = false;
                dgMovies.Columns.AddRange(GetGridViewColumns().ToArray());
                //dgMovies.Columns[ISSIMAGE].HeaderText = "";
                //log.Info("Columns Addded Successful for the type ");
            }
            catch (Exception ex)
            {
                //log.Error("Not able to configure the DashBoard Grid: " + ex.ToString());
                //throw new WorklistException(ex.ToString());
            }
        }

        #region [Function GetGridViewColumns]
        /// <FunctionName> GetWorksetGridColumns</FunctionName>
        /// <param >Need to provide workset type [Enumerator]</param>
        /// <returns>DataGridViewColumn type</returns>
        /// <summary>This function will lset the Grid columns </summary>
        /// <exception cref=" "/>
        /// <history>
        ///
        /// </history>
        #endregion
        private List<DataGridViewColumn> GetGridViewColumns()
        {
            List<DataGridViewColumn> gridColumns = new List<DataGridViewColumn>();
            try
            {
                DataGridViewColumn aCol;

                string[] columns = Configuration.GetConfigurationValues("SearchColumns");
                int columnCount = columns.Length;
                foreach (string columnName in columns)
                {
                    if (columnName.ToUpper().Contains("IMAGE"))
                    {
                        DataGridViewImageColumn col = new DataGridViewImageColumn();
                        int width = int.Parse(columnName.Split('~')[1]);
                        col.Name = "Image";
                        col.HeaderText = "";
                        col.ReadOnly = true;
                        col.Width = width;
                        col.ImageLayout = DataGridViewImageCellLayout.Stretch;
                        gridColumns.Add(col);
                        continue;
                    }
                    aCol = GetGridColumn(columnName);
                    gridColumns.Add(aCol);
                }

            }
            catch (Exception ex)
            {
                //throw new WorklistException(ex.ToString());
            }
            return gridColumns;
        }
        private DataGridViewTextBoxColumn GetGridColumn(string columnDetails)
        {
            DataGridViewTextBoxColumn gridColumn = new DataGridViewTextBoxColumn();
            try
            {
                string columnName = columnDetails.Split('~')[0];
                string columnHeader = columnDetails.Split('~')[1];
                int width = int.Parse(columnDetails.Split('~')[2]);
                if (width <= 0)
                    gridColumn.Visible = false;
                gridColumn.HeaderText = columnHeader;
                gridColumn.Name = columnName;
                gridColumn.Width = width;
                gridColumn.ReadOnly = true;
                return gridColumn;
            }
            catch (Exception ex)
            {
                //log.Error("Not able to create column for " + columnDetails);
                //throw new WorklistException(ex.ToString());
            }
            return null;
        }
        private Image GetImageForMovieType(string Language, double Size, int MyRating, double IMDBRating)
        {
            Image movieImage = Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"Images\MovieIconDefault.jpg")); ;
            if (MyRating >= 9 || IMDBRating >= 8)
            {
                movieImage = Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"Images\MovieIconTop250.jpg"));
            }
            else if (Size > 1500)
            {
                movieImage = Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"Images\MovieIconHD.jpg"));
            }
            else if (Language == "Hindi" || Language == "Marathi" || Language == "Telugu" || Language == "Tamil" || Language == "Indian Language")
            {
                movieImage = Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"Images\MovieIconIndian.jpg"));
            }
            else if (Language == "Foreign Language")
            {
                movieImage = Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"Images\MovieIconForeign.jpg"));
            }
            return movieImage;
        }
        private void ApplyStyleForRow(int iterator, string Language, double Size, int MyRating, double IMDBRating, int Year)
        {
            dgMovies.Rows[iterator].DefaultCellStyle.BackColor = Color.LightCyan;
            dgMovies.Rows[iterator].Cells["Image"].Value = GetImageForMovieType(Language, Size, MyRating, IMDBRating);
            switch (Language)
            {
                case "English":
                    dgMovies.Rows[iterator].Cells["Language"].Style.BackColor = Color.Cyan;
                    break;
                case "Hindi":
                    dgMovies.Rows[iterator].Cells["Language"].Style.BackColor = Color.DeepPink;
                    break;
                case "Marathi":
                    dgMovies.Rows[iterator].Cells["Language"].Style.BackColor = Color.Gold;
                    break;
                case "Telugu":
                case "Tamil":
                case "Indian Language":
                    dgMovies.Rows[iterator].Cells["Language"].Style.BackColor = Color.Pink;
                    break;
                default:
                    dgMovies.Rows[iterator].Cells["Language"].Style.BackColor = Color.Silver;
                    break;
            }

            if (Size < 350)
                dgMovies.Rows[iterator].Cells["Size"].Style.BackColor = Color.Tomato;
            else if (Size < 750)
                dgMovies.Rows[iterator].Cells["Size"].Style.BackColor = Color.LightBlue;
            else if (Size < 1400)
                dgMovies.Rows[iterator].Cells["Size"].Style.BackColor = Color.SkyBlue;
            else if (Size < 2500)
                dgMovies.Rows[iterator].Cells["Size"].Style.BackColor = Color.SteelBlue;
            else if (Size > 2500)
                dgMovies.Rows[iterator].Cells["Size"].Style.BackColor = Color.RoyalBlue;

            if (Year == 0)
                dgMovies.Rows[iterator].Cells["Year"].Style.BackColor = Color.WhiteSmoke;
            else if (Year >= DateTime.Now.Year - 1)
                dgMovies.Rows[iterator].Cells["Year"].Style.BackColor = Color.SpringGreen;
            else if (Year <= 1975)
                dgMovies.Rows[iterator].Cells["Year"].Style.BackColor = Color.Gold;
            else if (Year <= 1990)
                dgMovies.Rows[iterator].Cells["Year"].Style.BackColor = Color.Goldenrod;
            else
                dgMovies.Rows[iterator].Cells["Year"].Style.BackColor = Color.PaleTurquoise;

            if (MyRating == 0)
                dgMovies.Rows[iterator].Cells["MyRating"].Style.BackColor = Color.WhiteSmoke;
            else if (MyRating >= 8)
                dgMovies.Rows[iterator].Cells["MyRating"].Style.BackColor = Color.Lime;
            else if (MyRating >= 6)
                dgMovies.Rows[iterator].Cells["MyRating"].Style.BackColor = Color.LimeGreen;
            else
                dgMovies.Rows[iterator].Cells["MyRating"].Style.BackColor = Color.Bisque;
            if (IMDBRating == 0)
                dgMovies.Rows[iterator].Cells["IMDBRating"].Style.BackColor = Color.WhiteSmoke;
            else if (IMDBRating >= 8)
                dgMovies.Rows[iterator].Cells["IMDBRating"].Style.BackColor = Color.Lime;
            else if (IMDBRating >= 6.5)
                dgMovies.Rows[iterator].Cells["IMDBRating"].Style.BackColor = Color.LimeGreen;
            else
                dgMovies.Rows[iterator].Cells["IMDBRating"].Style.BackColor = Color.Bisque;
        }
        private void LoadControls()
        {
            string[] Languages = PlanetM_Utility.Configuration.GetConfigurationValues("Languages");
            string[] Genres = PlanetM_Utility.Configuration.GetConfigurationValues("Genres");

            comboBoxLanguage.Items.Add("ALL");
            foreach (string lang in Languages)
                comboBoxLanguage.Items.Add(lang);
            comboBoxLanguage.SelectedIndex = 0;
            comboBoxYear.Items.Add("ALL");
            for (int i = DateTime.Now.Year; i > 1900; i--)
                comboBoxYear.Items.Add(i.ToString());
            comboBoxYear.SelectedIndex = 0;
            comboBoxRating.Items.Add("ALL");
            for (int i = 0; i <= 10; i++)
                comboBoxRating.Items.Add(i.ToString());
            comboBoxRating.SelectedIndex = 0;

            comboBoxIsSeen.Items.Add("ALL");
            comboBoxIsSeen.Items.Add("Yes");
            comboBoxIsSeen.Items.Add("No");
            comboBoxIsSeen.SelectedIndex = 0;

            comboBoxIMDBRating.Items.Add("ALL");
            comboBoxIMDBRating.Items.Add("<= 4.0");
            comboBoxIMDBRating.Items.Add("> 4.0 & <= 7.0");
            comboBoxIMDBRating.Items.Add("> 7.0 & <= 8.0");
            comboBoxIMDBRating.Items.Add("> 8.0 & <= 9.0");
            comboBoxIMDBRating.Items.Add("> 9.0");
            comboBoxIMDBRating.SelectedIndex = 0;

            comboBoxSize.Items.Add("ALL");
            comboBoxSize.Items.Add("<= 350 MB");
            comboBoxSize.Items.Add("> 350 MB & <= 700 MB");
            comboBoxSize.Items.Add("> 700 MB & <= 1000 MB");
            comboBoxSize.Items.Add("> 1000 MB & <= 1500 MB");
            comboBoxSize.Items.Add("> 1500 MB & <= 2000 MB");
            comboBoxSize.Items.Add("> 2000 MB & <= 3000 MB");
            comboBoxSize.Items.Add("> 3000 MB & <= 5000 MB");
            comboBoxSize.Items.Add("> 5000 MB");
            comboBoxSize.SelectedIndex = 0;

            checkedListBoxGenre.Items.Add("ALL");
            foreach (string genre in Genres)
                checkedListBoxGenre.Items.Add(genre);
            for (int i = 0; i < checkedListBoxGenre.Items.Count; i++)
            {
                checkedListBoxGenre.SetItemChecked(i, true);
            }

            PlanetM_Utility.MovieSearchCriteria lastSearchCriteria = PlanetM_Utility.MovieSearchCriteria.GetLastCriteria(Environment.UserName);
            textBoxName.Text = lastSearchCriteria.MovieName;
            comboBoxLanguage.SelectedItem = lastSearchCriteria.MovieLanguage;
            comboBoxYear.SelectedItem = lastSearchCriteria.MovieYear;
            comboBoxSize.SelectedItem = lastSearchCriteria.MovieSize;
            comboBoxRating.SelectedItem = lastSearchCriteria.MovieMyRating;
            comboBoxIMDBRating.SelectedItem = lastSearchCriteria.MovieIMDBRating;
            comboBoxIsSeen.SelectedItem = lastSearchCriteria.MovieIsSeen;
        }
        #endregion

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            criteria = new AbstractLayer.MovieSearchCriteria();

            criteria.Name = textBoxName.Text.Trim();
            criteria.Language = comboBoxLanguage.SelectedItem.ToString();

            criteria.Year = comboBoxYear.SelectedItem.ToString();
            criteria.MyRating = comboBoxRating.SelectedItem.ToString();
            criteria.IsWatched = comboBoxIsSeen.SelectedItem.ToString();

            Regex regex = new Regex(@"(ALL)|(^)|(MB)|(&)|(\s\s)");
            criteria.Size = regex.Replace(comboBoxSize.SelectedItem.ToString(), match =>
            {
                switch (match.ToString())
                {
                    case "ALL":
                        return "Size = Size";
                    case "":
                        return "Size ";
                    case "MB":
                        return "";
                    case "&":
                        return "AND Size";
                    case "  ":
                        return "";
                }
                return match.ToString();
            });
            regex = new Regex(@"(ALL)|(^)|(&)|(\s\s)");
            criteria.IMDBRating = regex.Replace(comboBoxIMDBRating.SelectedItem.ToString(), match =>
            {
                switch (match.ToString())
                {
                    case "ALL":
                        return "IMDBRating = IMDBRating";
                    case "":
                        return "IMDBRating ";
                    case "&":
                        return "AND IMDBRating";
                    case "  ":
                        return "";
                }
                return match.ToString();
            });

            if (checkedListBoxGenre.CheckedIndices.Contains(0) && checkedListBoxGenre.CheckedItems.Count == checkedListBoxGenre.Items.Count)
            {
                criteria.Genre = "ALL";
            }
            else if (checkedListBoxGenre.CheckedItems.Count < checkedListBoxGenre.Items.Count - 1)
            {
                criteria.Genre = "";
                foreach (var genre in checkedListBoxGenre.CheckedItems)
                {
                    criteria.Genre += genre.ToString() + ",";
                }
                if (checkedListBoxGenre.CheckedItems.Count > 0)
                    criteria.Genre = criteria.Genre.Remove(criteria.Genre.Length - 1, 1);
            }

            DataTable movies = dbObj.GetAllMoviesBySearchCriteria(criteria).Tables[0];
            labelResults.Text = movies.Rows.Count.ToString();
            int iterator = 0;
            dgMovies.Rows.Clear();
            foreach (DataRow drMovie in movies.AsEnumerable())
            {
                dgMovies.Rows.Add();
                dgMovies.Rows[iterator].Cells["MovieName"].Value = drMovie["Name"];
                dgMovies.Rows[iterator].Cells["Language"].Value = drMovie["Language"];
                if (drMovie["Year"].ToString() == "0")
                    dgMovies.Rows[iterator].Cells["Year"].Value = "NA";
                else
                    dgMovies.Rows[iterator].Cells["Year"].Value = drMovie["Year"].ToString();
                if (drMovie["Seen"].ToString() == Boolean.TrueString)
                {
                    dgMovies.Rows[iterator].Cells["Seen"].Value = "Yes";
                }
                else if (drMovie["Seen"].ToString() == bool.FalseString)
                {
                    dgMovies.Rows[iterator].Cells["Seen"].Value = "No";
                }

                dgMovies.Rows[iterator].Cells["Size"].Value = drMovie["Size"];
                if (drMovie["MyRating"].ToString() == "0")
                    dgMovies.Rows[iterator].Cells["MyRating"].Value = "NA";
                else
                    dgMovies.Rows[iterator].Cells["MyRating"].Value = drMovie["MyRating"].ToString();
                if (drMovie["IMDBRating"].ToString() == "0")
                    dgMovies.Rows[iterator].Cells["IMDBRating"].Value = "NA";
                else
                    dgMovies.Rows[iterator].Cells["IMDBRating"].Value = drMovie["IMDBRating"].ToString();
                if (drMovie["Genre"].ToString().Length > 0)
                    dgMovies.Rows[iterator].Cells["Genre"].Value = drMovie["Genre"];
                else
                    dgMovies.Rows[iterator].Cells["Genre"].Value = "";
                ApplyStyleForRow(iterator, drMovie["Language"].ToString(), Convert.ToDouble(drMovie["Size"]), Convert.ToInt32(drMovie["MyRating"]), Convert.ToDouble(drMovie["IMDBRating"]), Convert.ToInt32(drMovie["Year"]));
                iterator++;
            }
            groupBoxExport.Enabled = true;
        }
        private void OpenSelectedMovie(string Name, string Language)
        {
            DataSet dtStMovie;
            Movie tmpMov = new Movie();
            tmpMov.Name = Name;
            tmpMov.Language = Language;
            dtStMovie = dbObj.GetMovieDetails(tmpMov.Name, tmpMov.Language);

            if (!dtStMovie.Tables[0].Rows[0].IsNull("Year"))
                tmpMov.Year = (int)dtStMovie.Tables[0].Rows[0]["Year"];
            tmpMov.IsWatched = (bool)dtStMovie.Tables[0].Rows[0]["Seen"];
            if (!dtStMovie.Tables[0].Rows[0].IsNull("SeenDate"))
                tmpMov.WhenSeen = (DateTime)dtStMovie.Tables[0].Rows[0]["SeenDate"];
            tmpMov.Size = (float)(double)dtStMovie.Tables[0].Rows[0]["Size"];
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
        private void dgMovies_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < dgMovies.RowCount && e.RowIndex > -1)
            {
                OpenSelectedMovie((string)dgMovies.Rows[e.RowIndex].Cells["MovieName"].Value, (string)dgMovies.Rows[e.RowIndex].Cells["Language"].Value);
            }
        }

        private void checkedListBoxGenre_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.Index == 0 && e.NewValue == CheckState.Checked && checkedListBoxGenre.SelectedIndex == 0)
            {
                for (int i = 1; i < checkedListBoxGenre.Items.Count; i++)
                {
                    checkedListBoxGenre.SetItemChecked(i, true);
                }
            }
            else if (e.Index == 0 && e.NewValue == CheckState.Unchecked && checkedListBoxGenre.SelectedIndex == 0)
            {
                for (int i = 1; i < checkedListBoxGenre.Items.Count; i++)
                {
                    checkedListBoxGenre.SetItemChecked(i, false);
                }
            }
            else if (e.Index != 0 && checkedListBoxGenre.SelectedIndex != 0 && e.NewValue == CheckState.Unchecked)
            {
                checkedListBoxGenre.SetItemChecked(0, false);
            }
            else if (e.Index != 0 && checkedListBoxGenre.SelectedIndex != 0 && checkedListBoxGenre.CheckedItems.Count == checkedListBoxGenre.Items.Count - 2 && e.NewValue == CheckState.Checked)
            {
                checkedListBoxGenre.SetItemChecked(0, true);
            }
        }

        private void textBoxName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                buttonSearch_Click(sender, e);
            }
        }

        private void Search_FormClosing(object sender, FormClosingEventArgs e)
        {
            PlanetM_Utility.MovieSearchCriteria lastSearchCriteria = new PlanetM_Utility.MovieSearchCriteria(Environment.UserName);
            lastSearchCriteria.MovieName = textBoxName.Text;
            lastSearchCriteria.MovieLanguage = comboBoxLanguage.SelectedItem.ToString();
            lastSearchCriteria.MovieYear = comboBoxYear.SelectedItem.ToString();
            lastSearchCriteria.MovieSize = comboBoxSize.SelectedItem.ToString();
            lastSearchCriteria.MovieMyRating = comboBoxRating.SelectedItem.ToString();
            lastSearchCriteria.MovieIMDBRating = comboBoxIMDBRating.SelectedItem.ToString();
            lastSearchCriteria.MovieIsSeen = comboBoxIsSeen.SelectedItem.ToString();
            lastSearchCriteria.Save();
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
                    }
                    else
                        MessageBox.Show("Error while deleting movie details from Database !", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Please select one Movie from the available Movies !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        #region Export Functionality

        private void buttonExcel_Click(object sender, EventArgs e)
        {
            //show a file save dialog and ensure the user selects
            //correct file to allow the export
            saveFileDialog.Filter = "Excel (*.xls)|*.xls";
            saveFileDialog.DefaultExt = "xls";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (!saveFileDialog.FileName.Equals(String.Empty))
                {
                    FileInfo f = new FileInfo(saveFileDialog.FileName);
                    if (f.Extension.Equals(".xls"))
                    {
                        ExportDataOperations.ExportGridToExcel(dgMovies, saveFileDialog.FileName);
                    }
                    else
                    {
                        MessageBox.Show("Invalid file type !!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("You did not pick a location to save file to !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void buttonPDF_Click(object sender, EventArgs e)
        {
            //show a file save dialog and ensure the user selects
            //correct file to allow the export
            saveFileDialog.Filter = "Adobe PDF Files (*.pdf)|*.pdf";
            saveFileDialog.DefaultExt = "pdf";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (!saveFileDialog.FileName.Equals(String.Empty))
                {
                    FileInfo f = new FileInfo(saveFileDialog.FileName);
                    if (f.Extension.Equals(".pdf"))
                    {
                        DataTable movies = dbObj.GetAllMoviesBySearchCriteria(criteria).Tables[0];
                        if (movies.Rows.Count > 0)
                            ExportDataOperations.ExportGridToPDF(movies, "Search", saveFileDialog.FileName);
                        else
                            MessageBox.Show("No data available to export !!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Invalid file type !!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("You did not pick a location to save file to !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void buttonCSV_Click(object sender, EventArgs e)
        {
            //show a file save dialog and ensure the user selects
            //correct file to allow the export
            saveFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            saveFileDialog.DefaultExt = "csv";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (!saveFileDialog.FileName.Equals(String.Empty))
                {
                    FileInfo f = new FileInfo(saveFileDialog.FileName);
                    if (f.Extension.Equals(".csv"))
                    {
                        ExportDataOperations.ExportGridToCSV(dgMovies, saveFileDialog.FileName);
                    }
                    else
                    {
                        MessageBox.Show("Invalid file type !!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("You did not pick a location to save file to !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void buttonTXT_Click(object sender, EventArgs e)
        {
            //show a file save dialog and ensure the user selects
            //correct file to allow the export
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt";
            saveFileDialog.DefaultExt = "txt";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (!saveFileDialog.FileName.Equals(String.Empty))
                {
                    FileInfo f = new FileInfo(saveFileDialog.FileName);
                    if (f.Extension.Equals(".txt"))
                    {
                        ExportDataOperations.ExportGridToText(dgMovies, saveFileDialog.FileName);
                    }
                    else
                    {
                        MessageBox.Show("Invalid file type !!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("You did not pick a location to save file to !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        #endregion
    }
}
