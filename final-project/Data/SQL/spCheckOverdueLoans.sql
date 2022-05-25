CREATE PROCEDURE [dbo].[CheckOverdueLoans]
    @studentGrace int = 1,
    @teacherGrace int = 2
AS
BEGIN
    -- prevent extra result set from SELECT statements
    SET NOCOUNT ON

    DECLARE @studentTypeID uniqueidentifier
    SET @studentTypeID = (
        SELECT (id)
        FROM Member_Type
        WHERE Member_Type.name='Student'
    )

    DECLARE @teacherTypeID uniqueidentifier
    SET @teacherTypeID = (
        SELECT (id)
        FROM Member_Type
        WHERE Member_Type.name='Teacher'
    )

    SELECT startDate, dueDate, title, firstName, lastName,
            country, city, addressLine1, addressLine2, postCode
    FROM Loan, Member, Address, Book_Copy, Book
    WHERE Loan.memberCardID = Member.cardID
        AND Address.id = Member.addressID
        AND Loan.bookCopyBarcode = Book_Copy.barcode
        AND Book.isbn = Book_Copy.bookISBN
        AND returnDate IS NULL
        AND ((Member.memberTypeID=@studentTypeID AND DATEADD(week,@studentGrace,dueDate) < GETDATE())
            OR (Member.memberTypeID=@teacherTypeID AND DATEADD(week,@teacherGrace,dueDate) < GETDATE()))
END