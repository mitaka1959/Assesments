SELECT 
    [ProductID], 
    [OrderQty], 
    [SalesOrderID]
FROM [Sales].[SalesOrderDetail]
WHERE [OrderQty] > (
    SELECT AVG([OrderQty]) 
    FROM [Sales].[SalesOrderDetail]
);


SELECT [CustomerID], [SalesOrderID], [OrderDate]
FROM [Sales].[SalesOrderHeader]
WHERE [CustomerID] IN (
    SELECT DISTINCT [CustomerID]
    FROM [Sales].[SalesOrderHeader]
    WHERE [OrderDate] >= DATEADD(DAY, -30, GETDATE())
);


SELECT [ProductID], [Name]
FROM [Production].[Product]
WHERE [ProductID] NOT IN (
    SELECT DISTINCT [ProductID]
    FROM [Sales].[SalesOrderDetail]
);


SELECT [CustomerID], [TotalSpent]
FROM (
    SELECT 
        [CustomerID],
        SUM([TotalDue]) AS TotalSpent
    FROM [Sales].[SalesOrderHeader]
    GROUP BY [CustomerID]
) AS CustomerTotals
ORDER BY TotalSpent DESC
OFFSET 0 ROWS FETCH NEXT 5 ROWS ONLY;


SELECT [BusinessEntityID], [JobTitle]
FROM [HumanResources].[Employee]
WHERE [BusinessEntityID] IN (
    SELECT DISTINCT [SalesPersonID]
    FROM [Sales].[SalesOrderHeader]
    WHERE [SalesPersonID] IS NOT NULL
);


SELECT [Product].[ProductID], [Product].[Name]
FROM [Production].[Product]
WHERE [ProductSubcategoryID] IN (
    SELECT [ProductSubcategoryID]
    FROM [Production].[ProductSubcategory]
    WHERE [ProductCategoryID] = (
        SELECT TOP 1 [ProductCategoryID]
        FROM (
            SELECT 
                [ProductCategoryID],
                SUM([Sales].[SalesOrderDetail].[LineTotal]) AS Revenue
            FROM [Sales].[SalesOrderDetail]
            JOIN [Production].[Product]
                ON [Sales].[SalesOrderDetail].[ProductID] = [Production].[Product].[ProductID]
            JOIN [Production].[ProductSubcategory]
                ON [Production].[Product].[ProductSubcategoryID] = [Production].[ProductSubcategory].[ProductSubcategoryID]
            GROUP BY [ProductCategoryID]
        ) AS CategoryRevenue
        ORDER BY Revenue DESC
    )
);


SELECT [SalesOrderID], [TerritoryID], [TotalDue]
FROM [Sales].[SalesOrderHeader] AS soh
WHERE [TotalDue] > (
    SELECT AVG([TotalDue])
    FROM [Sales].[SalesOrderHeader]
    WHERE [TerritoryID] = soh.[TerritoryID]
);


SELECT [ProductID], [Name]
FROM [Production].[Product]
WHERE [ProductID] IN (
    SELECT [ProductID]
    FROM [Sales].[SalesOrderDetail]
    GROUP BY [ProductID]
    HAVING COUNT(DISTINCT [SalesOrderID]) > 10
);
