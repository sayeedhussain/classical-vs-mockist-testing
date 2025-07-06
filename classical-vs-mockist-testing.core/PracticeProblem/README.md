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

## Target Classes

| **Class**                  | **Responsibility**                                       |
| -------------------------- | -------------------------------------------------------- |
| `LoanApplicationService`   | Coordinates evaluation logic                             |
| `CreditScoreClient`        | Calls external API to fetch credit score                 |
| `IncomeVerificationClient` | Calls external API to fetch applicant income             |
| `LoanApplication`          | Domain object to encapsulate evaluation logic            |
| `EligibilityPolicy`        | Applies DTI and score rules                              |
| `ResultNotifier`           | Sends result to external system (via REST, fake in test) |
