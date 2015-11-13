namespace PlanetM.Forms
{
    partial class RecentMovieViewer
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
            this.Size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AddedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStripMovieGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openMovieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteMoviesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.Size,
            this.AddedDate});
            this.dgMovies.ContextMenuStrip = this.contextMenuStripMovieGrid;
            this.dgMovies.Location = new System.Drawing.Point(12, 12);
            this.dgMovies.Name = "dgMovies";
            this.dgMovies.RowHeadersVisible = false;
            this.dgMovies.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgMovies.Size = new System.Drawing.Size(673, 344);
            this.dgMovies.TabIndex = 0;
            this.dgMovies.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMovies_CellDoubleClick);
            // 
            // MovieName
            // 
            this.MovieName.HeaderText = "Name";
            this.MovieName.Name = "MovieName";
            this.MovieName.Width = 350;
            // 
            // Language
            // 
            this.Language.HeaderText = "Language";
            this.Language.Name = "Language";
            // 
            // Size
            // 
            this.Size.HeaderText = "Size (MB)";
            this.Size.Name = "Size";
            this.Size.Width = 70;
            // 
            // AddedDate
            // 
            this.AddedDate.HeaderText = "AddedDate";
            this.AddedDate.Name = "AddedDate";
            this.AddedDate.Width = 130;
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
            // RecentMovieViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 368);
            this.Controls.Add(this.dgMovies);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "RecentMovieViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recent Movies";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RecentMovieViewer_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgMovies)).EndInit();
            this.contextMenuStripMovieGrid.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgMovies;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMovieGrid;
        private System.Windows.Forms.ToolStripMenuItem openMovieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteMoviesToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn MovieName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Language;
        private System.Windows.Forms.DataGridViewTextBoxColumn Size;
        private System.Windows.Forms.DataGridViewTextBoxColumn AddedDate;
    }
}