namespace BusinessLayer.Model.Request
{
    public class PetAddRequestModel
    {
        public Guid ShelterId { get; set; }
        public string? Name { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? Description { get; set; }
        public string Species { get; set; } = null!;
        public string? PhotoUrl { get; set; }
    }
}
