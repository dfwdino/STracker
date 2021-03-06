﻿CREATE TABLE [Events].[EventActions] (
    [ID]      INT           IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (50) NOT NULL,
    [Deleted] BIT           CONSTRAINT [DF_Actions_Deleted] DEFAULT ((0)) NOT NULL,
    [OwnerID] INT           NULL,
    CONSTRAINT [PK_Actions] PRIMARY KEY CLUSTERED ([ID] ASC)
);



