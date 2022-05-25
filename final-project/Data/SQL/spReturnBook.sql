CREATE PROCEDURE [dbo].[ReturnBook]
    @bookCopyBarcode uniqueidentifier
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    UPDATE Loan
	SET returnDate = GETDATE()
	WHERE returnDate IS NULL AND bookCopyBarcode = @bookCopyBarcode
	
	--Updating availability of book
	UPDATE Book_Copy
	SET isAvailable = 0
	WHERE barcode = @bookCopyBarcode
END
