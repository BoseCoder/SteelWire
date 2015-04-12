/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     2015/4/14 星期二 3:05:41                        */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CumulationReset') and o.name = 'FK_CUMULATI_RELATIONS_USER')
alter table CumulationReset
   drop constraint FK_CUMULATI_RELATIONS_USER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CuttingCriticalConfig') and o.name = 'FK_CUTCONF_RELATIONS_USER')
alter table CuttingCriticalConfig
   drop constraint FK_CUTCONF_RELATIONS_USER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CuttingCriticalConfig') and o.name = 'FK_CUTCONF_RELATIONS_CUTDIC')
alter table CuttingCriticalConfig
   drop constraint FK_CUTCONF_RELATIONS_CUTDIC
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CuttingCriticalDictionary') and o.name = 'FK_CUTDIC_RELATIONS_USER')
alter table CuttingCriticalDictionary
   drop constraint FK_CUTDIC_RELATIONS_USER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('DrillCollarConfig') and o.name = 'FK_DRILLCOL_RELATIONS_WORKCONF')
alter table DrillCollarConfig
   drop constraint FK_DRILLCOL_RELATIONS_WORKCONF
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('DrillPipeConfig') and o.name = 'FK_DRILLPIP_RELATIONS_WORKCONF')
alter table DrillPipeConfig
   drop constraint FK_DRILLPIP_RELATIONS_WORKCONF
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('DrillingDifficulty') and o.name = 'FK_DRILDIFF_RELATIONS_WORKDIC')
alter table DrillingDifficulty
   drop constraint FK_DRILDIFF_RELATIONS_WORKDIC
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('DrillingType') and o.name = 'FK_DRILTYPE_RELATIONS_WORKDIC')
alter table DrillingType
   drop constraint FK_DRILTYPE_RELATIONS_WORKDIC
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Machine') and o.name = 'FK_MACHINE_RELATIONS_USER')
alter table Machine
   drop constraint FK_MACHINE_RELATIONS_USER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('WireropeCutRole') and o.name = 'FK_CUTROLE_RELATIONS_CUTDIC')
alter table WireropeCutRole
   drop constraint FK_CUTROLE_RELATIONS_CUTDIC
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('WireropeEfficiency') and o.name = 'FK_EFFIC_RELATIONS_CUTDIC')
alter table WireropeEfficiency
   drop constraint FK_EFFIC_RELATIONS_CUTDIC
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('WireropeWorkload') and o.name = 'FK_WORKLOAD_RELATIONS_CUTDIC')
alter table WireropeWorkload
   drop constraint FK_WORKLOAD_RELATIONS_CUTDIC
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('WorkConfig') and o.name = 'FK_WORKCONF_RELATIONS_USER')
alter table WorkConfig
   drop constraint FK_WORKCONF_RELATIONS_USER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('WorkConfig') and o.name = 'FK_WORKCONF_RELATIONS_WORKDIC')
