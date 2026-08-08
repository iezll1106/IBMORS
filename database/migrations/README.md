# IBMORS Database Migrations

## Integrated Budget Monitoring, Obligation and Reporting System (IBMORS)

This folder contains the versioned SQL migration scripts used to create and update the IBMORS PostgreSQL database schema.

## Migration Order

Execute the migration scripts in numerical order.

| Migration | Description | Status |
|----------|-------------|--------|
| 001_master_tables.sql | Master reference tables | Planned |
| 002_budget_accounts.sql | Budget account master table | Planned |
| 003_appropriations.sql | Annual appropriations | Planned |
| 004_allotment_release_orders.sql | ARO header and details | Planned |
| 005_obligations.sql | OBR header and line items | Planned |
| 006_budget_adjustments.sql | Supplemental budgets, realignments, augmentations, reversions | Planned |
| 007_document_registry.sql | Supporting document registry | Planned |
| 008_audit_logs.sql | Audit trail and activity logs | Planned |

## Execution Procedure

1. Create the PostgreSQL database `ibmors`.
2. Execute `001_master_tables.sql`.
3. Execute each subsequent migration in sequence.
4. Verify tables and constraints after each migration.
5. Commit the migration file to GitHub after successful testing.

## Naming Convention

Migration files use the format:

```
NNN_description.sql
```

Examples:

```
001_master_tables.sql
002_budget_accounts.sql
003_appropriations.sql
```

## Rollback Strategy

If a migration fails:

1. Stop executing subsequent migrations.
2. Review the error.
3. Restore the database from the latest backup if necessary.
4. Correct the migration script.
5. Re-run the migration.

## IBMORS Development Standard

- Never modify a migration that has already been executed in production.
- Create a new migration file for all schema changes.
- All migrations must be committed to GitHub.
- Database changes must be documented in `docs/Change_Log.md`.

## Current Database Version

**Version:** 1.0

**Last Updated:** 2026-08-08

**Maintainer:** IBMORS Development Project