﻿CREATE TABLE [Sexual].[Positions] (
    [ID]      INT           IDENTITY (1, 1) NOT NULL,
    [Type]    NVARCHAR (50) NOT NULL,
    [Deleted] BIT           CONSTRAINT [DF_Positions_Removed] DEFAULT ((0)) NOT NULL,
    [OwnerID] INT           NULL,
    CONSTRAINT [PK_Positions] PRIMARY KEY CLUSTERED ([ID] ASC)
);





