USE EmployeeRegistrator;

-- admin 12345678
INSERT INTO Users
  (Login, Password_Hash, Password_Salt, Role, Email, NeedNotify)
VALUES
  ('admin', 0x55F3E83D9D8F50A2481487B562511F261556894B6AD6B563D6F32547C017FD22, 0x4CC7C0212EAC253FC5CFA6A40D9E77C7E26F25D452642A34FD18C389050509EB7E1611CB7C8967BA, 1, '89194485838@mail.ru', TRUE);


INSERT INTO Departments
  (Name, DeletedAtUtc)
VALUES
  ('Отдел маркетинга', NULL),
  ('Отдел разработки', NULL);

INSERT INTO Employees
  (FirstName, Surname, Patronymic, WorkplacePresenceRequired, PersonnelNumber, DepartmentId, DeletedAtUtc)
VALUES
  ('Иван', 'Иванов', 'Иванович', TRUE, 1, 1, NULL),
  ('Петр', 'Петров', 'Петрович', FALSE, 2, 1, NULL),
  ('Сидор', 'Сидоров', 'Сидорович', TRUE, 3, 2, NULL),
  ('Владимир', 'Владимиров', 'Владимирович', TRUE, 4, 2, NULL);

-- Иванов (Ходит редко и без опозданий)
INSERT INTO Registrations
  (DateTime, EmployeeId, EventType)
VALUES
  -- Пн
  ('2018-04-30 09:55:00', 1, 1),
  ('2018-04-30 17:56:00', 1, 2),

  -- Вт
  ('2018-05-01 09:59:00', 1, 1),
  ('2018-05-01 18:01:00', 1, 2),

  -- Ср
  ('2018-05-02 10:05:00', 1, 1),
  ('2018-05-02 18:24:00', 1, 2),

  -- Чт
  ('2018-05-03 09:57:00', 1, 1),
  ('2018-05-03 17:59:00', 1, 2),

  -- Пт
  ('2018-05-04 09:54:00', 1, 1),
  ('2018-05-04 18:04:00', 1, 2);


-- Петров (Ходит часто, с опозданиями, присутствие необязательно)
INSERT INTO Registrations
  (DateTime, EmployeeId, EventType)
VALUES
  -- Пн
  ('2018-04-30 11:09:00', 2, 1),
  ('2018-04-30 13:03:00', 2, 2),
  ('2018-04-30 13:17:00', 2, 1),
  ('2018-04-30 16:35:00', 2, 2),
  ('2018-04-30 16:59:00', 2, 1),
  ('2018-04-30 19:01:00', 2, 2),

  -- Вт
  ('2018-05-01 11:31:00', 2, 1),
  ('2018-05-01 15:53:00', 2, 2),
  ('2018-05-01 16:59:00', 2, 1),
  ('2018-05-01 19:03:00', 2, 2),

  -- Ср
  ('2018-05-02 09:55:00', 2, 1),
  ('2018-05-02 11:03:00', 2, 2),
  ('2018-05-02 11:21:00', 2, 1),
  ('2018-05-02 16:40:00', 2, 2),
  ('2018-05-02 16:59:00', 2, 1),
  ('2018-05-02 18:01:00', 2, 2),

  -- Чт
  ('2018-05-03 09:57:00', 2, 1),
  ('2018-05-03 14:00:00', 2, 2),
  ('2018-05-03 14:57:00', 2, 1),
  ('2018-05-03 18:01:00', 2, 2),

  -- Пт
  ('2018-05-04 10:07:00', 2, 1),
  ('2018-05-04 13:04:00', 2, 2),
  ('2018-05-04 14:35:00', 2, 1),
  ('2018-05-04 17:59:00', 2, 2);

-- Сидоров (Ходит часто, с опозданиями, присутствие обязательно)
INSERT INTO Registrations
  (DateTime, EmployeeId, EventType)
VALUES
  -- Пн
  ('2018-04-30 11:07:00', 3, 1),
  ('2018-04-30 13:01:00', 3, 2),
  ('2018-04-30 13:19:00', 3, 1),
  ('2018-04-30 16:31:00', 3, 2),
  ('2018-04-30 16:50:00', 3, 1),
  ('2018-04-30 19:04:00', 3, 2),

  -- Вт
  ('2018-05-01 11:30:00', 3, 1),
  ('2018-05-01 15:57:00', 3, 2),
  ('2018-05-01 16:59:00', 3, 1),
  ('2018-05-01 19:04:00', 3, 2),

  -- Ср
  ('2018-05-02 09:54:00', 3, 1),
  ('2018-05-02 11:01:00', 3, 2),
  ('2018-05-02 11:28:00', 3, 1),
  ('2018-05-02 16:44:00', 3, 2),
  ('2018-05-02 16:55:00', 3, 1),
  ('2018-05-02 18:02:00', 3, 2),

  -- Чт
  ('2018-05-03 09:55:00', 3, 1),
  ('2018-05-03 14:05:00', 3, 2),
  ('2018-05-03 14:51:00', 3, 1),
  ('2018-05-03 18:05:00', 3, 2),

  -- Пт
  ('2018-05-04 10:02:00', 3, 1),
  ('2018-05-04 13:03:00', 3, 2),
  ('2018-05-04 14:37:00', 3, 1),
  ('2018-05-04 17:50:00', 3, 2);

-- Владимир Владимирович (Ходит редко, с без опозданий, иногда через окно)
INSERT INTO Registrations
  (DateTime, EmployeeId, EventType)
VALUES
  -- Пн
  ('2018-04-30 10:00:00', 4, 1),
  ('2018-04-30 13:01:00', 4, 1),
  ('2018-04-30 19:04:00', 4, 2),

  -- Вт
  ('2018-05-01 09:57:00', 4, 1),
  ('2018-05-01 19:57:00', 4, 2),

  -- Ср
  ('2018-05-02 09:54:00', 4, 1),
  ('2018-05-02 11:01:00', 4, 2),
  ('2018-05-02 11:37:00', 4, 2),
  ('2018-05-02 11:59:00', 4, 2),
  ('2018-05-02 12:28:00', 4, 1),
  ('2018-05-02 16:44:00', 4, 2),
  ('2018-05-02 16:55:00', 4, 1),
  ('2018-05-02 18:02:00', 4, 2),

  -- Чт
  ('2018-05-03 09:55:00', 4, 1),
  ('2018-05-03 14:05:00', 4, 2),
  ('2018-05-03 14:51:00', 4, 1),
  ('2018-05-03 18:05:00', 4, 2),

  -- Пт
  ('2018-05-04 10:02:00', 4, 1),
  ('2018-05-04 13:03:00', 4, 2),
  ('2018-05-04 14:37:00', 4, 1),
  ('2018-05-04 17:50:00', 4, 2);
  
INSERT INTO buildings
  (Address, DeletedAtUtc)
VALUES
  ('Бульвар Гагарина, 37а', NULL),
  ('Студенческая, 38', NULL);

INSERT INTO entrances
  (Name, DeletedAtUtc, BuildingId)
VALUES
  ('Главный вход', NULL, 1),
  ('Главный вход', NULL, 2),
  ('Вход с торца', NULL, 2);