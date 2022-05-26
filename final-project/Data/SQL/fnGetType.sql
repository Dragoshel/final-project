CREATE FUNCTION [dbo].[GetType]
(
    -- Add the parameters for the function here
    @typeName varchar(40)
)
RETURNS uniqueidentifier
AS
BEGIN
    SET ANSI_NULLS ON
    SET QUOTED_IDENTIFIER ON
    -- Declare the return variable here
    DECLARE @typeID uniqueidentifier

    -- Add the T-SQL statements to compute the return value here
    SET @typeID = (
        SELECT (id)
        FROM Member_Type
        WHERE Member_Type.name = @typeName
    )

    -- Return the result of the function
    RETURN @typeID
END