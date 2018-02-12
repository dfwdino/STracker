CREATE TABLE [People].[People] (
    [ID]      INT            IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (50)  NOT NULL,
    [Notes]   NVARCHAR (MAX) NULL,
    [Hide]    BIT            CONSTRAINT [DF_People_Hide] DEFAULT ((0)) NOT NULL,
    [Deleted] BIT            CONSTRAINT [DF_People_Deleted] DEFAULT ((0)) NOT NULL,
    [OwnerID] INT            NULL,
    CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_People_Login] FOREIGN KEY ([OwnerID]) REFERENCES [Stracker].[Login] ([ID])
);



