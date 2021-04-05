CREATE PROCEDURE [dbo].[spVendors_Update]
	@Id int,
	@FirstName nvarchar(100),
	@LastName nvarchar(150),
	@CommissionRate float,
	@PaymentDue money
AS
begin

	set nocount on;

	UPDATE Vendors
	SET FirstName = @FirstName, LastName = @LastName, CommissionRate = @CommissionRate, PaymentDue = @PaymentDue
	WHERE Id = @Id;

end