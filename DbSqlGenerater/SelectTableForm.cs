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
    public partial class SelectTableForm : Form
    {        
        public List<string> SelectTables { get; set; }

        public event EventHandler Event;
        public SelectTableForm()
        {
            InitializeComponent();
            SelectTables=new List<string>();
            InitTable();
        }

        private void InitTable()
        {
            this.clbTable.DataSource = WordHelper.GetTableList();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            List<string> selectList=new List<string>();
            for (int i = 0; i < this.clbTable.Items.Count; i++)
            {
                if (this.clbTable.GetItemChecked(i))
                {
                    selectList.Add(this.clbTable.GetItemText(this.clbTable.Items[i]));
                }
            }
            SelectTables = selectList;
            Event?.Invoke(this,EventArgs.Empty);
            this.Close();
        }
    }
}
