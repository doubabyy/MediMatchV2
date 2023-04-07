CREATE TABLE [dbo].[Matches] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [PatientId]   NVARCHAR (MAX) NOT NULL,
    [DoctorId]    NVARCHAR (MAX) NOT NULL,
    [RequestedAt] DATETIME2 (7)  NOT NULL,
    [AcceptedAt]  DATETIME2 (7)  NULL,
    [RejectedAt]  DATETIME2 (7)  NULL,
    [Accepted]    BIT            NOT NULL,
    CONSTRAINT [PK_Matches] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
Create TRIGGER [dbo].[Match_Accepted] 
   ON  [dbo].[Matches] 
   AFTER update
AS 
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

--   declare @EndDate Date
    --set @EndDate = dateadd(m,12, getdate())

    declare @monthCounter Date
    set @monthCounter = dateadd(m,1, getdate())

    declare @c int
    set @c=0

    declare @patientId nvarchar(450)
    select @patientId = PatientId from inserted
	
	declare @doctorId nvarchar(450)
    select @doctorId = DoctorId from inserted

	declare @amount int
	select @amount = d.Rates 
		from inserted, Doctors d
		where inserted.DoctorId = d.ApplicationUserId

    declare @curYear int
    declare @curMonth int

    while(@c<1) 
    BEGIN
        set @curYear = year(@monthCounter)
        set @curMonth = month(@monthCounter)
        exec CreateBillingCycle @curYear, 
                              	@curMonth, 
                               	@patientId,
								@doctorId,
								@amount

        SET @c = @c + 1
        SET @monthCounter = dateadd(m,1, @monthCounter)
    END

END
