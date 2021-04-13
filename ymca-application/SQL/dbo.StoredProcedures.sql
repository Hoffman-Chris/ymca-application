USE [cs341-project]
GO
/****** Object:  StoredProcedure [dbo].[Create-ProgramParticipant]    Script Date: 3/28/2021 11:33:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Create-ProgramParticipant] @ProgramId int, @UserId nvarchar(128)
AS
INSERT INTO ProgramParticipants (ProgramId, UserId)
	VALUES (@ProgramId, @UserId)
GO
/****** Object:  StoredProcedure [dbo].[Get-FreeSpotsCount]    Script Date: 3/28/2021 11:33:58 AM ******/
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
/****** Object:  StoredProcedure [dbo].[Get-RegisterProgramInfo]    Script Date: 3/28/2021 11:33:58 AM ******/
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
/****** Object:  StoredProcedure [dbo].[Get-UserProgramInfo]    Script Date: 3/28/2021 11:33:58 AM ******/
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
/****** Object:  StoredProcedure [dbo].[Verify-UserEligibility]    Script Date: 3/28/2021 11:33:58 AM ******/
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
