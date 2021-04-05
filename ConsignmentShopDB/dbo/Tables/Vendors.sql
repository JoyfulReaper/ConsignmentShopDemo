CREATE TABLE [dbo].[Vendors]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FirstName] NVARCHAR(100) NOT NULL, 
    [LastName] NVARCHAR(150) NOT NULL, 
    [CommissionRate] FLOAT NOT NULL, 
    [PaymentDue] MONEY NOT NULL
)
