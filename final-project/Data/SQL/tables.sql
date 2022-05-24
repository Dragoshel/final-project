DECLARE @myid uniqueidentifier;
SET @myid = NEWID();

CREATE TABLE Address (
	id uniqueidentifier NOT NULL DEFAULT newid(),
	country varchar(20) NOT NULL,
	city varchar(20) NOT NULL,
	addressLine1 varchar(40) NOT NULL,
	addressLine2 varchar(40) NULL,
	postCode varchar(20) NOT NULL,

	CONSTRAINT PK_Address PRIMARY KEY (id)
);

CREATE TABLE Author (
	id uniqueidentifier NOT NULL DEFAULT newid(),
	firstName varchar(40) NOT NULL,
	lastName varchar(40) NOT NULL,
	
	CONSTRAINT PK_Author PRIMARY KEY (id)
);

CREATE TABLE Book (
	isbn char(17) NOT NULL,
	title varchar(240) NOT NULL,
	edition varchar(40) NOT NULL,
	subject varchar(40) NOT NULL,
	description varchar(240) NOT NULL,
	isLendable bit NOT NULL,
	inStock bit NOT NULL,
	
	CONSTRAINT PK_Book PRIMARY KEY (isbn)
);

CREATE TABLE Library (
	id uniqueidentifier NOT NULL DEFAULT newid(),
	
	CONSTRAINT PK_Library PRIMARY KEY (id)
);

CREATE TABLE Book_Author (
	id uniqueidentifier NOT NULL DEFAULT newid(),
	bookISBN char(17) NOT NULL,
	
	authorID uniqueidentifier NOT NULL,
	
	CONSTRAINT PK_Book_Author PRIMARY KEY (id),
	CONSTRAINT FK_Book_Author_Author FOREIGN KEY (authorID) REFERENCES Author(id),
	CONSTRAINT FK_Book_Author_Book FOREIGN KEY (bookISBN) REFERENCES Book(isbn)
);

CREATE TABLE Book_Copy (
	barcode uniqueidentifier NOT NULL DEFAULT newid(),
	isAvailable bit NOT NULL,
	bookISBN char(17) NOT NULL,
	
	libraryID uniqueidentifier NOT NULL,
	
	CONSTRAINT PK_Book_Copy PRIMARY KEY (barcode),
	CONSTRAINT FK_Book_Copy_Book FOREIGN KEY (bookISBN) REFERENCES Book(isbn),
	CONSTRAINT FK_Book_Copy_Library FOREIGN KEY (libraryID) REFERENCES Library(id)
);

CREATE TABLE InterLibrary_Loan (
	id uniqueidentifier NOT NULL DEFAULT newid(),
	startDate datetime NOT NULL,
	dueDate datetime NOT NULL,
	returnDate datetime NULL,
	direction bit NOT NULL,
	
	bookCopyBarcode uniqueidentifier NOT NULL,
	libraryID uniqueidentifier NOT NULL,
	
	CONSTRAINT PK_InterLibrary_Loan PRIMARY KEY (id),
	CONSTRAINT FK_InterLibrary_Loan_Book_Copy FOREIGN KEY (bookCopyBarcode) REFERENCES Book_Copy(barcode),
	CONSTRAINT FK_InterLibrary_Loan_Library FOREIGN KEY (libraryID) REFERENCES Library(id)
);

CREATE TABLE Librarian_Type (
	id char(36) NOT NULL,
	name varchar(40) NOT NULL,

	CONSTRAINT PK_LibrarianType PRIMARY KEY (ID)
);

CREATE TABLE Librarian (
	ssn varchar(11) NOT NULL,
	firstName varchar(40) NOT NULL,
	lastName varchar(40) NOT NULL,
	phoneNum varchar(20) NOT NULL,
	
	librarianTypeID char(36) NOT NULL,
	libraryID uniqueidentifier NOT NULL,
	
	CONSTRAINT PK_Librarian PRIMARY KEY (ssn),
	CONSTRAINT FK_Librarian_Library FOREIGN KEY (libraryID) REFERENCES Library(id),
	CONSTRAINT FK_Librarian_LibrarianType FOREIGN KEY (librarianTypeID) REFERENCES Librarian_Type(id)
);

