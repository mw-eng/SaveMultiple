namespace SaveMultiple
{
    partial class frmMain
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btSAVE = new System.Windows.Forms.Button();
            this.cbIMG = new System.Windows.Forms.CheckBox();
            this.cbSNP = new System.Windows.Forms.CheckBox();
            this.cbTRACE = new System.Windows.Forms.CheckBox();
            this.gbCh = new System.Windows.Forms.GroupBox();
            this.rbSELECT = new System.Windows.Forms.RadioButton();
            this.rbALL = new System.Windows.Forms.RadioButton();
            this.gbSaveTarg = new System.Windows.Forms.GroupBox();
            this.gbSelPort = new System.Windows.Forms.GroupBox();
            this.btCLEAR = new System.Windows.Forms.Button();
            this.btALL = new System.Windows.Forms.Button();
            this.clbPT = new System.Windows.Forms.CheckedListBox();
            this.ddlCH = new System.Windows.Forms.ComboBox();
            this.gbConf = new System.Windows.Forms.GroupBox();
            this.cbSING = new System.Windows.Forms.CheckBox();
            this.btCANCEL = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbFT = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gbCh.SuspendLayout();
            this.gbSaveTarg.SuspendLayout();
            this.gbSelPort.SuspendLayout();
            this.gbConf.SuspendLayout();
            this.SuspendLayout();
            // 
            // btSAVE
            // 
            this.btSAVE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSAVE.Location = new System.Drawing.Point(200, 310);
            this.btSAVE.Name = "btSAVE";
            this.btSAVE.Size = new System.Drawing.Size(75, 23);
            this.btSAVE.TabIndex = 0;
            this.btSAVE.Text = "Save";
            this.btSAVE.UseVisualStyleBackColor = true;
            this.btSAVE.Click += new System.EventHandler(this.btSAVE_Click);
            // 
            // cbIMG
            // 
            this.cbIMG.AutoSize = true;
            this.cbIMG.Checked = true;
            this.cbIMG.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIMG.Location = new System.Drawing.Point(6, 23);
            this.cbIMG.Name = "cbIMG";
            this.cbIMG.Size = new System.Drawing.Size(70, 17);
            this.cbIMG.TabIndex = 1;
            this.cbIMG.Text = "Screen";
            this.cbIMG.UseVisualStyleBackColor = true;
            this.cbIMG.CheckedChanged += new System.EventHandler(this.cbIMG_CheckedChanged);
            // 
            // cbSNP
            // 
            this.cbSNP.AutoSize = true;
            this.cbSNP.Checked = true;
            this.cbSNP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSNP.Location = new System.Drawing.Point(130, 23);
            this.cbSNP.Name = "cbSNP";
            this.cbSNP.Size = new System.Drawing.Size(52, 17);
            this.cbSNP.TabIndex = 2;
            this.cbSNP.Text = "SnP";
            this.cbSNP.UseVisualStyleBackColor = true;
            this.cbSNP.CheckedChanged += new System.EventHandler(this.cbSNP_CheckedChanged);
            // 
            // cbTRACE
            // 
            this.cbTRACE.AutoSize = true;
            this.cbTRACE.Checked = true;
            this.cbTRACE.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTRACE.Location = new System.Drawing.Point(71, 23);
            this.cbTRACE.Name = "cbTRACE";
            this.cbTRACE.Size = new System.Drawing.Size(62, 17);
            this.cbTRACE.TabIndex = 3;
            this.cbTRACE.Text = "Trace";
            this.cbTRACE.UseVisualStyleBackColor = true;
            this.cbTRACE.CheckedChanged += new System.EventHandler(this.cbTRACE_CheckedChanged);
            // 
            // gbCh
            // 
            this.gbCh.Controls.Add(this.rbSELECT);
            this.gbCh.Controls.Add(this.rbALL);
            this.gbCh.Location = new System.Drawing.Point(12, 12);
            this.gbCh.Name = "gbCh";
            this.gbCh.Size = new System.Drawing.Size(150, 70);
            this.gbCh.TabIndex = 4;
            this.gbCh.TabStop = false;
            this.gbCh.Text = "Channel";
            // 
            // rbSELECT
            // 
            this.rbSELECT.AutoSize = true;
            this.rbSELECT.Location = new System.Drawing.Point(6, 40);
            this.rbSELECT.Name = "rbSELECT";
            this.rbSELECT.Size = new System.Drawing.Size(65, 17);
            this.rbSELECT.TabIndex = 1;
            this.rbSELECT.Text = "Select";
            this.rbSELECT.UseVisualStyleBackColor = true;
            this.rbSELECT.CheckedChanged += new System.EventHandler(this.rbSELECT_CheckedChanged);
            // 
            // rbALL
            // 
            this.rbALL.AutoSize = true;
            this.rbALL.Checked = true;
            this.rbALL.Location = new System.Drawing.Point(6, 18);
            this.rbALL.Name = "rbALL";
            this.rbALL.Size = new System.Drawing.Size(42, 17);
            this.rbALL.TabIndex = 0;
            this.rbALL.TabStop = true;
            this.rbALL.Text = "All";
            this.rbALL.UseVisualStyleBackColor = true;
            this.rbALL.CheckedChanged += new System.EventHandler(this.rbALL_CheckedChanged);
            // 
            // gbSaveTarg
            // 
            this.gbSaveTarg.Controls.Add(this.gbSelPort);
            this.gbSaveTarg.Controls.Add(this.cbSNP);
            this.gbSaveTarg.Controls.Add(this.cbIMG);
            this.gbSaveTarg.Controls.Add(this.cbTRACE);
            this.gbSaveTarg.Location = new System.Drawing.Point(168, 12);
            this.gbSaveTarg.Name = "gbSaveTarg";
            this.gbSaveTarg.Size = new System.Drawing.Size(185, 251);
            this.gbSaveTarg.TabIndex = 5;
            this.gbSaveTarg.TabStop = false;
            this.gbSaveTarg.Text = "Save target";
            // 
            // gbSelPort
            // 
            this.gbSelPort.Controls.Add(this.btCLEAR);
            this.gbSelPort.Controls.Add(this.btALL);
            this.gbSelPort.Controls.Add(this.clbPT);
            this.gbSelPort.Location = new System.Drawing.Point(9, 45);
            this.gbSelPort.Name = "gbSelPort";
            this.gbSelPort.Size = new System.Drawing.Size(170, 200);
            this.gbSelPort.TabIndex = 4;
            this.gbSelPort.TabStop = false;
            this.gbSelPort.Text = "Select Port";
            // 
            // btCLEAR
            // 
            this.btCLEAR.Location = new System.Drawing.Point(89, 171);
            this.btCLEAR.Name = "btCLEAR";
            this.btCLEAR.Size = new System.Drawing.Size(75, 23);
            this.btCLEAR.TabIndex = 13;
            this.btCLEAR.Text = "Clear";
            this.btCLEAR.UseVisualStyleBackColor = true;
            this.btCLEAR.Click += new System.EventHandler(this.btCLEAR_Click);
            // 
            // btALL
            // 
            this.btALL.Location = new System.Drawing.Point(6, 171);
            this.btALL.Name = "btALL";
            this.btALL.Size = new System.Drawing.Size(75, 23);
            this.btALL.TabIndex = 12;
            this.btALL.Text = "ALL Check";
            this.btALL.UseVisualStyleBackColor = true;
            this.btALL.Click += new System.EventHandler(this.btALL_Click);
            // 
            // clbPT
            // 
            this.clbPT.CheckOnClick = true;
            this.clbPT.FormattingEnabled = true;
            this.clbPT.Location = new System.Drawing.Point(6, 18);
            this.clbPT.Name = "clbPT";
            this.clbPT.Size = new System.Drawing.Size(157, 130);
            this.clbPT.TabIndex = 11;
            this.clbPT.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbPT_ItemCheck);
            // 
            // ddlCH
            // 
            this.ddlCH.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlCH.Enabled = false;
            this.ddlCH.FormattingEnabled = true;
            this.ddlCH.Location = new System.Drawing.Point(79, 51);
            this.ddlCH.Name = "ddlCH";
            this.ddlCH.Size = new System.Drawing.Size(77, 20);
            this.ddlCH.TabIndex = 6;
            // 
            // gbConf
            // 
            this.gbConf.Controls.Add(this.cbSING);
            this.gbConf.Location = new System.Drawing.Point(11, 88);
            this.gbConf.Name = "gbConf";
            this.gbConf.Size = new System.Drawing.Size(145, 40);
            this.gbConf.TabIndex = 7;
            this.gbConf.TabStop = false;
            this.gbConf.Text = "Mode Settings";
            // 
            // cbSING
            // 
            this.cbSING.AutoSize = true;
            this.cbSING.Checked = true;
            this.cbSING.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSING.Location = new System.Drawing.Point(6, 18);
            this.cbSING.Name = "cbSING";
            this.cbSING.Size = new System.Drawing.Size(138, 17);
            this.cbSING.TabIndex = 0;
            this.cbSING.Text = "Auto Single Trigger";
            this.cbSING.UseVisualStyleBackColor = true;
            // 
            // btCANCEL
            // 
            this.btCANCEL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCANCEL.Location = new System.Drawing.Point(279, 310);
            this.btCANCEL.Name = "btCANCEL";
            this.btCANCEL.Size = new System.Drawing.Size(75, 23);
            this.btCANCEL.TabIndex = 8;
            this.btCANCEL.Text = "Cancel";
            this.btCANCEL.UseVisualStyleBackColor = true;
            this.btCANCEL.Click += new System.EventHandler(this.btCANCEL_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 279);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "File Title (File Name Header)";
            // 
            // tbFT
            // 
            this.tbFT.Location = new System.Drawing.Point(162, 276);
            this.tbFT.Name = "tbFT";
            this.tbFT.Size = new System.Drawing.Size(191, 19);
            this.tbFT.TabIndex = 10;
            this.tbFT.TextChanged += new System.EventHandler(this.tbFT_TextChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(80, 340);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(218, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Presented by Orient Microwave Corp.";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 361);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbFT);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btCANCEL);
            this.Controls.Add(this.gbConf);
            this.Controls.Add(this.ddlCH);
            this.Controls.Add(this.gbSaveTarg);
            this.Controls.Add(this.gbCh);
            this.Controls.Add(this.btSAVE);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.Text = "Save Multiple";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.gbCh.ResumeLayout(false);
            this.gbCh.PerformLayout();
            this.gbSaveTarg.ResumeLayout(false);
            this.gbSaveTarg.PerformLayout();
            this.gbSelPort.ResumeLayout(false);
            this.gbConf.ResumeLayout(false);
            this.gbConf.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btSAVE;
        private System.Windows.Forms.CheckBox cbIMG;
        private System.Windows.Forms.CheckBox cbSNP;
        private System.Windows.Forms.CheckBox cbTRACE;
        private System.Windows.Forms.GroupBox gbCh;
        private System.Windows.Forms.RadioButton rbSELECT;
        private System.Windows.Forms.RadioButton rbALL;
        private System.Windows.Forms.GroupBox gbSaveTarg;
        private System.Windows.Forms.ComboBox ddlCH;
        private System.Windows.Forms.GroupBox gbConf;
        private System.Windows.Forms.CheckBox cbSING;
        private System.Windows.Forms.Button btCANCEL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbFT;
        private System.Windows.Forms.CheckedListBox clbPT;
        private System.Windows.Forms.GroupBox gbSelPort;
        private System.Windows.Forms.Button btCLEAR;
        private System.Windows.Forms.Button btALL;
        private System.Windows.Forms.Label label2;
    }
}

