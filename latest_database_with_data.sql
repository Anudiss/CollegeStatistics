USE [master]
GO
/****** Object:  Database [CollegeStatistics]    Script Date: 03.05.2023 11:44:06 ******/
CREATE DATABASE [CollegeStatistics]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CollegeStatistics', FILENAME = N'C:\Users\Ильназ\CollegeStatistics.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CollegeStatistics_log', FILENAME = N'C:\Users\Ильназ\CollegeStatistics_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [CollegeStatistics] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CollegeStatistics].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CollegeStatistics] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CollegeStatistics] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CollegeStatistics] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CollegeStatistics] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CollegeStatistics] SET ARITHABORT OFF 
GO
ALTER DATABASE [CollegeStatistics] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [CollegeStatistics] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CollegeStatistics] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CollegeStatistics] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CollegeStatistics] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CollegeStatistics] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CollegeStatistics] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CollegeStatistics] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CollegeStatistics] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CollegeStatistics] SET  ENABLE_BROKER 
GO
ALTER DATABASE [CollegeStatistics] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CollegeStatistics] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CollegeStatistics] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CollegeStatistics] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CollegeStatistics] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CollegeStatistics] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CollegeStatistics] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CollegeStatistics] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CollegeStatistics] SET  MULTI_USER 
GO
ALTER DATABASE [CollegeStatistics] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CollegeStatistics] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CollegeStatistics] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CollegeStatistics] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CollegeStatistics] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CollegeStatistics] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [CollegeStatistics] SET QUERY_STORE = OFF
GO
USE [CollegeStatistics]
GO
/****** Object:  Table [dbo].[Attendance]    Script Date: 03.05.2023 11:44:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attendance](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LessonId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
	[IsAttended] [bit] NOT NULL,
 CONSTRAINT [PK_Attendance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DayOfWeek]    Script Date: 03.05.2023 11:44:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DayOfWeek](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Reduction] [nvarchar](2) NOT NULL,
 CONSTRAINT [PK_DayOfWeek] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 03.05.2023 11:44:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EducationForm]    Script Date: 03.05.2023 11:44:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EducationForm](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_EducationForm] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmergencySituation]    Script Date: 03.05.2023 11:44:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmergencySituation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LessonId] [int] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_EmergencySituation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 03.05.2023 11:44:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreationYear] [smallint] NOT NULL,
	[EducationFormId] [int] NOT NULL,
	[TeacherId] [int] NOT NULL,
	[SpecialityId] [int] NOT NULL,
	[Number] [int] NOT NULL,
	[GroupLeaderId] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Homework]    Script Date: 03.05.2023 11:44:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Homework](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IssueDate] [datetime] NOT NULL,
	[Deadline] [datetime] NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[ExecutionStatusId] [int] NOT NULL,
	[LessonId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Homework] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HomeworkExecutionStatus]    Script Date: 03.05.2023 11:44:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HomeworkExecutionStatus](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ExecutionStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lesson]    Script Date: 03.05.2023 11:44:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lesson](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TimetableId] [int] NOT NULL,
	[Datetime] [smalldatetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Lesson_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LessonType]    Script Date: 03.05.2023 11:44:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LessonType](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_LessonType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteToLesson]    Script Date: 03.05.2023 11:44:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteToLesson](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LessonId] [int] NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_NoteToLesson] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteToStudent]    Script Date: 03.05.2023 11:44:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteToStudent](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LessonId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_NoteToStudent] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Speciality]    Script Date: 03.05.2023 11:44:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Speciality](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[DepartmentId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Speciality] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 03.05.2023 11:44:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Patronymic] [nvarchar](50) NOT NULL,
	[GroupId] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudyPlan]    Script Date: 03.05.2023 11:44:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudyPlan](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SpecialityId] [int] NOT NULL,
	[SubjectId] [int] NOT NULL,
	[Course] [tinyint] NOT NULL,
	[StartDate] [date] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_StudyPlan_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudyPlanRecord]    Script Date: 03.05.2023 11:44:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudyPlanRecord](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LessonTypeId] [int] NOT NULL,
	[DurationinLessons] [int] NOT NULL,
	[Topic] [nvarchar](max) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[StudyPlanId] [int] NOT NULL,
 CONSTRAINT [PK_StudyPlanRecord] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subject]    Script Date: 03.05.2023 11:44:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subject](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Subject] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teacher]    Script Date: 03.05.2023 11:44:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teacher](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Patronymic] [nvarchar](50) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Teacher] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Timetable]    Script Date: 03.05.2023 11:44:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Timetable](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TeacherId] [int] NOT NULL,
	[SubjectId] [int] NOT NULL,
	[GroupId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Timetable] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimetableRecord]    Script Date: 03.05.2023 11:44:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimetableRecord](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Couple] [int] NOT NULL,
	[DayOfWeekId] [int] NOT NULL,
	[TimetableId] [int] NOT NULL,
 CONSTRAINT [PK_TimetableRecord] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[DayOfWeek] ([Id], [Name], [Reduction]) VALUES (0, N'Воскресенье', N'ВС')
INSERT [dbo].[DayOfWeek] ([Id], [Name], [Reduction]) VALUES (1, N'Понедельник', N'ПН')
INSERT [dbo].[DayOfWeek] ([Id], [Name], [Reduction]) VALUES (2, N'Вторник', N'ВТ')
INSERT [dbo].[DayOfWeek] ([Id], [Name], [Reduction]) VALUES (3, N'Среда', N'СР')
INSERT [dbo].[DayOfWeek] ([Id], [Name], [Reduction]) VALUES (4, N'Четверг', N'ЧТ')
INSERT [dbo].[DayOfWeek] ([Id], [Name], [Reduction]) VALUES (5, N'Пятница', N'ПТ')
INSERT [dbo].[DayOfWeek] ([Id], [Name], [Reduction]) VALUES (6, N'Суббота', N'СБ')
GO
SET IDENTITY_INSERT [dbo].[Department] ON 

INSERT [dbo].[Department] ([Id], [Name], [IsDeleted]) VALUES (1, N'Информационные системы и программирование', 0)
INSERT [dbo].[Department] ([Id], [Name], [IsDeleted]) VALUES (2, N'Сетевые технологии', 0)
INSERT [dbo].[Department] ([Id], [Name], [IsDeleted]) VALUES (3, N'Телекоммуникационные технологии и информационная безопасность', 0)
SET IDENTITY_INSERT [dbo].[Department] OFF
GO
INSERT [dbo].[EducationForm] ([ID], [Name]) VALUES (1, N'Бюджет')
INSERT [dbo].[EducationForm] ([ID], [Name]) VALUES (2, N'Коммерция')
GO
SET IDENTITY_INSERT [dbo].[Group] ON 

INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId], [IsDeleted]) VALUES (1, 2022, 1, 1, 11, 101, 2, 0)
INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId], [IsDeleted]) VALUES (2, 2022, 1, 2, 7, 111, 8, 0)
INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId], [IsDeleted]) VALUES (3, 2022, 1, 3, 5, 105, 13, 0)
INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId], [IsDeleted]) VALUES (4, 2022, 1, 4, 2, 120, 21, 0)
INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId], [IsDeleted]) VALUES (5, 2022, 1, 5, 2, 121, 23, 0)
INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId], [IsDeleted]) VALUES (6, 2022, 1, 6, 3, 125, 29, 0)
INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId], [IsDeleted]) VALUES (7, 2022, 1, 7, 3, 126, 34, 0)
INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId], [IsDeleted]) VALUES (8, 2022, 1, 8, 8, 130, 41, 0)
INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId], [IsDeleted]) VALUES (9, 2022, 1, 9, 8, 131, 43, 0)
INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId], [IsDeleted]) VALUES (10, 2022, 1, 10, 6, 135, 48, 0)
INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId], [IsDeleted]) VALUES (11, 2022, 2, 11, 11, 102, 54, 0)
INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId], [IsDeleted]) VALUES (12, 2022, 2, 12, 7, 112, 62, 0)
INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId], [IsDeleted]) VALUES (13, 2022, 2, 13, 5, 115, 67, 0)
INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId], [IsDeleted]) VALUES (14, 2022, 2, 14, 2, 122, 73, 0)
INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId], [IsDeleted]) VALUES (15, 2022, 2, 15, 5, 127, 78, 0)
INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId], [IsDeleted]) VALUES (16, 2022, 2, 16, 3, 128, 87, 0)
INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId], [IsDeleted]) VALUES (17, 2022, 2, 17, 8, 132, 91, 0)
INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId], [IsDeleted]) VALUES (18, 2022, 2, 18, 6, 136, 98, 0)
SET IDENTITY_INSERT [dbo].[Group] OFF
GO
INSERT [dbo].[HomeworkExecutionStatus] ([Id], [Name]) VALUES (1, N'Не выполнено')
INSERT [dbo].[HomeworkExecutionStatus] ([Id], [Name]) VALUES (2, N'Выполнено')
GO
INSERT [dbo].[LessonType] ([Id], [Name]) VALUES (1, N'Теоритическое занятие')
INSERT [dbo].[LessonType] ([Id], [Name]) VALUES (2, N'Практическая работа')
INSERT [dbo].[LessonType] ([Id], [Name]) VALUES (3, N'Лабораторная работа')
INSERT [dbo].[LessonType] ([Id], [Name]) VALUES (4, N'Контрольная работа')
INSERT [dbo].[LessonType] ([Id], [Name]) VALUES (5, N'Консультация')
INSERT [dbo].[LessonType] ([Id], [Name]) VALUES (6, N'Самостоятельная работа')
INSERT [dbo].[LessonType] ([Id], [Name]) VALUES (7, N'Учебная практика')
INSERT [dbo].[LessonType] ([Id], [Name]) VALUES (8, N'Курсовая работа')
INSERT [dbo].[LessonType] ([Id], [Name]) VALUES (9, N'Выпускная квалификационная работа')
INSERT [dbo].[LessonType] ([Id], [Name]) VALUES (10, N'Другой')
GO
SET IDENTITY_INSERT [dbo].[Speciality] ON 

INSERT [dbo].[Speciality] ([Id], [Name], [DepartmentId], [IsDeleted]) VALUES (1, N'Специалист по информационным системам', 1, 0)
INSERT [dbo].[Speciality] ([Id], [Name], [DepartmentId], [IsDeleted]) VALUES (2, N'Какая-то другая специальность', 1, 0)
INSERT [dbo].[Speciality] ([Id], [Name], [DepartmentId], [IsDeleted]) VALUES (3, N'Разработчик веб и мультимедийных приложений', 1, 0)
INSERT [dbo].[Speciality] ([Id], [Name], [DepartmentId], [IsDeleted]) VALUES (4, N'Специалист по информационным системам', 1, 0)
INSERT [dbo].[Speciality] ([Id], [Name], [DepartmentId], [IsDeleted]) VALUES (5, N'Компьютерные системы и комплексы', 2, 0)
INSERT [dbo].[Speciality] ([Id], [Name], [DepartmentId], [IsDeleted]) VALUES (6, N'Сетевое и системное администрирование', 2, 0)
INSERT [dbo].[Speciality] ([Id], [Name], [DepartmentId], [IsDeleted]) VALUES (7, N'Почтовая связь', 2, 0)
INSERT [dbo].[Speciality] ([Id], [Name], [DepartmentId], [IsDeleted]) VALUES (8, N'Обеспечение информационной безопасности автоматизированных систем', 3, 0)
INSERT [dbo].[Speciality] ([Id], [Name], [DepartmentId], [IsDeleted]) VALUES (9, N'Многоканальные телекоммуникационные системы', 3, 0)
INSERT [dbo].[Speciality] ([Id], [Name], [DepartmentId], [IsDeleted]) VALUES (10, N'Сети связи и системы коммутации', 3, 0)
INSERT [dbo].[Speciality] ([Id], [Name], [DepartmentId], [IsDeleted]) VALUES (11, N'Инфокоммуникационные сети и системы связи', 3, 0)
SET IDENTITY_INSERT [dbo].[Speciality] OFF
GO
SET IDENTITY_INSERT [dbo].[Student] ON 

INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (1, N'Баранов', N'Матвей', N'Александрович', 1, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (2, N'Власова', N'Анна', N'Ивановна', 1, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (3, N'Лебедев', N'Максим', N'Иванович', 1, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (4, N'Колесникова', N'Евгения', N'Игоревна', 1, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (5, N'Потапов', N'Вячеслав', N'Глебович', 1, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (6, N'Петров', N'Антон', N'Георгиевич', 2, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (7, N'Сомова', N'Ксения', N'Саввична', 2, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (8, N'Лебедева', N'Вера', N'Леонидовна', 2, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (9, N'Трошина', N'София', N'Ивановна', 2, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (10, N'Смирнов', N'Михаил', N'Михайлович', 2, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (11, N'Смирнов', N'Александр', N'Андреевич', 2, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (12, N'Егоров', N'Герман', N'Мирославович', 3, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (13, N'Громов', N'Кирилл', N'Иванович', 3, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (14, N'Титов', N'Даниил', N'Михайлович', 3, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (15, N'Кузнецов', N'Али', N'Романович', 3, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (16, N'Юдин', N'Валерий', N'Никитич', 3, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (17, N'Лаврентьев', N'Демид', N'Петрович', 4, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (18, N'Казаков', N'Мирон', N'Матвеевич', 4, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (19, N'Тихонова', N'Лейла', N'Ивановна', 4, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (20, N'Никифорова', N'Мария', N'Львовна', 4, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (21, N'Егорова', N'Арина', N'Александровна', 4, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (22, N'Агеев', N'Никита', N'Александрович', 5, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (23, N'Абрамов', N'Даниил', N'Матвеевич', 5, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (24, N'Устинова', N'Анастасия', N'Артёмовна', 5, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (25, N'Петров', N'Святослав', N'Сергеевич', 5, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (26, N'Волошин', N'Илья', N'Андреевич', 5, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (27, N'Воронов', N'Дамир', N'Никитич', 6, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (28, N'Шульгина', N'Мария', N'Львовна', 6, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (29, N'Попова', N'Таисия', N'Богдановна', 6, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (30, N'Скворцов', N'Ярослав', N'Лукич', 6, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (31, N'Назаров', N'Александр', N'Львович', 6, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (32, N'Попова', N'Майя', N'Павловна', 7, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (33, N'Горячев', N'Денис', N'Александрович', 7, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (34, N'Фокин', N'Давид', N'Вячеславович', 7, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (35, N'Горелов', N'Егор', N'Артёмович', 7, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (36, N'Данилова', N'София', N'Артемьевна', 7, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (37, N'Киселев', N'Руслан', N'Дмитриевич', 8, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (38, N'Зверева', N'Ольга', N'Ярославовна', 8, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (39, N'Софронов', N'Всеволод', N'Дмитриевич', 8, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (40, N'Курочкина', N'Софья', N'Максимовна', 8, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (41, N'Костин', N'Дмитрий', N'Артёмович', 8, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (42, N'Некрасова', N'Лилия', N'Михайловна', 9, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (43, N'Калашникова', N'Александра', N'Елисеевна', 9, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (44, N'Дегтярева', N'Полина', N'Егоровна', 9, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (45, N'Яковлев', N'Владимир', N'Фёдорович', 9, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (46, N'Петрова', N'Ясмина', N'Артёмовна', 9, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (47, N'Гришин', N'Евгений', N'Савельевич', 10, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (48, N'Корчагин', N'Дмитрий', N'Леонович', 10, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (49, N'Мальцева', N'Амелия', N'Егоровна', 10, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (50, N'Фролова', N'Яна', N'Данииловна', 10, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (51, N'Малышев', N'Максим', N'Саввич', 10, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (52, N'Сальникова', N'Виктория', N'Фёдоровна', 10, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (53, N'Верещагин', N'Даниэль', N'Артёмович', 11, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (54, N'Трифонов', N'Андрей', N'Степанович', 11, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (55, N'Нефедов', N'Эмир', N'Сергеевич', 11, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (56, N'Мельников', N'Дмитрий', N'Николаевич', 11, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (57, N'Михайлова', N'Алиса', N'Александровна', 11, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (58, N'Алексеева', N'Александра', N'Кирилловна', 11, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (59, N'Воронцов', N'Тимофей', N'Евгеньевич', 12, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (60, N'Копылова', N'Дарья', N'Захаровна', 12, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (61, N'Исаева', N'Анастасия', N'Вячеславовна', 12, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (62, N'Андреева', N'Александра', N'Александровна', 12, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (63, N'Самойлова', N'Анастасия', N'Львовна', 12, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (64, N'Коровина', N'Виктория', N'Александровна', 12, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (65, N'Соболева', N'Екатерина', N'Николаевна', 13, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (66, N'Борисова', N'Дарья', N'Романовна', 13, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (67, N'Волкова', N'Дарья', N'Ильинична', 13, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (68, N'Корнеев', N'Лука', N'Фёдорович', 13, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (69, N'Никитина', N'Ева', N'Александровна', 13, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (70, N'Иванов', N'Андрей', N'Петрович', 13, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (71, N'Федорова', N'Оливия', N'Владиславовна', 14, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (72, N'Юдина', N'Марьям', N'Владимировна', 14, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (73, N'Галкина', N'София', N'Артёмовна', 14, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (74, N'Семенов', N'Матвей', N'Никитич', 14, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (75, N'Чернова', N'Софья', N'Данииловна', 14, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (76, N'Нефедов', N'Арсений', N'Давидович', 14, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (77, N'Алексеева', N'Арина', N'Максимовна', 15, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (78, N'Михайлов', N'Дмитрий', N'Захарович', 15, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (79, N'Попов', N'Денис', N'Артёмович', 15, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (80, N'Павлов', N'Дмитрий', N'Кириллович', 15, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (81, N'Рябинин', N'Даниил', N'Романович', 15, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (82, N'Лебедева', N'Ярослава', N'Тимуровна', 15, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (83, N'Беляев', N'Артём', N'Лукич', 16, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (84, N'Максимова', N'Стефания', N'Павловна', 16, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (85, N'Куликов', N'Георгий', N'Александрович', 16, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (86, N'Калинин', N'Матвей', N'Дмитриевич', 16, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (87, N'Романов', N'Михаил', N'Даниилович', 16, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (88, N'Трофимова', N'Юлия', N'Алексеевна', 16, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (89, N'Горбунов', N'Владимир', N'Александрович', 17, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (90, N'Лебедев', N'Денис', N'Владимирович', 17, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (91, N'Васильева', N'Ева', N'Егоровна', 17, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (92, N'Тихонов', N'Дамир', N'Ярославович', 17, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (93, N'Антонов', N'Давид', N'Артёмович', 17, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (94, N'Петрова', N'Тея', N'Максимовна', 17, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (95, N'Дроздов', N'Даниил', N'Демидович', 18, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (96, N'Гусев', N'Данила', N'Алексеевич', 18, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (97, N'Марков', N'Лев', N'Григорьевич', 18, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (98, N'Гришина', N'Диана', N'Михайловна', 18, 0)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (99, N'Пахомов', N'Николай', N'Тимофеевич', 18, 0)
GO
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId], [IsDeleted]) VALUES (100, N'Черкасова', N'Алиса', N'Тимуровна', 18, 0)
SET IDENTITY_INSERT [dbo].[Student] OFF
GO
SET IDENTITY_INSERT [dbo].[Subject] ON 

INSERT [dbo].[Subject] ([Id], [Name], [IsDeleted]) VALUES (1, N'Проектирование и дизайн информационных систем', 0)
INSERT [dbo].[Subject] ([Id], [Name], [IsDeleted]) VALUES (2, N'История', 0)
INSERT [dbo].[Subject] ([Id], [Name], [IsDeleted]) VALUES (3, N'Физика', 0)
INSERT [dbo].[Subject] ([Id], [Name], [IsDeleted]) VALUES (4, N'Иностранный язык', 0)
INSERT [dbo].[Subject] ([Id], [Name], [IsDeleted]) VALUES (5, N'Современный русский язык', 0)
INSERT [dbo].[Subject] ([Id], [Name], [IsDeleted]) VALUES (6, N'Современный татарский язык', 0)
INSERT [dbo].[Subject] ([Id], [Name], [IsDeleted]) VALUES (7, N'Негативные факторы техносферы в профессиональной деятельности', 0)
INSERT [dbo].[Subject] ([Id], [Name], [IsDeleted]) VALUES (8, N'История России и мира в развитии профессиональных компетенций', 0)
INSERT [dbo].[Subject] ([Id], [Name], [IsDeleted]) VALUES (9, N'Геометрия', 0)
INSERT [dbo].[Subject] ([Id], [Name], [IsDeleted]) VALUES (10, N'Литература', 0)
INSERT [dbo].[Subject] ([Id], [Name], [IsDeleted]) VALUES (11, N'Информатика', 0)
INSERT [dbo].[Subject] ([Id], [Name], [IsDeleted]) VALUES (12, N'Химия и химические элементы и химические элементы', 0)
INSERT [dbo].[Subject] ([Id], [Name], [IsDeleted]) VALUES (13, N'Физическая культура', 0)
INSERT [dbo].[Subject] ([Id], [Name], [IsDeleted]) VALUES (14, N'Астрономия', 0)
SET IDENTITY_INSERT [dbo].[Subject] OFF
GO
SET IDENTITY_INSERT [dbo].[Teacher] ON 

INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic], [IsDeleted]) VALUES (1, N'Аркадьева', N'Оксана', N'Николаевна1', 0)
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic], [IsDeleted]) VALUES (2, N'Плаксин', N'Никита', N'Александрович', 0)
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic], [IsDeleted]) VALUES (3, N'Долгова', N'Ирина', N'Ивановна', 0)
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic], [IsDeleted]) VALUES (4, N'Иванов', N'Эдуард', N'Владимирович', 0)
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic], [IsDeleted]) VALUES (5, N'Иванов', N'Эдуард', N'Владимирович', 0)
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic], [IsDeleted]) VALUES (6, N'Архипов', N'Тихон', N'Авксентьевич', 0)
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic], [IsDeleted]) VALUES (7, N'Архипов', N'Тихон', N'Авксентьевич', 0)
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic], [IsDeleted]) VALUES (8, N'Селиверстов', N'Тимур', N'Андреевич', 0)
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic], [IsDeleted]) VALUES (9, N'Крюков', N'Тарас', N'Куприянович', 0)
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic], [IsDeleted]) VALUES (10, N'Мартынова', N'Таисия', N'Степановна', 0)
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic], [IsDeleted]) VALUES (11, N'Копылов', N'Соломон', N'Наумович', 0)
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic], [IsDeleted]) VALUES (12, N'Копылов', N'Соломон', N'Наумович', 0)
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic], [IsDeleted]) VALUES (13, N'Тимофеев', N'Севастьян', N'Серапионович', 0)
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic], [IsDeleted]) VALUES (14, N'Гордеев', N'Панкратий', N'Филатович', 0)
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic], [IsDeleted]) VALUES (15, N'Гордеев', N'Панкратий', N'Филатович', 0)
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic], [IsDeleted]) VALUES (16, N'Зуев', N'Осип', N'Оскарович', 0)
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic], [IsDeleted]) VALUES (17, N'Соболев', N'Моисей', N'Макарович', 0)
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic], [IsDeleted]) VALUES (18, N'Щукин', N'Модест', N'Пантелеймонович', 0)
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic], [IsDeleted]) VALUES (19, N'Никонов', N'Модест', N'Федорович', 0)
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic], [IsDeleted]) VALUES (20, N'Анисимов', N'Мирон', N'Митрофанович', 0)
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic], [IsDeleted]) VALUES (21, N'Анисимов', N'Мирон', N'Митрофанович', 0)
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic], [IsDeleted]) VALUES (22, N'Егорова', N'Лиана', N'Ефимовна', 0)
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic], [IsDeleted]) VALUES (23, N'Куликов', N'Корнелий', N'Германнович', 0)
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic], [IsDeleted]) VALUES (24, N'Куликов', N'Корнелий', N'Германнович', 0)
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic], [IsDeleted]) VALUES (25, N'Рожков', N'Казимир', N'Романович', 0)
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic], [IsDeleted]) VALUES (26, N'Рожков', N'Казимир', N'Романович', 0)
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic], [IsDeleted]) VALUES (27, N'Лаврентьева', N'Изольда', N'Валентиновна', 0)
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic], [IsDeleted]) VALUES (28, N'Фомина', N'Зоя', N'Петровна', 0)
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic], [IsDeleted]) VALUES (29, N'Зуев', N'Донат', N'Иванович', 0)
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic], [IsDeleted]) VALUES (30, N'1', N'1', N'1', 1)
SET IDENTITY_INSERT [dbo].[Teacher] OFF
GO
ALTER TABLE [dbo].[Attendance]  WITH CHECK ADD  CONSTRAINT [FK_Attendance_Lesson] FOREIGN KEY([LessonId])
REFERENCES [dbo].[Lesson] ([Id])
GO
ALTER TABLE [dbo].[Attendance] CHECK CONSTRAINT [FK_Attendance_Lesson]
GO
ALTER TABLE [dbo].[Attendance]  WITH CHECK ADD  CONSTRAINT [FK_Attendance_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([Id])
GO
ALTER TABLE [dbo].[Attendance] CHECK CONSTRAINT [FK_Attendance_Student]
GO
ALTER TABLE [dbo].[EmergencySituation]  WITH CHECK ADD  CONSTRAINT [FK_EmergencySituation_Lesson] FOREIGN KEY([LessonId])
REFERENCES [dbo].[Lesson] ([Id])
GO
ALTER TABLE [dbo].[EmergencySituation] CHECK CONSTRAINT [FK_EmergencySituation_Lesson]
GO
ALTER TABLE [dbo].[Group]  WITH CHECK ADD  CONSTRAINT [FK_Group_EducationForm] FOREIGN KEY([EducationFormId])
REFERENCES [dbo].[EducationForm] ([ID])
GO
ALTER TABLE [dbo].[Group] CHECK CONSTRAINT [FK_Group_EducationForm]
GO
ALTER TABLE [dbo].[Group]  WITH CHECK ADD  CONSTRAINT [FK_Group_Speciality] FOREIGN KEY([SpecialityId])
REFERENCES [dbo].[Speciality] ([Id])
GO
ALTER TABLE [dbo].[Group] CHECK CONSTRAINT [FK_Group_Speciality]
GO
ALTER TABLE [dbo].[Group]  WITH CHECK ADD  CONSTRAINT [FK_Group_Student] FOREIGN KEY([GroupLeaderId])
REFERENCES [dbo].[Student] ([Id])
GO
ALTER TABLE [dbo].[Group] CHECK CONSTRAINT [FK_Group_Student]
GO
ALTER TABLE [dbo].[Group]  WITH CHECK ADD  CONSTRAINT [FK_Group_Teacher] FOREIGN KEY([TeacherId])
REFERENCES [dbo].[Teacher] ([Id])
GO
ALTER TABLE [dbo].[Group] CHECK CONSTRAINT [FK_Group_Teacher]
GO
ALTER TABLE [dbo].[Homework]  WITH CHECK ADD  CONSTRAINT [FK_Homework_HomeworkExecutionStatus] FOREIGN KEY([ExecutionStatusId])
REFERENCES [dbo].[HomeworkExecutionStatus] ([Id])
GO
ALTER TABLE [dbo].[Homework] CHECK CONSTRAINT [FK_Homework_HomeworkExecutionStatus]
GO
ALTER TABLE [dbo].[Homework]  WITH CHECK ADD  CONSTRAINT [FK_Homework_Lesson] FOREIGN KEY([LessonId])
REFERENCES [dbo].[Lesson] ([Id])
GO
ALTER TABLE [dbo].[Homework] CHECK CONSTRAINT [FK_Homework_Lesson]
GO
ALTER TABLE [dbo].[NoteToLesson]  WITH CHECK ADD  CONSTRAINT [FK_NoteToLesson_Lesson] FOREIGN KEY([LessonId])
REFERENCES [dbo].[Lesson] ([Id])
GO
ALTER TABLE [dbo].[NoteToLesson] CHECK CONSTRAINT [FK_NoteToLesson_Lesson]
GO
ALTER TABLE [dbo].[NoteToStudent]  WITH CHECK ADD  CONSTRAINT [FK_NoteToStudent_Lesson] FOREIGN KEY([LessonId])
REFERENCES [dbo].[Lesson] ([Id])
GO
ALTER TABLE [dbo].[NoteToStudent] CHECK CONSTRAINT [FK_NoteToStudent_Lesson]
GO
ALTER TABLE [dbo].[NoteToStudent]  WITH CHECK ADD  CONSTRAINT [FK_NoteToStudent_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([Id])
GO
ALTER TABLE [dbo].[NoteToStudent] CHECK CONSTRAINT [FK_NoteToStudent_Student]
GO
ALTER TABLE [dbo].[Speciality]  WITH CHECK ADD  CONSTRAINT [FK_Speciality_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO
ALTER TABLE [dbo].[Speciality] CHECK CONSTRAINT [FK_Speciality_Department]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_Group] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Group] ([Id])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_Group]
GO
ALTER TABLE [dbo].[StudyPlan]  WITH CHECK ADD  CONSTRAINT [FK_StudyPlan_Speciality] FOREIGN KEY([SpecialityId])
REFERENCES [dbo].[Speciality] ([Id])
GO
ALTER TABLE [dbo].[StudyPlan] CHECK CONSTRAINT [FK_StudyPlan_Speciality]
GO
ALTER TABLE [dbo].[StudyPlan]  WITH CHECK ADD  CONSTRAINT [FK_StudyPlan_Subject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subject] ([Id])
GO
ALTER TABLE [dbo].[StudyPlan] CHECK CONSTRAINT [FK_StudyPlan_Subject]
GO
ALTER TABLE [dbo].[StudyPlanRecord]  WITH CHECK ADD  CONSTRAINT [FK_StudyPlanRecord_LessonType] FOREIGN KEY([LessonTypeId])
REFERENCES [dbo].[LessonType] ([Id])
GO
ALTER TABLE [dbo].[StudyPlanRecord] CHECK CONSTRAINT [FK_StudyPlanRecord_LessonType]
GO
ALTER TABLE [dbo].[StudyPlanRecord]  WITH CHECK ADD  CONSTRAINT [FK_StudyPlanRecord_StudyPlan] FOREIGN KEY([StudyPlanId])
REFERENCES [dbo].[StudyPlan] ([Id])
GO
ALTER TABLE [dbo].[StudyPlanRecord] CHECK CONSTRAINT [FK_StudyPlanRecord_StudyPlan]
GO
ALTER TABLE [dbo].[Timetable]  WITH CHECK ADD  CONSTRAINT [FK_Timetable_Group] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Group] ([Id])
GO
ALTER TABLE [dbo].[Timetable] CHECK CONSTRAINT [FK_Timetable_Group]
GO
ALTER TABLE [dbo].[Timetable]  WITH CHECK ADD  CONSTRAINT [FK_Timetable_Subject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subject] ([Id])
GO
ALTER TABLE [dbo].[Timetable] CHECK CONSTRAINT [FK_Timetable_Subject]
GO
ALTER TABLE [dbo].[Timetable]  WITH CHECK ADD  CONSTRAINT [FK_Timetable_Teacher] FOREIGN KEY([TeacherId])
REFERENCES [dbo].[Teacher] ([Id])
GO
ALTER TABLE [dbo].[Timetable] CHECK CONSTRAINT [FK_Timetable_Teacher]
GO
ALTER TABLE [dbo].[TimetableRecord]  WITH CHECK ADD  CONSTRAINT [FK_TimetableRecord_DayOfWeek] FOREIGN KEY([DayOfWeekId])
REFERENCES [dbo].[DayOfWeek] ([Id])
GO
ALTER TABLE [dbo].[TimetableRecord] CHECK CONSTRAINT [FK_TimetableRecord_DayOfWeek]
GO
ALTER TABLE [dbo].[TimetableRecord]  WITH CHECK ADD  CONSTRAINT [FK_TimetableRecord_Timetable] FOREIGN KEY([TimetableId])
REFERENCES [dbo].[Timetable] ([Id])
GO
ALTER TABLE [dbo].[TimetableRecord] CHECK CONSTRAINT [FK_TimetableRecord_Timetable]
GO
USE [master]
GO
ALTER DATABASE [CollegeStatistics] SET  READ_WRITE 
GO
