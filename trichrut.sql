USE [master]
GO
/****** Object:  Database [DoAnTSLongv1]    Script Date: 11/25/2017 11:33:57 AM ******/
CREATE DATABASE [DoAnTSLongv1]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DoAnTSLongv1', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\DoAnTSLongv1.mdf' , SIZE = 35840KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DoAnTSLongv1_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\DoAnTSLongv1_log.ldf' , SIZE = 11200KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DoAnTSLongv1] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DoAnTSLongv1].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DoAnTSLongv1] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DoAnTSLongv1] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DoAnTSLongv1] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DoAnTSLongv1] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DoAnTSLongv1] SET ARITHABORT OFF 
GO
ALTER DATABASE [DoAnTSLongv1] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DoAnTSLongv1] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DoAnTSLongv1] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DoAnTSLongv1] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DoAnTSLongv1] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DoAnTSLongv1] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DoAnTSLongv1] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DoAnTSLongv1] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DoAnTSLongv1] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DoAnTSLongv1] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DoAnTSLongv1] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DoAnTSLongv1] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DoAnTSLongv1] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DoAnTSLongv1] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DoAnTSLongv1] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DoAnTSLongv1] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DoAnTSLongv1] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DoAnTSLongv1] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DoAnTSLongv1] SET  MULTI_USER 
GO
ALTER DATABASE [DoAnTSLongv1] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DoAnTSLongv1] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DoAnTSLongv1] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DoAnTSLongv1] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [DoAnTSLongv1] SET DELAYED_DURABILITY = DISABLED 
GO
USE [DoAnTSLongv1]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 11/25/2017 11:34:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Description] [ntext] NULL,
	[CategoryParentId] [int] NOT NULL,
	[Location] [int] NULL,
	[Type] [int] NULL,
	[IsHome] [int] NULL,
	[Images] [nvarchar](max) NULL,
	[Url] [nvarchar](max) NULL,
	[DatePublish] [datetime] NULL,
	[DateUpdate] [datetime] NULL,
	[UserId] [int] NULL,
	[Publish] [int] NULL,
	[MetaTitle] [nvarchar](200) NULL,
	[MetaKeyword] [nvarchar](500) NULL,
	[MetaDescrption] [nvarchar](300) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Comments]    Script Date: 11/25/2017 11:34:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[Id] [nchar](15) NOT NULL,
	[GroupCommentId] [nchar](15) NULL,
	[Name] [nvarchar](500) NULL,
	[Rating] [decimal](18, 1) NULL,
	[Comment] [nvarchar](max) NULL,
	[Date] [nvarchar](50) NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Contact]    Script Date: 11/25/2017 11:34:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[Id] [nchar](15) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Email] [nvarchar](200) NOT NULL,
	[Contents] [nvarchar](200) NOT NULL,
	[DateSend] [datetime] NOT NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CoreWord]    Script Date: 11/25/2017 11:34:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CoreWord](
	[id] [nchar](15) NOT NULL,
	[aspect] [nvarchar](50) NULL,
	[core_word] [nvarchar](50) NULL,
 CONSTRAINT [PK_CoreWord] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CountKeyword]    Script Date: 11/25/2017 11:34:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CountKeyword](
	[Id] [nchar](15) NOT NULL,
	[KeyWordId] [nchar](15) NULL,
	[GroupCommentId] [nchar](15) NULL,
	[Count] [int] NULL,
 CONSTRAINT [PK_CountKeyword] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EducationJobs]    Script Date: 11/25/2017 11:34:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EducationJobs](
	[Id] [nchar](15) NOT NULL,
	[Title] [nvarchar](500) NULL,
	[Date] [nvarchar](200) NULL,
	[Contents] [nvarchar](500) NULL,
	[UserId] [int] NULL,
	[Publish] [bit] NULL,
	[Type] [int] NULL,
 CONSTRAINT [PK_EducationJobs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GroupComents]    Script Date: 11/25/2017 11:34:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupComents](
	[Id] [nchar](15) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[ProductId] [nchar](15) NULL,
	[Note] [nvarchar](max) NULL,
 CONSTRAINT [PK_GroupComents] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GroupWordComment]    Script Date: 11/25/2017 11:34:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupWordComment](
	[Id] [nchar](15) NOT NULL,
	[GroupWordId] [nchar](15) NULL,
	[CommentId] [nchar](15) NULL,
	[Score] [float] NOT NULL,
 CONSTRAINT [PK_GroupWordComment_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GroupWords]    Script Date: 11/25/2017 11:34:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupWords](
	[Id] [nchar](15) NOT NULL,
	[Word] [nvarchar](50) NULL,
	[C1] [int] NULL,
	[C2] [int] NULL,
	[C3] [int] NULL,
	[C4] [int] NULL,
	[C5] [int] NULL,
	[Total] [int] NULL,
	[ProductId] [nchar](15) NULL,
 CONSTRAINT [PK_GroupWords] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HobbiesInterests]    Script Date: 11/25/2017 11:34:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HobbiesInterests](
	[Id] [nchar](15) NOT NULL,
	[Title] [nvarchar](500) NULL,
	[Icons] [nvarchar](200) NULL,
	[UserId] [int] NULL,
	[Publish] [bit] NULL,
	[Type] [int] NULL,
 CONSTRAINT [PK_HobbiesInterests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[KeyWords]    Script Date: 11/25/2017 11:34:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KeyWords](
	[Id] [nchar](15) NOT NULL,
	[Word] [nvarchar](50) NULL,
	[GroupWordId] [nchar](15) NULL,
	[GroupCommentId] [nchar](15) NULL,
	[Type] [int] NULL,
	[Score] [decimal](18, 1) NULL,
	[Logs] [nvarchar](500) NULL,
	[C1] [int] NULL,
	[C2] [int] NULL,
	[C3] [int] NULL,
	[C4] [int] NULL,
	[C5] [int] NULL,
	[P1] [float] NULL,
	[P2] [float] NULL,
	[P3] [float] NULL,
	[P4] [float] NULL,
	[P5] [float] NULL,
	[Total] [int] NULL,
	[TypeWord] [int] NULL,
 CONSTRAINT [PK_TopWord] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[KeywordsCount]    Script Date: 11/25/2017 11:34:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KeywordsCount](
	[GroupKeyWordId] [nchar](15) NOT NULL,
	[KeyWord] [nchar](15) NOT NULL,
	[C1] [int] NULL,
	[C2] [int] NULL,
	[C3] [int] NULL,
	[C4] [int] NULL,
	[C5] [int] NULL,
	[P1] [float] NULL,
	[P2] [float] NULL,
	[P3] [float] NULL,
	[P4] [float] NULL,
	[P5] [float] NULL,
	[Total] [int] NULL,
	[GroupCommentId] [nchar](15) NULL,
 CONSTRAINT [PK_KeywordsCount] PRIMARY KEY CLUSTERED 
(
	[GroupKeyWordId] ASC,
	[KeyWord] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LogAddKeyWord]    Script Date: 11/25/2017 11:34:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogAddKeyWord](
	[Id] [nchar](15) NOT NULL,
	[GroupWordId] [nchar](15) NULL,
	[Words] [nvarchar](50) NULL,
	[Logs] [nvarchar](max) NULL,
	[GroupCommentId] [nchar](15) NULL,
 CONSTRAINT [PK_LogAddKeyWord] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LogLabel]    Script Date: 11/25/2017 11:34:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogLabel](
	[Id] [nchar](15) NOT NULL,
	[Steps] [nvarchar](50) NULL,
	[ReviewContent] [nvarchar](max) NULL,
	[LogCounts] [nvarchar](max) NULL,
	[GroupKeywords] [nvarchar](max) NULL,
	[GroupCommentId] [nchar](15) NULL,
 CONSTRAINT [PK_LogLabel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LogVocabulary]    Script Date: 11/25/2017 11:34:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogVocabulary](
	[Id] [nchar](15) NOT NULL,
	[CommentId] [nchar](15) NULL,
	[CreateAt] [datetime] NULL,
	[Logs] [nvarchar](max) NULL,
 CONSTRAINT [PK_LogVocabulary] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MailServer]    Script Date: 11/25/2017 11:34:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MailServer](
	[ID] [int] NOT NULL,
	[Host] [nvarchar](max) NOT NULL,
	[SenderMail] [nvarchar](max) NOT NULL,
	[EnableSSL] [bit] NOT NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[DisplayName] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Port] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_MailServer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Menu]    Script Date: 11/25/2017 11:34:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[Id] [nchar](15) NOT NULL,
	[CategoryId] [int] NULL,
	[TypeMenuId] [nchar](15) NULL,
	[Location] [int] NULL,
	[MenuParent] [nchar](15) NULL,
	[DatePublish] [datetime] NULL,
	[DateUpdate] [datetime] NULL,
	[UserId] [int] NULL,
	[Publish] [int] NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MySpecialities]    Script Date: 11/25/2017 11:34:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MySpecialities](
	[Id] [nchar](15) NOT NULL,
	[Title] [nvarchar](500) NULL,
	[Contents] [nvarchar](500) NULL,
	[Icon] [nvarchar](500) NULL,
	[UserId] [int] NULL,
	[Publish] [bit] NULL,
 CONSTRAINT [PK_MySpecialities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products]    Script Date: 11/25/2017 11:34:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [nchar](15) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Publish] [int] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Role]    Script Date: 11/25/2017 11:34:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [nchar](15) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RoleFunction]    Script Date: 11/25/2017 11:34:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleFunction](
	[Id] [nchar](15) NOT NULL,
	[RoleId] [nchar](15) NULL,
	[CategoryId] [int] NULL,
	[Location] [int] NULL,
	[FunctionParent] [nchar](15) NULL,
	[DatePublish] [datetime] NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_RoleFunction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RoleUsers]    Script Date: 11/25/2017 11:34:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleUsers](
	[Role_Id] [nchar](15) NOT NULL,
	[User_Id] [int] NOT NULL,
	[DatePublish] [datetime] NULL,
	[UserCreate] [int] NULL,
 CONSTRAINT [PK_RoleUsers] PRIMARY KEY CLUSTERED 
(
	[Role_Id] ASC,
	[User_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SeKeyWord]    Script Date: 11/25/2017 11:34:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SeKeyWord](
	[Id] [nchar](15) NOT NULL,
	[SeId] [nchar](15) NULL,
	[KeyWordId] [nchar](15) NULL,
	[CountNumber] [nvarchar](500) NULL,
 CONSTRAINT [PK_SeKeyWord] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Sentenses]    Script Date: 11/25/2017 11:34:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sentenses](
	[Id] [nchar](15) NOT NULL,
	[ContentReview] [nvarchar](max) NULL,
	[CommentId] [nchar](15) NULL,
	[Logs] [nvarchar](max) NULL,
 CONSTRAINT [PK_Se] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Sentensesnotword]    Script Date: 11/25/2017 11:34:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sentensesnotword](
	[Id] [nchar](15) NOT NULL,
	[ContentReview] [nvarchar](max) NULL,
	[CommentId] [nchar](15) NULL,
	[Logs] [nvarchar](max) NULL,
 CONSTRAINT [PK_Sentensesnotword] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SkillsAbilities]    Script Date: 11/25/2017 11:34:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SkillsAbilities](
	[Id] [nchar](15) NOT NULL,
	[Title] [nvarchar](500) NULL,
	[Percents] [nvarchar](500) NULL,
	[Icon] [nvarchar](200) NULL,
	[UserId] [int] NULL,
	[Publish] [bit] NULL,
	[Type] [int] NULL,
 CONSTRAINT [PK_SkillsAbilities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StopWords]    Script Date: 11/25/2017 11:34:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StopWords](
	[Id] [nchar](15) NOT NULL,
	[StopWord] [nvarchar](50) NULL,
 CONSTRAINT [PK_StopWord] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TypeMenu]    Script Date: 11/25/2017 11:34:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeMenu](
	[Id] [nchar](15) NOT NULL,
	[Name] [nvarchar](500) NULL,
	[Publich] [int] NULL,
 CONSTRAINT [PK_TypeMenu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 11/25/2017 11:34:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](200) NOT NULL,
	[PasswordHash] [nvarchar](200) NOT NULL,
	[FullName] [nvarchar](max) NULL,
	[Email] [nvarchar](200) NOT NULL,
	[RoleId] [nchar](15) NULL,
	[EmailConfirmaed] [bit] NULL,
	[Sex] [nchar](10) NULL,
	[Birthday] [datetime] NOT NULL,
	[Address] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](15) NULL,
	[Jobs] [nvarchar](max) NULL,
	[Attactment] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[Discriminator] [nvarchar](max) NULL,
	[Cover] [nvarchar](max) NULL,
	[Avata] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NULL,
	[LockoutEndDateUtc] [datetime] NOT NULL,
	[LockoutEnabled] [bit] NULL,
	[Lock] [bit] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[VectorWord]    Script Date: 11/25/2017 11:34:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VectorWord](
	[id] [nchar](15) NOT NULL,
	[word] [nvarchar](50) NULL,
	[vector] [text] NULL,
	[idaspect] [nchar](15) NULL,
 CONSTRAINT [PK_VectorWord] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Vocabulary]    Script Date: 11/25/2017 11:34:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vocabulary](
	[Id] [nchar](15) NOT NULL,
	[Word] [nvarchar](50) NULL,
	[TypeWord] [nvarchar](4) NULL,
	[Counts] [int] NULL,
	[GroupCommentId] [nchar](15) NULL,
	[Type] [int] NULL,
 CONSTRAINT [PK_Vocavu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Vocabulary_1]    Script Date: 11/25/2017 11:34:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vocabulary_1](
	[Id] [nchar](15) NOT NULL,
	[Word] [nvarchar](50) NULL,
	[TypeWord] [nvarchar](4) NULL,
	[Counts] [int] NULL,
	[GroupCommentId] [nchar](15) NULL,
	[Type] [int] NULL,
 CONSTRAINT [PK_Vocabulary_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Category]  WITH CHECK ADD  CONSTRAINT [FK_Category_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK_Category_User]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_GroupComents] FOREIGN KEY([GroupCommentId])
