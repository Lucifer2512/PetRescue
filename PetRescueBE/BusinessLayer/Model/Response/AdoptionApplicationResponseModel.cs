namespace BusinessLayer.Model.Response
{
    public class AdoptionApplicationResponseModel
    {
        public Guid ApplicationId { get; set; }
        public string? UserName { get; set; }
        public string? PetName { get; set; }
        public DateTime RequestDate { get; set; }
        public string? Status { get; set; }
        public string? Notes { get; set; }
    }

    public class AdoptionApplicationOnlyIdResponseModel
    {
        public Guid ApplicationId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? PetId { get; set; }
    }
}
