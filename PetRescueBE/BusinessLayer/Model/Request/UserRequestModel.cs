using System.ComponentModel;

namespace BusinessLayer.Models.Request
{
    public class UserRequestModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string Password { get; set; } = null!;
        // role default as user
        [DefaultValue("E7B8F3D2-4A2F-4C3B-8F4D-9C5D8A3E1B2C")]
        public Guid RoleId { get; set; }
        [DefaultValue("Active")]
        public string Status { get; set; } = null!;
    }

    public class UserRequestModelForUpdate
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string PasswordHash { get; set; } = null!;
        // role default as user
        [DefaultValue("E7B8F3D2-4A2F-4C3B-8F4D-9C5D8A3E1B2C")]
        public Guid RoleId { get; set; }
        [DefaultValue("Active")]
        public string Status { get; set; } = null!;
    }
}
