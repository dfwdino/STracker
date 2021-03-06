﻿CREATE TABLE [Events].[Locations] (
    [ID]      INT           IDENTITY (1, 1) NOT NULL,
    [Deleted] BIT           CONSTRAINT [DF_Locations_Deleted] DEFAULT ((0)) NOT NULL,
    [Name]    NVARCHAR (50) NOT NULL,
    [OwnerID] INT           NULL,
    CONSTRAINT [PK_Locations] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Locations_People] FOREIGN KEY ([OwnerID]) REFERENCES [People].[People] ([ID])
);



