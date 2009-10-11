namespace SampleCollection.PaletteJig
{
    partial class PaletteUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.promptPolyDirectlyButton = new System.Windows.Forms.Button();
            this.promptPolylineViaCommand = new System.Windows.Forms.Button();
            this.numberOfPointsLabel = new System.Windows.Forms.Label();
            this.numberOfPointsTextbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // promptPolyDirectlyButton
            // 
            this.promptPolyDirectlyButton.Location = new System.Drawing.Point(3, 3);
            this.promptPolyDirectlyButton.Name = "promptPolyDirectlyButton";
            this.promptPolyDirectlyButton.Size = new System.Drawing.Size(166, 23);
            this.promptPolyDirectlyButton.TabIndex = 0;
            this.promptPolyDirectlyButton.Text = "Prompt polyline directly";
            this.promptPolyDirectlyButton.UseVisualStyleBackColor = true;
            this.promptPolyDirectlyButton.Click += new System.EventHandler(this.promptPolyDirectlyButton_Click);
            // 
            // promptPolylineViaCommand
            // 
            this.promptPolylineViaCommand.Location = new System.Drawing.Point(3, 32);
            this.promptPolylineViaCommand.Name = "promptPolylineViaCommand";
            this.promptPolylineViaCommand.Size = new System.Drawing.Size(166, 23);
            this.promptPolylineViaCommand.TabIndex = 0;
            this.promptPolylineViaCommand.Text = "Prompt polyline via command";
            this.promptPolylineViaCommand.UseVisualStyleBackColor = true;
            this.promptPolylineViaCommand.Click += new System.EventHandler(this.promptPolylineViaCommand_Click);
            // 
            // numberOfPointsLabel
            // 
            this.numberOfPointsLabel.AutoSize = true;
            this.numberOfPointsLabel.Location = new System.Drawing.Point(3, 87);
            this.numberOfPointsLabel.Name = "numberOfPointsLabel";
            this.numberOfPointsLabel.Size = new System.Drawing.Size(140, 13);
            this.numberOfPointsLabel.TabIndex = 1;
            this.numberOfPointsLabel.Text = "Number of points in Polyline:";
            // 
            // numberOfPointsTextbox
            // 
            this.numberOfPointsTextbox.Enabled = false;
            this.numberOfPointsTextbox.Location = new System.Drawing.Point(3, 103);
            this.numberOfPointsTextbox.Name = "numberOfPointsTextbox";
            this.numberOfPointsTextbox.Size = new System.Drawing.Size(100, 20);
            this.numberOfPointsTextbox.TabIndex = 2;
            // 
            // PaletteUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numberOfPointsTextbox);
            this.Controls.Add(this.numberOfPointsLabel);
            this.Controls.Add(this.promptPolylineViaCommand);
            this.Controls.Add(this.promptPolyDirectlyButton);
            this.Name = "PaletteUserControl";
            this.Size = new System.Drawing.Size(180, 192);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button promptPolyDirectlyButton;
        private System.Windows.Forms.Button promptPolylineViaCommand;
        private System.Windows.Forms.Label numberOfPointsLabel;
        private System.Windows.Forms.TextBox numberOfPointsTextbox;
    }
}
