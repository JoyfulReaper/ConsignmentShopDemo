CREATE TABLE [dbo].[Items]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(200) NOT NULL, 
    [Description] NVARCHAR(2000) NULL, 
    [Price] MONEY NOT NULL, 
    [Sold] BIT NOT NULL,
    [OwnerId] INT NOT NULL, 
    [PaymentDistributed] BIT NOT NULL, 
    CONSTRAINT [FK_Items_Vendors] FOREIGN KEY ([OwnerId]) REFERENCES [Vendors]([Id]), 
)
