CREATE PROCEDURE [dbo].[spItems_GetAll]
	@StoreId int
AS
begin
	
	set nocount on;

	select [Id], [Name], [Description], [Price], [Sold], [OwnerId], [PaymentDistributed] 
	from dbo.Items
	where StoreId = @StoreId;

end