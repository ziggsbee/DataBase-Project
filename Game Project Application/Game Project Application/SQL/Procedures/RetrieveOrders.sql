﻿CREATE OR ALTER PROCEDURE GameStore.RetrieveOrders
@OrderId INT,
@First NVARCHAR(20),
@Last NVARCHAR(20),
@Email NVARCHAR(64)

AS

SELECT O.OrderId, C.First, C.Last, C.Email, SUM(OL.Quantity) AS Count, SUM(OL.Quantity * OL.UnitPrice) AS Total
FROM GameStore.Orders O
INNER JOIN GameStore.Customers C ON C.CustomerId = O.CustomerId
INNER JOIN GameStore.OrderLines OL ON OL.OrderId = O.OrderId
GROUP BY O.OrderId, C.First, C.Last, C.Email
ORDER BY C.CustomerId ASC, O.OrderId ASC, @Last ASC, @First ASC
GO