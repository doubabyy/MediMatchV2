-- =============================================
-- Author:        Eric
-- Create date: <Create Date,,>
-- Description:    Creates billing for next month
-- =============================================
Create PROCEDURE CreateBillingCycle
    -- Add the parameters for the stored procedure here
    @Year int = null,
    @Month int = null,
	@PatientId nvarchar(450) = null,
	@DoctorId nvarchar(450) = null,
	@Amount int = 0
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    if (@Year is null)
    BEGIN
        SET @Year = Year(DateAdd(m,1,getdate()))
    END
    if (@Month is null)
    BEGIN
        SET @Month = Month(DateAdd(m,1,getdate()))
    END

    DECLARE @DateOfBilling varchar(10)

    SET @DateOfBilling = Cast(@Month as varchar(2)) + '/' + 
                        Cast(Day(getdate()) as varchar(2)) + '/' + 
                        Cast(@Year as varchar(4))
    --print @DateOfBilling

    insert into Bills(Bill_details, Date_received, CardNum, Amount, DueDate, PaymentType, PatientId, DoctorId, Paid)
    select 'Monthly Subscription', NULL, '123', @Amount, Convert(date, @DateOfBilling), 'card', @PatientId, @DoctorId, 0
END
