CREATE PROCEDURE [dbo].[spItems_GetSoldByVendorId]
	@OwnerId int
AS
begin

	set nocount on;

	select [Id], [Name], [Description], [Price], [Sold], [OwnerId], [PaymentDistributed] 
	from dbo.Items
	where OwnerId = @OwnerId and Sold = 1;

end
