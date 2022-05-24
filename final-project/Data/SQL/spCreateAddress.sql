CREATE PROCEDURE [dbo].[CreateAddress]
    @country varchar(20),
    @city varchar(20),
    @addressLine1 varchar(40),
    @addressLine2 varchar(40),
    @postCode varchar(20)
AS
BEGIN
	SET NOCOUNT ON;

    INSERT INTO Address
    (country, city, addressLine1, addressLine2, postCode)
    OUTPUT inserted.ID
    VALUES (@country, @city, @addressLine1, @addressLine2, @postCode);
END;