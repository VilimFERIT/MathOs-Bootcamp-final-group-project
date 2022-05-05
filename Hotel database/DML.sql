INSERT INTO RoomType VALUES --Id, Description, HasBalcony, NumberOfBeds
('6d293d20-197e-4662-aa68-386230de3206', 'Regular', 0 , 2),
('1c346e1f-c899-43ec-baf3-7f6213ad4b3d', 'VIP', 1, 3);


INSERT INTO Room VALUES --RoomId, Number, RoomTypeId, Floor, Price
('578157b1-41ea-4488-86e8-4d78d640bcfe', 1, '6D293D20-197E-4662-AA68-386230DE3206', 1, 250.00),
('a6c9a09f-1756-4283-9a46-33d25503c1d5', 2, '6d293d20-197e-4662-aa68-386230de3206', 1, 250.00),
('65cfe441-51c6-4d7d-a1ae-f98dcdf72ec1', 3, '6d293d20-197e-4662-aa68-386230de3206', 1, 250.00),
('dc245b5e-671e-42e2-898e-961ad14ecb41', 4, '1c346e1f-c899-43ec-baf3-7f6213ad4b3d', 2, 400.00),
('47031a68-b186-4d49-b4cc-5df168005dc7', 5, '1c346e1f-c899-43ec-baf3-7f6213ad4b3d', 2, 400.00),
('7a19a83a-7fe0-4a2f-9cfd-6fbdd6b71abd', 6, '1c346e1f-c899-43ec-baf3-7f6213ad4b3d', 2, 400.00),
('b74d8fa9-11cd-4c64-a4d6-3915af480a40', 7, '1c346e1f-c899-43ec-baf3-7f6213ad4b3d', 2, 400.00),
('75ab93ef-e944-4a3a-81a9-85df65c9c09e', 8, '1c346e1f-c899-43ec-baf3-7f6213ad4b3d', 2, 400.00),
('7687e6bc-f272-4fdf-af31-b252b531cad8', 9, '1c346e1f-c899-43ec-baf3-7f6213ad4b3d', 3, 400.00),
('b735819b-f864-4f78-9e18-6ecb172dc89c', 10, '1c346e1f-c899-43ec-baf3-7f6213ad4b3d', 3, 400.00);




INSERT INTO Country VALUES --Id, Name
('2df0921b-0e52-4818-8c80-a1057325f95f', 'Croatia'),
('a529d587-eb5c-426d-93a4-e69cc918c0ac', 'Germany'),
('30fa3411-c633-4b5b-a507-bd52eebc7d42', 'Italy'),
('f5916ec0-3913-4d7e-8d93-f02e918d8e44', 'France'),
('cb54281e-39b1-4278-9d7c-a2458cdd7b75', 'Spain');





INSERT INTO PostalOffice VALUES --Id, Number, CityName, CountryId
('7a31f33e-f6d6-4e94-ba37-d4bfc5df50eb', 31000, 'Osijek', '2df0921b-0e52-4818-8c80-a1057325f95f'),
('16de2619-3235-4bf5-89ba-c531538550d3', 55000, 'Berlin', 'a529d587-eb5c-426d-93a4-e69cc918c0ac'),
('4f67baf3-11f6-4a86-bf8f-57d9e4add688', 70000, 'Rome', '30fa3411-c633-4b5b-a507-bd52eebc7d42'),
('8cadfb40-c4dd-44f4-8a80-401baf2fdde9', 25000, 'Paris', 'f5916ec0-3913-4d7e-8d93-f02e918d8e44'),
('c90ec5e6-8dc7-45a6-9bd1-0481ef6c9936', 15000, 'Barcelona', 'cb54281e-39b1-4278-9d7c-a2458cdd7b75');



INSERT INTO Address VALUES --Id, StreetName, PostalOfficeId
('c6a3892c-f20a-4ab2-bca9-d4be585176cf', 'Gacka 31', '7a31f33e-f6d6-4e94-ba37-d4bfc5df50eb'),
('b7a9f97f-7d07-4250-a242-fb75b9ccd760', 'Gunduliceva 50', '16de2619-3235-4bf5-89ba-c531538550d3'),
('e045ec06-8cbc-4296-ac48-25438b414348', 'Fruškogorska 5', 'c90ec5e6-8dc7-45a6-9bd1-0481ef6c9936');
/*
('01fa5e88-b7fe-4e1d-a2a5-d0fb836473ac', 'Kolodvorska 10', '8cadfb40-c4dd-44f4-8a80-401baf2fdde9'),
('3803438e-4b18-4845-8239-4d1c07723358', 'Kozjacka 15', 'c90ec5e6-8dc7-45a6-9bd1-0481ef6c9936');*/


