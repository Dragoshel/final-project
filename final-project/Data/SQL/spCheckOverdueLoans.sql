CREATE PROCEDURE [dbo].[CheckOverdueLoans]
AS
BEGIN

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

    SELECT Loan.startDate, Loan.dueDate, Book.title, Member.firstName, Member.lastName,
            Address.country, Address.city, Address.addressLine1, Address.addressLine2, Address.postCode
    FROM Loan, Member, Address, Book_Copy, Book
    WHERE Loan.memberCardID = Member.cardID
        AND Address.id = Member.addressID
        AND Loan.bookCopyBarcode = Book_Copy.barcode
        AND Book.isbn = Book_Copy.bookISBN
        AND returnDate IS NULL
        AND ((Member.memberTypeID=@studentTypeID AND DATEADD(week,1,dueDate) < GETDATE())
            OR (Member.memberTypeID=@teacherTypeID AND DATEADD(week,2,dueDate) < GETDATE()))
END