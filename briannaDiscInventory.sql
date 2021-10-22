/******************************************
 Date Created: 10/8/2021
 Date Last Modified: 10/21/2021
 Modified By: Brianna Baldiwn
 10/8/2021 - Initial implementation of disk db. Add users, grant permissions, add and create tables
 10/11/2021 - Changed artist_name data type to NCHAR
 10/14/2021 - Added inserts/data for media_type, status, genre, artist_type, and media | Made primary keys identifying
 10/15/2021 - Added inserts/data for borrower, artist, rental and disk_artist | Created a query for disks on loan
 10/19/2021 - made changes to SQL for disks on loan
 10/21/2021 - Add SQL for reports | created view for solo artists | add SQL to seperate artist first and last name
*
********************************************/

--drop and create database
use master;
DROP DATABASE IF EXISTS bri_disk_database;
go
CREATE DATABASE bri_disk_database;
go

--drop and create user and login
IF SUSER_ID('diskUserBB') IS NOT NULL
DROP LOGIN diskUserBB;
go
CREATE LOGIN diskUserBB WITH PASSWORD = 'MSPress#1',
    DEFAULT_DATABASE = bri_disk_database;
go

use bri_disk_database;
go
CREATE USER diskUserBB;
go

--grant read-all to new user: alter db_datareader and add user
ALTER ROLE db_datareader ADD MEMBER diskUserBB;
go

--create tables: lookup, independent, dependent
CREATE TABLE media_type
(media_type_id		INT			NOT NULL IDENTITY(1, 1) PRIMARY KEY,
 description		CHAR(10)	NOT NULL);
 go

 CREATE TABLE status
(status_id			INT			NOT NULL IDENTITY(1, 1) PRIMARY KEY,
 description		CHAR(10)	NOT NULL);
 go

 CREATE TABLE genre
(genre_id			INT			NOT NULL IDENTITY(1, 1) PRIMARY KEY,
 description		CHAR(10)	NOT NULL);
 go

 CREATE TABLE artist_type
(artist_type_id		INT			NOT NULL IDENTITY(1, 1) PRIMARY KEY,
 description		CHAR(10)	NOT NULL);
 go
 -- Reference project 3 for inserts
 CREATE TABLE media
 (media_id			INT			NOT NULL IDENTITY(1, 1) PRIMARY KEY,
  media_name		CHAR(50)	NOT NULL,
  release_date		DATE		NOT NULL,
  media_type_id		INT			NOT NULL,
  genre_id			INT			NOT NULL,
  status_id			INT			NOT NULL,
  FOREIGN KEY (media_type_id) REFERENCES media_type (media_type_id),
  FOREIGN KEY (status_id) REFERENCES status (status_id),
  FOREIGN KEY (genre_id) REFERENCES genre (genre_id)
  );
 go

 CREATE TABLE borrower 
 (borrower_id		INT			NOT NULL IDENTITY(1, 1) PRIMARY KEY,
  borrower_fname	CHAR(30)	NOT NULL,
  borrower_lname	CHAR(30)	NOT NULL,
  borrower_phone_num VARCHAR(15) NOT NULL,);
 go

 CREATE TABLE artist 
 (artist_id		INT			NOT NULL IDENTITY(1, 1) PRIMARY KEY,
 artist_name	NVARCHAR(50)	NOT NULL,
 artist_type_id	INT			NOT NULL,
 FOREIGN KEY (artist_type_id) REFERENCES artist_type (artist_type_id));
 go

 CREATE TABLE rental
 (rental_id		INT			NOT NULL IDENTITY(1, 1) PRIMARY KEY,
 borrowed_date	DATE		NOT NULL,
 returned_date	DATE		NULL,
 media_id		INT			NOT NULL,
 borrower_id	INT			NOT NULL,
 FOREIGN KEY (media_id) REFERENCES media (media_id),
 FOREIGN KEY (borrower_id) REFERENCES borrower (borrower_id));
 go

 CREATE TABLE disk_artist
 (disk_artist_id INT		NOT NULL IDENTITY(1, 1) PRIMARY KEY,
 artist_id		INT			NOT NULL,
 media_id		INT			NOT NULL,
 FOREIGN KEY (artist_id) REFERENCES artist (artist_id),
 FOREIGN KEY (media_id) REFERENCES media (media_id));
 go
