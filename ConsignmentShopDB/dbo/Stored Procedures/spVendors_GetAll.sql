CREATE PROCEDURE [dbo].[spVendors_GetAll]
AS
begin

	set nocount on;

	select [Id], [FirstName], [LastName], [CommissionRate], [PaymentDue]
	from Vendors;

end
