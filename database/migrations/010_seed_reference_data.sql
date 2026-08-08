-- =====================================================
-- IBMORS Migration 010
-- Seed Reference Data
-- =====================================================

INSERT INTO fiscal_years (year, is_active)
VALUES
(2026, TRUE);

--- Sources of Funds

INSERT INTO sources_of_funds (fund_code, fund_name)
VALUES
('GF', 'General Fund'),
('SEF', 'Special Education Fund'),
('TF', 'Trust Fund'),
('20% DF', '20% Development Fund'),
('GAD', 'Gender and Development Fund'),
('LEDI', 'Legislative-Executive Development Initiative'),
('LDRRMF', 'Local Disaster Risk Reduction and Management Fund');