CREATE TABLE [People].[STIREsults] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [STI]         NVARCHAR (50) NOT NULL,
    [Result]      NVARCHAR (50) NOT NULL,
    [PersonID]    INT           NULL,
    [Deleted]     BIT           CONSTRAINT [DF_STIREsults_Deleted] DEFAULT ((0)) NOT NULL,
    [ResultsDate] DATE          NULL,
    CONSTRAINT [PK_STIREsults] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_STIREsults_People] FOREIGN KEY ([PersonID]) REFERENCES [People].[People] ([ID])
);





