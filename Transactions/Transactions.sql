BEGIN TRANSACTION;

INSERT INTO [Sales].[Customer] ([PersonID], [StoreID], [TerritoryID])
VALUES (1, NULL, 1);

DECLARE @NewCustomerID INT = SCOPE_IDENTITY();

INSERT INTO [Sales].[SalesOrderHeader] ([CustomerID], [OrderDate], [DueDate], [ShipDate], [Status], [OnlineOrderFlag], [TotalDue], [TerritoryID])
VALUES (@NewCustomerID, GETDATE(), DATEADD(DAY, 5, GETDATE()), NULL, 1, 1, 0.00, 1);

IF @@ERROR <> 0 ROLLBACK TRANSACTION;
ELSE COMMIT TRANSACTION;


BEGIN TRANSACTION;

UPDATE [Production].[Product]
SET [ListPrice] = [ListPrice] + 50
WHERE [ProductID] = 707;

INSERT INTO [Production].[ProductCostHistory] ([ProductID], [StartDate], [StandardCost])
VALUES (707, GETDATE(), (SELECT [StandardCost] FROM [Production].[Product] WHERE [ProductID] = 707));

IF @@ERROR <> 0 ROLLBACK TRANSACTION;
ELSE COMMIT TRANSACTION;


BEGIN TRANSACTION;

UPDATE [Sales].[SalesOrderHeader]
SET [Status] = 6
WHERE [SalesOrderID] = 43659;

UPDATE [Production].[ProductInventory]
SET [Quantity] = [Quantity] + sd.[OrderQty]
FROM [Sales].[SalesOrderDetail] sd
WHERE sd.[SalesOrderID] = 43659 AND sd.[ProductID] = [Production].[ProductInventory].[ProductID];

IF @@ERROR <> 0 ROLLBACK TRANSACTION;
ELSE COMMIT TRANSACTION;


BEGIN TRANSACTION;

DELETE FROM [Sales].[SalesOrderDetail]
WHERE [ProductID] = 922;

DELETE FROM [Production].[Product]
WHERE [ProductID] = 922;

IF @@ERROR <> 0 ROLLBACK TRANSACTION;
ELSE COMMIT TRANSACTION;


BEGIN TRANSACTION;

UPDATE [HumanResources].[Employee]
SET [JobTitle] = 'Senior Sales Representative'
WHERE [BusinessEntityID] = 275;

INSERT INTO [HumanResources].[EmployeeDepartmentHistory] ([BusinessEntityID], [DepartmentID], [ShiftID], [StartDate])
VALUES (275, 3, 2, GETDATE());

IF @@ERROR <> 0 ROLLBACK TRANSACTION;
ELSE COMMIT TRANSACTION;


BEGIN TRANSACTION;

INSERT INTO [Sales].[SalesTerritory] ([Name], [CountryRegionCode], [Group])
VALUES ('Eastern Balkans', 'BG', 'Europe');

DECLARE @NewTerritoryID INT = SCOPE_IDENTITY();

UPDATE [Sales].[Customer]
SET [TerritoryID] = @NewTerritoryID
WHERE [TerritoryID] IS NULL;

IF @@ERROR <> 0 ROLLBACK TRANSACTION;
ELSE COMMIT TRANSACTION;


BEGIN TRANSACTION;

UPDATE [Production].[ProductInventory]
SET [Quantity] = [Quantity] - 100
WHERE [ProductID] = 776 AND [LocationID] = 1;

UPDATE [Production].[ProductInventory]
SET [Quantity] = [Quantity] + 100
WHERE [ProductID] = 776 AND [LocationID] = 6;

IF @@ERROR <> 0 ROLLBACK TRANSACTION;
ELSE COMMIT TRANSACTION;


BEGIN TRANSACTION;

UPDATE [Sales].[Customer]
SET [ModifiedDate] = GETDATE()
WHERE [CustomerID] = 30122;

UPDATE [Sales].[SalesOrderHeader]
SET [Status] = 6
WHERE [CustomerID] = 30122 AND [Status] IN (1, 2);

IF @@ERROR <> 0 ROLLBACK TRANSACTION;
ELSE COMMIT TRANSACTION;
