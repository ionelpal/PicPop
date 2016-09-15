CREATE TABLE [dbo].[ErrorsLog]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserName] NVARCHAR(MAX) NULL, 
    [UserAgent] NVARCHAR(MAX) NULL, 
    [Date] DATE NULL, 
    [Message] NVARCHAR(MAX) NULL, 
    [Method] NVARCHAR(MAX) NULL, 
    [Source] NVARCHAR(MAX) NULL, 
    [StackTrace] NVARCHAR(MAX) NULL, 
    [Url] NVARCHAR(MAX) NULL
)
