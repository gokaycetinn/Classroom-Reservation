USE [master]
GO
/****** Object:  Database [ClassroomDb]    Script Date: 17.05.2025 03:03:07 ******/
CREATE DATABASE [ClassroomDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ClassroomDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ClassroomDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ClassroomDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ClassroomDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [ClassroomDb] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ClassroomDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ClassroomDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ClassroomDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ClassroomDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ClassroomDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ClassroomDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [ClassroomDb] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ClassroomDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ClassroomDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ClassroomDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ClassroomDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ClassroomDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ClassroomDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ClassroomDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ClassroomDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ClassroomDb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ClassroomDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ClassroomDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ClassroomDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ClassroomDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ClassroomDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ClassroomDb] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [ClassroomDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ClassroomDb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ClassroomDb] SET  MULTI_USER 
GO
ALTER DATABASE [ClassroomDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ClassroomDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ClassroomDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ClassroomDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ClassroomDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ClassroomDb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ClassroomDb] SET QUERY_STORE = ON
GO
ALTER DATABASE [ClassroomDb] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ClassroomDb]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 17.05.2025 03:03:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 17.05.2025 03:03:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 17.05.2025 03:03:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 17.05.2025 03:03:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 17.05.2025 03:03:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 17.05.2025 03:03:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 17.05.2025 03:03:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 17.05.2025 03:03:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedbacks]    Script Date: 17.05.2025 03:03:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedbacks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InstructorId] [int] NOT NULL,
	[Message] [nvarchar](max) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[Rating] [int] NOT NULL,
	[ClassroomId] [nvarchar](max) NULL,
 CONSTRAINT [PK_Feedbacks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Instructors]    Script Date: 17.05.2025 03:03:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Instructors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[Password] [nvarchar](512) NOT NULL,
 CONSTRAINT [PK_Instructors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reservations]    Script Date: 17.05.2025 03:03:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClassroomId] [nvarchar](max) NOT NULL,
	[ReservationDate] [datetime2](7) NOT NULL,
	[RequestDate] [datetime2](7) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
	[InstructorId] [int] NOT NULL,
	[SemesterName] [nvarchar](max) NOT NULL,
	[InstructorName] [nvarchar](max) NOT NULL,
	[StartTime] [time](7) NULL,
	[EndTime] [time](7) NULL,
 CONSTRAINT [PK_Reservations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Semesters]    Script Date: 17.05.2025 03:03:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Semesters](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Year] [int] NOT NULL,
	[Season] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Semesters] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250514214850_InitialMigration', N'9.0.5')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'd1ca8cbc-86b1-4e1a-87f0-4ca180b67ed2', N'Instructor', N'INSTRUCTOR', NULL)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'f6d5c610-7859-44f9-9776-bcb12fc7827a', N'Admin', N'ADMIN', NULL)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a487c334-4ad4-4ee6-b4eb-d90be7a2d35d', N'd1ca8cbc-86b1-4e1a-87f0-4ca180b67ed2')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd5e6d741-31c6-4b25-8b18-8bb10311f45f', N'd1ca8cbc-86b1-4e1a-87f0-4ca180b67ed2')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'88df215c-4b6c-4dff-b681-c86e0f88ebbb', N'f6d5c610-7859-44f9-9776-bcb12fc7827a')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'88df215c-4b6c-4dff-b681-c86e0f88ebbb', N'admin', N'ADMIN', N'admin@classroom.com', N'ADMIN@CLASSROOM.COM', 1, N'AQAAAAIAAYagAAAAECB0Y4t9Pw8Awy99oRabvlRfbEa62DZbNUAyoWy0+SGsAqE+kL1GVDOmJ4MXMgB8JA==', N'HVTXJKLPR2ZG7ENST2GGAGWSGFTPKBPI', N'82fc1eb1-9ec8-44a0-ac7d-811075c86d85', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'a487c334-4ad4-4ee6-b4eb-d90be7a2d35d', N'gokaycetin44@gmail.com', N'GOKAYCETIN44@GMAIL.COM', N'gokaycetin44@gmail.com', N'GOKAYCETIN44@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEMqHjF7a7UOJRYOzYpxPb4tCq/oapmziUJHXZFq23KeL9o6zGjbVQManLeimbsFlYw==', N'F7LO3ZF64HOFLHDCY36NKTVEEHAO3V6K', N'4dfe4266-d1d4-45a5-b5a2-784a31ac27e8', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'd5e6d741-31c6-4b25-8b18-8bb10311f45f', N'ayse@gmail.com', N'AYSE@GMAIL.COM', N'ayse@gmail.com', N'AYSE@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEGoC3cRR8bt3M+J0zAbjN1EDPE515lZ/SDXXH2nuKqsmTLO8cAyDWoFRpmju0GzZcg==', N'S6VFUKUQMT5643QSOVYXQIB3NSMY3AWM', N'1d87472b-6c69-4338-926b-fb93ac504e2b', NULL, 0, 0, NULL, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[Feedbacks] ON 

