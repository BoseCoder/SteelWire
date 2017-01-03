/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     2017/1/3 15:46:16                            */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CriticalConfig') and o.name = 'FK_CRITICALCONF_R_SECURITYUSER')
alter table CriticalConfig
   drop constraint FK_CRITICALCONF_R_SECURITYUSER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CriticalConfig') and o.name = 'FK_CRITICALCONF_R_CRITICALDICT')
alter table CriticalConfig
   drop constraint FK_CRITICALCONF_R_CRITICALDICT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CriticalDictionary') and o.name = 'FK_CRITICALDICT_R_SECURITYUSER')
alter table CriticalDictionary
   drop constraint FK_CRITICALDICT_R_SECURITYUSER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CriticalRecord') and o.name = 'FK_CRITICALRECO_R_SECURITYUSER')
alter table CriticalRecord
   drop constraint FK_CRITICALRECO_R_SECURITYUSER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CriticalRecord') and o.name = 'FK_CRITICALRECO_R_CRITICALCONF')
alter table CriticalRecord
   drop constraint FK_CRITICALRECO_R_CRITICALCONF
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CriticalRecord') and o.name = 'FK_CRITICALRECO_R_WIRELINEINFO')
alter table CriticalRecord
   drop constraint FK_CRITICALRECO_R_WIRELINEINFO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CumulationConfig') and o.name = 'FK_CUMULATIONCO_R_CUMULATIONDI')
alter table CumulationConfig
   drop constraint FK_CUMULATIONCO_R_CUMULATIONDI
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CumulationConfig') and o.name = 'FK_CUMULATIONCO_R_SECURITYUSER')
alter table CumulationConfig
   drop constraint FK_CUMULATIONCO_R_SECURITYUSER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CumulationDictionary') and o.name = 'FK_CUMULATIONDI_R_SECURITYUSER')
alter table CumulationDictionary
   drop constraint FK_CUMULATIONDI_R_SECURITYUSER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CumulationRecord') and o.name = 'FK_CUMULATIONRE_R_CUMULATIONCO')
alter table CumulationRecord
   drop constraint FK_CUMULATIONRE_R_CUMULATIONCO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CumulationRecord') and o.name = 'FK_CUMULATIONRE_R_SECURITYUSER')
alter table CumulationRecord
   drop constraint FK_CUMULATIONRE_R_SECURITYUSER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CumulationRecord') and o.name = 'FK_CUMULATIONRE_R_CUTRECORD')
alter table CumulationRecord
   drop constraint FK_CUMULATIONRE_R_CUTRECORD
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CumulationRecord') and o.name = 'FK_CUMULATIONRE_R_CRITICALCONF')
alter table CumulationRecord
   drop constraint FK_CUMULATIONRE_R_CRITICALCONF
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CutRecord') and o.name = 'FK_CUTRECORD_R_SECURITYUSER')
alter table CutRecord
   drop constraint FK_CUTRECORD_R_SECURITYUSER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CutRecord') and o.name = 'FK_CUTRECORD_R_WIRELINEINFO')
alter table CutRecord
   drop constraint FK_CUTRECORD_R_WIRELINEINFO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('DrillDeviceConfig') and o.name = 'FK_DRILLDEVICEC_R_CUMULATIONCO')
alter table DrillDeviceConfig
   drop constraint FK_DRILLDEVICEC_R_CUMULATIONCO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('DrillingType') and o.name = 'FK_DRILLINGTYPE_R_CUMULATIONDI')
alter table DrillingType
   drop constraint FK_DRILLINGTYPE_R_CUMULATIONDI
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Machine') and o.name = 'FK_MACHINE_R_SECURITYUSER')
alter table Machine
   drop constraint FK_MACHINE_R_SECURITYUSER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('WirelineInfo') and o.name = 'FK_WIRELINEINFO_R_SECURITYUSER')
