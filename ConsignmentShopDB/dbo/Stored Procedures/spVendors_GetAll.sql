﻿CREATE PROCEDURE [dbo].[spVendors_GetAll]
	@StoreId int
AS
begin

	set nocount on;

	select [Id], [FirstName], [LastName], [CommissionRate], [PaymentDue]
	from Vendors
	where StoreId = @StoreId;

end
