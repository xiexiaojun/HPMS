IF OBJECT_ID ('dbo.HPMS_Rights') IS NOT NULL
	DROP TABLE dbo.HPMS_Rights
GO

CREATE TABLE dbo.HPMS_Rights
	(
	ID          INT IDENTITY NOT NULL,
	Name        VARCHAR (30) NOT NULL,
	Description VARCHAR (100) NOT NULL,
	Version     VARCHAR (150) NOT NULL,
	Status      INT NOT NULL
	)
GO

IF OBJECT_ID ('dbo.HPMS_Role') IS NOT NULL
	DROP TABLE dbo.HPMS_Role
GO

CREATE TABLE dbo.HPMS_Role
	(
	ID          INT IDENTITY NOT NULL,
	Name        VARCHAR (30) NOT NULL,
	Description VARCHAR (100) NOT NULL,
	RightsID    VARCHAR (200) NOT NULL,
	Status      INT NOT NULL,
	CreateID    INT,
	CreateDate  DATETIME
	)
GO

IF OBJECT_ID ('dbo.HPMS_User') IS NOT NULL
	DROP TABLE dbo.HPMS_User
GO

CREATE TABLE dbo.HPMS_User
	(
	ID         INT IDENTITY NOT NULL,
	UserName   VARCHAR (30) NOT NULL,
	Password   VARCHAR (18) NOT NULL,
	Salt       VARCHAR (18) NOT NULL,
	RoleID     INT,
	isSuper    BIT,
	CreateID   INT,
	CreateDate DATETIME NOT NULL,
	Status     INT NOT NULL
	)
GO

IF OBJECT_ID ('dbo.v_HPMS_rights') IS NOT NULL
	DROP VIEW dbo.v_HPMS_rights
GO

CREATE VIEW dbo.v_HPMS_rights
AS
SELECT HPMS_User.ID, UserName, Password, Salt, RoleID, isSuper, HPMS_User.CreateID, HPMS_User.CreateDate, HPMS_User.Status AS UserStatus,
HPMS_Role.Name AS RoleName,HPMS_Rights.Name AS RightsName,
HPMS_Role.Description AS RoleDescription,HPMS_Rights.ID AS RightsID,
HPMS_Role.Status AS RoleStatus,HPMS_Rights.Status AS RightsStatus
FROM dbo.HPMS_User 
LEFT JOIN HPMS_Role ON HPMS_User.RoleID=HPMS_Role.ID
LEFT JOIN HPMS_Rights on CHARINDEX (convert(varchar,HPMS_Rights.ID),HPMS_Role.RightsID)>0
GO

IF OBJECT_ID ('dbo.v_HPMS_user') IS NOT NULL
	DROP VIEW dbo.v_HPMS_user
GO

CREATE VIEW dbo.v_HPMS_user
AS
SELECT HPMS_User.ID, UserName, Password, Salt, RoleID, isSuper, HPMS_User.CreateID, HPMS_User.CreateDate, HPMS_User.Status AS UserStatus,
HPMS_Role.Name AS RoleName,
HPMS_Role.Description AS RoleDescription,
HPMS_Role.RightsID AS RoleRights,
HPMS_Role.Status AS RoleStatus
FROM dbo.HPMS_User 
LEFT JOIN HPMS_Role ON HPMS_User.RoleID=HPMS_Role.ID
GO










