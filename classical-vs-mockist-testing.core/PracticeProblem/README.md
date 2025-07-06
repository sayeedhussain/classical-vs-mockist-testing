## Evaluate Loan Application

When a customer applies for a loan, the system must:

- Fetch credit score from an external Credit Bureau API.

- Fetch monthly income from an external Employment Verification API.

- Check that credit score ≥ 700.

- Check that DTI (Debt-to-Income) ≤ 40%:
    proposed EMI / income <= 0.4

- Reject if any rule fails.

- Approve if all checks pass.

- Emit result to external system (via API call).

## Example Classes

| **Class**                  | **Responsibility**                                       |
| -------------------------- | -------------------------------------------------------- |
| `LoanApplicationService`   | Coordinates evaluation logic                             |
| `CreditScoreService`       | Calls external API to fetch credit score                 |
| `IncomeService`            | Calls external API to fetch applicant income             |
| `LoanApplication`          | Domain object to encapsulate evaluation logic            |
| `EligibilityPolicy`        | Applies DTI and score rules                              |
