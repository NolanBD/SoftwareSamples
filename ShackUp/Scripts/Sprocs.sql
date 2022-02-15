USE ShackUp
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'StatesSelectAll')
		DROP PROCEDURE StatesSelectAll
GO

CREATE PROCEDURE StatesSelectAll AS
BEGIN
	SELECT StateID, StateName
	FROM States
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'BathroomTypeSelectAll')
		DROP PROCEDURE BathroomTypeSelectAll
GO

CREATE PROCEDURE BathroomTypeSelectAll AS
BEGIN
	SELECT BathroomTypeID, BathroomTypeName
	FROM BathroomTypes
	ORDER BY BathroomTypeName
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ListingInsert')
		DROP PROCEDURE ListingInsert
GO

CREATE PROCEDURE ListingInsert (
	@ListingID int output,
	@UserID nvarchar(128),
	@StateID char(2),
	@BathroomTypeID int,
	@Nickname varchar(50),
	@City nvarchar(50),
	@Rate decimal(7,2),
	@SquareFootage decimal(7,2),
	@HasElectric bit,
	@HasHeat bit,
	@ListingDescription varchar(500),
	@ImageFileName varchar(50)
) AS
BEGIN
	INSERT INTO Listings (UserID, StateID, BathroomTypeID, Nickname, City, Rate, SquareFootage, HasElectric, HasHeat, ListingDescription, ImageFileName)
	VALUES (@UserID, @StateID, @BathroomTypeID, @Nickname, @City, @Rate, @SquareFootage, @HasElectric, @HasHeat, @ListingDescription, @ImageFileName);

	SET @ListingID = SCOPE_IDENTITY();
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ListingUpdate')
		DROP PROCEDURE ListingUpdate
GO

CREATE PROCEDURE ListingUpdate (
	@ListingID int,
	@UserID nvarchar(128),
	@StateID char(2),
	@BathroomTypeID int,
	@Nickname varchar(50),
	@City nvarchar(50),
	@Rate decimal(7,2),
	@SquareFootage decimal(7,2),
	@HasElectric bit,
	@HasHeat bit,
	@ListingDescription varchar(500),
	@ImageFileName varchar(50)
) AS
BEGIN
	UPDATE Listings SET 
	UserID = @UserID, 
	StateID = @StateID,
	BathroomTypeID = @BathroomTypeID,
	Nickname = @Nickname,
	City = @City,
	Rate = @Rate,
	SquareFootage = @SquareFootage,
	HasElectric = @HasElectric,
	HasHeat = @HasHeat,
	ListingDescription = @ListingDescription,
	ImageFileName = @ImageFileName
	WHERE ListingID = @ListingID
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ListingDelete')
		DROP PROCEDURE ListingDelete
GO

CREATE PROCEDURE ListingDelete (
	@ListingID int
) AS
BEGIN
	BEGIN TRANSACTION

	DELETE FROM Contacts WHERE ListingID = @ListingID;
	DELETE FROM Favorites WHERE ListingID = @ListingID;
	DELETE FROM Listings WHERE ListingID = @ListingID;

	COMMIT TRANSACTION
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ListingSelect')
		DROP PROCEDURE ListingSelect
GO

CREATE PROCEDURE ListingSelect (
	@ListingID int
) AS
BEGIN
	SELECT ListingID, UserID, StateID, BathroomTypeID, Nickname, City, Rate, SquareFootage, HasElectric, HasHeat, ListingDescription, ImageFileName
	FROM Listings
	WHERE ListingID = @ListingID
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ListingSelectRecent')
		DROP PROCEDURE ListingSelectRecent
GO

CREATE PROCEDURE ListingSelectRecent AS
BEGIN
	SELECT TOP 5 ListingID, UserID, Rate, City, StateID, ImageFileName
	FROM Listings
	ORDER BY CreatedDate DESC
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ListingSelectDetails')
		DROP PROCEDURE ListingSelectDetails
GO

