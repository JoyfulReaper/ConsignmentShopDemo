﻿CREATE PROCEDURE [dbo].[spItems_Insert]
	@Name nvarchar(200),
	@Description nvarchar(2000),
	@Price money,
	@Sold bit,
	@OwnerId int,
	@PaymentDistributed bit,
	@Id int = 0 output,
	@StoreId int
AS
begin
	
	set nocount on;

    insert into dbo.Items (Name, Description, Price, Sold, OwnerId, PaymentDistributed, StoreId)
	values (@Name, @Description, @Price, @Sold, @OwnerId, @PaymentDistributed, @StoreId);

	select @Id = SCOPE_IDENTITY();

end