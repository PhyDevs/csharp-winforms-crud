
USE school
GO

/* Creating Add Student Procedure */
CREATE PROCEDURE ADD_P 
	@id int, @f_name varchar(30), @l_name varchar(30), @city  varchar(50), @dep  varchar(60)
AS
	INSERT INTO Student VALUES (@id, @f_name, @l_name, @city, @dep)
GO

/* Creating Update Student Procedure */
CREATE PROCEDURE UPDATE_P
	@id int, @f_name varchar(30), @l_name varchar(30), @city  varchar(50), @dep  varchar(60)
AS
	UPDATE Student SET firstName = @f_name, lastName = @l_name, city = @city, department = @dep
	WHERE id = @id
GO

/* Creating Delete Student Procedure */
CREATE PROCEDURE DELETE_P
	@id int
AS
	DELETE FROM Student WHERE id = @id;
GO


select * from Student
