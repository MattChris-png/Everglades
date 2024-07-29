--CREATE SCHEMA Product
/*
CREATE TABLE PRODUCT (
	Id int IDENTITY(1,1) NOT NULL,
	[Name] varchar(255) NULL,
	[Description] nvarchar(max) NULL,
	Quantity int NOT NULL,
	Price numeric(10,2) NOT NULL,
	Useless1 nvarchar(50) NULL,

)
*/

CREATE PROCEDURE Product.UpdateProduct
@Name nvarchar(255)
, @Description nvarchar(max)
, @Quantity int
, @Price numeric(10,2)
, @Id int output
AS 
BEGIN
	UPDATE PRODUCT
	SET
	Name = @Name
	,Description = @Description
	, Quantity = @Quantity
	, Price = @Price
	WHERE
		ID = @Id
END


CREATE PROCEDURE Product.InsertProduct 
@Name nvarchar(255)
, @Description nvarchar(max)
, @Quantity int
, @Price numeric(10,2)
, @Id int output
AS 
BEGIN
	INSERT INTO PRODUCT([Name], [Description], Quantity, Price)
	VALUES(@Name, @Description, @Quantity, @Price)

	SET @Id = SCOPE_IDENTITY()
END


CREATE PROCEDURE Product.DeleteProduct
@Id int
AS
BEGIN
	DELETE FROM PRODUCT
	WHERE Id = @Id
END
GO

declare @newId int
exec Product.InsertProduct @Name = 'SP Product',
@Description = 'Product inserted from SP',
@Quantity = 10,
@Price = 1.23,
@Id = @newID out

select @newId


SELECT
  *
FROM
  INFORMATION_SCHEMA.TABLES;
GO

Select * FROM PRODUCT Order by ID

sp_help Product