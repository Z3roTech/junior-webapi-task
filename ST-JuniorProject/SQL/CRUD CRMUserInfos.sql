SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE ClientInformation
GO

IF OBJECT_ID('spCreateCRMUserInfo') IS NOT NULL
BEGIN 
DROP PROC spCreateCRMUserInfo
END
GO

IF OBJECT_ID('spReadCRMUserInfoById') IS NOT NULL
BEGIN 
DROP PROC spReadCRMUserInfoById
END
GO

IF OBJECT_ID('spReadCRMUserInfoByCuratorId') IS NOT NULL
BEGIN 
DROP PROC spReadCRMUserInfoByCuratorId
END
GO

IF OBJECT_ID('spUpdateCRMUserInfo') IS NOT NULL
BEGIN 
DROP PROC spUpdateCRMUserInfo
END
GO

IF OBJECT_ID('spDeleteCRMUserInfo') IS NOT NULL
BEGIN 
DROP PROC spDeleteCRMUserInfo
END
GO

CREATE PROCEDURE spCreateCRMUserInfo
	@CuratorId INT,
    @Login INT,
    @LineNumber INT
AS
BEGIN
	INSERT INTO CRMUserInfos(CuratorId, Login, LineNumber)
	VALUES(@CuratorId, @Login, @LineNumber);
END
GO

CREATE PROCEDURE spReadCRMUserInfoById
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM CRMUserInfos Where (Id = @Id);
END
GO

CREATE PROCEDURE spReadCRMUserInfoByCuratorId
	@CuratorId INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM CRMUserInfos Where (CuratorId = @CuratorId);
END
GO

CREATE PROCEDURE spUpdateCRMUserInfo
	@Id INT,
	@CuratorId INT,
    @Login INT,
    @LineNumber INT
AS
BEGIN
	UPDATE CRMUserInfos
	SET CuratorId = @CuratorId, Login = @Login, LineNumber = @LineNumber
	WHERE Id = @Id;
END
GO

CREATE PROCEDURE spDeleteCRMUserInfo
	@Id INT
AS
BEGIN
	DELETE FROM CRMUserInfos WHERE Id = @Id;
END
GO