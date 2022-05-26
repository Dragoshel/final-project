
CREATE PROCEDURE [dbo].[CreateLoan]
(
    -- Add the parameters for the stored procedure here
    @MemberCardID char(36) = NULL,  
    @Barcode char(36) = NULL
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON
	SET XACT_ABORT ON

	BEGIN TRY
		BEGIN TRANSACTION
			--Checking is member has a valid member card
			IF ((SELECT expiration FROM Member_Card WHERE Member_Card.id = @MemberCardID) < GETDATE())
				BEGIN
				PRINT 'Member card is expired.'
				RETURN
				END

			--Checking if book is available
			IF ((SELECT isAvailable FROM Book_Copy WHERE barcode = @Barcode) != 0)
				BEGIN
				PRINT 'Book is not available.'
				RETURN
				END

			--Checking is member has reached maximum amount of loans
			IF ((SELECT COUNT(id) FROM Loan WHERE memberCardID = @MemberCardID AND returnDate IS NULL) >= 5)
				BEGIN
				PRINT 'Member has reached the maximum amount of loans.'
				RETURN
				END

			--Setting due date based on member type
			DECLARE @CurrentDate datetime = GETDATE ( );
			DECLARE @DueDate datetime;
			DECLARE @MemberType int;

			SET @MemberType = (
				SELECT memberType
				FROM Member
				WHERE cardID = @MemberCardID
			)

			IF @MemberType = 0
				SET @DueDate = DATEADD(day, 21, @CurrentDate);
			ELSE IF @MemberType = 1
				SET @DueDate = DATEADD(month, 3, @CurrentDate); 


			-- Inserting new loan to Loan table
			INSERT INTO Loan (
			id,
			startDate,  
			dueDate,
			returnDate,
			memberCardID,
			bookCopyBarcode)
			OUTPUT inserted.id
			VALUES ( 
			NEWID ( ),
			@CurrentDate,  
			@DueDate,  
			NULL,  
			@MemberCardID,
			@Barcode)

			--Updating availability of book
			UPDATE Book_Copy
			SET isAvailable = 1
			WHERE barcode = @Barcode
		COMMIT TRANSACTION
	END TRY

	BEGIN CATCH
		PRINT 'Creating loan failed.';
		ROLLBACK;
		THROW;
	END CATCH
END