alter table WirelineInfo
   drop constraint FK_WIRELINEINFO_R_SECURITYUSER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('WireropeCutRole') and o.name = 'FK_WIREROPECUTR_R_CRITICALDICT')
alter table WireropeCutRole
   drop constraint FK_WIREROPECUTR_R_CRITICALDICT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('WireropeEfficiency') and o.name = 'FK_WIREROPEEFFI_R_CRITICALDICT')
alter table WireropeEfficiency
   drop constraint FK_WIREROPEEFFI_R_CRITICALDICT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('WireropeWorkload') and o.name = 'FK_WIREROPEWORK_R_CRITICALDICT')
alter table WireropeWorkload
   drop constraint FK_WIREROPEWORK_R_CRITICALDICT
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CriticalConfig')
            and   name  = 'Relationship_16_FK'
            and   indid > 0
            and   indid < 255)
   drop index CriticalConfig.Relationship_16_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CriticalConfig')
            and   name  = 'Relationship_4_FK'
            and   indid > 0
            and   indid < 255)
   drop index CriticalConfig.Relationship_4_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CriticalConfig')
            and   type = 'U')
   drop table CriticalConfig
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CriticalDictionary')
            and   name  = 'Relationship_14_FK'
            and   indid > 0
            and   indid < 255)
   drop index CriticalDictionary.Relationship_14_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CriticalDictionary')
            and   type = 'U')
   drop table CriticalDictionary
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CriticalRecord')
            and   name  = 'Relationship_19_FK'
            and   indid > 0
            and   indid < 255)
   drop index CriticalRecord.Relationship_19_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CriticalRecord')
            and   name  = 'Relationship_6_FK'
            and   indid > 0
            and   indid < 255)
   drop index CriticalRecord.Relationship_6_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CriticalRecord')
            and   name  = 'Relationship_5_FK'
            and   indid > 0
            and   indid < 255)
   drop index CriticalRecord.Relationship_5_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CriticalRecord')
            and   type = 'U')
   drop table CriticalRecord
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CumulationConfig')
            and   name  = 'Relationship_17_FK'
            and   indid > 0
            and   indid < 255)
   drop index CumulationConfig.Relationship_17_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CumulationConfig')
            and   name  = 'Relationship_11_FK'
            and   indid > 0
            and   indid < 255)
   drop index CumulationConfig.Relationship_11_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CumulationConfig')
            and   type = 'U')
   drop table CumulationConfig
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CumulationDictionary')
            and   name  = 'Relationship_15_FK'
            and   indid > 0
            and   indid < 255)
   drop index CumulationDictionary.Relationship_15_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CumulationDictionary')
            and   type = 'U')
   drop table CumulationDictionary
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CumulationRecord')
            and   name  = 'Relationship_21_FK'
            and   indid > 0
            and   indid < 255)
   drop index CumulationRecord.Relationship_21_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CumulationRecord')
            and   name  = 'Relationship_10_FK'
            and   indid > 0
            and   indid < 255)
   drop index CumulationRecord.Relationship_10_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CumulationRecord')
            and   name  = 'Relationship_9_FK'
            and   indid > 0
            and   indid < 255)
   drop index CumulationRecord.Relationship_9_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CumulationRecord')
            and   name  = 'Relationship_8_FK'
            and   indid > 0
            and   indid < 255)
   drop index CumulationRecord.Relationship_8_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CumulationRecord')
            and   type = 'U')
   drop table CumulationRecord
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CutRecord')
            and   name  = 'Relationship_20_FK'
            and   indid > 0
            and   indid < 255)
   drop index CutRecord.Relationship_20_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CutRecord')
            and   name  = 'Relationship_7_FK'
            and   indid > 0
            and   indid < 255)
   drop index CutRecord.Relationship_7_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CutRecord')
            and   type = 'U')
   drop table CutRecord
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('DrillDeviceConfig')
            and   name  = 'Relationship_22_FK'
            and   indid > 0
            and   indid < 255)
   drop index DrillDeviceConfig.Relationship_22_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DrillDeviceConfig')
            and   type = 'U')
   drop table DrillDeviceConfig
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('DrillingType')
            and   name  = 'Relationship_12_FK'
            and   indid > 0
            and   indid < 255)
   drop index DrillingType.Relationship_12_FK
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
            and   name  = 'Relationship_13_FK'
            and   indid > 0
            and   indid < 255)
   drop index Machine.Relationship_13_FK
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
           where  id    = object_id('WirelineInfo')
            and   name  = 'Relationship_18_FK'
            and   indid > 0
            and   indid < 255)
   drop index WirelineInfo.Relationship_18_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WirelineInfo')
            and   type = 'U')
   drop table WirelineInfo
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

