CREATE TABLE [dbo].[BlobFiles]
(
	[Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Filename]    NVARCHAR (500) NULL,
    [Description] NVARCHAR (256) NULL,
    [Comments]    NVARCHAR (MAX) NULL,
    [BlobKey]     NVARCHAR (500) NOT NULL,
    [Container]   INT            NOT NULL,
    [Type]        NVARCHAR (64)  NULL,
    [dtCreated]   DATETIME       NOT NULL,
    [CreatedBy]   NVARCHAR(128)            NOT NULL,
    CONSTRAINT [PK_Blob_Files] PRIMARY KEY CLUSTERED ([Id] ASC)
)
