using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Bing.DbGenerater;
using Bing.DbGenerater.Entities;
using Bing.DbGenerater.Interface;
using Bing.DbGenerater.Realization.SqlServer.DbMaintenance;

namespace Npoi.DoWord
{
    /// <summary>
    /// 数据库对比帮助类
    /// </summary>
    public class DbComparedHelper
    {
        /// <summary>
        /// 数据库维护中心
        /// </summary>
        public IDbMaintenance DbMaintenance { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 数据库字典
        /// </summary>
        public static Dictionary<string, Tuple<DbTableInfo, List<DbColumnInfo>>> DbDic =new Dictionary<string, Tuple<DbTableInfo, List<DbColumnInfo>>>();

        /// <summary>
        /// 文档数据
        /// </summary>
        public static List<TableInfo> DocData=new List<TableInfo>();

        /// <summary>
        /// 设置配置
        /// </summary>
        /// <param name="config"></param>
        public void SetConfig(Action<Config> config)
        {
            config?.Invoke(Config.Instance);
        }

        /// <summary>
        /// 初始化数据库字典
        /// </summary>
        private void InitDbDic()
        {
            if (DbMaintenance == null)
            {
                DbMaintenance=new SqlServerDbMaintenance();
            }
            var tableInfos = DbMaintenance.GetTableInfoList();
            foreach (var item in tableInfos)
            {
                var columns = DbMaintenance.GetColumnInfosByTableName(item.Name);
                DbDic.Add(item.Name, new Tuple<DbTableInfo, List<DbColumnInfo>>(item, columns));
            }
        }

        /// <summary>
        /// 初始化文档数据
        /// </summary>
        private void InitDocData()
        {
            DocData = WordHelper.ExcuteWord(FilePath);
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <returns></returns>
        public string Create()
        {
            if (!string.IsNullOrWhiteSpace(Config.Instance.DbConnection))
            {
                DbDic.Clear();
                InitDbDic();
            }
            InitDocData();
            StringBuilder sb=new StringBuilder();
            
            foreach (var item in DocData)
            {
                if (string.IsNullOrWhiteSpace(Config.Instance.DbConnection))
                {
                    GenerateTable(sb, item);
                    continue;
                }
                Tuple<DbTableInfo, List<DbColumnInfo>> table;
                var hasValue=DbDic.TryGetValue(item.Name,out table);
                if (!hasValue)
                {
                    GenerateTable(sb, item);
                }
                else
                {
                    GenerateAlterTableSql(sb, item, table);
                }
            }
            return sb.ToString();
        }

        private void GenerateTable(StringBuilder sb,TableInfo docTable)
        {
            if (Regex.IsMatch(docTable.Name, @"[\u4e00-\u9fa5]+", RegexOptions.IgnoreCase))
            {
                return;
            }
            sb.AppendFormat("/*创建 {0} 表开始*/", docTable.Name)
                .AppendLine();
            GenerateCreateTableConditionSql(sb, docTable.Name);
            GenerateCreateTableSql(sb,docTable);
            GenerateAddTableDescSql(sb,docTable);
            sb.AppendFormat("/*创建 {0} 表结束*/", docTable.Name)
                .AppendLine();
        }

        /// <summary>
        /// 生成创建表判断Sql
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="tableName"></param>
        private void GenerateCreateTableConditionSql(StringBuilder sb, string tableName)
        {
            sb.AppendLine("-- 判断是否有数据表存在，存在则删除");
            sb.AppendFormat(
                    "if exists (select * from dbo.sysobjects where id=object_id(N'{0}') and OBJECTPROPERTY(id,N'IsUserTable')=1)",
                    tableName)
                .AppendLine();
            sb.AppendFormat("\tdrop table {0}", tableName).AppendLine();
            sb.AppendLine("go").AppendLine();

            sb.AppendLine("-- 初始化开始");
            sb.AppendLine("set ansi_nulls on");
            sb.AppendLine("go");
            sb.AppendLine("set quoted_identifier on");
            sb.AppendLine("go");
            sb.AppendLine("set ansi_padding on");
            sb.AppendLine("go");
            sb.AppendLine("-- 初始化设置结束").AppendLine();
        }

        /// <summary>
        /// 生成创建表Sql
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="docTable"></param>
        private void GenerateCreateTableSql(StringBuilder sb, TableInfo docTable)
        {
            sb.AppendLine(string.Format("-- 开始创建数据库 {0} 表", docTable));
            sb.AppendFormat("create table {0} -- {1}", docTable.Name, docTable.Desc)
                .AppendLine()
                .AppendLine("(");
            var pk = docTable.Fields.FirstOrDefault();
            foreach (var column in docTable.Fields)
            {
                if (!string.IsNullOrWhiteSpace(column.Name))
                {
                    sb.AppendFormat("\t[{0}] {1} {2} {3}, -- {4}", column.Name, column.Type,
                            column.IsRequired == "N" ? "null" : "not null",
                            column.Desc == "ID" || (pk != null && column.Name == pk.Name) ? "primary key" : "",
                            column.Desc)
                        .AppendLine();
                }
            }
            sb.AppendLine(")");
            sb.AppendLine("set ansi_padding off");
            sb.AppendLine("go");
            sb.AppendLine(string.Format("-- 结束创建数据库 {0} 表", docTable));            
        }

        /// <summary>
        /// 生成添加表注释Sql
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="docTable"></param>
        private void GenerateAddTableDescSql(StringBuilder sb, TableInfo docTable)
        {
            sb.AppendLine(string.Format("-- 开始添加 {0} 表备注",docTable.Name));
            sb.AppendFormat(
                "exec sp_addextendedproperty N'MS_Description',N'{0}',N'user',N'dbo',N'table',N'{1}',default,default",
                docTable.Desc, docTable.Name).AppendLine();
            foreach (var column in docTable.Fields)
            {
                if (string.IsNullOrWhiteSpace(column.Name))
                {
                    continue;
                }
                GenerateAddColumnDescSql(sb, docTable.Name, column);
            }
            sb.AppendLine("go");
            sb.AppendLine(string.Format("-- 结束添加 {0} 表备注", docTable.Name));
        }

        /// <summary>
        /// 创建添加列注释Sql
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="tableName"></param>
        /// <param name="docColumn"></param>
        private void GenerateAddColumnDescSql(StringBuilder sb,string tableName, FieldInfo docColumn)
        {
            sb.AppendFormat("/* {0} - {1} */", tableName, docColumn.Name).AppendLine();
            sb.AppendFormat(
                    "exec sp_addextendedproperty N'MS_Description',N'{0}',N'user',N'dbo',N'table',N'{1}',N'column',N'{2}'",
                    docColumn.Desc, tableName, docColumn.Name)
                    .AppendLine();
        }

        /// <summary>
        /// 生成修改表Sql
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="docTable"></param>
        /// <param name="dbTable"></param>
        private void GenerateAlterTableSql(StringBuilder sb, TableInfo docTable,
            Tuple<DbTableInfo, List<DbColumnInfo>> dbTable)
        {
            sb.AppendFormat("-- 开始修改数据库 {0} 表", docTable.Name).AppendLine();
            var dbColumns = dbTable.Item2;
            foreach (var column in docTable.Fields)
            {
                var dbColumn = dbColumns.FirstOrDefault(x => x.DbColumnName == column.Name);
                // 创建数据列
                if (dbColumn==null)
                {
                    GenerateAddColumnSql(sb,docTable.Name,column);
                    GenerateAddColumnDescSql(sb, docTable.Name, column);
                }
                else
                {
                    var isNull = column.IsRequired == "N";
                    if (isNull != dbColumn.IsNullable ||
                        (!dbColumn.IsIdentity && !column.Type.StartsWith(dbColumn.DataType,
                             StringComparison.InvariantCultureIgnoreCase)) ||
                        (dbColumn.IsIdentity &&
                         !column.Type.StartsWith(dbColumn.DataType, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        GenerateAlterColumnSql(sb, docTable.Name, column);
                    }
                    if (!column.Desc.Equals(dbColumn.ColumnDescription, StringComparison.InvariantCultureIgnoreCase))
                    {
                        GenerateAlterColumnDescSql(sb, docTable.Name, column);
                    }
                }
            }

            if (docTable.Desc != dbTable.Item1.Description)
            {
                GenerateAlterTableDescSql(sb, docTable, string.IsNullOrWhiteSpace(dbTable.Item1.Description));
            }
            sb.AppendFormat("-- 结束修改数据库 {0} 表", docTable.Name).AppendLine();
        }

        /// <summary>
        /// 生成添加列Sql
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="tableName"></param>
        /// <param name="docColumn"></param>
        private void GenerateAddColumnSql(StringBuilder sb,string tableName, FieldInfo docColumn)
        {
            sb.AppendFormat("alter table {0} add {1} {2} {3}", tableName, docColumn.Name, docColumn.Type, "null")
                .AppendLine();
            if (docColumn.IsRequired == "Y")
            {
                sb.AppendLine("go");
                sb.AppendFormat("update {0} set {1} = {2} where {1} is null", tableName, docColumn.Name,
                    docColumn.GetDefaultValue()).AppendLine();
                sb.AppendLine("go");
                sb.AppendFormat("alter table {0} alter column {1} {2} {3}", tableName, docColumn.Name, docColumn.Type,
                    "not null").AppendLine();
                sb.AppendLine("go");
            }
        }

        /// <summary>
        /// 生成修改列Sql
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="tableName"></param>
        /// <param name="docColumn"></param>
        private void GenerateAlterColumnSql(StringBuilder sb, string tableName,  FieldInfo docColumn)
        {
            sb.AppendFormat("alter table {0} alter column {1} {2} {3}", tableName, docColumn.Name, docColumn.Type,
                docColumn.IsRequired == "N" ? "null" : "not null").AppendLine();
        }


        /// <summary>
        /// 生成修改表注释Sql
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="docTable"></param>
        /// <param name="isAdd"></param>
        private void GenerateAlterTableDescSql(StringBuilder sb, TableInfo docTable,bool isAdd)
        {
            sb.AppendFormat(
                    "exec {2} N'MS_Description',N'{0}',N'user',N'dbo',N'table',N'{1}',default,default",
                    docTable.Desc, docTable.Name, isAdd ? "sp_addextendedproperty" : "sp_updateextendedproperty")
                .AppendLine();
        }

        /// <summary>
        /// 生成修改列注释Sql
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="tableName"></param>
        /// <param name="docColumn"></param>
        private void GenerateAlterColumnDescSql(StringBuilder sb, string tableName, FieldInfo docColumn)
        {
            sb.AppendFormat("/* {0} - {1} */", tableName, docColumn.Name).AppendLine();
            sb.AppendFormat(
                    "exec sp_updateextendedproperty N'MS_Description',N'{0}',N'user',N'dbo',N'table',N'{1}',N'column',N'{2}'",
                    docColumn.Desc, tableName, docColumn.Name)
                .AppendLine();
        }
    }
}
