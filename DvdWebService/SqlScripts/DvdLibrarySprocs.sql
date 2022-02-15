USE DvdLibrary
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdSelectAll')
      DROP PROCEDURE DvdSelectAll
GO

CREATE PROCEDURE DvdSelectAll
AS
	SELECT DvdId, Title, Director, ReleaseYear, Rating, Notes
	FROM Dvd d
	ORDER BY Title
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdSelectById')
      DROP PROCEDURE DvdSelectById
GO

CREATE PROCEDURE DvdSelectById (
	@DvdId int
)
AS
	SELECT DvdId, Title, Director, ReleaseYear, Rating, Notes
	FROM Dvd
	WHERE DvdId = @DvdId
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdInsert')
      DROP PROCEDURE DvdInsert
GO

CREATE PROCEDURE DvdInsert (
	@DvdId int output,
	@ReleaseYear varchar(4),
	@Rating varchar(5),
	@Director varchar(50),
	@Title varchar(50),
	@Notes varchar(150)
)
AS
	INSERT INTO Dvd (Title, Director, ReleaseYear, Rating, Notes)
	VALUES (@Title, @Director, @ReleaseYear, @Rating, @Notes)

	SET @DvdId = SCOPE_IDENTITY()
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdUpdate')
      DROP PROCEDURE DvdUpdate
GO

CREATE PROCEDURE DvdUpdate (
	@DvdId int,
	@ReleaseYear varchar(4),
	@Rating varchar(5),
	@Director varchar(50),
	@Title varchar(50),
	@Notes varchar(150)
)
AS
	UPDATE Dvd
		SET ReleaseYear = @ReleaseYear,
		Rating = @Rating,
		Title = @Title,
		Director = @Director,
		Notes = @Notes
	WHERE DvdId = @DvdId
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdDelete')
      DROP PROCEDURE DvdDelete
GO

CREATE PROCEDURE DvdDelete (
	@DvdId int
)
AS
	DELETE FROM Dvd
	WHERE DvdId = @DvdId
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdSelectByDirector')
      DROP PROCEDURE DvdSelectByDirector
GO

CREATE PROCEDURE DvdSelectByDirector (
	@Director varchar(50)
)
AS
	SELECT DvdId, Title, Director, ReleaseYear, Rating, Notes
	FROM Dvd
	WHERE Director LIKE @Director
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdSelectByRating')
      DROP PROCEDURE DvdSelectByRating
GO

CREATE PROCEDURE DvdSelectByRating (
	@Rating varchar(5)
)
AS
	SELECT DvdId, Title, Director, ReleaseYear, Rating, Notes
	FROM Dvd
	WHERE Rating LIKE @Rating
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdSelectByYear')
      DROP PROCEDURE DvdSelectByYear
GO

CREATE PROCEDURE DvdSelectByYear (
	@ReleaseYear varchar(4)
)
AS
	SELECT DvdId, Title, Director, ReleaseYear, Rating, Notes
	FROM Dvd
	WHERE ReleaseYear LIKE @ReleaseYear
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdSelectByTitle')
      DROP PROCEDURE DvdSelectByTitle
GO

CREATE PROCEDURE DvdSelectByTitle (
	@Title varchar(50)
)
AS
	SELECT DvdId, Title, Director, ReleaseYear, Rating, Notes
	FROM Dvd
	WHERE Title LIKE @Title
GO