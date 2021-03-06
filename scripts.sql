USE [band_tracker]
GO
/****** Object:  Table [dbo].[bands]    Script Date: 6/16/2017 5:04:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bands](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[bands_venues]    Script Date: 6/16/2017 5:04:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bands_venues](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[band_id] [int] NULL,
	[venue_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[venues]    Script Date: 6/16/2017 5:04:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[venues](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[venues_source]    Script Date: 6/16/2017 5:04:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[venues_source](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NULL
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[bands] ON 

INSERT [dbo].[bands] ([id], [name]) VALUES (1, N'Parappa the Rappa')
INSERT [dbo].[bands] ([id], [name]) VALUES (2, N'Mega Parappa the Rappa')
INSERT [dbo].[bands] ([id], [name]) VALUES (3, N'Ultra Mega Parappa the Rappa')
INSERT [dbo].[bands] ([id], [name]) VALUES (4, N'Super Sonic Parappa The Rappa')
SET IDENTITY_INSERT [dbo].[bands] OFF
SET IDENTITY_INSERT [dbo].[bands_venues] ON 

INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (1, 1, 1)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (2, 2, 1)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (5, 1, 1)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (4, 3, 1)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (7, 2, 3)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (8, 3, 3)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (9, 4, 1)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (10, 3, 15)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (6, 1, 3)
SET IDENTITY_INSERT [dbo].[bands_venues] OFF
SET IDENTITY_INSERT [dbo].[venues] ON 

INSERT [dbo].[venues] ([id], [name]) VALUES (3, N'Club Fun')
INSERT [dbo].[venues] ([id], [name]) VALUES (15, N'Onion Master''s Dojo')
SET IDENTITY_INSERT [dbo].[venues] OFF
SET IDENTITY_INSERT [dbo].[venues_source] ON 

INSERT [dbo].[venues_source] ([id], [name]) VALUES (3, N'Club Fun')
INSERT [dbo].[venues_source] ([id], [name]) VALUES (15, N'Onion Master''s Dojo')
INSERT [dbo].[venues_source] ([id], [name]) VALUES (0, N'Onion Master''s Home')
SET IDENTITY_INSERT [dbo].[venues_source] OFF
