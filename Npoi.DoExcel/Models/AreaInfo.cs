using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Npoi.DoExcel.Models
{
    /// <summary>
    /// 行政区域信息
    /// </summary>
    public class AreaInfo
    {
        /// <summary>
        /// 系统编码
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 区域编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 上级ID
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 行政级别
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 区号
        /// </summary>
        public string AreaCode { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        public string Detail => GetDetail();

        /// <summary>
        /// 上级行政区域信息
        /// </summary>
        public AreaInfo ParentInfo { get; set; }

        /// <summary>
        /// 子列表
        /// </summary>
        public List<AreaInfo> ChildList { get; set; }=new List<AreaInfo>();

        /// <summary>
        /// 获取详细地址
        /// </summary>
        /// <returns></returns>
        protected string GetDetail()
        {
            if (ParentInfo == null)
            {
                return Name;
            }

            return ParentInfo.Detail + "," + Name;
        }
    }
}
