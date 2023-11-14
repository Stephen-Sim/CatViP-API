namespace CatViP_API.DTOs.ExpertDTOs
{
    public class ExpertApplicationDTO
    {
        public long Id { get; set; }

        public string Description { get; set; } = null!;

        public byte[] Documentation { get; set; } = null!;

        public string? RejectedReason { get; set; }

        public DateTime DateTime { get; set; }

        public string Status { get; set; } = null!;
    }
}
