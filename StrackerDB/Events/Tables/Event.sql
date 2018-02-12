CREATE TABLE [Events].[Event] (
    [ID]            INT            IDENTITY (1, 1) NOT NULL,
    [Date]          DATETIME       NOT NULL,
    [Notes]         NVARCHAR (255) NULL,
    [OverAllRating] INT            NULL,
    [Remove]        BIT            CONSTRAINT [DF_Event_Delete] DEFAULT ((0)) NOT NULL,
    [OrgamNumber]   INT            CONSTRAINT [DF_Event_OrgamNumber] DEFAULT ((0)) NOT NULL,
    [Deleted]       BIT            CONSTRAINT [DF_Event_Deleted] DEFAULT ((0)) NOT NULL,
    [OwnerID]       INT            NULL,
    CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Event_Login] FOREIGN KEY ([OwnerID]) REFERENCES [Stracker].[Login] ([ID])
);



