using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npoi.DoExcel.Models;
using NPOI.Extension;

namespace Npoi.DoExcel
{
    /// <summary>
    /// 导入配置
    /// </summary>
    public class ImportConfig
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            InitAreaExcelInfo();
        }

        /// <summary>
        /// 初始化区域Excel信息
        /// </summary>
        private static void InitAreaExcelInfo()
        {
            var fc = Excel.Setting.For<AreaExcelInfo>();
            fc.Property(x => x.Code).HasExcelIndex(0).HasExcelTitle("行政区划代码");
            fc.Property(x => x.StateName).HasExcelIndex(1).HasExcelTitle("省级");
            fc.Property(x => x.CityName).HasExcelIndex(2).HasExcelTitle("地级");
            fc.Property(x => x.DistrictName).HasExcelIndex(3).HasExcelTitle("县级");
            fc.Property(x => x.AreaCode).HasExcelIndex(4).HasExcelTitle("区号");
            fc.Property(x => x.ZipCode).HasExcelIndex(5).HasExcelTitle("邮编");
            fc.Property(x => x.Level).HasExcelIndex(6).HasExcelTitle("行政级别");

        }
    }
}
