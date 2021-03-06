USE [shawn_db]
GO

/****** Object:  Table [dbo].[RAINBOW_97]    Script Date: 11/7/2017 11:28:43 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[RAINBOW_97](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Word] [varchar](255) NOT NULL,
	[FirstLetter] [varchar](1) NOT NULL,
	[LetterCount] [int] NOT NULL,
	[MD5] [varchar](255) NULL,
	[SHA] [varchar](255) NULL,
	[SHA1] [varchar](255) NULL,
	[SHA2_256] [varchar](255) NULL,
	[SHA2_512] [varchar](255) NULL,
 CONSTRAINT [PK_RAINBOW_97] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

SET ANSI_PADDING ON

GO

/****** Object:  Index [IX_FIRSTLETTER_97]    Script Date: 11/7/2017 11:28:43 AM ******/
CREATE NONCLUSTERED INDEX [IX_FIRSTLETTER_97] ON [dbo].[RAINBOW_97]
(
	[FirstLetter] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

/****** Object:  Index [IX_LETTERCOUNT_97]    Script Date: 11/7/2017 11:28:43 AM ******/
CREATE NONCLUSTERED INDEX [IX_LETTERCOUNT_97] ON [dbo].[RAINBOW_97]
(
	[LetterCount] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

SET ANSI_PADDING ON

GO

/****** Object:  Index [IX_MD5_97]    Script Date: 11/7/2017 11:28:43 AM ******/
CREATE NONCLUSTERED INDEX [IX_MD5_97] ON [dbo].[RAINBOW_97]
(
	[MD5] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

SET ANSI_PADDING ON

GO

/****** Object:  Index [IX_SHA_97]    Script Date: 11/7/2017 11:28:43 AM ******/
CREATE NONCLUSTERED INDEX [IX_SHA_97] ON [dbo].[RAINBOW_97]
(
	[SHA] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

SET ANSI_PADDING ON

GO

/****** Object:  Index [IX_SHA1_97]    Script Date: 11/7/2017 11:28:43 AM ******/
CREATE NONCLUSTERED INDEX [IX_SHA1_97] ON [dbo].[RAINBOW_97]
(
	[SHA1] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

SET ANSI_PADDING ON

GO

/****** Object:  Index [IX_SHA2_256_97]    Script Date: 11/7/2017 11:28:43 AM ******/
CREATE NONCLUSTERED INDEX [IX_SHA2_256_97] ON [dbo].[RAINBOW_97]
(
	[SHA2_256] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

SET ANSI_PADDING ON

GO

/****** Object:  Index [IX_SHA2_512_97]    Script Date: 11/7/2017 11:28:43 AM ******/
CREATE NONCLUSTERED INDEX [IX_SHA2_512_97] ON [dbo].[RAINBOW_97]
(
	[SHA2_512] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

SET ANSI_PADDING ON

GO

/****** Object:  Index [IX_WORD_97]    Script Date: 11/7/2017 11:28:43 AM ******/
CREATE NONCLUSTERED INDEX [IX_WORD_97] ON [dbo].[RAINBOW_97]
(
	[Word] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


