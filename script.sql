USE [master]
GO
/****** Object:  Database [PetRescueDb]    Script Date: 10/16/2024 8:02:43 PM ******/
CREATE DATABASE [PetRescueDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PetRescueDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.DUY_THUAN\MSSQL\DATA\PetRescueDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PetRescueDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.DUY_THUAN\MSSQL\DATA\PetRescueDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [PetRescueDb] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PetRescueDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PetRescueDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PetRescueDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PetRescueDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PetRescueDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PetRescueDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [PetRescueDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PetRescueDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PetRescueDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PetRescueDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PetRescueDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PetRescueDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PetRescueDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PetRescueDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PetRescueDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PetRescueDb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [PetRescueDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PetRescueDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PetRescueDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PetRescueDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PetRescueDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PetRescueDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PetRescueDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PetRescueDb] SET RECOVERY FULL 
GO
ALTER DATABASE [PetRescueDb] SET  MULTI_USER 
GO
ALTER DATABASE [PetRescueDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PetRescueDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PetRescueDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PetRescueDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PetRescueDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PetRescueDb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'PetRescueDb', N'ON'
GO
ALTER DATABASE [PetRescueDb] SET QUERY_STORE = ON
GO
ALTER DATABASE [PetRescueDb] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [PetRescueDb]
GO
/****** Object:  Table [dbo].[AdoptionApplication]    Script Date: 10/16/2024 8:02:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdoptionApplication](
	[ApplicationID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[PetID] [uniqueidentifier] NOT NULL,
	[RequestDate] [datetime] NOT NULL,
	[Status] [nvarchar](255) NOT NULL,
	[Notes] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ApplicationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Donation]    Script Date: 10/16/2024 8:02:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Donation](
	[DonationID] [uniqueidentifier] NOT NULL,
	[EventID] [uniqueidentifier] NULL,
	[ShelterID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[Amount] [decimal](10, 2) NOT NULL,
	[DonationDate] [datetime] NOT NULL,
	[PaymentMethod] [nvarchar](50) NULL,
	[Notes] [nvarchar](255) NULL,
	[Status] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DonationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 10/16/2024 8:02:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event](
	[EventID] [uniqueidentifier] NOT NULL,
	[ShelterID] [uniqueidentifier] NULL,
	[ImageURL] [nvarchar](255) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[StartDateTime] [datetime] NOT NULL,
	[EndDateTime] [datetime] NULL,
	[Location] [nvarchar](255) NULL,
	[Status] [nvarchar](255) NOT NULL,
	[EventType] [nvarchar](255) NOT NULL,
	[Goal] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[EventID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pet]    Script Date: 10/16/2024 8:02:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pet](
	[PetID] [uniqueidentifier] NOT NULL,
	[ShelterID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Age] [int] NULL,
	[Gender] [nvarchar](10) NULL,
	[Description] [nvarchar](255) NULL,
	[Species] [nvarchar](255) NOT NULL,
	[Status] [nvarchar](20) NULL,
	[ArrivalDate] [date] NULL,
	[PhotoURL] [nvarchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[PetID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 10/16/2024 8:02:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleID] [uniqueidentifier] NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Shelter]    Script Date: 10/16/2024 8:02:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shelter](
	[ShelterID] [uniqueidentifier] NOT NULL,
	[ShelterName] [nvarchar](255) NOT NULL,
	[ShelterAddress] [nvarchar](255) NOT NULL,
	[ShelterPhoneNumber] [nvarchar](255) NOT NULL,
	[Balance] [decimal](18, 2) NOT NULL,
	[UsersID] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ShelterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 10/16/2024 8:02:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](255) NULL,
	[LastName] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NOT NULL,
	[PhoneNumber] [nvarchar](255) NULL,
	[Address] [nvarchar](255) NULL,
	[PasswordHash] [nvarchar](255) NOT NULL,
	[RoleID] [uniqueidentifier] NOT NULL,
	[Status] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Role] ([RoleID], [RoleName]) VALUES (N'd290f1ee-6c54-4b01-90e6-d701748f0851', N'Administrator')
INSERT [dbo].[Role] ([RoleID], [RoleName]) VALUES (N'f3c8d4e5-6b7a-4c9d-8e2f-0a1b2c3d4e5f', N'ShelterOwner')
INSERT [dbo].[Role] ([RoleID], [RoleName]) VALUES (N'e7b8f3d2-4a2f-4c3b-8f4d-9c5d8a3e1b2c', N'User')
GO
INSERT [dbo].[User] ([UserID], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [PasswordHash], [RoleID], [Status]) VALUES (N'3f21226b-30c1-4274-81a6-2ed9d9e0c54c', N'Pham Cao Duy', N'Thuan', N'phamcaoduythuan@gmail.com', N'123456789', N'Nowhere', N'LHm9kSn0tm+1Zvnjw3fXI4+HE0Cw6l0dGHS72+itnAGTuSV8', N'e7b8f3d2-4a2f-4c3b-8f4d-9c5d8a3e1b2c', N'Active')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [role_rolename_unique]    Script Date: 10/16/2024 8:02:44 PM ******/
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [role_rolename_unique] UNIQUE NONCLUSTERED 
(
	[RoleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [user_email_unique]    Script Date: 10/16/2024 8:02:44 PM ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [user_email_unique] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AdoptionApplication] ADD  DEFAULT (newid()) FOR [ApplicationID]
GO
ALTER TABLE [dbo].[AdoptionApplication] ADD  DEFAULT (getdate()) FOR [RequestDate]
GO
ALTER TABLE [dbo].[Donation] ADD  DEFAULT (newid()) FOR [DonationID]
GO
ALTER TABLE [dbo].[Donation] ADD  DEFAULT (getdate()) FOR [DonationDate]
GO
ALTER TABLE [dbo].[Event] ADD  DEFAULT (newid()) FOR [EventID]
GO
ALTER TABLE [dbo].[Pet] ADD  DEFAULT (newid()) FOR [PetID]
GO
ALTER TABLE [dbo].[Role] ADD  DEFAULT (newid()) FOR [RoleID]
GO
ALTER TABLE [dbo].[Shelter] ADD  DEFAULT (newid()) FOR [ShelterID]
GO
ALTER TABLE [dbo].[Shelter] ADD  DEFAULT ((0.00)) FOR [Balance]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT (newid()) FOR [UserID]
GO
ALTER TABLE [dbo].[AdoptionApplication]  WITH CHECK ADD  CONSTRAINT [adoptionapplication_petid_foreign] FOREIGN KEY([PetID])
REFERENCES [dbo].[Pet] ([PetID])
GO
ALTER TABLE [dbo].[AdoptionApplication] CHECK CONSTRAINT [adoptionapplication_petid_foreign]
GO
ALTER TABLE [dbo].[AdoptionApplication]  WITH CHECK ADD  CONSTRAINT [adoptionapplication_userid_foreign] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[AdoptionApplication] CHECK CONSTRAINT [adoptionapplication_userid_foreign]
GO
ALTER TABLE [dbo].[Donation]  WITH CHECK ADD  CONSTRAINT [donation_eventid_foreign] FOREIGN KEY([EventID])
REFERENCES [dbo].[Event] ([EventID])
GO
ALTER TABLE [dbo].[Donation] CHECK CONSTRAINT [donation_eventid_foreign]
GO
ALTER TABLE [dbo].[Donation]  WITH CHECK ADD  CONSTRAINT [donation_shelterid_foreign] FOREIGN KEY([ShelterID])
REFERENCES [dbo].[Shelter] ([ShelterID])
GO
ALTER TABLE [dbo].[Donation] CHECK CONSTRAINT [donation_shelterid_foreign]
GO
ALTER TABLE [dbo].[Donation]  WITH CHECK ADD  CONSTRAINT [donation_userid_foreign] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Donation] CHECK CONSTRAINT [donation_userid_foreign]
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [event_shelterid_foreign] FOREIGN KEY([ShelterID])
REFERENCES [dbo].[Shelter] ([ShelterID])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [event_shelterid_foreign]
GO
ALTER TABLE [dbo].[Pet]  WITH CHECK ADD  CONSTRAINT [pet_shelterid_foreign] FOREIGN KEY([ShelterID])
REFERENCES [dbo].[Shelter] ([ShelterID])
GO
ALTER TABLE [dbo].[Pet] CHECK CONSTRAINT [pet_shelterid_foreign]
GO
ALTER TABLE [dbo].[Shelter]  WITH CHECK ADD  CONSTRAINT [shelter_usersid_foreign] FOREIGN KEY([UsersID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Shelter] CHECK CONSTRAINT [shelter_usersid_foreign]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [user_roleid_foreign] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([RoleID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [user_roleid_foreign]
GO
USE [master]
GO
ALTER DATABASE [PetRescueDb] SET  READ_WRITE 
GO