-- create indexes if not done in table definition
	--Done
-- after testing, create new github repo and push
	--Done

-- populate tables

-- media_type inserts
INSERT INTO media_type 
	(description)
VALUES 
	('CD'),
	('Vinyl'),
	('8Track'),
	('Cassette');

-- status inserts
INSERT INTO status 
	(description)
VALUES 
	('Available'),
	('Missing'),
	('Damaged'),
	('On loan');

-- genre inserts
INSERT INTO genre 
	(description)
VALUES 
	('Pop'),
	('Punk'),
	('Rock'),
	('R&B'),
	('Classical'),
	('Hip Hop'),
	('Jazz'),
	('Country'),
	('Alt'),
	('Indie'),
	('Reggae'),
	('Electropop'),
	('Soul');

-- artist_type inserts
INSERT INTO artist_type 
	(description)
VALUES 
	('Solo'),
	('Group');

-- media inserts ()
INSERT INTO media
	(media_name, release_date,
	 media_type_id, status_id, genre_id)
VALUES 
	('Breathless', '10-20-1992', 2, 1, 7),
	('Hysteria', '08-03-1987', 1, 1, 3),
	('A Night at the Opera', '11-06-1975', 3, 4, 3),
	('Beyoncé', '06-24-2011', 1, 2, 4),
	('When Christmas Comes', '11-21-2011', 1, 2, 13),
	('Better', '04-29-2016', 1, 3, 1),
	('Wild-Eyed Southern Boys', '01-28-1981', 3, 4, 3),
	('Taylor Swift', '10-24-2006', 1, 4, 8),
	('Evermore', '12-11-2020', 2, 4, 9),
	('I Dont Dance', '09-09-1987', 1, 3, 8),
	('Oracular Spectacular', '10-02-2007', 2, 4, 10),
	('Little Dark Age', '02-09-2018', 2, 2, 12),
	('FOTO', '05-15-2019', 1, 3, 6),
	('Dogs Eating Dogs', '12-18-2012', 1, 1, 2),
	('Enema of the State', '06-01-1999', 1, 1, 2),
	('Exodus', '06-03-1977', 4, 3, 11),
	('Bach: The Goldberg Variations', '01-01-1956', 4, 1, 5),
	('Greatest Hits Volume One: The Singles', '11-13-2007', 2, 1, 9),
	('The Human Condition', '06-10-2006', 1, 4, 1),
	('Change of Scenery II', '05-05-2021', 1, 4, 1),
	('Abbey Road', '09-26-1969', 3, 1, 3),
	('Bigger, Better, Faster, More!', '10-13-1992', 2, 3, 3),
	('Slippery When Wet', '08-18-1986', 1, 2, 3);
--Update 1 row using a where clause
UPDATE media
SET release_date = '06-10-2016'
WHERE media_id = 19;

-- borrower inserts
INSERT INTO borrower
	(borrower_fname, borrower_lname, borrower_phone_num)