CREATE TABLE Member_Type (
	id char(36) NOT NULL,
	name varchar(40) NOT NULL,

	CONSTRAINT PK_MemberType PRIMARY KEY (ID)
);

CREATE TABLE [Member] (
	ssn varchar(11) NOT NULL,
	firstName varchar(40) NOT NULL,
	lastName varchar(40) NOT NULL,
	phoneNum varchar(20) NOT NULL,
	expiration datetime NOT NULL,
	
	memberTypeID char(36) NOT NULL,
	addressID uniqueidentifier NOT NULL,
	
	CONSTRAINT PK_Member PRIMARY KEY (ssn),
	CONSTRAINT FK_Member_Address FOREIGN KEY (addressID) REFERENCES Address(id),
	CONSTRAINT FK_Member_MemberType FOREIGN KEY (memberTypeID) REFERENCES Member_Type(id),
);

CREATE TABLE Loan (
	id uniqueidentifier NOT NULL DEFAULT newid(),
	startDate datetime NOT NULL,
	dueDate datetime NOT NULL,
	returnDate datetime NULL,
	
	memberSsn varchar(11) NOT NULL,
	bookCopyBarcode uniqueidentifier NOT NULL,
	
	CONSTRAINT PK_Loan PRIMARY KEY (id),
	CONSTRAINT FK_Loan_Book_Copy FOREIGN KEY (bookCopyBarcode) REFERENCES Book_Copy(barcode),
	CONSTRAINT FK_Loan_Member FOREIGN KEY (memberSsn) REFERENCES [Member](ssn)
);

-- Address
INSERT INTO Address (id,country,city,addressLine1,addressLine2,postCode) VALUES
	 (N'A5D07E55-CCC2-112E-349D-002FB4AC800D',N'Martinique',N'Sacramento',N'71 East Cowley Way',N'6/91',N'04448-1183'),
	 (N'D97CAA66-BABB-D204-7F75-005537098791',N'Zimbabwe',N'Bakersfield',N'19 Green Clarendon Road',NULL,N'10693-5923'),
	 (N'8ED861EA-1F6A-8CFB-FDA2-00714E24742E',N'Macedonia',N'Glendale',N'75 Green Old Blvd.',N'2/41',N'25421-3287'),
	 (N'DD5215DC-B25B-A789-DFAF-007567BBE762',N'Aruba',N'Santa Ana',N'838 Cowley Parkway',NULL,N'84798-2228'),
	 (N'C461E863-2513-8FEE-F2C1-00B0F75862BE',N'Afghanistan',N'Arlington',N'78 North Clarendon Parkway',N'2/92',N'03959-0540'),
	 (N'5B65EE7F-2EA8-F50E-DD1A-00BCBD9BDEE5',N'Uruguay',N'Dallas',N'59 White Fabien Way',NULL,N'63264-9412'),
	 (N'30701270-BA69-0479-9A55-00CB1F3306DE',N'Kyrgyzstan',N'Omaha',N'294 Nobel Way',NULL,N'88997-0870'),
	 (N'EE28B47B-9E8A-03F8-E4D5-00DE9E76FC4A',N'Spain',N'Glendale',N'62 North Oak Street',NULL,N'28029-2546'),
	 (N'EEBBCBF2-3F72-4309-61A8-00EFC4233C11',N'Kiribati',N'Columbus',N'804 Hague Way',N'3/93',N'09204-0113'),
	 (N'D871FCF5-5115-8329-97F6-01103A94F9BC',N'Eire',N'Shreveport',N'23 First Freeway',N'2/32',N'71712-3085');
-- Author
INSERT INTO Author (id,firstName,lastName) VALUES
	 (N'B025865D-D0B5-69BD-791E-0000ABA75D8D',N'Earnest',N'Malone'),
	 (N'BBE2B821-EFCB-8559-509F-000248E0B761',N'Alonzo',N'Vance'),
	 (N'3E531A4C-AD3E-4F8E-E7F1-0004200B8487',N'Arlene',N'Lowe'),
	 (N'E0C766EB-C650-FD73-15FA-000492FD32ED',N'Marilyn',N'Jimenez'),
	 (N'17A62EC6-CFA9-49F0-7D72-00053A9545E0',N'Jason',N'Randall'),
	 (N'E0D3860A-95CC-4E64-A026-0008DA67641C',N'Leon',N'Le'),
	 (N'0B83C624-CC14-4458-47E0-000A2A3F34F7',N'Jacob',N'Schmidt'),
	 (N'3C5BCF18-B0AE-2D7E-AEDB-000C7A3AA5AF',N'Barry',N'Jackson'),
	 (N'FD2C874E-67D2-B84B-A91E-000C92036A5F',N'Gregory',N'Roberson'),
	 (N'20A71E44-170E-2E4E-A903-000DE1BF504E',N'Casey',N'Guerrero');
