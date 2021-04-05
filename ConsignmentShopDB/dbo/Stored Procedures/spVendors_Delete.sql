CREATE PROCEDURE [dbo].[spVendors_Delete]
	@Id int
AS
begin

	set nocount on;

	delete from Vendors where Id = @Id;

end
