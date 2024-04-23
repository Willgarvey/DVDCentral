BEGIN
	INSERT INTO tblMovie (Id, Title, Description, FormatId, DirectorId, RatingId, Cost, InStkQty, ImagePath)
	VALUES
	(1,'Star Wars', 'A science fiction coming of age story',1, 3, 2, 7.99, 14, 'starwars.jpg'),
	(2, 'The Godfather', 'Part 1 of an Oscar winning drama', 2, 2, 2, 13.99, 7, 'godfather.jpg'),
	(3, 'Iron Man', 'A super hero origin story', 3, 1, 1, 8.99, 12, 'ironman.jpg')
END