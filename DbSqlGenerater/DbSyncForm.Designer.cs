namespace DbSqlGenerater
{
    partial class DbSyncForm
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
            this.lblFilePath = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnChooseFile = new System.Windows.Forms.Button();
            this.lblDbConnectionStr = new System.Windows.Forms.Label();
            this.txtDbConnectionStr = new System.Windows.Forms.TextBox();
            this.lblDbType = new System.Windows.Forms.Label();
            this.txtDbType = new System.Windows.Forms.TextBox();
            this.lblGenerateWay = new System.Windows.Forms.Label();
            this.cbGenerateWay = new System.Windows.Forms.ComboBox();
            this.lblSql = new System.Windows.Forms.Label();
            this.rtbSql = new System.Windows.Forms.RichTextBox();
            this.btnGenerateSql = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Location = new System.Drawing.Point(12, 31);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(65, 12);
            this.lblFilePath.TabIndex = 0;
            this.lblFilePath.Text = "文件路径：";
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(83, 28);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(385, 21);
            this.txtFilePath.TabIndex = 1;
            // 
            // btnChooseFile
            // 
            this.btnChooseFile.Location = new System.Drawing.Point(486, 26);
            this.btnChooseFile.Name = "btnChooseFile";
            this.btnChooseFile.Size = new System.Drawing.Size(75, 23);
            this.btnChooseFile.TabIndex = 2;
            this.btnChooseFile.Text = "选择文件";
            this.btnChooseFile.UseVisualStyleBackColor = true;
            this.btnChooseFile.Click += new System.EventHandler(this.btnChooseFile_Click);
            // 
            // lblDbConnectionStr
            // 
            this.lblDbConnectionStr.AutoSize = true;
            this.lblDbConnectionStr.Location = new System.Drawing.Point(12, 63);
            this.lblDbConnectionStr.Name = "lblDbConnectionStr";
            this.lblDbConnectionStr.Size = new System.Drawing.Size(113, 12);
            this.lblDbConnectionStr.TabIndex = 3;
            this.lblDbConnectionStr.Text = "数据库连接字符串：";
            // 
            // txtDbConnectionStr
            // 
            this.txtDbConnectionStr.Location = new System.Drawing.Point(131, 60);
            this.txtDbConnectionStr.Name = "txtDbConnectionStr";
            this.txtDbConnectionStr.Size = new System.Drawing.Size(337, 21);
            this.txtDbConnectionStr.TabIndex = 4;
            // 
            // lblDbType
            // 
            this.lblDbType.AutoSize = true;
            this.lblDbType.Location = new System.Drawing.Point(12, 120);
            this.lblDbType.Name = "lblDbType";
            this.lblDbType.Size = new System.Drawing.Size(77, 12);
            this.lblDbType.TabIndex = 7;
            this.lblDbType.Text = "数据库类型：";
            // 
            // txtDbType
            // 
            this.txtDbType.Location = new System.Drawing.Point(83, 117);
            this.txtDbType.Name = "txtDbType";
            this.txtDbType.Size = new System.Drawing.Size(385, 21);
            this.txtDbType.TabIndex = 8;
            // 
            // lblGenerateWay
            // 
            this.lblGenerateWay.AutoSize = true;
            this.lblGenerateWay.Location = new System.Drawing.Point(14, 146);
            this.lblGenerateWay.Name = "lblGenerateWay";
            this.lblGenerateWay.Size = new System.Drawing.Size(65, 12);
            this.lblGenerateWay.TabIndex = 9;
            this.lblGenerateWay.Text = "生成方式：";
            // 
            // cbGenerateWay
            // 
            this.cbGenerateWay.FormattingEnabled = true;
            this.cbGenerateWay.Items.AddRange(new object[] {
            "创建数据表",
            "同步数据表"});
            this.cbGenerateWay.Location = new System.Drawing.Point(83, 143);
            this.cbGenerateWay.Name = "cbGenerateWay";
            this.cbGenerateWay.Size = new System.Drawing.Size(385, 20);
            this.cbGenerateWay.TabIndex = 10;
            // 
            // lblSql
            // 
            this.lblSql.AutoSize = true;
            this.lblSql.Location = new System.Drawing.Point(12, 184);
            this.lblSql.Name = "lblSql";
            this.lblSql.Size = new System.Drawing.Size(59, 12);
            this.lblSql.TabIndex = 11;
            this.lblSql.Text = "生成Sql：";
            // 
            // rtbSql
            // 
            this.rtbSql.Location = new System.Drawing.Point(12, 210);
            this.rtbSql.Name = "rtbSql";
            this.rtbSql.Size = new System.Drawing.Size(938, 465);
            this.rtbSql.TabIndex = 12;
            this.rtbSql.Text = "";
            // 
            // btnGenerateSql
            // 
            this.btnGenerateSql.Location = new System.Drawing.Point(486, 143);
            this.btnGenerateSql.Name = "btnGenerateSql";
            this.btnGenerateSql.Size = new System.Drawing.Size(75, 23);
            this.btnGenerateSql.TabIndex = 13;
            this.btnGenerateSql.Text = "生成SQL";
            this.btnGenerateSql.UseVisualStyleBackColor = true;
            this.btnGenerateSql.Click += new System.EventHandler(this.btnGenerateSql_Click);
            // 
            // DbSyncForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 687);
            this.Controls.Add(this.btnGenerateSql);
            this.Controls.Add(this.rtbSql);
            this.Controls.Add(this.lblSql);
            this.Controls.Add(this.cbGenerateWay);
            this.Controls.Add(this.lblGenerateWay);
            this.Controls.Add(this.txtDbType);
            this.Controls.Add(this.lblDbType);
            this.Controls.Add(this.txtDbConnectionStr);
            this.Controls.Add(this.lblDbConnectionStr);
            this.Controls.Add(this.btnChooseFile);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.lblFilePath);
            this.Name = "DbSyncForm";
            this.Text = "数据库同步工具";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnChooseFile;
        private System.Windows.Forms.Label lblDbConnectionStr;
        private System.Windows.Forms.TextBox txtDbConnectionStr;
        private System.Windows.Forms.Label lblDbType;
        private System.Windows.Forms.TextBox txtDbType;
        private System.Windows.Forms.Label lblGenerateWay;
        private System.Windows.Forms.ComboBox cbGenerateWay;
        private System.Windows.Forms.Label lblSql;
        private System.Windows.Forms.RichTextBox rtbSql;
        private System.Windows.Forms.Button btnGenerateSql;
    }
}