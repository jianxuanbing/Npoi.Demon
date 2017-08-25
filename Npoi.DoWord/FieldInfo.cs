/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：Npoi.DoWord
 * 文件名：CellInfo
 * 版本号：v1.0.0.0
 * 唯一标识：9b13dc34-4f1d-4bdc-9969-973c6cd91b41
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/7/6 10:52:55
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/7/6 10:52:55
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
    /// 字段信息
    /// </summary>
    public class FieldInfo
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 字段说明
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        public string IsRequired { get; set; }

        /// <summary>
        /// 参考
        /// </summary>
        public string Reference { get; set; }
    }
}
