CREATE TABLE [Events].[EventLocations] (
    [ID]         INT IDENTITY (1, 1) NOT NULL,
    [Deleted]    BIT CONSTRAINT [DF_EventLocations_Deleted] DEFAULT ((0)) NOT NULL,
    [EventID]    INT NOT NULL,
    [LocationID] INT NOT NULL,
    CONSTRAINT [PK_EventLocations] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_EventLocations_Event] FOREIGN KEY ([EventID]) REFERENCES [Events].[Event] ([ID]),
    CONSTRAINT [FK_EventLocations_Locations] FOREIGN KEY ([LocationID]) REFERENCES [Events].[Locations] ([ID])
);

