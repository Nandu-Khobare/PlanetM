using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using AbstractLayer;
using PlanetM_Ops;
using System.Threading;
using PlanetM_Utility;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using PlanetM.Forms;
using System.Diagnostics;

namespace PlanetM
{
    public partial class DashboardPlanetM : Form
    {
        #region Initialization
        List<Movie> movies;
        DBOperations dbObj = new DBOperations();
        LicenseOperations loObj = new LicenseOperations();

        #region Keys Handling Code
        static string KeysPressed = string.Empty;
        static DateTime LastKeyPressed = DateTime.MinValue;
        #endregion

        #region SyncImdb
        delegate void DelegateTypeIMDBSync(int percentComplete, bool progressStatus, Movie movObj, MovieIMDB iMovObj);
        DelegateTypeIMDBSync ImdbSyncDelegate;
        Thread ImdbSyncThread;
        #endregion

        #region Checksum
        delegate void DelegateTypeChecksum(int percentComplete, string progressStatus);
        DelegateTypeChecksum ChecksumDelegate;
        ManualResetEvent CancelEvent = new ManualResetEvent(false);
        Thread checksumThread;
        private int highestPercentageReached = 0;
        #endregion
        public DashboardPlanetM()
        {
            if (loObj.ValidateLicense())
            {
                this.Hide();
                bool done = false;
                ThreadPool.QueueUserWorkItem((x) =>
                {
                    using (var splashForm = new Splash())
                    {
                        splashForm.Show();
                        while (!done)
                            Application.DoEvents();
                        splashForm.Close();
                    }
                });
                Thread.Sleep(250);
                InitializeComponent();
                LogWrapper.LogInfo("PlanetM Dashboard initialization complete.");
                ConfigureGridview();
                done = true;
                //Thread.Sleep(250);
                this.Show();
                this.Activate();
                this.BringToFront();
            }
            else
            {
                Environment.Exit(9);
            }
        }

