USE [cs341-project]
GO

/****** Object:  StoredProcedure [dbo].[Delete-ProgramParticipant]    Script Date: 4/24/2021 12:52:06 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE OR ALTER PROCEDURE [dbo].[Delete-ProgramParticipant] @ProgramId int, @UserId nvarchar(128)
AS
	DELETE FROM ProgramParticipants
		WHERE ProgramParticipants.ProgramId = @ProgramId AND ProgramParticipants.UserId = @UserId
		
	UPDATE Program
		SET CurrentParticipants = CurrentParticipants - 1
		WHERE ProgramId = @ProgramId
GO


USE [cs341-project]
GO

/****** Object:  StoredProcedure [dbo].[Create-ProgramParticipant]    Script Date: 4/24/2021 12:52:01 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE OR ALTER PROCEDURE [dbo].[Create-ProgramParticipant] @ProgramId int, @UserId nvarchar(128)
AS
	INSERT INTO ProgramParticipants (ProgramId, UserId)
		VALUES (@ProgramId, @UserId)

	UPDATE Program
		SET CurrentParticipants = CurrentParticipants + 1
		WHERE ProgramId = @ProgramId
GO