alter table WorkConfig
   drop constraint FK_WORKCONF_RELATIONS_WORKDIC
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('WorkDictionary') and o.name = 'FK_WORKDIC_RELATIONS_USER')
alter table WorkDictionary
   drop constraint FK_WORKDIC_RELATIONS_USER
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CumulationReset')
            and   name  = 'Relationship_14_FK'
            and   indid > 0
            and   indid < 255)
   drop index CumulationReset.Relationship_14_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CumulationReset')
            and   type = 'U')
   drop table CumulationReset
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CuttingCriticalConfig')
            and   name  = 'Relationship_11_FK'
            and   indid > 0
            and   indid < 255)
   drop index CuttingCriticalConfig.Relationship_11_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CuttingCriticalConfig')
            and   name  = 'Relationship_7_FK'
            and   indid > 0
            and   indid < 255)
   drop index CuttingCriticalConfig.Relationship_7_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CuttingCriticalConfig')
            and   type = 'U')
   drop table CuttingCriticalConfig
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CuttingCriticalDictionary')
            and   name  = 'Relationship_10_FK'
            and   indid > 0
            and   indid < 255)
   drop index CuttingCriticalDictionary.Relationship_10_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CuttingCriticalDictionary')
            and   type = 'U')
   drop table CuttingCriticalDictionary
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('DrillCollarConfig')
            and   name  = 'Relationship_6_FK'
            and   indid > 0
            and   indid < 255)
   drop index DrillCollarConfig.Relationship_6_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DrillCollarConfig')
            and   type = 'U')
   drop table DrillCollarConfig
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('DrillPipeConfig')
            and   name  = 'Relationship_9_FK'
            and   indid > 0
            and   indid < 255)
   drop index DrillPipeConfig.Relationship_9_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DrillPipeConfig')
            and   type = 'U')
   drop table DrillPipeConfig
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('DrillingDifficulty')
            and   name  = 'Relationship_5_FK'
            and   indid > 0
            and   indid < 255)
   drop index DrillingDifficulty.Relationship_5_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DrillingDifficulty')
            and   type = 'U')
   drop table DrillingDifficulty
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('DrillingType')
            and   name  = 'Relationship_4_FK'
            and   indid > 0
            and   indid < 255)
   drop index DrillingType.Relationship_4_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DrillingType')
            and   type = 'U')
   drop table DrillingType
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Machine')
            and   name  = 'Relationship_15_FK'
            and   indid > 0
            and   indid < 255)
   drop index Machine.Relationship_15_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Machine')
            and   type = 'U')
   drop table Machine
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SecurityUser')
            and   type = 'U')
   drop table SecurityUser
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('WireropeCutRole')
            and   name  = 'Relationship_2_FK'
            and   indid > 0
            and   indid < 255)
   drop index WireropeCutRole.Relationship_2_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WireropeCutRole')
            and   type = 'U')
   drop table WireropeCutRole
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('WireropeEfficiency')
            and   name  = 'Relationship_3_FK'
            and   indid > 0
            and   indid < 255)
   drop index WireropeEfficiency.Relationship_3_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WireropeEfficiency')
            and   type = 'U')
   drop table WireropeEfficiency
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('WireropeWorkload')
            and   name  = 'Relationship_1_FK'
            and   indid > 0
            and   indid < 255)
   drop index WireropeWorkload.Relationship_1_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WireropeWorkload')
            and   type = 'U')
   drop table WireropeWorkload
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('WorkConfig')
            and   name  = 'Relationship_13_FK'
            and   indid > 0
            and   indid < 255)
   drop index WorkConfig.Relationship_13_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('WorkConfig')
            and   name  = 'Relationship_8_FK'
            and   indid > 0
            and   indid < 255)
   drop index WorkConfig.Relationship_8_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WorkConfig')
            and   type = 'U')
   drop table WorkConfig
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('WorkDictionary')
            and   name  = 'Relationship_12_FK'
            and   indid > 0
            and   indid < 255)
   drop index WorkDictionary.Relationship_12_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WorkDictionary')
            and   type = 'U')
   drop table WorkDictionary
go

