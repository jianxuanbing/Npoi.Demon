using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Npoi.DoWord.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<string> sqls=new List<string>();
            //sqls = WordHelper.GenerateCreateTableSql(@"D:\紫云来数据库设计.docx", "StaMemberOrder", "StaGoodsOrder", "StaDayData");
            //sqls = WordHelper.GenerateCreateTableSql(@"D:\西域美农数据库设计.docx");
            //sqls = WordHelper.GenerateCreateTableSql(@"D:\公司官网概要设计.docx");
            sqls = WordHelper.GenerateCreateTableSql(@"D:\数据库设计.docx", "SysMerchantGroup", "SysTrade", "Region", "SysMerchant", "SysModule", "SysMenu", "SysRole", "SysRoleFunc", "SysLogin", "SysLoginRole");
            foreach (string sql in sqls)
            {
                Console.WriteLine(sql);
            }
        }

        [TestMethod]
        public void TestMethod2()
        {
            var result = WordHelper.GenerateAddColumnSql("BasGoodsDetail", "BusinessFlag", "int", "业务类型,0:普通商品,1:拍卖商品", true);
            Console.WriteLine(result);
        }

        [TestMethod]
        public void Test_GenerateDoc()
        {
            DbComparedHelper compared=new DbComparedHelper();
            compared.FilePath = @"";
            compared.SetConfig(config =>
            {
                config.DbConnection = "";
                config.DbName = "";
                config.ProviderName = "";
            });
            var result=compared.Create();
            Console.WriteLine(result);
        }
    }
}
