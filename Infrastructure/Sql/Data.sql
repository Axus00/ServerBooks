-- Insert sample data into Users
INSERT INTO Users (Name) VALUES 
('Alice Johnson'),
('Bob Smith'),
('Carol Davis'),
('David Brown'),
('Emma Wilson');

-- Insert sample data into UserDatas
INSERT INTO UserDatas (Email, Phone, UserId) VALUES 
('alice.johnson@example.com', '555-1234', 1),
('bob.smith@example.com', '555-5678', 2),
('carol.davis@example.com', '555-8765', 3),
('david.brown@example.com', '555-4321', 4),
('emma.wilson@example.com', '555-9876', 5);

-- Insert sample data into Role
INSERT INTO Role (Type) VALUES 
('Customer'),
('Admin');

-- Insert sample data into UserRole
INSERT INTO UserRole (UserId, RoleId) VALUES 
(1, 1),
(2, 1),
(3, 2),
(4, 1),
(5, 2);

-- Insert sample data into Author
INSERT INTO Author (Name) VALUES 
('Mark Twain'),
('Jane Austen'),
('George Orwell'),
('J.K. Rowling'),
('Ernest Hemingway');

-- Insert sample data into Books
INSERT INTO Books (Name, AuthorId, Quantity) VALUES 
('The Adventures of Tom Sawyer', 1, 10),
('Pride and Prejudice', 2, 8),
('1984', 3, 5),
('Harry Potter and the Sorcerer''s Stone', 4, 12),
('The Old Man and the Sea', 5, 7);

-- Insert sample data into BooksBorrow
INSERT INTO BooksBorrow (UserId, BookId, BorrowStatus, StartDate, EndDate) VALUES 
(1, 1, 'Approved', '2023-01-10 10:00:00', '2023-01-20 10:00:00'),
(2, 2, 'Pending', '2023-01-15 11:00:00', NULL),
(3, 3, 'Returned', '2023-01-12 12:00:00', '2023-01-22 12:00:00'),
(4, 4, 'Approved', '2023-01-18 13:00:00', '2023-01-28 13:00:00'),
(5, 5, 'Pending', '2023-01-20 14:00:00', NULL);