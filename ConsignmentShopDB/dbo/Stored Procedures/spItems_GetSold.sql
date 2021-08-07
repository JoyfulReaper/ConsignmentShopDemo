CREATE PROCEDURE [dbo].[spItems_GetSold]
	@StoreId int
AS
begin

	set nocount on;

	select [Id], [Name], [Description], [Price], [Sold], [OwnerId], [PaymentDistributed]
	from Items
	where Sold = 1 and StoreId = @StoreId;

end