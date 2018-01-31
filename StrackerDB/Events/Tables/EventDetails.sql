CREATE TABLE [Events].[EventDetails] (
    [ID]         INT IDENTITY (1, 1) NOT NULL,
    [EventID]    INT NOT NULL,
    [WhoDid]     INT NOT NULL,
    [ActionDone] INT NOT NULL,
    [ToWho]      INT NOT NULL,
    [Deleted]    BIT CONSTRAINT [DF_EventDetails_Deleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_EventDetails] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ActionsToEventActions] FOREIGN KEY ([ActionDone]) REFERENCES [Events].[EventActions] ([ID]),
    CONSTRAINT [FK_EventDetails_Event] FOREIGN KEY ([EventID]) REFERENCES [Events].[Event] ([ID]),
    CONSTRAINT [FK_PeopleToWho] FOREIGN KEY ([ToWho]) REFERENCES [People].[People] ([ID]),
    CONSTRAINT [FK_PeopleWhoDid] FOREIGN KEY ([WhoDid]) REFERENCES [People].[People] ([ID])
);

