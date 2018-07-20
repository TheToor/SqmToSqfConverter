namespace SqmToSqfConverter
{
    partial class GUI
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
            this.ButtonSelectFile = new System.Windows.Forms.Button();
            this.LabelSelectFile = new System.Windows.Forms.Label();
            this.ButtonConvert = new System.Windows.Forms.Button();
            this.ButtonSaveFile = new System.Windows.Forms.Button();
            this.LabelSaveFile = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ButtonSelectFile
            // 
            this.ButtonSelectFile.Location = new System.Drawing.Point(12, 12);
            this.ButtonSelectFile.Name = "ButtonSelectFile";
            this.ButtonSelectFile.Size = new System.Drawing.Size(75, 23);
            this.ButtonSelectFile.TabIndex = 0;
            this.ButtonSelectFile.Text = "File...";
            this.ButtonSelectFile.UseVisualStyleBackColor = true;
            this.ButtonSelectFile.Click += new System.EventHandler(this.ButtonSelectFile_Click);
            // 
            // LabelSelectFile
            // 
            this.LabelSelectFile.AutoSize = true;
            this.LabelSelectFile.Location = new System.Drawing.Point(93, 17);
            this.LabelSelectFile.Name = "LabelSelectFile";
            this.LabelSelectFile.Size = new System.Drawing.Size(0, 13);
            this.LabelSelectFile.TabIndex = 1;
            this.LabelSelectFile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ButtonConvert
            // 
            this.ButtonConvert.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonConvert.Location = new System.Drawing.Point(12, 70);
            this.ButtonConvert.Name = "ButtonConvert";
            this.ButtonConvert.Size = new System.Drawing.Size(776, 39);
            this.ButtonConvert.TabIndex = 2;
            this.ButtonConvert.Text = "Convert";
            this.ButtonConvert.UseVisualStyleBackColor = true;
            this.ButtonConvert.Click += new System.EventHandler(this.ButtonConvert_Click);
            // 
            // ButtonSaveFile
            // 
            this.ButtonSaveFile.Location = new System.Drawing.Point(12, 41);
            this.ButtonSaveFile.Name = "ButtonSaveFile";
            this.ButtonSaveFile.Size = new System.Drawing.Size(75, 23);
            this.ButtonSaveFile.TabIndex = 3;
            this.ButtonSaveFile.Text = "Save path...";
            this.ButtonSaveFile.UseVisualStyleBackColor = true;
            this.ButtonSaveFile.Click += new System.EventHandler(this.ButtonSaveFile_Click);
            // 
            // LabelSaveFile
            // 
            this.LabelSaveFile.AutoSize = true;
            this.LabelSaveFile.Location = new System.Drawing.Point(93, 46);
            this.LabelSaveFile.Name = "LabelSaveFile";
            this.LabelSaveFile.Size = new System.Drawing.Size(0, 13);
            this.LabelSaveFile.TabIndex = 4;
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 123);
            this.Controls.Add(this.LabelSaveFile);
            this.Controls.Add(this.ButtonSaveFile);
            this.Controls.Add(this.ButtonConvert);
            this.Controls.Add(this.LabelSelectFile);
            this.Controls.Add(this.ButtonSelectFile);
            this.Name = "GUI";
            this.Text = "Sqm to Sqf Converter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonSelectFile;
        private System.Windows.Forms.Label LabelSelectFile;
        private System.Windows.Forms.Button ButtonConvert;
        private System.Windows.Forms.Button ButtonSaveFile;
        private System.Windows.Forms.Label LabelSaveFile;
    }
}

