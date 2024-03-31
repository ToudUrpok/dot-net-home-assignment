
-- the task input data
DECLARE 
	@loan_amount FLOAT = 36000,
	@loan_term INT = 36,
	@interest_rate FLOAT = .08,
	@recycle_month INT = 12,
	@recycled_loan_term INT = 48,
	@recycled_interest_rate FLOAT = .045

-- variable with the result table where initial and recycled schedules will be inserted
DECLARE @result_table TABLE
(
	payment_number INT,
	beginning_balance DECIMAL(19,2),
	monthly_pay DECIMAL(19,2),
	interest DECIMAL(19,2),
	principal DECIMAL(19,2),
	ending_balance DECIMAL(19,2)
)

-- calculate initial monthly interest and monthly pay
DECLARE @monthly_interest FLOAT = @interest_rate / 12
DECLARE @monthly_pay FLOAT = @loan_amount * @monthly_interest / ( 1 - POWER(1 + @monthly_interest, - @loan_term) )
-- calculate initial amortization schedule
;WITH    cte_amortization_schedule
		(
			payment_number,
			monthly_interest,
			beginning_balance,
			monthly_pay,
			interest,
			principal,
			ending_balance
		) 
		AS
		(
			SELECT 
				1,
				@monthly_interest,
				@loan_amount,
				@monthly_pay,
				@loan_amount * @monthly_interest,
				@monthly_pay - @loan_amount * @monthly_interest,
				@loan_amount - ( @monthly_pay - @loan_amount * @monthly_interest ) 
			UNION ALL
			SELECT    
				payment_number + 1,
				monthly_interest,
				ending_balance,
				monthly_pay,
				ending_balance * monthly_interest,
				monthly_pay - ending_balance * monthly_interest,
				ending_balance - ( monthly_pay - ending_balance * monthly_interest )
			FROM    
				cte_amortization_schedule
			WHERE payment_number < @recycle_month
		)
-- insert initial schedule into result variable
INSERT @result_table		
SELECT 
    payment_number,
	CONVERT(DECIMAL(19,2), beginning_balance),
	CONVERT(DECIMAL(19,2), monthly_pay),
	CONVERT(DECIMAL(19,2), interest),
	CONVERT(DECIMAL(19,2), principal),
	CONVERT(DECIMAL(19,2), ending_balance)
FROM 
    cte_amortization_schedule

-- detecting remaining amount
DECLARE @remaining_amount FLOAT
SELECT @remaining_amount = MIN(ending_balance)
FROM @result_table

-- calculate recycled monthly interest and monthly pay
DECLARE @recycled_monthly_interest FLOAT = @recycled_interest_rate / 12
DECLARE @recycled_monthly_pay FLOAT = @remaining_amount * @recycled_monthly_interest / ( 1 - POWER(1 + @recycled_monthly_interest, - @recycled_loan_term) )
-- calculate recycled amortization schedule
;WITH   cte_amortization_schedule_recycled
		(
			payment_number,
			monthly_interest,
			beginning_balance,
			monthly_pay,
			interest,
			principal,
			ending_balance
		) 
		AS (
			SELECT 
				@recycle_month + 1,
				@recycled_monthly_interest,
				@remaining_amount,
				@recycled_monthly_pay,
				@remaining_amount * @recycled_monthly_interest,
				@recycled_monthly_pay - @remaining_amount * @recycled_monthly_interest,
				@remaining_amount - ( @recycled_monthly_pay - @remaining_amount * @recycled_monthly_interest )
			UNION ALL
			SELECT    
				payment_number + 1,
				monthly_interest,
				ending_balance,
				monthly_pay,
				ending_balance * monthly_interest,
				monthly_pay - ending_balance * monthly_interest,
				ending_balance - ( monthly_pay - ending_balance * monthly_interest )
			FROM    
				cte_amortization_schedule_recycled
			WHERE payment_number < @recycled_loan_term + @recycle_month
		)
-- insert recycled schedule into result variable
INSERT @result_table		
SELECT 
    payment_number,
	CONVERT(DECIMAL(19,2), beginning_balance),
	CONVERT(DECIMAL(19,2), monthly_pay),
	CONVERT(DECIMAL(19,2), interest),
	CONVERT(DECIMAL(19,2), principal),
	CONVERT(DECIMAL(19,2), ending_balance)
FROM 
    cte_amortization_schedule_recycled

-- select the result
SELECT
	payment_number AS [Payment Number],
	beginning_balance AS [Beginning Balance],
	monthly_pay AS [Monthly Pay],
	interest AS [Interest],
	principal AS [Principal],
	ending_balance AS [Ending Balance]
FROM @result_table
