USE [master];
GO

if exists (select * from sys.databases where name = N'HotelDB')
begin
	EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = 'HotelDB';
	ALTER DATABASE HotelDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE HotelDB;
end

CREATE DATABASE HotelDB;
GO

USE HotelDB;

CREATE TABLE RoomType (
	[Type] NVARCHAR(15) NOT NULL PRIMARY KEY,
	StandardOccupancy INT NOT NULL,
	MaxOccupancy INT NOT NULL,
	BasePrice DECIMAL(5, 2) NOT NULL,
	ExtraPerson DECIMAL(5, 2) NULL

);

CREATE TABLE Guest (
	GuestID INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	PhoneNumber NVARCHAR(15) NOT NULL,
	[Address] NVARCHAR(40) NOT NULL,
	City NVARCHAR(40) NOT NULL,
	[State] CHAR(2) NOT NULL,
	ZIP CHAR(5) NOT NULL,
	FirstName NVARCHAR(40) NOT NULL,
	LastName NVARCHAR(40) NOT NULL
);

CREATE TABLE Amenity (
	AmenityID int NOT NULL PRIMARY KEY IDENTITY(1, 1),
	AmenityType NVARCHAR(15) NOT NULL,
	ExtraCharge DECIMAL(5, 2) NULL
);

CREATE TABLE Reservation (
	ReservationID INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	CheckInDate DATE NOT NULL,
	CheckOutDate DATE NOT NULL,
	GuestID INT NOT NULL,
	CONSTRAINT fk_Reservation_Guest
		FOREIGN KEY (GuestID)
		REFERENCES Guest(GuestID)
);

CREATE TABLE Room (
	RoomNumber INT NOT NULL PRIMARY KEY,
	ADA BIT NOT NULL DEFAULT(1),
	[Type] NVARCHAR(15) NOT NULL
	CONSTRAINT fk_RoomType_Room
		FOREIGN KEY ([Type])
		REFERENCES RoomType([Type])
);

CREATE TABLE RoomReservation (
	ReservationID INT NOT NULL,
	RoomNumber INT NOT NULL,
	AdultGuests INT NOT NULL,
	ChildGuests INT NULL,
	TotalCost DECIMAL(7, 2),
	CONSTRAINT pk_RoomReservation PRIMARY KEY (ReservationID, RoomNumber),
	CONSTRAINT fk_RoomReservation_Room FOREIGN KEY (RoomNumber)
		REFERENCES Room(RoomNumber),
	CONSTRAINT fk_RoomReservation_Reservation FOREIGN KEY (ReservationID)
		REFERENCES Reservation(ReservationID)
);

CREATE TABLE RoomAmenity (
	RoomNumber INT NOT NULL,
	AmenityID INT NOT NULL,
	CONSTRAINT pk_RoomAmenity PRIMARY KEY (RoomNumber, AmenityID),
	CONSTRAINT fk_RoomAmenity_Room FOREIGN KEY (RoomNumber)
		REFERENCES Room(RoomNumber),
	CONSTRAINT fk_RoomAmenity_Amenity FOREIGN KEY (AmenityID)
		REFERENCES Amenity(AmenityID)
);