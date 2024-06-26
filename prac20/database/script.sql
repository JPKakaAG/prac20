USE [praktos20]
GO
/****** Object:  Table [dbo].[Заказы]    Script Date: 29.05.2024 5:19:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Заказы](
	[Дата] [date] NULL,
	[НомерЗаказа] [int] NOT NULL,
	[Клиент] [nvarchar](50) NULL,
	[МаркаАвтомобиля] [nvarchar](50) NULL,
	[КодРаботы] [int] NULL,
	[КодИсполнителя] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[НомерЗаказа] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[СведенияОКлиентах]    Script Date: 29.05.2024 5:19:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[СведенияОКлиентах](
	[Клиент] [nvarchar](50) NOT NULL,
	[НаименованиеОбъекта] [nvarchar](100) NULL,
	[АдресОбъекта] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Клиент] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[СправочникВидовРабот]    Script Date: 29.05.2024 5:19:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[СправочникВидовРабот](
	[КодРаботы] [int] NOT NULL,
	[МаркаАвтомобиля] [nvarchar](50) NULL,
	[НаименованиеРаботы] [nvarchar](100) NULL,
	[СтоимостьРаботы] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[КодРаботы] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[СправочникИсполнителейРабот]    Script Date: 29.05.2024 5:19:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[СправочникИсполнителейРабот](
	[КодИсполнителя] [int] NOT NULL,
	[НаименованиеОрганизации] [nvarchar](100) NULL,
	[Адрес] [nvarchar](100) NULL,
	[Телефон] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[КодИсполнителя] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Заказы]  WITH CHECK ADD  CONSTRAINT [FK_Заказы_Клиент] FOREIGN KEY([Клиент])
REFERENCES [dbo].[СведенияОКлиентах] ([Клиент])
GO
ALTER TABLE [dbo].[Заказы] CHECK CONSTRAINT [FK_Заказы_Клиент]
GO
ALTER TABLE [dbo].[Заказы]  WITH CHECK ADD  CONSTRAINT [FK_Заказы_КодИсполнителя] FOREIGN KEY([КодИсполнителя])
REFERENCES [dbo].[СправочникИсполнителейРабот] ([КодИсполнителя])
GO
ALTER TABLE [dbo].[Заказы] CHECK CONSTRAINT [FK_Заказы_КодИсполнителя]
GO
ALTER TABLE [dbo].[Заказы]  WITH CHECK ADD  CONSTRAINT [FK_Заказы_КодРаботы] FOREIGN KEY([КодРаботы])
REFERENCES [dbo].[СправочникВидовРабот] ([КодРаботы])
GO
ALTER TABLE [dbo].[Заказы] CHECK CONSTRAINT [FK_Заказы_КодРаботы]
GO
