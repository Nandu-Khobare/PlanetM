namespace PlanetM.Forms
{
    partial class DiscrepancyReportsIMDBViewer
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
            this.dgMovies = new System.Windows.Forms.DataGridView();
            this.IMDBID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Language = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Year = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rating = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Oscars = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Genre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStripMovieGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openMovieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteMoviesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxDiscReportType = new System.Windows.Forms.ComboBox();
            this.labelDiscrepancyCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgMovies)).BeginInit();
            this.contextMenuStripMovieGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgMovies
            // 
            this.dgMovies.AllowUserToAddRows = false;
            this.dgMovies.AllowUserToDeleteRows = false;
            this.dgMovies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgMovies.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IMDBID,
            this.Title,
            this.Language,
            this.Year,
            this.Rating,
            this.Oscars,
            this.Genre});
            this.dgMovies.ContextMenuStrip = this.contextMenuStripMovieGrid;
            this.dgMovies.Location = new System.Drawing.Point(12, 48);
            this.dgMovies.Name = "dgMovies";
            this.dgMovies.RowHeadersVisible = false;
            this.dgMovies.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgMovies.Size = new System.Drawing.Size(876, 396);
            this.dgMovies.TabIndex = 0;
            this.dgMovies.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMovies_CellDoubleClick);
            // 
            // IMDBID
            // 
            this.IMDBID.HeaderText = "IMDBID";
            this.IMDBID.Name = "IMDBID";
            this.IMDBID.Visible = false;
            // 
            // Title
            // 
            this.Title.HeaderText = "Title";
            this.Title.Name = "Title";
            this.Title.Width = 325;
            // 
            // Language
            // 
            this.Language.HeaderText = "Language";
            this.Language.Name = "Language";
            // 
            // Year
            // 
            this.Year.HeaderText = "Year";
            this.Year.Name = "Year";
            this.Year.Width = 50;
            // 
            // Rating
            // 
            this.Rating.HeaderText = "Rating";
            this.Rating.Name = "Rating";
            this.Rating.Width = 50;
            // 
            // Oscars
            // 
            this.Oscars.HeaderText = "Oscars";
            this.Oscars.Name = "Oscars";
            this.Oscars.Width = 50;
            // 
            // Genre
            // 
            this.Genre.HeaderText = "Genre";
            this.Genre.Name = "Genre";
            this.Genre.Width = 280;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Report For :";
            // 
            // comboBoxDiscReportType
            // 
            this.comboBoxDiscReportType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxDiscReportType.FormattingEnabled = true;
            this.comboBoxDiscReportType.Location = new System.Drawing.Point(103, 12);
            this.comboBoxDiscReportType.Name = "comboBoxDiscReportType";
            this.comboBoxDiscReportType.Size = new System.Drawing.Size(446, 24);
            this.comboBoxDiscReportType.TabIndex = 2;
            this.comboBoxDiscReportType.SelectedIndexChanged += new System.EventHandler(this.comboBoxDiscReportType_SelectedIndexChanged);
            // 
            // labelDiscrepancyCount
            // 
            this.labelDiscrepancyCount.AutoSize = true;
            this.labelDiscrepancyCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDiscrepancyCount.ForeColor = System.Drawing.Color.Red;
            this.labelDiscrepancyCount.Location = new System.Drawing.Point(589, 15);
            this.labelDiscrepancyCount.Name = "labelDiscrepancyCount";
            this.labelDiscrepancyCount.Size = new System.Drawing.Size(145, 13);
            this.labelDiscrepancyCount.TabIndex = 3;
            this.labelDiscrepancyCount.Text = "Discrepancies Found : 0";
            // 
            // DiscrepancyReportsIMDBViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 455);
            this.Controls.Add(this.dgMovies);
            this.Controls.Add(this.comboBoxDiscReportType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelDiscrepancyCount);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "DiscrepancyReportsIMDBViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Discrepancy Reports - MiniIMDB";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DiscrepancyReportsIMDBViewer_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgMovies)).EndInit();
            this.contextMenuStripMovieGrid.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgMovies;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMovieGrid;
        private System.Windows.Forms.ToolStripMenuItem openMovieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteMoviesToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxDiscReportType;
        private System.Windows.Forms.Label labelDiscrepancyCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn IMDBID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn Language;
        private System.Windows.Forms.DataGridViewTextBoxColumn Year;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rating;
        private System.Windows.Forms.DataGridViewTextBoxColumn Oscars;
        private System.Windows.Forms.DataGridViewTextBoxColumn Genre;
    }
}