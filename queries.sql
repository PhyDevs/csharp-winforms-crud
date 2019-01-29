
/* Creating The Dtatabas */
CREATE DATABASE school
GO

use school
GO

/* Creating The Students Table */
CREATE TABLE Student (
	id int PRIMARY KEY NOT NULL,
	firstName varchar(30) NOT NULL,
	lastName varchar(30) NOT NULL,
	city varchar(50) NOT NULL,
	department varchar(60) NOT NULL
)
GO



/* Creating Dummy Data */
INSERT INTO Student VALUES 
	(1, 'John', 'Doe', 'New York', 'Physics'),
	(2, 'David', 'Doe', 'Paris', 'Geology'),
	(3, 'Adam', 'Doe', 'Rome', 'History'),
	(4, 'Roman', 'Doe', 'Napoli', 'Management'),
	(5, 'Jack', 'Doe', 'Madrid', 'Mathematics');

GO

/*Selecting From Students Table*/
select * from Student 
