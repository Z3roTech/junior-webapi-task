SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE ClientInformation
GO

IF OBJECT_ID('spCreateUser') IS NOT NULL
BEGIN 
DROP PROC spCreateUser
END
GO

IF OBJECT_ID('spReadUserByLogin') IS NOT NULL
BEGIN 
DROP PROC spReadUserByLogin
END
GO

IF OBJECT_ID('spReadUserById') IS NOT NULL
BEGIN 
DROP PROC spReadUserById
END
GO

IF OBJECT_ID('spUpdateUser') IS NOT NULL
BEGIN 
DROP PROC spUpdateUser
END
GO

IF OBJECT_ID('spDeleteUser') IS NOT NULL
BEGIN 
DROP PROC spDeleteUser
END
GO

CREATE PROCEDURE spCreateUser
	@Name NVARCHAR(MAX),
	@Login INT,
	@LineNumber INT
AS
BEGIN
	INSERT INTO Users(Name, Login, LineNumber)
	VALUES(@Name, @Login, @LineNumber);
END
GO

CREATE PROCEDURE spReadUserByLogin
	@Login INT,
	@LineNumber INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM Users Where (Login = @Login AND LineNumber = @LineNumber);
END
GO

CREATE PROCEDURE spReadUserById
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM Users Where (Id = @Id);
END
GO

CREATE PROCEDURE spUpdateUser
	@Id INT,
	@Name NVARCHAR(MAX),
	@Login INT,
	@LineNumber INT
AS
BEGIN
	UPDATE Users
	SET Name = @Name, Login = @Login, LineNumber = @LineNumber
	WHERE Id = @Id;
END
GO

CREATE PROCEDURE spDeleteUser
	@Id INT
AS
BEGIN
	DELETE FROM Users WHERE Id = @Id;
END
GO