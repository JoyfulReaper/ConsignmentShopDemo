CREATE PROCEDURE [dbo].[spStores_GetAll]

AS
begin

	set nocount on;

	select [Id], [Name], [StoreBank], [StoreProfit]
	from Stores;

end
