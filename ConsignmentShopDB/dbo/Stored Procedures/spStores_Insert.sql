CREATE PROCEDURE [dbo].[spStores_Insert]
	@Id int = 0 output,
	@Name nvarchar(100),
	@StoreBank money,
	@StoreProfit money
AS
begin

	set nocount on;

	insert into Stores ([Name], StoreBank, StoreProfit)
	values (@Name, @StoreBank, @StoreProfit);

	select @Id = SCOPE_IDENTITY();

end
