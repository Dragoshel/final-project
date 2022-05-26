CREATE PROCEDURE [dbo].[CreateLoan]
(
    -- Add the parameters for the stored procedure here
    @memberCardID uniqueidentifier,
    @barcode uniqueidentifier
)
AS
BEGIN
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

	-- BEGIN TRY
		--Checking is member has a valid member card
		IF ((SELECT expiration FROM Member WHERE Member.cardID = @memberCardID) < GETDATE())
			BEGIN
			PRINT 'Member card is expired.'
			RETURN
			END

		--Checking if book is available
		IF ((SELECT isAvailable FROM Book_Copy WHERE barcode = @barcode) = 0)
			BEGIN
			PRINT 'Book is not available.'
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

		PRINT 'MemberCardID--------'
		PRINT @memberCardID
		SET @memberTypeID = (
			SELECT (memberTypeID)
			FROM Member
			WHERE cardID = @memberCardID
		)

		PRINT 'Member--------'
		PRINT @memberTypeID
		PRINT 'Student--------'
		PRINT @studentTypeID
		PRINT 'Current--------'
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
	-- END TRY

	-- BEGIN CATCH
	-- 	PRINT 'Creating loan failed.'
	-- 	IF @@tranCount > 0
    --       ROLLBACK TRANSACTION
	-- 	THROW;
	-- END CATCH
END