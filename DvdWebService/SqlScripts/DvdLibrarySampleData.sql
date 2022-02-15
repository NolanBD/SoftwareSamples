USE DvdLibrary
GO

SET IDENTITY_INSERT Dvd ON

INSERT INTO Dvd (DvdId, Rating, Director, Title, ReleaseYear, Notes)
VALUES (1, 'G', 'Sam Jones', 'A Great Tale', '2015', 'This really is a great tale!'),
	(2, 'PG', 'Joe Smith', 'A Good Tale', '2012', 'This really is a good tale!'),
	(3, 'PG-13', 'Joe Baker', 'A Super Tale', '2011', 'This really is a super tale!'),
	(4, 'R', 'John Samson', 'A Bad Tale', '1999', 'This really is a bad tale!')
	(5, 'NC-17', 'Grug', 'A Terrible Tale', '2005', 'This really is a terrible tale!')

SET IDENTITY_INSERT Dvd OFF