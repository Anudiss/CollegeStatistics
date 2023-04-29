# ���������� ��������

## ���������, �������, �������, ��������

### ������� ����������� ��� �������������� �������� ���������� �������

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

INSERT [dbo].[CommisionCurator] ([Id], [Surname], [Name], [Patronymic], [DepartmentId]) VALUES (1, N'�������', N'�����', N'���������', 1)
INSERT [dbo].[CommisionCurator] ([Id], [Surname], [Name], [Patronymic], [DepartmentId]) VALUES (2, N'������', N'�����', N'���������', 2)
INSERT [dbo].[CommisionCurator] ([Id], [Surname], [Name], [Patronymic], [DepartmentId]) VALUES (3, N'��������', N'����', N'����������', 3)
SET IDENTITY_INSERT [dbo].[CommisionCurator] OFF
GO
SET IDENTITY_INSERT [dbo].[Department] ON 

INSERT [dbo].[Department] ([Id], [Name]) VALUES (1, N'�������������� ����������')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (2, N'������� ����������')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (3, N'�������������������� ���������� � �������������� ������������')
SET IDENTITY_INSERT [dbo].[Department] OFF
GO
SET IDENTITY_INSERT [dbo].[EducationForm] ON 

INSERT [dbo].[EducationForm] ([ID], [Name]) VALUES (1, N'������')
INSERT [dbo].[EducationForm] ([ID], [Name]) VALUES (2, N'���������')
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

INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (1, N'�����������', CAST(N'08:00:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (2, N'�����������', CAST(N'09:40:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (3, N'�����������', CAST(N'11:50:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (4, N'�����������', CAST(N'13:40:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (5, N'�����������', CAST(N'15:20:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (6, N'�����������', CAST(N'17:00:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (7, N'�����������', CAST(N'18:40:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (8, N'�����������', CAST(N'08:00:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (9, N'�����������', CAST(N'09:40:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (10, N'�����������', CAST(N'11:50:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (11, N'�����������', CAST(N'13:40:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (12, N'�����������', CAST(N'15:20:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (13, N'�����������', CAST(N'17:00:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (14, N'�����������', CAST(N'18:40:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (15, N'�������', CAST(N'08:00:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (16, N'�������', CAST(N'09:40:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (17, N'�������', CAST(N'11:50:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (18, N'�������', CAST(N'13:40:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (19, N'�������', CAST(N'15:20:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (20, N'�������', CAST(N'17:00:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (21, N'�������', CAST(N'18:40:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (22, N'�����', CAST(N'08:00:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (23, N'�����', CAST(N'09:40:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (24, N'�����', CAST(N'11:50:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (25, N'�����', CAST(N'13:40:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (26, N'�����', CAST(N'15:20:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (27, N'�����', CAST(N'17:00:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (28, N'�����', CAST(N'18:40:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (29, N'�������', CAST(N'08:00:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (30, N'�������', CAST(N'09:40:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (31, N'�������', CAST(N'11:50:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (32, N'�������', CAST(N'13:40:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (33, N'�������', CAST(N'15:20:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (34, N'�������', CAST(N'17:00:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (35, N'�������', CAST(N'18:40:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (36, N'�������', CAST(N'08:00:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (37, N'�������', CAST(N'09:40:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (38, N'�������', CAST(N'11:50:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (39, N'�������', CAST(N'13:40:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (40, N'�������', CAST(N'15:20:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (41, N'�������', CAST(N'17:00:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (42, N'�������', CAST(N'18:40:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (43, N'�������', CAST(N'08:00:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (44, N'�������', CAST(N'09:40:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (45, N'�������', CAST(N'11:30:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (46, N'�������', CAST(N'13:10:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (47, N'�������', CAST(N'14:50:00' AS Time))
INSERT [dbo].[LessonsStartTime] ([Id], [DayOfWeek], [Time]) VALUES (48, N'�������', CAST(N'16:30:00' AS Time))
SET IDENTITY_INSERT [dbo].[LessonsStartTime] OFF
GO
INSERT [dbo].[LessonType] ([Id], [Name]) VALUES (1, N'������')
INSERT [dbo].[LessonType] ([Id], [Name]) VALUES (2, N'������������ ������')
INSERT [dbo].[LessonType] ([Id], [Name]) VALUES (3, N'��������')
GO
SET IDENTITY_INSERT [dbo].[Speciality] ON 

INSERT [dbo].[Speciality] ([Id], [Name], [DepartmentId]) VALUES (1, N'������������� ��� ������', 1)
INSERT [dbo].[Speciality] ([Id], [Name], [DepartmentId]) VALUES (2, N'�����������', 1)
INSERT [dbo].[Speciality] ([Id], [Name], [DepartmentId]) VALUES (3, N'����������� ��� � �������������� ����������', 1)
INSERT [dbo].[Speciality] ([Id], [Name], [DepartmentId]) VALUES (4, N'���������� �� �������������� ��������', 1)
INSERT [dbo].[Speciality] ([Id], [Name], [DepartmentId]) VALUES (5, N'������������ ������� � ���������', 2)
INSERT [dbo].[Speciality] ([Id], [Name], [DepartmentId]) VALUES (6, N'������� � ��������� �����������������', 2)
INSERT [dbo].[Speciality] ([Id], [Name], [DepartmentId]) VALUES (7, N'�������� �����', 2)
INSERT [dbo].[Speciality] ([Id], [Name], [DepartmentId]) VALUES (8, N'����������� �������������� ������������ ������������������ ������', 3)
INSERT [dbo].[Speciality] ([Id], [Name], [DepartmentId]) VALUES (9, N'�������������� �������������������� �������', 3)
INSERT [dbo].[Speciality] ([Id], [Name], [DepartmentId]) VALUES (10, N'���� ����� � ������� ����������', 3)
INSERT [dbo].[Speciality] ([Id], [Name], [DepartmentId]) VALUES (11, N'�������������������� ���� � ������� �����', 3)
SET IDENTITY_INSERT [dbo].[Speciality] OFF
GO
SET IDENTITY_INSERT [dbo].[Student] ON 

INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (1, N'�������', N'������', N'�������������', NULL)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (2, N'�������', N'����', N'��������', 7)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (3, N'�������', N'������', N'��������', NULL)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (4, N'�����������', N'�������', N'��������', 7)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (5, N'�������', N'��������', N'��������', NULL)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (6, N'������', N'�����', N'����������', 1)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (7, N'������', N'������', N'��������', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (8, N'��������', N'����', N'����������', 1)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (9, N'�������', N'�����', N'��������', 1)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (10, N'�������', N'������', N'����������', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (11, N'�������', N'���������', N'���������', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (12, N'������', N'������', N'������������', 1)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (13, N'������', N'������', N'��������', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (14, N'�����', N'������', N'����������', NULL)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (15, N'��������', N'���', N'���������', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (16, N'����', N'�������', N'�������', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (17, N'����������', N'�����', N'��������', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (18, N'�������', N'�����', N'���������', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (19, N'��������', N'�����', N'��������', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (20, N'����������', N'�����', N'�������', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (21, N'�������', N'�����', N'�������������', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (22, N'�����', N'������', N'�������������', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (23, N'�������', N'������', N'���������', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (24, N'��������', N'���������', N'��������', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (25, N'������', N'���������', N'���������', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (26, N'�������', N'����', N'���������', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (27, N'�������', N'�����', N'�������', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (28, N'��������', N'�����', N'�������', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (29, N'������', N'������', N'����������', 6)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (30, N'��������', N'�������', N'�����', 6)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (31, N'�������', N'���������', N'�������', 6)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (32, N'������', N'����', N'��������', 7)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (33, N'�������', N'�����', N'�������������', 7)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (34, N'�����', N'�����', N'������������', 7)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (35, N'�������', N'����', N'��������', 7)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (36, N'��������', N'�����', N'����������', 7)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (37, N'�������', N'������', N'����������', 8)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (38, N'�������', N'�����', N'�����������', 8)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (39, N'��������', N'��������', N'����������', 8)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (40, N'���������', N'�����', N'����������', 8)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (41, N'������', N'�������', N'��������', 8)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (42, N'���������', N'�����', N'����������', 9)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (43, N'�����������', N'����������', N'���������', 9)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (44, N'���������', N'������', N'��������', 9)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (45, N'�������', N'��������', N'Ը�������', 9)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (46, N'�������', N'������', N'��������', 9)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (47, N'������', N'�������', N'����������', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (48, N'��������', N'�������', N'��������', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (49, N'��������', N'������', N'��������', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (50, N'�������', N'���', N'����������', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (51, N'�������', N'������', N'������', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (52, N'����������', N'��������', N'Ը�������', 10)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (53, N'���������', N'�������', N'��������', 11)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (54, N'��������', N'������', N'����������', 11)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (55, N'�������', N'����', N'���������', 11)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (56, N'���������', N'�������', N'����������', 11)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (57, N'���������', N'�����', N'�������������', 11)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (58, N'���������', N'����������', N'����������', 11)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (59, N'��������', N'�������', N'����������', 12)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (60, N'��������', N'�����', N'���������', 12)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (61, N'������', N'���������', N'������������', 12)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (62, N'��������', N'����������', N'�������������', 12)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (63, N'���������', N'���������', N'�������', 12)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (64, N'��������', N'��������', N'�������������', 12)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (65, N'��������', N'���������', N'����������', 13)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (66, N'��������', N'�����', N'���������', 13)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (67, N'�������', N'�����', N'���������', 13)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (68, N'�������', N'����', N'Ը�������', 13)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (69, N'��������', N'���', N'�������������', 13)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (70, N'������', N'������', N'��������', 13)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (71, N'��������', N'������', N'�������������', 14)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (72, N'�����', N'������', N'������������', 14)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (73, N'�������', N'�����', N'��������', 14)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (74, N'�������', N'������', N'�������', 14)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (75, N'�������', N'�����', N'����������', 14)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (76, N'�������', N'�������', N'���������', 14)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (77, N'���������', N'�����', N'����������', 15)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (78, N'��������', N'�������', N'���������', 15)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (79, N'�����', N'�����', N'��������', 15)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (80, N'������', N'�������', N'����������', 15)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (81, N'�������', N'������', N'���������', 15)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (82, N'��������', N'��������', N'���������', 15)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (83, N'������', N'����', N'�����', 16)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (84, N'���������', N'��������', N'��������', 16)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (85, N'�������', N'�������', N'�������������', 16)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (86, N'�������', N'������', N'����������', 16)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (87, N'�������', N'������', N'����������', 16)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (88, N'���������', N'����', N'����������', 16)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (89, N'��������', N'��������', N'�������������', 17)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (90, N'�������', N'�����', N'������������', 17)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (91, N'���������', N'���', N'��������', 17)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (92, N'�������', N'�����', N'�����������', 17)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (93, N'�������', N'�����', N'��������', 17)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (94, N'�������', N'���', N'����������', 17)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (95, N'�������', N'������', N'���������', 18)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (96, N'�����', N'������', N'����������', 18)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (97, N'������', N'���', N'�����������', 18)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (98, N'�������', N'�����', N'����������', 18)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (99, N'�������', N'�������', N'����������', 18)
GO
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (100, N'���������', N'�����', N'���������', 18)
SET IDENTITY_INSERT [dbo].[Student] OFF
GO
SET IDENTITY_INSERT [dbo].[Subject] ON 

INSERT [dbo].[Subject] ([Id], [Name]) VALUES (1, N'����������')
INSERT [dbo].[Subject] ([Id], [Name]) VALUES (2, N'�������')
INSERT [dbo].[Subject] ([Id], [Name]) VALUES (3, N'������')
INSERT [dbo].[Subject] ([Id], [Name]) VALUES (4, N'����������� ����')
INSERT [dbo].[Subject] ([Id], [Name]) VALUES (5, N'����������� ������� ����')
INSERT [dbo].[Subject] ([Id], [Name]) VALUES (6, N'����������� ��������� ����')
INSERT [dbo].[Subject] ([Id], [Name]) VALUES (7, N'���������� ������� ���������� � ���������������� ������������')
INSERT [dbo].[Subject] ([Id], [Name]) VALUES (8, N'������� ������ � ���� � �������� ���������������� �����������')
INSERT [dbo].[Subject] ([Id], [Name]) VALUES (9, N'���������')
INSERT [dbo].[Subject] ([Id], [Name]) VALUES (10, N'����������')
INSERT [dbo].[Subject] ([Id], [Name]) VALUES (11, N'�����������')
INSERT [dbo].[Subject] ([Id], [Name]) VALUES (12, N'����� � ���������� �������� � ���������� ��������')
INSERT [dbo].[Subject] ([Id], [Name]) VALUES (13, N'���������� ��������')
INSERT [dbo].[Subject] ([Id], [Name]) VALUES (14, N'����������')
SET IDENTITY_INSERT [dbo].[Subject] OFF
GO
SET IDENTITY_INSERT [dbo].[Teacher] ON 

INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (1, N'���������', N'��������', N'��������������')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (2, N'��������', N'�����', N'���������')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (3, N'�������', N'�����', N'���������')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (4, N'������', N'������', N'������������')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (5, N'������', N'������', N'������������')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (6, N'�������', N'�����', N'������������')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (7, N'�������', N'�����', N'������������')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (8, N'�����������', N'�����', N'���������')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (9, N'������', N'�����', N'�����������')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (10, N'���������', N'������', N'����������')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (11, N'�������', N'�������', N'��������')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (12, N'�������', N'�������', N'��������')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (13, N'��������', N'���������', N'������������')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (14, N'�������', N'���������', N'���������')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (15, N'�������', N'���������', N'���������')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (16, N'����', N'����', N'���������')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (17, N'�������', N'������', N'���������')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (18, N'�����', N'������', N'���������������')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (19, N'�������', N'������', N'���������')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (20, N'��������', N'�����', N'������������')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (21, N'��������', N'�����', N'������������')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (22, N'�������', N'�����', N'��������')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (23, N'�������', N'��������', N'�����������')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (24, N'�������', N'��������', N'�����������')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (25, N'������', N'�������', N'���������')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (26, N'������', N'�������', N'���������')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (27, N'�����������', N'�������', N'������������')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (28, N'������', N'���', N'��������')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (29, N'����', N'�����', N'��������')
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
