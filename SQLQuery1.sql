INSERT INTO Status (Title) VALUES ('В ожидании'), ('В работе'), ('Выполнено');

INSERT INTO Client (FirstName, LastName, PhoneNumber)
VALUES 
    ('Иван', 'Иванов', '89001112233'),
    ('Петр', 'Петров', '89004445566'),
    ('Сергей', 'Сергеев', '89007778899');


INSERT INTO Executor (FirstName, LastName, PhoneNumber)
VALUES 
    ('Алексей', 'Алексеев', '89001122334'),
    ('Дмитрий', 'Дмитриев', '89002233445'),
    ('Евгений', 'Евгеньев', '89003344556');


SELECT RequestId, RequstNumber, Equipment, Type, Description, CreatedDate
FROM Request
WHERE StatusId = 1 
ORDER BY CreatedDate DESC;
