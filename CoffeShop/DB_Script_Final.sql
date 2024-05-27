
USE [QL_Quan_Coffee]
GO

/****** Object:  Table [dbo].[TM_AREA]    Script Date: 5/9/2024 21:04:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TM_AREA]') AND type in (N'U'))
DROP TABLE [dbo].[TM_AREA]
GO

/****** Object:  Table [dbo].[TM_AREA]    Script Date: 5/9/2024 21:04:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TM_AREA](
	[ID_Area] [int] IDENTITY(1,1) NOT NULL,
	[Area_Name] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Area] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  Table [dbo].[TM_BILL]    Script Date: 5/9/2024 21:05:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TM_BILL]') AND type in (N'U'))
DROP TABLE [dbo].[TM_BILL]
GO

/****** Object:  Table [dbo].[TM_BILL]    Script Date: 5/9/2024 21:05:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TM_BILL](
	[ID_Bill] [int] IDENTITY(1,1) NOT NULL,
	[Total] [int] NULL,
	[PaymentTime] [nvarchar](100) NULL,
	[ID_Area] [int] NOT NULL,
	[ID_Table] [int] NOT NULL,
	[ID_User] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Bill] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  Table [dbo].[TM_BILL_FOOD]    Script Date: 5/9/2024 21:05:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TM_BILL_FOOD]') AND type in (N'U'))
DROP TABLE [dbo].[TM_BILL_FOOD]
GO

/****** Object:  Table [dbo].[TM_BILL_FOOD]    Script Date: 5/9/2024 21:05:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TM_BILL_FOOD](
	[ID_Bill] [int] NOT NULL,
	[ID_Food] [varchar](10) NOT NULL,
	[Quantity] [int] NULL
) ON [PRIMARY]
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  Table [dbo].[TM_CATELOGY]    Script Date: 5/9/2024 21:06:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TM_CATELOGY]') AND type in (N'U'))
DROP TABLE [dbo].[TM_CATELOGY]
GO

/****** Object:  Table [dbo].[TM_CATELOGY]    Script Date: 5/9/2024 21:06:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TM_CATELOGY](
	[ID_Catelogy] [int] IDENTITY(1,1) NOT NULL,
	[Catelogy_Name] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Catelogy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  Table [dbo].[TM_FOOD]    Script Date: 5/9/2024 21:06:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TM_FOOD]') AND type in (N'U'))
DROP TABLE [dbo].[TM_FOOD]
GO

/****** Object:  Table [dbo].[TM_FOOD]    Script Date: 5/9/2024 21:06:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TM_FOOD](
	[ID_Food] [int] IDENTITY(1,1) NOT NULL,
	[Food_Name] [nvarchar](50) NOT NULL,
	[Unit_Cost] [int] NULL,
	[Discount] [int] NULL,
	[ID_Catelogy] [int] NOT NULL,
	[Base64_Image] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Food] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  Table [dbo].[TM_TABLE]    Script Date: 5/9/2024 21:06:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TM_TABLE]') AND type in (N'U'))
DROP TABLE [dbo].[TM_TABLE]
GO

/****** Object:  Table [dbo].[TM_TABLE]    Script Date: 5/9/2024 21:06:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TM_TABLE](
	[ID_Table] [int] IDENTITY(1,1) NOT NULL,
	[Table_Name] [nvarchar](10) NULL,
	[Table_Status] [varchar](5) NULL,
	[ID_Area] [int] NULL,
	[ID_Bill] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Table] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  Table [dbo].[TM_USER]    Script Date: 5/9/2024 21:06:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TM_USER]') AND type in (N'U'))
DROP TABLE [dbo].[TM_USER]
GO

/****** Object:  Table [dbo].[TM_USER]    Script Date: 5/9/2024 21:06:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TM_USER](
	[ID_User] [int] IDENTITY(1,1) NOT NULL,
	[Pass] [varchar](50) NOT NULL,
	[UserName] [nvarchar](100) NULL,
	[Email] [varchar](100) NULL,
	[PhoneNumber] [varchar](10) NULL,
	[CCCD] [varchar](12) NULL,
	[Posision] [nvarchar](100) NULL,
	[Working_Days] [int] NULL,
	[FullName] [nvarchar](max) NULL,
	[DateCreate] [varchar] (200),
PRIMARY KEY CLUSTERED 
(
	[ID_User] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  StoredProcedure [dbo].[TM_AREA_Delete]    Script Date: 5/9/2024 21:07:59 ******/
