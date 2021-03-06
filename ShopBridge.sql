USE [ShopBridge]
GO
/****** Object:  Schema [Masters]    Script Date: 12/1/2021 5:53:11 PM ******/
CREATE SCHEMA [Masters]
GO
/****** Object:  Schema [Transactions]    Script Date: 12/1/2021 5:53:11 PM ******/
CREATE SCHEMA [Transactions]
GO
/****** Object:  Schema [UAM]    Script Date: 12/1/2021 5:53:11 PM ******/
CREATE SCHEMA [UAM]
GO
/****** Object:  Table [Masters].[tblCategory]    Script Date: 12/1/2021 5:53:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Masters].[tblCategory](
	[Cat_ID] [int] IDENTITY(1,1) NOT NULL,
	[Cat_Name] [nvarchar](max) NULL,
	[Active] [bit] NULL,
	[Created_By] [nvarchar](100) NULL,
	[Created_Date] [datetime] NULL,
	[Modify_By] [nvarchar](100) NULL,
	[Modify_Date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Cat_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Masters].[tblSubCategory]    Script Date: 12/1/2021 5:53:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Masters].[tblSubCategory](
	[SubCat_ID] [int] IDENTITY(1,1) NOT NULL,
	[SubCat_Name] [nvarchar](max) NULL,
	[MainCat_ID] [int] NULL,
	[Active] [bit] NULL,
	[Created_By] [nvarchar](100) NULL,
	[Created_Date] [datetime] NULL,
	[Modify_By] [nvarchar](100) NULL,
	[Modify_Date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[SubCat_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Transactions].[tblinventory_details]    Script Date: 12/1/2021 5:53:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Transactions].[tblinventory_details](
	[T_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[SubCat_ID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Unit_Price] [decimal](18, 2) NOT NULL,
	[Total_Price] [decimal](18, 2) NOT NULL,
	[Product_Description] [nvarchar](100) NOT NULL,
	[Active] [bit] NULL,
	[Created_By] [nvarchar](100) NULL,
	[Created_Date] [datetime] NULL,
	[Modify_By] [nvarchar](100) NULL,
	[Modify_Date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[T_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [Masters].[tblCategory] ON 

GO
INSERT [Masters].[tblCategory] ([Cat_ID], [Cat_Name], [Active], [Created_By], [Created_Date], [Modify_By], [Modify_Date]) VALUES (1, N'Electronics', 1, N'Admin', CAST(N'2021-11-25 00:00:00.000' AS DateTime), NULL, NULL)
GO
SET IDENTITY_INSERT [Masters].[tblCategory] OFF
GO
SET IDENTITY_INSERT [Masters].[tblSubCategory] ON 

GO
INSERT [Masters].[tblSubCategory] ([SubCat_ID], [SubCat_Name], [MainCat_ID], [Active], [Created_By], [Created_Date], [Modify_By], [Modify_Date]) VALUES (1, N'Computer-Desktop', 1, 1, N'Admin', CAST(N'2021-11-25 00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [Masters].[tblSubCategory] ([SubCat_ID], [SubCat_Name], [MainCat_ID], [Active], [Created_By], [Created_Date], [Modify_By], [Modify_Date]) VALUES (2, N'Computer-Laptop', 1, 1, N'Admin', CAST(N'2021-11-25 00:00:00.000' AS DateTime), NULL, NULL)
GO
SET IDENTITY_INSERT [Masters].[tblSubCategory] OFF
GO
ALTER TABLE [Masters].[tblSubCategory]  WITH CHECK ADD  CONSTRAINT [FK_tblSubCategory_tblCategory] FOREIGN KEY([MainCat_ID])
REFERENCES [Masters].[tblCategory] ([Cat_ID])
GO
ALTER TABLE [Masters].[tblSubCategory] CHECK CONSTRAINT [FK_tblSubCategory_tblCategory]
GO
ALTER TABLE [Transactions].[tblinventory_details]  WITH CHECK ADD  CONSTRAINT [FK_tblinventory_details_tblSubCategory] FOREIGN KEY([SubCat_ID])
REFERENCES [Masters].[tblSubCategory] ([SubCat_ID])
GO
ALTER TABLE [Transactions].[tblinventory_details] CHECK CONSTRAINT [FK_tblinventory_details_tblSubCategory]
GO
/****** Object:  StoredProcedure [Masters].[usp_Get_Category_SubCategory_Details]    Script Date: 12/1/2021 5:53:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--EXEC [Masters].[usp_Get_Category_SubCategory_Details]
CREATE PROCEDURE[Masters].[usp_Get_Category_SubCategory_Details]
@Type	NVARCHAR(100)=NULL
AS
BEGIN
    if(@Type='Get_inventory_details')
	BEGIN
		SELECT T.T_ID,MC.Cat_ID,Mc.Cat_Name ,Msc.SubCat_ID,Msc.SubCat_Name, T.Quantity,T.Unit_Price,T.Total_Price,T.Product_Description,T.Created_By,T.Created_Date FROM Masters.tblCategory Mc WITH(NOLOCK)
		INNER JOIN Masters.tblSubCategory Msc WITH(NOLOCK) ON Msc.MainCat_ID=Mc.Cat_ID
		INNER JOIN [Transactions].[tblinventory_details] T WITH(NOLOCK) ON T.SubCat_ID=Msc.SubCat_ID
		WHERE Mc.Active=1 AND Msc.Active=1 AND T.Active=1 ORDER BY T.T_ID DESC
	END
	else if(@Type='Get_category_details')
	BEGIN
		SELECT MC.Cat_ID,Mc.Cat_Name ,Msc.SubCat_ID,Msc.SubCat_Name FROM Masters.tblCategory Mc WITH(NOLOCK)
		INNER JOIN Masters.tblSubCategory Msc WITH(NOLOCK) ON Msc.MainCat_ID=Mc.Cat_ID
		WHERE Mc.Active=1 AND Msc.Active=1 
	END
END

GO
/****** Object:  StoredProcedure [Transactions].[usp_Insert_Inventory_Details]    Script Date: 12/1/2021 5:53:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--EXEC [Transactions].[usp_Insert_Inventory_Details] 0,1,2,1000,2000,'NA','Admin','I'
--EXEC [Transactions].[usp_Insert_Inventory_Details] 1,1,2,2000,4000,'NAModify','Admin','u'
--EXEC [Transactions].[usp_Insert_Inventory_Details] 1,1,2,1000,2000,'NA','Admin','D'
CREATE PROCEDURE [Transactions].[usp_Insert_Inventory_Details] 
	@T_ID					BIGINT=0,
	@SubCat_ID				INT = 1,
	@Quantity				INT = 0,
	@Unit_Price				Decimal(18,2)= 0,
	@Total_Price			Decimal(18,2)= 0,
	@Product_Description	NVARCHAR(100) = '',
	@Created_By				NVARCHAR(100) = '',
	@Type					NVARCHAR(10) =''
AS
BEGIN
	DECLARE @msg NVARCHAR(1000)=''
	IF(@Type='I')
	BEGIN
		INSERT INTO Transactions.tblinventory_details(SubCat_ID, Quantity, Unit_Price, Total_Price, Product_Description, Active, Created_By, Created_Date)
		VALUES(@SubCat_ID,@Quantity,@Unit_Price,@Total_Price,@Product_Description,1,@Created_By,GETDATE())
		SET @msg='Inventory record Inserted successfully.'
	END
	ELSE IF(@Type='U')
	BEGIN
		IF EXISTS(SELECT T_ID FROM [Transactions].[tblinventory_details] WHERE T_ID=@T_ID )
		BEGIN
			UPDATE Transactions.tblinventory_details 
			SET Quantity=@Quantity, 
			Unit_Price=@Unit_Price, 
			Total_Price=@Total_Price, 
			Product_Description=@Product_Description,
			Modify_By=@Created_By,
			Modify_Date=GETDATE()
			WHERE T_ID=@T_ID
			SET @msg='Inventory record Updated successfully.'
		END
		ELSE
		BEGIN
			SET @msg='Inventory record not exists in db! Unable to Update!!'
		END
	END
	ELSE IF(@Type='D')
	BEGIN
		IF EXISTS(SELECT T_ID FROM [Transactions].[tblinventory_details] WHERE T_ID=@T_ID )
		BEGIN
			UPDATE Transactions.tblinventory_details 
			SET Active=0,
			Modify_By=@Created_By,
			Modify_Date=GETDATE()
			WHERE T_ID=@T_ID
			SET @msg='Inventory record Deleted successfully.'
		END
		ELSE
		BEGIN
			SET @msg='Inventory record not exists in db! Unable to delete!!'
		END
	END
	SELECT @msg AS Msg 

END

GO
/****** Object:  StoredProcedure [UAM].[usp_Authenticate]    Script Date: 12/1/2021 5:53:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--EXEC[UAM].[usp_Authenticate]'pravin','pravina'
CREATE PROCEDURE [UAM].[usp_Authenticate]
	@Users_Name			NVARCHAR(100)=NULL, 
	@Passwords			NVARCHAR(100)=NULL
AS
BEGIN
	DECLARE @c INT=0
	DECLARE @msg NVARCHAR(1000)=''
	SET @c=(SELECT COUNT (Users_Name) FROM [UAM].[tblUsers] WHERE Users_Name=@Users_Name AND Passwords=@Passwords AND ACTIVE=1)
	IF(@c>0)
	BEGIN
		SET @msg='Successfully Login'
	END
	ELSE
	BEGIN
		SET @msg='Unable to Login! Please try after some time!!'
	END
	SELECT @msg AS Msg
END

GO