VALUES
	('Mickey', 'Mouse', '907-867-5309'),
	('Frodo', 'Baggins', '111-222-3333'),
	('Samwise', 'Gamgee', '618-300-5167'),
	('Charlie', 'Brown', '869-391-5836'),
	('Lucy', 'van Pelt', '868-351-5617'),
	('Linus', 'van Pelt', '664-447-6584'),
	('Peppermint', 'Patty', '989-369-3462'),
	('Sally', 'Brown', '727-524-1654'),
	('Minnie', 'Mouse', '978-948-1640'),
	('Han', 'Solo', '212-576-1511'),
	('Peter', 'Pan', '240-337-5181'),
	('Homer', 'Simpson', '240-337-5181'),
	('Peter', 'Parker', '512-846-8174'),
	('Jon', 'Snow', '831-576-2423'),
	('Mary', 'Poppins', '636-788-5535'),
	('Harry', 'Potter', '941-397-1395'),
	('Willy', 'Wonka', '507-236-7129'),
	('Scooby', 'Doo', '858-573-7940'),
	('Michael', 'Scott', '541-254-3071'),
	('Porky', 'Pig', '256-666-1798'),
	('Marvin', 'Martian', '423-826-6099');
--Delete one row with a WHERE clause
DELETE FROM borrower
WHERE borrower_id = 20;

-- artist inserts
INSERT INTO artist
	(artist_name, artist_type_id)
VALUES
	('Kenny G', 1),
	('Def Leppard', 2),
	('Queen', 2),
	('Beyoncé', 1),
	('Mariah Carey', 1),
	('Haley Reinhart', 1),
	('38 Special', 2),
	('Taylor Swift', 1),
	('Lee Brice', 1),
	('MGMT', 2),
	('Kota the Friend', 1),
	('Blink-182', 2),
	('Bob Marley and the Wailers', 2),
	('Glenn Gould', 1),
	('Goo Goo Dolls', 2),
	('Jon Bellion', 1),
	('Quinn XCII', 1),
	('The Beatles', 2),
	('4 Non Blondes', 2),
	('Bon Jovi', 2),
	('John Legend', 1);

-- rental inserts
INSERT INTO rental
	(borrowed_date, returned_date, media_id, borrower_id)
VALUES
	('11-02-2017', '01-09-2018', 1, 21),
	('08-19-2020', NULL, 3, 1),
	('10-30-2020', NULL, 7, 5),
	('05-15-2020', NULL, 9, 9),
	('05-15-2020', NULL, 8, 9),
	('06-15-2021', NULL, 11, 13),
	('06-05-2018', NULL, 19, 2),
	('09-06-2017', NULL, 20, 16),
	('08-02-2018', '01-09-2019', 12, 6),
	('11-07-2018', '05-12-2019', 10, 5),
	('05-01-2018', '10-07-2018', 8, 11),
	('06-03-2020', '09-05-2021', 14, 3),
	('09-11-2018', '11-03-2019', 15, 12),
	('05-11-2018', '02-27-2019', 20, 7),
	('01-04-2020', '11-01-2021', 22, 15),
	('03-05-2018', '08-01-2019', 23, 14),
	('05-25-2019', '09-21-2020', 12, 8),
	('07-07-2017', '07-08-2018', 11, 13),
	('02-21-2020', '01-18-2021', 8, 12),
	('10-27-2018', '11-25-2018', 4, 18),
	('08-21-2018', '10-16-2019', 11, 19);

-- disk_artist inserts
INSERT INTO disk_artist
	(media_id, artist_id)
VALUES
	(1, 1),
	(2, 2),
	(3, 3),
	(4, 4),
	(5, 5),
	(5, 21),
	(6, 6),
	(7, 7),
	(8, 8),
	(9, 8),
	(10, 9),
	(11, 10),
	(12, 10),
	(13, 11),
	(14, 12),
	(15, 12),
	(16, 13),
	(17, 14),
	(18, 15),
	(19, 16),
	(20, 17),
	(21, 18),
	(22, 19),
	(23, 20);

--query list of disks on loan
SELECT borrower_id, media_id, borrowed_date, returned_date
FROM rental
WHERE returned_date IS NULL
ORDER BY media_id;

/*
select * from media;
select * from disk_artist;
select * from artist;
*/

/*
select * from media;
select * from rental;
select * from borrower;
*/

