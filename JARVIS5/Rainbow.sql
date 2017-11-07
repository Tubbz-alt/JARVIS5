USE [shawn_db]
GO

/****** Object:  Table [dbo].[RAINBOW_97]    Script Date: 11/7/2017 9:43:02 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[RAINBOW_97](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
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

select 'drop table '+TABLE_NAME, * from INFORMATION_SCHEMA.TABLES where TABLE_NAME like 'RAINBOW%'
drop table RAINBOW_91
drop table RAINBOW_92
drop table RAINBOW_93
drop table RAINBOW_94
drop table RAINBOW_95
drop table RAINBOW_96
drop table RAINBOW_123
drop table RAINBOW_124
drop table RAINBOW_97
drop table RAINBOW_125
drop table RAINBOW_126
drop table RAINBOW_98
drop table RAINBOW_99
drop table RAINBOW_100
drop table RAINBOW_101
drop table RAINBOW_102
drop table RAINBOW_103
drop table RAINBOW_104
drop table RAINBOW_105
drop table RAINBOW_106
drop table RAINBOW_107
drop table RAINBOW_108
drop table RAINBOW_109
drop table RAINBOW_110
drop table RAINBOW_111
drop table RAINBOW_112
drop table RAINBOW_113
drop table RAINBOW_114
drop table RAINBOW_115
drop table RAINBOW_116
drop table RAINBOW_117
drop table RAINBOW_118
drop table RAINBOW_119
drop table RAINBOW_120
drop table RAINBOW_121
drop table RAINBOW_122
drop table RAINBOW_48
drop table RAINBOW_49
drop table RAINBOW_50
drop table RAINBOW_51
drop table RAINBOW_52
drop table RAINBOW_53
drop table RAINBOW_54
drop table RAINBOW_55
drop table RAINBOW_56
drop table RAINBOW_57
drop table RAINBOW_32
drop table RAINBOW_33
drop table RAINBOW_34
drop table RAINBOW_35
drop table RAINBOW_36
drop table RAINBOW_37
drop table RAINBOW_38
drop table RAINBOW_39
drop table RAINBOW_40
drop table RAINBOW_41
drop table RAINBOW_42
drop table RAINBOW_43
drop table RAINBOW_44
drop table RAINBOW_45
drop table RAINBOW_46
drop table RAINBOW_47
drop table RAINBOW_58
drop table RAINBOW_59
drop table RAINBOW_60
drop table RAINBOW_61
drop table RAINBOW_62
drop table RAINBOW_63
drop table RAINBOW_64
