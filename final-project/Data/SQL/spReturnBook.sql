/****** Object:  StoredProcedure [dbo].[ReturnBook]    Script Date: 2022. 05. 27. 17:39:05 ******/
-- SET ANSI_NULLS ON
-- GO
-- SET QUOTED_IDENTIFIER ON
-- GO
-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[ReturnBook]
(
    -- Add the parameters for the stored procedure here
    @bookCopyBarcode char(36)
)
AS
BEGIN
	BEGIN TRY
		SET NOCOUNT ON
		SET XACT_ABORT ON

		--Checking if the book is in a normal loan or an interlibrary loan.
		DECLARE @LoanType bit = NULL
		IF (@bookCopyBarcode IN (SELECT bookCopyBarcode FROM Loan WHERE returnDate IS NULL))
			SET @LoanType = 0
		ELSE IF (@bookCopyBarcode IN (SELECT bookCopyBarcode FROM InterLibrary_Loan WHERE returnDate IS NULL))
			SET @LoanType = 1

		BEGIN TRANSACTION
			-- SET NOCOUNT ON added to prevent extra result sets from
			-- interfering with SELECT statements.
			SET NOCOUNT ON

			IF @LoanType = 0
				UPDATE Loan
				SET returnDate = GETDATE()
				WHERE returnDate IS NULL AND bookCopyBarcode = @bookCopyBarcode
			ELSE IF @LoanType = 1
				UPDATE InterLibrary_Loan
				SET returnDate = GETDATE()
				WHERE returnDate IS NULL AND bookCopyBarcode = @bookCopyBarcode
	
			--Updating availability of book
			UPDATE Book_Copy
			SET isAvailable = 1
			WHERE barcode = @bookCopyBarcode
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF (@@tranCount > 0)
			PRINT 'Failed to return book.'
			ROLLBACK TRANSACTION;
		THROW;
	END CATCH
END
