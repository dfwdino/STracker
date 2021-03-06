﻿CREATE TABLE [People].[SocalSites] (
    [ID]       INT           IDENTITY (1, 1) NOT NULL,
    [SiteName] NVARCHAR (50) NOT NULL,
    [Link]     NVARCHAR (50) NOT NULL,
    [PersonID] INT           NULL,
    [Deleted]  BIT           CONSTRAINT [DF_SocalSites_Deleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_SocalSites] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_SocalSites_People] FOREIGN KEY ([PersonID]) REFERENCES [People].[People] ([ID])
);