REFERENCES [dbo].[GroupComents] ([Id])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_GroupComents]
GO
ALTER TABLE [dbo].[Contact]  WITH CHECK ADD  CONSTRAINT [FK_Contact_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [FK_Contact_User]
GO
ALTER TABLE [dbo].[CountKeyword]  WITH CHECK ADD  CONSTRAINT [FK_CountKeyword_KeyWords] FOREIGN KEY([KeyWordId])
REFERENCES [dbo].[KeyWords] ([Id])
GO
ALTER TABLE [dbo].[CountKeyword] CHECK CONSTRAINT [FK_CountKeyword_KeyWords]
GO
ALTER TABLE [dbo].[EducationJobs]  WITH CHECK ADD  CONSTRAINT [FK_EducationJobs_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[EducationJobs] CHECK CONSTRAINT [FK_EducationJobs_User]
GO
ALTER TABLE [dbo].[GroupComents]  WITH CHECK ADD  CONSTRAINT [FK_GroupComents_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[GroupComents] CHECK CONSTRAINT [FK_GroupComents_Products]
GO
ALTER TABLE [dbo].[GroupWordComment]  WITH CHECK ADD  CONSTRAINT [FK_GroupWordComment_GroupWords] FOREIGN KEY([GroupWordId])
REFERENCES [dbo].[GroupWords] ([Id])
GO
ALTER TABLE [dbo].[GroupWordComment] CHECK CONSTRAINT [FK_GroupWordComment_GroupWords]
GO
ALTER TABLE [dbo].[HobbiesInterests]  WITH CHECK ADD  CONSTRAINT [FK_HobbiesInterests_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[HobbiesInterests] CHECK CONSTRAINT [FK_HobbiesInterests_User]
GO
ALTER TABLE [dbo].[KeyWords]  WITH CHECK ADD  CONSTRAINT [FK_TopWords_GroupWords] FOREIGN KEY([GroupWordId])
REFERENCES [dbo].[GroupWords] ([Id])
GO
ALTER TABLE [dbo].[KeyWords] CHECK CONSTRAINT [FK_TopWords_GroupWords]
GO
ALTER TABLE [dbo].[KeywordsCount]  WITH CHECK ADD  CONSTRAINT [FK_KeywordsCount_GroupWords] FOREIGN KEY([GroupKeyWordId])
REFERENCES [dbo].[GroupWords] ([Id])
GO
ALTER TABLE [dbo].[KeywordsCount] CHECK CONSTRAINT [FK_KeywordsCount_GroupWords]
GO
ALTER TABLE [dbo].[KeywordsCount]  WITH CHECK ADD  CONSTRAINT [FK_KeywordsCount_KeyWords] FOREIGN KEY([KeyWord])
REFERENCES [dbo].[KeyWords] ([Id])
GO
ALTER TABLE [dbo].[KeywordsCount] CHECK CONSTRAINT [FK_KeywordsCount_KeyWords]
GO
ALTER TABLE [dbo].[LogAddKeyWord]  WITH CHECK ADD  CONSTRAINT [FK_LogAddKeyWord_GroupWords] FOREIGN KEY([GroupWordId])
REFERENCES [dbo].[GroupWords] ([Id])
GO
ALTER TABLE [dbo].[LogAddKeyWord] CHECK CONSTRAINT [FK_LogAddKeyWord_GroupWords]
GO
ALTER TABLE [dbo].[Menu]  WITH CHECK ADD  CONSTRAINT [FK_Menu_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[Menu] CHECK CONSTRAINT [FK_Menu_Category]
GO
ALTER TABLE [dbo].[Menu]  WITH CHECK ADD  CONSTRAINT [FK_Menu_TypeMenu] FOREIGN KEY([TypeMenuId])
REFERENCES [dbo].[TypeMenu] ([Id])
GO
ALTER TABLE [dbo].[Menu] CHECK CONSTRAINT [FK_Menu_TypeMenu]
GO
ALTER TABLE [dbo].[MySpecialities]  WITH CHECK ADD  CONSTRAINT [FK_MySpecialities_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[MySpecialities] CHECK CONSTRAINT [FK_MySpecialities_User]
GO
ALTER TABLE [dbo].[RoleFunction]  WITH CHECK ADD  CONSTRAINT [FK_RoleFunction_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[RoleFunction] CHECK CONSTRAINT [FK_RoleFunction_Category]
GO
ALTER TABLE [dbo].[RoleFunction]  WITH CHECK ADD  CONSTRAINT [FK_RoleFunction_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[RoleFunction] CHECK CONSTRAINT [FK_RoleFunction_Role]
GO
ALTER TABLE [dbo].[SeKeyWord]  WITH CHECK ADD  CONSTRAINT [FK_SeKeyWord_GroupWords] FOREIGN KEY([KeyWordId])
REFERENCES [dbo].[GroupWords] ([Id])
GO
ALTER TABLE [dbo].[SeKeyWord] CHECK CONSTRAINT [FK_SeKeyWord_GroupWords]
GO
ALTER TABLE [dbo].[Sentenses]  WITH CHECK ADD  CONSTRAINT [FK_Sentenses_Comments] FOREIGN KEY([CommentId])
REFERENCES [dbo].[Comments] ([Id])
GO
ALTER TABLE [dbo].[Sentenses] CHECK CONSTRAINT [FK_Sentenses_Comments]
GO
ALTER TABLE [dbo].[SkillsAbilities]  WITH CHECK ADD  CONSTRAINT [FK_SkillsAbilities_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[SkillsAbilities] CHECK CONSTRAINT [FK_SkillsAbilities_User]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
USE [master]
GO
ALTER DATABASE [DoAnTSLongv1] SET  READ_WRITE 
GO