/*==============================================================*/
/* Table: CriticalConfig                                        */
/*==============================================================*/
create table CriticalConfig (
   ID                   bigint               identity,
   DictionaryID         bigint               not null,
   ConfigUserID         bigint               not null,
   DerrickHeight        decimal(18,8)        not null,
   WirelineMaxPower     decimal(18,8)        not null,
   RotaryHookWorkload   decimal(18,8)        not null,
   RollerDiameter       decimal(18,8)        not null,
   RopeCount            bigint               not null,
   ConfigTime           datetime             not null,
   ConfigTimeStamp      bigint               not null,
   constraint PK_CRITICALCONFIG primary key (ID)
)
go

/*==============================================================*/
/* Index: Relationship_4_FK                                     */
/*==============================================================*/
create index Relationship_4_FK on CriticalConfig (
DictionaryID ASC
)
go

/*==============================================================*/
/* Index: Relationship_16_FK                                    */
/*==============================================================*/
create index Relationship_16_FK on CriticalConfig (
ConfigUserID ASC
)
go

/*==============================================================*/
/* Table: CriticalDictionary                                    */
/*==============================================================*/
create table CriticalDictionary (
   ID                   bigint               identity,
   ConfigUserID         bigint               not null,
   ConfigTime           datetime             not null,
   ConfigTimeStamp      bigint               not null,
   constraint PK_CRITICALDICTIONARY primary key (ID)
)
go

/*==============================================================*/
/* Index: Relationship_14_FK                                    */
/*==============================================================*/
create index Relationship_14_FK on CriticalDictionary (
ConfigUserID ASC
)
go

/*==============================================================*/
/* Table: CriticalRecord                                        */
/*==============================================================*/
create table CriticalRecord (
   ID                   bigint               identity,
   CriticalConfigID     bigint               not null,
   WirelineID           bigint               not null,
   CalculateUserID      bigint               not null,
   WirelineDiameter     varchar(100)         not null,
   CriticalValue        decimal(18,8)        not null,
   CalculateTime        datetime             not null,
   constraint PK_CRITICALRECORD primary key (ID)
)
go

/*==============================================================*/
/* Index: Relationship_5_FK                                     */
/*==============================================================*/
create index Relationship_5_FK on CriticalRecord (
CriticalConfigID ASC
)
go

/*==============================================================*/
/* Index: Relationship_6_FK                                     */
/*==============================================================*/
create index Relationship_6_FK on CriticalRecord (
WirelineID ASC
)
go

/*==============================================================*/
/* Index: Relationship_19_FK                                    */
/*==============================================================*/
create index Relationship_19_FK on CriticalRecord (
CalculateUserID ASC
)
go

/*==============================================================*/
/* Table: CumulationConfig                                      */
/*==============================================================*/
create table CumulationConfig (
   ID                   bigint               identity,
   DictionaryID         bigint               not null,
   ConfigUserID         bigint               not null,
   RealRotaryHookWorkload decimal(18,8)        not null,
   FluidDensity         decimal(18,8)        not null,
   ElevatorWeight       decimal(18,8)        not null,
   DrillingShallowHeight decimal(18,8)        not null,
   DrillingDeepHeight   decimal(18,8)        not null,
   DrillingType         varchar(100)         not null,
   RedressingCount      bigint               not null,
   TripShallowHeight    decimal(18,8)        not null,
   TripDeepHeight       decimal(18,8)        not null,
   TripCount            bigint               not null,
   BushingHeight        decimal(18,8)        not null,
   CoringShallowHeight  decimal(18,8)        not null,
   CoringDeepHeight     decimal(18,8)        not null,
   ConfigTime           datetime             not null,
   ConfigTimeStamp      bigint               not null,
   constraint PK_CUMULATIONCONFIG primary key (ID)
)
go

