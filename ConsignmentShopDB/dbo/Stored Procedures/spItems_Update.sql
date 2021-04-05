CREATE PROCEDURE [dbo].[spItems_Update]
	@Id int,
	@Name nvarchar(200),
	@Description nvarchar(2000),
	@Price money,
	@Sold bit,
	@OwnerId int,
	@PaymentDistributed bit
AS
begin

	set nocount on;

	UPDATE Items
	SET Name = @Name, Description = @Description, Price = @Price, Sold = @Sold, OwnerId = @OwnerId, PaymentDistributed = @PaymentDistributed
	WHERE Id = @Id;

end