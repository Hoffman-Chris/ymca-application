USE [cs341-project]
GO
/****** Object:  Table [dbo].[Instructor]    Script Date: 3/14/2021 11:44:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Instructor](
	[InstructorId] [int] IDENTITY(1,1) NOT NULL,
	[InstructorFirstName] [nvarchar](255) NOT NULL,
	[InstructorMiddleName] [nvarchar](255) NULL,
	[InstructorLastName] [nvarchar](255) NOT NULL,
	[InstructorFullName] [nchar](765) NOT NULL,
 CONSTRAINT [PK_Instructor] PRIMARY KEY CLUSTERED 
(
	[InstructorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Instructor] ON 

INSERT [dbo].[Instructor] ([InstructorId], [InstructorFirstName], [InstructorMiddleName], [InstructorLastName], [InstructorFullName]) VALUES (1, N'Free', N'M.', N'Willy', N'Free M. Willy                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ')
SET IDENTITY_INSERT [dbo].[Instructor] OFF
GO
