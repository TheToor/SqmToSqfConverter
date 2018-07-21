namespace SqmToSqfConverter.Forms
{
    partial class Stats
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
            this.LabelCountVehicles = new System.Windows.Forms.Label();
            this.LabelCountUnits = new System.Windows.Forms.Label();
            this.LabelCountGroups = new System.Windows.Forms.Label();
            this.LabelDetailedStats = new System.Windows.Forms.Label();
            this.PanelDetailedStats = new System.Windows.Forms.Panel();
            this.PanelDetailedStats.SuspendLayout();
            this.SuspendLayout();
            // 
            // LabelCountVehicles
            // 
            this.LabelCountVehicles.AutoSize = true;
            this.LabelCountVehicles.Location = new System.Drawing.Point(13, 13);
            this.LabelCountVehicles.Name = "LabelCountVehicles";
            this.LabelCountVehicles.Size = new System.Drawing.Size(77, 13);
            this.LabelCountVehicles.TabIndex = 0;
            this.LabelCountVehicles.Text = "Vehicles: 0000";
            // 
            // LabelCountUnits
            // 
            this.LabelCountUnits.AutoSize = true;
            this.LabelCountUnits.Location = new System.Drawing.Point(96, 13);
            this.LabelCountUnits.Name = "LabelCountUnits";
            this.LabelCountUnits.Size = new System.Drawing.Size(61, 13);
            this.LabelCountUnits.TabIndex = 1;
            this.LabelCountUnits.Text = "Units: 0000";
            // 
            // LabelCountGroups
            // 
            this.LabelCountGroups.AutoSize = true;
            this.LabelCountGroups.Location = new System.Drawing.Point(163, 13);
            this.LabelCountGroups.Name = "LabelCountGroups";
            this.LabelCountGroups.Size = new System.Drawing.Size(71, 13);
            this.LabelCountGroups.TabIndex = 2;
            this.LabelCountGroups.Text = "Groups: 0000";
            // 
            // LabelDetailedStats
            // 
            this.LabelDetailedStats.AutoSize = true;
            this.LabelDetailedStats.Location = new System.Drawing.Point(3, 0);
            this.LabelDetailedStats.Name = "LabelDetailedStats";
            this.LabelDetailedStats.Size = new System.Drawing.Size(0, 13);
            this.LabelDetailedStats.TabIndex = 3;
            // 
            // PanelDetailedStats
            // 
            this.PanelDetailedStats.AutoScroll = true;
            this.PanelDetailedStats.Controls.Add(this.LabelDetailedStats);
            this.PanelDetailedStats.Location = new System.Drawing.Point(16, 29);
            this.PanelDetailedStats.Name = "PanelDetailedStats";
            this.PanelDetailedStats.Size = new System.Drawing.Size(218, 409);
            this.PanelDetailedStats.TabIndex = 4;
            // 
            // Stats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(252, 450);
            this.Controls.Add(this.PanelDetailedStats);
            this.Controls.Add(this.LabelCountGroups);
            this.Controls.Add(this.LabelCountUnits);
            this.Controls.Add(this.LabelCountVehicles);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(268, 489);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(268, 489);
            this.Name = "Stats";
            this.ShowIcon = false;
            this.Text = "Stats";
            this.PanelDetailedStats.ResumeLayout(false);
            this.PanelDetailedStats.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelCountVehicles;
        private System.Windows.Forms.Label LabelCountUnits;
        private System.Windows.Forms.Label LabelCountGroups;
        private System.Windows.Forms.Label LabelDetailedStats;
        private System.Windows.Forms.Panel PanelDetailedStats;
    }
}