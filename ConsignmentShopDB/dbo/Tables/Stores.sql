CREATE TABLE [dbo].[Stores]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(100) NOT NULL, 
    [StoreBank] MONEY NOT NULL, 
    [StoreProfit] MONEY NOT NULL
)
