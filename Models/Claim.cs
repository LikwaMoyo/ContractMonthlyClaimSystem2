using System;
using System.ComponentModel.DataAnnotations;

namespace ContractMonthlyClaimSystem2.Models
{
    public class Claim
    {
        public int Id { get; set; }
        public string LecturerId { get; set; } = string.Empty;
        public DateTime DateSubmitted { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public string? AdditionalNotes { get; set; }
        public ClaimStatus Status { get; set; }
        public string? SupportingDocumentPath { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string? UpdatedBy { get; set; }
    }

    public enum ClaimStatus
    {
        Submitted,
        UnderReview,
        ApprovedByCoordinator,
        ApprovedByManager,
        Rejected,
        Settled,
        Pending
    }
}