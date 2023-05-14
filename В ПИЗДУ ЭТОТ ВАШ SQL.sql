USE [CollegeStatistics]
GO
/****** Object:  Table [dbo].[Attendance]    Script Date: 14.05.2023 21:45:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attendance](
	[LessonId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
	[IsAttented] [bit] NOT NULL,
 CONSTRAINT [PK_Attendance_1] PRIMARY KEY CLUSTERED 
(
	[LessonId] ASC,
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DayOfWeek]    Script Date: 14.05.2023 21:45:54 ******/
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
/****** Object:  Table [dbo].[Department]    Script Date: 14.05.2023 21:45:54 ******/
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
/****** Object:  Table [dbo].[EducationForm]    Script Date: 14.05.2023 21:45:54 ******/
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
/****** Object:  Table [dbo].[EmergencySituation]    Script Date: 14.05.2023 21:45:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmergencySituation](
	[Id] [int] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_EmergencySituation_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 14.05.2023 21:45:54 ******/
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
/****** Object:  Table [dbo].[Homework]    Script Date: 14.05.2023 21:45:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Homework](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Topic] [nvarchar](100) NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Homework] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HomeworkExecutionStatus]    Script Date: 14.05.2023 21:45:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HomeworkExecutionStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ExecutionStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HomeworkStudent]    Script Date: 14.05.2023 21:45:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HomeworkStudent](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LessonId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
	[HomeworkExecutionStatusId] [int] NOT NULL,
	[Mark] [smallint] NOT NULL,
 CONSTRAINT [PK_HomeworkStudent_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lesson]    Script Date: 14.05.2023 21:45:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lesson](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudyPlanRecordId] [int] NOT NULL,
	[GroupId] [int] NOT NULL,
	[Time] [time](0) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsRestoring] [bit] NOT NULL,
	[Date] [date] NOT NULL,
 CONSTRAINT [PK_Lesson_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LessonHomework]    Script Date: 14.05.2023 21:45:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LessonHomework](
	[LessonId] [int] NOT NULL,
	[HomeworkId] [int] NOT NULL,
	[Deadline] [smalldatetime] NOT NULL,
	[IssueDate] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_LessonHomework_1] PRIMARY KEY CLUSTERED 
(
	[LessonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LessonType]    Script Date: 14.05.2023 21:45:54 ******/
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
/****** Object:  Table [dbo].[NoteToLesson]    Script Date: 14.05.2023 21:45:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteToLesson](
	[Id] [int] NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_NoteToLesson_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteToStudent]    Script Date: 14.05.2023 21:45:54 ******/
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
/****** Object:  Table [dbo].[Speciality]    Script Date: 14.05.2023 21:45:54 ******/
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
/****** Object:  Table [dbo].[Student]    Script Date: 14.05.2023 21:45:54 ******/
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
/****** Object:  Table [dbo].[StudyPlan]    Script Date: 14.05.2023 21:45:54 ******/
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
/****** Object:  Table [dbo].[StudyPlanRecord]    Script Date: 14.05.2023 21:45:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudyPlanRecord](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LessonTypeId] [int] NOT NULL,
	[DurationInLessons] [int] NOT NULL,
	[Topic] [nvarchar](max) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[StudyPlanId] [int] NOT NULL,
 CONSTRAINT [PK_StudyPlanRecord] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subject]    Script Date: 14.05.2023 21:45:54 ******/
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
/****** Object:  Table [dbo].[Teacher]    Script Date: 14.05.2023 21:45:54 ******/
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
/****** Object:  Table [dbo].[Timetable]    Script Date: 14.05.2023 21:45:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Timetable](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TeacherId] [int] NOT NULL,
	[StudyPlanId] [int] NOT NULL,
	[GroupId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Timetable] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimetableRecord]    Script Date: 14.05.2023 21:45:54 ******/
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
ALTER TABLE [dbo].[EmergencySituation]  WITH CHECK ADD  CONSTRAINT [FK_EmergencySituation_Lesson] FOREIGN KEY([Id])
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
ALTER TABLE [dbo].[HomeworkStudent]  WITH CHECK ADD  CONSTRAINT [FK_HomeworkStudent_Lesson] FOREIGN KEY([LessonId])
REFERENCES [dbo].[Lesson] ([Id])
GO
ALTER TABLE [dbo].[HomeworkStudent] CHECK CONSTRAINT [FK_HomeworkStudent_Lesson]
GO
ALTER TABLE [dbo].[HomeworkStudent]  WITH CHECK ADD  CONSTRAINT [FK_HomeworkStudent_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([Id])
GO
ALTER TABLE [dbo].[HomeworkStudent] CHECK CONSTRAINT [FK_HomeworkStudent_Student]
GO
ALTER TABLE [dbo].[HomeworkStudent]  WITH CHECK ADD  CONSTRAINT [FK_LessonHomework_HomeworkExecutionStatus] FOREIGN KEY([HomeworkExecutionStatusId])
REFERENCES [dbo].[HomeworkExecutionStatus] ([Id])
GO
ALTER TABLE [dbo].[HomeworkStudent] CHECK CONSTRAINT [FK_LessonHomework_HomeworkExecutionStatus]
GO
ALTER TABLE [dbo].[Lesson]  WITH CHECK ADD  CONSTRAINT [FK_Lesson_Group] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Group] ([Id])
GO
ALTER TABLE [dbo].[Lesson] CHECK CONSTRAINT [FK_Lesson_Group]
GO
ALTER TABLE [dbo].[Lesson]  WITH CHECK ADD  CONSTRAINT [FK_Lesson_StudyPlanRecord] FOREIGN KEY([StudyPlanRecordId])
REFERENCES [dbo].[StudyPlanRecord] ([Id])
GO
ALTER TABLE [dbo].[Lesson] CHECK CONSTRAINT [FK_Lesson_StudyPlanRecord]
GO
ALTER TABLE [dbo].[LessonHomework]  WITH CHECK ADD  CONSTRAINT [FK_LessonHomework_Homework1] FOREIGN KEY([HomeworkId])
REFERENCES [dbo].[Homework] ([Id])
GO
ALTER TABLE [dbo].[LessonHomework] CHECK CONSTRAINT [FK_LessonHomework_Homework1]
GO
ALTER TABLE [dbo].[LessonHomework]  WITH CHECK ADD  CONSTRAINT [FK_LessonHomework_Lesson] FOREIGN KEY([LessonId])
REFERENCES [dbo].[Lesson] ([Id])
GO
ALTER TABLE [dbo].[LessonHomework] CHECK CONSTRAINT [FK_LessonHomework_Lesson]
GO
ALTER TABLE [dbo].[NoteToLesson]  WITH CHECK ADD  CONSTRAINT [FK_NoteToLesson_Lesson] FOREIGN KEY([Id])
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
ALTER TABLE [dbo].[Timetable]  WITH CHECK ADD  CONSTRAINT [FK_Timetable_StudyPlan] FOREIGN KEY([StudyPlanId])
REFERENCES [dbo].[StudyPlan] ([Id])
GO
ALTER TABLE [dbo].[Timetable] CHECK CONSTRAINT [FK_Timetable_StudyPlan]
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
