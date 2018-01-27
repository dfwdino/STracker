CREATE TABLE [dbo].[Fucking] (
    [ID]           INT IDENTITY (1, 1) NOT NULL,
    [PoistionID]   INT NOT NULL,
    [EventID]      INT NOT NULL,
    [TopPerson]    INT NOT NULL,
    [BottomPerson] INT NOT NULL,
    [Deleted]      BIT CONSTRAINT [DF_Fucking_Deleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Fucking] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Fucking_BottomPerson] FOREIGN KEY ([BottomPerson]) REFERENCES [dbo].[People] ([ID]),
    CONSTRAINT [FK_Fucking_Event] FOREIGN KEY ([EventID]) REFERENCES [dbo].[Event] ([ID]),
    CONSTRAINT [FK_Fucking_Positions] FOREIGN KEY ([PoistionID]) REFERENCES [dbo].[Positions] ([ID]),
    CONSTRAINT [FK_TopPerson] FOREIGN KEY ([TopPerson]) REFERENCES [dbo].[People] ([ID])
);

