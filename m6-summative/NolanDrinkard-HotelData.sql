USE HotelDB;

INSERT INTO Guest (FirstName, LastName, PhoneNumber, [Address], City, [State], ZIP) VALUES
	('Nolan', 'Drinkard', '(330) 554-6239', '404 North Mantua Street', 'Kent', 'OH', '44240'),
	('Mack', 'Simmer', '(291) 553-0508', '379 Old Shore Street', 'Council Bluffs', 'IA', '51501'),
	('Bettyann', 'Seery', '(478) 277-9632', '750 Wintergreen Dr.', 'Wasilla', 'AK', '99654'),
	('Duane', 'Cullison', '(308) 494-0198', '9662 Foxrun Lane', 'Harlingen', 'TX', '78552'),
	('Karie', 'Yang', '(214) 730-0298', '9378 W. Augusta Ave.', 'West Deptford', 'NJ', '08096'),
	('Aurore', 'Lipton', '(377) 507-0974', '762 Wild Rose Street', 'Saginaw', 'MI', '48601'),
	('Zachery', 'Luechtefeld', '(814) 485-2615', '7 Poplar Dr.', 'Arvada', 'CO', '80003'),
	('Jeremiah', 'Pendergrass', '(279) 491-0960', '70 Oakwood St.', 'Zion', 'IL', '60099'),
	('Walter', 'Holaway', '(446) 396-6785', '7556 Arrowhead St.', 'Cumberland', 'RI', '02864'),
	('Wilfred', 'Vise', '(834) 727-1001', '77 West Surrey Street', 'Oswego', 'NY', '13126'),
	('Maritza', 'Tilton', '(446) 351-6860', '939 Linda Rd.', 'Burke', 'VA', '22015'),
	('Joleen', 'Tison', '(231) 893-2755', '87 Queen St.', 'Drexel Hill', 'PA', '19026');

INSERT INTO Amenity (AmenityType, ExtraCharge) VALUES
	('Microwave', NULL),
	('Refrigerator', NULL),
	('Oven', NULL),
	('Jacuzzi', 25);

INSERT INTO RoomType ([Type], ExtraPerson, MaxOccupancy, StandardOccupancy, BasePrice) VALUES
	('Single', NULL, 2, 2, 149.99),
	('Double', 10, 4, 2, 174.99),
	('Suite', 20, 8, 3, 399.99);

INSERT INTO Room (RoomNumber, ADA, [Type]) VALUES
	(201, 0, 'Double'),
	(202, 1, 'Double'),
	(203, 0, 'Double'),
	(204, 1, 'Double'),
	(205, 0, 'Single'),
	(206, 1, 'Single'),
	(207, 0, 'Single'),
	(208, 1, 'Single'),
	(301, 0, 'Double'),
	(302, 1, 'Double'),
	(303, 0, 'Double'),
	(304, 1, 'Double'),
	(305, 0, 'Single'),
	(306, 1, 'Single'),
	(307, 0, 'Single'),
	(308, 1, 'Single'),
	(401, 1, 'Suite'),
	(402, 1, 'Suite');

INSERT INTO Reservation (GuestID, CheckInDate, CheckOutDate) VALUES
	(2, '2023/02/02', '2023/02/04'),
	(3, '2023/02/05', '2023/02/10'),
	(4, '2023/02/22', '2023/02/24'),
	(5, '2023/03/06', '2023/03/07'),
	(1, '2023/03/17', '2023/03/20'),
	(6, '2023/03/18', '2023/03/23'),
	(7, '2023/03/29', '2023/03/31'),
	(8, '2023/01/31', '2023/04/05'),
	(9, '2023/04/09', '2023/04/13'),
	(10, '2023/04/23', '2023/04/23'),
	(11, '2023/05/30', '2023/06/02'),
	(12, '2023/06/10', '2023/05/14'),
	(6, '2023/06/17', '2023/06/18'),
	(1, '2023/06/28', '2023/07/02'),
	(9, '2023/07/13', '2023/07/14'),
	(10, '2023/07/18', '2023/07/21'),
	(3, '2023/07/28', '2023/07/29'),
	(3, '2023/08/23', '2023/09/01'),
	(2, '2023/09/16', '2023/09/17'),
	(5, '2023/09/13', '2023/09/15'),
	(4, '2023/11/22', '2023/11/25'),
	(2, '2023/11/22', '2023/11/25'),
	(11, '2023/12/24', '2023/12/28');

INSERT INTO RoomReservation (ReservationID, RoomNumber, AdultGuests, ChildGuests, TotalCost) VALUES
	(1, 308, 1, 0, 299.98),
	(2, 203, 2, 1, 999.95),
	(3, 305, 2, 0, 349.98),
	(4, 201, 2, 2, 199.99),
	(5, 307, 1, 1, 524.97),
	(6, 302, 3, 0, 924.95),
	(7, 202, 2, 2, 349.98),
	(8, 304, 2, 0, 874.95),
	(9, 301, 1, 0, 799.96),
	(10, 207, 1, 1, 174.99),
	(11, 401, 2, 4, 1199.97),
	(12, 206, 2, 0, 599.96),
	(12, 208, 1, 0, 599.96),
	(13, 304, 3, 0, 184.99),
	(14, 205, 2, 0, 699.96),
	(15, 204, 3, 1, 184.99),
	(16, 401, 4, 2, 1259.97),
	(17, 303, 2, 1, 199.99),
	(17, 305, 1, 0, 349.98),
	(18, 208, 2, 0, 149.99),
	(19, 203, 2, 2, 399.98),
	(20, 401, 2, 2, 1199.97),
	(21, 206, 2, 0, 449.97),
	(21, 301, 2, 2, 599.97),
	(22, 302, 2, 0, 699.96);

INSERT INTO RoomAmenity (RoomNumber, AmenityID) VALUES
	(201, 1),
	(201, 4),
	(202, 2),
	(203, 1),
	(203, 4),
	(204, 2),
	(205, 1),
	(205, 2),
	(205, 4),
	(206, 1),
	(206, 2),
	(207, 1),
	(207, 2),
	(207, 4),
	(208, 1),
	(208, 2),
	(301, 1),
	(301, 4),
	(302, 2),
	(303, 1),
	(303, 4),
	(304, 2),
	(305, 1),
	(305, 2),
	(305, 4),
	(306, 1),
	(306, 2),
	(307, 1),
	(307, 2),
	(307, 4),
	(308, 1),
	(308, 2),
	(401, 1),
	(401, 2),
	(401, 3),
	(402, 1),
	(402, 2),
	(402, 3);

SELECT * FROM Guest;
SELECT * FROM Reservation;
SELECT * FROM RoomReservation;

DELETE rr FROM RoomReservation rr
INNER JOIN Reservation r
	ON rr.ReservationID = r.ReservationID
WHERE r.GuestID = 8;

DELETE FROM Reservation
WHERE GuestId = 8;

DELETE FROM Guest
WHERE GuestID = 8;

SELECT * FROM Guest;
SELECT * FROM Reservation;
SELECT * FROM RoomReservation;
