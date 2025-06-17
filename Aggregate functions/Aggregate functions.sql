SELECT TOP 10
    [Production].[Product].[Name],
    SUM([Sales].[SalesOrderDetail].[OrderQty]) AS TotalQuantitySold,
    SUM([Sales].[SalesOrderDetail].[LineTotal]) AS TotalRevenue
FROM [Sales].[SalesOrderDetail]
JOIN [Production].[Product]
    ON [Sales].[SalesOrderDetail].[ProductID] = [Production].[Product].[ProductID]
GROUP BY [Production].[Product].[Name]
ORDER BY TotalQuantitySold DESC, TotalRevenue DESC;


SELECT 
    [Sales].[SalesTerritory].[TerritoryID],
    [Sales].[SalesTerritory].[Name],
    SUM([Sales].[SalesOrderHeader].[TotalDue]) AS TotalRevenue
FROM [Sales].[SalesOrderHeader]
JOIN [Sales].[SalesTerritory]
    ON [Sales].[SalesOrderHeader].[TerritoryID] = [Sales].[SalesTerritory].[TerritoryID]
GROUP BY [Sales].[SalesTerritory].[TerritoryID], [Sales].[SalesTerritory].[Name]
ORDER BY TotalRevenue DESC;


SELECT 
    [Sales].[Customer].[CustomerID],
    SUM([Sales].[SalesOrderHeader].[TotalDue]) AS TotalSpending
FROM [Sales].[Customer]
JOIN [Sales].[SalesOrderHeader]
    ON [Sales].[Customer].[CustomerID] = [Sales].[SalesOrderHeader].[CustomerID]
GROUP BY [Sales].[Customer].[CustomerID]
ORDER BY TotalSpending DESC;


SELECT 
    YEAR([Sales].[SalesOrderHeader].[OrderDate]) AS OrderYear,
    MONTH([Sales].[SalesOrderHeader].[OrderDate]) AS OrderMonth,
    SUM([Sales].[SalesOrderHeader].[TotalDue]) AS MonthlyRevenue
FROM [Sales].[SalesOrderHeader]
GROUP BY 
    YEAR([Sales].[SalesOrderHeader].[OrderDate]),
    MONTH([Sales].[SalesOrderHeader].[OrderDate])
ORDER BY OrderYear, OrderMonth;


SELECT 
    [HumanResources].[Employee].[BusinessEntityID],
    [Person].[Person].[FirstName],
    [Person].[Person].[LastName],
    SUM([Sales].[SalesOrderHeader].[TotalDue]) AS TotalSales
FROM [Sales].[SalesOrderHeader]
JOIN [HumanResources].[Employee]
    ON [Sales].[SalesOrderHeader].[SalesPersonID] = [HumanResources].[Employee].[BusinessEntityID]
JOIN [Person].[Person]
    ON [HumanResources].[Employee].[BusinessEntityID] = [Person].[Person].[BusinessEntityID]
GROUP BY 
    [HumanResources].[Employee].[BusinessEntityID],
    [Person].[Person].[FirstName],
    [Person].[Person].[LastName]
ORDER BY TotalSales DESC;


SELECT 
    [Production].[ProductCategory].[Name] AS CategoryName,
    SUM([Sales].[SalesOrderDetail].[LineTotal]) AS TotalRevenue
FROM [Sales].[SalesOrderDetail]
JOIN [Production].[Product]
    ON [Sales].[SalesOrderDetail].[ProductID] = [Production].[Product].[ProductID]
JOIN [Production].[ProductSubcategory]
    ON [Production].[Product].[ProductSubcategoryID] = [Production].[ProductSubcategory].[ProductSubcategoryID]
JOIN [Production].[ProductCategory]
    ON [Production].[ProductSubcategory].[ProductCategoryID] = [Production].[ProductCategory].[ProductCategoryID]
GROUP BY [Production].[ProductCategory].[Name]
ORDER BY TotalRevenue DESC;


SELECT 
    [Sales].[SalesTerritory].[Name] AS TerritoryName,
    AVG([Sales].[SalesOrderHeader].[TotalDue]) AS AverageOrderValue
FROM [Sales].[SalesOrderHeader]
JOIN [Sales].[SalesTerritory]
    ON [Sales].[SalesOrderHeader].[TerritoryID] = [Sales].[SalesTerritory].[TerritoryID]
GROUP BY [Sales].[SalesTerritory].[Name]
ORDER BY AverageOrderValue DESC;


SELECT 
    [Sales].[SalesOrderHeader].[SalesOrderID],
    DATEDIFF(DAY, [Sales].[SalesOrderHeader].[OrderDate], [Sales].[SalesOrderHeader].[ShipDate]) AS DaysToShip,
    [Sales].[SalesOrderHeader].[TotalDue]
FROM [Sales].[SalesOrderHeader]
WHERE DATEDIFF(DAY, [Sales].[SalesOrderHeader].[OrderDate], [Sales].[SalesOrderHeader].[ShipDate]) > 5
ORDER BY DaysToShip DESC;
