CREATE PROCEDURE [dbo].[CreateStudent]
    @ssn varchar(11),
    @firstname varchar(40),
    @lastname varchar(40),
    @phoneNum varchar(20),
    @expiration datetime,

    @country varchar(20),
    @city varchar(20),
    @addressLine1 varchar(40),
    @addressLine2 varchar(40),
    @postCode varchar(20)
AS
BEGIN
    -- prevent extra result set from SELECT statements
    SET NOCOUNT ON
    -- automatic rollback in case of exception
    SET XACT_ABORT ON

    BEGIN TRY
        DECLARE @memberTypeID uniqueidentifier
        SET @memberTypeID = (
            SELECT (id)
            FROM Member_Type
            WHERE Member_Type.name='Student'
        )
        
        DECLARE @addressID uniqueidentifier
        SET @addressID = NEWID()
        INSERT INTO Address
        (id, country, city, addressLine1, addressLine2, postCode)
        VALUES (@addressID, @country, @city, @addressLine1, @addressLine2, @postCode)

        INSERT INTO Member
        (ssn, firstName, lastName, phoNenum, expiration, memberTypeID, addressID)
        OUTPUT inserted.*
        VALUES (@ssn, @firstName, @lastName, @phoNenum, @expiration, @memberTypeID, @addressID)
    END TRY

    BEGIN CATCH
        PRINT 'Creating student member failed.'

        SELECT ERROR_NUMBER() AS ErrorNumber,
                ERROR_MESSAGE() AS ErrorMessage
    END CATCH
END