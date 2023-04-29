USE [StatisticsSchool]
GO
/****** Object:  Table [dbo].[Attendance]    Script Date: 29.04.2023 8:18:56 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CommisionCurator]    Script Date: 29.04.2023 8:18:56 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DayOfWeek]    Script Date: 29.04.2023 8:18:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DayOfWeek](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_DayOfWeek] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 29.04.2023 8:18:56 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EducationForm]    Script Date: 29.04.2023 8:18:56 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmergencySituation]    Script Date: 29.04.2023 8:18:56 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 29.04.2023 8:18:56 ******/
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
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Homework]    Script Date: 29.04.2023 8:18:56 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HomeworkExecutionStatus]    Script Date: 29.04.2023 8:18:56 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lesson]    Script Date: 29.04.2023 8:18:56 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LessonType]    Script Date: 29.04.2023 8:18:56 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteToLesson]    Script Date: 29.04.2023 8:18:56 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteToStudent]    Script Date: 29.04.2023 8:18:56 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Speciality]    Script Date: 29.04.2023 8:18:56 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 29.04.2023 8:18:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Patronymic] [nvarchar](50) NOT NULL,
	[GroupId] [int] NOT NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudyPlan]    Script Date: 29.04.2023 8:18:56 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subject]    Script Date: 29.04.2023 8:18:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subject](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Subject] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teacher]    Script Date: 29.04.2023 8:18:56 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Timetable]    Script Date: 29.04.2023 8:18:56 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimetableRecord]    Script Date: 29.04.2023 8:18:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimetableRecord](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Time] [time](0) NOT NULL,
	[DayOfWeekId] [int] NOT NULL,
	[TimetableId] [int] NOT NULL,
 CONSTRAINT [PK_TimetableRecord] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[DayOfWeek] ON 

INSERT [dbo].[DayOfWeek] ([Id], [Name]) VALUES (0, N'Воскресенье')
INSERT [dbo].[DayOfWeek] ([Id], [Name]) VALUES (1, N'Понедельник')
INSERT [dbo].[DayOfWeek] ([Id], [Name]) VALUES (2, N'Вторник')
INSERT [dbo].[DayOfWeek] ([Id], [Name]) VALUES (3, N'Среда')
INSERT [dbo].[DayOfWeek] ([Id], [Name]) VALUES (4, N'Четверг')
INSERT [dbo].[DayOfWeek] ([Id], [Name]) VALUES (5, N'Пятница')
INSERT [dbo].[DayOfWeek] ([Id], [Name]) VALUES (6, N'Суббота')
SET IDENTITY_INSERT [dbo].[DayOfWeek] OFF
GO
SET IDENTITY_INSERT [dbo].[Department] ON 

INSERT [dbo].[Department] ([Id], [Name]) VALUES (1, N'Телекоммуникационные')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (2, N'Какой-то другой')
SET IDENTITY_INSERT [dbo].[Department] OFF
GO
SET IDENTITY_INSERT [dbo].[EducationForm] ON 

INSERT [dbo].[EducationForm] ([ID], [Name]) VALUES (1, N'Бюджет')
INSERT [dbo].[EducationForm] ([ID], [Name]) VALUES (2, N'Коммерция')
SET IDENTITY_INSERT [dbo].[EducationForm] OFF
GO
SET IDENTITY_INSERT [dbo].[Group] ON 

INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId]) VALUES (1, 0, 2, 1, 2, 32, NULL)
INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId]) VALUES (2, 0, 1, 1, 1, 0, NULL)
INSERT [dbo].[Group] ([Id], [CreationYear], [EducationFormId], [TeacherId], [SpecialityId], [Number], [GroupLeaderId]) VALUES (3, 0, 2, 1, 1, 0, NULL)
SET IDENTITY_INSERT [dbo].[Group] OFF
GO
INSERT [dbo].[LessonType] ([Id], [Name]) VALUES (1, N'Лекция')
INSERT [dbo].[LessonType] ([Id], [Name]) VALUES (2, N'Лабораторная работа')
INSERT [dbo].[LessonType] ([Id], [Name]) VALUES (3, N'Практика')
GO
SET IDENTITY_INSERT [dbo].[Speciality] ON 

INSERT [dbo].[Speciality] ([Id], [Name], [DepartmentId]) VALUES (1, N'Почтовая связь', 1)
INSERT [dbo].[Speciality] ([Id], [Name], [DepartmentId]) VALUES (2, N'Специалист поkjk', 2)
INSERT [dbo].[Speciality] ([Id], [Name], [DepartmentId]) VALUES (3, N'Фцвkjk', 2)
SET IDENTITY_INSERT [dbo].[Speciality] OFF
GO
SET IDENTITY_INSERT [dbo].[Student] ON 

INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (3, N'цвввф', N'ФЫВфц', N'фывфцв', 2)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (4, N'hgfd', N'ghj', N'dfgh', 1)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (5, N'jh', N'ghjk', N'gfds', 1)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (6, N'asd', N'asdwd', N'asdawd', 1)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (7, N'asd', N'a', N'wda', 1)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (8, N'ajsd', N'jkdw', N'lk', 1)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (9, N'2dasd', N'ddasdw', N'gfa', 1)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (10, N'2dasd', N'ddasdw', N'gfa', 1)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (11, N'2dasd', N'ddasdw', N'gfa', 1)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (12, N'2dasd', N'ddasdw', N'gfa', 1)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (13, N'2dasd', N'ddasdw', N'gfa', 1)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (14, N'2dasd', N'ddasdw', N'gfa', 1)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (15, N'2dasd', N'ddasdw', N'gfa', 1)
INSERT [dbo].[Student] ([Id], [Surname], [Name], [Patronymic], [GroupId]) VALUES (16, N'2dasd', N'ddasdw', N'gfa', 1)
SET IDENTITY_INSERT [dbo].[Student] OFF
GO
SET IDENTITY_INSERT [dbo].[Subject] ON 

INSERT [dbo].[Subject] ([Id], [Name]) VALUES (1, N'Русский язык')
INSERT [dbo].[Subject] ([Id], [Name]) VALUES (2, N'Английский язык')
INSERT [dbo].[Subject] ([Id], [Name]) VALUES (3, N'Прокетирование и дизайн информационных систем')
SET IDENTITY_INSERT [dbo].[Subject] OFF
GO
SET IDENTITY_INSERT [dbo].[Teacher] ON 

INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (1, N'Плаксин', N'Никита', N'Александрович')
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
