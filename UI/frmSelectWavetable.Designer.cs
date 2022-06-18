namespace UI
{
    partial class frmSelectWavetable
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
            this.tvWaveTables = new System.Windows.Forms.TreeView();
            this.cmdSelect = new System.Windows.Forms.Button();
            this.CmdCancel = new System.Windows.Forms.Button();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timShowPreviousWave = new System.Windows.Forms.Timer(this.components);
            this.chkPlayPreview = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // tvWaveTables
            // 
            this.tvWaveTables.BackColor = System.Drawing.Color.AliceBlue;
            this.tvWaveTables.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tvWaveTables.ForeColor = System.Drawing.Color.Blue;
            this.tvWaveTables.Location = new System.Drawing.Point(12, 12);
            this.tvWaveTables.Name = "tvWaveTables";
            this.tvWaveTables.Size = new System.Drawing.Size(322, 426);
            this.tvWaveTables.TabIndex = 0;
            // 
            // cmdSelect
            // 
            this.cmdSelect.Location = new System.Drawing.Point(874, 12);
            this.cmdSelect.Name = "cmdSelect";
            this.cmdSelect.Size = new System.Drawing.Size(79, 32);
            this.cmdSelect.TabIndex = 1;
            this.cmdSelect.Text = "Select";
            this.cmdSelect.UseVisualStyleBackColor = true;
            // 
            // CmdCancel
            // 
            this.CmdCancel.Location = new System.Drawing.Point(874, 50);
            this.CmdCancel.Name = "CmdCancel";
            this.CmdCancel.Size = new System.Drawing.Size(79, 32);
            this.CmdCancel.TabIndex = 2;
            this.CmdCancel.Text = "Cancel";
            this.CmdCancel.UseVisualStyleBackColor = true;
            // 
            // picPreview
            // 
            this.picPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.picPreview.Location = new System.Drawing.Point(353, 153);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(600, 285);
            this.picPreview.TabIndex = 3;
            this.picPreview.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(353, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "WaveTable Preview:";
            // 
            // chkPlayPreview
            // 
            this.chkPlayPreview.AutoSize = true;
            this.chkPlayPreview.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkPlayPreview.Checked = true;
            this.chkPlayPreview.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPlayPreview.ForeColor = System.Drawing.Color.Blue;
            this.chkPlayPreview.Location = new System.Drawing.Point(861, 134);
            this.chkPlayPreview.Name = "chkPlayPreview";
            this.chkPlayPreview.Size = new System.Drawing.Size(92, 19);
            this.chkPlayPreview.TabIndex = 19;
            this.chkPlayPreview.Text = "Play Preview";
            this.chkPlayPreview.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkPlayPreview.UseVisualStyleBackColor = true;
            // 
            // frmSelectWavetable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(970, 454);
            this.Controls.Add(this.chkPlayPreview);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picPreview);
            this.Controls.Add(this.CmdCancel);
            this.Controls.Add(this.cmdSelect);
            this.Controls.Add(this.tvWaveTables);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelectWavetable";
            this.Text = "Select WaveTable File";
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TreeView tvWaveTables;
        private Button cmdSelect;
        private Button CmdCancel;
        private PictureBox picPreview;
        private Label label1;
        private System.Windows.Forms.Timer timShowPreviousWave;
        private CheckBox chkPlayPreview;
    }
}