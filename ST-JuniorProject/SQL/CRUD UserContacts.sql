SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE ClientInformation
GO

IF OBJECT_ID('spCreateUserContact') IS NOT NULL
BEGIN 
DROP PROC spCreateUserContact
END
GO

IF OBJECT_ID('spReadUserContactById') IS NOT NULL
BEGIN 
DROP PROC spReadUserContactById
END
GO

IF OBJECT_ID('spReadUserContactByClientId') IS NOT NULL
BEGIN 
DROP PROC spReadUserContactByClientId
END
GO

IF OBJECT_ID('spUpdateUserContact') IS NOT NULL
BEGIN 
DROP PROC spUpdateUserContact
END
GO

IF OBJECT_ID('spDeleteUserContact') IS NOT NULL
BEGIN 
DROP PROC spDeleteUserContact
END
GO

CREATE PROCEDURE spCreateUserContact
	@ClientId INT,
	@PhoneNumber NVARCHAR(MAX)
AS
BEGIN
	INSERT INTO UserContacts(ClientId, PhoneNumber)
	VALUES(@ClientId, @PhoneNumber);
END
GO

CREATE PROCEDURE spReadUserContactByClientId
	@ClientId INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM UserContacts Where (ClientId = @ClientId);
END
GO

CREATE PROCEDURE spReadUserContactById
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM UserContacts Where (Id = @Id);
END
GO

CREATE PROCEDURE spUpdateUserContact
	@Id INT,
	@ClientId INT,
	@PhoneNumber NVARCHAR(MAX)
AS
BEGIN
	UPDATE UserContacts
	SET ClientId = @ClientId, PhoneNumber = @PhoneNumber
	WHERE Id = @Id;
END
GO

CREATE PROCEDURE spDeleteUserContact
	@Id INT
AS
BEGIN
	DELETE FROM UserContacts WHERE Id = @Id;
END
GO