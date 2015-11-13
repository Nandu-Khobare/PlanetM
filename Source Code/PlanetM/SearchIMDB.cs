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
    public partial class SearchIMDB : Form
    {
        #region Initialization

        DBOperations dbObj = new DBOperations();
        AbstractLayer.MovieSearchIMDBCriteria criteria;
        public SearchIMDB()
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

                string[] columns = Configuration.GetConfigurationValues("SearchIMDBColumns");
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
        private Image GetImageForMovieType(string Language, int Oscars, int Top250)
        {
            Image movieImage = Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"Images\MovieIconDefault.jpg")); ;
            if (Top250 > 0)
            {
                movieImage = Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"Images\MovieIconTop250.jpg"));
            }
            else if (Oscars > 0)
            {
                movieImage = Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"Images\MovieIconOscar.jpg"));
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
        private void ApplyStyleForRow(int iterator, string Language, int Year, double Rating, string MpaaRating, int Awards, int Nominations, int Top250, int Oscars)
        {
            dgMovies.Rows[iterator].DefaultCellStyle.BackColor = Color.LightCyan;
            dgMovies.Rows[iterator].Cells["Image"].Value = GetImageForMovieType(Language, Oscars, Top250);
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

            if (Year == 0)
                dgMovies.Rows[iterator].Cells["Year"].Style.BackColor = Color.Azure;
            else if (Year >= DateTime.Now.Year - 2)
                dgMovies.Rows[iterator].Cells["Year"].Style.BackColor = Color.SpringGreen;
            else if (Year <= 1975)
                dgMovies.Rows[iterator].Cells["Year"].Style.BackColor = Color.Gold;
            else if (Year <= 1990)
                dgMovies.Rows[iterator].Cells["Year"].Style.BackColor = Color.Goldenrod;
            else
                dgMovies.Rows[iterator].Cells["Year"].Style.BackColor = Color.PaleTurquoise;

            if (Rating == 0)
                dgMovies.Rows[iterator].Cells["Rating"].Style.BackColor = Color.Azure;
            else if (Rating >= 8)
                dgMovies.Rows[iterator].Cells["Rating"].Style.BackColor = Color.Lime;
            else if (Rating >= 6)
                dgMovies.Rows[iterator].Cells["Rating"].Style.BackColor = Color.LimeGreen;
            else
                dgMovies.Rows[iterator].Cells["Rating"].Style.BackColor = Color.Bisque;
            if (MpaaRating == "")
                dgMovies.Rows[iterator].Cells["MpaaRating"].Style.BackColor = Color.Azure;
            else if (MpaaRating == "R")
                dgMovies.Rows[iterator].Cells["MpaaRating"].Style.BackColor = Color.Moccasin;
            else if (MpaaRating == "X" || MpaaRating == "UNRATED")
                dgMovies.Rows[iterator].Cells["MpaaRating"].Style.BackColor = Color.Magenta;
            else if (MpaaRating.StartsWith("PG") || MpaaRating == "G")
                dgMovies.Rows[iterator].Cells["MpaaRating"].Style.BackColor = Color.GreenYellow;
            else if (MpaaRating.StartsWith("TV"))
                dgMovies.Rows[iterator].Cells["MpaaRating"].Style.BackColor = Color.PapayaWhip;
            else
                dgMovies.Rows[iterator].Cells["MpaaRating"].Style.BackColor = Color.Beige;

            if (Awards == 0)
                dgMovies.Rows[iterator].Cells["Awards"].Style.BackColor = Color.Azure;
            else if (Awards >= 50)
                dgMovies.Rows[iterator].Cells["Awards"].Style.BackColor = Color.SteelBlue;
            else if (Awards >= 20)
                dgMovies.Rows[iterator].Cells["Awards"].Style.BackColor = Color.LightSteelBlue;
            else
                dgMovies.Rows[iterator].Cells["Awards"].Style.BackColor = Color.PowderBlue;
            if (Nominations == 0)
                dgMovies.Rows[iterator].Cells["Nominations"].Style.BackColor = Color.Azure;
            else if (Nominations >= 50)
                dgMovies.Rows[iterator].Cells["Nominations"].Style.BackColor = Color.SteelBlue;
            else if (Nominations >= 20)
                dgMovies.Rows[iterator].Cells["Nominations"].Style.BackColor = Color.LightSteelBlue;
            else
                dgMovies.Rows[iterator].Cells["Nominations"].Style.BackColor = Color.PowderBlue;
            if (Top250 == 0)
                dgMovies.Rows[iterator].Cells["Top250"].Style.BackColor = Color.MintCream;
            else if (Top250 <= 50)
                dgMovies.Rows[iterator].Cells["Top250"].Style.BackColor = Color.Gold;
            else if (Top250 <= 150)
                dgMovies.Rows[iterator].Cells["Top250"].Style.BackColor = Color.PaleGoldenrod;
            else
                dgMovies.Rows[iterator].Cells["Top250"].Style.BackColor = Color.PapayaWhip;
            if (Oscars == 0)
                dgMovies.Rows[iterator].Cells["Oscars"].Style.BackColor = Color.Azure;
            else if (Oscars >= 8)
                dgMovies.Rows[iterator].Cells["Oscars"].Style.BackColor = Color.Gold;
            else if (Oscars >= 4)
                dgMovies.Rows[iterator].Cells["Oscars"].Style.BackColor = Color.SkyBlue;
            else
                dgMovies.Rows[iterator].Cells["Oscars"].Style.BackColor = Color.MediumTurquoise;
        }
        private void LoadControls()
        {
            string[] Languages = PlanetM_Utility.Configuration.GetConfigurationValues("Languages");
            string[] Genres = PlanetM_Utility.Configuration.GetConfigurationValues("Genres");
            string[] MpaaRatings = PlanetM_Utility.Configuration.GetConfigurationValues("MPAARatings");

            comboBoxSearchType.Items.Add("Title");
            comboBoxSearchType.Items.Add("Star");
            comboBoxSearchType.Items.Add("Director");
            comboBoxSearchType.Items.Add("Writer");
            comboBoxSearchType.Items.Add("Cast");
            comboBoxSearchType.SelectedIndex = 0;
            comboBoxLanguage.Items.Add("ALL");
            foreach (string lang in Languages)
                comboBoxLanguage.Items.Add(lang);
            comboBoxLanguage.SelectedIndex = 0;
            comboBoxYear.Items.Add("ALL");
            for (int i = DateTime.Now.Year; i > 1900; i--)
                comboBoxYear.Items.Add(i.ToString());
            comboBoxYear.SelectedIndex = 0;

            comboBoxRating.Items.Add("ALL");
            comboBoxRating.Items.Add("<= 4.0");
            comboBoxRating.Items.Add("> 4.0 & <= 6.0");
            comboBoxRating.Items.Add("> 6.0 & <= 7.0");
            comboBoxRating.Items.Add("> 7.0 & <= 8.0");
            comboBoxRating.Items.Add("> 8.0 & <= 9.0");
            comboBoxRating.Items.Add("> 9.0");
            comboBoxRating.SelectedIndex = 0;
            comboBoxMpaaRating.Items.Add("ALL");
            foreach (string MpaaRating in MpaaRatings)
                comboBoxMpaaRating.Items.Add(MpaaRating);
            comboBoxMpaaRating.SelectedIndex = 0;
            comboBoxTop250.Items.Add("ALL");
            comboBoxTop250.Items.Add("Yes");
            comboBoxTop250.Items.Add("No");
            comboBoxTop250.SelectedIndex = 0;
            comboBoxOscars.Items.Add("ALL");
            for (int i = 0; i < 15; i++)
            {
                comboBoxOscars.Items.Add(i);
            }
            comboBoxOscars.SelectedIndex = 0;

            checkedListBoxGenre.Items.Add("ALL");
            foreach (string genre in Genres)
                checkedListBoxGenre.Items.Add(genre);
            for (int i = 0; i < checkedListBoxGenre.Items.Count; i++)
            {
                checkedListBoxGenre.SetItemChecked(i, true);
            }

            PlanetM_Utility.MovieSearchIMDBCriteria lastSearchCriteria = PlanetM_Utility.MovieSearchIMDBCriteria.GetLastCriteria(Environment.UserName);
            comboBoxSearchType.SelectedItem = lastSearchCriteria.SearchType;
            textBoxKeywords.Text = lastSearchCriteria.SearchKeywords;
            comboBoxLanguage.SelectedItem = lastSearchCriteria.MovieLanguage;
            comboBoxYear.SelectedItem = lastSearchCriteria.MovieYear;
            comboBoxRating.SelectedItem = lastSearchCriteria.MovieRating;
            comboBoxMpaaRating.SelectedItem = lastSearchCriteria.MovieMpaaRating;
            comboBoxTop250.SelectedItem = lastSearchCriteria.MovieTop250;
            comboBoxOscars.SelectedItem = lastSearchCriteria.MovieOscars;
        }
        #endregion

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            criteria = new AbstractLayer.MovieSearchIMDBCriteria();

            criteria.SearchType = comboBoxSearchType.SelectedItem.ToString();
            criteria.Keywords = textBoxKeywords.Text.Trim();
            criteria.Language = comboBoxLanguage.SelectedItem.ToString();

            criteria.Year = comboBoxYear.SelectedItem.ToString();
            Regex regex = new Regex(@"(ALL)|(^)|(&)|(\s\s)");
            criteria.Rating = regex.Replace(comboBoxRating.SelectedItem.ToString(), match =>
            {
                switch (match.ToString())
                {
                    case "ALL":
                        return "Rating = Rating";
                    case "":
                        return "Rating ";
                    case "&":
                        return "AND Rating";
                    case "  ":
                        return "";
                }
                return match.ToString();
            });
            criteria.MpaaRating = comboBoxMpaaRating.SelectedItem.ToString();

            criteria.IsTop250 = comboBoxTop250.SelectedItem.ToString();
            criteria.Oscars = comboBoxOscars.SelectedItem.ToString();
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

            DataTable movies = dbObj.GetAllMoviesFromIMDBBySearchCriteria(criteria).Tables[0];
            labelResults.Text = movies.Rows.Count.ToString();
            int iterator = 0;
            dgMovies.Rows.Clear();
            foreach (DataRow drMovie in movies.AsEnumerable())
            {
                dgMovies.Rows.Add();
                dgMovies.Rows[iterator].Cells["ImdbID"].Value = drMovie["ImdbID"];
                dgMovies.Rows[iterator].Cells["Title"].Value = drMovie["Title"];
                dgMovies.Rows[iterator].Cells["Language"].Value = drMovie["Language"];
                if (drMovie["Year"].ToString() == "0")
                    dgMovies.Rows[iterator].Cells["Year"].Value = "NA";
                else
                    dgMovies.Rows[iterator].Cells["Year"].Value = drMovie["Year"].ToString();
                if (drMovie["Rating"].ToString() == "0.00")
                    dgMovies.Rows[iterator].Cells["Rating"].Value = "NA";
                else
                    dgMovies.Rows[iterator].Cells["Rating"].Value = drMovie["Rating"].ToString();
                if (drMovie["Genre"].ToString().Length > 0)
                    dgMovies.Rows[iterator].Cells["Genre"].Value = drMovie["Genre"];
                else
                    dgMovies.Rows[iterator].Cells["Genre"].Value = "";

                if (drMovie["MpaaRating"].ToString().Length > 0)
                    dgMovies.Rows[iterator].Cells["MpaaRating"].Value = drMovie["MpaaRating"];
                else
                    dgMovies.Rows[iterator].Cells["MpaaRating"].Value = "";
                dgMovies.Rows[iterator].Cells["Awards"].Value = drMovie["Awards"];
                dgMovies.Rows[iterator].Cells["Nominations"].Value = drMovie["Nominations"];
                dgMovies.Rows[iterator].Cells["Top250"].Value = drMovie["Top250"];
                dgMovies.Rows[iterator].Cells["Oscars"].Value = drMovie["Oscars"];

                ApplyStyleForRow(iterator, drMovie["Language"].ToString(), Convert.ToInt32(drMovie["Year"]), Convert.ToDouble(drMovie["Rating"]), drMovie["MpaaRating"].ToString(), Convert.ToInt32(drMovie["Awards"]), Convert.ToInt32(drMovie["Nominations"]), Convert.ToInt32(drMovie["Top250"]), Convert.ToInt32(drMovie["Oscars"]));
                iterator++;
            }
            groupBoxExport.Enabled = true;
        }

        private void dgMovies_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < dgMovies.RowCount && e.RowIndex > -1)
            {
                MovieIMDBDetails fmMovieImdb = new MovieIMDBDetails((string)dgMovies.Rows[e.RowIndex].Cells["ImdbID"].Value);
                fmMovieImdb.Show();
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
            PlanetM_Utility.MovieSearchIMDBCriteria lastSearchCriteria = new PlanetM_Utility.MovieSearchIMDBCriteria(Environment.UserName);
            lastSearchCriteria.SearchType = comboBoxSearchType.SelectedItem.ToString();
            lastSearchCriteria.SearchKeywords = textBoxKeywords.Text;
            lastSearchCriteria.MovieLanguage = comboBoxLanguage.SelectedItem.ToString();
            lastSearchCriteria.MovieYear = comboBoxYear.SelectedItem.ToString();
            lastSearchCriteria.MovieRating = comboBoxRating.SelectedItem.ToString();
            lastSearchCriteria.MovieMpaaRating = comboBoxMpaaRating.SelectedItem.ToString();
            lastSearchCriteria.MovieTop250 = comboBoxTop250.SelectedItem.ToString();
            lastSearchCriteria.MovieOscars = comboBoxOscars.SelectedItem.ToString();
            lastSearchCriteria.Save();
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
            if (dgMovies.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Do you want to delete Movie : " + dgMovies.SelectedRows[0].Cells["Title"].Value + " from MiniIMDB ?", "Delete Movie Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (dbObj.DeleteMovieFromIMDB((string)dgMovies.SelectedRows[0].Cells["ImdbID"].Value))
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
                        DataTable movies = dbObj.GetAllMoviesFromIMDBBySearchCriteria(criteria).Tables[0];
                        if (movies.Rows.Count > 0)
                            ExportDataOperations.ExportGridToPDF(movies, "ImdbSearch", saveFileDialog.FileName);
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