/*==============================================================*/
/* Index: Relationship_11_FK                                    */
/*==============================================================*/
create index Relationship_11_FK on CumulationConfig (
DictionaryID ASC
)
go

/*==============================================================*/
/* Index: Relationship_17_FK                                    */
/*==============================================================*/
create index Relationship_17_FK on CumulationConfig (
ConfigUserID ASC
)
go

/*==============================================================*/
/* Table: CumulationDictionary                                  */
/*==============================================================*/
create table CumulationDictionary (
   ID                   bigint               identity,
   ConfigUserID         bigint               not null,
   ConfigTime           datetime             not null,
   ConfigTimeStamp      bigint               not null,
   constraint PK_CUMULATIONDICTIONARY primary key (ID)
)
go

/*==============================================================*/
/* Index: Relationship_15_FK                                    */
/*==============================================================*/
create index Relationship_15_FK on CumulationDictionary (
ConfigUserID ASC
)
go

/*==============================================================*/
/* Table: CumulationRecord                                      */
/*==============================================================*/
create table CumulationRecord (
   ID                   bigint               identity,
   CutRecordID          bigint               not null,
   CriticalConfigID     bigint               not null,
   CumulationConfigID   bigint               not null,
   CalculateUserID      bigint               not null,
   CumulationValue      decimal(18,8)        not null,
   CalculateTime        datetime             not null,
   constraint PK_CUMULATIONRECORD primary key (ID)
)
go

/*==============================================================*/
/* Index: Relationship_8_FK                                     */
/*==============================================================*/
create index Relationship_8_FK on CumulationRecord (
CutRecordID ASC
)
go

/*==============================================================*/
/* Index: Relationship_9_FK                                     */
/*==============================================================*/
create index Relationship_9_FK on CumulationRecord (
CriticalConfigID ASC
)
go

/*==============================================================*/
/* Index: Relationship_10_FK                                    */
/*==============================================================*/
create index Relationship_10_FK on CumulationRecord (
CumulationConfigID ASC
)
go

/*==============================================================*/
/* Index: Relationship_21_FK                                    */
/*==============================================================*/
create index Relationship_21_FK on CumulationRecord (
CalculateUserID ASC
)
go

/*==============================================================*/
/* Table: CutRecord                                             */
/*==============================================================*/
create table CutRecord (
   ID                   bigint               identity,
   WirelineID           bigint               not null,
   UpdateUserID         bigint               not null,
   CumulationValue      decimal(18,8)        not null,
   CutValue             decimal(18,8)        not null,
   RemainValue          decimal(18,8)        not null,
   CutLength            decimal(18,8)        not null,
   UpdateTime           datetime             not null,
   constraint PK_CUTRECORD primary key (ID)
)
go

/*==============================================================*/
/* Index: Relationship_7_FK                                     */
/*==============================================================*/
create index Relationship_7_FK on CutRecord (
WirelineID ASC
)
go

/*==============================================================*/
/* Index: Relationship_20_FK                                    */
/*==============================================================*/
create index Relationship_20_FK on CutRecord (
UpdateUserID ASC
)
go

/*==============================================================*/
/* Table: DrillDeviceConfig                                     */
/*==============================================================*/
create table DrillDeviceConfig (
   ID                   bigint               identity,
   CumulationConfigID   bigint               not null,
   Name                 varchar(100)         not null,
   Type                 varchar(100)         not null,
   Weight               decimal(18,8)        not null,
   Length               decimal(18,8)        not null,
   constraint PK_DRILLDEVICECONFIG primary key (ID)
)
go

