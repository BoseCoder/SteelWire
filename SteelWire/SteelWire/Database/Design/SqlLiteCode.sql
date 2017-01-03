create table SecurityUser (
ID                   INTEGER PRIMARY KEY ASC AUTOINCREMENT NOT NULL,
Account              TEXT(100)                   not null,
Password             TEXT(200)                   not null,
Name                 TEXT(100)                   not null,
Checked              BOOLEAN                       not null,
Enabled              BOOLEAN                       not null,
RegistrationTime     DATETIME                           not null,
UpdateTime           DATETIME                           not null
);

create table CriticalDictionary (
ID                   INTEGER PRIMARY KEY ASC AUTOINCREMENT NOT NULL,
ConfigUserID         INTEGER                         not null,
ConfigTime           DATETIME                           not null,
ConfigTimeStamp      INTEGER                         not null,
foreign key (ConfigUserID)
      references SecurityUser (ID)
);

create table CriticalConfig (
ID                   INTEGER PRIMARY KEY ASC AUTOINCREMENT NOT NULL,
DictionaryID         INTEGER                         not null,
ConfigUserID         INTEGER                         not null,
DerrickHeight        DECIMAL(18,8)                  not null,
WirelineMaxPower     DECIMAL(18,8)                  not null,
RotaryHookWorkload   DECIMAL(18,8)                  not null,
RollerDiameter       DECIMAL(18,8)                  not null,
RopeCount            INTEGER                         not null,
ConfigTime           DATETIME                           not null,
ConfigTimeStamp      INTEGER                         not null,
foreign key (DictionaryID)
      references CriticalDictionary (ID),
foreign key (ConfigUserID)
      references SecurityUser (ID)
);

create unique index CriticalConfig_PK on CriticalConfig (
ID ASC
);

create  index Relationship_4_FK on CriticalConfig (
DictionaryID ASC
);

create  index Relationship_16_FK on CriticalConfig (
ConfigUserID ASC
);

create unique index CriticalDictionary_PK on CriticalDictionary (
ID ASC
);

create  index Relationship_14_FK on CriticalDictionary (
ConfigUserID ASC
);

create table WirelineInfo (
ID                   INTEGER PRIMARY KEY ASC AUTOINCREMENT NOT NULL,
UpdateUserID         INTEGER                         not null,
Number               TEXT(100)                   not null,
Diameter             TEXT(100)                   not null,
Struct               TEXT(100)                   not null,
StrongLevel          TEXT(100)                   not null,
TwistDirection       TEXT(100)                   not null,
OrderLength          DECIMAL(18,8)                  not null,
UnitSystem           TEXT(100)                   not null,
UpdateTime           DATETIME                           not null,
foreign key (UpdateUserID)
      references SecurityUser (ID)
);

create table CriticalRecord (
ID                   INTEGER PRIMARY KEY ASC AUTOINCREMENT NOT NULL,
CriticalConfigID     INTEGER                         not null,
WirelineID           INTEGER                         not null,
CalculateUserID      INTEGER                         not null,
WirelineDiameter     TEXT(100)                       not null,
CriticalValue        DECIMAL(18,8)                  not null,
CalculateTime        DATETIME                           not null,
foreign key (CriticalConfigID)
      references CriticalConfig (ID),
foreign key (WirelineID)
      references WirelineInfo (ID),
foreign key (CalculateUserID)
      references SecurityUser (ID)
);

create unique index CriticalRecord_PK on CriticalRecord (
ID ASC
);

create  index Relationship_5_FK on CriticalRecord (
CriticalConfigID ASC
);

create  index Relationship_6_FK on CriticalRecord (
WirelineID ASC
);

create  index Relationship_19_FK on CriticalRecord (
CalculateUserID ASC
);

