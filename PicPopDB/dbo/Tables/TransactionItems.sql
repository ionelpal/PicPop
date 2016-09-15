CREATE TABLE [dbo].[TransactionItems]
(
	[ImageId] INT NOT NULL , 
    [TransactionId] INT NOT NULL, 
    [Value] MONEY NOT NULL, 
	CONSTRAINT [FK_TransactionItems_Transaction] FOREIGN KEY ([TransactionId]) REFERENCES [Transactions]([Id]),
	CONSTRAINT [FK_TransactionItems_Images] FOREIGN KEY ([ImageId]) REFERENCES [PicPopImages]([Id]), 
    PRIMARY KEY ([ImageId], [TransactionId])
)

GO

CREATE TRIGGER [dbo].[Trigger_TransactionItems]
    ON [dbo].[TransactionItems]
    FOR INSERT
    AS
    BEGIN
		Declare @userId nvarchar(155), @tid int
		set @tid = (select transactionId from inserted)
		set @userId = (select userId from Transactions where id = @tid)

		Execute CreationSellTransaction @tid, @userId
    END