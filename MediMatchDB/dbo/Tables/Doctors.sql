CREATE TABLE [dbo].[Doctors] (
    [ApplicationUserId] NVARCHAR (450) NOT NULL,
    [Description]       NVARCHAR (400) NOT NULL,
    [Availability]      NVARCHAR (400) NOT NULL,
    [Specialty]         NVARCHAR (MAX) NOT NULL,
    [Rates]             INT            NOT NULL,
    [AcceptsInsurance]  BIT            NOT NULL,
    CONSTRAINT [PK_Doctors] PRIMARY KEY CLUSTERED ([ApplicationUserId] ASC),
    CONSTRAINT [FK_Doctors_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);

