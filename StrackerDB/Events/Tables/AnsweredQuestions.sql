CREATE TABLE [Events].[AnsweredQuestions] (
    [ID]         INT            IDENTITY (1, 1) NOT NULL,
    [Deleted]    BIT            CONSTRAINT [DF_AnsweredQuestions_Deleted] DEFAULT ((0)) NOT NULL,
    [Hide]       BIT            CONSTRAINT [DF_AnsweredQuestions_Hide] DEFAULT ((0)) NOT NULL,
    [Answered]   NVARCHAR (MAX) NOT NULL,
    [QuestionID] INT            NOT NULL,
    [PersonID]   INT            NOT NULL,
    [EntryDate]  DATETIME       CONSTRAINT [DF_AnsweredQuestions_EntryDate] DEFAULT (getdate()) NOT NULL,
    [EventDate]  DATE           NOT NULL,
    CONSTRAINT [PK_AnsweredQuestions] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AnsweredQuestions_AskedQuestions] FOREIGN KEY ([QuestionID]) REFERENCES [Events].[AskedQuestions] ([ID]),
    CONSTRAINT [FK_AnsweredQuestions_People] FOREIGN KEY ([PersonID]) REFERENCES [People].[People] ([ID])
);

