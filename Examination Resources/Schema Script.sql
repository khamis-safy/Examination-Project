USE [ExaminationDB]
GO
CREATE TABLE [dbo].[tblExam](
	[ExamID] [nvarchar](50) NOT NULL,
	[TotalGrads] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_tblExam] PRIMARY KEY CLUSTERED 
(
	[ExamID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblExamQuestion](
	[QuestionID] [int] IDENTITY(1,1) NOT NULL,
	[ExamID] [nvarchar](50) NOT NULL,
	[QuestionText] [nvarchar](max) NOT NULL,
	[QuestionType] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_tblExamQuestion] PRIMARY KEY CLUSTERED 
(
	[QuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblQuestionChoice](
	[ChoiceID] [int] IDENTITY(1,1) NOT NULL,
	[QuestionID] [int] NOT NULL,
	[ChoiceText] [nvarchar](max) NOT NULL,
	[IsCorrectChoice] [bit] NOT NULL,
 CONSTRAINT [PK_tblQuestionChoice] PRIMARY KEY CLUSTERED 
(
	[ChoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblStudent](
	[StudentID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
 CONSTRAINT [PK_tblStudent] PRIMARY KEY CLUSTERED 
(
	[StudentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblStudentsAnswer](
	[AnswerID] [int] IDENTITY(1,1) NOT NULL,
	[StudentID] [int] NOT NULL,
	[QuestionID] [int] NOT NULL,
	[AnswerText] [nvarchar](max) NULL,
	[SelectedChoiceID] [int] NULL,
	[IsCorrectAnswer] [bit] NOT NULL,
 CONSTRAINT [PK_tblStudentsAnswer] PRIMARY KEY CLUSTERED 
(
	[AnswerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[tblStudentsInExam](
	[ExamID] [nvarchar](50) NOT NULL,
	[StudentID] [int] NOT NULL,
	[JoinedAt] [datetime] NOT NULL,
	[FinishedAt] [datetime] NOT NULL,
	[TotalGrade] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tblExamQuestion]  WITH CHECK ADD  CONSTRAINT [FK_tblExamQuestion_tblExam] FOREIGN KEY([ExamID])
REFERENCES [dbo].[tblExam] ([ExamID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblExamQuestion] CHECK CONSTRAINT [FK_tblExamQuestion_tblExam]
GO
ALTER TABLE [dbo].[tblQuestionChoice]  WITH CHECK ADD  CONSTRAINT [FK_tblQuestionChoice_tblExamQuestion] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[tblExamQuestion] ([QuestionID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblQuestionChoice] CHECK CONSTRAINT [FK_tblQuestionChoice_tblExamQuestion]
GO
ALTER TABLE [dbo].[tblStudentsAnswer]  WITH CHECK ADD  CONSTRAINT [FK_tblStudentsAnswer_tblExamQuestion] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[tblExamQuestion] ([QuestionID])
GO
ALTER TABLE [dbo].[tblStudentsAnswer] CHECK CONSTRAINT [FK_tblStudentsAnswer_tblExamQuestion]
GO
ALTER TABLE [dbo].[tblStudentsAnswer]  WITH CHECK ADD  CONSTRAINT [FK_tblStudentsAnswer_tblStudent] FOREIGN KEY([StudentID])
REFERENCES [dbo].[tblStudent] ([StudentID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblStudentsAnswer] CHECK CONSTRAINT [FK_tblStudentsAnswer_tblStudent]
GO
ALTER TABLE [dbo].[tblStudentsInExam]  WITH CHECK ADD  CONSTRAINT [FK_tblStudentsInExam_tblExam] FOREIGN KEY([ExamID])
REFERENCES [dbo].[tblExam] ([ExamID])
GO
ALTER TABLE [dbo].[tblStudentsInExam] CHECK CONSTRAINT [FK_tblStudentsInExam_tblExam]
GO
ALTER TABLE [dbo].[tblStudentsInExam]  WITH CHECK ADD  CONSTRAINT [FK_tblStudentsInExam_tblStudent] FOREIGN KEY([StudentID])
REFERENCES [dbo].[tblStudent] ([StudentID])
GO
ALTER TABLE [dbo].[tblStudentsInExam] CHECK CONSTRAINT [FK_tblStudentsInExam_tblStudent]
GO
