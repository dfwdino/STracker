CREATE TABLE [dbo].[EventDetails] (
    [ID]         INT IDENTITY (1, 1) NOT NULL,
    [EventID]    INT NOT NULL,
    [WhoDid]     INT NOT NULL,
    [ActionDone] INT NOT NULL,
    [ToWho]      INT NOT NULL,
    [Deleted]    BIT CONSTRAINT [DF_EventDetails_Deleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_EventDetails] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ActionsToEventActions] FOREIGN KEY ([ActionDone]) REFERENCES [dbo].[Actions] ([ID]),
    CONSTRAINT [FK_EventDetails_Event] FOREIGN KEY ([EventID]) REFERENCES [dbo].[Event] ([ID]),
    CONSTRAINT [FK_PeopleToWho] FOREIGN KEY ([ToWho]) REFERENCES [dbo].[People] ([ID]),
    CONSTRAINT [FK_PeopleWhoDid] FOREIGN KEY ([WhoDid]) REFERENCES [dbo].[People] ([ID])
);

