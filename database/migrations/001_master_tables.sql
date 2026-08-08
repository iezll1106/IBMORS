-- =====================================================
-- IBMORS Migration 001
-- Master Tables
-- Integrated Budget Monitoring, Obligation and Reporting System
-- Version: 1.0
-- Created: 2026-08-08
-- =====================================================

-- =====================================================
-- Fiscal Years
-- =====================================================
CREATE TABLE fiscal_years (
    fiscal_year_id SERIAL PRIMARY KEY,
    year INTEGER NOT NULL UNIQUE,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- =====================================================
-- Offices
-- =====================================================
CREATE TABLE offices (
    office_id SERIAL PRIMARY KEY,
    office_code VARCHAR(20) NOT NULL UNIQUE,
    office_name VARCHAR(200) NOT NULL,
    office_type VARCHAR(50),
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- =====================================================
-- Budget Officers
-- =====================================================
CREATE TABLE budget_officers (
    budget_officer_id SERIAL PRIMARY KEY,
    employee_number VARCHAR(50),
    full_name VARCHAR(150) NOT NULL,
    email VARCHAR(150),
    username VARCHAR(50) NOT NULL UNIQUE,
    password_hash TEXT NOT NULL,
    role VARCHAR(50) NOT NULL DEFAULT 'BudgetOfficer',
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- =====================================================
-- Office Assignments
-- One budget officer may be assigned to multiple offices.
-- One office may have multiple active budget officers.
-- =====================================================
CREATE TABLE office_assignments (
    assignment_id SERIAL PRIMARY KEY,
    budget_officer_id INTEGER NOT NULL,
    office_id INTEGER NOT NULL,
    assigned_from DATE NOT NULL,
    assigned_to DATE,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_assignment_officer
        FOREIGN KEY (budget_officer_id)
        REFERENCES budget_officers (budget_officer_id),

    CONSTRAINT fk_assignment_office
        FOREIGN KEY (office_id)
        REFERENCES offices (office_id)
);

-- =====================================================
-- Sources of Funds
-- =====================================================
CREATE TABLE sources_of_funds (
    source_of_fund_id SERIAL PRIMARY KEY,
    fund_code VARCHAR(30) NOT NULL UNIQUE,
    fund_name VARCHAR(200) NOT NULL,
    description TEXT,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- =====================================================
-- Responsibility Centers
-- =====================================================
CREATE TABLE responsibility_centers (
    responsibility_center_id SERIAL PRIMARY KEY,
    center_code VARCHAR(30) NOT NULL UNIQUE,
    center_name VARCHAR(200) NOT NULL,
    office_id INTEGER NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_responsibility_office
        FOREIGN KEY (office_id)
        REFERENCES offices (office_id)
);

-- =====================================================
-- End of Migration 001
-- =====================================================