INSERT [dbo].[Feedbacks] ([Id], [InstructorId], [Message], [CreatedDate], [Rating], [ClassroomId]) VALUES (26, 4, N'--', CAST(N'2025-05-15T23:54:25.3425177' AS DateTime2), 4, N'A')
INSERT [dbo].[Feedbacks] ([Id], [InstructorId], [Message], [CreatedDate], [Rating], [ClassroomId]) VALUES (27, 4, N'--', CAST(N'2025-05-15T23:54:40.4352541' AS DateTime2), 5, N'C')
INSERT [dbo].[Feedbacks] ([Id], [InstructorId], [Message], [CreatedDate], [Rating], [ClassroomId]) VALUES (28, 4, N'--', CAST(N'2025-05-15T23:54:53.4565340' AS DateTime2), 5, N'E')
INSERT [dbo].[Feedbacks] ([Id], [InstructorId], [Message], [CreatedDate], [Rating], [ClassroomId]) VALUES (29, 4, N'-', CAST(N'2025-05-15T23:55:01.2203968' AS DateTime2), 1, N'D')
INSERT [dbo].[Feedbacks] ([Id], [InstructorId], [Message], [CreatedDate], [Rating], [ClassroomId]) VALUES (30, 4, N'Güzel', CAST(N'2025-05-15T23:55:16.0875688' AS DateTime2), 5, N'B')
INSERT [dbo].[Feedbacks] ([Id], [InstructorId], [Message], [CreatedDate], [Rating], [ClassroomId]) VALUES (31, 4, N'İyi', CAST(N'2025-05-15T23:55:25.9250920' AS DateTime2), 5, N'E')
INSERT [dbo].[Feedbacks] ([Id], [InstructorId], [Message], [CreatedDate], [Rating], [ClassroomId]) VALUES (32, 4, N'Fena değil.', CAST(N'2025-05-15T23:55:37.6383699' AS DateTime2), 3, N'C')
INSERT [dbo].[Feedbacks] ([Id], [InstructorId], [Message], [CreatedDate], [Rating], [ClassroomId]) VALUES (33, 4, N'Kötü', CAST(N'2025-05-15T23:55:48.2255757' AS DateTime2), 2, N'D')
INSERT [dbo].[Feedbacks] ([Id], [InstructorId], [Message], [CreatedDate], [Rating], [ClassroomId]) VALUES (35, 4, N'Çok iyi', CAST(N'2025-05-15T23:56:21.3545527' AS DateTime2), 5, N'B')
INSERT [dbo].[Feedbacks] ([Id], [InstructorId], [Message], [CreatedDate], [Rating], [ClassroomId]) VALUES (37, 4, N'güzel sınıf', CAST(N'2025-05-17T02:20:18.0571987' AS DateTime2), 5, N'B')
INSERT [dbo].[Feedbacks] ([Id], [InstructorId], [Message], [CreatedDate], [Rating], [ClassroomId]) VALUES (38, 4, N'meraba', CAST(N'2025-05-17T02:38:39.9303994' AS DateTime2), 4, N'B')
INSERT [dbo].[Feedbacks] ([Id], [InstructorId], [Message], [CreatedDate], [Rating], [ClassroomId]) VALUES (39, 4, N'message', CAST(N'2025-05-17T02:52:31.1085058' AS DateTime2), 4, N'B')
SET IDENTITY_INSERT [dbo].[Feedbacks] OFF
GO
SET IDENTITY_INSERT [dbo].[Instructors] ON 

