CREATE TABLE [dbo].[Profiles]
(
    [UserId] NVARCHAR(128) NOT NULL, 
    [FirstName] NVARCHAR(255) NOT NULL, 
    [LastName] NVARCHAR(255) NOT NULL, 
    [Address] NVARCHAR(255) NULL, 
    [DOB] DATETIME NULL, 
    [Telephone] NVARCHAR(50) NULL, 
    [GenderId] INT NOT NULL,
	CONSTRAINT [FK_Profiles_Users] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers]([Id]),
	CONSTRAINT [FK_Profiles_Gender] FOREIGN KEY ([GenderId]) REFERENCES [Genders]([Id]), 
    PRIMARY KEY ([UserId])
)
