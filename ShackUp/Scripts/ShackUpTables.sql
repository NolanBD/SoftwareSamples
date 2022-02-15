Use ShackUp
Go

IF EXISTS(SELECT * FROM sys.tables WHERE name='Contacts')
	DROP TABLE Contacts
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Favorites')
	DROP TABLE Favorites
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Listings')
	DROP TABLE Listings
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='States')
	DROP TABLE States
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='BathroomTypes')
	DROP TABLE BathroomTypes
GO

CREATE TABLE BathroomTypes (
	BathroomTypeID int identity(1,1) not null primary key,
	BathroomTypeName varchar(15) not null
)

CREATE TABLE States (
	StateID char(2) not null primary key,
	StateName varchar(15) not null
)

CREATE TABLE Listings (
	ListingID int identity(1,1) not null primary key,
	UserID nvarchar(128) not null,
	StateID char(2) not null foreign key references States(StateID),
	BathroomTypeID int null foreign key references BathroomTypes(BathroomTypeID),
	Nickname nvarchar(50) not null,
	City nvarchar(50) not null,
	Rate decimal(7,2) not null,
	SquareFootage decimal(7,2) not null,
	HasElectric bit not null,
	HasHeat bit not null,
	ListingDescription varchar(500) null,
	ImageFileName varchar(50),
	CreatedDate datetime2 not null default(getdate())
)

CREATE TABLE Contacts (
	ListingID int not null foreign key references Listings(ListingID),
	UserID nvarchar(128) not null,
	primary key (ListingID, UserID)
)

CREATE TABLE Favorites (
	ListingID int not null foreign key references Listings(ListingID),
	UserID nvarchar(128) not null,
	primary key (ListingID, UserID)
)