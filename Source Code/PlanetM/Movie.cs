using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AbstractLayer;
using PlanetM_Ops;
using PlanetM_Utility;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace PlanetM
{
    public partial class MovieDetails : Form
    {
        #region Initialization
        Movie mov;
        bool IsCoverUpdateRequired = false;
        DBOperations dbObj = new DBOperations();
        string movieName, Language;
        public MovieDetails()
        {
            InitializeComponent();
            LoadControls();
            btnUpdateMovie.Text = "Add Movie";
            btnDeleteMovie.Enabled = btnOpenFolder.Enabled = btnPlayMovie.Enabled = buttonShowIMDB.Enabled = false;
            comboBoxYear.SelectedIndex = comboBoxRating.SelectedIndex = comboBoxLanguage.SelectedIndex = 0;
            linkLabelIMDB.Enabled = linkLabelMergeMovie.Enabled = false;
        }
        public MovieDetails(Movie movObj)
        {
            InitializeComponent();
            LoadControls();
            mov = movObj;
            movieName = movObj.Name;
            Language = movObj.Language;
        }
        public MovieDetails(string ImdbID)
        {
            InitializeComponent();
            LoadControls();
            DataSet dtStMovie;
            dtStMovie = dbObj.GetMovieDetails(ImdbID);
            if (dtStMovie.Tables.Count != 0)
            {
                LoadMovieDataFromDataSet(dtStMovie);
            }
            else if (MessageBox.Show("Movie not found in PlanetM for ID : " + ImdbID + Environment.NewLine + "Do you want to create dummy entry in PlanetM?", "Add in PlanetM?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                string strErrorMsg = string.Empty;
                if (!dbObj.AddNewMovieFromIMDB(ImdbID, ref strErrorMsg))
                {
                    MessageHandler.ShowError("Error while creating entry in PlanetM. " + Environment.NewLine + strErrorMsg);
                    this.Close();
                    return;
                }
                else
                {
                    dtStMovie = dbObj.GetMovieDetails(ImdbID);
                    if (dtStMovie.Tables.Count != 0)
                    {
                        LoadMovieDataFromDataSet(dtStMovie);
                    }
                    else
                    {
                        MessageHandler.ShowError("Error while creating entry in PlanetM. Contact technical support.");
                        this.Close();
                    }
                }
            }
            else
                this.Close();
        }
        private void LoadControls()
        {
            pictureBoxCover.Image = global::PlanetM.Properties.Resources.NoCover;
            pictureBoxCover.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxCover.BorderStyle = BorderStyle.FixedSingle;
            string[] Languages = PlanetM_Utility.Configuration.GetConfigurationValues("Languages");
            string[] Genres = PlanetM_Utility.Configuration.GetConfigurationValues("Genres");
            foreach (string lang in Languages)
                comboBoxLanguage.Items.Add(lang);
            for (int i = DateTime.Now.Year; i > 1900; i--)
                comboBoxYear.Items.Add(i);
            dateTimePicker1.MinDate = DateTime.Parse("01-01-1901");
            dateTimePicker1.MaxDate = DateTime.Now.Date.AddDays(1);
            dateTimePicker1.Value = DateTime.Now.Date;
            for (int i = 0; i <= 10; i++)
                comboBoxRating.Items.Add(i);
            foreach (string lang in Genres)
                checkedListBoxGenre.Items.Add(lang);
        }
        private void LoadMovieDataFromDataSet(DataSet dtStMovie)
        {
            mov = new Movie();
            mov.Name = dtStMovie.Tables[0].Rows[0]["Name"].ToString();
            movieName = mov.Name;
            mov.Language = dtStMovie.Tables[0].Rows[0]["Language"].ToString();
            Language = mov.Language;
            if (!dtStMovie.Tables[0].Rows[0].IsNull("Year"))
                mov.Year = (int)dtStMovie.Tables[0].Rows[0]["Year"];
            mov.IsWatched = (bool)dtStMovie.Tables[0].Rows[0]["Seen"];
            if (!dtStMovie.Tables[0].Rows[0].IsNull("SeenDate"))
                mov.WhenSeen = (DateTime)dtStMovie.Tables[0].Rows[0]["SeenDate"];
            mov.Size = (float)(double)dtStMovie.Tables[0].Rows[0]["Size"];
            if (!dtStMovie.Tables[0].Rows[0].IsNull("MyRating"))
                mov.MyRating = (int)dtStMovie.Tables[0].Rows[0]["MyRating"];
            if (!dtStMovie.Tables[0].Rows[0].IsNull("IMDBRating"))
                mov.IMDBRating = (float)Convert.ToDouble(dtStMovie.Tables[0].Rows[0]["IMDBRating"]);
            if (!dtStMovie.Tables[0].Rows[0].IsNull("IMDBID"))
                mov.IMDBID = (string)dtStMovie.Tables[0].Rows[0]["IMDBID"];
            if (!dtStMovie.Tables[0].Rows[0].IsNull("Quality"))
                mov.Quality = (string)dtStMovie.Tables[0].Rows[0]["Quality"];
            foreach (string genre in dtStMovie.Tables[0].Rows[0]["Genre"].ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
            {
                mov.Genre.Add(genre);
            }
            foreach (DataRow dr in dtStMovie.Tables[0].Rows)
            {
                mov.Locations.Add(dr.Field<string>("Location"));
                mov.Checksums.Add(dr.Field<string>("Checksum"));
                if (dr.Field<object>("LocationSize") != null)
                    mov.LocationSize.Add((float)dr.Field<double>("LocationSize"));
            }
        }
        private void fmMovie_Load(object sender, EventArgs e)
        {
            if (mov != null && movieName != null)
            {
                MemoryStream stream = new MemoryStream(dbObj.GetMovieCover(mov));
                //With the code below, you are in fact converting the byte array of image to the real image.
                if (stream.Capacity != 0)
                    pictureBoxCover.Image = Image.FromStream(stream);

                this.Text = mov.Language + " : " + mov.Name;
                textBoxName.Text = mov.Name;
                comboBoxLanguage.Text = mov.Language;

                comboBoxYear.Text = mov.Year.ToString();
                checkBoxIsWatched.Checked = mov.IsWatched;
                if (mov.WhenSeen != DateTime.MinValue && mov.WhenSeen != DateTime.MinValue.AddYears(1899))
                    dateTimePicker1.Value = mov.WhenSeen;

                comboBoxRating.Text = mov.MyRating.ToString();
                textBoxIMDBID.Text = mov.IMDBID;
                textBoxIMDBRating.Text = mov.IMDBRating.ToString();
                textBoxQuality.Text = mov.Quality;
                textBoxSize.Text = mov.Size.ToString();
                foreach (string genre in mov.Genre)
                    checkedListBoxGenre.SetItemChecked(checkedListBoxGenre.Items.IndexOf(genre), true);

                for (int i = 0; i < mov.Locations.Count; i++)
                {
                    if (mov.Locations[i] != null)
                    {
                        listBoxLocations.Items.Add(mov.Locations[i]);
                        listBoxChecksum.Items.Add(mov.LocationSize[i] + " : " + mov.Checksums[i]);
                    }
                }

                for (int i = mov.Locations.Count - 1; i >= 0; i--)
                {
                    if (mov.Locations[i] != null)
                    {
                        Button button = new Button();
                        button.Tag = mov.Locations[i];
                        button.Dock = DockStyle.Top;
                        button.Size = new Size(0, 40);

                        AssignImageForLocationButton(button, Path.GetPathRoot(mov.Locations[i]));
                        button.BackgroundImageLayout = ImageLayout.Stretch;

                        ContextMenuStrip cntxMenuForLocationButtons = new System.Windows.Forms.ContextMenuStrip();
                        ToolStripItem Open = cntxMenuForLocationButtons.Items.Add("Open movie folder");
                        Open.Image = global::PlanetM.Properties.Resources.Open;
                        ToolStripItem Play = cntxMenuForLocationButtons.Items.Add("Play movie");
                        Play.Image = global::PlanetM.Properties.Resources.Play;
                        ToolStripItem Delete = cntxMenuForLocationButtons.Items.Add("Delete location");
                        Delete.Image = global::PlanetM.Properties.Resources.Remove;
                        cntxMenuForLocationButtons.Tag = mov.Locations[i];
                        button.ContextMenuStrip = cntxMenuForLocationButtons;

                        cntxMenuForLocationButtons.ItemClicked += new ToolStripItemClickedEventHandler(cntxMenuForLocationButtons_ItemClicked);
                        button.Click += new EventHandler(LocationButtonClickEvent);
                        this.panelAvailableOnLocations.Controls.Add(button);
                    }
                }
            }
        }

        void cntxMenuForLocationButtons_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ContextMenuStrip ctxMenuStrip = sender as ContextMenuStrip;

            if (e.ClickedItem.Text.Contains("Play"))
            {
                if (Directory.Exists(ctxMenuStrip.Tag.ToString()))
                {
                    StringBuilder VLCPlayerArgument = new StringBuilder();
                    DirectoryInfo movieDir = new DirectoryInfo(ctxMenuStrip.Tag.ToString());
                    string[] fileExtentions = PlanetM_Utility.Configuration.GetConfigurationValues("VideoFilesPattern");
                    foreach (string fileExtention in fileExtentions)
                    {
                        foreach (var file in movieDir.GetFiles(fileExtention, SearchOption.AllDirectories))
                        {
                            VLCPlayerArgument.Append(Regex.Replace(file.FullName, @"(^)|($)", match =>
                            {
                                switch (match.ToString())
                                {
                                    case "":
                                        return "\"";
                                }
                                return match.ToString();
                            }));
                            VLCPlayerArgument.Append(" ");
                        }
                    }
                    Process.Start(PlanetM_Utility.Configuration.ReadConfig("VLCPlayerFullPath"), VLCPlayerArgument.ToString());
                }
                else
                {
                    MessageBox.Show("Selected path not available! Please verify again !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (e.ClickedItem.Text.Contains("Delete"))
            {
                if (dbObj.DeleteMovieLocation(ctxMenuStrip.Tag.ToString()))
                {
                    MessageBox.Show("Location details has been deleted from Database", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error while deleting location details from Database !", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (e.ClickedItem.Text.Contains("Open"))
            {
                if (Directory.Exists(ctxMenuStrip.Tag.ToString()))
                {
                    Process.Start("Explorer.exe", ctxMenuStrip.Tag.ToString());
                }
                else
                {
                    MessageBox.Show("The path : " + ctxMenuStrip.Tag.ToString() + " not available! Please verify again !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void AssignImageForLocationButton(Button button, string Location)
        {
            string[] ExternalHardDrivePaths = PlanetM_Utility.Configuration.GetConfigurationValues("LocationExternalHardDrive");
            string[] LaptopPaths = PlanetM_Utility.Configuration.GetConfigurationValues("LocationLaptop");
            string[] DVDDrivePaths = PlanetM_Utility.Configuration.GetConfigurationValues("LocationDVD");
            string[] PCPaths = PlanetM_Utility.Configuration.GetConfigurationValues("LocationPC");

            if (ExternalHardDrivePaths.Contains(Location))
                button.BackgroundImage = global::PlanetM.Properties.Resources.LocationExternalHardDrive;
            else if (LaptopPaths.Contains(Location))
                button.BackgroundImage = global::PlanetM.Properties.Resources.LocationLaptop;
            else if (DVDDrivePaths.Contains(Location))
                button.BackgroundImage = global::PlanetM.Properties.Resources.LocationDVD;
            else if (PCPaths.Contains(Location))
                button.BackgroundImage = global::PlanetM.Properties.Resources.LocationPC;
            else
            {
                if (Location != "")
                {
                    Uri path = new Uri(Location);
                    if (PCPaths.Contains(path.Host.ToUpper()))
                        button.BackgroundImage = global::PlanetM.Properties.Resources.LocationPC;
                    else if (path.IsUnc)
                        button.BackgroundImage = global::PlanetM.Properties.Resources.LocationNetwork;
                    else
                        button.BackgroundImage = global::PlanetM.Properties.Resources.LocationOther;
                }
                else
                    button.BackgroundImage = global::PlanetM.Properties.Resources.LocationOther;
            }
        }
        void LocationButtonClickEvent(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                if (Directory.Exists(button.Tag.ToString()))
                {
                    Process.Start("Explorer.exe", button.Tag.ToString());
                }
                else
                {
                    MessageBox.Show("The path : " + button.Tag.ToString() + " not available! Please verify again !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        #endregion

        private void MovieDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnUpdateMovie_Click(object sender, EventArgs e)
        {
            if (mov != null && movieName != null)
            {
                mov.Name = textBoxName.Text.Trim();
                mov.Language = comboBoxLanguage.SelectedItem.ToString();

                if (IsCoverUpdateRequired)
                {
                    MemoryStream stream = new MemoryStream();
                    //through the instruction below, we save the image to byte in the object "stream".
                    pictureBoxCover.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] bytes = stream.ToArray();
                    dbObj.UpdateMovieCover(mov, bytes);
                }

                mov.WhenSeen = dateTimePicker1.Value;
                mov.Quality = textBoxQuality.Text.Trim();
                mov.Genre.Clear();
                foreach (string genre in checkedListBoxGenre.CheckedItems)
                {
                    mov.Genre.Add(genre);
                }

                if (dbObj.UpdateMovie(movieName, Language, mov))
                {
                    MessageBox.Show("Movie details has been updated in Database", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                    MessageBox.Show("Error while updating movie details in Database !", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else            //Adding new movie
            {
                if (textBoxName.Text.Trim().Length <= 0)
                {
                    MessageBox.Show("Please give a proper name to the movie !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                mov = new Movie();
                mov.Name = textBoxName.Text.Trim();
                mov.Language = comboBoxLanguage.SelectedItem.ToString();
                if (dbObj.GetMovieDetails(mov.Name, mov.Language).Tables[0].Rows.Count > 0)
                {
                    MessageBox.Show("Movie already exists in PlanetM !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                mov.Size = 0;
                mov.Year = Convert.ToInt32(comboBoxYear.SelectedItem.ToString());
                mov.Location = "NA";
                dbObj.AddNewMovie(mov);

                if (IsCoverUpdateRequired)
                {
                    MemoryStream stream = new MemoryStream();
                    //through the instruction below, we save the image to byte in the object "stream".
                    pictureBoxCover.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] bytes = stream.ToArray();
                    dbObj.UpdateMovieCover(mov, bytes);
                }

                mov.MyRating = Convert.ToInt32(comboBoxRating.SelectedItem.ToString());
                mov.IsWatched = checkBoxIsWatched.Checked;
                mov.WhenSeen = dateTimePicker1.Value;
                if (textBoxIMDBRating.Text != "" || textBoxIMDBRating.Text != string.Empty)
                    mov.IMDBRating = (float)Convert.ToDouble(textBoxIMDBRating.Text);
                mov.Quality = textBoxQuality.Text.Trim();
                mov.Genre.Clear();
                foreach (string genre in checkedListBoxGenre.CheckedItems)
                {
                    mov.Genre.Add(genre);
                }

                if (dbObj.UpdateMovie(mov.Name, mov.Language, mov))
                {
                    MessageBox.Show("Movie details has been added in Database", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    dbObj.DeleteMovie(mov);
                    MessageBox.Show("Error while adding movie details in Database !", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBoxCover.Image = Image.FromFile(openFileDialog1.FileName);
                IsCoverUpdateRequired = true;
            }
        }

        private void checkBoxIsWatched_CheckedChanged(object sender, EventArgs e)
        {
            if (mov != null)
                mov.IsWatched = checkBoxIsWatched.Checked;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pictureBoxCover.Image = global::PlanetM.Properties.Resources.NoCover;
            //pictureBox1.Image = Image.FromFile(@"..\Images\NoCover.jpg");
            IsCoverUpdateRequired = true;
        }

        private void btnDeleteMovie_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete Movie : " + mov.Name + " from PlanetM ?", "Delete Movie Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                if (dbObj.DeleteMovie(mov))
                {
                    MessageBox.Show("Movie details has been deleted from Database", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                    MessageBox.Show("Error while deleting movie details from Database !", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonDeleteLocation_Click(object sender, EventArgs e)
        {
            if (listBoxLocations.SelectedItems.Count == 1)
            {
                if (dbObj.DeleteMovieLocation(listBoxLocations.SelectedItem.ToString()))
                {
                    MessageBox.Show("Location details has been deleted from Database", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error while deleting location details from Database !", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (listBoxLocations.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select one location from the available locations !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (listBoxLocations.SelectedItems.Count > 1)
            {
                MessageBox.Show("Please select only one location from the available locations !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnPlayMovie_Click(object sender, EventArgs e)
        {
            if (listBoxLocations.SelectedItems.Count == 1)
            {
                if (Directory.Exists(listBoxLocations.SelectedItem.ToString()))
                {
                    StringBuilder VLCPlayerArgument = new StringBuilder();
                    DirectoryInfo movieDir = new DirectoryInfo(listBoxLocations.SelectedItem.ToString());
                    string[] fileExtentions = PlanetM_Utility.Configuration.GetConfigurationValues("VideoFilesPattern");
                    foreach (string fileExtention in fileExtentions)
                    {
                        foreach (var file in movieDir.GetFiles(fileExtention, SearchOption.AllDirectories))
                        {
                            VLCPlayerArgument.Append(Regex.Replace(file.FullName, @"(^)|($)", match =>
                            {
                                switch (match.ToString())
                                {
                                    case "":
                                        return "\"";
                                }
                                return match.ToString();
                            }));
                            VLCPlayerArgument.Append(" ");
                        }
                    }
                    Process.Start(PlanetM_Utility.Configuration.ReadConfig("VLCPlayerFullPath"), VLCPlayerArgument.ToString());
                }
                else
                {
                    MessageBox.Show("Selected path not available! Please verify again !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (listBoxLocations.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select one location from the available locations !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (listBoxLocations.SelectedItems.Count > 1)
            {
                MessageBox.Show("Please select only one location from the available locations !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            if (listBoxLocations.Items.Count == 1)
            {
                if (Directory.Exists(listBoxLocations.Items[0].ToString()))
                {
                    Process.Start("Explorer.exe", listBoxLocations.Items[0].ToString());
                }
                else
                {
                    MessageBox.Show("The path : " + listBoxLocations.Items[0] + " not available! Please verify again !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (listBoxLocations.SelectedItems.Count == 1)
            {
                if (Directory.Exists(listBoxLocations.SelectedItem.ToString()))
                {
                    Process.Start("Explorer.exe", listBoxLocations.SelectedItem.ToString());
                }
                else
                {
                    MessageBox.Show("The path : " + listBoxLocations.SelectedItem.ToString() + " not available! Please verify again !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (listBoxLocations.SelectedItems.Count == 0 && listBoxLocations.Items.Count != 0)
            {
                MessageBox.Show("Please select one location from the available locations !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (listBoxLocations.SelectedItems.Count > 1)
            {
                MessageBox.Show("Please select only one location from the available locations !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (listBoxLocations.Items.Count == 0)
            {
                MessageBox.Show("Movie not available in system !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void comboBoxYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mov != null)
                mov.Year = Convert.ToInt32(comboBoxYear.SelectedItem.ToString());
        }

        private void comboBoxRating_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mov != null)
                mov.MyRating = Convert.ToInt32(comboBoxRating.SelectedItem.ToString());
        }

        private void buttonUpdateChecksum_Click(object sender, EventArgs e)
        {
            if (listBoxLocations.SelectedItems.Count == 1)
            {
                if (Directory.Exists(listBoxLocations.SelectedItem.ToString()))
                {
                    mov.Location = listBoxLocations.SelectedItem.ToString();
                    mov.Checksum = FolderOperations.GetInstance().GetMD5HashFromFile(mov.Location);
                    if (dbObj.UpdateMovieChecksum(mov))
                    {
                        MessageBox.Show("Checksum has been updated in the Database for selected location.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error while updating checksum in the Database !", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Selected path not available! Please verify again !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (listBoxLocations.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select one location from the available locations !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (listBoxLocations.SelectedItems.Count > 1)
            {
                MessageBox.Show("Please select only one location from the available locations !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonShowIMDB_Click(object sender, EventArgs e)
        {
            foreach (string collectionWord in Configuration.GetConfigurationValues("CollectionGroupWords"))
            {
                if (mov.Name.Contains(collectionWord))
                {
                    MessageBox.Show("Not applicable for film-series !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            if (textBoxIMDBID.Text.Length > 5)
            {
                MovieIMDBDetails movIMDB = new MovieIMDBDetails(mov, true);
                if (!movIMDB.IsDisposed)
                {
                    movIMDB.Show();
                    this.Close();
                }
            }
            else
            {
                if (MessageBox.Show("Movie not available in miniIMDB !" + Environment.NewLine + "Do you want to retrieve information from Internet?", "Connect to Internet?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    Cursor = Cursors.WaitCursor;
                    MovieIMDBDetails movIMDB = new MovieIMDBDetails(mov, false);
                    if (!movIMDB.IsDisposed)
                    {
                        movIMDB.Show();
                        this.Close();
                    }
                    else
                        Cursor = Cursors.Default;
                }
            }
        }

        private void linkLabelIMDB_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (textBoxIMDBID.Text.Length == 0)
            {
                string strIMDBID = Interaction.InputBox("Please enter IMDB ID :", "IMDB ID").Trim();
                if (strIMDBID.Length > 5)
                {
                    string strErrorMsg = string.Empty;
                    dbObj.SynchronizeMovieWithIMDB(mov.Name, mov.Language, strIMDBID, ref strErrorMsg);
                    if (!string.IsNullOrEmpty(strErrorMsg))
                        MessageHandler.ShowError(strErrorMsg, "Failure!");
                    else
                    {
                        MessageHandler.ShowInfo(mov.Name + " has been synchronized with IMDB ID : " + strIMDBID, "Sync Complete!");
                        this.Close();
                    }
                }
                else
                    MessageBox.Show("Please enter proper IMDB ID !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("Not applicable for movies already synced with IMDB !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void linkLabelMergeMovie_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (textBoxIMDBID.Text.Length == 0)
            {
                string mergeMovieName = Interaction.InputBox("Please enter movie name to merge with :", "Merge Movie Name").Trim();
                if (mergeMovieName.Length > 0)
                {
                    string strErrorMsg = string.Empty;
                    dbObj.MergeMovieWithExistingMovie(mov.Name, mov.Language, mergeMovieName, ref strErrorMsg);
                    if (!string.IsNullOrEmpty(strErrorMsg))
                        MessageHandler.ShowError(strErrorMsg, "Failure!");
                    else
                    {
                        MessageHandler.ShowInfo(mov.Name + " has been merged with movie : " + mergeMovieName, "Merge Complete!");
                        this.Close();
                    }
                }
                else
                    MessageBox.Show("Please enter proper Movie Name !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("Not applicable for movies synced with IMDB !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
