# Статистика колледжа

## Мещеряков, Хасанов, Гарипов, Алексеев

### Сделаны справочники для редактирования объектов предметной области

USE [CollegeStatistics]
GO
/****** Object:  Table [dbo].[Attendance]    Script Date: 29.04.2023 10:04:17 ******/
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
/****** Object:  Table [dbo].[CommisionCurator]    Script Date: 29.04.2023 10:04:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommisionCurator](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Patronymic] [nvarchar](50) NOT NULL,
	[DepartmentId] [int] NOT NULL,
 CONSTRAINT [PK_CommisionCurator] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DayOfWeek]    Script Date: 29.04.2023 10:04:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DayOfWeek](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_DayOfWeek] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 29.04.2023 10:04:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EducationForm]    Script Date: 29.04.2023 10:04:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EducationForm](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_EducationForm] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmergencySituation]    Script Date: 29.04.2023 10:04:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmergencySituation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LessonId] [int] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_EmergencySituation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 29.04.2023 10:04:17 ******/
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
	[GroupLeaderId] [int] NOT NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Homework]    Script Date: 29.04.2023 10:04:17 ******/
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
 CONSTRAINT [PK_Homework] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HomeworkExecutionStatus]    Script Date: 29.04.2023 10:04:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HomeworkExecutionStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ExecutionStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lesson]    Script Date: 29.04.2023 10:04:17 ******/
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
/****** Object:  Table [dbo].[LessonsStartTime]    Script Date: 29.04.2023 10:04:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LessonsStartTime](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DayOfWeek] [nvarchar](150) NOT NULL,
	[Time] [time](0) NOT NULL,
 CONSTRAINT [PK_LessonsStartTime] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LessonType]    Script Date: 29.04.2023 10:04:17 ******/
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
/****** Object:  Table [dbo].[NoteToLesson]    Script Date: 29.04.2023 10:04:17 ******/
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
/****** Object:  Table [dbo].[NoteToStudent]    Script Date: 29.04.2023 10:04:17 ******/
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
/****** Object:  Table [dbo].[Speciality]    Script Date: 29.04.2023 10:04:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Speciality](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[DepartmentId] [int] NOT NULL,
 CONSTRAINT [PK_Speciality] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 29.04.2023 10:04:17 ******/
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
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudyPlan]    Script Date: 29.04.2023 10:04:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudyPlan](
	[SpecialityId] [int] NOT NULL,
	[SubjectId] [int] NOT NULL,
	[Course] [tinyint] NOT NULL,
	[LessonTypeId] [int] NOT NULL,
	[DurationInLessons] [int] NOT NULL,
	[Topic] [nvarchar](max) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[StartDate] [date] NOT NULL,
 CONSTRAINT [PK_StudyPlan] PRIMARY KEY CLUSTERED 
(
	[SpecialityId] ASC,
	[SubjectId] ASC,
	[Course] ASC,
	[LessonTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subject]    Script Date: 29.04.2023 10:04:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subject](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_Subject] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teacher]    Script Date: 29.04.2023 10:04:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teacher](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Patronymic] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Teacher] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Timetable]    Script Date: 29.04.2023 10:04:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Timetable](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TeacherId] [int] NOT NULL,
	[SubjectId] [int] NOT NULL,
	[GroupId] [int] NOT NULL,
 CONSTRAINT [PK_Timetable] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimetableRecord]    Script Date: 29.04.2023 10:04:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimetableRecord](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Time] [time](7) NOT NULL,
	[DayOfWeekId] [int] NOT NULL,
	[TimetableId] [int] NOT NULL,
 CONSTRAINT [PK_TimetableRecord] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[CommisionCurator] ON 

