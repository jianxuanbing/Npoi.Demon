using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npoi.DoExcel.Models;
using NPOI.Extension;

namespace Npoi.DoExcel.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void Init()
        {
            ImportConfig.Init();
        }

        [TestMethod]
        public void TestMethod1()
        {            
            var list = Excel.Load<AreaExcelInfo>(@"D:\\China.xlsx").ToList();

            var result = AreaConvert.To(list);
            // 验证数据完整性
            foreach (var item in list)
            {
                if (result.All(x => x.Detail != item.GetFullName()))
                {
                    Console.WriteLine(item.GetFullName());
                }
            }
        }

        [TestMethod]
        public void Test_BuildInsertSql()
        {
            var list = Excel.Load<AreaExcelInfo>(@"D:\\China.xlsx").ToList();

            var convertResult = AreaConvert.To(list);

            var sql = AreaConvert.BuildInsertSql(convertResult);
            Console.WriteLine(sql);
        }
    }
}
