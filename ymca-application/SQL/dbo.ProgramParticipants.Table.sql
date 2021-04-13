USE [cs341-project]
GO
/****** Object:  Table [dbo].[ProgramParticipants]    Script Date: 3/28/2021 11:33:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProgramParticipants](
	[ProgramId] [int] NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_ProgramParticipants] PRIMARY KEY CLUSTERED 
(
	[ProgramId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProgramParticipants]  WITH NOCHECK ADD  CONSTRAINT [FK_ProgramParticipants_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProgramParticipants] CHECK CONSTRAINT [FK_ProgramParticipants_AspNetUsers]
GO
ALTER TABLE [dbo].[ProgramParticipants]  WITH NOCHECK ADD  CONSTRAINT [FK_ProgramParticipants_Program] FOREIGN KEY([ProgramId])
REFERENCES [dbo].[Program] ([ProgramId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProgramParticipants] CHECK CONSTRAINT [FK_ProgramParticipants_Program]
GO
