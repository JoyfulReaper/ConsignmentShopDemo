CREATE PROCEDURE [dbo].[spItems_Delete]
	@Id int
AS
begin

set nocount on;

delete from Items where Id = @Id;

end
