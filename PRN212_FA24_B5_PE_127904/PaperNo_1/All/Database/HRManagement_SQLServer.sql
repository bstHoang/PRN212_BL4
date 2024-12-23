
-- Tạo cơ sở dữ liệu
CREATE DATABASE HRManagement;
GO
USE HRManagement;
GO

-- Bảng Position
CREATE TABLE Position (
    PositionID INT IDENTITY(1,1) PRIMARY KEY,
    PositionName NVARCHAR(100) NOT NULL
);
GO

-- Bảng Department
CREATE TABLE Department (
    DepartmentID INT IDENTITY(1,1) PRIMARY KEY,
    DepartmentName NVARCHAR(100) NOT NULL
);
GO

-- Bảng Employee
CREATE TABLE Employee (
    EmployeeID INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    PositionID INT NOT NULL,
    DepartmentID INT NOT NULL,
    DateOfBirth DATE NOT NULL,
    DateOfHire DATE NOT NULL,
    FOREIGN KEY (PositionID) REFERENCES Position(PositionID),
    FOREIGN KEY (DepartmentID) REFERENCES Department(DepartmentID)
);
GO

-- Bảng Project
CREATE TABLE Project (
    ProjectID INT IDENTITY(1,1) PRIMARY KEY,
    ProjectName NVARCHAR(100) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE,
    Budget DECIMAL(15, 2) NOT NULL
);
GO

-- Bảng EmployeeProject (Quan hệ n-n giữa Employee và Project)
CREATE TABLE EmployeeProject (
    EmployeeID INT NOT NULL,
    ProjectID INT NOT NULL,
    RoleInProject NVARCHAR(100),
    PRIMARY KEY (EmployeeID, ProjectID),
    FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID),
    FOREIGN KEY (ProjectID) REFERENCES Project(ProjectID)
);
GO

-- Bảng TrainingProgram
CREATE TABLE TrainingProgram (
    TrainingID INT IDENTITY(1,1) PRIMARY KEY,
    TrainingName NVARCHAR(100) NOT NULL,
    Duration INT NOT NULL,
    TrainerName NVARCHAR(100) NOT NULL
);
GO

-- Bảng EmployeeTraining (Quan hệ n-n giữa Employee và TrainingProgram)
CREATE TABLE EmployeeTraining (
    EmployeeID INT NOT NULL,
    TrainingID INT NOT NULL,
    CompletionStatus NVARCHAR(50),
    PRIMARY KEY (EmployeeID, TrainingID),
    FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID),
    FOREIGN KEY (TrainingID) REFERENCES TrainingProgram(TrainingID)
);
GO

-- Insert dữ liệu cho bảng Position
INSERT INTO Position (PositionName)
VALUES 
    (N'Employee'),
    (N'Manager'),
    (N'Supervisor');
GO

-- Insert dữ liệu cho bảng Department
INSERT INTO Department (DepartmentName)
VALUES 
    (N'Human Resources'),
    (N'Accounting'),
    (N'IT');
GO

-- Insert dữ liệu cho bảng Employee
INSERT INTO Employee (FullName, PositionID, DepartmentID, DateOfBirth, DateOfHire)
VALUES 
    (N'John Doe', 1, 1, '1985-03-10', '2010-01-15'),
    (N'Jane Smith', 2, 1, '1990-07-25', '2015-09-20'),
    (N'Michael Johnson', 1, 2, '1988-05-18', '2012-04-10'),
    (N'Sarah Williams', 3, 2, '1992-11-30', '2018-08-01'),
    (N'David Brown', 1, 3, '1995-01-22', '2020-05-14');
GO

-- Insert dữ liệu cho bảng Project
INSERT INTO Project (ProjectName, StartDate, EndDate, Budget)
VALUES 
    (N'Project A', '2023-01-01', '2023-06-30', 100000),
    (N'Project B', '2023-02-01', '2023-07-31', 200000),
    (N'Project C', '2023-03-01', '2023-08-31', 150000),
    (N'Project D', '2023-04-01', '2023-09-30', 120000),
    (N'Project E', '2023-05-01', '2023-10-31', 170000);
GO

-- Insert dữ liệu cho bảng EmployeeProject
INSERT INTO EmployeeProject (EmployeeID, ProjectID, RoleInProject)
VALUES 
    (1, 1, N'Software Development'),
    (1, 2, N'Testing'),
    (1, 3, N'Project Management'),
    (2, 2, N'Analysis'),
    (2, 3, N'Testing'),
    (2, 4, N'Financial Management'),
    (3, 1, N'Testing'),
    (3, 3, N'Software Development'),
    (3, 4, N'Analysis'),
    (4, 2, N'Project Management'),
    (4, 4, N'Software Development'),
    (4, 5, N'Testing'),
    (5, 1, N'Testing'),
    (5, 3, N'Analysis'),
    (5, 5, N'Software Development');
GO

-- Insert dữ liệu cho bảng TrainingProgram
INSERT INTO TrainingProgram (TrainingName, Duration, TrainerName)
VALUES 
    (N'Soft Skills Training', 20, N'Anna Taylor'),
    (N'Time Management', 15, N'James Scott'),
    (N'Advanced Technical Skills', 30, N'Emily Roberts'),
    (N'Professional Presentation', 10, N'Chris Johnson'),
    (N'Teamwork Skills', 25, N'Susan Lee');
GO

-- Insert dữ liệu cho bảng EmployeeTraining
INSERT INTO EmployeeTraining (EmployeeID, TrainingID, CompletionStatus)
VALUES 
    (1, 1, N'Completed'),
    (1, 2, N'Completed'),
    (1, 3, N'In Progress'),
    (2, 2, N'Completed'),
    (2, 3, N'Completed'),
    (2, 4, N'In Progress'),
    (3, 1, N'In Progress'),
    (3, 3, N'Completed'),
    (3, 5, N'Completed'),
    (4, 2, N'In Progress'),
    (4, 4, N'Completed'),
    (4, 5, N'Completed'),
    (5, 1, N'Completed'),
    (5, 3, N'Completed'),
    (5, 4, N'In Progress');
GO