CREATE PROCEDURE ListingSelectDetails (
	@ListingID int
) AS
BEGIN
	SELECT ListingID, UserID, Nickname, StateID, City, Rate, SquareFootage, HasElectric, HasHeat, l.ListingDescription, l.BathroomTypeID, 
	BathroomTypeName, ImageFileName
	FROM Listings l
		INNER JOIN BathroomTypes b on l.BathroomTypeID = b.BathroomTypeID
	WHERE ListingID = @ListingID
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ListingSelectFavorites')
		DROP PROCEDURE ListingSelectFavorites
GO

CREATE PROCEDURE ListingSelectFavorites (
	@UserID nvarchar(128)	
) AS
BEGIN
	SELECT l.ListingID, l.City, l.StateID, l.Rate, l.SquareFootage, l.UserID,
	l.HasElectric, l.HasHeat, b.BathroomTypeName, l.BathroomTypeID
	FROM Favorites f
		INNER JOIN Listings l on f.ListingID = l.ListingID
		INNER JOIN BathroomTypes b on l.BathroomTypeId = b.BathroomTypeID
	WHERE f.UserID = @UserID;
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ListingSelectContacts')
		DROP PROCEDURE ListingSelectContacts
GO

CREATE PROCEDURE ListingSelectContacts (
	@UserID nvarchar(128)	
) AS
BEGIN
	SELECT l.ListingID, u.Email, u.Id as UserID, l.Nickname, l.City, l.StateID, l.Rate
	FROM Listings l
		INNER JOIN Contacts c ON l.ListingID = c.ListingID
		INNER JOIN AspNetUsers u on c.UserID = u.Id 
	WHERE l.UserID = @UserID;
END
GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ListingSelectByUser')
		DROP PROCEDURE ListingSelectByUser
GO

CREATE PROCEDURE ListingSelectByUser (
	@UserID nvarchar(128)
) AS
BEGIN
	SELECT ListingID, UserID, Nickname, StateID, City, Rate, SquareFootage, HasElectric, HasHeat, l.BathroomTypeID, 
	BathroomTypeName, ImageFileName
	FROM Listings l
		INNER JOIN BathroomTypes b on l.BathroomTypeID = b.BathroomTypeID
	WHERE UserID = @UserID
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'FavoriteInsert')
		DROP PROCEDURE FavoriteInsert
GO

CREATE PROCEDURE FavoriteInsert(
	@UserID nvarchar(128),
	@ListingID int
) AS
BEGIN
	INSERT INTO Favorites(UserID, ListingID)
	VALUES (@UserID, @ListingID)
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'FavoriteDelete')
		DROP PROCEDURE FavoriteDelete
GO

CREATE PROCEDURE FavoriteDelete(
	@UserID nvarchar(128),
	@ListingID int
) AS
BEGIN
	DELETE FROM Favorites
	WHERE UserID = @UserID AND ListingID = @ListingID
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ContactInsert')
		DROP PROCEDURE ContactInsert
GO

CREATE PROCEDURE ContactInsert(
	@UserID nvarchar(128),
	@ListingID int
) AS
BEGIN
	INSERT INTO Contacts(UserID, ListingID)
	VALUES (@UserID, @ListingID)
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ContactDelete')
		DROP PROCEDURE ContactDelete
GO

CREATE PROCEDURE ContactDelete(
	@UserID nvarchar(128),
	@ListingID int
) AS
BEGIN
	DELETE FROM Contacts
	WHERE UserID = @UserID AND ListingID = @ListingID
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ContactSelect')
		DROP PROCEDURE ContactSelect
GO

CREATE PROCEDURE ContactSelect(
	@UserID nvarchar(128),
	@ListingID int
) AS
BEGIN
	SELECT UserID, ListingID
	FROM Contacts
	WHERE UserID = @UserID AND ListingID = @ListingID
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'FavoriteSelect')
		DROP PROCEDURE FavoriteSelect
GO

CREATE PROCEDURE FavoriteSelect(
	@UserID nvarchar(128),
	@ListingID int
) AS
BEGIN
	SELECT UserID, ListingID
	FROM Favorites
	WHERE UserID = @UserID AND ListingID = @ListingID
END
GO