CREATE TABLE [Sexual].[Holes] (
    [ID]      INT           IDENTITY (1, 1) NOT NULL,
    [Area]    NVARCHAR (50) NOT NULL,
    [Deleted] BIT           CONSTRAINT [DF_Holes_Deleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Holes] PRIMARY KEY CLUSTERED ([ID] ASC)
);