DROP PROCEDURE [dbo].[TM_AREA_Delete]
GO

/****** Object:  StoredProcedure [dbo].[TM_AREA_Delete]    Script Date: 5/9/2024 21:07:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[TM_AREA_Delete]
@area_ID int
AS
BEGIN
	delete from TM_AREA
	where ID_Area=@area_ID
END
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  StoredProcedure [dbo].[TM_AREA_Get_All]    Script Date: 5/9/2024 21:08:10 ******/
DROP PROCEDURE [dbo].[TM_AREA_Get_All]
GO

/****** Object:  StoredProcedure [dbo].[TM_AREA_Get_All]    Script Date: 5/9/2024 21:08:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[TM_AREA_Get_All]
AS
BEGIN
	select * from TM_AREA
END
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  StoredProcedure [dbo].[TM_AREA_Insert]    Script Date: 5/9/2024 21:08:20 ******/
DROP PROCEDURE [dbo].[TM_AREA_Insert]
GO

/****** Object:  StoredProcedure [dbo].[TM_AREA_Insert]    Script Date: 5/9/2024 21:08:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE proc [dbo].[TM_AREA_Insert]
@area_Name nvarchar(500)
as
begin
INSERT INTO [dbo].[TM_AREA]
           ([Area_Name])
     VALUES
           (@area_Name)
end
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  StoredProcedure [dbo].[TM_AREA_Update]    Script Date: 5/9/2024 21:08:29 ******/
DROP PROCEDURE [dbo].[TM_AREA_Update]
GO

/****** Object:  StoredProcedure [dbo].[TM_AREA_Update]    Script Date: 5/9/2024 21:08:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



create proc [dbo].[TM_AREA_Update]
@id_Area int, @area_Name nvarchar(500)
as
begin
	Update TM_AREA
	Set Area_Name=@area_Name
	where ID_Area=@id_Area
end
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  StoredProcedure [dbo].[TM_Bill_Delete]    Script Date: 5/9/2024 21:08:40 ******/
DROP PROCEDURE [dbo].[TM_Bill_Delete]
GO

/****** Object:  StoredProcedure [dbo].[TM_Bill_Delete]    Script Date: 5/9/2024 21:08:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[TM_Bill_Delete]
@bill_id int
as
begin
	Delete from TM_BILL
	Where ID_Bill=@bill_id

	Delete from TM_BILL_FOOD
	Where ID_Bill=@bill_id

	Update TM_TABLE set Table_Status = 'OFF', ID_Bill = NULL
	Where ID_Bill = @bill_id
end
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  StoredProcedure [dbo].[TM_BILL_FOOD_Delete]    Script Date: 5/9/2024 21:09:00 ******/
DROP PROCEDURE [dbo].[TM_BILL_FOOD_Delete]
GO

/****** Object:  StoredProcedure [dbo].[TM_BILL_FOOD_Delete]    Script Date: 5/9/2024 21:09:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create proc [dbo].[TM_BILL_FOOD_Delete]
@id_Bill int,@id_Food int
as
begin 
	Delete from TM_BILL_FOOD
	where ID_Bill=@id_Bill and ID_Food=@id_Food
end
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  StoredProcedure [dbo].[TM_BILL_FOOD_GetAll]    Script Date: 5/9/2024 21:09:07 ******/
DROP PROCEDURE [dbo].[TM_BILL_FOOD_GetAll]
GO

/****** Object:  StoredProcedure [dbo].[TM_BILL_FOOD_GetAll]    Script Date: 5/9/2024 21:09:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create proc [dbo].[TM_BILL_FOOD_GetAll]
as
begin
	select * from TM_BILL_FOOD
end
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  StoredProcedure [dbo].[TM_BILL_FOOD_Insert]    Script Date: 5/9/2024 21:09:17 ******/
DROP PROCEDURE [dbo].[TM_BILL_FOOD_Insert]
GO