        private void ShowSplashForm()
        {
            this.Hide();
            bool done = false;
            ThreadPool.QueueUserWorkItem((x) =>
            {
                using (var splashForm = new Splash())
                {
                    splashForm.Show();
                    while (!done)
                        Application.DoEvents();
                    splashForm.Close();
                }
            });
            Thread.Sleep(3000); // Emulate hardwork
            done = true;
            this.Show();
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
                LogWrapper.LogInfo("Execution started!");
                dgMovies.Columns.Clear();
                dgMovies.AutoGenerateColumns = false;
                dgMovies.Columns.AddRange(GetGridViewColumns().ToArray());
                LogWrapper.LogInfo("Execution finished!");
                //dgMovies.Columns[ISSIMAGE].HeaderText = "";
                //log.Info("Columns Addded Successful for the type ");
            }
            catch (Exception ex)
            {
                LogWrapper.LogError(ex);
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

                string[] columns = Configuration.GetConfigurationValues("DashBoardColumns");
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
                    LogWrapper.LogInfo("Added column " + aCol.Name);
                }
            }
            catch (Exception ex)
            {
                LogWrapper.LogError(ex);
                MessageHandler.ShowError(ex);
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
                LogWrapper.LogInfo(string.Format("Column details :: Name : {0} Width : {1}", columnName, width));
                return gridColumn;
            }
            catch (Exception ex)
            {
                LogWrapper.LogError(ex);
            }
            return null;
        }
        private Image GetImageForMovieType(string Language, double Size, int MyRating)
        {
            Image movieImage = null;
            try
            {
                movieImage = Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"Images\MovieIconDefault.jpg")); ;
                if (MyRating >= 9)
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
            }
            catch (Exception ex)
            {
                LogWrapper.LogError(ex);
            }
            return movieImage;
        }
        private void ApplyStyleForRow(int iterator, string Language, double Size, double IMDBRating, int MyRating, int Year)
        {
            try
            {
                dgMovies.Rows[iterator].DefaultCellStyle.BackColor = Color.LightCyan;
                dgMovies.Rows[iterator].Cells["Image"].Value = GetImageForMovieType(Language, Size, MyRating);
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
                else if (Year >= DateTime.Now.Year - 2)
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
            catch (Exception ex)
            {
                LogWrapper.LogError(ex);
            }
        }
        #endregion
        private Form IsFormAlreadyOpen(Type FormType)
        {
            foreach (Form OpenForm in Application.OpenForms)
            {
                if (OpenForm.GetType() == FormType)
                    return OpenForm;
            }
            return null;
        }
        private void LoadMovies()
        {
            LogWrapper.LogInfo("Execution started!");
            try
            {
                DataTable movies = dbObj.GetAllMovies().Tables[0];
                this.Text = "PlanetM - Dashboard :: Movies Count : " + movies.Rows.Count.ToString();
                LogWrapper.LogInfo(this.Text);
                dgMovies.Rows.Clear();
                int iterator = 0;
                foreach (DataRow drMovie in movies.AsEnumerable())
                {
                    dgMovies.Rows.Add();

                    dgMovies.Rows[iterator].Cells["MovieName"].Value = drMovie["Name"];
                    dgMovies.Rows[iterator].Cells["Language"].Value = drMovie["Language"];
                    if (drMovie["Year"].ToString() == "0")
                        dgMovies.Rows[iterator].Cells["Year"].Value = "NA";
                    else
                        dgMovies.Rows[iterator].Cells["Year"].Value = drMovie["Year"].ToString();
                    dgMovies.Rows[iterator].Cells["Seen"].Value = drMovie["Seen"].ToString();
                    dgMovies.Rows[iterator].Cells["Size"].Value = drMovie["Size"];
                    if (drMovie["IMDBRating"].ToString() == "0.00")
                        dgMovies.Rows[iterator].Cells["IMDBRating"].Value = "NA";
                    else
                        dgMovies.Rows[iterator].Cells["IMDBRating"].Value = drMovie["IMDBRating"].ToString();
                    if (drMovie["MyRating"].ToString() == "0")
                        dgMovies.Rows[iterator].Cells["MyRating"].Value = "NA";
                    else
                        dgMovies.Rows[iterator].Cells["MyRating"].Value = drMovie["MyRating"].ToString();
                    if (drMovie["Genre"].ToString().Length > 0)
                        dgMovies.Rows[iterator].Cells["Genre"].Value = drMovie["Genre"];
                    else
                        dgMovies.Rows[iterator].Cells["Genre"].Value = "";
                    ApplyStyleForRow(iterator, drMovie["Language"].ToString(), Convert.ToDouble(drMovie["Size"]), Convert.ToDouble(drMovie["IMDBRating"]), Convert.ToInt32(drMovie["MyRating"]), Convert.ToInt32(drMovie["Year"]));
                    //LogWrapper.LogInfo(string.Format("Added Movie :: Name : {0}, Language : {1} to the PlanetM Dashboard", drMovie["Name"], drMovie["Language"]));
                    iterator++;
                }
            }
            catch (Exception ex)
            {
                LogWrapper.LogError(ex);
            }
            LogWrapper.LogInfo("Execution finished!");
        }
        #region ScanMovies
        private void ScanAndAddInDB()
        {
            movies = FolderOperations.GetInstance().scanForMovies();

            listBoxMovies.Items.Clear();
            foreach (Movie movie in movies)
            {
                listBoxMovies.Items.Add(movie.Name);
            }
            if (MessageBox.Show("Do you want to add movies in PlanetM ?", "Add in PlanetM ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;
            foreach (Movie movie in movies)
            {
                dbObj.AddNewMovie(movie);
            }
            toolStripButtonAddSelected.Enabled = addSelectedMoviesToolStripMenuItem.Enabled = btnChecksum.Enabled = updateChecksumToolStripMenuItem.Enabled = true;
            MessageHandler.ShowInfo("Movies scanned and added into database successfully !");
            LoadMovies();
        }
        private void ScanAndViewMovies()
        {
            movies = FolderOperations.GetInstance().scanForMovies();

            listBoxMovies.Items.Clear();
            foreach (Movie movie in movies)
            {
                listBoxMovies.Items.Add(movie.Name);
            }
            toolStripButtonAddSelected.Enabled = addSelectedMoviesToolStripMenuItem.Enabled = btnChecksum.Enabled = updateChecksumToolStripMenuItem.Enabled = true;
        }
        private void ScanFolderAndViewMovies()
        {
            ScanFolderOptions dlg = new ScanFolderOptions();
            if (dlg.ShowDialog() != DialogResult.OK) return;

            movies = FolderOperations.GetInstance().getMoviesForFolder(dlg.SelectedFolderPath, dlg.SelectedMovieLanguage);
            //movies = FolderOperations.GetInstance().getMoviesForFolder(@"G:\", "English");
            listBoxMovies.Items.Clear();
            foreach (Movie movie in movies)
            {
                listBoxMovies.Items.Add(movie.Name);
            }
            toolStripButtonAddSelected.Enabled = addSelectedMoviesToolStripMenuItem.Enabled = btnChecksum.Enabled = updateChecksumToolStripMenuItem.Enabled = true;
        }
        private void scanForMoviesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScanAndViewMovies();
        }
        private void scanAndAddInDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScanAndAddInDB();
        }
        private void scanFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScanFolderAndViewMovies();
        }
        private void toolStripButtonScanForMovies_Click(object sender, EventArgs e)
        {
            ScanAndViewMovies();
        }
        private void toolStripButtonScanAddForMovies_Click(object sender, EventArgs e)
        {
            ScanAndAddInDB();
        }
        private void toolStripButtonScanFolder_Click(object sender, EventArgs e)
        {
            ScanFolderAndViewMovies();
        }
        #endregion

        private void AddSelectedMovies()
        {
            if (listBoxMovies.SelectedItems.Count < 1)
            {
                MessageBox.Show("You should select some movies from the list !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            foreach (string movieName in listBoxMovies.SelectedItems)
            {
                Movie movie = movies.Find(mov => mov.Name == movieName);
                dbObj.AddNewMovie(movie);
            }
            MessageHandler.ShowInfo("Selected movies added into database successfully !");
            LoadMovies();
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


        private void toolStripButtonAddSelected_Click(object sender, EventArgs e)
        {
            AddSelectedMovies();
        }
        private void addSelectedMoviesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSelectedMovies();
        }
        private void Dashboard_Load(object sender, EventArgs e)
        {
            this.Hide();
            bool done = false;
            ThreadPool.QueueUserWorkItem((x) =>
            {
                using (var splashForm = new Splash())
                {
                    splashForm.Show();
                    while (!done)
                        Application.DoEvents();
                    splashForm.Close();
                }
            });
            Thread.Sleep(500);
            LoadMovies();
            done = true;
            this.Show();
        }
        private void toolStripButtonLoadMovies_Click(object sender, EventArgs e)
        {
            LoadMovies();
        }
        private void moviesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadMovies();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void DashboardPlanetM_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want to exit PlanetM ?", "Close PlanetM ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                e.Cancel = true;
        }

        private void toolStripButtonFind_Click(object sender, EventArgs e)
        {
            FindMoviesInGrid();
        }

        private void findWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindMoviesInGrid();
        }

        void FindMoviesInGrid()
        {
            string strMovieName = Interaction.InputBox("Please enter Movie Name :", "Movie Name").Trim();
            if (strMovieName.Length > 0)
            {
                //int cntItemsFound = 0;
                List<DataGridViewRow> results = new List<DataGridViewRow>();
                foreach (DataGridViewRow row in dgMovies.Rows)
                {
                    row.Selected = false;
                    try
                    {
                        if (row.Cells["MovieName"].Value != null && Regex.IsMatch(row.Cells["MovieName"].Value.ToString(), strMovieName, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Compiled))
                        {
                            //if (cntItemsFound == 0)
                            //{
                            //    dgMovies.FirstDisplayedScrollingRowIndex = row.Index;
                            //    dgMovies.CurrentCell = dgMovies[0, row.Index];
                            //}
                            //row.Selected = true;
                            //cntItemsFound++;
                            results.Add(row);
                            dgMovies.Rows.Remove(row);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageHandler.ShowError(ex);
                        break;
                    }
                }
                if (results.Count > 0)
                {
                    foreach (var row in results)
                        dgMovies.Rows.Add(row);
                    dgMovies.FirstDisplayedScrollingRowIndex = dgMovies.Rows.Count - results.Count;
                    dgMovies.CurrentCell = dgMovies[0, dgMovies.Rows.Count - results.Count];
                    foreach (var row in results)
                        row.Selected = true;
                }
                MessageHandler.ShowInfo(results.Count.ToString() + " records found in search!");
            }
            else
                MessageBox.Show("Please enter proper Movie Name !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        #region OpenOtherForms
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutPlanetM fmAbout;
            if ((fmAbout = (AboutPlanetM)IsFormAlreadyOpen(typeof(AboutPlanetM))) == null)
            {
                fmAbout = new AboutPlanetM();
                fmAbout.Show();
            }
            else
            {
                fmAbout.Show();
                fmAbout.Focus();
            }
        }
        private void OpenSearchForm()
        {
            Search fmSearch;
            if ((fmSearch = (Search)IsFormAlreadyOpen(typeof(Search))) == null)
            {
                fmSearch = new Search();
                fmSearch.Show();
            }
            else
            {
                fmSearch.Show();
                fmSearch.Focus();
            }
        }
        private void OpenISearchForm()
        {
            SearchIMDB fmISearch;
            if ((fmISearch = (SearchIMDB)IsFormAlreadyOpen(typeof(SearchIMDB))) == null)
            {
                fmISearch = new SearchIMDB();
                fmISearch.Show();
            }
            else
            {
                fmISearch.Show();
                fmISearch.Focus();
            }
        }
        private void OpenMiniIMDBForm()
        {
            DashboardMiniIMDB fmMiniIMDB;
            if ((fmMiniIMDB = (DashboardMiniIMDB)IsFormAlreadyOpen(typeof(DashboardMiniIMDB))) == null)
            {
                fmMiniIMDB = new DashboardMiniIMDB();
                fmMiniIMDB.Show();
            }
            else
            {
                fmMiniIMDB.Show();
                fmMiniIMDB.Focus();
            }
        }
        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSearchForm();
        }
        private void toolStripButtonSearch_Click(object sender, EventArgs e)
        {
            OpenSearchForm();
        }
        private void iSearchWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenISearchForm();
        }
        private void toolStripButtonISearch_Click(object sender, EventArgs e)
        {
            OpenISearchForm();
        }
        private void toolStripButtonMiniIMDB_Click(object sender, EventArgs e)
        {
            OpenMiniIMDBForm();
        }
        private void miniIMDBAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMiniIMDBForm();
        }
        private void addNewMovieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MovieDetails fmmovie = new MovieDetails();
            fmmovie.Show();
        }

        private void toolStripButtonAddNewMovie_Click(object sender, EventArgs e)
        {
            MovieDetails fmmovie = new MovieDetails();
            fmmovie.Show();
        }
        #endregion

        #region Checksum
        private void btnChecksum_Click(object sender, EventArgs e)
        {
            UpdateChecksumForSelected();
        }
        private void updateChecksumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateChecksumForSelected();
        }
        private void btnCancelChecksum_Click(object sender, EventArgs e)
        {
            CancelChecksumOperation();
        }
        private void cancelChecksumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CancelChecksumOperation();
        }
        private void UpdateChecksumForSelected()
        {
            // Set the delegate
            ChecksumDelegate = UpdateChecksumProgressDetails;
            if (listBoxMovies.SelectedItems.Count < 1)
            {
                MessageBox.Show("You should select some movies from the list !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            btnChecksum.Enabled = false;
            updateChecksumToolStripMenuItem.Enabled = false;
            btnCancelChecksum.Enabled = true;
            cancelChecksumToolStripMenuItem.Enabled = true;
            progressBar.Value = highestPercentageReached = 0;
            labelChecksumProgress.Text = "";
            //listBoxMovies.Items.Clear();
            List<string> movieNames = new List<string>();
            foreach (var item in listBoxMovies.SelectedItems)
            {
                movieNames.Add(item.ToString());
            }
            checksumThread = new Thread(new ParameterizedThreadStart(ChecksumBackgroundOperation));
            checksumThread.Start(movieNames);
            CancelEvent.Reset();
        }
        private void CancelChecksumOperation()
        {
            // Raise (signal) the event
            CancelEvent.Set();
            // Wait for the thread to finish
            // (i.e. let it process the signal request, and return)
            checksumThread.Join(); // Wait for thread to exit!
            // Notify the user
            btnCancelChecksum.Enabled = false;
            cancelChecksumToolStripMenuItem.Enabled = false;
            btnChecksum.Enabled = true;
            updateChecksumToolStripMenuItem.Enabled = true;
        }
        public void ChecksumBackgroundOperation(object args)
        {
            List<string> movieNames = (List<string>)args;
            int count = 0;
            foreach (string movieName in movieNames)
            {
                // Check on each iteration if event was raised (signaled)
                // that implicitly means if user requested to 'Cancel'
                if (CancelEvent.WaitOne(0, false) == true)
                {
                    this.BeginInvoke(this.ChecksumDelegate, highestPercentageReached, "Operation Cancelled !");
                    return;
                }
                else
                {
                    Movie movie = movies.Find(mov => mov.Name == movieName);
                    movie.Checksum = FolderOperations.GetInstance().GetMD5HashFromFile(movie.Location);
                    dbObj.UpdateMovieChecksum(movie);
                    //Thread.Sleep(3000);
                    count++;
                    // Report progress as a percentage of the total task.
                    int percentComplete = (int)((float)count / (float)movieNames.Count * 100);
                    if (percentComplete > highestPercentageReached)
                    {
                        highestPercentageReached = percentComplete;
                        try
                        {
                            this.BeginInvoke(this.ChecksumDelegate, percentComplete, "Processed Movie : " + movieName);
                        }
                        catch (InvalidOperationException)
                        { return; }
                    }
                }
            }
        }
        // This is delegated function, runs in the primary-thread (i.e. the thread that owns the Form!)
        void UpdateChecksumProgressDetails(int percentComplete, string progressStatus)
        {
            labelChecksumProgress.Text = percentComplete.ToString() + "% complete! " + progressStatus;
            listBoxStatus.Items.Insert(0, progressStatus);
            progressBar.Value = percentComplete;
            //listBoxMovies.BackColor = Color.Red;
            //listBox1.Items.Add(checksum);
            if (percentComplete == 100)
            {
                btnChecksum.Enabled = true;
                updateChecksumToolStripMenuItem.Enabled = true;
                btnCancelChecksum.Enabled = false;
                cancelChecksumToolStripMenuItem.Enabled = false;
            }
        }
        #endregion

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
        private void selectedMovieToolStripMenuItem_Click(object sender, EventArgs e)
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
        private void toolStripButtonDeleteSelected_Click(object sender, EventArgs e)
        {
            DeleteSelectedMovie();
        }
        private void deleteSelectedMovieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSelectedMovie();
        }

        #region Export Functionality

        private void ExportGridDataToPDF(string type)
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
                        DataTable movies = null; ;
                        if (type == "ALL")
                            movies = dbObj.GetAllMovies().Tables[0];
                        else if (type == "Limited")
                            movies = dbObj.GetAllMoviesForPDFExport().Tables[0];
                        ExportDataOperations.ExportGridToPDF(movies, type, saveFileDialog.FileName);
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

        private void ExportGridDataToExcel()
        {
            //show a file save dialog and ensure the user selects
            //correct file to allow the export
            saveFileDialog.Filter = "Excel Files (*.xls)|*.xls";
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

        private void toolStripButtonExportXL_Click(object sender, EventArgs e)
        {
            ExportGridDataToExcel();
        }

        private void toolStripButtonExportPDF_Click(object sender, EventArgs e)
        {
            ExportGridDataToPDF("Limited");
        }

        private void toExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportGridDataToExcel();
        }

        private void toPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportGridDataToPDF("Limited");
        }

        private void toPDFAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportGridDataToPDF("ALL");
        }

        private void toCSVToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void toTextToolStripMenuItem_Click(object sender, EventArgs e)
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

        #region Printing
        private void toPrinterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDataGridView.Print_DataGridView(dgMovies);
        }
        private void toPrinterVisibleGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog ppvw = new PrintPreviewDialog();
            ppvw.Document = printDocumentMovies;
            if (ppvw.ShowDialog() != DialogResult.OK)
                return;
            printDocumentMovies.Print();
        }
        private void toolStripButtonPrint_Click(object sender, EventArgs e)
        {
            PrintDataGridView.Print_DataGridView(dgMovies);
        }
        private void printDocumentMovies_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(this.dgMovies.Width, this.dgMovies.Height);
            this.dgMovies.DrawToBitmap(bm, new Rectangle(0, 0, this.dgMovies.Size.Width, this.dgMovies.Height));
            //int height = (dgMovies.RowCount) * (dgMovies.Height) / (dgMovies.DisplayedRowCount(true));
            //this.dgMovies.DrawToBitmap(bm, new Rectangle(0, 0, dgMovies.Width, height));
            e.Graphics.DrawImage(bm, 0, 0);
        }
        #endregion

        #region IMDBSync
        private void syncWithIMDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SyncSelectedMoviesFromIMDB();
        }
        private void syncWithIMDBToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SyncSelectedMoviesFromIMDB();
        }
        private void toolStripButtonSyncIMDB_Click(object sender, EventArgs e)
        {
            SyncSelectedMoviesFromIMDB();
        }
        private void SyncSelectedMoviesFromIMDB()
        {
            // Set the delegate
            ImdbSyncDelegate = UpdateImdbSyncProgressDetails;
            if (dgMovies.SelectedRows.Count > 0)
            {
                List<Movie> movies = new List<Movie>();
                Movie mov;
                bool skipMovie;
                foreach (DataGridViewRow dgMov in dgMovies.SelectedRows)
                {
                    skipMovie = false;
                    mov = new Movie();
                    mov.Name = (string)dgMov.Cells["MovieName"].Value;
                    foreach (string collectionWord in Configuration.GetConfigurationValues("CollectionGroupWords"))
                    {
                        if (mov.Name.Contains(collectionWord))
                            skipMovie = true;
                    }
                    if (!skipMovie)
                    {
                        mov.Language = (string)dgMov.Cells["Language"].Value;
                        movies.Add(mov);
                    }
                    else if (skipMovie)
                    {
                        listBoxStatus.Items.Insert(0, string.Format("Movie : {0} is not eligible for syncing.", mov.Name));
                    }
                }
                if (movies.Count > 0)
                {
                    if (MessageBox.Show("Do you want to retrieve information from Internet?", "Connect to Internet?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        syncWithIMDBToolStripMenuItem.Enabled = syncWithIMDBToolStripMenuItem1.Enabled = toolStripButtonSyncIMDB.Enabled = false;
                        ImdbSyncThread = new Thread(new ParameterizedThreadStart(ImdbSyncBackgroundOperation));
                        ImdbSyncThread.Start(movies);
                    }
                }
                else
                    MessageBox.Show("No valid movies available for Synching !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                progressBar.Value = highestPercentageReached = 0;
                labelChecksumProgress.Text = "";
            }
            else
                MessageBox.Show("Please select few Movies from the available Movies !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public void ImdbSyncBackgroundOperation(object args)
        {
            List<Movie> movies = (List<Movie>)args;
            int count = 0;

            foreach (Movie movie in movies)
            {
                Regex regex = new Regex(@"(\.)|(-)|(\[)|(\])|(\s{2,})");
                string movieNameForSearch = regex.Replace(movie.Name, match =>
                {
                    switch (match.ToString())
                    {
                        case ".":
                        case "-":
                        case "[":
                        case "]":
                            return " ";
                        case "  ":
                        case "   ":
                        case "    ":
                            return "";
                    }
                    return match.ToString();
                });
                IMDBOperations iOps = new IMDBOperations(movieNameForSearch);
                count++;
                // Report progress as a percentage of the total task.
                int percentComplete = (int)((float)count / (float)movies.Count * 100);
                if (percentComplete > highestPercentageReached)
                {
                    highestPercentageReached = percentComplete;
                    try
                    {
                        this.BeginInvoke(this.ImdbSyncDelegate, percentComplete, iOps.status, movie, iOps.iMov);
                    }
                    catch (InvalidOperationException)
                    { return; }
                }
            }
        }
        /// <summary>
        /// This is delegated function, runs in the primary-thread (i.e. the thread that owns the Form!)
        /// </summary>
        /// <param name="percentComplete"></param>
        /// <param name="progressStatus"></param>
        /// <param name="movObj"></param>
        /// <param name="iMovObj"></param>
        void UpdateImdbSyncProgressDetails(int percentComplete, bool progressStatus, Movie movObj, MovieIMDB iMovObj)
        {
            if (progressStatus == true)
            {
                labelChecksumProgress.Text = percentComplete.ToString() + "% complete! Processed Movie : " + movObj.Name;
                listBoxStatus.Items.Insert(0, string.Format("Processed Movie : {0}", movObj.Name));
                MovieIMDBDetails movIMDB = new MovieIMDBDetails(movObj, iMovObj);
                if (!movIMDB.IsDisposed)
                    movIMDB.Show();
            }
            else
            {
                labelChecksumProgress.Text = percentComplete.ToString() + "% complete! Can not process Movie : " + movObj.Name;
                listBoxStatus.Items.Insert(0, string.Format("Can not process Movie : {0}", movObj.Name));
            }
            progressBar.Value = percentComplete;
            if (percentComplete == 100)
                syncWithIMDBToolStripMenuItem.Enabled = syncWithIMDBToolStripMenuItem1.Enabled = toolStripButtonSyncIMDB.Enabled = true;
        }
        #endregion

        #region GetAllMoviePosters
        private void GetAllMoviePosters()
        {
            string folderPath;
            FolderBrowserDialog browseFolder = new FolderBrowserDialog();
            browseFolder.RootFolder = Environment.SpecialFolder.MyComputer;
            browseFolder.ShowNewFolderButton = true;
            if (browseFolder.ShowDialog() == DialogResult.OK)
                folderPath = browseFolder.SelectedPath;
            else
                return;
            //folderPath = @"D:\Study\MyDBs\Backup\Posters";
            FileStream fs;
            BinaryWriter bw;
            byte[] imageBytes;
            int cnt = 0;
            foreach (DataRow dr in dbObj.GetAllMovieCovers().Tables[0].AsEnumerable())
            {
                string regexSearch = new string(Path.GetInvalidFileNameChars());
                Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
                if (!dr.IsNull("Poster"))
                {
                    imageBytes = (byte[])dr["Poster"];
                    if (imageBytes.LongLength > 0)
                    {
                        string posterFileName = folderPath + "\\" + dr["Language"] + " - " + r.Replace(dr["Name"].ToString(), "") + ".jpg";
                        fs = new FileStream(posterFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                        bw = new BinaryWriter(fs);
                        bw.Write(imageBytes);
                        bw.Close();
                        fs.Close();
                        cnt++;
                    }
                }
            }
            MessageBox.Show(cnt + " movie posters has been exported to selected path successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Process.Start("Explorer.exe", folderPath);
        }
        private void toolStripButtonGetAllMovieCovers_Click(object sender, EventArgs e)
        {
            GetAllMoviePosters();
        }

        private void allMoviePostersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetAllMoviePosters();
        }
        #endregion

        #region View Duplicate Movies
        private void ViewDuplicateMovies()
        {
            DuplicatesViewer fmDuplicatesViewer;
            if ((fmDuplicatesViewer = (DuplicatesViewer)IsFormAlreadyOpen(typeof(DuplicatesViewer))) == null)
            {
                fmDuplicatesViewer = new DuplicatesViewer();
                fmDuplicatesViewer.Show();
            }
            else
            {
                fmDuplicatesViewer.Show();
                fmDuplicatesViewer.Focus();
            }
        }
        private void duplicateMoviesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewDuplicateMovies();
        }
        private void toolStripButtonViewDuplicateMovies_Click(object sender, EventArgs e)
        {
            ViewDuplicateMovies();
        }
        #endregion

        #region View Recent Movies
        private void ViewRecentMovies()
        {
            RecentMovieViewer fmRecentMovieViewer;
            if ((fmRecentMovieViewer = (RecentMovieViewer)IsFormAlreadyOpen(typeof(RecentMovieViewer))) == null)
            {
                fmRecentMovieViewer = new RecentMovieViewer();
                fmRecentMovieViewer.Show();
            }
            else
            {
                fmRecentMovieViewer.Show();
                fmRecentMovieViewer.Focus();
            }
        }
        private void recentMoviesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewRecentMovies();
        }
        private void toolStripButtonRecentMovies_Click(object sender, EventArgs e)
        {
            ViewRecentMovies();
        }
        #endregion

        #region View Watchlist
        private void ViewWatchlist()
        {
            WatchlistViewer fmWatchlistViewer;
            if ((fmWatchlistViewer = (WatchlistViewer)IsFormAlreadyOpen(typeof(WatchlistViewer))) == null)
            {
                fmWatchlistViewer = new WatchlistViewer();
                fmWatchlistViewer.Show();
            }
            else
            {
                fmWatchlistViewer.Show();
                fmWatchlistViewer.Focus();
            }
        }
        private void myWatchlistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewWatchlist();
        }
        private void toolStripButtonWatchlist_Click(object sender, EventArgs e)
        {
            ViewWatchlist();
        }
        #endregion

        #region MarkMoviesAsSeenUnseen
        private void MarkMovieAsSeen()
        {
            if (dgMovies.SelectedRows.Count > 0 && dgMovies.SelectedRows.Count < 6)
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
                MessageBox.Show("Please select 1-5 Movies from the available Movies !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void MarkMovieAsUnSeen()
        {
            if (dgMovies.SelectedRows.Count > 0 && dgMovies.SelectedRows.Count < 6)
            {
                string strErrorMsg = string.Empty;
                foreach (DataGridViewRow dgMov in dgMovies.SelectedRows)
                {
                    if (!dbObj.UpdateMovieAsSeenUnseen((string)dgMov.Cells["MovieName"].Value,
                                                       (string)dgMov.Cells["Language"].Value, false, ref strErrorMsg) && !string.IsNullOrEmpty(strErrorMsg))
                        MessageHandler.ShowError(strErrorMsg, "Failure!");
                }
                LoadMovies();
            }
            else
                MessageBox.Show("Please select 1-5 Movies from the available Movies !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void seenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MarkMovieAsSeen();
        }

        private void notSeenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MarkMovieAsUnSeen();
        }

        private void seenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MarkMovieAsSeen();
        }

        private void unseenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MarkMovieAsUnSeen();
        }

        #endregion

        #region Import Ratings Functionality

        private void ImportMyRatingsOnIMDB()
        {
            //show a file open dialog and ensure the user selects correct file to allow the import
            OpenFileDialog openFileDialogCSV = new OpenFileDialog
                                                   {
                                                       InitialDirectory = Environment.SpecialFolder.MyComputer.ToString(),
                                                       Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                                                       FilterIndex = 1,
                                                       RestoreDirectory = true
                                                   };

            if (openFileDialogCSV.ShowDialog() == DialogResult.OK)
            {
                string strErrorMsg = string.Empty;
                ImportDataOperations import = new ImportDataOperations();
                if (import.ImportMyRatingsFromCSV(openFileDialogCSV.FileName, ref strErrorMsg))
                {
                    MessageHandler.ShowInfo(string.Format("MyRatings imported from File : {0} successfully !", openFileDialogCSV.SafeFileName));
                    LoadMovies();
                }
                else
                    MessageHandler.ShowError(string.Format("Error while importing file : {0}\nError details : {1}", openFileDialogCSV.SafeFileName, strErrorMsg));
            }
            //string strErrorMsg = string.Empty;
            //ImportDataOperations import = new ImportDataOperations();
            //import.ImportIMDbMoviesFromCSV(@"C:\Users\KeNJe\Desktop\PlanetM\Import\Test.csv", ref strErrorMsg);
        }

        private void importRatingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportMyRatingsOnIMDB();
        }

        private void toolStripButtonImportRatings_Click(object sender, EventArgs e)
        {
            ImportMyRatingsOnIMDB();
        }

        #endregion

        #region View Discrepancy Reports
        private void ViewDiscrepancyReports()
        {
            DiscrepancyReportsViewer fmDiscrepancyReportsViewer;
            if ((fmDiscrepancyReportsViewer = (DiscrepancyReportsViewer)IsFormAlreadyOpen(typeof(DiscrepancyReportsViewer))) == null)
            {
                fmDiscrepancyReportsViewer = new DiscrepancyReportsViewer();
                fmDiscrepancyReportsViewer.Show();
            }
            else
            {
                fmDiscrepancyReportsViewer.Show();
                fmDiscrepancyReportsViewer.Focus();
            }
        }

        private void discrepancyReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewDiscrepancyReports();
        }

        private void toolStripButtonDiscrepancy_Click(object sender, EventArgs e)
        {
            ViewDiscrepancyReports();
        }
        #endregion

        private void dgMovies_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetterOrDigit(e.KeyChar))
            {
                if ((DateTime.Now - LastKeyPressed).TotalSeconds >= 1)
                    KeysPressed = e.KeyChar.ToString();
                else
                    KeysPressed += e.KeyChar.ToString();
                LastKeyPressed = DateTime.Now;
                for (int i = 0; i < dgMovies.SelectedRows.Count; i++)
                    dgMovies.SelectedRows[i].Selected = false;
                for (int i = 0; i < dgMovies.Rows.Count; i++)
                {
                    if (dgMovies.Rows[i].Cells["MovieName"].Value.ToString().StartsWith(KeysPressed, StringComparison.CurrentCultureIgnoreCase))
                    {
                        dgMovies.Rows[i].Selected = true;
                        dgMovies.CurrentCell = dgMovies[0, i];
                        dgMovies.FirstDisplayedScrollingRowIndex = i;
                        return;
                    }
                }
            }
        }

        private void dgMovies_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.Handled = true;
                OpenSelectedMovie((string)dgMovies.CurrentRow.Cells["MovieName"].Value, (string)dgMovies.CurrentRow.Cells["Language"].Value);
            }
        }
    }
}
