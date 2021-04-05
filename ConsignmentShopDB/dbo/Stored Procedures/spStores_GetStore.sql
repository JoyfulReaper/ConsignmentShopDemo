CREATE PROCEDURE [dbo].[spStores_Get]
	@Name nvarchar(100)
AS
begin

	set nocount on;

	select [Id], [Name], [StoreBank], [StoreProfit]
	from Stores
	where [Name] = @Name;

end