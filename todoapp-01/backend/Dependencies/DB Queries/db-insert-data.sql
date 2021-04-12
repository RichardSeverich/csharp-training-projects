USE TodoDB3;

INSERT INTO Projects (UUID, Name)
VALUES
('00000000-0000-0000-0000-000000000000', 'My projects');

INSERT INTO Projects (UUID, Name, Parent)
VALUES
('494c7fbc-0fde-4230-a15c-d5bb903f8291', 'Maths', '00000000-0000-0000-0000-000000000000'),
('494c7fbc-0fde-4230-a15c-d5bb903f8293', 'Natural Sciences', '00000000-0000-0000-0000-000000000000'),
('494c7fbc-0fde-4230-a15c-d5bb903f8294', 'Sum', '494c7fbc-0fde-4230-a15c-d5bb903f8291'),
('494c7fbc-0fde-4230-a15c-d5bb903f8295', 'Subtraction', '494c7fbc-0fde-4230-a15c-d5bb903f8291'),
('494c7fbc-0fde-4230-a15c-d5bb903f8296', 'Terrestrial Biology', '494c7fbc-0fde-4230-a15c-d5bb903f8293'),
('494c7fbc-0fde-4230-a15c-d5bb903f8297', 'Marine Biology', '494c7fbc-0fde-4230-a15c-d5bb903f8293');

INSERT INTO Tasks (UUID, Status, Entry, Description, Start, "End", Priority, ProjectUuid)
VALUES
('494c7fbc-0fde-4230-a15c-d5bb903f8292', 'Pending', '2020-12-12T00:00:00Z', 'Research about imaginary numbers', null, null, 'Low', '494c7fbc-0fde-4230-a15c-d5bb903f8294'),
('6486c57d-6251-41b9-b87d-0de13ae54781', 'Completed', '2020-12-15T00:00:00Z', 'Do my sum exercises', '2020-12-15T00:00:00Z', '2020-12-16T00:00:00Z', 'Medium', '494c7fbc-0fde-4230-a15c-d5bb903f8294'),
('e13d8ca5-0a14-4dba-8453-77ad216f4063', 'Deleted', '2021-01-16T00:00:00Z', 'Teach my sister', '2021-01-16T00:00:00Z', '2021-01-17T00:00:00Z', 'High', '494c7fbc-0fde-4230-a15c-d5bb903f8295'),
('27b875d8-a109-4dbb-bd1f-3e8ca68955f0', 'In progress', '2021-01-27T00:00:00Z', 'Do my subtraction exercises', '2021-01-27T00:00:00Z', null, 'Medium', '494c7fbc-0fde-4230-a15c-d5bb903f8295'),
('7fe382f8-e614-4ae7-bc34-904ed03c227d', 'Pending', '2021-01-28T00:00:00Z', 'See rocks', null, null, 'Low', '494c7fbc-0fde-4230-a15c-d5bb903f8296'),
('71deb984-50e9-4f9e-b931-617e98665d26', 'In progress', '2021-01-28T00:00:00Z', 'Vaccinate a dog', '2021-01-28T00:00:00Z', null, 'High', '494c7fbc-0fde-4230-a15c-d5bb903f8296'),
('a9e86fea-94ce-4b21-a5e2-9f08ca9e5572', 'Completed', '2021-01-28T00:00:00Z', 'Vaccinate a cat', '2021-01-28T00:00:00Z', '2021-01-29T00:00:00Z', 'High', '494c7fbc-0fde-4230-a15c-d5bb903f8296'),
('e0840528-6d4c-4af7-a2dd-e18dd30e9fe1', 'Pending', '2021-02-07T00:00:00Z', 'Research about shark', null, null, 'Medium', '494c7fbc-0fde-4230-a15c-d5bb903f8297'),
('82e10ce4-ad78-478b-9188-a886cebd62db', 'Completed', '2021-02-07T00:00:00Z', 'Research about octopus', '2021-02-07T00:00:00Z', '2021-02-08T00:00:00Z', 'Medium', '494c7fbc-0fde-4230-a15c-d5bb903f8297'),
('57be0929-617e-442b-abb2-375858030cf2', 'Deleted', '2021-02-08T00:00:00Z', 'Research about fish', null, '2021-02-08T23:00:00Z', 'Medium', '494c7fbc-0fde-4230-a15c-d5bb903f8297');

INSERT INTO Tags (Name)
VALUES
('Research'),
('Today'),
('Teach'),
('Exercise');

INSERT INTO TaskTags(Id_Task, Id_Tag)
VALUES
(1, 1),
(2, 4),
(3, 2),
(3, 3),
(4, 4),
(6, 2),
(7, 2),
(8, 1),
(9, 1),
(10, 1);
