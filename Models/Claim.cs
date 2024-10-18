namespace ContractMonthlyClaimSystem2.Models
{
    public class Claim
    {
        public int Id { get; set; }
        public string LecturerId { get; set; }
        public DateTime DateSubmitted { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public string AdditionalNotes { get; set; }
        public ClaimStatus Status { get; set; }
        public string SupportingDocumentPath { get; set; }
    }

    public enum ClaimStatus
    {
        Pending,
        Approved,
        Rejected
    }

}
