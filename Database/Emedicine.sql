CREATE DATABASE Emedicine;
USE Emedicine;

CREATE TABLE Users(ID INT IDENTITY(1,1) PRIMARY KEY, FirstName VARCHAR(100),LastName VARCHAR(100), Password VARCHAR(100), Email VARCHAR(100),
Fund DECIMAL(18,2), Type VARCHAR(100), Status INT, CreatedOn Datetime);

CREATE TABLE Medicines(ID INT IDENTITY(1,1) PRIMARY KEY, Name VARCHAR(100),Manufacturer VARCHAR(100),
UnitPrice DECIMAL(18,2), Discount DECIMAL(18,2), Quantity INT, ExpDate Datetime, ImageUrl VARCHAR(100), Status INT);

CREATE TABLE Cart(ID INT IDENTITY(1,1) PRIMARY KEY, UserId INT, MedicineId INT, 
UnitPrice DECIMAL(18,2),Quantity INT, TotalPrice DECIMAL(18,2));

--[] is used because Order is a researved keyword in sql and using [...] is used to excape it
--No need again since I'm using Orders but i just want to note this.
CREATE TABLE Orders(ID INT IDENTITY(1,1) PRIMARY KEY, UserId INT, OrderNO VARCHAR(100), 
OrderTotal DECIMAL(18,2),OrderStatus VARCHAR(100));

CREATE TABLE OrderItems(ID INT IDENTITY(1,1) PRIMARY KEY, OrderId INT, MedicineId INT, UnitPrice DECIMAL(18,2),
Discount DECIMAL(18,2),Quantity INT, TotalPrice DECIMAL(18,2),);

SELECT * FROM Users;
SELECT * FROM Medicines;
SELECT * FROM Cart;
SELECT * FROM Orders;
SELECT * FROM OrderItems;
