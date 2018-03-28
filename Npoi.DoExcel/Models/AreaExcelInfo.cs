using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Npoi.DoExcel.Models
{
    /// <summary>
    /// 行政区域Excel信息
    /// </summary>
    public class AreaExcelInfo
    {
        /// <summary>
        /// 行政区域代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 省份名
        /// </summary>
        public string StateName { get; set; }

        /// <summary>
        /// 城市名
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// 区名
        /// </summary>
        public string DistrictName { get; set; }

        /// <summary>
        /// 区号
        /// </summary>
        public string AreaCode { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// 行政级别
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// 完整地址
        /// </summary>
        public string Detail => GetFullName();

        /// <summary>
        /// 获取完整名称
        /// </summary>
        /// <returns></returns>
        public string GetFullName()
        {
            if (!string.IsNullOrWhiteSpace(DistrictName))
            {
                return StateName + "," + CityName + "," + DistrictName;
            }
            if (!string.IsNullOrWhiteSpace(CityName))
            {
                return StateName + "," + CityName;
            }
            if (!string.IsNullOrWhiteSpace(StateName))
            {
                return StateName;
            }

            return string.Empty;
        }
    }
}
