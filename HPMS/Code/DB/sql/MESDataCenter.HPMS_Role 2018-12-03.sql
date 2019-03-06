-- Server: 172.20.23.107,1433
-- Table:  HPMS_Role
-- Date:   2018/12/3 11:31

INSERT INTO HPMS_Role (Name, Description, RightsID, Status, CreateID, CreateDate, isSuper) VALUES ('admin', '超级管理员', '1,2,3', 1, 1, '2018-10-31 17:07:10.37', 1)
GO
INSERT INTO HPMS_Role (Name, Description, RightsID, Status, CreateID, CreateDate, isSuper) VALUES ('一般', '测试', '1,2,3', -1, 1, '2018-10-31 17:08:30', NULL)
GO
INSERT INTO HPMS_Role (Name, Description, RightsID, Status, CreateID, CreateDate, isSuper) VALUES ('一般2', '测试用', '2,3', 1, 1, '2018-10-31 19:53:21', NULL)
GO
INSERT INTO HPMS_Role (Name, Description, RightsID, Status, CreateID, CreateDate, isSuper) VALUES ('测试用', 'ssss', '1,2,3', 1, 1, '2018-11-01 14:28:30', NULL)
GO
