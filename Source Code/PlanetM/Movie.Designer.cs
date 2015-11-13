namespace PlanetM
{
    partial class MovieDetails
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MovieDetails));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxIsWatched = new System.Windows.Forms.CheckBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.panelPoster = new System.Windows.Forms.Panel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.pictureBoxCover = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.linkLabelIMDB = new System.Windows.Forms.LinkLabel();
            this.buttonShowIMDB = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxLanguage = new System.Windows.Forms.ComboBox();
            this.comboBoxYear = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBoxIMDBID = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxSize = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBoxRating = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkedListBoxGenre = new System.Windows.Forms.CheckedListBox();
            this.textBoxQuality = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxIMDBRating = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label13 = new System.Windows.Forms.Label();
            this.linkLabelMergeMovie = new System.Windows.Forms.LinkLabel();
            this.buttonUpdateChecksum = new System.Windows.Forms.Button();
            this.buttonDeleteLocation = new System.Windows.Forms.Button();
            this.listBoxChecksum = new System.Windows.Forms.ListBox();
            this.listBoxLocations = new System.Windows.Forms.ListBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.panelAvailableOnLocations = new System.Windows.Forms.Panel();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.btnPlayMovie = new System.Windows.Forms.Button();
            this.btnDeleteMovie = new System.Windows.Forms.Button();
            this.btnUpdateMovie = new System.Windows.Forms.Button();
            this.panelPoster.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCover)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Language";
            // 
            // checkBoxIsWatched
            // 
            this.checkBoxIsWatched.AutoSize = true;
            this.checkBoxIsWatched.Location = new System.Drawing.Point(20, 113);
            this.checkBoxIsWatched.Name = "checkBoxIsWatched";
            this.checkBoxIsWatched.Size = new System.Drawing.Size(173, 17);
            this.checkBoxIsWatched.TabIndex = 3;
            this.checkBoxIsWatched.Text = "Have you watched this Movie?";
            this.checkBoxIsWatched.UseVisualStyleBackColor = true;
            this.checkBoxIsWatched.CheckedChanged += new System.EventHandler(this.checkBoxIsWatched_CheckedChanged);
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(109, 25);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(299, 20);
            this.textBoxName.TabIndex = 7;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Title = "Select Cover Image";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(9, 197);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(73, 13);
            this.linkLabel1.TabIndex = 10;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Update Cover";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(247, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Year";
            // 
            // panelPoster
            // 
            this.panelPoster.Controls.Add(this.linkLabel2);
            this.panelPoster.Controls.Add(this.pictureBoxCover);
            this.panelPoster.Controls.Add(this.linkLabel1);
            this.panelPoster.Location = new System.Drawing.Point(58, 12);
            this.panelPoster.Name = "panelPoster";
            this.panelPoster.Size = new System.Drawing.Size(185, 219);
            this.panelPoster.TabIndex = 12;
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(88, 197);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(78, 13);
            this.linkLabel2.TabIndex = 11;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Remove Cover";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // pictureBoxCover
            // 
            this.pictureBoxCover.Location = new System.Drawing.Point(28, 13);
            this.pictureBoxCover.Name = "pictureBoxCover";
            this.pictureBoxCover.Size = new System.Drawing.Size(130, 180);
            this.pictureBoxCover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxCover.TabIndex = 5;
            this.pictureBoxCover.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 237);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(426, 232);
            this.tabControl1.TabIndex = 13;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.linkLabelIMDB);
            this.tabPage1.Controls.Add(this.buttonShowIMDB);
            this.tabPage1.Controls.Add(this.dateTimePicker1);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.comboBoxLanguage);
            this.tabPage1.Controls.Add(this.comboBoxYear);
            this.tabPage1.Controls.Add(this.textBoxName);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.checkBoxIsWatched);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(418, 206);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Movie";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // linkLabelIMDB
            // 
            this.linkLabelIMDB.AutoSize = true;
            this.linkLabelIMDB.Location = new System.Drawing.Point(356, 186);
            this.linkLabelIMDB.Name = "linkLabelIMDB";
            this.linkLabelIMDB.Size = new System.Drawing.Size(61, 13);
            this.linkLabelIMDB.TabIndex = 18;
            this.linkLabelIMDB.TabStop = true;
            this.linkLabelIMDB.Text = "Sync IMDB";
            this.linkLabelIMDB.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelIMDB_LinkClicked);
            // 
            // buttonShowIMDB
            // 
            this.buttonShowIMDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonShowIMDB.Image = global::PlanetM.Properties.Resources.IMDB;
            this.buttonShowIMDB.Location = new System.Drawing.Point(291, 95);
            this.buttonShowIMDB.Name = "buttonShowIMDB";
            this.buttonShowIMDB.Size = new System.Drawing.Size(80, 90);
            this.buttonShowIMDB.TabIndex = 17;
            this.buttonShowIMDB.Text = "~IMDB~";
            this.buttonShowIMDB.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonShowIMDB.UseVisualStyleBackColor = true;
            this.buttonShowIMDB.Click += new System.EventHandler(this.buttonShowIMDB_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(113, 144);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(117, 20);
            this.dateTimePicker1.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 150);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "When?";
            // 
            // comboBoxLanguage
            // 
            this.comboBoxLanguage.FormattingEnabled = true;
            this.comboBoxLanguage.Location = new System.Drawing.Point(109, 65);
            this.comboBoxLanguage.Name = "comboBoxLanguage";
            this.comboBoxLanguage.Size = new System.Drawing.Size(121, 21);
            this.comboBoxLanguage.TabIndex = 13;
            // 
            // comboBoxYear
            // 
            this.comboBoxYear.FormattingEnabled = true;
            this.comboBoxYear.Location = new System.Drawing.Point(291, 65);
            this.comboBoxYear.Name = "comboBoxYear";
            this.comboBoxYear.Size = new System.Drawing.Size(117, 21);
            this.comboBoxYear.TabIndex = 12;
            this.comboBoxYear.SelectedIndexChanged += new System.EventHandler(this.comboBoxYear_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBoxIMDBID);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.textBoxSize);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.comboBoxRating);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.checkedListBoxGenre);
            this.tabPage2.Controls.Add(this.textBoxQuality);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.textBoxIMDBRating);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(418, 206);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Details";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBoxIMDBID
            // 
            this.textBoxIMDBID.Enabled = false;
            this.textBoxIMDBID.Location = new System.Drawing.Point(86, 19);
            this.textBoxIMDBID.Name = "textBoxIMDBID";
            this.textBoxIMDBID.Size = new System.Drawing.Size(100, 20);
            this.textBoxIMDBID.TabIndex = 14;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(48, 13);
            this.label12.TabIndex = 13;
            this.label12.Text = "IMDB ID";
            // 
            // textBoxSize
            // 
            this.textBoxSize.Enabled = false;
            this.textBoxSize.Location = new System.Drawing.Point(86, 161);
            this.textBoxSize.Name = "textBoxSize";
            this.textBoxSize.Size = new System.Drawing.Size(100, 20);
            this.textBoxSize.TabIndex = 12;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 164);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(27, 13);
            this.label11.TabIndex = 11;
            this.label11.Text = "Size";
            // 
            // comboBoxRating
            // 
            this.comboBoxRating.FormattingEnabled = true;
            this.comboBoxRating.Location = new System.Drawing.Point(86, 85);
            this.comboBoxRating.MaxDropDownItems = 5;
            this.comboBoxRating.Name = "comboBoxRating";
            this.comboBoxRating.Size = new System.Drawing.Size(100, 21);
            this.comboBoxRating.TabIndex = 9;
            this.comboBoxRating.SelectedIndexChanged += new System.EventHandler(this.comboBoxRating_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Your Rating";
            // 
            // checkedListBoxGenre
            // 
            this.checkedListBoxGenre.FormattingEnabled = true;
            this.checkedListBoxGenre.Location = new System.Drawing.Point(248, 14);
            this.checkedListBoxGenre.Name = "checkedListBoxGenre";
            this.checkedListBoxGenre.Size = new System.Drawing.Size(150, 169);
            this.checkedListBoxGenre.TabIndex = 6;
            // 
            // textBoxQuality
            // 
            this.textBoxQuality.Location = new System.Drawing.Point(86, 125);
            this.textBoxQuality.Name = "textBoxQuality";
            this.textBoxQuality.Size = new System.Drawing.Size(100, 20);
            this.textBoxQuality.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(206, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Genre";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 128);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Quality";
            // 
            // textBoxIMDBRating
            // 
            this.textBoxIMDBRating.Enabled = false;
            this.textBoxIMDBRating.Location = new System.Drawing.Point(86, 53);
            this.textBoxIMDBRating.Name = "textBoxIMDBRating";
            this.textBoxIMDBRating.Size = new System.Drawing.Size(100, 20);
            this.textBoxIMDBRating.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "IMDB Rating";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Controls.Add(this.linkLabelMergeMovie);
            this.tabPage3.Controls.Add(this.buttonUpdateChecksum);
            this.tabPage3.Controls.Add(this.buttonDeleteLocation);
            this.tabPage3.Controls.Add(this.listBoxChecksum);
            this.tabPage3.Controls.Add(this.listBoxLocations);
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(418, 206);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Misc.";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 120);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(33, 13);
            this.label13.TabIndex = 7;
            this.label13.Text = "Size :";
            // 
            // linkLabelMergeMovie
            // 
            this.linkLabelMergeMovie.AutoSize = true;
            this.linkLabelMergeMovie.Location = new System.Drawing.Point(352, 8);
            this.linkLabelMergeMovie.Name = "linkLabelMergeMovie";
            this.linkLabelMergeMovie.Size = new System.Drawing.Size(66, 13);
            this.linkLabelMergeMovie.TabIndex = 6;
            this.linkLabelMergeMovie.TabStop = true;
            this.linkLabelMergeMovie.Text = "MergeMovie";
            this.linkLabelMergeMovie.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelMergeMovie_LinkClicked);
            // 
            // buttonUpdateChecksum
            // 
            this.buttonUpdateChecksum.Location = new System.Drawing.Point(236, 3);
            this.buttonUpdateChecksum.Name = "buttonUpdateChecksum";
            this.buttonUpdateChecksum.Size = new System.Drawing.Size(110, 23);
            this.buttonUpdateChecksum.TabIndex = 5;
            this.buttonUpdateChecksum.Text = "Update Checksum";
            this.buttonUpdateChecksum.UseVisualStyleBackColor = true;
            this.buttonUpdateChecksum.Click += new System.EventHandler(this.buttonUpdateChecksum_Click);
            // 
            // buttonDeleteLocation
            // 
            this.buttonDeleteLocation.Location = new System.Drawing.Point(130, 3);
            this.buttonDeleteLocation.Name = "buttonDeleteLocation";
            this.buttonDeleteLocation.Size = new System.Drawing.Size(100, 23);
            this.buttonDeleteLocation.TabIndex = 4;
            this.buttonDeleteLocation.Text = "Delete Location";
            this.buttonDeleteLocation.UseVisualStyleBackColor = true;
            this.buttonDeleteLocation.Click += new System.EventHandler(this.buttonDeleteLocation_Click);
            // 
            // listBoxChecksum
            // 
            this.listBoxChecksum.FormattingEnabled = true;
            this.listBoxChecksum.Location = new System.Drawing.Point(6, 142);
            this.listBoxChecksum.Name = "listBoxChecksum";
            this.listBoxChecksum.Size = new System.Drawing.Size(409, 56);
            this.listBoxChecksum.TabIndex = 3;
            // 
            // listBoxLocations
            // 
            this.listBoxLocations.FormattingEnabled = true;
            this.listBoxLocations.HorizontalScrollbar = true;
            this.listBoxLocations.Location = new System.Drawing.Point(6, 31);
            this.listBoxLocations.Name = "listBoxLocations";
            this.listBoxLocations.Size = new System.Drawing.Size(409, 82);
            this.listBoxLocations.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(103, 120);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Checksum";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 6);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Locations";
            // 
            // panelAvailableOnLocations
            // 
            this.panelAvailableOnLocations.Location = new System.Drawing.Point(20, 25);
            this.panelAvailableOnLocations.Name = "panelAvailableOnLocations";
            this.panelAvailableOnLocations.Size = new System.Drawing.Size(45, 206);
            this.panelAvailableOnLocations.TabIndex = 17;
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Image = global::PlanetM.Properties.Resources.Open;
            this.btnOpenFolder.Location = new System.Drawing.Point(350, 119);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(83, 86);
            this.btnOpenFolder.TabIndex = 16;
            this.btnOpenFolder.Text = "Open Folder";
            this.btnOpenFolder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
            // 
            // btnPlayMovie
            // 
            this.btnPlayMovie.Image = global::PlanetM.Properties.Resources.Play;
            this.btnPlayMovie.Location = new System.Drawing.Point(350, 26);
            this.btnPlayMovie.Name = "btnPlayMovie";
            this.btnPlayMovie.Size = new System.Drawing.Size(83, 86);
            this.btnPlayMovie.TabIndex = 15;
            this.btnPlayMovie.Text = "Play Movie";
            this.btnPlayMovie.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPlayMovie.UseVisualStyleBackColor = true;
            this.btnPlayMovie.Click += new System.EventHandler(this.btnPlayMovie_Click);
            // 
            // btnDeleteMovie
            // 
            this.btnDeleteMovie.Image = global::PlanetM.Properties.Resources.Remove;
            this.btnDeleteMovie.Location = new System.Drawing.Point(261, 119);
            this.btnDeleteMovie.Name = "btnDeleteMovie";
            this.btnDeleteMovie.Size = new System.Drawing.Size(83, 87);
            this.btnDeleteMovie.TabIndex = 14;
            this.btnDeleteMovie.Text = "Delete Movie";
            this.btnDeleteMovie.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDeleteMovie.UseVisualStyleBackColor = true;
            this.btnDeleteMovie.Click += new System.EventHandler(this.btnDeleteMovie_Click);
            // 
            // btnUpdateMovie
            // 
            this.btnUpdateMovie.Image = global::PlanetM.Properties.Resources.Update;
            this.btnUpdateMovie.Location = new System.Drawing.Point(261, 26);
            this.btnUpdateMovie.Name = "btnUpdateMovie";
            this.btnUpdateMovie.Size = new System.Drawing.Size(83, 86);
            this.btnUpdateMovie.TabIndex = 4;
            this.btnUpdateMovie.Text = "Update Movie";
            this.btnUpdateMovie.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnUpdateMovie.UseVisualStyleBackColor = true;
            this.btnUpdateMovie.Click += new System.EventHandler(this.btnUpdateMovie_Click);
            // 
            // MovieDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 481);
            this.Controls.Add(this.panelAvailableOnLocations);
            this.Controls.Add(this.btnOpenFolder);
            this.Controls.Add(this.btnPlayMovie);
            this.Controls.Add(this.btnDeleteMovie);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panelPoster);
            this.Controls.Add(this.btnUpdateMovie);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "MovieDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Movie";
            this.Load += new System.EventHandler(this.fmMovie_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MovieDetails_KeyDown);
            this.panelPoster.ResumeLayout(false);
            this.panelPoster.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCover)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxIsWatched;
        private System.Windows.Forms.Button btnUpdateMovie;
        private System.Windows.Forms.PictureBox pictureBoxCover;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelPoster;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ComboBox comboBoxLanguage;
        private System.Windows.Forms.ComboBox comboBoxYear;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxIMDBRating;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxQuality;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox listBoxChecksum;
        private System.Windows.Forms.ListBox listBoxLocations;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.CheckedListBox checkedListBoxGenre;
        private System.Windows.Forms.ComboBox comboBoxRating;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxSize;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnDeleteMovie;
        private System.Windows.Forms.Button buttonDeleteLocation;
        private System.Windows.Forms.Button buttonUpdateChecksum;
        private System.Windows.Forms.Button btnPlayMovie;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.TextBox textBoxIMDBID;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button buttonShowIMDB;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.LinkLabel linkLabelIMDB;
        private System.Windows.Forms.LinkLabel linkLabelMergeMovie;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panelAvailableOnLocations;
    }
}