-- Book
INSERT INTO Book (isbn,title,edition,subject,description,isLendable,inStock) VALUES
	 (N'000-0-15-860431-3',N'rqulltbiiiipbudzxso',N'2',N'vjppssvzakawtnem',N'quad e e Sed transit. homo, linguens e vobis Pro et et transit. regit, Pro novum si cognitio, Et estis',1,1),
	 (N'000-0-16-494488-8',N'shzisbkamtqmdcisgjfjw',N'4',N'ncywenfptz',N'non vantis. delerium. Et apparens pars Tam et e quad Versus quantare vobis cognitio, ut non Quad non',0,1),
	 (N'000-0-23-816053-7',N'vjqbxowazkolbypmsfpwrlbncgohjllyitlczr',N'3',N'vdafptqhhphdrfdgipexhsmyhewzptm',N'imaginator et vobis brevens, transit. et quo Longam, glavans brevens, quo in essit. gravum regit, Et',0,0),
	 (N'000-0-31-895909-1',N'lpcsubjkbzndjhulxezcefloiqtli',N'5',N'bpvqnklpvoab',N'quo, si venit. Tam si quad bono sed non quad rarendum fecit, Multum apparens dolorum et si quad plorum volcans habitatio parte plurissimum',1,1),
	 (N'000-0-32-046695-5',N'ialukvivtqoycetpo',N'6',N'pbvlnvrkoeoiyhqocfkhqisveriwfxeitekjnplu',N'eggredior. gravum delerium. pars quartu quo fecundio, homo, quo non brevens, fecit, glavans parte estum.',1,1),
	 (N'000-0-53-793928-9',N'kklxrcysaaveunq',N'2',N'pggaoimhhrcxdkbsaziciw',N'quis vantis. transit. brevens, travissimantor plurissimum delerium. estis esset apparens et brevens,',1,1),
	 (N'000-0-53-956952-3',N'upbkawvbadhhxipqafsz',N'8',N'pechkmoohijiojkwcrzui',N'dolorum quad parte glavans regit, rarendum nomen quad et delerium. et brevens, e homo, essit. et gravis gravis',1,1),
	 (N'000-0-71-516981-5',N'lejqqweqndghirdomtkgnawjvptixgb',N'6',N'ymcgutmbqeobdyoaelesdkzkszkdxzmiwlat',N'Versus ut vantis. gravis gravis non essit. rarendum in quo gravum nomen linguens quis glavans apparens homo,',1,0),
	 (N'000-0-77-041573-1',N'bndrzaiqykcvqngkpcgjchofojzwzbaanmf',N'1',N'utonimtfpavuawqdleka',N'quorum quo rarendum et non gravum non estis linguens nomen fecundio, si homo, et et gravum plurissimum',1,1),
	 (N'000-0-81-287511-0',N'wpiizrniwzeqhscrexpffcrxnxtswigyzrehyfu',N'3',N'psgxlzgwyotpjhwmwv',N'e gravum vantis. non vantis. linguens volcans homo, et trepicandor vantis. quo estum. quoque et cognitio,',0,1);
-- Library
INSERT INTO Library (id) VALUES
	 (N'06A482D2-677C-F405-770B-0C891E6FD68B'),
	 (N'C753E97D-7824-ADF6-1438-6AC5BE2F5DAA'),
	 (N'7A6E9B97-D5DC-501B-BBE8-880604E7D9E9'),
	 (N'83ACC27E-FE44-CD8A-8196-A8B7B4D2D11F'),
	 (N'6E4B1382-9A3B-E247-C483-BE4CCABE01A4');
