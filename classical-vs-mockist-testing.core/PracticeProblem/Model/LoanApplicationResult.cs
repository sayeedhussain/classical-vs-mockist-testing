public record LoanApplicationResult(string ApplicantId, LoanStatus Status);

public enum LoanStatus { Approved, Rejected }
