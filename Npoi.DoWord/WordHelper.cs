/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：Npoi.DoWord
 * 文件名：WordHelper
 * 版本号：v1.0.0.0
 * 唯一标识：66b177c7-c003-45d5-ab40-570c0322edd4
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/7/6 10:35:16
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/7/6 10:35:16
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NPOI.XWPF.UserModel;

namespace Npoi.DoWord
{
    /// <summary>
    /// Word帮助类
    /// </summary>
    public class WordHelper
    {
        public static List<TableInfo> Data=new List<TableInfo>();

        /// <summary>
        /// 加载Word数据
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static void LoadWordData(string filePath)
        {
            Data = ExcuteWord(filePath);
        }

        /// <summary>
        /// 获取表列表
        /// </summary>
        /// <returns></returns>
        public static List<string> GetTableList()
        {
            return Data.Select(x => x.Name).ToList();
        }

        /// <summary>
        /// 获取表列表
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static List<string> GetTableList(string filePath)
        {
            if (Data.Count == 0)
            {
                Data = ExcuteWord(filePath);
            }            
            return Data.Select(x => x.Name).ToList();
        }

        /// <summary>
        /// 提取Word文档
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static List<TableInfo> ExcuteWord(string filePath)
        {            
            List<TableInfo> tableList=new List<TableInfo>();
            using (FileStream stream=File.OpenRead(filePath))
            {
                XWPFDocument doc=new XWPFDocument(stream);
                var tables = doc.Tables;
                //遍历表格
                foreach (var table in tables)
                {
                    TableInfo tableInfo=new TableInfo();                    
                    List<FieldInfo> fields = new List<FieldInfo>();
                    //遍历行
                    var rows = table.Rows;
                    string copyTabelName = string.Empty;
                    for (int i = 0; i < rows.Count; i++)
                    {                        
                        FieldInfo field = new FieldInfo();
                        //遍历列
                        var cells = rows[i].GetTableCells();
                        for (int j = 0; j < cells.Count; j++)
                        {                            
                            var paras = cells[j].Paragraphs;
                            StringBuilder sb = new StringBuilder();
                            foreach (var para in paras)
                            {                                
                                string text = para.ParagraphText.Trim();
                                sb.Append(text);
                            }
                            if (i == 0 && j == 1)
                            {
                                string name = sb.ToString();
                                string[] multiTable = name.Split('/');
                                if (multiTable.Length > 1)
                                {
                                    copyTabelName = multiTable[1].Trim();
                                }
                                tableInfo.Name = multiTable[0].Trim();
                            }
                            else if (i == 1 && j == 1)
                            {
                                tableInfo.Desc = sb.ToString();
                            }
                            else if (i == 2 && j == 1)
                            {
                                tableInfo.Explain = sb.ToString();
                            }
                            else if(i>3)
                            {
                                if (j == 0)
                                {
                                    field.Name = sb.ToString();
                                }
                                else if (j == 1)
                                {
                                    field.Type = sb.ToString().ToLower();
                                }
                                else if (j == 2)
                                {
                                    field.Desc = sb.ToString();
                                }
                                else if (j == 3)
                                {
                                    field.IsRequired = sb.ToString();
                                }
                                else if (j == 4)
                                {
                                    field.Reference = sb.ToString();
                                }
                            }
                        }
                        if (i > 3)
                        {
                            field.InitDecimal();
                            fields.Add(field);
                        }                        
                    }                    
                    tableInfo.Fields = fields;
                    tableList.Add(tableInfo);
                    //解决复制表问题
                    if (!string.IsNullOrEmpty(copyTabelName))
                    {
                        TableInfo copyTable=new TableInfo();
                        copyTable.Name = copyTabelName;
                        copyTable.Desc = tableInfo.Desc;
                        copyTable.Explain = tableInfo.Explain;
                        copyTable.Fields = tableInfo.Fields;                        
                        tableList.Add(copyTable);
                        copyTabelName = string.Empty;
                    }
                }
                return tableList;
            }            
        }

        /// <summary>
        /// 获取Json
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static string GetJson(string filePath)
        {
            var tables = ExcuteWord(filePath);
            return JsonConvert.SerializeObject(tables);
        }

