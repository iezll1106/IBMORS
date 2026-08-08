-- =====================================================
-- IBMORS Migration 003
-- Annual Appropriations
-- =====================================================

CREATE TABLE appropriations (
    appropriation_id SERIAL PRIMARY KEY,

    fiscal_year_id INTEGER NOT NULL,
    office_id INTEGER NOT NULL,
    budget_account_id INTEGER NOT NULL,
    source_of_fund_id INTEGER NOT NULL,
    responsibility_center_id INTEGER,

    appropriation_amount NUMERIC(18,2) NOT NULL DEFAULT 0,

    q1_allocation NUMERIC(18,2) NOT NULL DEFAULT 0,
    q2_allocation NUMERIC(18,2) NOT NULL DEFAULT 0,
    q3_allocation NUMERIC(18,2) NOT NULL DEFAULT 0,
    q4_allocation NUMERIC(18,2) NOT NULL DEFAULT 0,

    revised_appropriation NUMERIC(18,2) NOT NULL DEFAULT 0,

    remarks TEXT,

    is_active BOOLEAN NOT NULL DEFAULT TRUE,

    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_app_fiscal_year
        FOREIGN KEY (fiscal_year_id)
        REFERENCES fiscal_years (fiscal_year_id),

    CONSTRAINT fk_app_office
        FOREIGN KEY (office_id)
        REFERENCES offices (office_id),

    CONSTRAINT fk_app_budget_account
        FOREIGN KEY (budget_account_id)
        REFERENCES budget_accounts (budget_account_id),

    CONSTRAINT fk_app_source_of_fund
        FOREIGN KEY (source_of_fund_id)
        REFERENCES sources_of_funds (source_of_fund_id),

    CONSTRAINT fk_app_responsibility_center
        FOREIGN KEY (responsibility_center_id)
        REFERENCES responsibility_centers (responsibility_center_id),

    CONSTRAINT uq_appropriation_unique
        UNIQUE (
            fiscal_year_id,
            office_id,
            budget_account_id,
            source_of_fund_id,
            responsibility_center_id
        )
);

--- After creating the table, we can add;

SELECT table_name
FROM information_schema.tables
WHERE table_schema = 'public'
ORDER BY table_name;

--- This field gives us a place to store the current authorized appropriation 
--- after adjustments, while still preserving the original appropriation amount.