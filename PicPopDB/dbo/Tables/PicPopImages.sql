CREATE TABLE [dbo].[PicPopImages]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] NVARCHAR(128) NOT NULL, 
	[Name] NVARCHAR(255) NOT NULL, 
    [Amount] MONEY NOT NULL DEFAULT 0.00, 
	[CategoryId] INT NULL, 
    [DtAdded] DATETIME NOT NULL, 
    [CreatedBy] NVARCHAR(128) NOT NULL, 
    [DtModified] DATETIME NULL, 
    [ModifiedBy] NVARCHAR(128) NULL, 
    [Active] BIT NOT NULL DEFAULT 1, 
    
    CONSTRAINT [FK_Images_Users] FOREIGN KEY ([UserID]) REFERENCES [AspNetUsers]([Id]),
    CONSTRAINT [FK_Categories_PicPopImages] FOREIGN KEY ([CategoryId]) REFERENCES [Categories]([Id])
)
