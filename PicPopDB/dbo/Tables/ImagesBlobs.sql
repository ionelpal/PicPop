CREATE TABLE [dbo].[ImagesBlobs]
(
	[ImageId] INT NOT NULL , 
    [BlobId] INT NOT NULL, 
    PRIMARY KEY ([BlobId], [ImageId]), 
    CONSTRAINT [FK_ImagesBlobs_Images] FOREIGN KEY ([ImageId]) REFERENCES [PicPopImages]([Id]),
	CONSTRAINT [FK_ImagesBlobs_BlobFile] FOREIGN KEY ([BlobId]) REFERENCES [BlobFiles]([Id])
)
