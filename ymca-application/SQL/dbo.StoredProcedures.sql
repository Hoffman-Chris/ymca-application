USE [cs341-project]
GO
/****** Object:  StoredProcedure [dbo].[Create-ProgramParticipant]    Script Date: 5/5/2021 9:53:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE [dbo].[Create-ProgramParticipant] @ProgramId int, @UserId nvarchar(128)
AS
	INSERT INTO ProgramParticipants (ProgramId, UserId)
		VALUES (@ProgramId, @UserId)

	UPDATE Program
		SET CurrentParticipants = CurrentParticipants + 1
		WHERE ProgramId = @ProgramId
GO
/****** Object:  StoredProcedure [dbo].[Delete-ProgramParticipant]    Script Date: 5/5/2021 9:53:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE [dbo].[Delete-ProgramParticipant] @ProgramId int, @UserId nvarchar(128)
AS
	DELETE FROM ProgramParticipants
		WHERE ProgramParticipants.ProgramId = @ProgramId AND ProgramParticipants.UserId = @UserId
		
	UPDATE Program
		SET CurrentParticipants = CurrentParticipants - 1
		WHERE ProgramId = @ProgramId
GO
/****** Object:  StoredProcedure [dbo].[Disable-User]    Script Date: 5/5/2021 9:53:33 AM ******/
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
/****** Object:  StoredProcedure [dbo].[Get-FreeSpotsCount]    Script Date: 5/5/2021 9:53:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Get-FreeSpotsCount] @ProgramId int
AS
	IF EXISTS (SELECT ProgramId FROM ProgramParticipants WHERE ProgramId = @ProgramId)
		SELECT MaxParticipants - count(ProgramParticipants.ProgramId) AS 'Available'
			FROM Program
				JOIN ProgramParticipants
					ON Program.ProgramId = ProgramParticipants.ProgramId
			WHERE Program.ProgramId = @ProgramId
			GROUP BY MaxParticipants
	ELSE
		SELECT MaxParticipants AS 'Available'
		FROM Program
		WHERE Program.ProgramId = @ProgramId
GO
/****** Object:  StoredProcedure [dbo].[Get-ProgramParticipants]    Script Date: 5/5/2021 9:53:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Get-ProgramParticipants] @ProgramId int
AS
	SELECT FirstName, LastName, Email
		FROM AspNetUsers
			JOIN ProgramParticipants ON AspNetUsers.Id = ProgramParticipants.UserId
			JOIN Program ON ProgramParticipants.ProgramId = Program.ProgramId
		WHERE Program.ProgramId = @ProgramId
GO
/****** Object:  StoredProcedure [dbo].[Get-RegisterProgramInfo]    Script Date: 5/5/2021 9:53:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Get-RegisterProgramInfo] @ProgramId int
AS
	SELECT ProgramId, StartTime, EndTime, StartDate, EndDate, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday
		FROM Program
		WHERE ProgramId = @ProgramId
GO
/****** Object:  StoredProcedure [dbo].[Get-UserProgramInfo]    Script Date: 5/5/2021 9:53:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Get-UserProgramInfo] @UserId nvarchar(128)
AS
	SELECT ProgramId, StartTime, EndTime, StartDate, EndDate, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday
		FROM Program
		WHERE ProgramId IN (SELECT ProgramId FROM ProgramParticipants WHERE UserId = @UserId)
GO
/****** Object:  StoredProcedure [dbo].[Verify-UserEligibility]    Script Date: 5/5/2021 9:53:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Verify-UserEligibility] @ProgramId int, @UserId nvarchar(128)
AS
	SELECT 1
		FROM ProgramParticipants
		WHERE ProgramId = @ProgramId AND UserId = @UserId
GO
