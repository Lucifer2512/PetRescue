using System.ComponentModel.DataAnnotations;

namespace PetRescueFE.Pages.Model
{
    public class PetAddRequestModelFE
    {
        [Required]
        public Guid ShelterId { get; set; }

        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string? Name { get; set; }

        [Range(0, 100, ErrorMessage = "Age must be between 0 and 100.")]
        public int? Age { get; set; }

        [RegularExpression("Male|Female|Other", ErrorMessage = "Gender must be 'Male', 'Female', or 'Other'.")]
        public string? Gender { get; set; }

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Species is required.")]
        public string Species { get; set; } = null!;
        public byte[]? Image { get; set; }
    }
}
