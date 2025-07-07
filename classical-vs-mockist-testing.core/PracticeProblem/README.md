## Evaluate Loan Application

When a customer applies for a loan, the system must:

- Fetch credit score from an external Credit Bureau API.

- Fetch monthly income from an external Employment Verification API.

- Check that credit score ≥ 700.

- Check that DTI (Debt-to-Income) ≤ 30%:
    proposed EMI / income <= 0.3

- Reject if any rule fails.

- Approve if all checks pass

## APIs

- Get income for applicantId ('applicant-123') https://mocki.io/v1/c3aa98f6-7aec-42eb-903a-95edabf28980
- Get credit score for applicantId ('applicant-123') https://mocki.io/v1/62d1604c-2fa9-4163-832d-3347d8e4d730