INSERT [dbo].[Instructors] ([Id], [Name], [Email], [Password]) VALUES (4, N'Gökay', N'gokaycetin44@gmail.com', N'AQAAAAIAAYagAAAAEMqHjF7a7UOJRYOzYpxPb4tCq/oapmziUJHXZFq23KeL9o6zGjbVQManLeimbsFlYw==')
INSERT [dbo].[Instructors] ([Id], [Name], [Email], [Password]) VALUES (6, N'aysenur', N'ayse@gmail.com', N'AQAAAAIAAYagAAAAEGoC3cRR8bt3M+J0zAbjN1EDPE515lZ/SDXXH2nuKqsmTLO8cAyDWoFRpmju0GzZcg==')
SET IDENTITY_INSERT [dbo].[Instructors] OFF
GO
SET IDENTITY_INSERT [dbo].[Reservations] ON 

INSERT [dbo].[Reservations] ([Id], [ClassroomId], [ReservationDate], [RequestDate], [Status], [InstructorId], [SemesterName], [InstructorName], [StartTime], [EndTime]) VALUES (21, N'B', CAST(N'2025-05-17T11:47:00.0000000' AS DateTime2), CAST(N'2025-05-17T01:44:15.3576479' AS DateTime2), N'Approved', 4, N'2025 - Spring', N'Gökay', CAST(N'11:47:00' AS Time), CAST(N'14:45:00' AS Time))
INSERT [dbo].[Reservations] ([Id], [ClassroomId], [ReservationDate], [RequestDate], [Status], [InstructorId], [SemesterName], [InstructorName], [StartTime], [EndTime]) VALUES (22, N'E', CAST(N'2025-05-21T09:00:00.0000000' AS DateTime2), CAST(N'2025-05-17T01:46:22.0057651' AS DateTime2), N'Approved', 4, N'2025 - Spring', N'Gökay', CAST(N'09:00:00' AS Time), CAST(N'11:20:00' AS Time))
INSERT [dbo].[Reservations] ([Id], [ClassroomId], [ReservationDate], [RequestDate], [Status], [InstructorId], [SemesterName], [InstructorName], [StartTime], [EndTime]) VALUES (23, N'C', CAST(N'2025-05-22T11:52:00.0000000' AS DateTime2), CAST(N'2025-05-17T01:48:41.4246790' AS DateTime2), N'Rejected', 4, N'2025 - Spring', N'Gökay', CAST(N'11:52:00' AS Time), CAST(N'15:00:00' AS Time))
INSERT [dbo].[Reservations] ([Id], [ClassroomId], [ReservationDate], [RequestDate], [Status], [InstructorId], [SemesterName], [InstructorName], [StartTime], [EndTime]) VALUES (24, N'C', CAST(N'2025-05-20T15:50:00.0000000' AS DateTime2), CAST(N'2025-05-17T01:49:23.8524024' AS DateTime2), N'Pending', 6, N'2025 - Spring', N'ayse', CAST(N'15:50:00' AS Time), CAST(N'18:49:00' AS Time))
INSERT [dbo].[Reservations] ([Id], [ClassroomId], [ReservationDate], [RequestDate], [Status], [InstructorId], [SemesterName], [InstructorName], [StartTime], [EndTime]) VALUES (25, N'D', CAST(N'2025-05-20T10:52:00.0000000' AS DateTime2), CAST(N'2025-05-17T01:49:43.9681882' AS DateTime2), N'Approved', 6, N'2025 - Spring', N'ayse', CAST(N'10:52:00' AS Time), CAST(N'13:49:00' AS Time))
INSERT [dbo].[Reservations] ([Id], [ClassroomId], [ReservationDate], [RequestDate], [Status], [InstructorId], [SemesterName], [InstructorName], [StartTime], [EndTime]) VALUES (26, N'A', CAST(N'2025-05-18T10:50:00.0000000' AS DateTime2), CAST(N'2025-05-17T01:50:40.8488502' AS DateTime2), N'Approved', 5, N'2025 - Spring', N'Gokay2', CAST(N'10:50:00' AS Time), CAST(N'12:10:00' AS Time))
INSERT [dbo].[Reservations] ([Id], [ClassroomId], [ReservationDate], [RequestDate], [Status], [InstructorId], [SemesterName], [InstructorName], [StartTime], [EndTime]) VALUES (27, N'C', CAST(N'2025-05-20T16:30:00.0000000' AS DateTime2), CAST(N'2025-05-17T01:51:54.4264446' AS DateTime2), N'Pending', 5, N'2025 - Spring', N'Gokay2', CAST(N'16:30:00' AS Time), CAST(N'17:50:00' AS Time))
INSERT [dbo].[Reservations] ([Id], [ClassroomId], [ReservationDate], [RequestDate], [Status], [InstructorId], [SemesterName], [InstructorName], [StartTime], [EndTime]) VALUES (28, N'C', CAST(N'2025-05-30T10:00:00.0000000' AS DateTime2), CAST(N'2025-05-17T01:57:13.5275418' AS DateTime2), N'Pending', 4, N'2025 - Spring', N'Gökay', CAST(N'10:00:00' AS Time), CAST(N'12:15:00' AS Time))
INSERT [dbo].[Reservations] ([Id], [ClassroomId], [ReservationDate], [RequestDate], [Status], [InstructorId], [SemesterName], [InstructorName], [StartTime], [EndTime]) VALUES (31, N'B', CAST(N'2025-05-19T19:46:00.0000000' AS DateTime2), CAST(N'2025-05-17T02:44:57.4016635' AS DateTime2), N'Pending', 4, N'2025 - Spring', N'Gökay', CAST(N'19:46:00' AS Time), CAST(N'20:47:00' AS Time))
INSERT [dbo].[Reservations] ([Id], [ClassroomId], [ReservationDate], [RequestDate], [Status], [InstructorId], [SemesterName], [InstructorName], [StartTime], [EndTime]) VALUES (32, N'C', CAST(N'2025-05-25T03:52:00.0000000' AS DateTime2), CAST(N'2025-05-17T02:51:40.4518391' AS DateTime2), N'Pending', 4, N'2025 - Spring', N'Gökay', CAST(N'03:52:00' AS Time), CAST(N'04:53:00' AS Time))
SET IDENTITY_INSERT [dbo].[Reservations] OFF
GO
SET IDENTITY_INSERT [dbo].[Semesters] ON 

INSERT [dbo].[Semesters] ([Id], [Year], [Season]) VALUES (1, 2025, N'Spring')
INSERT [dbo].[Semesters] ([Id], [Year], [Season]) VALUES (2, 2026, N'Fall')
SET IDENTITY_INSERT [dbo].[Semesters] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 17.05.2025 03:03:08 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 17.05.2025 03:03:08 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 17.05.2025 03:03:08 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 17.05.2025 03:03:08 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 17.05.2025 03:03:08 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 17.05.2025 03:03:08 ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 17.05.2025 03:03:08 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Feedbacks_InstructorId]    Script Date: 17.05.2025 03:03:08 ******/
CREATE NONCLUSTERED INDEX [IX_Feedbacks_InstructorId] ON [dbo].[Feedbacks]
(
	[InstructorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Feedbacks]  WITH CHECK ADD  CONSTRAINT [FK_Feedbacks_Instructors_InstructorId] FOREIGN KEY([InstructorId])
REFERENCES [dbo].[Instructors] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Feedbacks] CHECK CONSTRAINT [FK_Feedbacks_Instructors_InstructorId]
GO
USE [master]
GO
ALTER DATABASE [ClassroomDb] SET  READ_WRITE 
GO