-----Project 4-----
Use bri_disk_database;
go
-- 3) Show the disks in your database and any associated Individual artists only
SELECT media_name AS 'Disk Name', CONVERT(varchar, release_date, 101) AS 'Release Date', 
	IIF(CHARINDEX(' ', artist_name) > 0, LEFT(artist_name, CHARINDEX(' ', artist_name)), artist_name) as 'Artist First Name',
	IIF(CHARINDEX(' ', artist_name) > 0, RIGHT(artist_name, LEN(artist_name) - CHARINDEX(' ', artist_name)), '') as 'Artist Last Name'
FROM media 
	JOIN disk_artist ON disk_artist.media_id = media.media_id
	JOIN artist ON artist.artist_id = disk_artist.artist_id
WHERE artist_type_id = 1
ORDER BY 'Artist Last Name';
go

-- 4) Create a view called View_Individual_Artist that shows the artists’ names and not group names. Include the artist id in the view definition but do not display the id in your output
DROP VIEW IF EXISTS View_Individual_Artist;
go
CREATE VIEW View_Individual_Artist
AS
	SELECT artist_name, artist_id
	FROM artist
	WHERE artist_type_id = 1;
go
SELECT IIF(CHARINDEX(' ', artist_name) > 0, LEFT(artist_name, CHARINDEX(' ', artist_name)), artist_name) as 'First Name',
	IIF(CHARINDEX(' ', artist_name) > 0, RIGHT(artist_name, LEN(artist_name) - CHARINDEX(' ', artist_name)), '') as 'Last Name'
FROM View_Individual_Artist
ORDER BY 'Last Name';
go

-- 5) Show the disks in your database and any associated Group artists only
SELECT media_name AS 'Disk Name', CONVERT(varchar, release_date, 101) AS 'Release Date', artist_name AS 'Group Name'
	FROM media 
		JOIN disk_artist ON disk_artist.media_id = media.media_id
		JOIN artist ON artist.artist_id = disk_artist.artist_id
	WHERE artist_type_id = 2
ORDER BY artist_name;
go

-- 6) Re-write the previous query using the View_Individual_Artist view. Do not redefine the view. Consider using ‘NOT EXISTS’ or ‘NOT IN’ as the only restriction in the WHERE clause or a join. The output matches the output from the previous query.
SELECT media_name AS 'Disk Name', CONVERT(varchar, release_date, 101) AS 'Release Date', artist_name AS 'Group Name'
FROM media 
	JOIN disk_artist ON disk_artist.media_id = media.media_id
	JOIN artist ON artist.artist_id = disk_artist.artist_id
WHERE artist.artist_id NOT IN 
	(SELECT artist_id 
	FROM View_Individual_Artist)
ORDER BY artist_name;
go

--7) Show the borrowed disks and who borrowed them.
SELECT TOP 100 PERCENT borrower_fname AS 'First', borrower_lname AS 'Last', media_name AS 'Disk Name', borrowed_date AS 'Borrowed Date', returned_date AS 'Returned Date'
FROM media 
	JOIN rental ON rental.media_id = media.media_id
	JOIN borrower ON borrower.borrower_id = rental.borrower_id
ORDER BY borrower_lname;
go

-- 8) Show the number of times a disk has been borrowed
SELECT media.media_id AS 'DiskId', media_name AS 'Disk Name', COUNT(rental.media_id) AS TimesBorrowed
FROM media
	JOIN rental ON rental.media_id = media.media_id
GROUP BY media.media_id, media_name
ORDER BY media.media_id;
go

-- 9) Show the disks outstanding or on-loan and who has each disk.
SELECT media_name AS 'Disk Name', borrowed_date AS 'Borrowed', returned_date AS 'Returned', borrower_lname AS 'Last Name'
FROM rental 
	JOIN media ON rental.media_id = media.media_id
	JOIN borrower ON borrower.borrower_id = rental.borrower_id
WHERE returned_date IS NULL
ORDER BY borrower_lname;
go