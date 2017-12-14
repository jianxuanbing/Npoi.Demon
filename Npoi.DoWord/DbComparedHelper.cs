using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public static IDbMaintenance DbMaintenance { get; set; }

        /// <summary>
        /// 数据库字典
        /// </summary>
        public static Dictionary<string, Tuple<DbTableInfo, List<DbColumnInfo>>> DbDic =new Dictionary<string, Tuple<DbTableInfo, List<DbColumnInfo>>>();

        /// <summary>
        /// 设置配置
        /// </summary>
        /// <param name="config"></param>
        public static void SetConfig(Action<Config> config)
        {
            config?.Invoke(Config.Instance);
        }

        /// <summary>
        /// 初始化数据库字典
        /// </summary>
        public static void InitDbDic()
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
    }
}
