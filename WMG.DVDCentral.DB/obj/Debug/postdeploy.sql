/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

BEGIN
	INSERT INTO tblCustomer (Id, FirstName, LastName, UserId, Address, City, State, Zip, Phone, ImagePath)
	VALUES
	(1, 'Bart', 'Simpson', 1, '20 Main Street', 'Springfield', 'IL', '22334', '9204442233', '.\path.png'),
	(2, 'Michael', 'Jordan', 2, '2150 South Grace Street', 'Charleston', 'WV', '22335', '9204441456', '.\path.png'),
	(3, 'Marty', 'McFly', 3, '40 Grove Avenue', 'Harriet',  'VA', '22337','9202109803', '.\path.png')
END
BEGIN
	INSERT INTO tblDirector (Id, FirstName, LastName)
	VALUES
	(1, 'Jon', 'Favreau'),
	(2, 'Francis', 'Copala'),
	(3, 'George', 'Lucas')
END
BEGIN
	INSERT INTO tblFormat (Id, Description)
	VALUES
	(1, 'DVD'),
	(2, 'Blu Ray'),
	(3, 'VHS')
END
BEGIN
	INSERT INTO tblGenre (Id, Description)
	VALUES
	(1, 'Adventure'),
	(2, 'Drama'),
	(3, 'Science Fiction')
END
BEGIN
	INSERT INTO tblMovie (Id, Title, Description, FormatId, DirectorId, RatingId, Cost, InStkQty, ImagePath)
	VALUES
	(1,'Star Wars', 'A science fiction coming of age story',1, 3, 2, 7.99, 14, 'starwars.jpg'),
	(2, 'The Godfather', 'Part 1 of an Oscar winning drama', 2, 2, 2, 13.99, 7, 'godfather.jpg'),
	(3, 'Iron Man', 'A super hero origin story', 3, 1, 1, 8.99, 12, 'ironman.jpg')
END
BEGIN
	INSERT INTO tblMovieGenre (Id, MovieId, GenreId)
	VALUES
	(1, 1, 1),
	(2, 2, 2),
	(3, 3, 3)
END
BEGIN
	INSERT INTO tblOrder (Id, CustomerId, OrderDate, ShipDate, UserId)
	VALUES
	(1,1,2020-09-15,2020-09-16,1),
	(2,2,2019-08-08,2019-09-01,2),
	(3,2,2018-07-07,2018-07-12,2)
	END
BEGIN
	INSERT INTO tblOrderItem (Id, OrderId, Quantity, MovieId, Cost)
	VALUES
	(1,1,1,1,7.99),
	(2,2,1,2,13.99),
	(3,3,1,3,8.99)
END
BEGIN
	INSERT INTO tblRating (Id, Description)
	VALUES
	(1, 'PG'),
	(2, 'PG-13'),
	(3, 'R')
END
GO
