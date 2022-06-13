/****** Object:  StoredProcedure [dbo].[CreateInterLibraryLoan]    Script Date: 2022. 05. 27. 17:38:27 ******/
-- SET ANSI_NULLS ON
-- GO
-- SET QUOTED_IDENTIFIER ON
-- GO
-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[CreateInterLibraryLoan]
(
    -- Add the parameters for the stored procedure here
    @libraryID char(36),		
    @bookCopyBarcode char(36),
	@dueDate datetime,
	@direction bit
)
AS
BEGIN
	BEGIN TRY
		SET NOCOUNT ON
		SET XACT_ABORT ON
		BEGIN TRANSACTION
			-- SET NOCOUNT ON added to prevent extra result sets from
			-- interfering with SELECT statements.
			SET NOCOUNT ON

			--Checking if book is available
			IF ((SELECT isAvailable FROM Book_Copy WHERE barcode = @bookCopyBarcode) = 0)
				BEGIN
					PRINT 'Book is not available.'
				RETURN
				END

			-- Inserting new loan to InterLibrary_Loan table
			INSERT INTO InterLibrary_Loan
			(startDate,dueDate,returnDate,direction,libraryID,bookCopyBarcode)
			OUTPUT INSERTED.*
			VALUES (GETDATE(),@dueDate,NULL,@direction,@libraryID,@bookCopyBarcode)

			--Updating availability of book
			UPDATE Book_Copy
			SET isAvailable = 0
			WHERE barcode = @bookCopyBarcode
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF (@@tranCount > 0)
			PRINT 'Creating interlibrary loan failed.'
			ROLLBACK TRANSACTION;
		THROW;
	END CATCH
END