INSERT INTO EndUser VALUES --Id, UserName, Password
('2e0b6abb-cb89-49a4-90ba-7150b632ea65', 'Mikey', 'Mikey_123');


INSERT INTO Receptionist VALUES --Id, FirstName, LastName, EndUserId
('7ae2e88d-3753-4269-85ce-22ef74bbd552', 'Michael', 'Scott', '2E0B6ABB-CB89-49A4-90BA-7150B632EA65');


INSERT INTO Guest VALUES --Id, Pid, FirstName, LastName, RoomId, AddressId, IsActive
('942b5a5d-47c0-4801-b5b1-c4161cc4c66e', '12345678910', 'John', 'Doe', '578157b1-41ea-4488-86e8-4d78d640bcfe', 'c6a3892c-f20a-4ab2-bca9-d4be585176cf', 0),
('1015be23-e953-4369-9f04-2ae61ce3ce80', '12345678911', 'Johnny', 'Dewey', '578157b1-41ea-4488-86e8-4d78d640bcfe', 'b7a9f97f-7d07-4250-a242-fb75b9ccd760', 1);

INSERT INTO Guest VALUES --Id, Pid, FirstName, LastName, RoomId, AddressId, IsActive
('bcaf1860-e51c-444a-b581-c0066f3361b0', '12121212121', 'Gordon', 'Freeman', 'a6c9a09f-1756-4283-9a46-33d25503c1d5', 'c6a3892c-f20a-4ab2-bca9-d4be585176cf', 1),
('1402cc7b-a633-4303-b107-7116b5f26d04', '89898989898', 'Alyx', 'Vance', 'a6c9a09f-1756-4283-9a46-33d25503c1d5', 'b7a9f97f-7d07-4250-a242-fb75b9ccd760', 1),
('7e1afc60-948f-4cc3-8315-f2bf259593f2', '9876543210', 'Elon', 'Musk', 'b735819b-f864-4f78-9e18-6ecb172dc89c', 'e045ec06-8cbc-4296-ac48-25438b414348', 1);



INSERT INTO Reservation VALUES --Id, ReceptionistId, CreationDate, IsActive, GuestId
('e5208538-1180-4f03-bf8b-3b88f2fc1c88','7AE2E88D-3753-4269-85CE-22EF74BBD552', GETDATE(), 0, '942b5a5d-47c0-4801-b5b1-c4161cc4c66e'),
('c91ebbfb-fc0d-41d6-a623-650f45cb0b15', '7AE2E88D-3753-4269-85CE-22EF74BBD552', GETDATE(), 1, '1015be23-e953-4369-9f04-2ae61ce3ce80');


INSERT INTO RoomReservation VALUES --RoomId, ReservationId, StartDate, EndDate
('578157b1-41ea-4488-86e8-4d78d640bcfe', 'e5208538-1180-4f03-bf8b-3b88f2fc1c88', '2022-04-25', '2022-04-30', 1),
('578157b1-41ea-4488-86e8-4d78d640bcfe', 'c91ebbfb-fc0d-41d6-a623-650f45cb0b15', '2022-05-03', '2022-05-07', 1);


INSERT INTO Payment VALUES ('22c81e8d-ad8b-430b-a684-36c4baa3f4e0', 'Cash'),
('212532f3-0f36-4419-8ba0-c0a9bee38a0e', 'Credit card');

INSERT INTO Receipt VALUES --Id, Price, PaymentMethod, ReservationId
('3050de76-631e-47c6-bfdf-79fb69853a76', 200.00, '22c81e8d-ad8b-430b-a684-36c4baa3f4e0', 'e5208538-1180-4f03-bf8b-3b88f2fc1c88'),
('f6100226-f877-4926-9e79-f6c12c4d9139', 200.00, '212532f3-0f36-4419-8ba0-c0a9bee38a0e', 'c91ebbfb-fc0d-41d6-a623-650f45cb0b15');