/****** Object:  StoredProcedure [dbo].[TM_BILL_FOOD_Insert]    Script Date: 5/9/2024 21:09:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create proc [dbo].[TM_BILL_FOOD_Insert]
@id_Bill int,@id_Food int ,@quantity int
as
begin 
	Insert into TM_BILL_FOOD(ID_Bill,ID_Food ,Quantity)
	values(
		@id_Bill,
		@id_Food, 
		@quantity
	)
end
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  StoredProcedure [dbo].[TM_BILL_FOOD_Select]    Script Date: 5/9/2024 21:09:26 ******/
DROP PROCEDURE [dbo].[TM_BILL_FOOD_Select]
GO

/****** Object:  StoredProcedure [dbo].[TM_BILL_FOOD_Select]    Script Date: 5/9/2024 21:09:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create proc [dbo].[TM_BILL_FOOD_Select]
@id_Bill int,@id_Food int
as
begin
	Select * from TM_BILL_FOOD
	inner join TM_FOOD on TM_BILL_FOOD.ID_Food=TM_FOOD.ID_Food
	where TM_BILL_FOOD.ID_Bill=@id_Bill and TM_BILL_FOOD.ID_Food=@id_Food
end
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  StoredProcedure [dbo].[TM_BILL_FOOD_Update]    Script Date: 5/9/2024 21:09:42 ******/
DROP PROCEDURE [dbo].[TM_BILL_FOOD_Update]
GO

/****** Object:  StoredProcedure [dbo].[TM_BILL_FOOD_Update]    Script Date: 5/9/2024 21:09:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create proc [dbo].[TM_BILL_FOOD_Update]
@id_Bill int,@id_Food int,@quantity int
as
begin 
	Update TM_BILL_FOOD
	set
		ID_Food=@id_Food,
		Quantity=@quantity
	where ID_Bill=@id_Bill
end
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  StoredProcedure [dbo].[TM_BILL_GetAll]    Script Date: 5/9/2024 21:09:53 ******/
DROP PROCEDURE [dbo].[TM_BILL_GetAll]
GO

/****** Object:  StoredProcedure [dbo].[TM_BILL_GetAll]    Script Date: 5/9/2024 21:09:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create proc [dbo].[TM_BILL_GetAll]
as
begin
	select * from TM_BILL
end
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  StoredProcedure [dbo].[TM_BILL_Insert]    Script Date: 5/9/2024 21:10:01 ******/
DROP PROCEDURE [dbo].[TM_BILL_Insert]
GO

/****** Object:  StoredProcedure [dbo].[TM_BILL_Insert]    Script Date: 5/9/2024 21:10:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create proc [dbo].[TM_BILL_Insert]
@total int, @paymentTime nvarchar(100),@id_Area int,@id_Table int,@id_User int
as
begin
INSERT INTO [dbo].[TM_BILL]
           ([Total]
           ,[PaymentTime]
           ,[ID_Area]
           ,[ID_Table]
           ,[ID_User])
     VALUES
           (@total,@paymentTime,@id_Area,@id_Table,@id_User)
end
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  StoredProcedure [dbo].[TM_BILL_Select]    Script Date: 5/9/2024 21:10:10 ******/
DROP PROCEDURE [dbo].[TM_BILL_Select]
GO

/****** Object:  StoredProcedure [dbo].[TM_BILL_Select]    Script Date: 5/9/2024 21:10:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create proc [dbo].[TM_BILL_Select]
@id_Bill int
as
begin
	Select * 
	from TM_BILL
	where ID_Bill=@id_Bill
end
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  StoredProcedure [dbo].[TM_BILL_Update]    Script Date: 5/9/2024 21:10:19 ******/
DROP PROCEDURE [dbo].[TM_BILL_Update]
GO

/****** Object:  StoredProcedure [dbo].[TM_BILL_Update]    Script Date: 5/9/2024 21:10:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




create proc [dbo].[TM_BILL_Update]
@id int ,@total int, @paymentTime nvarchar(100),@id_Area int,@id_Table int
as
begin
UPDATE [dbo].[TM_BILL]
   SET 
	Total=@total,
	PaymentTime=@paymentTime
	where
		ID_Bill=@id
end
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  StoredProcedure [dbo].[TM_CATELOGY_Delete]    Script Date: 5/9/2024 21:10:28 ******/
DROP PROCEDURE [dbo].[TM_CATELOGY_Delete]
GO

