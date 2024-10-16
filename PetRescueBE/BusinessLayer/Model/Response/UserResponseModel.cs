namespace BusinessLayer.Models.Response
{
    public class UserResponseModel
    {
        public Guid UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string PasswordHash { get; set; } = null!;
        public Guid RoleId { get; set; }
        public string Status { get; set; } = null!;
    }
}
