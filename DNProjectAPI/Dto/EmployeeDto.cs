using System.ComponentModel.DataAnnotations;

namespace DNProjectAPI.Dto
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Department { get; set; } = null!;

        [Required]
        public string Position { get; set; } = null!;

        [Required]
        public decimal Salary { get; set; } = 0.0m;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public DateOnly? DOB { get; set; }
    }
}
