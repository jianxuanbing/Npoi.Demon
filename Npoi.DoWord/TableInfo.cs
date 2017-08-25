/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：Npoi.DoWord
 * 文件名：TableInfo
 * 版本号：v1.0.0.0
 * 唯一标识：51e6286b-5857-4705-bbb6-973e5e174eef
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/7/6 10:51:45
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/7/6 10:51:45
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Npoi.DoWord
{
    /// <summary>
    /// 表信息
    /// </summary>
    public class TableInfo
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Explain { get; set; }

        /// <summary>
        /// 字段集合
        /// </summary>
        public List<FieldInfo> Fields { get; set; }
    }
}
