USE ToDoApp;

CREATE TABLE Projects(
   UUID CHAR(36) NOT NULL PRIMARY KEY,
   Name CHAR(30) NOT NULL,
   Pattern CHAR(36),
   FOREIGN KEY (Pattern) REFERENCES Projects(UUID)
);

CREATE TABLE Tasks(
   Id INT IDENTITY(1,1) PRIMARY KEY,
   UUID CHAR(36) NOT NULL UNIQUE,
   Status CHAR(11) NOT NULL,
   Entry CHAR(27),
   Description TEXT NOT NULL,
   Start CHAR(27),
   "End" CHAR(27),
   Due CHAR(27),
   Priority TEXT,
   Depends INT,
   Project CHAR(36),
   FOREIGN KEY (Depends) REFERENCES Tasks(Id),
   FOREIGN KEY (Project) REFERENCES Projects(UUID),
);

CREATE TABLE Tags(
   Name CHAR(30) NOT NULL PRIMARY KEY,
);

CREATE TABLE Relations_Task_Tag(
   Id INT IDENTITY(1,1) PRIMARY KEY,
   Id_Task INT NOT NULL,
   Name_Tag CHAR(30) NOT NULL,
   FOREIGN KEY (Id_Task) REFERENCES Tasks(Id),
   FOREIGN KEY (Name_Tag) REFERENCES Tags(Name),
   UNIQUE(Id_Task, Name_Tag)
);