/****** Object:  StoredProcedure [dbo].[TM_CATELOGY_Delete]    Script Date: 5/9/2024 21:10:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[TM_CATELOGY_Delete]
@id_Catelogy int
AS
BEGIN
	delete from TM_CATELOGY
	where ID_Catelogy=@id_Catelogy
END
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  StoredProcedure [dbo].[TM_CATELOGY_GetAll]    Script Date: 5/9/2024 21:10:36 ******/
DROP PROCEDURE [dbo].[TM_CATELOGY_GetAll]
GO

/****** Object:  StoredProcedure [dbo].[TM_CATELOGY_GetAll]    Script Date: 5/9/2024 21:10:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



create proc [dbo].[TM_CATELOGY_GetAll]
as
begin
	select * from TM_CATELOGY
end
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  StoredProcedure [dbo].[TM_CATELOGY_Insert]    Script Date: 5/9/2024 21:10:46 ******/
DROP PROCEDURE [dbo].[TM_CATELOGY_Insert]
GO

/****** Object:  StoredProcedure [dbo].[TM_CATELOGY_Insert]    Script Date: 5/9/2024 21:10:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[TM_CATELOGY_Insert]
@catelogy_Name nvarchar(50)
AS
BEGIN
	insert TM_CATELOGY(Catelogy_Name)
	values (@catelogy_Name)
END
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  StoredProcedure [dbo].[TM_CATELOGY_Update]    Script Date: 5/9/2024 21:10:54 ******/
DROP PROCEDURE [dbo].[TM_CATELOGY_Update]
GO

/****** Object:  StoredProcedure [dbo].[TM_CATELOGY_Update]    Script Date: 5/9/2024 21:10:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[TM_CATELOGY_Update]
@id_Catelogy int,@catelogy_Name nvarchar(50)
AS
BEGIN
	update TM_CATELOGY
	set
	
	Catelogy_Name=@catelogy_Name
	where
	ID_Catelogy=@id_Catelogy
END
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  StoredProcedure [dbo].[TM_Delete_Bill]    Script Date: 5/9/2024 21:11:01 ******/
DROP PROCEDURE [dbo].[TM_Delete_Bill]
GO

/****** Object:  StoredProcedure [dbo].[TM_Delete_Bill]    Script Date: 5/9/2024 21:11:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE proc [dbo].[TM_Delete_Bill]
@bill_id int
as
begin
	Delete from TM_BILL
	Where ID_Bill=@bill_id

	Delete from TM_BILL_FOOD
	Where ID_Bill=@bill_id
end
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  StoredProcedure [dbo].[TM_FOOD_Delete]    Script Date: 5/9/2024 21:11:09 ******/
DROP PROCEDURE [dbo].[TM_FOOD_Delete]
GO

/****** Object:  StoredProcedure [dbo].[TM_FOOD_Delete]    Script Date: 5/9/2024 21:11:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[TM_FOOD_Delete]
@food_ID int
AS
BEGIN
	delete from TM_FOOD
	where ID_Food=@food_ID
END
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  StoredProcedure [dbo].[TM_FOOD_Insert]    Script Date: 5/9/2024 21:11:16 ******/
DROP PROCEDURE [dbo].[TM_FOOD_Insert]
GO

/****** Object:  StoredProcedure [dbo].[TM_FOOD_Insert]    Script Date: 5/9/2024 21:11:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[TM_FOOD_Insert]
@food_Name nvarchar(50),@unit_Cost int,@discount int,@id_Catelogy int,@base64_Image varchar(MAX)
as
begin
	insert into TM_FOOD(Food_Name,Unit_Cost,Discount,ID_Catelogy,Base64_Image)
	values(
		@food_Name,
		@unit_Cost,
		@discount,
		@id_Catelogy,
		@base64_Image
	)
end
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  StoredProcedure [dbo].[TM_FOOD_Select_All]    Script Date: 5/9/2024 21:11:23 ******/
DROP PROCEDURE [dbo].[TM_FOOD_Select_All]
GO

