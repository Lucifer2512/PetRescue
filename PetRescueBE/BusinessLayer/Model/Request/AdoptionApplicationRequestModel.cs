namespace BusinessLayer.Model.Request
{
    public class AdoptionApplicationRequestModel
    {
        public Guid PetId { get; set; }
        public Guid UserId { get; set; }
        public string? Notes { get; set; }
    }

    public class AdoptionApplicationRequestModelForUpdate
    {
        public string? Status { get; set; }
        public string? Notes { get; set; }
    }

}
