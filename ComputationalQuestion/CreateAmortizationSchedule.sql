GO

CREATE FUNCTION create_amortization_schedule_table
(
	@loan_amount FLOAT,
	@loan_term INT,		-- in months
	@interest_rate FLOAT
)
RETURNS @result TABLE
	(
		payment_number INT,
		beginning_balance DECIMAL(19,2),
		monthly_pay DECIMAL(19,2),
		interest DECIMAL(19,2),
		principal DECIMAL(19,2),
		ending_balance DECIMAL(19,2)
	)
AS
BEGIN
	DECLARE @monthly_interest FLOAT = @interest_rate / 12
	DECLARE @monthly_pay FLOAT = @loan_amount * @monthly_interest / ( 1 - POWER( 1 + @monthly_interest , - @loan_term ) )

	DECLARE @beginning_balance FLOAT = @loan_amount
	DECLARE @interest FLOAT = @beginning_balance * @monthly_interest
	DECLARE @principal FLOAT = @monthly_pay - @interest
	DECLARE @ending_balace FLOAT = @beginning_balance - @principal
	DECLARE @counter INT = 1

	WHILE ( @counter <= @loan_term )
	BEGIN
		INSERT @result
		SELECT
			@counter AS [payment_number] ,
			CONVERT( DECIMAL(19,2) , @beginning_balance ) AS [beginning_balance] ,
			CONVERT( DECIMAL(19,2) , @monthly_pay ) AS [monthly_pay] ,
			CONVERT( DECIMAL(19,2) , @interest ) AS [interest] ,
			CONVERT( DECIMAL(19,2) , @principal ) AS [principal] ,
			CONVERT( DECIMAL(19,2) , @ending_balace ) AS [ending_balance]
	
		SET @beginning_balance = @ending_balace
		SET @interest = @beginning_balance * @monthly_interest
		SET @principal = @monthly_pay - @interest
		SET @ending_balace = @beginning_balance - @principal
		SET @Counter  = @Counter  + 1
	END

	RETURN
END

GO


DECLARE 
	@loan_amount FLOAT = 36000,
	@loan_term INT = 36,
	@interest_rate FLOAT = .08,
	@recycle_month INT = 12,
	@recycled_loan_term INT = 48,
	@recycled_interest_rate FLOAT = .045;

DECLARE @schedule_table TABLE
(
	payment_number INT,
	beginning_balance DECIMAL(19,2),
	monthly_pay DECIMAL(19,2),
	interest DECIMAL(19,2),
	principal DECIMAL(19,2),
	ending_balance DECIMAL(19,2)
)

-- Creating initial loan amortization schedule and inserting 12 rows from it to the result table
INSERT @schedule_table
SELECT *
FROM create_amortization_schedule_table( @loan_amount , @loan_term , @interest_rate )
WHERE payment_number < @recycle_month + 1

-- Detecting remaining amount
DECLARE @remaining_amount DECIMAL(19,2)
SELECT @remaining_amount = MIN(ending_balance) 
FROM @schedule_table

-- Creating recycled loan amortization schedule on the remaining amount and inserting it to the result table
INSERT @schedule_table
SELECT
	payment_number + @recycle_month,
	beginning_balance,
	monthly_pay,
	interest,
	principal,
	ending_balance
FROM create_amortization_schedule_table( @remaining_amount , @recycled_loan_term , @recycled_interest_rate )


SELECT
	payment_number AS [Payment Number],
	beginning_balance AS [Beginning Balance],
	monthly_pay AS [Monthly Pay],
	interest AS [Interest],
	principal AS [Principal],
	ending_balance AS [Ending Balance]
FROM @schedule_table
