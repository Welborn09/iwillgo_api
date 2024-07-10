INSERT INTO [dbo].[Opportunity] (
    [PK_Opportunity], [EventName], [Address], [City], [State], [Zip], 
    [EventDate], [EventTimeTo], [EventTimeFrom], [Description], 
    [Host_UserId], [CreatedDate], [CreatedBy], [ModifiedDate], 
    [ModifiedBy], [Active]
) VALUES 
(
    NEWID(), 'Event 1', '123 Main St', 'City1', 'State1', '12345', 
    '2024-07-10', '18:00', '14:00', 'Description for Event 1', 
    NEWID(), DEFAULT, NEWID(), NULL, NULL, 1
), 
(
    NEWID(), 'Event 2', '456 Oak St', 'City2', 'State2', '23456', 
    '2024-07-11', '19:00', '15:00', 'Description for Event 2', 
    NEWID(), DEFAULT, NEWID(), NULL, NULL, 1
), 
(
    NEWID(), 'Event 3', '789 Pine St', 'City3', 'State3', '34567', 
    '2024-07-12', '20:00', '16:00', 'Description for Event 3', 
    NEWID(), DEFAULT, NEWID(), NULL, NULL, 1
), 
(
    NEWID(), 'Event 4', '101 Maple St', 'City4', 'State4', '45678', 
    '2024-07-13', '21:00', '17:00', 'Description for Event 4', 
    NEWID(), DEFAULT, NEWID(), NULL, NULL, 1
), 
(
    NEWID(), 'Event 5', '202 Cedar St', 'City5', 'State5', '56789', 
    '2024-07-14', '22:00', '18:00', 'Description for Event 5', 
    NEWID(), DEFAULT, NEWID(), NULL, NULL, 1
), 
(
    NEWID(), 'Event 6', '303 Birch St', 'City6', 'State6', '67890', 
    '2024-07-15', '23:00', '19:00', 'Description for Event 6', 
    NEWID(), DEFAULT, NEWID(), NULL, NULL, 1
), 
(
    NEWID(), 'Event 7', '404 Walnut St', 'City7', 'State7', '78901', 
    '2024-07-16', '00:00', '20:00', 'Description for Event 7', 
    NEWID(), DEFAULT, NEWID(), NULL, NULL, 1
), 
(
    NEWID(), 'Event 8', '505 Ash St', 'City8', 'State8', '89012', 
    '2024-07-17', '01:00', '21:00', 'Description for Event 8', 
    NEWID(), DEFAULT, NEWID(), NULL, NULL, 1
), 
(
    NEWID(), 'Event 9', '606 Elm St', 'City9', 'State9', '90123', 
    '2024-07-18', '02:00', '22:00', 'Description for Event 9', 
    NEWID(), DEFAULT, NEWID(), NULL, NULL, 1
), 
(
    NEWID(), 'Event 10', '707 Fir St', 'City10', 'State10', '01234', 
    '2024-07-19', '03:00', '23:00', 'Description for Event 10', 
    NEWID(), DEFAULT, NEWID(), NULL, NULL, 1
);