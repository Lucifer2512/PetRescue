namespace PetRescueFE.Pages.Model
{
    public class PetUpdateRequestModelFE
    {
        public Guid PetId { get; set; }
        public Guid ShelterId { get; set; }
        public string? Name { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? Description { get; set; }
        public string Species { get; set; } = null!;
        public string? Status { get; set; }
        public byte[]? Image { get; set; }
    }
}
