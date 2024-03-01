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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbSELECT = new System.Windows.Forms.RadioButton();
            this.rbALL = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ddlCH = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbSING = new System.Windows.Forms.CheckBox();
            this.btCANCEL = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btSAVE
            // 
            this.btSAVE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSAVE.Location = new System.Drawing.Point(217, 160);
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
            this.cbIMG.Size = new System.Drawing.Size(59, 16);
            this.cbIMG.TabIndex = 1;
            this.cbIMG.Text = "Screen";
            this.cbIMG.UseVisualStyleBackColor = true;
            // 
            // cbSNP
            // 
            this.cbSNP.AutoSize = true;
            this.cbSNP.Checked = true;
            this.cbSNP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSNP.Location = new System.Drawing.Point(6, 45);
            this.cbSNP.Name = "cbSNP";
            this.cbSNP.Size = new System.Drawing.Size(44, 16);
            this.cbSNP.TabIndex = 2;
            this.cbSNP.Text = "SnP";
            this.cbSNP.UseVisualStyleBackColor = true;
            // 
            // cbTRACE
            // 
            this.cbTRACE.AutoSize = true;
            this.cbTRACE.Checked = true;
            this.cbTRACE.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTRACE.Location = new System.Drawing.Point(6, 67);
            this.cbTRACE.Name = "cbTRACE";
            this.cbTRACE.Size = new System.Drawing.Size(53, 16);
            this.cbTRACE.TabIndex = 3;
            this.cbTRACE.Text = "Trace";
            this.cbTRACE.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbSELECT);
            this.groupBox1.Controls.Add(this.rbALL);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(150, 70);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Channel";
            // 
            // rbSELECT
            // 
            this.rbSELECT.AutoSize = true;
            this.rbSELECT.Location = new System.Drawing.Point(6, 40);
            this.rbSELECT.Name = "rbSELECT";
            this.rbSELECT.Size = new System.Drawing.Size(55, 16);
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
            this.rbALL.Size = new System.Drawing.Size(37, 16);
            this.rbALL.TabIndex = 0;
            this.rbALL.TabStop = true;
            this.rbALL.Text = "All";
            this.rbALL.UseVisualStyleBackColor = true;
            this.rbALL.CheckedChanged += new System.EventHandler(this.rbALL_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbSNP);
            this.groupBox2.Controls.Add(this.cbIMG);
            this.groupBox2.Controls.Add(this.cbTRACE);
            this.groupBox2.Location = new System.Drawing.Point(12, 88);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(100, 95);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Save target";
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
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbSING);
            this.groupBox3.Location = new System.Drawing.Point(168, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 56);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Mode Settings";
            // 
            // cbSING
            // 
            this.cbSING.AutoSize = true;
            this.cbSING.Checked = true;
            this.cbSING.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSING.Location = new System.Drawing.Point(6, 18);
            this.cbSING.Name = "cbSING";
            this.cbSING.Size = new System.Drawing.Size(123, 16);
            this.cbSING.TabIndex = 0;
            this.cbSING.Text = "Auto Single Trigger";
            this.cbSING.UseVisualStyleBackColor = true;
            // 
            // btCANCEL
            // 
            this.btCANCEL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCANCEL.Location = new System.Drawing.Point(298, 160);
            this.btCANCEL.Name = "btCANCEL";
            this.btCANCEL.Size = new System.Drawing.Size(75, 23);
            this.btCANCEL.TabIndex = 8;
            this.btCANCEL.Text = "Cancel";
            this.btCANCEL.UseVisualStyleBackColor = true;
            this.btCANCEL.Click += new System.EventHandler(this.btCANCEL_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 195);
            this.Controls.Add(this.btCANCEL);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.ddlCH);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btSAVE);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "Save Multiple";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btSAVE;
        private System.Windows.Forms.CheckBox cbIMG;
        private System.Windows.Forms.CheckBox cbSNP;
        private System.Windows.Forms.CheckBox cbTRACE;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbSELECT;
        private System.Windows.Forms.RadioButton rbALL;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox ddlCH;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox cbSING;
        private System.Windows.Forms.Button btCANCEL;
    }
}

