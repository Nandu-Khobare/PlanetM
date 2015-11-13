namespace PlanetM.Forms
{
    partial class WatchlistViewer
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
            this.MovieName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Language = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Year = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ImdbRating = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Genre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AddedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStripMovieGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openMovieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteMoviesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.markAsSeenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.MovieName,
            this.Language,
            this.Year,
            this.ImdbRating,
            this.Genre,
            this.AddedDate});
            this.dgMovies.ContextMenuStrip = this.contextMenuStripMovieGrid;
            this.dgMovies.Location = new System.Drawing.Point(12, 12);
            this.dgMovies.Name = "dgMovies";
            this.dgMovies.RowHeadersVisible = false;
            this.dgMovies.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgMovies.Size = new System.Drawing.Size(824, 344);
            this.dgMovies.TabIndex = 0;
            this.dgMovies.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMovies_CellDoubleClick);
            // 
            // MovieName
            // 
            this.MovieName.HeaderText = "Name";
            this.MovieName.Name = "MovieName";
            this.MovieName.Width = 280;
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
            // ImdbRating
            // 
            this.ImdbRating.HeaderText = "Rating";
            this.ImdbRating.Name = "ImdbRating";
            this.ImdbRating.Width = 50;
            // 
            // Genre
            // 
            this.Genre.HeaderText = "Genre";
            this.Genre.Name = "Genre";
            this.Genre.Width = 200;
            // 
            // AddedDate
            // 
            this.AddedDate.HeaderText = "AddedDate";
            this.AddedDate.Name = "AddedDate";
            this.AddedDate.Width = 120;
            // 
            // contextMenuStripMovieGrid
            // 
            this.contextMenuStripMovieGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openMovieToolStripMenuItem,
            this.deleteMoviesToolStripMenuItem,
            this.markAsSeenToolStripMenuItem});
            this.contextMenuStripMovieGrid.Name = "contextMenuStripMovieGrid";
            this.contextMenuStripMovieGrid.Size = new System.Drawing.Size(153, 92);
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
            // markAsSeenToolStripMenuItem
            // 
            this.markAsSeenToolStripMenuItem.Name = "markAsSeenToolStripMenuItem";
            this.markAsSeenToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.markAsSeenToolStripMenuItem.Text = "Mark As Seen";
            this.markAsSeenToolStripMenuItem.Click += new System.EventHandler(this.markAsSeenToolStripMenuItem_Click);
            // 
            // WatchlistViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 368);
            this.Controls.Add(this.dgMovies);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "WatchlistViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "My Watchlist";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WatchlistViewer_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgMovies)).EndInit();
            this.contextMenuStripMovieGrid.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgMovies;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMovieGrid;
        private System.Windows.Forms.ToolStripMenuItem openMovieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteMoviesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem markAsSeenToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn MovieName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Language;
        private System.Windows.Forms.DataGridViewTextBoxColumn Year;
        private System.Windows.Forms.DataGridViewTextBoxColumn ImdbRating;
        private System.Windows.Forms.DataGridViewTextBoxColumn Genre;
        private System.Windows.Forms.DataGridViewTextBoxColumn AddedDate;
    }
}