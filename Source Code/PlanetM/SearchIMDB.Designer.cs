namespace PlanetM
{
    partial class SearchIMDB
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchIMDB));
            this.buttonSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxMainSerach = new System.Windows.Forms.GroupBox();
            this.comboBoxSearchType = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBoxOscars = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxTop250 = new System.Windows.Forms.ComboBox();
            this.comboBoxMpaaRating = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.checkedListBoxGenre = new System.Windows.Forms.CheckedListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxRating = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxYear = new System.Windows.Forms.ComboBox();
            this.comboBoxLanguage = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxKeywords = new System.Windows.Forms.TextBox();
            this.groupBoxExport = new System.Windows.Forms.GroupBox();
            this.buttonTXT = new System.Windows.Forms.Button();
            this.buttonCSV = new System.Windows.Forms.Button();
            this.buttonPDF = new System.Windows.Forms.Button();
            this.buttonExcel = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.dgMovies = new System.Windows.Forms.DataGridView();
            this.contextMenuStripMovieGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openMovieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteMoviesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.labelResults = new System.Windows.Forms.Label();
            this.groupBoxMainSerach.SuspendLayout();
            this.groupBoxExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMovies)).BeginInit();
            this.contextMenuStripMovieGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(983, 140);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(75, 45);
            this.buttonSearch.TabIndex = 9;
            this.buttonSearch.Text = "Go Find !";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(281, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 100;
            this.label1.Text = "Enter Keywords";
            // 
            // groupBoxMainSerach
            // 
            this.groupBoxMainSerach.Controls.Add(this.comboBoxSearchType);
            this.groupBoxMainSerach.Controls.Add(this.label10);
            this.groupBoxMainSerach.Controls.Add(this.comboBoxOscars);
            this.groupBoxMainSerach.Controls.Add(this.label9);
            this.groupBoxMainSerach.Controls.Add(this.label7);
            this.groupBoxMainSerach.Controls.Add(this.comboBoxTop250);
            this.groupBoxMainSerach.Controls.Add(this.comboBoxMpaaRating);
            this.groupBoxMainSerach.Controls.Add(this.label4);
            this.groupBoxMainSerach.Controls.Add(this.label8);
            this.groupBoxMainSerach.Controls.Add(this.checkedListBoxGenre);
            this.groupBoxMainSerach.Controls.Add(this.label5);
            this.groupBoxMainSerach.Controls.Add(this.comboBoxRating);
            this.groupBoxMainSerach.Controls.Add(this.label3);
            this.groupBoxMainSerach.Controls.Add(this.comboBoxYear);
            this.groupBoxMainSerach.Controls.Add(this.comboBoxLanguage);
            this.groupBoxMainSerach.Controls.Add(this.label2);
            this.groupBoxMainSerach.Controls.Add(this.textBoxKeywords);
            this.groupBoxMainSerach.Controls.Add(this.label1);
            this.groupBoxMainSerach.Location = new System.Drawing.Point(12, 12);
            this.groupBoxMainSerach.Name = "groupBoxMainSerach";
            this.groupBoxMainSerach.Size = new System.Drawing.Size(750, 179);
            this.groupBoxMainSerach.TabIndex = 2;
            this.groupBoxMainSerach.TabStop = false;
            this.groupBoxMainSerach.Text = "IMDB Search Criteria";
            // 
            // comboBoxSearchType
            // 
            this.comboBoxSearchType.FormattingEnabled = true;
            this.comboBoxSearchType.Location = new System.Drawing.Point(88, 26);
            this.comboBoxSearchType.Name = "comboBoxSearchType";
            this.comboBoxSearchType.Size = new System.Drawing.Size(135, 21);
            this.comboBoxSearchType.TabIndex = 108;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 13);
            this.label10.TabIndex = 107;
            this.label10.Text = "Search By";
            // 
            // comboBoxOscars
            // 
            this.comboBoxOscars.FormattingEnabled = true;
            this.comboBoxOscars.Location = new System.Drawing.Point(368, 136);
            this.comboBoxOscars.Name = "comboBoxOscars";
            this.comboBoxOscars.Size = new System.Drawing.Size(121, 21);
            this.comboBoxOscars.TabIndex = 106;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(281, 139);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 105;
            this.label9.Text = "Oscars";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 144);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 13);
            this.label7.TabIndex = 104;
            this.label7.Text = "Is Top250 ?";
            // 
            // comboBoxTop250
            // 
            this.comboBoxTop250.FormattingEnabled = true;
            this.comboBoxTop250.Location = new System.Drawing.Point(88, 136);
            this.comboBoxTop250.Name = "comboBoxTop250";
            this.comboBoxTop250.Size = new System.Drawing.Size(135, 21);
            this.comboBoxTop250.TabIndex = 103;
            // 
            // comboBoxMpaaRating
            // 
            this.comboBoxMpaaRating.FormattingEnabled = true;
            this.comboBoxMpaaRating.Location = new System.Drawing.Point(368, 99);
            this.comboBoxMpaaRating.Name = "comboBoxMpaaRating";
            this.comboBoxMpaaRating.Size = new System.Drawing.Size(121, 21);
            this.comboBoxMpaaRating.TabIndex = 102;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(281, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 101;
            this.label4.Text = "MPAA Rating";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 102);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Rating";
            // 
            // checkedListBoxGenre
            // 
            this.checkedListBoxGenre.FormattingEnabled = true;
            this.checkedListBoxGenre.Location = new System.Drawing.Point(599, 61);
            this.checkedListBoxGenre.Name = "checkedListBoxGenre";
            this.checkedListBoxGenre.Size = new System.Drawing.Size(131, 109);
            this.checkedListBoxGenre.TabIndex = 8;
            this.checkedListBoxGenre.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxGenre_ItemCheck);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(539, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Genre";
            // 
            // comboBoxRating
            // 
            this.comboBoxRating.FormattingEnabled = true;
            this.comboBoxRating.Location = new System.Drawing.Point(88, 99);
            this.comboBoxRating.Name = "comboBoxRating";
            this.comboBoxRating.Size = new System.Drawing.Size(135, 21);
            this.comboBoxRating.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(281, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Year";
            // 
            // comboBoxYear
            // 
            this.comboBoxYear.FormattingEnabled = true;
            this.comboBoxYear.Location = new System.Drawing.Point(368, 61);
            this.comboBoxYear.Name = "comboBoxYear";
            this.comboBoxYear.Size = new System.Drawing.Size(121, 21);
            this.comboBoxYear.TabIndex = 3;
            // 
            // comboBoxLanguage
            // 
            this.comboBoxLanguage.FormattingEnabled = true;
            this.comboBoxLanguage.Location = new System.Drawing.Point(88, 61);
            this.comboBoxLanguage.Name = "comboBoxLanguage";
            this.comboBoxLanguage.Size = new System.Drawing.Size(135, 21);
            this.comboBoxLanguage.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Language";
            // 
            // textBoxKeywords
            // 
            this.textBoxKeywords.Location = new System.Drawing.Point(368, 26);
            this.textBoxKeywords.Name = "textBoxKeywords";
            this.textBoxKeywords.Size = new System.Drawing.Size(362, 20);
            this.textBoxKeywords.TabIndex = 1;
            this.textBoxKeywords.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxName_KeyPress);
            // 
            // groupBoxExport
            // 
            this.groupBoxExport.Controls.Add(this.buttonTXT);
            this.groupBoxExport.Controls.Add(this.buttonCSV);
            this.groupBoxExport.Controls.Add(this.buttonPDF);
            this.groupBoxExport.Controls.Add(this.buttonExcel);
            this.groupBoxExport.Enabled = false;
            this.groupBoxExport.Location = new System.Drawing.Point(792, 12);
            this.groupBoxExport.Name = "groupBoxExport";
            this.groupBoxExport.Size = new System.Drawing.Size(259, 115);
            this.groupBoxExport.TabIndex = 101;
            this.groupBoxExport.TabStop = false;
            this.groupBoxExport.Text = "Export";
            // 
            // buttonTXT
            // 
            this.buttonTXT.Location = new System.Drawing.Point(106, 53);
            this.buttonTXT.Name = "buttonTXT";
            this.buttonTXT.Size = new System.Drawing.Size(75, 23);
            this.buttonTXT.TabIndex = 3;
            this.buttonTXT.Text = "Text";
            this.buttonTXT.UseVisualStyleBackColor = true;
            this.buttonTXT.Click += new System.EventHandler(this.buttonTXT_Click);
            // 
            // buttonCSV
            // 
            this.buttonCSV.Location = new System.Drawing.Point(11, 53);
            this.buttonCSV.Name = "buttonCSV";
            this.buttonCSV.Size = new System.Drawing.Size(75, 23);
            this.buttonCSV.TabIndex = 2;
            this.buttonCSV.Text = "CSV";
            this.buttonCSV.UseVisualStyleBackColor = true;
            this.buttonCSV.Click += new System.EventHandler(this.buttonCSV_Click);
            // 
            // buttonPDF
            // 
            this.buttonPDF.Location = new System.Drawing.Point(106, 19);
            this.buttonPDF.Name = "buttonPDF";
            this.buttonPDF.Size = new System.Drawing.Size(75, 23);
            this.buttonPDF.TabIndex = 1;
            this.buttonPDF.Text = "PDF";
            this.buttonPDF.UseVisualStyleBackColor = true;
            this.buttonPDF.Click += new System.EventHandler(this.buttonPDF_Click);
            // 
            // buttonExcel
            // 
            this.buttonExcel.Location = new System.Drawing.Point(11, 19);
            this.buttonExcel.Name = "buttonExcel";
            this.buttonExcel.Size = new System.Drawing.Size(75, 23);
            this.buttonExcel.TabIndex = 0;
            this.buttonExcel.Text = "Excel";
            this.buttonExcel.UseVisualStyleBackColor = true;
            this.buttonExcel.Click += new System.EventHandler(this.buttonExcel_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(789, 170);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 15);
            this.label6.TabIndex = 11;
            this.label6.Text = "Results Found :";
            // 
            // dgMovies
            // 
            this.dgMovies.AllowUserToAddRows = false;
            this.dgMovies.AllowUserToDeleteRows = false;
            this.dgMovies.AllowUserToOrderColumns = true;
            this.dgMovies.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgMovies.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgMovies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgMovies.ContextMenuStrip = this.contextMenuStripMovieGrid;
            this.dgMovies.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgMovies.Location = new System.Drawing.Point(12, 199);
            this.dgMovies.Name = "dgMovies";
            this.dgMovies.RowHeadersVisible = false;
            this.dgMovies.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgMovies.Size = new System.Drawing.Size(1046, 333);
            this.dgMovies.TabIndex = 3;
            this.dgMovies.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMovies_CellDoubleClick);
            // 
            // contextMenuStripMovieGrid
            // 
            this.contextMenuStripMovieGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openMovieToolStripMenuItem,
            this.deleteMoviesToolStripMenuItem});
            this.contextMenuStripMovieGrid.Name = "contextMenuStripMovieGrid";
            this.contextMenuStripMovieGrid.Size = new System.Drawing.Size(144, 48);
            // 
            // openMovieToolStripMenuItem
            // 
            this.openMovieToolStripMenuItem.Name = "openMovieToolStripMenuItem";
            this.openMovieToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.openMovieToolStripMenuItem.Text = "Open Movie";
            this.openMovieToolStripMenuItem.Click += new System.EventHandler(this.openMovieToolStripMenuItem_Click);
            // 
            // deleteMoviesToolStripMenuItem
            // 
            this.deleteMoviesToolStripMenuItem.Name = "deleteMoviesToolStripMenuItem";
            this.deleteMoviesToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.deleteMoviesToolStripMenuItem.Text = "Delete Movie";
            this.deleteMoviesToolStripMenuItem.Click += new System.EventHandler(this.deleteMoviesToolStripMenuItem_Click);
            // 
            // labelResults
            // 
            this.labelResults.AutoSize = true;
            this.labelResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelResults.Location = new System.Drawing.Point(902, 170);
            this.labelResults.Name = "labelResults";
            this.labelResults.Size = new System.Drawing.Size(0, 15);
            this.labelResults.TabIndex = 102;
            // 
            // SearchIMDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1074, 545);
            this.Controls.Add(this.labelResults);
            this.Controls.Add(this.dgMovies);
            this.Controls.Add(this.groupBoxMainSerach);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.groupBoxExport);
            this.Controls.Add(this.label6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SearchIMDB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "iSearch";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Search_FormClosing);
            this.groupBoxMainSerach.ResumeLayout(false);
            this.groupBoxMainSerach.PerformLayout();
            this.groupBoxExport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgMovies)).EndInit();
            this.contextMenuStripMovieGrid.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxMainSerach;
        private System.Windows.Forms.ComboBox comboBoxLanguage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxKeywords;
        private System.Windows.Forms.DataGridView dgMovies;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxYear;
        private System.Windows.Forms.CheckedListBox checkedListBoxGenre;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxRating;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMovieGrid;
        private System.Windows.Forms.ToolStripMenuItem openMovieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteMoviesToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBoxExport;
        private System.Windows.Forms.Button buttonTXT;
        private System.Windows.Forms.Button buttonCSV;
        private System.Windows.Forms.Button buttonPDF;
        private System.Windows.Forms.Button buttonExcel;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Label labelResults;
        private System.Windows.Forms.ComboBox comboBoxOscars;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxTop250;
        private System.Windows.Forms.ComboBox comboBoxMpaaRating;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxSearchType;
        private System.Windows.Forms.Label label10;
    }
}