/****** Object:  StoredProcedure [dbo].[TM_FOOD_Select_All]    Script Date: 5/9/2024 21:11:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



create proc [dbo].[TM_FOOD_Select_All]
as
begin
	Select * from TM_FOOD
end	
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  StoredProcedure [dbo].[TM_FOOD_Select_By_Catelogy]    Script Date: 5/9/2024 21:11:30 ******/
DROP PROCEDURE [dbo].[TM_FOOD_Select_By_Catelogy]
GO

/****** Object:  StoredProcedure [dbo].[TM_FOOD_Select_By_Catelogy]    Script Date: 5/9/2024 21:11:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE proc [dbo].[TM_FOOD_Select_By_Catelogy]
@catelogy_id int
as
begin
	Select * from TM_FOOD
	where ID_Catelogy=@catelogy_id
end	
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  StoredProcedure [dbo].[TM_FOOD_Update]    Script Date: 5/9/2024 21:11:38 ******/
DROP PROCEDURE [dbo].[TM_FOOD_Update]
GO

/****** Object:  StoredProcedure [dbo].[TM_FOOD_Update]    Script Date: 5/9/2024 21:11:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[TM_FOOD_Update]
@id_Food int,@food_Name nvarchar(50),@unit_Cost int,@discount int,@id_Catelogy int,@base64_Image varchar(MAX)
as
begin
	Update TM_FOOD
	set 	
		Food_Name=@food_Name,
		Unit_Cost=@unit_Cost,
		Discount=@discount,
		ID_Catelogy=@id_Catelogy,
		Base64_Image=@base64_Image
	where 
		TM_FOOD.ID_Food=@id_Food
end
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  StoredProcedure [dbo].[TM_TABLE_Delete]    Script Date: 5/9/2024 21:11:46 ******/
DROP PROCEDURE [dbo].[TM_TABLE_Delete]
GO

/****** Object:  StoredProcedure [dbo].[TM_TABLE_Delete]    Script Date: 5/9/2024 21:11:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[TM_TABLE_Delete]
@table_ID int
AS
BEGIN
	delete from TM_TABLE
	where ID_Table=@table_ID
END
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  StoredProcedure [dbo].[TM_TABLE_GetAll]    Script Date: 5/9/2024 21:11:53 ******/
DROP PROCEDURE [dbo].[TM_TABLE_GetAll]
GO

/****** Object:  StoredProcedure [dbo].[TM_TABLE_GetAll]    Script Date: 5/9/2024 21:11:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE proc [dbo].[TM_TABLE_GetAll]
@area int
as
begin
	select ROW_NUMBER() OVER(ORDER BY ID_Table) as STT,* from TM_TABLE
	where ID_Area=@area
end
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  StoredProcedure [dbo].[TM_TABLE_Insert]    Script Date: 5/9/2024 21:12:00 ******/
DROP PROCEDURE [dbo].[TM_TABLE_Insert]
GO

/****** Object:  StoredProcedure [dbo].[TM_TABLE_Insert]    Script Date: 5/9/2024 21:12:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




create proc [dbo].[TM_TABLE_Insert]
	@table_Name nvarchar(10),@table_Status varchar(5),@id_Area int
as
begin
INSERT INTO [dbo].[TM_TABLE]
           ([Table_Name]
           ,[Table_Status]
           ,[ID_Area])
     VALUES
           (
		   @table_Name,
		   @table_Status,
		   @id_Area
		   )
end
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  StoredProcedure [dbo].[TM_TABLE_Update]    Script Date: 5/9/2024 21:12:07 ******/
DROP PROCEDURE [dbo].[TM_TABLE_Update]
GO

/****** Object:  StoredProcedure [dbo].[TM_TABLE_Update]    Script Date: 5/9/2024 21:12:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




create proc [dbo].[TM_TABLE_Update]
@table_ID int,	@table_Name nvarchar(10),@table_Status varchar(5),@id_Area int, @bill_id int
as
begin
	Update TM_TABLE
	set
           [Table_Name]=@table_Name,
           [Table_Status]=@table_Status ,
		   [ID_Bill]=@bill_id
     where [ID_Area]= @id_Area and ID_Table=@table_ID
end
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  StoredProcedure [dbo].[TM_TABLE_Update_ChangeStatus]    Script Date: 5/9/2024 21:12:21 ******/
DROP PROCEDURE [dbo].[TM_TABLE_Update_ChangeStatus]
GO

