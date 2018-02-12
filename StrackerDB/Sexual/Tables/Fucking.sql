CREATE TABLE [Sexual].[Fucking] (
    [ID]           INT IDENTITY (1, 1) NOT NULL,
    [PoistionID]   INT NOT NULL,
    [EventID]      INT NOT NULL,
    [TopPerson]    INT NOT NULL,
    [BottomPerson] INT NOT NULL,
    [Deleted]      BIT CONSTRAINT [DF_Fucking_Deleted] DEFAULT ((0)) NOT NULL,
    [OwnerID]      INT NULL,
    CONSTRAINT [PK_Fucking] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Fucking_BottomPerson] FOREIGN KEY ([BottomPerson]) REFERENCES [People].[People] ([ID]),
    CONSTRAINT [FK_Fucking_Event] FOREIGN KEY ([EventID]) REFERENCES [Events].[Event] ([ID]),
    CONSTRAINT [FK_Fucking_Positions] FOREIGN KEY ([PoistionID]) REFERENCES [Sexual].[Positions] ([ID]),
    CONSTRAINT [FK_TopPerson] FOREIGN KEY ([TopPerson]) REFERENCES [People].[People] ([ID])
);



