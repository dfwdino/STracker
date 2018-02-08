CREATE TABLE [Events].[AskedQuestions] (
    [ID]       INT           IDENTITY (1, 1) NOT NULL,
    [Deleted]  BIT           CONSTRAINT [DF_PlayQuestions_Deleted] DEFAULT ((0)) NOT NULL,
    [Hide]     BIT           CONSTRAINT [DF_PlayQuestions_Hide] DEFAULT ((0)) NOT NULL,
    [Question] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_PlayQuestions] PRIMARY KEY CLUSTERED ([ID] ASC)
);

