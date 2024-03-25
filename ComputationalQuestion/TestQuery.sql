DECLARE 
	@n INT = 36,				-- total # of payments
	@ir FLOAT = .08,			-- anual interest rate (note: enter as a decimal... So %5 would be entered as 0.05...)
	@pv FLOAT = 36000.00,		-- present value (original loan amount)
	@npy INT = 12,				-- # of periods per year 
	@beg_dt DATE = GETDATE(),	-- the date of the first payment
	@recycle_pn INT = 13,		-- recycle payment number
	@recycle_ir FLOAT = .045,	-- recycle anual interest rate
	@recycle_n INT = 48;		-- recycle # of payments

DECLARE @result TABLE
(
	pmt_num BIGINT,
	payment_date DATE,
	beg_ballance decimal(19,2),
	scheduled_pmt decimal(19, 2),
	amt_to_intrest decimal(19,2),
	amt_to_principal decimal(19,2),
	end_ballance decimal(19,2)
)

INSERT @result EXECUTE CREATE_LOAN_AMORTIZATION_SCHEDULE @n, @ir, @pv, @npy, @beg_dt

SELECT * FROM @result

DECLARE
	@ra FLOAT,															--  remaining amount
	@recycled_beg_dt DATE = DATEADD(month, @recycle_pn - 1, @beg_dt);	--  the date of the first payment of recycled loan

SET @ra =
(
	SELECT TOP 1 beg_ballance 
	FROM @result 
	WHERE pmt_num = @recycle_pn
)

EXECUTE CREATE_LOAN_AMORTIZATION_SCHEDULE @recycle_n, @recycle_ir, @ra, @npy, @recycled_beg_dt
