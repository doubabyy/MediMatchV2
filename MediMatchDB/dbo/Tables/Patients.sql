CREATE TABLE [dbo].[Patients] (
    [ApplicationUserId]  NVARCHAR (450) NOT NULL,
    [Gender]             NVARCHAR (50)  NULL,
    [DepAnx]             BIT            NOT NULL,
    [DepAnxDesc]         NVARCHAR (MAX) NULL,
    [SuicThoughts]       BIT            NOT NULL,
    [SuicThoughtsDesc]   NVARCHAR (MAX) NULL,
    [SubstanceAbuse]     BIT            NOT NULL,
    [SubstanceAbuseDesc] NVARCHAR (MAX) NULL,
    [SupportSystem]      NVARCHAR (MAX) NULL,
    [Therapy]            BIT            NOT NULL,
    [TherapyDesc]        NVARCHAR (MAX) NULL,
    [ProblemsDesc]       NVARCHAR (MAX) NULL,
    [TreatmentGoals]     NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Patients] PRIMARY KEY CLUSTERED ([ApplicationUserId] ASC),
    CONSTRAINT [FK_Patients_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);

