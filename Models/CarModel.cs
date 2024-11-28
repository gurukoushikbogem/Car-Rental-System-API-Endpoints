using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.Models
{
    public class CarModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Car make is required.")]
        [StringLength(50, ErrorMessage = "Make cannot exceed 50 characters.")]
        public string Makes { get; set; }

        [Required(ErrorMessage = "Car model is required.")]
        [StringLength(50, ErrorMessage = "Model cannot exceed 50 characters.")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Manufacturing year is required.")]
        [Range(1900, 2100, ErrorMessage = "Year must be between 1900 and 2100.")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Price per day is required.")]
        [Range(0, 10000, ErrorMessage = "Price per day must be between 0 and 10,000.")]
        public decimal PricePerDay { get; set; }

        public bool IsAvailable { get; set; } = true;
    }
}
