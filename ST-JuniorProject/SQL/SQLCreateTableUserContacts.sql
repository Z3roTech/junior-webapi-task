USE [ClientInformation]
GO

/****** Object:  Table [dbo].[UserContacts]    Script Date: 19.05.2021 14:50:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserContacts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClientId] [int] NOT NULL,
	[PhoneNumber] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserContacts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[UserContacts]  WITH CHECK ADD  CONSTRAINT [FK_UserContacts_Users_ClientId] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[UserContacts] CHECK CONSTRAINT [FK_UserContacts_Users_ClientId]
GO