/*==============================================================*/
/* Table: CumulationReset                                       */
/*==============================================================*/
create table CumulationReset (
   ID                   int                  identity,
   UpdateUserID         int                  not null,
   CriticalValue        decimal(18,8)        not null,
   CumulationValue      decimal(18,8)        not null,
   ResetValue           decimal(18,8)        not null,
   RemainValue          decimal(18,8)        not null,
   UpdateTime           datetime             not null,
   constraint PK_CUMULATIONRESET primary key nonclustered (ID)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '累积重置记录',
   'user', @CurrentUser, 'table', 'CumulationReset'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ID',
   'user', @CurrentUser, 'table', 'CumulationReset', 'column', 'ID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'UpdateUserID',
   'user', @CurrentUser, 'table', 'CumulationReset', 'column', 'UpdateUserID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '切绳临界值',
   'user', @CurrentUser, 'table', 'CumulationReset', 'column', 'CriticalValue'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '切绳累积值',
   'user', @CurrentUser, 'table', 'CumulationReset', 'column', 'CumulationValue'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '重置值',
   'user', @CurrentUser, 'table', 'CumulationReset', 'column', 'ResetValue'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '剩余值',
   'user', @CurrentUser, 'table', 'CumulationReset', 'column', 'RemainValue'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'CumulationReset', 'column', 'UpdateTime'
go

/*==============================================================*/
/* Index: Relationship_14_FK                                    */
/*==============================================================*/
create index Relationship_14_FK on CumulationReset (
UpdateUserID ASC
)
go

/*==============================================================*/
/* Table: CuttingCriticalConfig                                 */
/*==============================================================*/
create table CuttingCriticalConfig (
   ID                   int                  identity,
   DictionaryID         int                  not null,
   ConfigUserID         int                  not null,
   DerrickHeight        decimal(18,8)        not null,
   WirelineMaxPower     decimal(18,8)        not null,
   RotaryHookWorkload   decimal(18,8)        not null,
   RollerDiameter       decimal(18,8)        not null,
   WirelineDiameter     decimal(18,8)        not null,
   RopeCount            int                  not null,
   ConfigTime           datetime             not null,
   CuttingCriticalValue decimal(18,8)        not null,
   constraint PK_CUTTINGCRITICALCONFIG primary key nonclustered (ID)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '切绳临界值当前配置',
   'user', @CurrentUser, 'table', 'CuttingCriticalConfig'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ID',
   'user', @CurrentUser, 'table', 'CuttingCriticalConfig', 'column', 'ID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'DictionaryID',
   'user', @CurrentUser, 'table', 'CuttingCriticalConfig', 'column', 'DictionaryID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ConfigUserID',
   'user', @CurrentUser, 'table', 'CuttingCriticalConfig', 'column', 'ConfigUserID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '井架高度',
   'user', @CurrentUser, 'table', 'CuttingCriticalConfig', 'column', 'DerrickHeight'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '破断拉力',
   'user', @CurrentUser, 'table', 'CuttingCriticalConfig', 'column', 'WirelineMaxPower'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '大钩荷载',
   'user', @CurrentUser, 'table', 'CuttingCriticalConfig', 'column', 'RotaryHookWorkload'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '滚筒直径',
   'user', @CurrentUser, 'table', 'CuttingCriticalConfig', 'column', 'RollerDiameter'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '钢丝绳直径',
   'user', @CurrentUser, 'table', 'CuttingCriticalConfig', 'column', 'WirelineDiameter'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '承载绳根数',
   'user', @CurrentUser, 'table', 'CuttingCriticalConfig', 'column', 'RopeCount'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '配置时间',
   'user', @CurrentUser, 'table', 'CuttingCriticalConfig', 'column', 'ConfigTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '临界值',
   'user', @CurrentUser, 'table', 'CuttingCriticalConfig', 'column', 'CuttingCriticalValue'
go

/*==============================================================*/
/* Index: Relationship_7_FK                                     */
/*==============================================================*/
create index Relationship_7_FK on CuttingCriticalConfig (
DictionaryID ASC
)
go

/*==============================================================*/
/* Index: Relationship_11_FK                                    */
/*==============================================================*/
create index Relationship_11_FK on CuttingCriticalConfig (
ConfigUserID ASC
)
go

/*==============================================================*/
/* Table: CuttingCriticalDictionary                             */
/*==============================================================*/
create table CuttingCriticalDictionary (
   ID                   int                  identity,
   ConfigUserID         int                  not null,
   ConfigTime           datetime             not null,
   constraint PK_CUTTINGCRITICALDICTIONARY primary key nonclustered (ID)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '切绳临界值配置字典',
   'user', @CurrentUser, 'table', 'CuttingCriticalDictionary'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ID',
   'user', @CurrentUser, 'table', 'CuttingCriticalDictionary', 'column', 'ID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ConfigUserID',
   'user', @CurrentUser, 'table', 'CuttingCriticalDictionary', 'column', 'ConfigUserID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '配置时间',
   'user', @CurrentUser, 'table', 'CuttingCriticalDictionary', 'column', 'ConfigTime'
go

/*==============================================================*/
/* Index: Relationship_10_FK                                    */
/*==============================================================*/
create index Relationship_10_FK on CuttingCriticalDictionary (
ConfigUserID ASC
)
go

/*==============================================================*/
/* Table: DrillCollarConfig                                     */
/*==============================================================*/
create table DrillCollarConfig (
   ID                   int                  identity,
   WorkConfigID         int                  not null,
   DrillCollarName      varchar(50)          not null,
   DrillCollarWeight    decimal(18,8)        not null,
   DrillCollarLength    decimal(18,8)        not null,
   constraint PK_DRILLCOLLARCONFIG primary key nonclustered (ID)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '钻铤当前配置',
   'user', @CurrentUser, 'table', 'DrillCollarConfig'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ID',
   'user', @CurrentUser, 'table', 'DrillCollarConfig', 'column', 'ID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'WorkConfigID',
   'user', @CurrentUser, 'table', 'DrillCollarConfig', 'column', 'WorkConfigID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '钻铤名称',
   'user', @CurrentUser, 'table', 'DrillCollarConfig', 'column', 'DrillCollarName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '钻铤公称质量',
   'user', @CurrentUser, 'table', 'DrillCollarConfig', 'column', 'DrillCollarWeight'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '钻铤长度',
   'user', @CurrentUser, 'table', 'DrillCollarConfig', 'column', 'DrillCollarLength'
go

/*==============================================================*/
/* Index: Relationship_6_FK                                     */
/*==============================================================*/
create index Relationship_6_FK on DrillCollarConfig (
WorkConfigID ASC
)
go

/*==============================================================*/
/* Table: DrillPipeConfig                                       */
/*==============================================================*/
create table DrillPipeConfig (
   ID                   int                  identity,
   WorkConfigID         int                  not null,
   DrillPipeName        varchar(50)          not null,
   DrillPipeWeight      decimal(18,8)        not null,
   DrillPipeStandLength decimal(18,8)        not null,
   DrillPipeType        varchar(50)          not null,
   DrillPipeLength      decimal(18,8)        not null,
   constraint PK_DRILLPIPECONFIG primary key nonclustered (ID)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '钻杆当前配置',
   'user', @CurrentUser, 'table', 'DrillPipeConfig'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ID',
   'user', @CurrentUser, 'table', 'DrillPipeConfig', 'column', 'ID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'WorkConfigID',
   'user', @CurrentUser, 'table', 'DrillPipeConfig', 'column', 'WorkConfigID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '钻杆名称',
   'user', @CurrentUser, 'table', 'DrillPipeConfig', 'column', 'DrillPipeName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '钻杆公称质量',
   'user', @CurrentUser, 'table', 'DrillPipeConfig', 'column', 'DrillPipeWeight'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '钻杆立根长度',
   'user', @CurrentUser, 'table', 'DrillPipeConfig', 'column', 'DrillPipeStandLength'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '钻杆类型',
   'user', @CurrentUser, 'table', 'DrillPipeConfig', 'column', 'DrillPipeType'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '钻杆长度',
   'user', @CurrentUser, 'table', 'DrillPipeConfig', 'column', 'DrillPipeLength'
go

/*==============================================================*/
/* Index: Relationship_9_FK                                     */
/*==============================================================*/
create index Relationship_9_FK on DrillPipeConfig (
WorkConfigID ASC
)
go

/*==============================================================*/
/* Table: DrillingDifficulty                                    */
/*==============================================================*/
create table DrillingDifficulty (
   ID                   int                  identity,
   DictionaryID         int                  not null,
   DrillingDifficultyName varchar(50)          not null,
   DrillingDifficultyValue decimal(18,8)        not null,
   constraint PK_DRILLINGDIFFICULTY primary key nonclustered (ID)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '钻井难度',
   'user', @CurrentUser, 'table', 'DrillingDifficulty'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ID',
   'user', @CurrentUser, 'table', 'DrillingDifficulty', 'column', 'ID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'DictionaryID',
   'user', @CurrentUser, 'table', 'DrillingDifficulty', 'column', 'DictionaryID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '钻井难度名称',
   'user', @CurrentUser, 'table', 'DrillingDifficulty', 'column', 'DrillingDifficultyName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '钻井难度系数',
   'user', @CurrentUser, 'table', 'DrillingDifficulty', 'column', 'DrillingDifficultyValue'
go

/*==============================================================*/
/* Index: Relationship_5_FK                                     */
/*==============================================================*/
create index Relationship_5_FK on DrillingDifficulty (
DictionaryID ASC
)
go

/*==============================================================*/
/* Table: DrillingType                                          */
/*==============================================================*/
create table DrillingType (
   ID                   int                  identity,
   DictionaryID         int                  not null,
   DrillingTypeName     varchar(50)          not null,
   Coefficient          decimal(18,8)        not null,
   constraint PK_DRILLINGTYPE primary key nonclustered (ID)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '钻井类型',
   'user', @CurrentUser, 'table', 'DrillingType'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ID',
   'user', @CurrentUser, 'table', 'DrillingType', 'column', 'ID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'DictionaryID',
   'user', @CurrentUser, 'table', 'DrillingType', 'column', 'DictionaryID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '钻井类型名称',
   'user', @CurrentUser, 'table', 'DrillingType', 'column', 'DrillingTypeName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '钻井类型系数',
   'user', @CurrentUser, 'table', 'DrillingType', 'column', 'Coefficient'
go

/*==============================================================*/
/* Index: Relationship_4_FK                                     */
/*==============================================================*/
create index Relationship_4_FK on DrillingType (
DictionaryID ASC
)
go

/*==============================================================*/
/* Table: Machine                                               */
/*==============================================================*/
create table Machine (
   ID                   int                  identity,
   RegisterUserID       int                  not null,
   MachineCode          varchar(50)          not null,
   RegistTime           datetime             not null,
   constraint PK_MACHINE primary key nonclustered (ID)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '用户电脑',
   'user', @CurrentUser, 'table', 'Machine'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ID',
   'user', @CurrentUser, 'table', 'Machine', 'column', 'ID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'RegisterUserID',
   'user', @CurrentUser, 'table', 'Machine', 'column', 'RegisterUserID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '机器码',
   'user', @CurrentUser, 'table', 'Machine', 'column', 'MachineCode'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '注册时间',
   'user', @CurrentUser, 'table', 'Machine', 'column', 'RegistTime'
go

/*==============================================================*/
/* Index: Relationship_15_FK                                    */
/*==============================================================*/
create index Relationship_15_FK on Machine (
RegisterUserID ASC
)
go

/*==============================================================*/
/* Table: SecurityUser                                          */
/*==============================================================*/
create table SecurityUser (
   ID                   int                  identity,
   Account              varchar(50)          not null,
   Password             varchar(200)         not null,
   Name                 varchar(50)          not null,
   Checked              bit                  not null,
   Enabled              bit                  not null,
   RegistTime           datetime             not null,
   UpdateTime           datetime             not null,
   constraint PK_SECURITYUSER primary key nonclustered (ID)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '用户',
   'user', @CurrentUser, 'table', 'SecurityUser'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'SecurityUserID',
   'user', @CurrentUser, 'table', 'SecurityUser', 'column', 'ID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '账户名',
   'user', @CurrentUser, 'table', 'SecurityUser', 'column', 'Account'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '密码',
   'user', @CurrentUser, 'table', 'SecurityUser', 'column', 'Password'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户名',
   'user', @CurrentUser, 'table', 'SecurityUser', 'column', 'Name'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '校验标识',
   'user', @CurrentUser, 'table', 'SecurityUser', 'column', 'Checked'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '启用标识',
   'user', @CurrentUser, 'table', 'SecurityUser', 'column', 'Enabled'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '注册时间',
   'user', @CurrentUser, 'table', 'SecurityUser', 'column', 'RegistTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'SecurityUser', 'column', 'UpdateTime'
go

/*==============================================================*/
/* Table: WireropeCutRole                                       */
/*==============================================================*/
create table WireropeCutRole (
   ID                   int                  identity,
   DictionaryID         int                  not null,
   "Key"                varchar(50)          not null,
   MinDerrickHeight     decimal(18,8)        not null,
   MaxDerrickHeight     decimal(18,8)        not null,
   MinCutLength         decimal(18,8)        not null,
   MaxCutLength         decimal(18,8)        not null,
   constraint PK_WIREROPECUTROLE primary key nonclustered (ID)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '切绳长度规则',
   'user', @CurrentUser, 'table', 'WireropeCutRole'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ID',
   'user', @CurrentUser, 'table', 'WireropeCutRole', 'column', 'ID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'DictionaryID',
   'user', @CurrentUser, 'table', 'WireropeCutRole', 'column', 'DictionaryID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Key',
   'user', @CurrentUser, 'table', 'WireropeCutRole', 'column', 'Key'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '井架高度最小值',
   'user', @CurrentUser, 'table', 'WireropeCutRole', 'column', 'MinDerrickHeight'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '井架高度最大值',
   'user', @CurrentUser, 'table', 'WireropeCutRole', 'column', 'MaxDerrickHeight'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '切绳长度最小值',
   'user', @CurrentUser, 'table', 'WireropeCutRole', 'column', 'MinCutLength'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '切绳长度最大值',
   'user', @CurrentUser, 'table', 'WireropeCutRole', 'column', 'MaxCutLength'
go

/*==============================================================*/
/* Index: Relationship_2_FK                                     */
/*==============================================================*/
create index Relationship_2_FK on WireropeCutRole (
DictionaryID ASC
)
go

/*==============================================================*/
/* Table: WireropeEfficiency                                    */
/*==============================================================*/
create table WireropeEfficiency (
   ID                   int                  identity,
   DictionaryID         int                  not null,
   RopeCount            int                  not null,
   SlidingValue         decimal(18,8)        not null,
   RollingValue         decimal(18,8)        not null,
   constraint PK_WIREROPEEFFICIENCY primary key nonclustered (ID)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '缠绕效率',
   'user', @CurrentUser, 'table', 'WireropeEfficiency'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ID',
   'user', @CurrentUser, 'table', 'WireropeEfficiency', 'column', 'ID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'DictionaryID',
   'user', @CurrentUser, 'table', 'WireropeEfficiency', 'column', 'DictionaryID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '承载绳根数',
   'user', @CurrentUser, 'table', 'WireropeEfficiency', 'column', 'RopeCount'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '滑动轴承滑轮缠绕效率',
   'user', @CurrentUser, 'table', 'WireropeEfficiency', 'column', 'SlidingValue'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '滚动轴承滑轮缠绕效率',
   'user', @CurrentUser, 'table', 'WireropeEfficiency', 'column', 'RollingValue'
go

/*==============================================================*/
/* Index: Relationship_3_FK                                     */
/*==============================================================*/
create index Relationship_3_FK on WireropeEfficiency (
DictionaryID ASC
)
go

/*==============================================================*/
/* Table: WireropeWorkload                                      */
/*==============================================================*/
create table WireropeWorkload (
   ID                   int                  identity,
   DictionaryID         int                  not null,
   Diameter             decimal(18,8)        not null,
   Workload             decimal(18,8)        not null,
   constraint PK_WIREROPEWORKLOAD primary key nonclustered (ID)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '钢丝绳每米工作量',
   'user', @CurrentUser, 'table', 'WireropeWorkload'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ID',
   'user', @CurrentUser, 'table', 'WireropeWorkload', 'column', 'ID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'DictionaryID',
   'user', @CurrentUser, 'table', 'WireropeWorkload', 'column', 'DictionaryID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '钢丝绳直径',
   'user', @CurrentUser, 'table', 'WireropeWorkload', 'column', 'Diameter'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '钢丝绳每米工作量',
   'user', @CurrentUser, 'table', 'WireropeWorkload', 'column', 'Workload'
go

/*==============================================================*/
/* Index: Relationship_1_FK                                     */
/*==============================================================*/
create index Relationship_1_FK on WireropeWorkload (
DictionaryID ASC
)
go

/*==============================================================*/
/* Table: WorkConfig                                            */
/*==============================================================*/
create table WorkConfig (
   ID                   int                  identity,
   DictionaryID         int                  not null,
   ConfigUserID         int                  not null,
   FluidDensity         decimal(18,8)        not null,
   ElevatorWeight       decimal(18,8)        not null,
   DrillingShallowHeight decimal(18,8)        not null,
   DrillingDeepHeight   decimal(18,8)        not null,
   DrillingType         varchar(50)          not null,
   DrillingDifficulty   varchar(50)          not null,
   TripShallowHeight    decimal(18,8)        not null,
   TripDeepHeight       decimal(18,8)        not null,
   TripCount            decimal(18,8)        not null,
   BushingWeight        decimal(18,8)        not null,
   BushingLength        decimal(18,8)        not null,
   BushingHeight        decimal(18,8)        not null,
   CoringShallowHeight  decimal(18,8)        not null,
   CoringDeepHeight     decimal(18,8)        not null,
   ConfigTime           datetime             not null,
   WorkValue            decimal(18,8)        not null,
   constraint PK_WORKCONFIG primary key nonclustered (ID)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '累积当前配置',
   'user', @CurrentUser, 'table', 'WorkConfig'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ID',
   'user', @CurrentUser, 'table', 'WorkConfig', 'column', 'ID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'DictionaryID',
   'user', @CurrentUser, 'table', 'WorkConfig', 'column', 'DictionaryID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ConfigUserID',
   'user', @CurrentUser, 'table', 'WorkConfig', 'column', 'ConfigUserID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '钻井液密度',
   'user', @CurrentUser, 'table', 'WorkConfig', 'column', 'FluidDensity'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '油车-吊卡总质量',
   'user', @CurrentUser, 'table', 'WorkConfig', 'column', 'ElevatorWeight'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '钻井作业深度（浅）',
   'user', @CurrentUser, 'table', 'WorkConfig', 'column', 'DrillingShallowHeight'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '钻井作业深度（深）',
   'user', @CurrentUser, 'table', 'WorkConfig', 'column', 'DrillingDeepHeight'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '钻井类型',
   'user', @CurrentUser, 'table', 'WorkConfig', 'column', 'DrillingType'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '钻井难度',
   'user', @CurrentUser, 'table', 'WorkConfig', 'column', 'DrillingDifficulty'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '起下钻作业深度（浅）',
   'user', @CurrentUser, 'table', 'WorkConfig', 'column', 'TripShallowHeight'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '起下钻作业深度（深）',
   'user', @CurrentUser, 'table', 'WorkConfig', 'column', 'TripDeepHeight'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '起下钻作业次数',
   'user', @CurrentUser, 'table', 'WorkConfig', 'column', 'TripCount'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '套管公称质量',
   'user', @CurrentUser, 'table', 'WorkConfig', 'column', 'BushingWeight'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '套管单根长度',
   'user', @CurrentUser, 'table', 'WorkConfig', 'column', 'BushingLength'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '下套管深度',
   'user', @CurrentUser, 'table', 'WorkConfig', 'column', 'BushingHeight'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '取岩心作业深度（浅）',
   'user', @CurrentUser, 'table', 'WorkConfig', 'column', 'CoringShallowHeight'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '取岩心作业深度（深）',
   'user', @CurrentUser, 'table', 'WorkConfig', 'column', 'CoringDeepHeight'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '配置时间',
   'user', @CurrentUser, 'table', 'WorkConfig', 'column', 'ConfigTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '累积值',
   'user', @CurrentUser, 'table', 'WorkConfig', 'column', 'WorkValue'
go

/*==============================================================*/
/* Index: Relationship_8_FK                                     */
/*==============================================================*/
create index Relationship_8_FK on WorkConfig (
DictionaryID ASC
)
go

/*==============================================================*/
/* Index: Relationship_13_FK                                    */
/*==============================================================*/
create index Relationship_13_FK on WorkConfig (
ConfigUserID ASC
)
go

/*==============================================================*/
/* Table: WorkDictionary                                        */
/*==============================================================*/
create table WorkDictionary (
   ID                   int                  identity,
   ConfigUserID         int                  not null,
   ConfigTime           datetime             not null,
   constraint PK_WORKDICTIONARY primary key nonclustered (ID)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '累积配置字典',
   'user', @CurrentUser, 'table', 'WorkDictionary'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ID',
   'user', @CurrentUser, 'table', 'WorkDictionary', 'column', 'ID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ConfigUserID',
   'user', @CurrentUser, 'table', 'WorkDictionary', 'column', 'ConfigUserID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '配置时间',
   'user', @CurrentUser, 'table', 'WorkDictionary', 'column', 'ConfigTime'
go

/*==============================================================*/
/* Index: Relationship_12_FK                                    */
/*==============================================================*/
create index Relationship_12_FK on WorkDictionary (
ConfigUserID ASC
)
go

alter table CumulationReset
   add constraint FK_CUMULATI_RELATIONS_USER foreign key (UpdateUserID)
      references SecurityUser (ID)
go

alter table CuttingCriticalConfig
   add constraint FK_CUTCONF_RELATIONS_USER foreign key (ConfigUserID)
      references SecurityUser (ID)
go

alter table CuttingCriticalConfig
   add constraint FK_CUTCONF_RELATIONS_CUTDIC foreign key (DictionaryID)
      references CuttingCriticalDictionary (ID)
go

alter table CuttingCriticalDictionary
   add constraint FK_CUTDIC_RELATIONS_USER foreign key (ConfigUserID)
      references SecurityUser (ID)
go

alter table DrillCollarConfig
   add constraint FK_DRILLCOL_RELATIONS_WORKCONF foreign key (WorkConfigID)
      references WorkConfig (ID)
go

alter table DrillPipeConfig
   add constraint FK_DRILLPIP_RELATIONS_WORKCONF foreign key (WorkConfigID)
      references WorkConfig (ID)
go

alter table DrillingDifficulty
   add constraint FK_DRILDIFF_RELATIONS_WORKDIC foreign key (DictionaryID)
      references WorkDictionary (ID)
go

alter table DrillingType
   add constraint FK_DRILTYPE_RELATIONS_WORKDIC foreign key (DictionaryID)
      references WorkDictionary (ID)
go

alter table Machine
   add constraint FK_MACHINE_RELATIONS_USER foreign key (RegisterUserID)
      references SecurityUser (ID)
go

alter table WireropeCutRole
   add constraint FK_CUTROLE_RELATIONS_CUTDIC foreign key (DictionaryID)
      references CuttingCriticalDictionary (ID)
go

alter table WireropeEfficiency
   add constraint FK_EFFIC_RELATIONS_CUTDIC foreign key (DictionaryID)
      references CuttingCriticalDictionary (ID)
go

alter table WireropeWorkload
   add constraint FK_WORKLOAD_RELATIONS_CUTDIC foreign key (DictionaryID)
      references CuttingCriticalDictionary (ID)
go

alter table WorkConfig
   add constraint FK_WORKCONF_RELATIONS_USER foreign key (ConfigUserID)
      references SecurityUser (ID)
go

alter table WorkConfig
   add constraint FK_WORKCONF_RELATIONS_WORKDIC foreign key (DictionaryID)
      references WorkDictionary (ID)
go

alter table WorkDictionary
   add constraint FK_WORKDIC_RELATIONS_USER foreign key (ConfigUserID)
      references SecurityUser (ID)
go

