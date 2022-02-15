USE master
GO

IF EXISTS(SELECT * FROM sys.databases WHERE name='DvdLibrary')
DROP DATABASE DvdLibrary
GO

CREATE DATABASE DvdLibrary
GO

USE DvdLibrary
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Dvd')
	DROP TABLE Dvd
GO

CREATE TABLE Dvd (
	DvdId int identity(1,1) primary key not null,
	Rating varchar(5) not null,
	Director varchar(50) not null,
	Title varchar(50) not null,
	ReleaseYear varchar(4) not null,
	Notes varchar(150) null
)