-- Server: 172.20.23.107,1433
-- Table:  HPMS_User
-- Date:   2018/12/3 11:31

INSERT INTO HPMS_User (UserName, Password, Salt, RoleID, isSuper, CreateID, CreateDate, Status) VALUES ('admin', '123456', 'figoba', 1, 1, 0, '2018-10-24 10:56:50', 1)
GO
INSERT INTO HPMS_User (UserName, Password, Salt, RoleID, isSuper, CreateID, CreateDate, Status) VALUES ('test', '123456', 'figoba', 3, NULL, 1, '2018-10-31 19:24:17', -1)
GO
INSERT INTO HPMS_User (UserName, Password, Salt, RoleID, isSuper, CreateID, CreateDate, Status) VALUES ('test1', '123456', 'figoba', 2, NULL, 1, '2018-10-31 19:35:14', -1)
GO
INSERT INTO HPMS_User (UserName, Password, Salt, RoleID, isSuper, CreateID, CreateDate, Status) VALUES ('test2', '123456', 'figoba', 2, NULL, 1, '2018-10-31 19:35:21', -1)
GO
INSERT INTO HPMS_User (UserName, Password, Salt, RoleID, isSuper, CreateID, CreateDate, Status) VALUES ('testA', '123456', 'figoba', 2, NULL, 1, '2018-10-31 19:40:38', 0)
GO
INSERT INTO HPMS_User (UserName, Password, Salt, RoleID, isSuper, CreateID, CreateDate, Status) VALUES ('testB', '123456', 'figoba', 2, NULL, 1, '2018-10-31 19:52:08', -1)
GO