/****** Object:  StoredProcedure [dbo].[TM_TABLE_Update_ChangeStatus]    Script Date: 5/9/2024 21:12:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



create proc [dbo].[TM_TABLE_Update_ChangeStatus]
	@id_Table int,@table_Status varchar(5),@id_Area int
as
begin
--Thay đổi trạng thái của bàn theo mã bàn và mã khu vực
Update [dbo].[TM_TABLE]
  set 
  Table_Status=@table_Status
  where 
  ID_Table=@id_Table and ID_Area=@id_Area
end
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  StoredProcedure [dbo].[TM_User_Delete]    Script Date: 5/9/2024 21:12:28 ******/
DROP PROCEDURE [dbo].[TM_User_Delete]
GO

/****** Object:  StoredProcedure [dbo].[TM_User_Delete]    Script Date: 5/9/2024 21:12:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[TM_User_Delete]
@user_ID int
as
begin
	Delete from TM_USER
	Where ID_User=@user_ID
end
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  StoredProcedure [dbo].[TM_USER_Get_All]    Script Date: 5/9/2024 21:12:35 ******/
DROP PROCEDURE [dbo].[TM_USER_Get_All]
GO

/****** Object:  StoredProcedure [dbo].[TM_USER_Get_All]    Script Date: 5/9/2024 21:12:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[TM_USER_Get_All]
@UserName varchar(50), @pass varchar(50)
AS
BEGIN
	select * from TM_USER
	where UserName=@UserName and Pass=@pass
END
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  StoredProcedure [dbo].[TM_USER_GetAll_By_Boss]    Script Date: 5/9/2024 21:12:50 ******/
DROP PROCEDURE [dbo].[TM_USER_GetAll_By_Boss]
GO

/****** Object:  StoredProcedure [dbo].[TM_USER_GetAll_By_Boss]    Script Date: 5/9/2024 21:12:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



create proc [dbo].[TM_USER_GetAll_By_Boss]
as
begin
	select * from TM_USER
end
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  StoredProcedure [dbo].[TM_USER_Insert]    Script Date: 5/9/2024 21:12:59 ******/
DROP PROCEDURE [dbo].[TM_USER_Insert]
GO

/****** Object:  StoredProcedure [dbo].[TM_USER_Insert]    Script Date: 5/9/2024 21:12:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE proc [dbo].[TM_USER_Insert]
@pass varchar(50),@userName nvarchar(100), @email varchar(100),@phoneNumber varchar(10), @cccd varchar(12),@position nvarchar(100),@working_Day int,@fullname nvarchar(max), @datecreate nvarchar(200)
as
begin
	Insert into TM_USER(Pass,UserName,Email,PhoneNumber,CCCD,Posision,Working_Days,FullName, DateCreate)
	values 
	(
	@pass,
	@userName,
	@email,
	@phoneNumber,
	@cccd,
	@position,
	@working_Day,
	@fullname,
	@datecreate
	)
end
GO

USE [QL_Quan_Coffee]
GO

/****** Object:  StoredProcedure [dbo].[TM_USER_Update]    Script Date: 5/9/2024 21:13:07 ******/
DROP PROCEDURE [dbo].[TM_USER_Update]
GO

/****** Object:  StoredProcedure [dbo].[TM_USER_Update]    Script Date: 5/9/2024 21:13:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE proc [dbo].[TM_USER_Update]
@id_User int,@pass varchar(50),@userName nvarchar(100), @email varchar(100),@phoneNumber varchar(10), @cccd varchar(12),@position nvarchar(100),@working_Day int, @fullname nvarchar(max)
as
begin
	Update TM_USER
	set
		Pass=@pass,
		UserName=@userName,
		Email=@email,
		PhoneNumber=@phoneNumber,
		CCCD=@cccd,
		Posision=@position,
		Working_Days=@working_Day,
		FullName = @fullname
		where ID_User=@id_User
end
GO
  