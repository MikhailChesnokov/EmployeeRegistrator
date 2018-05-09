USE EmployeeRegistrator
GO

INSERT INTO Employees VALUES
  ('Иван', 'Иванович', '1', 'Иванов', 'true'),
  ('Петр', 'Петрович', '2', 'Петров', 'false'),
  ('Сидор', 'Сидорович', '3', 'Сидоров', 'true'),
  ('Владимир', 'Владимирович', '4', 'Владимиров', 'true')
GO

-- Иванов (Ходит редко и без опозданий)
INSERT INTO Registrations VALUES
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
  ('2018-05-04 18:04:00', 1, 2)
GO


-- Петров (Ходит часто, с опозданиями, присутствие необязательно)
INSERT INTO Registrations VALUES
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
  ('2018-05-04 17:59:00', 2, 2)
GO

-- Сидоров (Ходит часто, с опозданиями, присутствие обязательно)
INSERT INTO Registrations VALUES
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
  ('2018-05-04 17:50:00', 3, 2)
GO

-- Владимир Владимирович (Ходит редко, с без опозданий, иногда через окно)
INSERT INTO Registrations VALUES
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
  ('2018-05-04 17:50:00', 4, 2)
GO