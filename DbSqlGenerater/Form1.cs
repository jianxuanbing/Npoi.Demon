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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog=new OpenFileDialog();
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
            string text = this.cbGenerateType.SelectedItem.ToString();
            if (string.IsNullOrWhiteSpace(text))
            {
                MessageBox.Show("请选择生成方式!", "提示");
                return;
            }
            string includeText = rtbIncludeTable.Text.Trim();
            string[] includeTable=null;
            if (!string.IsNullOrWhiteSpace(includeText))
            {
                includeTable = includeText.Split(',');
            }            
            List<string> result=new List<string>();
            switch (text)
            {
                case "生成创表sql":

                    result = includeTable == null
                        ? WordHelper.GenerateCreateTableSql(filePath)
                        : WordHelper.GenerateCreateTableSql(filePath, includeTable);
                    break;
                case "生成添加列sql":
                    result = WordHelper.GenerateAddColumnSql(filePath);
                    break;
                case "生成添加备注sql":
                    result = WordHelper.GenerateAddDescSql(filePath);
                    break;
                default:
                    MessageBox.Show("未知生成方式!", "警告");
                    return;                    
            }
            StringBuilder sb=new StringBuilder();
            foreach (var item in result)
            {
                sb.AppendLine(item);
            }
            this.rtbSql.Text = sb.ToString();
        }

        private void SetSelectTable(object sender, EventArgs e)
        {
            SelectTableForm f2 = (SelectTableForm) sender;
            this.rtbIncludeTable.Text = string.Join(",", f2.SelectTables);
        }

        private void btnSelectTable_Click(object sender, EventArgs e)
        {
            string filePath = txtFilePath.Text;
            if (string.IsNullOrWhiteSpace(filePath))
            {
                MessageBox.Show("请选择doc文档!", "提示");
                return;
            }
            WordHelper.LoadWordData(filePath);
            SelectTableForm form=new SelectTableForm();
            form.Event+=new EventHandler(SetSelectTable);
            form.Show();
        }
    }
}
