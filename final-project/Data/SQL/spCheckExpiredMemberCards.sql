CREATE PROCEDURE [dbo].[CheckExpiredMemberCards]
AS
BEGIN
    SET NOCOUNT ON

    DECLARE @studentTypeID uniqueidentifier
    SET @studentTypeID = (
        SELECT (id)
        FROM Member_Type
        WHERE Member_Type.name='Student'
    )

    SELECT *
    FROM Member
    WHERE Member.memberTypeID=@studentTypeID
    AND Member.expiration < GETDATE()
END