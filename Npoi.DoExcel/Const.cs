using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Npoi.DoExcel
{
    internal class Const
    {
        /// <summary>
        /// 临时行政区域创表Sql
        /// </summary>
        public const string TEMP_REGION_CREATE_TABLE_SQL = @"/*创建TempRegion表开始*/
--判断是否有数据表存在，存在则删除
if exists (select * from dbo.sysobjects where id=object_id(N'TempRegion') and OBJECTPROPERTY(id,N'IsUserTable')=1)
	drop table TempRegion
go

--初始化开始
set ansi_nulls on
go
set quoted_identifier on
go
set ansi_padding on
go
--初始化设置结束

--开始创建数据库表
create table TempRegion --地区
(
	[ID] uniqueidentifier not null primary key, -- 系统编号
	[Name] nvarchar(64) not null , -- 名称
    [MergerName] nvarchar(100) not null , -- 合并名称
	[Code] nvarchar(64) null , -- 编码
	[ParentID] uniqueidentifier null , -- 上级ID，第一级为null
	[Level] int not null , -- 级别。0 国家，1省，2市，3区
	[ZipCode] varchar(10) null, -- 邮政编码
	[AreaCode] varchar(10) null, -- 区号
	[Status] int not null , -- 状态。1正常，0禁用
	[Creater] nvarchar(64) not null , -- 创建人
	[CreateTime] datetime not null , -- 创建时间
	[Editor] nvarchar(64) not null , -- 编辑人
	[EditTime] datetime not null , -- 编辑时间
	[Note] nvarchar(255) null , -- 备注
)
set ansi_padding off
go
--给表添加备注
/*TempRegion*/
exec sp_addextendedproperty N'MS_Description',N'行政区域',N'user',N'dbo',N'table',N'TempRegion',default,default--给列添加备注
/*ID*/
exec sp_addextendedproperty N'MS_Description',N'系统编号',N'user',N'dbo',N'table',N'TempRegion',N'column',N'ID'
/*Name*/
exec sp_addextendedproperty N'MS_Description',N'名称',N'user',N'dbo',N'table',N'TempRegion',N'column',N'Name'
/*MergerName*/
exec sp_addextendedproperty N'MS_Description',N'合并名称',N'user',N'dbo',N'table',N'TempRegion',N'column',N'MergerName'
/*Code*/
exec sp_addextendedproperty N'MS_Description',N'编码',N'user',N'dbo',N'table',N'TempRegion',N'column',N'Code'
/*ParentID*/
exec sp_addextendedproperty N'MS_Description',N'上级地区ID，第一级为null',N'user',N'dbo',N'table',N'TempRegion',N'column',N'ParentID'
/*Level*/
exec sp_addextendedproperty N'MS_Description',N'级别。0国家，1省，2市，3区',N'user',N'dbo',N'table',N'TempRegion',N'column',N'Level'
/*ZipCode*/
exec sp_addextendedproperty N'MS_Description',N'邮政编码',N'user',N'dbo',N'table',N'TempRegion',N'column',N'ZipCode'
/*AreaCode*/
exec sp_addextendedproperty N'MS_Description',N'区号',N'user',N'dbo',N'table',N'TempRegion',N'column',N'AreaCode'
/*Status*/
exec sp_addextendedproperty N'MS_Description',N'状态。1正常，0禁用',N'user',N'dbo',N'table',N'TempRegion',N'column',N'Status'
/*Creater*/
exec sp_addextendedproperty N'MS_Description',N'创建人',N'user',N'dbo',N'table',N'TempRegion',N'column',N'Creater'
/*CreateTime*/
exec sp_addextendedproperty N'MS_Description',N'创建时间',N'user',N'dbo',N'table',N'TempRegion',N'column',N'CreateTime'
/*Editor*/
exec sp_addextendedproperty N'MS_Description',N'编辑人',N'user',N'dbo',N'table',N'TempRegion',N'column',N'Editor'
/*EditTime*/
exec sp_addextendedproperty N'MS_Description',N'编辑时间',N'user',N'dbo',N'table',N'TempRegion',N'column',N'EditTime'
/*Note*/
exec sp_addextendedproperty N'MS_Description',N'备注',N'user',N'dbo',N'table',N'TempRegion',N'column',N'Note'
go
/*创建TempRegion表结束*/";

        /// <summary>
        /// 插入临时区域表Sql
        /// </summary>
        public const string INSERT_TEMP_REGION_SQL = @"insert into TempRegion(ID, Name, MergerName, Code, ParentID, Level, ZipCode, AreaCode, Status, Creater, CreateTime, Editor, EditTime, Note)values('{0}', N'{1}', N'{2}', N'{3}', {4}, {5}, '{6}', '{7}', {8}, N'{9}', GETDATE(), N'{9}', GETDATE(), N'{10}')";
    }
}
