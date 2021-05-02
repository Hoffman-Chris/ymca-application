USE [cs341-project]
GO

/****** Object:  StoredProcedure [dbo].[Disable-User]    Script Date: 5/2/2021 2:23:17 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Disable-User] @UserId nvarchar(128)
AS
	UPDATE [AspNetUsers]
		SET [AspNetUsers].LockoutEndDateUtc = '9999-12-31', [AspNetUsers].Active = 0
		WHERE [AspNetUsers].Id = @UserId

	UPDATE [Program]
		SET [Program].CurrentParticipants = CurrentParticipants - 1
		WHERE [Program].ProgramId IN (SELECT [ProgramParticipants].ProgramId FROM [ProgramParticipants] WHERE [ProgramParticipants].UserId = @UserId)

	DELETE FROM [ProgramParticipants]
		WHERE [ProgramParticipants].UserId = @UserId
GO