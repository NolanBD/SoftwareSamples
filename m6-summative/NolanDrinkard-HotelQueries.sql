USE HotelDB;

--1. Write a query that returns a list of reservations that end in July 2023, including the name of the guest, 
--	the room number(s), and the reservation dates.
SELECT 
	r.CheckOutDate,
	r.ReservationID,
	g.FirstName,
	g.LastName
FROM Reservation r
INNER JOIN Guest g ON r.GuestID = g.GuestID
WHERE r.CheckOutDate BETWEEN '2023/06/30' AND '2023/08/01';

--CheckOutDate  ReservationID   FirstName   LastName
--2023-07-02	14				Nolan		Drinkard
--2023-07-14	15				Walter		Holaway
--2023-07-21	16				Wilfred		Vise
--2023-07-29	17				Bettyann	Seery

--2. Write a query that returns a list of all reservations for rooms with a jacuzzi,
--displaying the guest's name, the room number, and the dates of the reservation.

SELECT
	r.CheckInDate,
	r.CheckOutDate,
	r.ReservationID,
	rr.RoomNumber,
	g.FirstName,
	g.LastName,
	ra.AmenityID
FROM Reservation r
INNER JOIN Guest g ON r.GuestID = g.GuestID
INNER JOIN RoomReservation rr ON rr.ReservationID = r.ReservationID
INNER JOIN RoomAmenity ra ON rr.RoomNumber = ra.RoomNumber
WHERE ra.AmenityID = 4;

--CheckInDate   CheckOutDate  ReservationID   RoomNumber	FirstName	LastName AmenityID
--2023-02-05	2023-02-10	  2  	          203			Bettyann	Seery		4
--2023-02-22	2023-02-24	  3	              305			Duane		Cullison	4
--2023-03-06	2023-03-07	  4				  201			Karie		Yang		4
--2023-03-17	2023-03-20	  5				  307			Nolan		Drinkard	4
--2023-04-09	2023-04-13	  9				  301			Walter		Holaway		4
--2023-04-23	2023-04-23	  10			  207			Wilfred		Vise		4
--2023-06-28	2023-07-02	  14			  205			Nolan		Drinkard	4
--2023-07-28	2023-07-29	  17			  303			Bettyann	Seery		4
--2023-07-28	2023-07-29	  17			  305			Bettyann	Seery		4
--2023-09-16	2023-09-17	  19			  203			Mack		Simmer		4
--2023-11-22	2023-11-25	  21			  301			Duane		Cullison	4

--3. Write a query that returns all the rooms reserved for a specific guest,
--including the guest's name, the room(s) reserved, the starting date of the reservation, 
--and how many people were included in the reservation.

SELECT
	r.CheckInDate,
	rr.RoomNumber,
	rr.AdultGuests + rr.ChildGuests AS TotalGuests,
	g.FirstName,
	g.LastName
From Reservation r
INNER JOIN Guest g ON r.GuestID = g.GuestID
INNER JOIN RoomReservation rr ON r.ReservationID = rr.ReservationID
WHERE g.GuestID = 1;

--CheckInDate	RoomNumber  TotalGuests		FirstName	LastName
--2023-03-17	307			2				Nolan		Drinkard
--2023-06-28	205			2				Nolan		Drinkard

--4. Write a query that returns a list of rooms, reservation ID, and per-room cost for each reservation. 
--The results should include all rooms, whether or not there is a reservation associated with the room.

SELECT 
	r.RoomNumber,
	room_res.ReservationID,
	room_res.TotalCost
FROM Room r
FULL OUTER JOIN RoomReservation room_res ON r.RoomNumber = room_res.RoomNumber
LEFT OUTER JOIN Reservation res ON room_res.ReservationID = res.ReservationID

--RoomNumber	ReservationID	TotalCost
--201			4				199.99
--202			7				349.98
--203			2				999.95
--203			19				399.98
--204			15				184.99
--205			14				699.96
--206			12				599.96
--206			21				449.97
--207			10				174.99
--208			12				599.96
--208			18				149.99
--301			9				799.96
--301			21				599.97
--302			6				924.95
--302			22				699.96
--303			17				199.99
--304			13				184.99
--305			3				349.98
--305			17				349.98
--306			NULL			NULL
--307			5				524.97
--308			1				299.98
--401			11				1199.97
--401			16				1259.97
--401			20				1199.97
--402			NULL			NULL

--5. Write a query that returns all the rooms accommodating at least three guests 
--and that are reserved on any date in April 2023.

SELECT 
	r.RoomNumber,
	res.CheckInDate,
	res.CheckOutDate
FROM Room r
INNER JOIN RoomType rt ON r.[Type] = rt.[Type]
INNER JOIN RoomReservation room_res ON r.RoomNumber = room_res.RoomNumber
INNER JOIN Reservation res ON room_res.ReservationID = res.ReservationID
WHERE room_res.AdultGuests + room_res.ChildGuests >=3
	AND ((res.CheckInDate > '2023-03-31' AND res.CheckInDate < '2023-05-01') OR (res.CheckOutDate < '2023-05-01' AND res.CheckOutDate > '2023-03-31'));

--RoomNumber	CheckInDate		CheckOutDate

--6. Write a query that returns a list of all guest names and the number of reservations per guest, 
--sorted starting with the guest with the most reservations and then by the guest's last name.

SELECT
	g.FirstName,
	g.LastName,
	COUNT(r.ReservationID) AS NumberOfReservations
FROM Guest g
FULL OUTER JOIN Reservation r ON g.GuestID = r.GuestID
GROUP BY g.FirstName, g.LastName
ORDER BY NumberOfReservations DESC, g.LastName;

--FirstName		LastName	NumberOfReservations
--Bettyann		Seery		3
--Mack			Simmer		3
--Duane			Cullison	2
--Nolan			Drinkard	2
--Walter		Holaway		2
--Aurore		Lipton		2
--Maritza		Tilton		2
--Wilfred		Vise		2
--Karie			Yang		2
--Zachery		Luechtefeld	1
--Joleen		Tison		1

--7. Write a query that displays the name, address, 
--and phone number of a guest based on their phone number.

SELECT
	g.PhoneNumber,
	g.FirstName,
	g.LastName,
	g.[Address]
FROM Guest g
WHERE g.PhoneNumber = '(291) 553-0508'

--PhoneNumber		FirstName	LastName	Address
--(291) 553-0508	Mack		Simmer		379 Old Shore Street