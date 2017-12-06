namespace DbSqlGenerater
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.cbGenerateType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rtbIncludeTable = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rtbSql = new System.Windows.Forms.RichTextBox();
            this.btnGenerateSql = new System.Windows.Forms.Button();
            this.btnSelectTable = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(378, 32);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(75, 23);
            this.btnSelectFile.TabIndex = 0;
            this.btnSelectFile.Text = "选择文件";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Location = new System.Drawing.Point(12, 35);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(65, 12);
            this.lblFilePath.TabIndex = 1;
            this.lblFilePath.Text = "文件路径：";
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(83, 32);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(283, 21);
            this.txtFilePath.TabIndex = 2;
            // 
            // cbGenerateType
            // 
            this.cbGenerateType.FormattingEnabled = true;
            this.cbGenerateType.Items.AddRange(new object[] {
            "生成创表sql",
            "生成添加列sql",
            "生成添加备注sql"});
            this.cbGenerateType.Location = new System.Drawing.Point(83, 71);
            this.cbGenerateType.Name = "cbGenerateType";
            this.cbGenerateType.Size = new System.Drawing.Size(283, 20);
            this.cbGenerateType.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "生成方式：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "包含表：（表名逗号分隔）";
            // 
            // rtbIncludeTable
            // 
            this.rtbIncludeTable.Location = new System.Drawing.Point(14, 125);
            this.rtbIncludeTable.Name = "rtbIncludeTable";
            this.rtbIncludeTable.Size = new System.Drawing.Size(936, 129);
            this.rtbIncludeTable.TabIndex = 6;
            this.rtbIncludeTable.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 268);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "生成Sql：";
            // 
            // rtbSql
            // 
            this.rtbSql.Location = new System.Drawing.Point(14, 296);
            this.rtbSql.Name = "rtbSql";
            this.rtbSql.Size = new System.Drawing.Size(936, 379);
            this.rtbSql.TabIndex = 8;
            this.rtbSql.Text = "";
            // 
            // btnGenerateSql
            // 
            this.btnGenerateSql.Location = new System.Drawing.Point(378, 69);
            this.btnGenerateSql.Name = "btnGenerateSql";
            this.btnGenerateSql.Size = new System.Drawing.Size(75, 23);
            this.btnGenerateSql.TabIndex = 9;
            this.btnGenerateSql.Text = "生成Sql";
            this.btnGenerateSql.UseVisualStyleBackColor = true;
            this.btnGenerateSql.Click += new System.EventHandler(this.btnGenerateSql_Click);
            // 
            // btnSelectTable
            // 
            this.btnSelectTable.Location = new System.Drawing.Point(180, 98);
            this.btnSelectTable.Name = "btnSelectTable";
            this.btnSelectTable.Size = new System.Drawing.Size(75, 23);
            this.btnSelectTable.TabIndex = 10;
            this.btnSelectTable.Text = "选择表";
            this.btnSelectTable.UseVisualStyleBackColor = true;
            this.btnSelectTable.Click += new System.EventHandler(this.btnSelectTable_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 687);
            this.Controls.Add(this.btnSelectTable);
            this.Controls.Add(this.btnGenerateSql);
            this.Controls.Add(this.rtbSql);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rtbIncludeTable);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbGenerateType);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.lblFilePath);
            this.Controls.Add(this.btnSelectFile);
            this.Name = "Form1";
            this.Text = "数据库Sql生成器";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.ComboBox cbGenerateType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox rtbIncludeTable;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox rtbSql;
        private System.Windows.Forms.Button btnGenerateSql;
        private System.Windows.Forms.Button btnSelectTable;
    }
}

