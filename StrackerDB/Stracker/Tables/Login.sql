CREATE TABLE [Stracker].[Login] (
    [ID]       INT            IDENTITY (1, 1) NOT NULL,
    [Deleted]  BIT            CONSTRAINT [DF_Login_Deleted] DEFAULT ((0)) NOT NULL,
    [UserName] NVARCHAR (50)  NOT NULL,
    [FullName] NVARCHAR (50)  NOT NULL,
    [Hash]     NVARCHAR (MAX) NULL,
    [Salt]     NVARCHAR (MAX) NULL,
    [Password] NVARCHAR (50)  NULL,
    CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED ([ID] ASC)
);



