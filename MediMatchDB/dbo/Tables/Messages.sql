CREATE TABLE [dbo].[Messages] (
    [MessageId]          INT            IDENTITY (1, 1) NOT NULL,
    [MessageTxt]         NVARCHAR (MAX) NOT NULL,
    [MessageDate]        DATETIME2 (7)  NOT NULL,
    [MessageFromID]      NVARCHAR (MAX) NOT NULL,
    [MessageToID]        NVARCHAR (MAX) NOT NULL,
    [ApplicationUserId]  NVARCHAR (450) NULL,
    [ApplicationUserId1] NVARCHAR (450) NULL,
    CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED ([MessageId] ASC),
    CONSTRAINT [FK_Messages_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Messages_AspNetUsers_ApplicationUserId1] FOREIGN KEY ([ApplicationUserId1]) REFERENCES [dbo].[AspNetUsers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Messages_ApplicationUserId]
    ON [dbo].[Messages]([ApplicationUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Messages_ApplicationUserId1]
    ON [dbo].[Messages]([ApplicationUserId1] ASC);