-- Book_Author
INSERT INTO Book_Author (id,bookISBN,authorID) VALUES
	 (N'94A55F4D-6FD3-2A2D-9732-0000149EFEBA',N'000-0-15-860431-3',N'B025865D-D0B5-69BD-791E-0000ABA75D8D'),
	 (N'B6FE6464-AFF9-D5DE-4AFC-0000E78EE150',N'000-0-16-494488-8',N'BBE2B821-EFCB-8559-509F-000248E0B761'),
	 (N'8C357CD5-0FA5-B4CC-EA16-0000F2EE309F',N'000-0-23-816053-7',N'3E531A4C-AD3E-4F8E-E7F1-0004200B8487'),
	 (N'E9F3C7C1-4920-2C85-E09E-000187354AF9',N'000-0-31-895909-1',N'E0C766EB-C650-FD73-15FA-000492FD32ED'),
	 (N'DE56F060-2380-F502-B403-0001ED608D78',N'000-0-32-046695-5',N'17A62EC6-CFA9-49F0-7D72-00053A9545E0'),
	 (N'38C08D92-3936-B6AD-E356-0002726F222D',N'000-0-53-793928-9',N'E0D3860A-95CC-4E64-A026-0008DA67641C'),
	 (N'EA8E9878-68F2-D85A-7AEE-0002912A8070',N'000-0-53-956952-3',N'0B83C624-CC14-4458-47E0-000A2A3F34F7'),
	 (N'CBC9337A-1B49-1EDE-7BA0-0002A5B3656F',N'000-0-71-516981-5',N'3C5BCF18-B0AE-2D7E-AEDB-000C7A3AA5AF'),
	 (N'8D62744B-10CA-5223-C706-0002AD7A9E99',N'000-0-77-041573-1',N'FD2C874E-67D2-B84B-A91E-000C92036A5F'),
	 (N'C525140E-F67E-14C6-8A7C-0002DBD34962',N'000-0-81-287511-0',N'20A71E44-170E-2E4E-A903-000DE1BF504E');
-- Book_Copy
INSERT INTO Book_Copy (barcode,isAvailable,bookISBN,libraryID) VALUES
	 (N'BDD9323B-32B2-23E5-DBCF-0000025455D8',1,N'000-0-15-860431-3',N'06A482D2-677C-F405-770B-0C891E6FD68B'),
	 (N'C47A8ED7-A127-F2EE-6F24-000008E8CC77',1,N'000-0-16-494488-8',N'C753E97D-7824-ADF6-1438-6AC5BE2F5DAA'),
	 (N'B6671605-9F67-A9DD-D9AA-000029A1B70A',1,N'000-0-23-816053-7',N'7A6E9B97-D5DC-501B-BBE8-880604E7D9E9'),
	 (N'9F50A24D-3C19-3F2C-9294-0000469EA971',1,N'000-0-31-895909-1',N'83ACC27E-FE44-CD8A-8196-A8B7B4D2D11F'),
	 (N'173C4DED-DCBE-650F-69B8-00006700E4C5',1,N'000-0-32-046695-5',N'6E4B1382-9A3B-E247-C483-BE4CCABE01A4'),
	 (N'1C947F6D-0E87-E5D9-A06C-0000926BB15C',1,N'000-0-53-793928-9',N'06A482D2-677C-F405-770B-0C891E6FD68B'),
	 (N'DCB70608-C170-CB8D-3E17-0000B5F6E10C',0,N'000-0-53-956952-3',N'C753E97D-7824-ADF6-1438-6AC5BE2F5DAA'),
	 (N'6FB99467-D133-DE09-1B58-0000BB3095EC',1,N'000-0-71-516981-5',N'7A6E9B97-D5DC-501B-BBE8-880604E7D9E9'),
	 (N'05F972AD-4CB9-1CE5-578C-0000FAD5F912',0,N'000-0-77-041573-1',N'83ACC27E-FE44-CD8A-8196-A8B7B4D2D11F'),
	 (N'83881BA4-79DC-6A57-F03E-00018C6D4991',1,N'000-0-81-287511-0',N'6E4B1382-9A3B-E247-C483-BE4CCABE01A4');
