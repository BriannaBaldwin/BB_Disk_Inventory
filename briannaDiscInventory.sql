/******************************************
 Date Created: 10/8/2021
 Date Last Modified: 10/08/2021
 Modified By: Brianna Baldiwn
 10/8/2021 - Initial implementation of disk db. Add users, grand permissions, add and create tables
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
(media_type_id		INT			NOT NULL PRIMARY KEY,
 description		CHAR(10)	NOT NULL);
 go

 CREATE TABLE status
(status_id			INT			NOT NULL PRIMARY KEY,
 description		CHAR(10)	NOT NULL);
 go

 CREATE TABLE genre
(genre_id			INT			NOT NULL PRIMARY KEY,
 description		CHAR(10)	NOT NULL);
 go

 CREATE TABLE artist_type
(artist_type_id		INT			NOT NULL PRIMARY KEY,
 description		CHAR(10)	NOT NULL);
 go

 CREATE TABLE media
 (media_id			INT			NOT NULL PRIMARY KEY,
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
 (borrower_id		INT			NOT NULL PRIMARY KEY,
  borrower_fname	CHAR(30)	NOT NULL,
  borrower_lname	CHAR(30)	NOT NULL,
  borrower_phone_num VARCHAR(10) NOT NULL,);
 go

 CREATE TABLE artist 
 (artist_id		INT			NOT NULL PRIMARY KEY,
 artist_name	CHAR(50)	NOT NULL,
 artist_type_id	INT			NOT NULL,
 FOREIGN KEY (artist_type_id) REFERENCES artist_type (artist_type_id));
 go

 CREATE TABLE rental
 (rental_id		INT			NOT NULL PRIMARY KEY,
 borrowed_date	DATE		NOT NULL,
 returned_date	DATE		NULL,
 due_date		DATE		NOT NULL,
 media_id		INT			NOT NULL,
 borrower_id	INT			NOT NULL,
 FOREIGN KEY (media_id) REFERENCES media (media_id),
 FOREIGN KEY (borrower_id) REFERENCES borrower (borrower_id));
 go

 CREATE TABLE disk_artist
 (disk_artist_id INT		NOT NULL PRIMARY KEY,
 artist_id		INT			NOT NULL,
 media_id		INT			NOT NULL,
 FOREIGN KEY (artist_id) REFERENCES artist (artist_id),
 FOREIGN KEY (media_id) REFERENCES media (media_id));
 go
--create indexes if not done in table definition
--after testing, create new github repo and push