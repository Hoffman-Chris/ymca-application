USE [cs341-project]
GO
/****** Object:  Table [dbo].[Program]    Script Date: 3/14/2021 11:44:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Program](
	[ProgramId] [int] IDENTITY(1,1) NOT NULL,
	[ProgramName] [nvarchar](255) NOT NULL,
	[ProgramDescription] [nvarchar](255) NOT NULL,
	[StartTime] [time](7) NULL,
	[EndTime] [time](7) NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[Monday] [bit] NULL,
	[Tuesday] [bit] NULL,
	[Wednesday] [bit] NULL,
	[Thursday] [bit] NULL,
	[Friday] [bit] NULL,
	[Saturday] [bit] NULL,
	[Sunday] [bit] NULL,
	[InstructorId] [int] NULL,
	[MaxParticipants] [int] NOT NULL,
	[MemberPrice] [decimal](5, 2) NOT NULL,
	[NonMemberPrice] [decimal](5, 2) NOT NULL,
 CONSTRAINT [PK_Program] PRIMARY KEY CLUSTERED 
(
	[ProgramId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Program] ON 

INSERT [dbo].[Program] ([ProgramId], [ProgramName], [ProgramDescription], [StartTime], [EndTime], [StartDate], [EndDate], [Monday], [Tuesday], [Wednesday], [Thursday], [Friday], [Saturday], [Sunday], [InstructorId], [MaxParticipants], [MemberPrice], [NonMemberPrice]) VALUES (1, N'Taekwondo', N'A karate class for kids.', NULL, NULL, CAST(N'2021-03-01' AS Date), CAST(N'2021-03-31' AS Date), 1, NULL, 1, NULL, 1, NULL, NULL, 1, 10, CAST(149.99 AS Decimal(5, 2)), CAST(299.99 AS Decimal(5, 2)))
SET IDENTITY_INSERT [dbo].[Program] OFF
GO
ALTER TABLE [dbo].[Program]  WITH CHECK ADD  CONSTRAINT [FK_Program_Instructor] FOREIGN KEY([InstructorId])
REFERENCES [dbo].[Instructor] ([InstructorId])
GO
ALTER TABLE [dbo].[Program] CHECK CONSTRAINT [FK_Program_Instructor]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'References primary key Instructor Id from Instructor table.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Program', @level2type=N'CONSTRAINT',@level2name=N'FK_Program_Instructor'
GO
