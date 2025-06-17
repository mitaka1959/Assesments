USE AdventureWorks2022;

--SELECT FirstName, LastName, BusinessEntityID AS Employee_ID
--FROM Person.Person
--ORDER BY LastName;


--SELECT 
    --Person.BusinessEntityID, 
    --Person.FirstName, 
    --Person.LastName, 
    --PersonPhone.PhoneNumber
--FROM Person.Person, Person.PersonPhone
--WHERE Person.BusinessEntityID = PersonPhone.BusinessEntityID
  --AND Person.LastName LIKE 'L%'
--ORDER BY Person.LastName, Person.FirstName;


--SELECT 
    --ROW_NUMBER() OVER (PARTITION BY a.PostalCode ORDER BY s.SalesYTD DESC) AS RowNum,
    --p.LastName,
    --s.SalesYTD,
    --a.PostalCode
--FROM 
    --Sales.SalesPerson s, 
    --Person.Person p, 
    --Person.BusinessEntityAddress bea, 
    --Person.Address a
--WHERE 
    --s.BusinessEntityID = p.BusinessEntityID
    --AND s.BusinessEntityID = bea.BusinessEntityID
    --AND bea.AddressID = a.AddressID
    --AND s.TerritoryID IS NOT NULL
    --AND s.SalesYTD > 0
--ORDER BY 
    --a.PostalCode ASC, 
    --s.SalesYTD DESC;



SELECT  SalesOrderID,
SUM(LineTotal) AS TotalCost
FROM Sales.SalesOrderDetail
GROUP BY SalesOrderID
HAVING SUM(LineTotal) > 100000;