-- InterLibrary_Load
INSERT INTO InterLibrary_Loan (id,direction,startDate,dueDate,returnDate,bookCopyBarcode,libraryID) VALUES
	 (N'6A3B971D-3ECD-071E-B9A2-000D3282E0F8',1,'2012-11-26 01:58:13.000','2013-01-05 08:28:15.000','2012-12-24 03:03:08.400',N'BDD9323B-32B2-23E5-DBCF-0000025455D8',N'06A482D2-677C-F405-770B-0C891E6FD68B'),
	 (N'1240F489-E8B2-D092-8144-004C5A91C2BF',0,'2011-05-06 08:31:47.300','2011-07-04 12:30:56.340','2011-06-06 02:59:49.190',N'C47A8ED7-A127-F2EE-6F24-000008E8CC77',N'C753E97D-7824-ADF6-1438-6AC5BE2F5DAA'),
	 (N'AC2DD08F-5A04-0434-A5E1-006218347212',0,'2015-09-18 23:34:43.290','2015-11-02 02:38:24.560','2010-09-04 04:27:36.740',N'B6671605-9F67-A9DD-D9AA-000029A1B70A',N'7A6E9B97-D5DC-501B-BBE8-880604E7D9E9'),
	 (N'D9C83357-0542-0A47-D4AF-006A1E218BAE',1,'2010-09-06 01:17:34.370','2010-10-26 17:18:11.760','2020-08-23 02:56:06.160',N'9F50A24D-3C19-3F2C-9294-0000469EA971',N'83ACC27E-FE44-CD8A-8196-A8B7B4D2D11F'),
	 (N'321E0F50-92C4-CF66-72BE-007471546B4C',1,'2010-07-16 07:57:56.920','2010-09-03 22:05:41.740','2018-08-17 14:11:57.070',N'173C4DED-DCBE-650F-69B8-00006700E4C5',N'6E4B1382-9A3B-E247-C483-BE4CCABE01A4'),
	 (N'2D003F0A-78DC-EF52-29A2-0077A38B0758',0,'2020-07-15 05:59:50.620','2020-10-03 15:08:42.640','2013-11-09 01:09:26.310',N'1C947F6D-0E87-E5D9-A06C-0000926BB15C',N'06A482D2-677C-F405-770B-0C891E6FD68B'),
	 (N'4DC25374-C2C9-7B50-65D4-00CBAAC101EA',0,'2018-07-08 00:08:22.840','2018-09-07 15:29:40.510','2014-06-04 21:50:11.740',N'DCB70608-C170-CB8D-3E17-0000B5F6E10C',N'C753E97D-7824-ADF6-1438-6AC5BE2F5DAA'),
	 (N'36E67165-6DD1-B3B3-C1C4-00DDE33B81C1',1,'2013-09-20 22:24:05.910','2013-11-21 20:27:41.910',NULL,N'6FB99467-D133-DE09-1B58-0000BB3095EC',N'7A6E9B97-D5DC-501B-BBE8-880604E7D9E9'),
	 (N'F9DDAEEB-8A89-47F8-E60C-012EA4FA1410',1,'2010-03-31 01:47:21.140','2010-06-10 18:04:04.830',NULL,N'05F972AD-4CB9-1CE5-578C-0000FAD5F912',N'83ACC27E-FE44-CD8A-8196-A8B7B4D2D11F'),
	 (N'F8DF91D4-6A8B-5ECF-64E9-0193F7FC0CE9',0,'2014-05-02 21:05:04.470','2014-07-14 00:08:29.880',NULL,N'83881BA4-79DC-6A57-F03E-00018C6D4991',N'6E4B1382-9A3B-E247-C483-BE4CCABE01A4');
-- Librarian_Type
INSERT INTO Librarian_Type (id, name) VALUES
	 (N'6A49BAA6-7E02-40F7-9590-1522392A81FB', N'Chief_Librarian'),
	 (N'3C618959-C4F1-4C4F-A76C-3C66DE90AB18', N'Departmental_Associate_Librarian'),
	 (N'0758DFE1-A1A6-4B5F-B774-451FA3489138', N'Reference_Librarian'),
	 (N'EF58E066-9FE6-4FAD-B975-67683189F019', N'Checkout_Staff'),
	 (N'D3C26D7E-0E21-4DEF-978C-2B74BF797133', N'Library_Assistant');
