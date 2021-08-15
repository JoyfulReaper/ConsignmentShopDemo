CREATE PROCEDURE [dbo].[spVendors_Get]
	@Id int
AS
begin

	set nocount on;

	select [Id], [FirstName], [LastName], [CommissionRate], [PaymentDue], [StoreId]
	from dbo.Vendors
	where Id = @Id;

end