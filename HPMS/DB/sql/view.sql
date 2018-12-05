IF OBJECT_ID ('dbo.v_HPMS_rights') IS NOT NULL
	DROP VIEW dbo.v_HPMS_rights
GO

CREATE VIEW dbo.v_HPMS_rights
AS
SELECT HPMS_User.ID, UserName, Password, Salt, RoleID, HPMS_User.isSuper, HPMS_User.CreateID, HPMS_User.CreateDate, HPMS_User.Status AS UserStatus,
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
SELECT HPMS_User.ID, UserName, Password, Salt, RoleID, HPMS_User.isSuper, HPMS_User.CreateID, HPMS_User.CreateDate, HPMS_User.Status AS UserStatus,
HPMS_Role.Name AS RoleName,
HPMS_Role.Description AS RoleDescription,
HPMS_Role.RightsID AS RoleRights,
HPMS_Role.Status AS RoleStatus
FROM dbo.HPMS_User 
LEFT JOIN HPMS_Role ON HPMS_User.RoleID=HPMS_Role.ID
GO