-- Librarian
INSERT INTO Librarian (ssn,firstName,lastName,phoneNum,libraryID,librarianTypeID) VALUES
	 (N'026-15-2938',N'Anna',N'Kline',N'674-995-7529',N'83ACC27E-FE44-CD8A-8196-A8B7B4D2D11F',N'6A49BAA6-7E02-40F7-9590-1522392A81FB'),
	 (N'028-49-7232',N'Randolph',N'Ramos',N'255-954-8099',N'06A482D2-677C-F405-770B-0C891E6FD68B',N'3C618959-C4F1-4C4F-A76C-3C66DE90AB18'),
	 (N'034-40-6868',N'Keri',N'Gaines',N'696-838-4377',N'7A6E9B97-D5DC-501B-BBE8-880604E7D9E9',N'0758DFE1-A1A6-4B5F-B774-451FA3489138'),
	 (N'047-51-6448',N'Marvin',N'Lamb',N'758-824-9831',N'6E4B1382-9A3B-E247-C483-BE4CCABE01A4',N'EF58E066-9FE6-4FAD-B975-67683189F019'),
	 (N'053-12-3653',N'Barbara',N'Sharp',N'282-869-5405',N'83ACC27E-FE44-CD8A-8196-A8B7B4D2D11F',N'D3C26D7E-0E21-4DEF-978C-2B74BF797133'),
	 (N'054-89-5847',N'Stephan',N'Knox',N'853-054-0617',N'83ACC27E-FE44-CD8A-8196-A8B7B4D2D11F',N'6A49BAA6-7E02-40F7-9590-1522392A81FB'),
	 (N'055-51-4939',N'Jocelyn',N'Horton',N'577-836-8554',N'06A482D2-677C-F405-770B-0C891E6FD68B',N'3C618959-C4F1-4C4F-A76C-3C66DE90AB18'),
	 (N'090-32-0051',N'Everett',N'Lam',N'835-607-7010',N'83ACC27E-FE44-CD8A-8196-A8B7B4D2D11F',N'0758DFE1-A1A6-4B5F-B774-451FA3489138'),
	 (N'135-31-6567',N'Dominic',N'Thomas',N'455-350-3292',N'C753E97D-7824-ADF6-1438-6AC5BE2F5DAA',N'EF58E066-9FE6-4FAD-B975-67683189F019'),
	 (N'135-71-0360',N'Arlene',N'Davies',N'323-616-7227',N'06A482D2-677C-F405-770B-0C891E6FD68B',N'D3C26D7E-0E21-4DEF-978C-2B74BF797133');
-- Member_Type
INSERT INTO Member_Type (id, name) VALUES
	 (N'0EF3BFEB-888C-1FD0-24E3-00288FD76A6E', N'Student'),
	 (N'0EF3BFEB-888C-1FD0-24E3-00123BC2DEF2', N'Teacher');
-- Member
INSERT INTO [Member] (ssn,firstName,lastName,phoneNum,expiration,addressID,memberTypeID) VALUES
	 (N'000-01-3878',N'Faith',N'Davidson',N'762-749-8977','2012-11-26 01:58:13.000',N'A5D07E55-CCC2-112E-349D-002FB4AC800D', N'0EF3BFEB-888C-1FD0-24E3-00288FD76A6E'),
	 (N'000-01-5141',N'Kenneth',N'Munoz',N'745-949-0140','2011-05-06 08:31:47.300',N'D97CAA66-BABB-D204-7F75-005537098791', N'0EF3BFEB-888C-1FD0-24E3-00288FD76A6E'),
	 (N'000-04-3107',N'Lloyd',N'Boone',N'598-803-1714','2015-09-18 23:34:43.290',N'8ED861EA-1F6A-8CFB-FDA2-00714E24742E', N'0EF3BFEB-888C-1FD0-24E3-00288FD76A6E'),
	 (N'000-05-9731',N'Stefanie',N'Melendez',N'227-031-6560','2010-09-06 01:17:34.370',N'DD5215DC-B25B-A789-DFAF-007567BBE762', N'0EF3BFEB-888C-1FD0-24E3-00288FD76A6E'),
	 (N'000-08-9704',N'Gregory',N'Briggs',N'019-021-2903','2010-07-16 07:57:56.920',N'C461E863-2513-8FEE-F2C1-00B0F75862BE', N'0EF3BFEB-888C-1FD0-24E3-00288FD76A6E'),
	 (N'000-14-9088',N'Marcia',N'Andrade',N'617-534-0056','2020-07-15 05:59:50.620',N'5B65EE7F-2EA8-F50E-DD1A-00BCBD9BDEE5', N'0EF3BFEB-888C-1FD0-24E3-00123BC2DEF2'),
	 (N'000-17-0623',N'Shane',N'Calderon',N'538-095-4856','2018-07-08 00:08:22.840',N'30701270-BA69-0479-9A55-00CB1F3306DE', N'0EF3BFEB-888C-1FD0-24E3-00123BC2DEF2'),
	 (N'000-19-3511',N'Mickey',N'Lewis',N'825-619-0140','2013-09-20 22:24:05.910',N'EE28B47B-9E8A-03F8-E4D5-00DE9E76FC4A', N'0EF3BFEB-888C-1FD0-24E3-00123BC2DEF2'),
	 (N'000-33-5699',N'Joel',N'Shaw',N'389-608-3137','2010-03-31 01:47:21.140',N'EEBBCBF2-3F72-4309-61A8-00EFC4233C11', N'0EF3BFEB-888C-1FD0-24E3-00123BC2DEF2'),
	 (N'000-39-9060',N'Ruby',N'Hinton',N'488-424-4613','2014-05-02 21:05:04.470',N'D871FCF5-5115-8329-97F6-01103A94F9BC', N'0EF3BFEB-888C-1FD0-24E3-00123BC2DEF2');
