create database Prog7311task2

create table Users(
Username varchar(255) NOT NULL PRIMARY KEY,
Password varchar(255),
Status varchar(255)
)
create table Products(
productName varchar(255) NOT NULL PRIMARY KEY,
productPrice int,
productCategory varchar(255)
)
create table Orders(
orderId int NOT NULL PRIMARY KEY,
Username varchar(255) FOREIGN KEY REFERENCES Users(Username),
productName varchar(255) FOREIGN KEY REFERENCES Products(productName),
)


INSERT INTO Users
VALUES('BigBuyer' ,'FF2AF797E083231D9DEA3500DB18A7A5440617528CB165B8623D66A4C6E33708' , 'Customer'),
      ('JonHanmon' ,'85A39AB345D672FF8CA9B9C6876F3ADCACF45EE7C1E2DBD2408FD338BD55E07E' , 'Employee')

INSERT INTO Products
VALUES('6 Pack Apples',35,'Produce'),
	  ('6 Pack Oranges',45,'Produce'),
      ('ToolKit',200,'Hardware'),
	  ('5 Planks of Wood',350,'DIY')

INSERT INTO Orders
VALUES(1,'BigBuyer','ToolKit'),
	  (2,'BigBuyer','5 Planks of Wood')



select * from Orders
select * from Products
select * from Users
