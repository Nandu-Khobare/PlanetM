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
using System.Configuration;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Collections;
using Microsoft.VisualBasic;
using System.Speech.Synthesis;

namespace PlanetM
{
    public partial class MovieIMDBDetails : Form
    {
        #region Initialization
        Movie mov;
        MovieIMDB iMov;
        bool IsAvailableInDB = false, IsPosterUpdateRequired = false;
        DBOperations dbObj = new DBOperations();
        SpeechSynthesizer speaker = new SpeechSynthesizer();
        string movieName, Language;
        public MovieIMDBDetails()
        {
            InitializeComponent();
            LoadControls();
            btnDeleteMovie.Enabled = btnOpenInIE.Enabled = btnAddMovie.Enabled = false;
            Language = comboBoxLanguage.Text = "English";
        }
        public MovieIMDBDetails(Movie movObj, bool IsAvailable)
        {
            InitializeComponent();
            LoadControls();
            mov = movObj;
            movieName = movObj.Name;
            Language = movObj.Language;
            if (IsAvailable)
            {
                LoadMovieDataFromDB(mov.IMDBID);
            }
            else
            {
                LoadMovieDataFromInternet();
                btnDeleteMovie.Enabled = false;
            }
        }
        public MovieIMDBDetails(string ImdbID)
        {
            InitializeComponent();
            LoadControls();
            LoadMovieDataFromDB(ImdbID);
            Language = iMov.Language;
        }
        /// <summary>
        /// This constructor will be called when Syncing Movies from PlanetM
        /// </summary>
        /// <param name="movObj"></param>
        /// <param name="iMovObj"></param>
        public MovieIMDBDetails(Movie movObj,MovieIMDB iMovObj)
        {
            InitializeComponent();
            LoadControls();
            mov = movObj;
            movieName = movObj.Name;
            Language = movObj.Language;
            iMov = iMovObj;
            iMov.Language = mov.Language;
            btnDeleteMovie.Enabled = false;
        }
        /// <summary>
        /// This constructor will be called when Syncing Movies from MiniIMDB
        /// </summary>
        /// <param name="iMovObj"></param>
        public MovieIMDBDetails(MovieIMDB iMovObj)
        {
            InitializeComponent();
            LoadControls();
            iMov = iMovObj;
            Language = iMov.Language;
            IsAvailableInDB = true;
            btnAddMovie.Text = "Update Movie";
        }
        private void LoadControls()
        {
            pictureBoxPoster.Image = global::PlanetM.Properties.Resources.NoCover;
            pictureBoxPoster.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxPoster.BorderStyle = BorderStyle.FixedSingle;
            string[] Languages = PlanetM_Utility.Configuration.GetConfigurationValues("Languages");
            foreach (string lang in Languages)
                comboBoxLanguage.Items.Add(lang);
        }
        private void LoadMovieDataFromDB(string ImdbID)
        {
            DataSet dtStMovie;
            dtStMovie = dbObj.GetMovieDetailsFromIMDB(ImdbID);
            if (dtStMovie.Tables[0].Rows.Count == 1)
            {
                IsAvailableInDB = true;
                btnAddMovie.Text = "Update Movie";
                iMov = new MovieIMDB();
                if (!dtStMovie.Tables[0].Rows[0].IsNull("IMDBID"))
                    iMov.ImdbID = (string)dtStMovie.Tables[0].Rows[0]["IMDBID"];
                if (!dtStMovie.Tables[0].Rows[0].IsNull("Title"))
                    iMov.Title = dtStMovie.Tables[0].Rows[0]["Title"].ToString();
                if (!dtStMovie.Tables[0].Rows[0].IsNull("Language"))
                    iMov.Language = dtStMovie.Tables[0].Rows[0]["Language"].ToString();
                if (!dtStMovie.Tables[0].Rows[0].IsNull("Year"))
                    iMov.Year = dtStMovie.Tables[0].Rows[0]["Year"].ToString();
                if (!dtStMovie.Tables[0].Rows[0].IsNull("Rating"))
                    iMov.Rating = (dtStMovie.Tables[0].Rows[0]["Rating"]).ToString();
                if (!dtStMovie.Tables[0].Rows[0].IsNull("MyRating"))
                    iMov.MyRating = (dtStMovie.Tables[0].Rows[0]["MyRating"]).ToString();
                iMov.Genres = new ArrayList();
                foreach (string genre in dtStMovie.Tables[0].Rows[0]["Genre"].ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                {
                    iMov.Genres.Add(genre);
                }

                if (!dtStMovie.Tables[0].Rows[0].IsNull("MPAARating"))
                    iMov.MpaaRating = dtStMovie.Tables[0].Rows[0]["MPAARating"].ToString();
                if (!dtStMovie.Tables[0].Rows[0].IsNull("ReleaseDate"))
                    iMov.ReleaseDate = string.Format("{0:d MMMM yyyy}", Convert.ToDateTime(dtStMovie.Tables[0].Rows[0]["ReleaseDate"]));
                if (!dtStMovie.Tables[0].Rows[0].IsNull("Runtime"))
                    iMov.Runtime = dtStMovie.Tables[0].Rows[0]["Runtime"].ToString();
                if (!dtStMovie.Tables[0].Rows[0].IsNull("Tagline"))
                    iMov.Tagline = dtStMovie.Tables[0].Rows[0]["Tagline"].ToString();
                if (!dtStMovie.Tables[0].Rows[0].IsNull("Plot"))
                    iMov.Plot = dtStMovie.Tables[0].Rows[0]["Plot"].ToString();
                if (!dtStMovie.Tables[0].Rows[0].IsNull("Storyline"))
                    iMov.Storyline = dtStMovie.Tables[0].Rows[0]["Storyline"].ToString();
                if (!dtStMovie.Tables[0].Rows[0].IsNull("PosterURL"))
                    iMov.PosterURL = dtStMovie.Tables[0].Rows[0]["PosterURL"].ToString();
                if (!dtStMovie.Tables[0].Rows[0].IsNull("IMDBURL"))
                    iMov.ImdbURL = dtStMovie.Tables[0].Rows[0]["IMDBURL"].ToString();
                if (!dtStMovie.Tables[0].Rows[0].IsNull("Awards"))
                    iMov.Awards = dtStMovie.Tables[0].Rows[0]["Awards"].ToString();
                if (!dtStMovie.Tables[0].Rows[0].IsNull("Nominations"))
                    iMov.Nominations = dtStMovie.Tables[0].Rows[0]["Nominations"].ToString();
                if (!dtStMovie.Tables[0].Rows[0].IsNull("Top250"))
                    iMov.Top250 = dtStMovie.Tables[0].Rows[0]["Top250"].ToString();
                if (!dtStMovie.Tables[0].Rows[0].IsNull("Oscars"))
                    iMov.Oscars = dtStMovie.Tables[0].Rows[0]["Oscars"].ToString();

                if (!dtStMovie.Tables[0].Rows[0].IsNull("Poster"))
                {
                    MemoryStream stream = new MemoryStream((byte[])dtStMovie.Tables[0].Rows[0]["Poster"]);
                    if (stream.Capacity != 0)
                        iMov.Poster = Image.FromStream(stream);
                }

                iMov.Directors = new ArrayList();
                foreach (DataRow dr in dtStMovie.Tables[1].Rows)
                {
                    iMov.Directors.Add(dr.Field<string>("Director"));
                } iMov.Stars = new ArrayList();
                foreach (DataRow dr in dtStMovie.Tables[2].Rows)
                {
                    iMov.Stars.Add(dr.Field<string>("Star"));
                }

                iMov.Writers = new ArrayList();
                foreach (DataRow dr in dtStMovie.Tables[3].Rows)
                {
                    iMov.Writers.Add(dr.Field<string>("Writer"));
                }
                iMov.Cast = new ArrayList();
                foreach (DataRow dr in dtStMovie.Tables[4].Rows)
                {
                    iMov.Cast.Add(dr.Field<string>("Cast"));
                }
            }
            else
            {
                if (MessageBox.Show("Record not found in miniImdb for ID : " + ImdbID + Environment.NewLine + "Do you want to retrieve information from Internet?", "Connect to Internet?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    LoadMovieDataFromInternet();
                    btnDeleteMovie.Enabled = false;
                }
                else
                    this.Close();
            }
        }
        private void LoadMovieDataFromInternet()
        {
            Regex regex = new Regex(@"(\.)|(-)|(\[)|(\])|(\s{2,})");
            string movieNameForSearch = regex.Replace(movieName, match =>
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
            if (iOps.status == true)
            {
                iMov = iOps.iMov;
                iMov.Language = mov.Language;
            }
            else
            {
                this.Close();
                //MessageBox.Show("Error while getting information ! " + Environment.NewLine + iOps.errorMsg, "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadMovieDataFromInternet(string movieNameKeywords)
        {
            IMDBOperations iOps = new IMDBOperations(movieNameKeywords);
            if (iOps.status == true)
            {
                iMov = iOps.iMov;
                if (mov != null)
                    iMov.Language = mov.Language;
                DisplayIMovieData();
                btnAddMovie.Enabled = btnOpenInIE.Enabled = true;
            }
        }
        private void LoadMovieDataFromInternet(MovieIMDB movie)
        {
            IMDBOperations iOps = new IMDBOperations(movie);
            if (iOps.status == true)
            {
                iMov = iOps.iMov;
                DisplayIMovieData();
            }
        }
        private void DisplayIMovieData()
        {
            if (iMov.Poster != null)
                pictureBoxPoster.Image = iMov.Poster;
            if (iMov.Language != null)
            {
                comboBoxLanguage.Text = iMov.Language;
                this.Text = iMov.Language + " : " + iMov.Title;
            }
            else
            {
                comboBoxLanguage.Text = Language;
                this.Text = Language + " : " + iMov.Title;
            }
            textBoxTitle.Text = iMov.Title;
            textBoxYear.Text = iMov.Year;

            textBoxTagline.Text = iMov.Tagline;
            textBoxIMDBRating.Text = iMov.Rating;
            textBoxMyRating.Text = iMov.MyRating;
            listBoxGenre.Items.Clear();
            foreach (string genre in iMov.Genres)
            {
                listBoxGenre.Items.Add(genre);
            }

            textBoxImdbID.Text = iMov.ImdbID;
            textBoxMpaaRating.Text = iMov.MpaaRating;
            textBoxReleaseDate.Text = iMov.ReleaseDate;
            textBoxRuntime.Text = iMov.Runtime;

            textBoxAwards.Text = iMov.Awards;
            textBoxNominations.Text = iMov.Nominations;
            textBoxTop250.Text = iMov.Top250;
            textBoxOscars.Text = iMov.Oscars;

            StringBuilder Stars = new StringBuilder();
            foreach (string star in iMov.Stars)
            {
                Stars.Append(star + " , ");
            }
            if (iMov.Stars.Count != 0)
                Stars.Remove(Stars.Length - 2, 2);
            textBoxStars.Text = Stars.ToString();
            StringBuilder Directors = new StringBuilder();
            foreach (string dir in iMov.Directors)
            {
                Directors.Append(dir + " , ");
            }
            if (iMov.Directors.Count != 0)
                Directors.Remove(Directors.Length - 2, 2);
            textBoxDirectors.Text = Directors.ToString();
            StringBuilder Writers = new StringBuilder();
            foreach (string writer in iMov.Writers)
            {
                Writers.Append(writer + " , ");
            }
            if (iMov.Writers.Count != 0)
                Writers.Remove(Writers.Length - 2, 2);
            textBoxWriters.Text = Writers.ToString();
            listBoxCast.Items.Clear();
            foreach (string cast in iMov.Cast)
            {
                listBoxCast.Items.Add(cast);
            }

            richTextBoxPlot.Text = iMov.Plot;
            richTextBoxStoryline.Text = iMov.Storyline;
        }
        private void MovieIMDBDetails_Load(object sender, EventArgs e)
        {
            if ((iMov != null) && (mov != null && movieName != null || IsAvailableInDB))
            {
                DisplayIMovieData();
                speaker.Rate = 0;
                speaker.Volume = 100;
                speaker.SelectVoice("Microsoft Anna");
            }
        }
        #endregion

        private void MovieIMDBDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBoxPoster.Image = Image.FromFile(openFileDialog1.FileName);
                IsPosterUpdateRequired = true;
            }
        }
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pictureBoxPoster.Image = global::PlanetM.Properties.Resources.NoCover;
            IsPosterUpdateRequired = false;
        }
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //show a file save dialog and ensure the user selects correct file to allow save
            saveFileDialog1.Filter = "JPG Image (*.jpg)|*.jpg|Bitmap Image (.bmp)|*.bmp|Gif Image (.gif)|*.gif|Png Image (.png)|*.png|Tiff Image (.tiff)|*.tiff|Wmf Image (.wmf)|*.wmf";
            saveFileDialog1.DefaultExt = "jpg";
            saveFileDialog1.Title = "Save an Image File";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (!saveFileDialog1.FileName.Equals(String.Empty))
                {
                    FileInfo f = new FileInfo(saveFileDialog1.FileName);
                    if (f.Extension.Equals(".jpg") || f.Extension.Equals(".jpeg") || f.Extension.Equals(".jpe"))
                        pictureBoxPoster.Image.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    else if (f.Extension.Equals(".bmp"))
                        pictureBoxPoster.Image.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                    else if (f.Extension.Equals(".gif"))
                        pictureBoxPoster.Image.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Gif);
                    else if (f.Extension.Equals(".png"))
                        pictureBoxPoster.Image.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    else if (f.Extension.Equals(".tiff"))
                        pictureBoxPoster.Image.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Tiff);
                    else if (f.Extension.Equals(".wmf"))
                        pictureBoxPoster.Image.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Wmf);
                    else
                        MessageBox.Show("Invalid file type !!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("You did not pick a location to save file to !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnDeleteMovie_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete Movie : " + iMov.Title + " from MiniIMDB ?", "Delete Movie Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                if (dbObj.DeleteMovieFromIMDB(iMov.ImdbID))
                {
                    MessageBox.Show("Movie details has been deleted from Database", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                    MessageBox.Show("Error while deleting movie details from Database !", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadMovieDataFromForm()
        {
            iMov.Title = textBoxTitle.Text.Trim();
            iMov.Language = comboBoxLanguage.SelectedItem.ToString();
            iMov.Year = textBoxYear.Text.Trim();
            iMov.MpaaRating = textBoxMpaaRating.Text.Trim();
            iMov.ReleaseDate = textBoxReleaseDate.Text.Trim();
            iMov.Runtime = textBoxRuntime.Text.Trim();
            iMov.Awards = textBoxAwards.Text.Trim();
            iMov.Nominations = textBoxNominations.Text.Trim();
            iMov.Top250 = textBoxTop250.Text.Trim();
            iMov.Oscars = textBoxOscars.Text.Trim();
            iMov.Tagline = textBoxTagline.Text.Trim();
            iMov.Plot = richTextBoxPlot.Text.Trim();
            iMov.Storyline = richTextBoxStoryline.Text.Trim();
        }
        private void btnAddMovie_Click(object sender, EventArgs e)
        {
            if (mov != null && iMov != null && movieName != null)
            {
                if (textBoxTitle.Text.Trim().Length <= 0)
                {
                    MessageBox.Show("Please give a proper title to the movie !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (comboBoxLanguage.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select language for the movie !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                byte[] bytes = new byte[0];
                if (iMov.Poster != null || IsPosterUpdateRequired)
                {
                    MemoryStream stream = new MemoryStream();
                    //through the instruction below, we save the image to byte in the object "stream".
                    pictureBoxPoster.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    bytes = stream.ToArray();
                }
                LoadMovieDataFromForm();
                if (!dbObj.AddNewMovieIMDB(iMov, bytes))
                {
                    MessageBox.Show("Error while adding movie details in Database !", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                mov.Name = iMov.Title;
                mov.Language = iMov.Language;
                mov.IMDBID = iMov.ImdbID;
                if (iMov.Rating != "")
                    mov.IMDBRating = (float)Convert.ToDouble(iMov.Rating);
                mov.Year = Convert.ToInt32(iMov.Year);
                mov.Genre.Clear();
                foreach (string genre in iMov.Genres)
                {
                    mov.Genre.Add(genre);
                }
                if (dbObj.UpdateMovieFromIMDB(movieName, Language, mov, bytes))
                {
                    MessageBox.Show("Movie details has been added in Database", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error while adding movie details in Database !", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (IsAvailableInDB || iMov != null)
            {
                if (textBoxTitle.Text.Trim().Length <= 0)
                {
                    MessageBox.Show("Please give a proper title to the movie !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (comboBoxLanguage.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select language for the movie !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                byte[] bytes = new byte[0];
                if (iMov.Poster != null || IsPosterUpdateRequired)
                {
                    MemoryStream stream = new MemoryStream();
                    //through the instruction below, we save the image to byte in the object "stream".
                    pictureBoxPoster.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    bytes = stream.ToArray();
                }
                LoadMovieDataFromForm();
                if (dbObj.AddNewMovieIMDB(iMov, bytes))
                {
                    MessageBox.Show("Movie details has been added in Database", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                    MessageBox.Show("Error while adding movie details in Database !", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRetrieveMovieFromIMDB_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to retrieve information from Internet?", "Connect to Internet?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                Cursor = Cursors.WaitCursor;
                if (mov == null && iMov == null)
                {
                    string strMovieSearchKeywords = Interaction.InputBox("Please enter keywords for movie search :", "Search Movie").Trim();
                    if (strMovieSearchKeywords.Length >= 1)
                    {
                        LoadMovieDataFromInternet(strMovieSearchKeywords);
                    }
                    else
                        MessageBox.Show("Please enter proper movie keywords !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (mov != null)
                {
                    LoadMovieDataFromInternet(mov.Name);
                }
                else if (iMov != null)
                {
                    LoadMovieDataFromInternet(iMov);
                }
                Cursor = Cursors.Default;

            }
        }

        private void btnOpenInIE_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to browse information on Internet?", "Open in Brouser?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                Process.Start("IExplore.exe", iMov.ImdbURL);
        }

        private void buttonSpeak_Click(object sender, EventArgs e)
        {
            speaker.SpeakAsyncCancelAll();
            speaker.SpeakAsync(richTextBoxStoryline.Text);
        }

        private void MovieIMDBDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            speaker.SpeakAsyncCancelAll();
        }

        private void btnBackToPlanetM_Click(object sender, EventArgs e)
        {
            if (textBoxImdbID.Text.Length > 5)
            {
                MovieDetails mov = new MovieDetails(textBoxImdbID.Text.Trim());
                if (!mov.IsDisposed)
                {
                    mov.Show();
                    this.Close();
                }
            }
        }
    }
}
