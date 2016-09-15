CREATE PROCEDURE [dbo].[CreationSellTransaction]
	@transactionId int = 0,
	@userId nvarchar(155)
AS


CREATE TABLE #TemporalTransaction 
(
	[ImageId] int Not null,
	[Amount] money not null	
)

Insert Into #TemporalTransaction
Select ti.ImageId, ti.Value from transactions t Inner join transactionItems ti on ti.transactionId = t.Id where t.Id = @transactionId

DECLARE @LastTransactionId int = 0, @Amount Money, @ImageId int, @sellUserId nvarchar(155)

WHILE (Select COUNT(ImageID) from #TemporalTransaction) > 0
BEGIN
	Select @ImageId= ImageId, @Amount = Amount from #TemporalTransaction

	set @sellUserId = (select UserId from PicPopImages where Id = @ImageId)

	IF @LastTransactionId = 0 OR @sellUserId != (Select userId from Transactions where id = @LastTransactionId)
	BEGIN		
		Insert into Transactions (UserId,TypeId,Amount,DtAdded, CreatedBy)
		Values (@sellUserId, 0, @Amount, getdate(), @userId)

		set @LastTransactionID = SCOPE_IDENTITY()
	END

	INSERT INTO TransactionItems (ImageId,TransactionId,Value)
	VALUES (@ImageId, @LastTransactionId, @Amount)


	delete from #TemporalTransaction where ImageId= @ImageId and Amount = @Amount
END

DROP TABLE #TemporalTransaction
	
RETURN 0
