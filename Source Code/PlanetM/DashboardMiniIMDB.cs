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
using System.Diagnostics;
using System.Text.RegularExpressions;
using PlanetM.Forms;
using Microsoft.VisualBasic;

namespace PlanetM
{
    public partial class DashboardMiniIMDB : Form
    {
        #region Initialization
        DBOperations dbObj = new DBOperations();

        #region Keys Handling Code
        static string KeysPressed = string.Empty;
        static DateTime LastKeyPressed = DateTime.MinValue;
        #endregion

        #region SyncImdb
        delegate void DelegateTypeIMDBSync(int percentComplete, bool progressStatus, MovieIMDB iMovObj);
        DelegateTypeIMDBSync ImdbSyncDelegate;
        Thread ImdbSyncThread;
        private int highestPercentageReached = 0;
        #endregion

        public DashboardMiniIMDB()
        {
            InitializeComponent();
            LogWrapper.LogInfo("MiniIMDB Dashboard initialization complete.");
            ConfigureGridview();
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

                string[] columns = Configuration.GetConfigurationValues("MiniIMDBColumns");
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
                LogWrapper.LogInfo(string.Format("Column details :: Name : {0} Width : {1}", columnName, width));
                return gridColumn;
            }
            catch (Exception ex)
            {
                //log.Error("Not able to create column for " + columnDetails);
                //throw new WorklistException(ex.ToString());
            }
            return null;
        }
        private Image GetImageForMovieType(string Language, double Rating, int Oscars)
        {
            Image movieImage = null;
            try
            {
                movieImage = Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"Images\MovieIconDefault.jpg"));
                if (Rating >= 8)
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
            }
            catch (Exception ex)
            {
                LogWrapper.LogError(ex);
            }
            return movieImage;
        }
        private void ApplyStyleForRow(int iterator, string Language, int Year, double Rating, int MyRating, string MpaaRating, int Awards, int Nominations, int Oscars)
        {
            try
            {
                dgMovies.Rows[iterator].DefaultCellStyle.BackColor = Color.LightCyan;
                dgMovies.Rows[iterator].Cells["Image"].Value = GetImageForMovieType(Language, Rating, Oscars);
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

                if (MyRating == 0)
                    dgMovies.Rows[iterator].Cells["MyRating"].Style.BackColor = Color.Azure;
                else if (MyRating >= 8)
                    dgMovies.Rows[iterator].Cells["MyRating"].Style.BackColor = Color.Lime;
                else if (MyRating >= 6)
                    dgMovies.Rows[iterator].Cells["MyRating"].Style.BackColor = Color.LimeGreen;
                else
                    dgMovies.Rows[iterator].Cells["MyRating"].Style.BackColor = Color.Bisque;
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
                if (Oscars == 0)
                    dgMovies.Rows[iterator].Cells["Oscars"].Style.BackColor = Color.Azure;
                else if (Oscars >= 8)
                    dgMovies.Rows[iterator].Cells["Oscars"].Style.BackColor = Color.Gold;
                else if (Oscars >= 4)
                    dgMovies.Rows[iterator].Cells["Oscars"].Style.BackColor = Color.SkyBlue;
                else
                    dgMovies.Rows[iterator].Cells["Oscars"].Style.BackColor = Color.MediumTurquoise;
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
                DataTable dtMovies = dbObj.GetAllMoviesFromIMDB().Tables[0];
                this.Text = "MiniIMDB - Dashboard :: Movies Count : " + dtMovies.Rows.Count.ToString();
                LogWrapper.LogInfo(this.Text);
                dgMovies.Rows.Clear();
                int iterator = 0;
                foreach (DataRow drMovie in dtMovies.AsEnumerable())
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
                    if (drMovie["MyRating"].ToString() == "0")
                        dgMovies.Rows[iterator].Cells["MyRating"].Value = "NA";
                    else
                        dgMovies.Rows[iterator].Cells["MyRating"].Value = drMovie["MyRating"].ToString();
                    if (drMovie["Genre"].ToString().Length > 0)
                        dgMovies.Rows[iterator].Cells["Genre"].Value = drMovie["Genre"];
                    else
                        dgMovies.Rows[iterator].Cells["Genre"].Value = "";
                    if (drMovie["MpaaRating"].ToString().Length > 0)
                        dgMovies.Rows[iterator].Cells["MpaaRating"].Value = drMovie["MpaaRating"];
                    else
                        dgMovies.Rows[iterator].Cells["MpaaRating"].Value = "";
                    dgMovies.Rows[iterator].Cells["ReleaseDate"].Value = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(drMovie["ReleaseDate"]));
                    dgMovies.Rows[iterator].Cells["Awards"].Value = drMovie["Awards"];
                    dgMovies.Rows[iterator].Cells["Nominations"].Value = drMovie["Nominations"];
                    dgMovies.Rows[iterator].Cells["Oscars"].Value = drMovie["Oscars"];
                    //LogWrapper.LogInfo(string.Format("Added Movie :: Title : {0}, Language : {1} to the MiniIMDB Dashboard", drMovie["Title"], drMovie["Language"]));
                    ApplyStyleForRow(iterator, drMovie["Language"].ToString(), Convert.ToInt32(drMovie["Year"]), Convert.ToDouble(drMovie["Rating"]), Convert.ToInt32(drMovie["MyRating"]), drMovie["MpaaRating"].ToString(), Convert.ToInt32(drMovie["Awards"]), Convert.ToInt32(drMovie["Nominations"]), Convert.ToInt32(drMovie["Oscars"]));
                    iterator++;
                }
            }
            catch (Exception ex)
            {
                LogWrapper.LogError(ex);
            }
            LogWrapper.LogInfo("Execution finished!");
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
        private void Dashboard_Load(object sender, EventArgs e)
        {
            LoadMovies();
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
        #region OpenOtherForms
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
        #endregion
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
        private void selectedMovieToolStripMenuItem_Click(object sender, EventArgs e)
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
                        if (type == "ImdbALL")
                            movies = dbObj.GetAllMoviesFromIMDB().Tables[0];
                        else if (type == "ImdbLimited")
                            movies = dbObj.GetAllMoviesFromIMDBForPDFExport().Tables[0];
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
            ExportGridDataToPDF("ImdbLimited");
        }

        private void toExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportGridDataToExcel();
        }

        private void toPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportGridDataToPDF("ImdbLimited");
        }

        private void toPDFAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportGridDataToPDF("ImdbALL");
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

        private void addNewMovieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MovieIMDBDetails fmMovieImdb = new MovieIMDBDetails();
            fmMovieImdb.Show();
        }

        private void toolStripButtonAddNewMovie_Click(object sender, EventArgs e)
        {
            MovieIMDBDetails fmMovieImdb = new MovieIMDBDetails();
            fmMovieImdb.Show();
        }
        private void ShowWishlist()
        {
            //Form fmWishlist = new Form();
            //ListBox listMovies = new ListBox();
            //fmWishlist.Controls.Add(listMovies);
            //listMovies.Dock = DockStyle.Fill;
            //fmWishlist.Text = "Wishlist";
            //fmWishlist.ShowIcon = fmWishlist.ShowInTaskbar = false;
            //fmWishlist.Size = new System.Drawing.Size(360, 540);
            //fmWishlist.StartPosition = FormStartPosition.Manual;
            //fmWishlist.Location = new Point(50, 50);
            //DataTable dtWishlistMovies = dbObj.GetMoviesForWishlistFromIMDB().Tables[0];
            //foreach (DataRow drMovie in dtWishlistMovies.AsEnumerable())
            //{
            //    listMovies.Items.Add(drMovie["Language"] + " : " + drMovie["Title"]);
            //}
            //fmWishlist.Show();
            WishlistViewer fmWishlistViewer;
            if ((fmWishlistViewer = (WishlistViewer)IsFormAlreadyOpen(typeof(WishlistViewer))) == null)
            {
                fmWishlistViewer = new WishlistViewer();
                fmWishlistViewer.Show();
            }
            else
            {
                fmWishlistViewer.Show();
                fmWishlistViewer.Focus();
            }
        }
        private void wishListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowWishlist();
        }
        private void toolStripButtonWishlist_Click(object sender, EventArgs e)
        {
            ShowWishlist();
        }

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

        private void toolStripButtonFind_Click(object sender, EventArgs e)
        {
            FindMoviesInGrid();
        }

        private void findWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindMoviesInGrid();
        }

        private void FindMoviesInGrid()
        {
            string strMovieName = Interaction.InputBox("Please enter Movie Title :", "Movie Title").Trim();
            if (strMovieName.Length > 0)
            {
                //int cntItemsFound = 0;
                List<DataGridViewRow> results = new List<DataGridViewRow>();
                foreach (DataGridViewRow row in dgMovies.Rows)
                {
                    row.Selected = false;
                    try
                    {
                        if (row.Cells["Title"].Value != null && Regex.IsMatch(row.Cells["Title"].Value.ToString(), strMovieName, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Compiled))
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
                MessageBox.Show("Please enter proper Movie Title !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        #region Import Functionality

        private void ImportMoviesInMiniIMDB()
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
                if (import.ImportIMDbMoviesFromCSV(openFileDialogCSV.FileName, ref strErrorMsg))
                {
                    MessageHandler.ShowInfo(string.Format("Movies imported from File : {0} to MiniIMDB successfully !", openFileDialogCSV.SafeFileName));
                    LoadMovies();
                }
                else
                    MessageHandler.ShowError(string.Format("Error while importing file : {0}\nError details : {1}", openFileDialogCSV.SafeFileName, strErrorMsg));
            }
        }

        private void importMoviesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportMoviesInMiniIMDB();
        }

        private void toolStripButtonImport_Click(object sender, EventArgs e)
        {
            ImportMoviesInMiniIMDB();
        }

        #endregion

        #region IMDBSync
        private void toolStripButtonSyncIMDB_Click(object sender, EventArgs e)
        {
            SyncSelectedMoviesFromIMDB(false);
        }
        private void syncWithIMDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SyncSelectedMoviesFromIMDB(false);
        }
        private void syncWithIMDBToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SyncSelectedMoviesFromIMDB(false);
        }
        private void toolStripButtonSyncSilentIMDB_Click(object sender, EventArgs e)
        {
            SyncSelectedMoviesFromIMDB(true);
        }
        private void silentSyncWithIMDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SyncSelectedMoviesFromIMDB(true);
        }
        private void silentSyncWithIMDBToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SyncSelectedMoviesFromIMDB(true);
        }
        private void SyncSelectedMoviesFromIMDB(bool IsSilent)
        {
            if (MessageBox.Show("Do you want to retrieve information from Internet?", "Connect to Internet?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                LogWrapper.LogInfo("Execution started!");
                // Set the delegate
                if (IsSilent)
                    ImdbSyncDelegate = UpdateImdbSilentSyncProgressDetails;
                else
                    ImdbSyncDelegate = UpdateImdbSyncProgressDetails;

                if (dgMovies.SelectedRows.Count > 0)
                {
                    List<MovieIMDB> movies = new List<MovieIMDB>();
                    MovieIMDB mov;
                    foreach (DataGridViewRow dgMov in dgMovies.SelectedRows)
                    {
                        mov = new MovieIMDB();
                        mov.Title = (string)dgMov.Cells["Title"].Value;
                        mov.ImdbID = (string)dgMov.Cells["ImdbID"].Value;
                        mov.ImdbURL = "http://www.imdb.com/title/" + mov.ImdbID + "/";
                        mov.Language = (string)dgMov.Cells["Language"].Value;
                        movies.Add(mov);
                        LogWrapper.LogInfo(string.Format("Movie :: Title : {0}, ImdbID : {1}, Language : {2} >> selected for Sync Operation.", mov.Title, mov.ImdbID, mov.Language));
                    }

                    syncWithIMDBToolStripMenuItem.Enabled = syncWithIMDBToolStripMenuItem1.Enabled = toolStripButtonSyncIMDB.Enabled = false;
                    silentSyncWithIMDBToolStripMenuItem.Enabled = silentSyncWithIMDBToolStripMenuItem1.Enabled = toolStripButtonSyncSilentIMDB.Enabled = false;
                    ImdbSyncThread = new Thread(new ParameterizedThreadStart(ImdbSyncBackgroundOperation));
                    ImdbSyncThread.Start(movies);

                    highestPercentageReached = 0;
                    labelProgress.Text = "";
                }
                else
                    MessageBox.Show("Please select few Movies from the available Movies !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LogWrapper.LogInfo("Execution finished!");
            }
        }
        public void ImdbSyncBackgroundOperation(object args)
        {
            List<MovieIMDB> movies = (List<MovieIMDB>)args;
            int count = 0;

            foreach (MovieIMDB movie in movies)
            {
                IMDBOperations iOps = new IMDBOperations(movie);
                count++;
                // Report progress as a percentage of the total task.
                int percentComplete = (int)((float)count / (float)movies.Count * 100);
                if (percentComplete > highestPercentageReached)
                {
                    highestPercentageReached = percentComplete;
                    try
                    {
                        this.BeginInvoke(this.ImdbSyncDelegate, percentComplete, iOps.status, iOps.iMov);
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
        /// <param name="iMovObj"></param>
        void UpdateImdbSyncProgressDetails(int percentComplete, bool progressStatus, MovieIMDB iMovObj)
        {
            if (progressStatus == true)
            {
                labelProgress.Text = string.Format("iSync : {0}% complete! Processed Movie : {1}", percentComplete, iMovObj.Title);
                MovieIMDBDetails movIMDB = new MovieIMDBDetails(iMovObj);
                if (!movIMDB.IsDisposed)
                    movIMDB.Show();
            }
            else
                labelProgress.Text = string.Format("iSync : {0}% complete! Can not process Movie : {1}", percentComplete, iMovObj.Title);

            if (percentComplete == 100)
            {
                syncWithIMDBToolStripMenuItem.Enabled = syncWithIMDBToolStripMenuItem1.Enabled = toolStripButtonSyncIMDB.Enabled = true;
                silentSyncWithIMDBToolStripMenuItem.Enabled = silentSyncWithIMDBToolStripMenuItem1.Enabled = toolStripButtonSyncSilentIMDB.Enabled = true;
            }
        }
        /// <summary>
        /// This is delegated function, runs in the primary-thread (i.e. the thread that owns the Form!)
        /// </summary>
        /// <param name="percentComplete"></param>
        /// <param name="progressStatus"></param>
        /// <param name="iMovObj"></param>
        void UpdateImdbSilentSyncProgressDetails(int percentComplete, bool progressStatus, MovieIMDB iMovObj)
        {
            if (progressStatus == true)
            {
                byte[] bytes = new byte[0];
                if (iMovObj.Poster != null)
                {
                    MemoryStream stream = new MemoryStream();
                    //through the instruction below, we save the image to byte in the object "stream".
                    iMovObj.Poster.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    bytes = stream.ToArray();
                }
                if (dbObj.AddNewMovieIMDB(iMovObj, bytes))
                {
                    labelProgress.Text = string.Format("iSync : {0}% complete! Processed Movie : {1}", percentComplete, iMovObj.Title);
                }
                else
                {
                    labelProgress.Text = string.Format("iSync : {0}% complete! Can not process Movie : {1}", percentComplete, iMovObj.Title);
                }
            }
            else
                labelProgress.Text = string.Format("iSync : {0}% complete! Can not process Movie : {1}", percentComplete, iMovObj.Title);

            if (percentComplete == 100)
            {
                syncWithIMDBToolStripMenuItem.Enabled = syncWithIMDBToolStripMenuItem1.Enabled = toolStripButtonSyncIMDB.Enabled = true;
                silentSyncWithIMDBToolStripMenuItem.Enabled = silentSyncWithIMDBToolStripMenuItem1.Enabled = toolStripButtonSyncSilentIMDB.Enabled = true;
            }
        }
        #endregion

        #region View Discrepancy Reports
        private void ViewDiscrepancyReports()
        {
            DiscrepancyReportsIMDBViewer fmDiscrepancyReportsIMDBViewer;
            if ((fmDiscrepancyReportsIMDBViewer = (DiscrepancyReportsIMDBViewer)IsFormAlreadyOpen(typeof(DiscrepancyReportsIMDBViewer))) == null)
            {
                fmDiscrepancyReportsIMDBViewer = new DiscrepancyReportsIMDBViewer();
                fmDiscrepancyReportsIMDBViewer.Show();
            }
            else
            {
                fmDiscrepancyReportsIMDBViewer.Show();
                fmDiscrepancyReportsIMDBViewer.Focus();
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

        #region MarkMoviesAsSeenUnseen
        private void MarkMovieAsWishlist()
        {
            if (dgMovies.SelectedRows.Count > 0 && dgMovies.SelectedRows.Count < 6)
            {
                string strErrorMsg = string.Empty;
                foreach (DataGridViewRow dgMov in dgMovies.SelectedRows)
                {
                    if (!dbObj.UpdateMovieImdbAsWishlist((string)dgMov.Cells["ImdbID"].Value, true, ref strErrorMsg) && !string.IsNullOrEmpty(strErrorMsg))
                        MessageHandler.ShowError(strErrorMsg, "Failure!");
                }
                LoadMovies();
            }
            else
                MessageBox.Show("Please select 1-5 Movies from the available Movies !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void MarkMovieAsNonWishlist()
        {
            if (dgMovies.SelectedRows.Count > 0 && dgMovies.SelectedRows.Count < 6)
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
                MessageBox.Show("Please select 1-5 Movies from the available Movies !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void wishlistToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            MarkMovieAsWishlist();
        }

        private void nonWishlistToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MarkMovieAsNonWishlist();
        }

        private void wishlistToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MarkMovieAsWishlist();
        }

        private void nonWishlistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MarkMovieAsNonWishlist();
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
                    if (dgMovies.Rows[i].Cells["Title"].Value.ToString().StartsWith(KeysPressed, StringComparison.CurrentCultureIgnoreCase))
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
                MovieIMDBDetails fmMovieImdb = new MovieIMDBDetails((string)dgMovies.CurrentRow.Cells["ImdbID"].Value);
                fmMovieImdb.Show();
            }
        }
    }
}
