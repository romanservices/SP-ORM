USE [master]
GO
/****** Object:  Database [DataBaseA]    Script Date: 02/11/2015 22:09:02 ******/
CREATE DATABASE [DataBaseA] ON  PRIMARY 
( NAME = N'DataBaseA', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SUBTRACE\MSSQL\DATA\DataBaseA.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DataBaseA_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SUBTRACE\MSSQL\DATA\DataBaseA_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DataBaseA] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DataBaseA].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DataBaseA] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [DataBaseA] SET ANSI_NULLS OFF
GO
ALTER DATABASE [DataBaseA] SET ANSI_PADDING OFF
GO
ALTER DATABASE [DataBaseA] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [DataBaseA] SET ARITHABORT OFF
GO
ALTER DATABASE [DataBaseA] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [DataBaseA] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [DataBaseA] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [DataBaseA] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [DataBaseA] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [DataBaseA] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [DataBaseA] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [DataBaseA] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [DataBaseA] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [DataBaseA] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [DataBaseA] SET  DISABLE_BROKER
GO
ALTER DATABASE [DataBaseA] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [DataBaseA] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [DataBaseA] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [DataBaseA] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [DataBaseA] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [DataBaseA] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [DataBaseA] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [DataBaseA] SET  READ_WRITE
GO
ALTER DATABASE [DataBaseA] SET RECOVERY SIMPLE
GO
ALTER DATABASE [DataBaseA] SET  MULTI_USER
GO
ALTER DATABASE [DataBaseA] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [DataBaseA] SET DB_CHAINING OFF
GO
USE [DataBaseA]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 02/11/2015 22:09:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customer](
	[FirstName] [varchar](1000) NOT NULL,
	[LastName] [varchar](1000) NOT NULL,
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[AddressId] [bigint] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Customer] ON
INSERT [dbo].[Customer] ([FirstName], [LastName], [Id], [AddressId]) VALUES (N'Bill', N'Baxter', 1, 1)
INSERT [dbo].[Customer] ([FirstName], [LastName], [Id], [AddressId]) VALUES (N'Ted', N'Craven', 2, 2)
INSERT [dbo].[Customer] ([FirstName], [LastName], [Id], [AddressId]) VALUES (N'Gilbert', N'Smith', 3, 3)
INSERT [dbo].[Customer] ([FirstName], [LastName], [Id], [AddressId]) VALUES (N'Richard', N'Vaughan', 4, 3)
SET IDENTITY_INSERT [dbo].[Customer] OFF
/****** Object:  Table [dbo].[Address]    Script Date: 02/11/2015 22:09:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Address](
	[Line1] [varchar](1000) NOT NULL,
	[Line2] [varchar](1000) NULL,
	[City] [varchar](255) NOT NULL,
	[State] [varchar](2) NOT NULL,
	[Zip] [varchar](20) NOT NULL,
	[Id] [bigint] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Address] ON
INSERT [dbo].[Address] ([Line1], [Line2], [City], [State], [Zip], [Id]) VALUES (N'1234 Main st', NULL, N'Seattle', N'WA', N'98168', 1)
INSERT [dbo].[Address] ([Line1], [Line2], [City], [State], [Zip], [Id]) VALUES (N'123456 West South St', N'CO: Speaker', N'Glen Burnie', N'MD', N'21060', 2)
INSERT [dbo].[Address] ([Line1], [Line2], [City], [State], [Zip], [Id]) VALUES (N'7 West', NULL, N'Towson', N'MD', N'21221', 3)
SET IDENTITY_INSERT [dbo].[Address] OFF
/****** Object:  StoredProcedure [dbo].[_Save_Customer]    Script Date: 02/11/2015 22:09:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[_Save_Customer] @CustomerId int,
@FirstName AS varchar(1000),
@LastName AS varchar(1000),
@Line1 AS varchar(1000),
@Line2 AS varchar(1000),
@City AS varchar(255),
@State varchar(2),
@Zip AS varchar(20)
AS
BEGIN
  IF (@CustomerId > 0)
  BEGIN
    DECLARE @AddressId int = (SELECT
      AddressId
    FROM Customer
    WHERE Id = @CustomerId)
    UPDATE Customer
    SET FirstName = ISNULL(@FirstName, FirstName),
        LastName = ISNULL(@LastName, LastName)
    WHERE Id = @CustomerId
    IF (@AddressId > 0)
    BEGIN
      UPDATE [Address]
      SET Line1 = ISNULL(@Line1, Line1),
          Line2 = ISNULL(@Line2, Line2),
          City = ISNULL(@City, City),
          [State] = ISNULL(@State, [State]),
          Zip = ISNULL(@Zip, Zip)
    END
    ELSE
    BEGIN
      INSERT INTO [Address] (Line1, Line2, City, State, Zip)
        VALUES (@Line1, @Line2, @City, @State, @Zip)
      SET @AddressId = @@IDENTITY
      UPDATE Customer
      SET AddressId = @AddressId
      WHERE Id = @CustomerId
    END

  END
  ELSE
  BEGIN
    INSERT INTO Customer (FirstName, LastName)
      VALUES (@FirstName, @LastName)
    SET @CustomerId = @@IDENTITY
    INSERT INTO [Address] (Line1, Line2, City, State, Zip)
      VALUES (@Line1, @Line2, @City, @State, @Zip)
    SET @AddressId = @@IDENTITY
    UPDATE Customer
    SET AddressId = @AddressId
    WHERE Id = @CustomerId

  END


END
GO
/****** Object:  StoredProcedure [dbo].[_List_CustomerAndAddress]    Script Date: 02/11/2015 22:09:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[_List_CustomerAndAddress]
 @CustomerId int = null
AS
BEGIN
if(@CustomerId = 0)
BEGIN
set @CustomerId = null
END
	select  c.Id as CustomerId, c.FirstName, c.LastName, a.Line1, a.Line2, a.City, a.State, a.Zip  from Customer c
	inner join [Address] a on c.AddressId = a.Id 
	where c.Id = isnull(@CustomerId, c.Id) 
END
GO
