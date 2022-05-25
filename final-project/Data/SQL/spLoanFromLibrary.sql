CREATE PROCEDURE [dbo].[LoanFromLibrary]
    @libraryID uniqueidentifier,
    @bookCopyBarcode uniqueidentifier,
	@dueDate datetime,
	@direction bit
AS
BEGIN
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
	SET isAvailable = 1
	WHERE barcode = @bookCopyBarcode
END
