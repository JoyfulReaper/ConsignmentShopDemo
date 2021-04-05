CREATE PROCEDURE [dbo].[spVendors_Insert]
	@FirstName varchar(100),
	@LastName varchar(150),
	@CommissionRate float,
	@PaymentDue money,
	@Id int = 0 output
AS
begin

	set nocount on;

	insert into Vendors (FirstName, LastName, CommissionRate, PaymentDue)
	values (@FirstName, @LastName, @CommissionRate, @PaymentDue);

	select @Id = SCOPE_IDENTITY();

end