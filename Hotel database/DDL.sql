CREATE TABLE EndUser
(
Id UNIQUEIDENTIFIER PRIMARY KEY,
Username varchar(30),
Password varchar (30)
)


CREATE TABLE Receptionist
(
Id UNIQUEIDENTIFIER  PRIMARY KEY,
FirstName varchar(30),
LastName varchar(30),
EndUserId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES EndUser(Id)
)


CREATE TABLE RoomType
(
Id UNIQUEIDENTIFIER PRIMARY KEY,
Description varchar(30),
HasBalcony BIT,
NumberOfBeds int,
)

CREATE TABLE Room
(
Id UNIQUEIDENTIFIER PRIMARY KEY,
Number int,
RoomTypeId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES RoomType(Id),
RoomFloor int,
Price decimal(8, 2)
)


CREATE TABLE Country
(
Id UNIQUEIDENTIFIER PRIMARY KEY,
Name varchar(30)
)

CREATE TABLE PostalOffice
(
Id UNIQUEIDENTIFIER PRIMARY KEY,
Number int,
CityName varchar(30),
CountryId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Country(Id)
)

CREATE TABLE Address
(
Id UNIQUEIDENTIFIER PRIMARY KEY,
StreetName varchar(30),
PostalOfficeId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES PostalOffice(Id)
)

CREATE TABLE Guest
(
Id UNIQUEIDENTIFIER PRIMARY KEY,
Pid varchar(20),
FirstName varchar(30),
LastName varchar(30),
RoomId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Room(Id),
AddressId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Address(Id),
IsActive BIT
)



CREATE TABLE Reservation 
(
Id UNIQUEIDENTIFIER PRIMARY KEY,
ReceptionistId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Receptionist(Id),
CreationDate datetime,
IsActive BIT,
GuestId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Guest(Id)
)

CREATE TABLE Payment
(
Id UNIQUEIDENTIFIER PRIMARY KEY,
Method varchar(30)
)


CREATE TABLE Receipt 
(
Id UNIQUEIDENTIFIER PRIMARY KEY,
Price decimal(8, 2),
Payment UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Payment(Id),
ReservationId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Reservation(Id),
)


CREATE TABLE RoomReservation
(
RoomId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Room(Id),
ReservationId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Reservation(Id),
StartDate datetime,
EndDate datetime,
IsActive BIT
)

SELECT Guest.Id, Guest.FirstName, Guest.LastName, Guest.Pid, Guest.IsActive, Room.Number
FROM Guest
LEFT JOIN Room
ON Guest.RoomId = Room.Id
WHERE 1=1 AND IsActive=0;

SELECT Guest.Id, Guest.FirstName, Guest.LastName, Guest.Pid, Guest.IsActive, Room.Number, Address.StreetName, PostalOffice.CityName, Country.Name
FROM Guest
INNER JOIN Room ON Guest.RoomId = Room.Id
INNER JOIN Address ON Guest.AddressId = Address.Id
INNER JOIN PostalOffice ON Address.PostalOfficeId = PostalOffice.Id
INNER JOIN Country ON Country.Id = PostalOffice.CountryId;

UPDATE Guest SET IsActive=1 WHERE Guest.Id = '17EEB2FA-1C58-4FB2-B46F-A782076E7308';
SELECT Room.Id, Room.Number, Room.RoomFloor, Room.Price, RoomType.Description, Roomtype.HasBalcony, RoomType.NumberOfBeds FROM Room LEFT JOIN RoomType ON Room.RoomTypeId = RoomType.Id WHERE 1=1 AND Room.Id NOT IN (SELECT RoomId FROM RoomReservation WHERE ('2022-05-01' <= EndDate AND '2022-05-04' >= StartDate))

SELECT * FROM Reservation WHERE GuestId = '...'


SELECT DATEDIFF(day, '2022-05-01', '2022-05-04');


SELECT * FROM Payment;
SELECT * FROM EndUser;
SELECT * FROM Receptionist;
SELECT * FROM RoomType;
SELECT * FROM Room;
SELECT * FROM Guest;
SELECT * FROM Address;
SELECT * FROM PostalOffice;
SELECT * FROM Country;
SELECT * FROM RoomReservation;
SELECT * FROM Receipt;
SELECT * FROM Reservation;
SELECT * FROM Payment;

SELECT * FROM RoomType WHERE HasBalcony = 0;


SELECT Room.Id, Room.Number, Room.RoomFloor, Room.Price, RoomType.Description, RoomType.HasBalcony, RoomType.NumberOfBeds
FROM Room
LEFT JOIN RoomType
ON Room.RoomTypeId = RoomType.Id
WHERE RoomType.Description = 'Regular' AND Room.Id NOT IN
(SELECT RoomId FROM RoomReservation WHERE ('2022-05-01' <= EndDate AND '2022-05-04' >= StartDate) AND IsActive = 1);


DELETE FROM RoomReservation;
DELETE FROM Receipt;
DELETE FROM Reservation;
DELETE FROM Guest;
DELETE FROM Address;
DELETE FROM PostalOffice;
DELETE FROM Country;
DELETE FROM Receptionist;
DELETE FROM EndUser;
DELETE FROM Room;
DELETE FROM RoomType;

DELETE FROM Payment;


DELETE FROM Guest WHERE FirstName = 'Marin';




DROP TABLE RoomReservation;
DROP TABLE Receipt;
DROP TABLE Reservation;
DROP TABLE Payment;
DROP TABLE Guest;
DROP TABLE Address;
DROP TABLE PostalOffice;
DROP TABLE Country;
DROP TABLE Receptionist;
DROP TABLE EndUser;
DROP TABLE Room;
DROP TABLE RoomType;

