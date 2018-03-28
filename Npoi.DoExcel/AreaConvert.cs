using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npoi.DoExcel.Models;

namespace Npoi.DoExcel
{
    /// <summary>
    /// 区域转换
    /// </summary>
    public class AreaConvert
    {
        /// <summary>
        /// Excel区域 转换 区域结构
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<AreaInfo> To(List<AreaExcelInfo> source)
        {
            //source = source.Where(x => x.StateName == "河南省").ToList();
            List<AreaInfo> list=new List<AreaInfo>();
            InitTree(list,source,1,null,null,null);            
            return list;
        }

        /// <summary>
        /// 生成插入Sql
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string BuildInsertSql(List<AreaInfo> source)
        {
            StringBuilder sb=new StringBuilder();
            foreach (var areaInfo in source)
            {
                sb.AppendFormat(Const.INSERT_TEMP_REGION_SQL, areaInfo.Id, areaInfo.Name, areaInfo.Detail,
                    areaInfo.Code, areaInfo.ParentId == null ? "null" : $"'{areaInfo.ParentId}'", areaInfo.Level,
                    areaInfo.ZipCode, areaInfo.AreaCode, 1, "系统初始化",
                    "");
                sb.AppendLine();
            }

            return sb.ToString();
        }

        private static void InitTree(List<AreaInfo> list, List<AreaExcelInfo> source, int level,Guid? parentId=null,AreaInfo parentInfo=null,List<AreaInfo> childList=null)
        {
            var levelStr = level.ToString();

            var currentLevelList = source.Where(x => x.Level == levelStr).ToList();            

            if (!currentLevelList.Any())
            {
                return;
            }

            if (parentInfo != null)
            {
                currentLevelList = currentLevelList.Where(x => x.Detail.StartsWith(parentInfo.Detail)).ToList();
                //if (level == 2)
                //{
                //    currentLevelList = currentLevelList.Where(x => x.GetFullName() == parentInfo.Detail).ToList();
                //}
                //else if (level == 3)
                //{
                //    currentLevelList = currentLevelList.Where(x => x.CityName == parentInfo.Name).ToList();
                //}
                //else if (level == 4)
                //{
                //    currentLevelList = currentLevelList.Where(x => x.DistrictName == parentInfo.Name).ToList();
                //}
            }

            currentLevelList.ForEach(x =>
            {
                AreaInfo area=new AreaInfo();
                area.Id = ParseCode(x.Code);
                area.Code = x.Code;
                area.Name = GetName(x);
                area.ParentId = parentId;
                area.Level = level;
                area.AreaCode = x.AreaCode;
                area.ZipCode = x.ZipCode;
                area.ParentInfo = parentInfo;                
                list.Add(area);
                InitTree(list, source, area.Level + 1, area.Id, area, area.ChildList);
                childList?.Add(area);
                
            });

        }

        private static Guid ParseCode(string code)
        {
            return new Guid(string.Format("00000000-0000-0000-0000-000000{0}", code));
        }

        private static string GetName(AreaExcelInfo source)
        {
            if (source.Level == "1")
            {
                return source.StateName;
            }

            if (source.Level == "2")
            {
                return source.CityName;
            }

            if (source.Level == "3")
            {
                return source.DistrictName;
            }

            return string.Empty;
        }        
    }
}
