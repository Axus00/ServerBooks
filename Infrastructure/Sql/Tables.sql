-- Active: 1722302981010@@bokgtp7j8jg7rk6dcziu-mysql.services.clever-cloud.com@3306@bokgtp7j8jg7rk6dcziu
CREATE TABLE Users (
    Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Names VARCHAR(255) NOT NULL,
    Status ENUM("Active", "Removed")
);

CREATE TABLE UserDatas (
    Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Email VARCHAR(45) NOT NULL,
    Phone VARCHAR(45) NOT NULL,
    UserId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE TABLE Role (
    Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Type ENUM('Customer', 'Admin') NOT NULL
);

CREATE TABLE UserRole (
    Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    UserId INT NOT NULL,
    RoleId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (RoleId) REFERENCES Role(Id)
);

CREATE TABLE Author (
    Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(45) NOT NULL,
    Status ENUM("Active", "Removed")
);

CREATE TABLE Books (
    Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    AuthorId INT NOT NULL,
    Quantity INT NOT NULL,
    Status ENUM("Active", "Removed"),
    FOREIGN KEY (AuthorId) REFERENCES Author(Id)
);

CREATE TABLE BooksBorrow (
    Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    UserId INT NOT NULL,
    BookId INT NOT NULL,
    BorrowStatus ENUM('Pending', 'Approved', 'Returned') NOT NULL,
    StartDate DATETIME NOT NULL,
    EndDate DATETIME,
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (BookId) REFERENCES Books(Id)
);

DESCRIBE Users;