create table CumulationDictionary (
ID                   INTEGER PRIMARY KEY ASC AUTOINCREMENT NOT NULL,
ConfigUserID         INTEGER                         not null,
ConfigTime           DATETIME                           not null,
ConfigTimeStamp      INTEGER                         not null,
foreign key (ConfigUserID)
      references SecurityUser (ID)
);

create table CumulationConfig (
ID                   INTEGER PRIMARY KEY ASC AUTOINCREMENT NOT NULL,
DictionaryID         INTEGER                         not null,
ConfigUserID         INTEGER                         not null,
RealRotaryHookWorkload DECIMAL(18,8)                  not null,
FluidDensity         DECIMAL(18,8)                  not null,
ElevatorWeight       DECIMAL(18,8)                  not null,
DrillingShallowHeight DECIMAL(18,8)                  not null,
DrillingDeepHeight   DECIMAL(18,8)                  not null,
DrillingType         TEXT(100)                   not null,
RedressingCount      INTEGER                         not null,
TripShallowHeight    DECIMAL(18,8)                  not null,
TripDeepHeight       DECIMAL(18,8)                  not null,
TripCount            INTEGER                         not null,
BushingHeight        DECIMAL(18,8)                  not null,
CoringShallowHeight  DECIMAL(18,8)                  not null,
CoringDeepHeight     DECIMAL(18,8)                  not null,
ConfigTime           DATETIME                           not null,
ConfigTimeStamp      INTEGER                         not null,
foreign key (DictionaryID)
      references CumulationDictionary (ID),
foreign key (ConfigUserID)
      references SecurityUser (ID)
);

create unique index CumulationConfig_PK on CumulationConfig (
ID ASC
);

create  index Relationship_11_FK on CumulationConfig (
DictionaryID ASC
);

create  index Relationship_17_FK on CumulationConfig (
ConfigUserID ASC
);

create unique index CumulationDictionary_PK on CumulationDictionary (
ID ASC
);

create  index Relationship_15_FK on CumulationDictionary (
ConfigUserID ASC
);

create table CutRecord (
ID                   INTEGER PRIMARY KEY ASC AUTOINCREMENT NOT NULL,
WirelineID           INTEGER                         not null,
UpdateUserID         INTEGER                         not null,
CumulationValue      DECIMAL(18,8)                  not null,
CutValue             DECIMAL(18,8)                  not null,
RemainValue          DECIMAL(18,8)                  not null,
CutLength            DECIMAL(18,8)                  not null,
UpdateTime           DATETIME                           not null,
foreign key (WirelineID)
      references WirelineInfo (ID),
foreign key (UpdateUserID)
      references SecurityUser (ID)
);

create table CumulationRecord (
ID                   INTEGER PRIMARY KEY ASC AUTOINCREMENT NOT NULL,
CutRecordID          INTEGER                         not null,
CriticalConfigID     INTEGER                         not null,
CumulationConfigID   INTEGER                         not null,
CalculateUserID      INTEGER                         not null,
CumulationValue      DECIMAL(18,8)                  not null,
CalculateTime        DATETIME                           not null,
foreign key (CutRecordID)
      references CutRecord (ID),
foreign key (CriticalConfigID)
      references CriticalConfig (ID),
foreign key (CumulationConfigID)
      references CumulationConfig (ID),
foreign key (CalculateUserID)
      references SecurityUser (ID)
);

create unique index CumulationRecord_PK on CumulationRecord (
ID ASC
);

create  index Relationship_8_FK on CumulationRecord (
CutRecordID ASC
);

create  index Relationship_9_FK on CumulationRecord (
CriticalConfigID ASC
);

create  index Relationship_10_FK on CumulationRecord (
CumulationConfigID ASC
);

create  index Relationship_21_FK on CumulationRecord (
CalculateUserID ASC
);

create unique index CutRecord_PK on CutRecord (
ID ASC
);

create  index Relationship_7_FK on CutRecord (
WirelineID ASC
);

create  index Relationship_20_FK on CutRecord (
UpdateUserID ASC
);

