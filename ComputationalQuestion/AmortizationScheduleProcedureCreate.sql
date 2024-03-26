CREATE PROCEDURE CREATE_LOAN_AMORTIZATION_SCHEDULE
	@n INT,			-- total # of payments
	@ir FLOAT,		-- anual interest rate (note: enter as a decimal... So %5 would be entered as 0.05...)
	@pv FLOAT,		-- present value (original loan amount)
	@npy INT,		-- # of periods per year 
	@beg_dt DATE	-- the date of the first payment.
AS
	WITH 
		cte_n1 (n) AS ( SELECT 1 FROM (VALUES (1),(1),(1),(1),(1),(1),(1),(1),(1),(1)) n (n) ), -- 10 rows
		cte_n2 (n) AS ( SELECT 1 FROM cte_n1 a CROSS JOIN cte_n1 b ),							-- 100 rows
		cte_Tally (n) AS
		(
			SELECT TOP (@n) ROW_NUMBER() OVER (ORDER BY a.n)
			FROM cte_n2 a CROSS JOIN cte_n2 b													-- 10000 rows
			ORDER BY a.n
		)
	SELECT pmt_num = t.n,
		   pd.payment_date,
		   beg_ballance = CONVERT(decimal(19,2), pv.beg_ballance),
		   scheduled_pmt = CONVERT(decimal(19, 2), pmt.pmt_calc),
		   amt_to_intrest = CONVERT(decimal(19,2), ipmt.ipmt),
		   amt_to_principal = CONVERT(decimal(19,2), ppmt.ppmt),
		   end_ballance = CONVERT(decimal(19,2), pv.end_ballance)
	FROM cte_Tally AS t
		 CROSS APPLY ( VALUES (@ir / @npy) ) AS c (ir_per_year)
		 CROSS APPLY ( VALUES (POWER(1 + c.ir_per_year, @n), POWER(1 + c.ir_per_year, t.n), POWER(1 + c.ir_per_year, t.n-1) ) ) AS cp (power1, power2, power3)
		 CROSS APPLY ( VALUES (@pv / (cp.power1 - 1) * (c.ir_per_year * cp.power1)) ) AS pmt (pmt_calc)
		 CROSS APPLY ( VALUES (ABS(-@pv * cp.power3 + pmt.pmt_calc * (cp.power3 - 1) / c.ir_per_year), 
							   ABS(-@pv * cp.power2 + pmt.pmt_calc * (cp.power2 - 1) / c.ir_per_year) ) ) AS pv (beg_ballance, end_ballance)
		 CROSS APPLY ( VALUES (pv.beg_ballance * c.ir_per_year) ) AS ipmt (ipmt)
		 CROSS APPLY ( VALUES (pmt.pmt_calc - ipmt.ipmt) ) AS ppmt (ppmt)
		 CROSS APPLY ( VALUES (CASE 
									WHEN @npy <= 12 THEN DATEADD(MONTH, (12 / @npy) * (t.n - 1), @beg_dt)
									WHEN @npy = 26 THEN DATEADD(WEEK, 2 * (t.n - 1), @beg_dt)
									ELSE DATEADD(DAY, (365 / @npy) * (t.n - 1), @beg_dt)
							   END)
					 ) AS pd(payment_date);