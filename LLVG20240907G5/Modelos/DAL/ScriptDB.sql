create database dbLLVG20240907;

use dbLLVG20240907;

create table ProductLLVG(
Id int identity(1,1) primary key,
NombreLLVG varchar(50)NOT NULL,
DescripcionLLVG varchar(50),
PrecioLLVG Decimal(10,2)NOT NULL
);