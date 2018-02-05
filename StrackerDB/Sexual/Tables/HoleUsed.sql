CREATE TABLE [Sexual].[HoleUsed] (
    [ID]      INT IDENTITY (1, 1) NOT NULL,
    [HoleID]  INT NOT NULL,
    [Deleted] BIT CONSTRAINT [DF_HoleUsed_Deleted] DEFAULT ((0)) NOT NULL,
    [EventID] INT NOT NULL,
    CONSTRAINT [PK_HoleUsed] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HoleUsed_Event] FOREIGN KEY ([EventID]) REFERENCES [Events].[Event] ([ID]),
    CONSTRAINT [FK_HoleUsed_Holes] FOREIGN KEY ([HoleID]) REFERENCES [Sexual].[Holes] ([ID])
);



