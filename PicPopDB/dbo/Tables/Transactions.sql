CREATE TABLE [dbo].[Transactions]
(
    [Id] INT NOT NULL IDENTITY, 
	[UserId] NVARCHAR(128) NOT NULL, 
    [TypeId] BIT NOT NULL, 
    [Amount] MONEY NOT NULL DEFAULT 0.00, 
	[DtAdded] DATETIME NOT NULL, 
    [CreatedBy] NVARCHAR(128) NOT NULL,     
    CONSTRAINT [PK_Transactions] PRIMARY KEY ([Id]),     
    CONSTRAINT [FK_Transactions_Users] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers]([Id])
)