/*==============================================================*/
/* Index: Relationship_22_FK                                    */
/*==============================================================*/
create index Relationship_22_FK on DrillDeviceConfig (
CumulationConfigID ASC
)
go

/*==============================================================*/
/* Table: DrillingType                                          */
/*==============================================================*/
create table DrillingType (
   ID                   bigint               identity,
   DictionaryID         bigint               not null,
   Name                 varchar(100)         not null,
   Coefficient          decimal(18,8)        not null,
   constraint PK_DRILLINGTYPE primary key (ID)
)
go

/*==============================================================*/
/* Index: Relationship_12_FK                                    */
/*==============================================================*/
create index Relationship_12_FK on DrillingType (
DictionaryID ASC
)
go

/*==============================================================*/
/* Table: Machine                                               */
/*==============================================================*/
create table Machine (
   ID                   bigint               identity,
   RegistrationUserID   bigint               not null,
   MachineCode          varchar(100)         not null,
   RegistrationTime     datetime             not null,
   constraint PK_MACHINE primary key (ID)
)
go

/*==============================================================*/
/* Index: Relationship_13_FK                                    */
/*==============================================================*/
create index Relationship_13_FK on Machine (
RegistrationUserID ASC
)
go

/*==============================================================*/
/* Table: SecurityUser                                          */
/*==============================================================*/
create table SecurityUser (
   ID                   bigint               identity,
   Account              varchar(100)         not null,
   Password             varchar(200)         not null,
   Name                 varchar(100)         not null,
   Checked              bit                  not null,
   Enabled              bit                  not null,
   RegistrationTime     datetime             not null,
   UpdateTime           datetime             not null,
   constraint PK_SECURITYUSER primary key (ID)
)
go

/*==============================================================*/
/* Table: WirelineInfo                                          */
/*==============================================================*/
create table WirelineInfo (
   ID                   bigint               identity,
   UpdateUserID         bigint               not null,
   Number               varchar(100)         not null,
   Diameter             varchar(100)         not null,
   Struct               varchar(100)         not null,
   StrongLevel          varchar(100)         not null,
   TwistDirection       varchar(100)         not null,
   OrderLength          decimal(18,8)        not null,
   UnitSystem           varchar(100)         not null,
   UpdateTime           datetime             not null,
   constraint PK_WIRELINEINFO primary key (ID)
)
go

/*==============================================================*/
/* Index: Relationship_18_FK                                    */
/*==============================================================*/
create index Relationship_18_FK on WirelineInfo (
UpdateUserID ASC
)
go

/*==============================================================*/
/* Table: WireropeCutRole                                       */
/*==============================================================*/
create table WireropeCutRole (
   ID                   bigint               identity,
   DictionaryID         bigint               not null,
   "Key"                varchar(100)         not null,
   MinDerrickHeight     decimal(18,8)        not null,
   MaxDerrickHeight     decimal(18,8)        not null,
   MinCutLength         decimal(18,8)        not null,
   MaxCutLength         decimal(18,8)        not null,
   constraint PK_WIREROPECUTROLE primary key (ID)
)
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
   ID                   bigint               identity,
   DictionaryID         bigint               not null,
   RopeCount            bigint               not null,
   SlidingValue         decimal(18,8)        not null,
   RollingValue         decimal(18,8)        not null,
   constraint PK_WIREROPEEFFICIENCY primary key (ID)
)
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
   ID                   bigint               identity,
   DictionaryID         bigint               not null,
   "Key"                varchar(100)         not null,
   Name                 varchar(100)         not null,
   UnitSystem           varchar(100)         not null,
   Diameter             decimal(18,8)        not null,
   Workload             decimal(18,8)        not null,
   constraint PK_WIREROPEWORKLOAD primary key (ID)
)
go