        /// <summary>
        /// 生成创表sql
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="include">包含表</param>
        /// <returns></returns>
        public static List<string> GenerateCreateTableSql(string filePath,params string[] include)
        {
            List<string> sqls=new List<string>();
            if (Data.Count == 0)
            {
                Data = ExcuteWord(filePath);
            }
            var tables = Data;

            foreach (var table in tables)
            {
                if (include.Length > 0)
                {
                    if (include.All(x => table.Name != x))
                    {
                        continue;
                    }
                }
                if (Regex.IsMatch(table.Name, @"[\u4e00-\u9fa5]+", RegexOptions.IgnoreCase))
                {
                    continue;
                }
                StringBuilder sb=new StringBuilder();
                //写表信息
                sb.AppendFormat("/*创建{0}表开始*/\r\n", table.Name);
                sb.Append("--判断是否有数据表存在，存在则删除\r\n");
                sb.AppendFormat(
                    "if exists (select * from dbo.sysobjects where id=object_id(N'{0}') and OBJECTPROPERTY(id,N'IsUserTable')=1)\r\n\t",
                    table.Name);
                sb.AppendFormat("drop table {0}\r\n", table.Name);
                sb.Append("go\r\n\r\n");
                sb.Append("--初始化开始\r\n");
                sb.Append("set ansi_nulls on\r\ngo\r\n");
                sb.Append("set quoted_identifier on\r\ngo\r\n");
                sb.Append("set ansi_padding on\r\ngo\r\n");
                sb.Append("--初始化设置结束\r\n\r\n");
                sb.Append("--开始创建数据库表\r\n");
                sb.AppendFormat("create table {0} --{1}\r\n(\r\n", table.Name, table.Desc);
                //写字段
                var pk = table.Fields.FirstOrDefault();
                foreach (var field in table.Fields)
                {
                    if (!string.IsNullOrEmpty(field.Name))
                    {
                        sb.AppendFormat("\t[{0}] {1} {2} {3}, --{4}\n", field.Name, field.Type,
                            field.IsRequired == "N" ? "null" : "not null",
                            field.Desc == "ID" || (pk != null && field.Name == pk.Name) ? "primary key" : "", field.Desc);
                    }                    
                }
                sb.Append(")\r\nset ansi_padding off\r\ngo\r\n");
                //写备注
                sb.Append("--给表添加备注\n");
                sb.AppendFormat("/*{0}*/\n", table.Name);
                sb.AppendFormat(
                    "exec sp_addextendedproperty N'MS_Description',N'{0}',N'user',N'dbo',N'table',N'{1}',default,default",
                    table.Desc, table.Name);
                sb.Append("--给列添加备注\n");
                foreach (var field in table.Fields)
                {
                    if (!string.IsNullOrEmpty(field.Name))
                    {
                        sb.AppendFormat("/*{0}*/\n", field.Name);
                        sb.AppendFormat(
                            "exec sp_addextendedproperty N'MS_Description',N'{0}',N'user',N'dbo',N'table',N'{1}',N'column',N'{2}'\n",
                            field.Desc, table.Name, field.Name);
                    }                    
                }
                sb.Append("go\n");
                sb.AppendFormat("/*创建{0}表结束*/", table.Name);
                sqls.Add(sb.ToString());
            }

            return sqls;
        }

        /// <summary>
        /// 生成添加列Sql
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static List<string> GenerateAddColumnSql(string filePath)
        {
            List<string> sqls = new List<string>();
            if (Data.Count == 0)
            {
                Data = ExcuteWord(filePath);
            }
            var tables = Data;
            foreach (var table in tables)
            {
                StringBuilder sb=new StringBuilder();
                foreach (var field in table.Fields)
                {
                    sb.AppendFormat("if COL_LENGTH('{0}','{1}') is not null\r\n", table.Name, field.Name);
                    sb.Append("\t");
                    sb.AppendFormat("alter table {0} add {1} {2} {3}\n", table.Name, field.Name, field.Type,
                        field.IsRequired == "N" ? "null" : "not null");
                    sb.Append("\t");
                    sb.AppendFormat(
                        "exec sp_addextendedproperty N'MS_Description',N'{0}',N'user',N'dbo',N'table',N'{1}',N'column',N'{2}'\n",
                        field.Desc, table.Name, field.Name);
                }
                sqls.Add(sb.ToString());
            }

            return sqls;
        }

        /// <summary>
        /// 生成添加列Sql
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="type">字段类型</param>
        /// <param name="desc">备注</param>
        /// <param name="isNull">是否为空</param>
        /// <returns></returns>
        public static string GenerateAddColumnSql(string tableName, string fieldName, string type, string desc,
            bool isNull)
        {
            StringBuilder sb=new StringBuilder();

            sb.AppendFormat("if COL_LENGTH('{0}','{1}') is not null\r\n", tableName, fieldName);
            sb.Append("\t");
            sb.AppendFormat("alter table {0} add {1} {2} {3}\n", tableName, fieldName, type,
                isNull? "null" : "not null");
            sb.Append("\t");
            sb.AppendFormat(
                "exec sp_addextendedproperty N'MS_Description',N'{0}',N'user',N'dbo',N'table',N'{1}',N'column',N'{2}'\n",
                desc, tableName, fieldName);

            return sb.ToString();
        }

        /// <summary>
        /// 生成添加备注Sql
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static List<string> GenerateAddDescSql(string filePath)
        {
            List<string> sqls = new List<string>();
            if (Data.Count == 0)
            {
                Data = ExcuteWord(filePath);
            }
            var tables = Data;
            foreach (var table in tables)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var field in table.Fields)
                {
                    string execSql =
                        string.Format(
                            "exec sp_addextendedproperty N'MS_Description',N'{0}',N'user',N'dbo',N'table',N'{1}',N'column',N'{2}'",
                            field.Desc, table.Name, field.Name);
                    //sb.AppendFormat("if col_lenght('{0}','{1}') is not null\r\n", table.Name, field.Name);
                    //sb.Append("\t");
                    sb.Append("begin try\n");
                    sb.Append("\t");
                    sb.Append(execSql+"\n");
                    sb.Append("end try\n");
                    sb.Append("begin catch\n");
                    sb.Append("\t");
                    sb.AppendFormat("print '{0}'\n",execSql.Replace("'",""));
                    sb.Append("end catch\n");
                }
                sqls.Add(sb.ToString());
            }

            return sqls;
        }

        
    }
}
