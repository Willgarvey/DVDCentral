BEGIN
	INSERT INTO tblCustomer (Id, FirstName, LastName, UserId, Address, City, State, Zip, Phone, ImagePath)
	VALUES
	(1, 'Bart', 'Simpson', 1, '20 Main Street', 'Springfield', 'IL', '22334', '9204442233', '.\path.png'),
	(2, 'Michael', 'Jordan', 2, '2150 South Grace Street', 'Charleston', 'WV', '22335', '9204441456', '.\path.png'),
	(3, 'Marty', 'McFly', 3, '40 Grove Avenue', 'Harriet',  'VA', '22337','9202109803', '.\path.png')
END