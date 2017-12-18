using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npoi.DoWord;

namespace DbSqlGenerater
{
    public partial class DbSyncForm : Form
    {
        public DbSyncForm()
        {
            InitializeComponent();
            this.txtDbType.Text = "System.Data.Sqlclient";            
            this.txtDbConnectionStr.Text = "";
        }

        

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "所有 Word 文档(*.docx,*.doc)|*.docx;*.doc";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = fileDialog.FileName;
            }
        }

        private void btnGenerateSql_Click(object sender, EventArgs e)
        {
            string filePath = txtFilePath.Text;
            if (string.IsNullOrWhiteSpace(filePath))
            {
                MessageBox.Show("请选择doc文档!", "提示");
                return;
            }
            string text = this.cbGenerateWay.SelectedItem.ToString();
            if (string.IsNullOrWhiteSpace(text))
            {
                MessageBox.Show("请选择生成方式!", "提示");
                return;
            }
            string dbConnectionStr = this.txtDbConnectionStr.Text;            
            string dbType = this.txtDbType.Text;
            switch (text)
            {
                case "创建数据表":
                    break;
                case "同步数据表":
                    if (string.IsNullOrWhiteSpace(dbConnectionStr))
                    {
                        MessageBox.Show("请填写数据库连接字符串", "提示");
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(dbType))
                    {
                        MessageBox.Show("请填写数据库类型", "提示");
                        return;
                    }
                    break;
                default:
                    MessageBox.Show("未知生成方式!", "警告");
                    return;
            }
            DbComparedHelper comparedHelper = new DbComparedHelper();
            comparedHelper.FilePath = filePath;
            if (text == "同步数据表")
            {
                comparedHelper.SetConfig(config =>
                {
                    config.DbConnection = dbConnectionStr;
                    config.ProviderName = dbType;
                });
            }

            var result=comparedHelper.Create();
            this.rtbSql.Text = result;
        }
    }
}
