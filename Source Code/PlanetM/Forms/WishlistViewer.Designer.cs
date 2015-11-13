namespace PlanetM.Forms
{
    partial class WishlistViewer
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
            this.Top250 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Oscars = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Genre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStripMovieGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openMovieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteMoviesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.markAsNonWishlistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.Top250,
            this.Oscars,
            this.Genre});
            this.dgMovies.ContextMenuStrip = this.contextMenuStripMovieGrid;
            this.dgMovies.Location = new System.Drawing.Point(12, 12);
            this.dgMovies.Name = "dgMovies";
            this.dgMovies.RowHeadersVisible = false;
            this.dgMovies.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgMovies.Size = new System.Drawing.Size(800, 344);
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
            this.Title.Width = 280;
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
            // Top250
            // 
            this.Top250.HeaderText = "Top 250";
            this.Top250.Name = "Top250";
            this.Top250.Width = 50;
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
            this.Genre.Width = 200;
            // 
            // contextMenuStripMovieGrid
            // 
            this.contextMenuStripMovieGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openMovieToolStripMenuItem,
            this.deleteMoviesToolStripMenuItem,
            this.markAsNonWishlistToolStripMenuItem});
            this.contextMenuStripMovieGrid.Name = "contextMenuStripMovieGrid";
            this.contextMenuStripMovieGrid.Size = new System.Drawing.Size(185, 92);
            // 
            // openMovieToolStripMenuItem
            // 
            this.openMovieToolStripMenuItem.Name = "openMovieToolStripMenuItem";
            this.openMovieToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.openMovieToolStripMenuItem.Text = "Open Movie";
            this.openMovieToolStripMenuItem.Click += new System.EventHandler(this.openMovieToolStripMenuItem_Click);
            // 
            // deleteMoviesToolStripMenuItem
            // 
            this.deleteMoviesToolStripMenuItem.Name = "deleteMoviesToolStripMenuItem";
            this.deleteMoviesToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.deleteMoviesToolStripMenuItem.Text = "Delete Movie";
            this.deleteMoviesToolStripMenuItem.Click += new System.EventHandler(this.deleteMoviesToolStripMenuItem_Click);
            // 
            // markAsNonWishlistToolStripMenuItem
            // 
            this.markAsNonWishlistToolStripMenuItem.Name = "markAsNonWishlistToolStripMenuItem";
            this.markAsNonWishlistToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.markAsNonWishlistToolStripMenuItem.Text = "Mark As NonWishlist";
            this.markAsNonWishlistToolStripMenuItem.Click += new System.EventHandler(this.markAsNonWishlistToolStripMenuItem_Click);
            // 
            // WishlistViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 368);
            this.Controls.Add(this.dgMovies);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "WishlistViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "My Wishlist";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WishlistViewer_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgMovies)).EndInit();
            this.contextMenuStripMovieGrid.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgMovies;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMovieGrid;
        private System.Windows.Forms.ToolStripMenuItem openMovieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteMoviesToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn IMDBID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn Language;
        private System.Windows.Forms.DataGridViewTextBoxColumn Year;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rating;
        private System.Windows.Forms.DataGridViewTextBoxColumn Top250;
        private System.Windows.Forms.DataGridViewTextBoxColumn Oscars;
        private System.Windows.Forms.DataGridViewTextBoxColumn Genre;
        private System.Windows.Forms.ToolStripMenuItem markAsNonWishlistToolStripMenuItem;
    }
}