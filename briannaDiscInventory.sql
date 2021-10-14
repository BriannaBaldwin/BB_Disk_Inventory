/******************************************
 Date Created: 10/8/2021
 Date Last Modified: 10/14/2021
 Modified By: Brianna Baldiwn
 10/8/2021 - Initial implementation of disk db. Add users, grant permissions, add and create tables
 10/11/2021 - Changed artist_name data type to NCHAR
 10/14/2021 - Added inserts/data for media_type, status, genre, artist_type, and media
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
  borrower_phone_num VARCHAR(10) NOT NULL,);
 go

 CREATE TABLE artist 
 (artist_id		INT			NOT NULL IDENTITY(1, 1) PRIMARY KEY,
 artist_name	NCHAR(50)	NOT NULL,
 artist_type_id	INT			NOT NULL,
 FOREIGN KEY (artist_type_id) REFERENCES artist_type (artist_type_id));
 go

 CREATE TABLE rental
 (rental_id		INT			NOT NULL IDENTITY(1, 1) PRIMARY KEY,
 borrowed_date	DATE		NOT NULL,
 returned_date	DATE		NULL,
 due_date		DATE		NOT NULL,
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
	('Electropop');

-- artist_type inserts
INSERT INTO artist_type 
	(description)
VALUES 
	('Solo'),
	('Group');

-- media inserts
INSERT INTO media
	(media_name, release_date,
	 media_type_id, status_id, genre_id)
 VALUES 
	('Breathless', '10-20-1992', 2, 1, 7),
	('Hysteria', '08-03-1987', 1, 1, 3),
	('A Night at the Opera', '11-06-1975', 3, 4, 3),
	('Beyoncé', '06-24-2011', 1, 2, 4),
	('Andy Grammer', '06-14-2011', 1, 2, 1),
	('Better', '04-29-2016', 1, 3, 1),
	('Wild-Eyed Southern Boys', '01-28-1981', 3, 4, 3),
	('Taylor Swift', '10-24-2006', 1, 2, 8),
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
	('Change of Scenery II', '05-05-2021', 1, 4, 1);
	--Update 1 row using a where clause
	UPDATE media
	SET release_date = '06-10-2016'
	WHERE media_id = 19;