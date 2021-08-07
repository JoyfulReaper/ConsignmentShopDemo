CREATE PROCEDURE [dbo].[spItems_GetUnsold]
	@StoreId int
AS
begin
	
	set nocount on;

	select [Id], [Name], [Description], [Price], [Sold], [OwnerId], [PaymentDistributed] 
	from Items
	where sold = 0 and StoreId = @StoreId;

end