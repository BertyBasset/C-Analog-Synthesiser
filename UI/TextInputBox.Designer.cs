namespace UI
{
    partial class TextInputBox
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
            this.lblLabel = new System.Windows.Forms.Label();
            this.txtText = new System.Windows.Forms.TextBox();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblLabel
            // 
            this.lblLabel.Location = new System.Drawing.Point(-2, 44);
            this.lblLabel.Name = "lblLabel";
            this.lblLabel.Size = new System.Drawing.Size(95, 23);
            this.lblLabel.TabIndex = 0;
            this.lblLabel.Text = "label1";
            this.lblLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtText
            // 
            this.txtText.Location = new System.Drawing.Point(93, 41);
            this.txtText.Name = "txtText";
            this.txtText.Size = new System.Drawing.Size(148, 23);
            this.txtText.TabIndex = 1;
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(259, 23);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(74, 28);
            this.cmdOK.TabIndex = 2;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(259, 62);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(74, 28);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // TextInputBox
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(350, 108);
            this.ControlBox = false;
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.txtText);
            this.Controls.Add(this.lblLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "TextInputBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TextInputBox";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblLabel;
        private TextBox txtText;
        private Button cmdOK;
        private Button cmdCancel;
    }
}