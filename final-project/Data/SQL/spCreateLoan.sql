/****** Object:  StoredProcedure [dbo].[CreateLoan]    Script Date: 2022. 05. 27. 16:18:35 ******/
-- SET ANSI_NULLS ON
-- SET QUOTED_IDENTIFIER ON

CREATE PROCEDURE [dbo].[CreateLoan]
(
    -- Add the parameters for the stored procedure here
    @memberCardID char(36) = NULL,  
    @barcode char(36) = NULL
)
AS
BEGIN
	BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET ANSI_NULLS ON
	SET QUOTED_IDENTIFIER ON

	SET NOCOUNT ON
	SET XACT_ABORT ON

	DECLARE @studentTypeID uniqueidentifier
	SET @studentTypeID = dbo.GetType('Student')

	DECLARE @teacherTypeID uniqueidentifier
	SET @teacherTypeID = dbo.GetType('Teacher')

		BEGIN TRANSACTION
			--Checking is member has a valid member card
			IF ((SELECT expiration FROM Member WHERE Member.cardID = @memberCardID) < GETDATE())
				BEGIN
				PRINT 'Member card is expired.'
				RETURN
				END

			--Checking if book copy is available
			IF ((SELECT isAvailable FROM Book_Copy WHERE barcode = @barcode) = 0)
				BEGIN
				PRINT 'Book copy is not available.'
				RETURN
				END

			--Checking is member has reached maximum amount of loans
			IF ((SELECT COUNT(id) FROM Loan WHERE memberCardID = @memberCardID AND returnDate IS NULL) >= 5)
				BEGIN
				PRINT 'Member has reached the maximum amount of loans.'
				RETURN
				END

			--Setting due date based on member type
			DECLARE @currentDate datetime = GETDATE ( );
			DECLARE @dueDate datetime;
			DECLARE @memberTypeID uniqueidentifier;

			PRINT @memberCardID
			SET @memberTypeID = (
				SELECT (memberTypeID)
				FROM Member
				WHERE cardID = @memberCardID
			)

			PRINT @memberTypeID
			PRINT @studentTypeID
			PRINT @currentDate
			PRINT @dueDate
		
			IF @memberTypeID = @studentTypeID
				SET @dueDate = DATEADD(day, 21, @currentDate);
			ELSE IF @memberTypeID = @teacherTypeID
				SET @dueDate = DATEADD(month, 3, @currentDate); 

			-- Inserting new loan to Loan table
			INSERT INTO Loan
			(startDate,dueDate,returnDate,memberCardID,bookCopyBarcode)
			OUTPUT INSERTED.*
			VALUES (@currentDate,@dueDate,NULL,@memberCardID,@barcode)

			--Updating availability of book
			UPDATE Book_Copy
			SET isAvailable = 0
			WHERE barcode=@barcode
		COMMIT TRANSACTION
	END TRY

	BEGIN CATCH
		IF (@@tranCount > 0)
			PRINT 'Creating loan failed.'
			ROLLBACK TRANSACTION;
		THROW;
	END CATCH
END