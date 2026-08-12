-- ============================================================
-- IBMORS Migration 005
-- Office Management Enhancements
-- eBudget Office Structure Support
-- Version: 1.1
-- Created: 2026-08-10
-- ============================================================
-- Add parent office relationship
ALTER TABLE offices
ADD COLUMN IF NOT EXISTS parent_office_id INTEGER;
-- Add office status
ALTER TABLE offices
ADD COLUMN IF NOT EXISTS status VARCHAR(20) NOT NULL DEFAULT 'Active';
-- Add last updated timestamp
ALTER TABLE offices
ADD COLUMN IF NOT EXISTS updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP;
-- Create self-referencing relationship
ALTER TABLE offices
ADD CONSTRAINT fk_offices_parent FOREIGN KEY (parent_office_id) REFERENCES offices(office_id);
-- Optional: create index for faster hierarchy queries
CREATE INDEX IF NOT EXISTS idx_offices_parent ON offices(parent_office_id);
-- ============================================================
-- End of Migration 005
-- ============================================================