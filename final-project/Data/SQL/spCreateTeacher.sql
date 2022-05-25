CREATE PROCEDURE [dbo].[CreateTeacher]
    @ssn varchar(11),
    @firstname varchar(40),
    @lastname varchar(40),
    @phoneNum varchar(20),
    @expiration datetime,

    @campusID uniqueidentifier
AS
BEGIN
    -- prevent extra result set from SELECT statements
    SET NOCOUNT ON
    -- automatic rollback in case of exception
    SET XACT_ABORT ON

    BEGIN TRY
        DECLARE @memberTypeID uniqueidentifier;
        SET @memberTypeID = (
            SELECT (id)
            FROM Member_Type
            WHERE Member_Type.name='Teacher'
        );

        DECLARE @addressID uniqueidentifier;
        SET @addressID = (
            SELECT (id)
            FROM Address
            WHERE Address.id=@campusID
        );

        INSERT INTO Member
        (ssn, firstName, lastName, phoNenum, expiration, memberTypeID, addressID)
        OUTPUT INSERTED.*
        VALUES (@ssn, @firstName, @lastName, @phoNenum, @expiration, @memberTypeID, @addressID);
        
    END TRY

    BEGIN CATCH
        PRINT 'Creating teacher member failed.'

        SELECT ERROR_NUMBER() AS ErrorNumber,
                ERROR_MESSAGE() AS ErrorMessage
    END CATCH
END