-- Loan
INSERT INTO Loan (id,startDate,dueDate,returnDate,memberSsn,bookCopyBarcode) VALUES
	 (N'0EF2BFEA-9B8C-3FD0-74E3-00288FD76A6E','2012-11-26 01:58:13.000','2013-01-05 08:28:15.000','2012-12-24 03:03:08.400',N'000-01-3878',N'BDD9323B-32B2-23E5-DBCF-0000025455D8'),
	 (N'6F957E8C-1216-7D59-E330-0028E5EE5166','2011-05-06 08:31:47.300','2011-07-04 12:30:56.340','2011-06-06 02:59:49.190',N'000-01-5141',N'C47A8ED7-A127-F2EE-6F24-000008E8CC77'),
	 (N'593ABCE9-D0A7-687E-DB99-0035CEE91B34','2015-09-18 23:34:43.290','2015-11-02 02:38:24.560',NULL,N'000-04-3107',N'B6671605-9F67-A9DD-D9AA-000029A1B70A'),
	 (N'2A274A00-CC3A-32B3-3E60-003A1F4A1EC7','2010-09-06 01:17:34.370','2010-10-26 17:18:11.760',NULL,N'000-05-9731',N'9F50A24D-3C19-3F2C-9294-0000469EA971'),
	 (N'179B9B69-40BF-A465-A4D6-004815D171CE','2010-07-16 07:57:56.920','2010-09-03 22:05:41.740','2010-09-04 04:27:36.740',N'000-08-9704',N'173C4DED-DCBE-650F-69B8-00006700E4C5'),
	 (N'E9240A5F-250B-325C-B6D9-00493C3EC71B','2020-07-15 05:59:50.620','2020-10-03 15:08:42.640','2020-08-23 02:56:06.160',N'000-14-9088',N'1C947F6D-0E87-E5D9-A06C-0000926BB15C'),
	 (N'D5D04B05-9D31-8BDB-62CE-0049ACBED96B','2018-07-08 00:08:22.840','2018-09-07 15:29:40.510','2018-08-17 14:11:57.070',N'000-17-0623',N'DCB70608-C170-CB8D-3E17-0000B5F6E10C'),
	 (N'3DFDC253-2C71-D427-1C07-00578494EA3A','2013-09-20 22:24:05.910','2013-11-21 20:27:41.910','2013-11-09 01:09:26.310',N'000-19-3511',N'6FB99467-D133-DE09-1B58-0000BB3095EC'),
	 (N'39A1B7BD-0F0C-F685-6B90-00627937812A','2010-03-31 01:47:21.140','2010-06-10 18:04:04.830',NULL,N'000-33-5699',N'05F972AD-4CB9-1CE5-578C-0000FAD5F912'),
	 (N'9CEAC1F7-570E-B785-4206-00659083F45F','2014-05-02 21:05:04.470','2014-07-14 00:08:29.880','2014-06-04 21:50:11.740',N'000-39-9060',N'83881BA4-79DC-6A57-F03E-00018C6D4991');