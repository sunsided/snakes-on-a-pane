using GameWindow.Rendering;

namespace GameWindow
{
    /// <summary>
    /// Class MainForm.
    /// </summary>
    partial class MainForm
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
            this.renderTarget1 = new RenderTarget();
            this.SuspendLayout();
            // 
            // renderTarget1
            // 
            this.renderTarget1.Location = new System.Drawing.Point(12, 12);
            this.renderTarget1.Name = "renderTarget1";
            this.renderTarget1.Size = new System.Drawing.Size(641, 481);
            this.renderTarget1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 505);
            this.Controls.Add(this.renderTarget1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Snake\'s on a Pane";
            this.ResumeLayout(false);

        }

        #endregion

        private RenderTarget renderTarget1;
    }
}

