GO

CREATE PROCEDURE create_loan_amortization_schedule_with_recycle
	@loan_amount FLOAT,
	@loan_term INT,
	@interest_rate FLOAT,
	@recycle_month INT,
	@recycled_loan_term INT,
	@recycled_interest_rate FLOAT
AS	
	-- calculate initial monthly interest and monthly pay
	DECLARE @monthly_interest FLOAT = @interest_rate / 12
	DECLARE @monthly_pay FLOAT = @loan_amount * @monthly_interest / ( 1 - POWER(1 + @monthly_interest, - @loan_term) )
	-- calculate recycled monthly interest and monthly pay
	DECLARE @recycled_monthly_interest FLOAT = @recycled_interest_rate / 12

			-- create initial amortization schedule
	;WITH   cte_amortization_schedule
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
			),
			-- preparing data for recycled loan schedule
		    loan_recycle_data
			(
				remaining_amount,
				recycled_monthly_pay
			)
			AS (
				SELECT 
					MIN(ending_balance),
					MIN(ending_balance) * @recycled_monthly_interest / ( 1 - POWER(1 + @recycled_monthly_interest, - @recycled_loan_term) )
				FROM cte_amortization_schedule
			),
			-- create recycled amortization schedule
		    cte_amortization_schedule_recycled
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
					remaining_amount,
					recycled_monthly_pay,
					remaining_amount * @recycled_monthly_interest,
					recycled_monthly_pay - remaining_amount * @recycled_monthly_interest,
					remaining_amount - ( recycled_monthly_pay - remaining_amount * @recycled_monthly_interest )
				FROM
					loan_recycle_data
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
	SELECT
		payment_number AS [Payment Number],
		CONVERT(DECIMAL(19,2), beginning_balance) AS [Beginning Balance],
		CONVERT(DECIMAL(19,2), monthly_pay) AS [Monthly Pay],
		CONVERT(DECIMAL(19,2), interest) AS [Interest],
		CONVERT(DECIMAL(19,2), principal) AS [Principal],
		CONVERT(DECIMAL(19,2), ending_balance) AS [Ending Balance]
	FROM cte_amortization_schedule
	UNION ALL
	SELECT
		payment_number AS [Payment Number],
		CONVERT(DECIMAL(19,2), beginning_balance) AS [Beginning Balance],
		CONVERT(DECIMAL(19,2), monthly_pay) AS [Monthly Pay],
		CONVERT(DECIMAL(19,2), interest) AS [Interest],
		CONVERT(DECIMAL(19,2), principal) AS [Principal],
		CONVERT(DECIMAL(19,2), ending_balance) AS [Ending Balance]
	FROM cte_amortization_schedule_recycled

GO


-- the task input data
DECLARE
	@loan_amount FLOAT = 36000,
	@loan_term INT = 36,
	@interest_rate FLOAT = .08,
	@recycle_month INT = 12,
	@recycled_loan_term INT = 48,
	@recycled_interest_rate FLOAT = .045

EXECUTE create_loan_amortization_schedule_with_recycle @loan_amount, @loan_term, @interest_rate, @recycle_month, @recycled_loan_term, @recycled_interest_rate
