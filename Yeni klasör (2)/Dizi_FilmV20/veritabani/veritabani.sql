USE Dizi_FilmV20;
GO

-- Mevcut tabloları sil
DROP TABLE IF EXISTS WatchRecords;
DROP TABLE IF EXISTS Contents;
DROP TABLE IF EXISTS Users;
GO

-- Users tablosunu oluştur
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Password NVARCHAR(100) NOT NULL,
    IsAdmin BIT NOT NULL
);
GO

-- Contents tablosunu oluştur
CREATE TABLE Contents (
    ContentID INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(200) NOT NULL,
    Genre NVARCHAR(100),
    Duration NVARCHAR(50),
    ReleaseYear INT
);
GO

-- WatchRecords tablosunu oluştur
CREATE TABLE WatchRecords (
    RecordID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT,
    ContentID INT,
    WatchStatus NVARCHAR(20),
    Rating INT,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (ContentID) REFERENCES Contents(ContentID)
);
GO
-- Admin ve kullanıcı ekle
INSERT INTO Users (Email, Password, IsAdmin)
VALUES
('admin@mail.com', '1234', 1),
('rukenkaracol@gmail.com', '1234', 0);
GO

-- İçerik verileri ekle
INSERT INTO Contents (Title, Genre, Duration, ReleaseYear)
VALUES
('Breaking Bad', 'Drama', '49 dk', 2008),
('Stranger Things', 'Bilim Kurgu', '51 dk', 2016),
('The Office', 'Komedi', '22 dk', 2005),
('The Queen''s Gambit', 'Drama', '60 dk', 2020),
('Friends', 'Komedi', '25 dk', 1994),
('Dark', 'Bilim Kurgu', '60 dk', 2017),
('Sherlock', 'Gizem', '88 dk', 2010),
('Money Heist', 'Suç', '45 dk', 2017),
('Wednesday', 'Gizem+Komedi', '47 dk', 2022),
('The Witcher', 'Fantastik', '60 dk', 2019);
GO
