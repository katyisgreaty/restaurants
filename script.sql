CREATE DATABASE [restaurants]
GO



USE [restaurants]
GO
/****** Object:  Table [dbo].[cuisine]    Script Date: 2/22/2017 4:48:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cuisine](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[restaurant]    Script Date: 2/22/2017 4:48:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[restaurant](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[price] [varchar](4) NULL,
	[vibe] [varchar](255) NULL,
	[cuisine_id] [int] NULL
) ON [PRIMARY]

GO
