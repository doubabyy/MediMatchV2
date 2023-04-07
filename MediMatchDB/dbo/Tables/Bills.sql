CREATE TABLE [dbo].[Bills] (
    [Bill_Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Bill_details]  NVARCHAR (MAX) NOT NULL,
    [Date_received] DATETIME2 (7)  NULL,
    [CardNum]       NVARCHAR (MAX) NOT NULL,
    [Amount]        INT            NOT NULL,
    [DueDate]       DATETIME2 (7)  NOT NULL,
    [PaymentType]   NVARCHAR (MAX) NOT NULL,
    [PatientId]     NVARCHAR (MAX) NOT NULL,
    [DoctorId]      NVARCHAR (MAX) NOT NULL,
    [Paid]          BIT            NOT NULL,
    CONSTRAINT [PK_Bills] PRIMARY KEY CLUSTERED ([Bill_Id] ASC)
);