create table DrillDeviceConfig (
ID                   INTEGER PRIMARY KEY ASC AUTOINCREMENT NOT NULL,
CumulationConfigID   INTEGER                         not null,
Name                 TEXT(100)                   not null,
Type                 TEXT(100)                   not null,
Weight               DECIMAL(18,8)                  not null,
Length               DECIMAL(18,8)                  not null,
foreign key (CumulationConfigID)
      references CumulationConfig (ID)
);

create unique index DrillDeviceConfig_PK on DrillDeviceConfig (
ID ASC
);

create  index Relationship_22_FK on DrillDeviceConfig (
CumulationConfigID ASC
);

create table DrillingType (
ID                   INTEGER PRIMARY KEY ASC AUTOINCREMENT NOT NULL,
DictionaryID         INTEGER                         not null,
Name                 TEXT(100)                   not null,
Coefficient          DECIMAL(18,8)                  not null,
foreign key (DictionaryID)
      references CumulationDictionary (ID)
);

create unique index DrillingType_PK on DrillingType (
ID ASC
);

create  index Relationship_12_FK on DrillingType (
DictionaryID ASC
);

create table Machine (
ID                   INTEGER PRIMARY KEY ASC AUTOINCREMENT NOT NULL,
RegistrationUserID   INTEGER                         not null,
MachineCode          TEXT(100)                   not null,
RegistrationTime     DATETIME                           not null,
foreign key (RegistrationUserID)
      references SecurityUser (ID)
);

create unique index Machine_PK on Machine (
ID ASC
);

create  index Relationship_13_FK on Machine (
RegistrationUserID ASC
);

create unique index SecurityUser_PK on SecurityUser (
ID ASC
);

create unique index WirelineInfo_PK on WirelineInfo (
ID ASC
);

create  index Relationship_18_FK on WirelineInfo (
UpdateUserID ASC
);

create table WireropeCutRole (
ID                   INTEGER PRIMARY KEY ASC AUTOINCREMENT NOT NULL,
DictionaryID         INTEGER                         not null,
Key                  TEXT(100)                   not null,
MinDerrickHeight     DECIMAL(18,8)                  not null,
MaxDerrickHeight     DECIMAL(18,8)                  not null,
MinCutLength         DECIMAL(18,8)                  not null,
MaxCutLength         DECIMAL(18,8)                  not null,
foreign key (DictionaryID)
      references CriticalDictionary (ID)
);

create unique index WireropeCutRole_PK on WireropeCutRole (
ID ASC
);

create  index Relationship_2_FK on WireropeCutRole (
DictionaryID ASC
);

create table WireropeEfficiency (
ID                   INTEGER PRIMARY KEY ASC AUTOINCREMENT NOT NULL,
DictionaryID         INTEGER                         not null,
RopeCount            INTEGER                         not null,
SlidingValue         DECIMAL(18,8)                  not null,
RollingValue         DECIMAL(18,8)                  not null,
foreign key (DictionaryID)
      references CriticalDictionary (ID)
);

create unique index WireropeEfficiency_PK on WireropeEfficiency (
ID ASC
);

create  index Relationship_3_FK on WireropeEfficiency (
DictionaryID ASC
);

create table WireropeWorkload (
ID                   INTEGER PRIMARY KEY ASC AUTOINCREMENT NOT NULL,
DictionaryID         INTEGER                         not null,
Key                  TEXT(100)                   not null,
Name                 TEXT(100)                   not null,
UnitSystem           TEXT(100)                   not null,
Diameter             DECIMAL(18,8)                  not null,
Workload             DECIMAL(18,8)                  not null,
foreign key (DictionaryID)
      references CriticalDictionary (ID)
);

create unique index WireropeWorkload_PK on WireropeWorkload (
ID ASC
);

create  index Relationship_1_FK on WireropeWorkload (
DictionaryID ASC
);

