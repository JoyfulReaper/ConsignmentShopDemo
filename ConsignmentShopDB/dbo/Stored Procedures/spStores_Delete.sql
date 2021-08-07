CREATE PROCEDURE [dbo].[spStores_Delete]
	@Id int
AS
begin

	set nocount on;

	delete from Stores where Id = @Id;

end
