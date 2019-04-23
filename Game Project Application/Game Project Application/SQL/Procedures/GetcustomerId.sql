﻿CREATE OR ALTER PROCEDURE GameStore.GetCustomerId
	@Email NVARCHAR(64),
	@FirstName NVARCHAR(64),
	@LastName NVARCHAR(64)
AS


SELECT IIF( C.Email = @Email AND C.FirstName = @FirstName AND C.LastName = @LastName, C.CustomerId, -1) AS CustomerId
FROM GameStore.Customers C
GO