/*==============================================================*/
/* Index: Relationship_1_FK                                     */
/*==============================================================*/
create index Relationship_1_FK on WireropeWorkload (
DictionaryID ASC
)
go

alter table CriticalConfig
   add constraint FK_CRITICALCONF_R_SECURITYUSER foreign key (ConfigUserID)
      references SecurityUser (ID)
go

alter table CriticalConfig
   add constraint FK_CRITICALCONF_R_CRITICALDICT foreign key (DictionaryID)
      references CriticalDictionary (ID)
go

alter table CriticalDictionary
   add constraint FK_CRITICALDICT_R_SECURITYUSER foreign key (ConfigUserID)
      references SecurityUser (ID)
go

alter table CriticalRecord
   add constraint FK_CRITICALRECO_R_SECURITYUSER foreign key (CalculateUserID)
      references SecurityUser (ID)
go

alter table CriticalRecord
   add constraint FK_CRITICALRECO_R_CRITICALCONF foreign key (CriticalConfigID)
      references CriticalConfig (ID)
go

alter table CriticalRecord
   add constraint FK_CRITICALRECO_R_WIRELINEINFO foreign key (WirelineID)
      references WirelineInfo (ID)
go

alter table CumulationConfig
   add constraint FK_CUMULATIONCO_R_CUMULATIONDI foreign key (DictionaryID)
      references CumulationDictionary (ID)
go

alter table CumulationConfig
   add constraint FK_CUMULATIONCO_R_SECURITYUSER foreign key (ConfigUserID)
      references SecurityUser (ID)
go

alter table CumulationDictionary
   add constraint FK_CUMULATIONDI_R_SECURITYUSER foreign key (ConfigUserID)
      references SecurityUser (ID)
go

alter table CumulationRecord
   add constraint FK_CUMULATIONRE_R_CUMULATIONCO foreign key (CumulationConfigID)
      references CumulationConfig (ID)
go

alter table CumulationRecord
   add constraint FK_CUMULATIONRE_R_SECURITYUSER foreign key (CalculateUserID)
      references SecurityUser (ID)
go

alter table CumulationRecord
   add constraint FK_CUMULATIONRE_R_CUTRECORD foreign key (CutRecordID)
      references CutRecord (ID)
go

alter table CumulationRecord
   add constraint FK_CUMULATIONRE_R_CRITICALCONF foreign key (CriticalConfigID)
      references CriticalConfig (ID)
go

alter table CutRecord
   add constraint FK_CUTRECORD_R_SECURITYUSER foreign key (UpdateUserID)
      references SecurityUser (ID)
go

alter table CutRecord
   add constraint FK_CUTRECORD_R_WIRELINEINFO foreign key (WirelineID)
      references WirelineInfo (ID)
go

alter table DrillDeviceConfig
   add constraint FK_DRILLDEVICEC_R_CUMULATIONCO foreign key (CumulationConfigID)
      references CumulationConfig (ID)
go

alter table DrillingType
   add constraint FK_DRILLINGTYPE_R_CUMULATIONDI foreign key (DictionaryID)
      references CumulationDictionary (ID)
go

alter table Machine
   add constraint FK_MACHINE_R_SECURITYUSER foreign key (RegistrationUserID)
      references SecurityUser (ID)
go

alter table WirelineInfo
   add constraint FK_WIRELINEINFO_R_SECURITYUSER foreign key (UpdateUserID)
      references SecurityUser (ID)
go

alter table WireropeCutRole
   add constraint FK_WIREROPECUTR_R_CRITICALDICT foreign key (DictionaryID)
      references CriticalDictionary (ID)
go

alter table WireropeEfficiency
   add constraint FK_WIREROPEEFFI_R_CRITICALDICT foreign key (DictionaryID)
      references CriticalDictionary (ID)
go

alter table WireropeWorkload
   add constraint FK_WIREROPEWORK_R_CRITICALDICT foreign key (DictionaryID)
      references CriticalDictionary (ID)
go

