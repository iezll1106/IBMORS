-- =====================================================
-- Budget Accounts (Object of Expenditure)
-- =====================================================

CREATE TABLE budget_accounts (
    budget_account_id SERIAL PRIMARY KEY,

    account_code VARCHAR(30) NOT NULL UNIQUE,
    account_name VARCHAR(255) NOT NULL,

    expenditure_class VARCHAR(50) NOT NULL,
    account_category VARCHAR(100),
    object_of_expenditure VARCHAR(255),

    account_level INTEGER DEFAULT 1,

    parent_account_id INTEGER,

    is_active BOOLEAN NOT NULL DEFAULT TRUE,

    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_budget_account_parent
        FOREIGN KEY (parent_account_id)
        REFERENCES budget_accounts (budget_account_id)
);