INSERT [dbo].[CommisionCurator] ([Id], [Surname], [Name], [Patronymic], [DepartmentId]) VALUES (1, N'Зубкова', N'Алина', N'Сергеевна', 1)
INSERT [dbo].[CommisionCurator] ([Id], [Surname], [Name], [Patronymic], [DepartmentId]) VALUES (2, N'Вилюмс', N'Луиза', N'Артуровна', 2)
INSERT [dbo].[CommisionCurator] ([Id], [Surname], [Name], [Patronymic], [DepartmentId]) VALUES (3, N'Тимохина', N'Вера', N'Алексеевна', 3)
SET IDENTITY_INSERT [dbo].[CommisionCurator] OFF
GO
SET IDENTITY_INSERT [dbo].[Department] ON 

INSERT [dbo].[Department] ([Id], [Name]) VALUES (1, N'Информационные технологии')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (2, N'Сетевые технологии')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (3, N'Телекоммуникационные технологии и информационная безопасность')
SET IDENTITY_INSERT [dbo].[Department] OFF
GO
SET IDENTITY_INSERT [dbo].[EducationForm] ON 

INSERT [dbo].[EducationForm] ([ID], [Name]) VALUES (1, N'Бюджет')
INSERT [dbo].[EducationForm] ([ID], [Name]) VALUES (2, N'Коммерция')
SET IDENTITY_INSERT [dbo].[EducationForm] OFF
GO
SET IDENTITY_INSERT [dbo].[Group] ON 

INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId]) VALUES (1, 2022, 1, 1, 11, 101, 2)
INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId]) VALUES (2, 2022, 1, 2, 7, 111, 8)
INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId]) VALUES (3, 2022, 1, 3, 5, 105, 13)
INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId]) VALUES (4, 2022, 1, 4, 2, 120, 21)
INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId]) VALUES (5, 2022, 1, 5, 2, 121, 23)
INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId]) VALUES (6, 2022, 1, 6, 3, 125, 29)
INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId]) VALUES (7, 2022, 1, 7, 3, 126, 34)
INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId]) VALUES (8, 2022, 1, 8, 8, 130, 41)
INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId]) VALUES (9, 2022, 1, 9, 8, 131, 43)
INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId]) VALUES (10, 2022, 1, 10, 6, 135, 48)
INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId]) VALUES (11, 2022, 2, 11, 11, 102, 54)
INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId]) VALUES (12, 2022, 2, 12, 7, 112, 62)
INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId]) VALUES (13, 2022, 2, 13, 5, 115, 67)
INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId]) VALUES (14, 2022, 2, 14, 2, 122, 72)
INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId]) VALUES (15, 2022, 2, 15, 5, 127, 78)
INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId]) VALUES (16, 2022, 2, 16, 3, 128, 87)
INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId]) VALUES (17, 2022, 2, 17, 8, 132, 91)
INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId]) VALUES (18, 2022, 2, 18, 6, 136, 98)
SET IDENTITY_INSERT [dbo].[Group] OFF
GO
SET IDENTITY_INSERT [dbo].[LessonsStartTime] ON 

INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (1, N'Воскресенье', CAST(N'08:00:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (2, N'Воскресенье', CAST(N'09:40:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (3, N'Воскресенье', CAST(N'11:50:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (4, N'Воскресенье', CAST(N'13:40:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (5, N'Воскресенье', CAST(N'15:20:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (6, N'Воскресенье', CAST(N'17:00:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (7, N'Воскресенье', CAST(N'18:40:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (8, N'Понедельник', CAST(N'08:00:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (9, N'Понедельник', CAST(N'09:40:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (10, N'Понедельник', CAST(N'11:50:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (11, N'Понедельник', CAST(N'13:40:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (12, N'Понедельник', CAST(N'15:20:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (13, N'Понедельник', CAST(N'17:00:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (14, N'Понедельник', CAST(N'18:40:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (15, N'Вторник', CAST(N'08:00:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (16, N'Вторник', CAST(N'09:40:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (17, N'Вторник', CAST(N'11:50:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (18, N'Вторник', CAST(N'13:40:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (19, N'Вторник', CAST(N'15:20:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (20, N'Вторник', CAST(N'17:00:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (21, N'Вторник', CAST(N'18:40:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (22, N'Среда', CAST(N'08:00:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (23, N'Среда', CAST(N'09:40:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (24, N'Среда', CAST(N'11:50:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (25, N'Среда', CAST(N'13:40:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (26, N'Среда', CAST(N'15:20:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (27, N'Среда', CAST(N'17:00:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (28, N'Среда', CAST(N'18:40:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (29, N'Четверг', CAST(N'08:00:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (30, N'Четверг', CAST(N'09:40:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (31, N'Четверг', CAST(N'11:50:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (32, N'Четверг', CAST(N'13:40:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (33, N'Четверг', CAST(N'15:20:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (34, N'Четверг', CAST(N'17:00:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (35, N'Четверг', CAST(N'18:40:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (36, N'Пятница', CAST(N'08:00:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (37, N'Пятница', CAST(N'09:40:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (38, N'Пятница', CAST(N'11:50:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (39, N'Пятница', CAST(N'13:40:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (40, N'Пятница', CAST(N'15:20:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (41, N'Пятница', CAST(N'17:00:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (42, N'Пятница', CAST(N'18:40:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (43, N'Суббота', CAST(N'08:00:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (44, N'Суббота', CAST(N'09:40:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (45, N'Суббота', CAST(N'11:30:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (46, N'Суббота', CAST(N'13:10:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (47, N'Суббота', CAST(N'14:50:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (48, N'Суббота', CAST(N'16:30:00' AS Time))
SET IDENTITY_INSERT [dbo].[LessonsStartTime] OFF
GO
INSERT [dbo].[LessonType] ([Id], [Name]) VALUES (1, N'Лекция')
INSERT [dbo].[LessonType] ([Id], [Name]) VALUES (2, N'Лабораторная работа')
INSERT [dbo].[LessonType] ([Id], [Name]) VALUES (3, N'Практика')
GO
SET IDENTITY_INSERT [dbo].[Speciality] ON 

INSERT [dbo].[Speciality] ([Id], [Name], [DepartmentId]) VALUES (1, N'Администратор баз данных', 1)
INSERT [dbo].[Speciality] ([Id], [Name], [DepartmentId]) VALUES (2, N'Программист', 1)
INSERT [dbo].[Speciality] ([Id], [Name], [DepartmentId]) VALUES (3, N'Разработчик веб и мультимедийных приложений', 1)
INSERT [dbo].[Speciality] ([Id], [Name], [DepartmentId]) VALUES (4, N'Специалист по информационным системам', 1)
INSERT [dbo].[Speciality] ([Id], [Name], [DepartmentId]) VALUES (5, N'Компьютерные системы и комплексы', 2)
INSERT [dbo].[Speciality] ([Id], [Name], [DepartmentId]) VALUES (6, N'Сетевое и системное администрирование', 2)
INSERT [dbo].[Speciality] ([Id], [Name], [DepartmentId]) VALUES (7, N'Почтовая связь', 2)
INSERT [dbo].[Speciality] ([Id], [Name], [DepartmentId]) VALUES (8, N'Обеспечение информационной безопасности автоматизированных систем', 3)
INSERT [dbo].[Speciality] ([Id], [Name], [DepartmentId]) VALUES (9, N'Многоканальные телекоммуникационные системы', 3)
INSERT [dbo].[Speciality] ([Id], [Name], [DepartmentId]) VALUES (10, N'Сети связи и системы коммутации', 3)
INSERT [dbo].[Speciality] ([Id], [Name], [DepartmentId]) VALUES (11, N'Инфокоммуникационные сети и системы связи', 3)
SET IDENTITY_INSERT [dbo].[Speciality] OFF
GO
SET IDENTITY_INSERT [dbo].[Student] ON 

INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (1, N'Баранов', N'Матвей', N'Александрович', NULL)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (2, N'Власова', N'Анна', N'Ивановна', 7)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (3, N'Лебедев', N'Максим', N'Иванович', NULL)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (4, N'Колесникова', N'Евгения', N'Игоревна', 7)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (5, N'Потапов', N'Вячеслав', N'Глебович', NULL)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (6, N'Петров', N'Антон', N'Георгиевич', 1)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (7, N'Сомова', N'Ксения', N'Саввична', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (8, N'Лебедева', N'Вера', N'Леонидовна', 1)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (9, N'Трошина', N'София', N'Ивановна', 1)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (10, N'Смирнов', N'Михаил', N'Михайлович', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (11, N'Смирнов', N'Александр', N'Андреевич', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (12, N'Егоров', N'Герман', N'Мирославович', 1)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (13, N'Громов', N'Кирилл', N'Иванович', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (14, N'Титов', N'Даниил', N'Михайлович', NULL)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (15, N'Кузнецов', N'Али', N'Романович', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (16, N'Юдин', N'Валерий', N'Никитич', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (17, N'Лаврентьев', N'Демид', N'Петрович', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (18, N'Казаков', N'Мирон', N'Матвеевич', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (19, N'Тихонова', N'Лейла', N'Ивановна', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (20, N'Никифорова', N'Мария', N'Львовна', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (21, N'Егорова', N'Арина', N'Александровна', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (22, N'Агеев', N'Никита', N'Александрович', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (23, N'Абрамов', N'Даниил', N'Матвеевич', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (24, N'Устинова', N'Анастасия', N'Артёмовна', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (25, N'Петров', N'Святослав', N'Сергеевич', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (26, N'Волошин', N'Илья', N'Андреевич', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (27, N'Воронов', N'Дамир', N'Никитич', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (28, N'Шульгина', N'Мария', N'Львовна', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (29, N'Попова', N'Таисия', N'Богдановна', 6)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (30, N'Скворцов', N'Ярослав', N'Лукич', 6)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (31, N'Назаров', N'Александр', N'Львович', 6)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (32, N'Попова', N'Майя', N'Павловна', 7)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (33, N'Горячев', N'Денис', N'Александрович', 7)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (34, N'Фокин', N'Давид', N'Вячеславович', 7)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (35, N'Горелов', N'Егор', N'Артёмович', 7)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (36, N'Данилова', N'София', N'Артемьевна', 7)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (37, N'Киселев', N'Руслан', N'Дмитриевич', 8)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (38, N'Зверева', N'Ольга', N'Ярославовна', 8)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (39, N'Софронов', N'Всеволод', N'Дмитриевич', 8)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (40, N'Курочкина', N'Софья', N'Максимовна', 8)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (41, N'Костин', N'Дмитрий', N'Артёмович', 8)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (42, N'Некрасова', N'Лилия', N'Михайловна', 9)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (43, N'Калашникова', N'Александра', N'Елисеевна', 9)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (44, N'Дегтярева', N'Полина', N'Егоровна', 9)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (45, N'Яковлев', N'Владимир', N'Фёдорович', 9)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (46, N'Петрова', N'Ясмина', N'Артёмовна', 9)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (47, N'Гришин', N'Евгений', N'Савельевич', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (48, N'Корчагин', N'Дмитрий', N'Леонович', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (49, N'Мальцева', N'Амелия', N'Егоровна', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (50, N'Фролова', N'Яна', N'Данииловна', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (51, N'Малышев', N'Максим', N'Саввич', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (52, N'Сальникова', N'Виктория', N'Фёдоровна', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (53, N'Верещагин', N'Даниэль', N'Артёмович', 11)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (54, N'Трифонов', N'Андрей', N'Степанович', 11)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (55, N'Нефедов', N'Эмир', N'Сергеевич', 11)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (56, N'Мельников', N'Дмитрий', N'Николаевич', 11)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (57, N'Михайлова', N'Алиса', N'Александровна', 11)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (58, N'Алексеева', N'Александра', N'Кирилловна', 11)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (59, N'Воронцов', N'Тимофей', N'Евгеньевич', 12)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (60, N'Копылова', N'Дарья', N'Захаровна', 12)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (61, N'Исаева', N'Анастасия', N'Вячеславовна', 12)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (62, N'Андреева', N'Александра', N'Александровна', 12)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (63, N'Самойлова', N'Анастасия', N'Львовна', 12)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (64, N'Коровина', N'Виктория', N'Александровна', 12)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (65, N'Соболева', N'Екатерина', N'Николаевна', 13)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (66, N'Борисова', N'Дарья', N'Романовна', 13)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (67, N'Волкова', N'Дарья', N'Ильинична', 13)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (68, N'Корнеев', N'Лука', N'Фёдорович', 13)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (69, N'Никитина', N'Ева', N'Александровна', 13)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (70, N'Иванов', N'Андрей', N'Петрович', 13)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (71, N'Федорова', N'Оливия', N'Владиславовна', 14)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (72, N'Юдина', N'Марьям', N'Владимировна', 14)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (73, N'Галкина', N'София', N'Артёмовна', 14)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (74, N'Семенов', N'Матвей', N'Никитич', 14)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (75, N'Чернова', N'Софья', N'Данииловна', 14)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (76, N'Нефедов', N'Арсений', N'Давидович', 14)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (77, N'Алексеева', N'Арина', N'Максимовна', 15)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (78, N'Михайлов', N'Дмитрий', N'Захарович', 15)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (79, N'Попов', N'Денис', N'Артёмович', 15)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (80, N'Павлов', N'Дмитрий', N'Кириллович', 15)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (81, N'Рябинин', N'Даниил', N'Романович', 15)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (82, N'Лебедева', N'Ярослава', N'Тимуровна', 15)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (83, N'Беляев', N'Артём', N'Лукич', 16)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (84, N'Максимова', N'Стефания', N'Павловна', 16)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (85, N'Куликов', N'Георгий', N'Александрович', 16)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (86, N'Калинин', N'Матвей', N'Дмитриевич', 16)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (87, N'Романов', N'Михаил', N'Даниилович', 16)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (88, N'Трофимова', N'Юлия', N'Алексеевна', 16)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (89, N'Горбунов', N'Владимир', N'Александрович', 17)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (90, N'Лебедев', N'Денис', N'Владимирович', 17)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (91, N'Васильева', N'Ева', N'Егоровна', 17)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (92, N'Тихонов', N'Дамир', N'Ярославович', 17)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (93, N'Антонов', N'Давид', N'Артёмович', 17)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (94, N'Петрова', N'Тея', N'Максимовна', 17)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (95, N'Дроздов', N'Даниил', N'Демидович', 18)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (96, N'Гусев', N'Данила', N'Алексеевич', 18)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (97, N'Марков', N'Лев', N'Григорьевич', 18)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (98, N'Гришина', N'Диана', N'Михайловна', 18)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (99, N'Пахомов', N'Николай', N'Тимофеевич', 18)
GO
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (100, N'Черкасова', N'Алиса', N'Тимуровна', 18)
SET IDENTITY_INSERT [dbo].[Student] OFF
GO
SET IDENTITY_INSERT [dbo].[Subject] ON 

INSERT [dbo].[Subject] ([Id], [Name]) VALUES (1, N'Математика')
INSERT [dbo].[Subject] ([Id], [Name]) VALUES (2, N'История')
INSERT [dbo].[Subject] ([Id], [Name]) VALUES (3, N'Физика')
INSERT [dbo].[Subject] ([Id], [Name]) VALUES (4, N'Иностранный язык')
INSERT [dbo].[Subject] ([Id], [Name]) VALUES (5, N'Современный русский язык')
INSERT [dbo].[Subject] ([Id], [Name]) VALUES (6, N'Современный татарский язык')
INSERT [dbo].[Subject] ([Id], [Name]) VALUES (7, N'Негативные факторы техносферы в профессиональной деятельности')
INSERT [dbo].[Subject] ([Id], [Name]) VALUES (8, N'История России и мира в развитии профессиональных компетенций')
INSERT [dbo].[Subject] ([Id], [Name]) VALUES (9, N'Геометрия')
INSERT [dbo].[Subject] ([Id], [Name]) VALUES (10, N'Литература')
INSERT [dbo].[Subject] ([Id], [Name]) VALUES (11, N'Информатика')
INSERT [dbo].[Subject] ([Id], [Name]) VALUES (12, N'Химия и химические элементы и химические элементы')
INSERT [dbo].[Subject] ([Id], [Name]) VALUES (13, N'Физическая культура')
INSERT [dbo].[Subject] ([Id], [Name]) VALUES (14, N'Астрономия')
SET IDENTITY_INSERT [dbo].[Subject] OFF
GO
SET IDENTITY_INSERT [dbo].[Teacher] ON 

INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (1, N'Кириллова', N'Ярослава', N'Константиновна')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (2, N'Мамонтов', N'Юлиан', N'Борисович')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (3, N'Ширяева', N'Эрида', N'Оскаровна')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (4, N'Иванов', N'Эдуард', N'Владимирович')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (5, N'Иванов', N'Эдуард', N'Владимирович')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (6, N'Архипов', N'Тихон', N'Авксентьевич')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (7, N'Архипов', N'Тихон', N'Авксентьевич')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (8, N'Селиверстов', N'Тимур', N'Андреевич')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (9, N'Крюков', N'Тарас', N'Куприянович')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (10, N'Мартынова', N'Таисия', N'Степановна')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (11, N'Копылов', N'Соломон', N'Наумович')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (12, N'Копылов', N'Соломон', N'Наумович')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (13, N'Тимофеев', N'Севастьян', N'Серапионович')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (14, N'Гордеев', N'Панкратий', N'Филатович')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (15, N'Гордеев', N'Панкратий', N'Филатович')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (16, N'Зуев', N'Осип', N'Оскарович')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (17, N'Соболев', N'Моисей', N'Макарович')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (18, N'Щукин', N'Модест', N'Пантелеймонович')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (19, N'Никонов', N'Модест', N'Федорович')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (20, N'Анисимов', N'Мирон', N'Митрофанович')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (21, N'Анисимов', N'Мирон', N'Митрофанович')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (22, N'Егорова', N'Лиана', N'Ефимовна')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (23, N'Куликов', N'Корнелий', N'Германнович')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (24, N'Куликов', N'Корнелий', N'Германнович')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (25, N'Рожков', N'Казимир', N'Романович')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (26, N'Рожков', N'Казимир', N'Романович')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (27, N'Лаврентьева', N'Изольда', N'Валентиновна')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (28, N'Фомина', N'Зоя', N'Петровна')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (29, N'Зуев', N'Донат', N'Иванович')
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
ALTER TABLE [dbo].[CommisionCurator]  WITH CHECK ADD  CONSTRAINT [FK_CommisionCurator_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO
ALTER TABLE [dbo].[CommisionCurator] CHECK CONSTRAINT [FK_CommisionCurator_Department]
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
ALTER TABLE [dbo].[StudyPlan]  WITH CHECK ADD  CONSTRAINT [FK_StudyPlan_LessonType] FOREIGN KEY([LessonTypeId])
REFERENCES [dbo].[LessonType] ([Id])
GO
ALTER TABLE [dbo].[StudyPlan] CHECK CONSTRAINT [FK_StudyPlan_LessonType]
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
ALTER DATABASE [StatiscticsSchool2] SET  READ_WRITE 
GO
