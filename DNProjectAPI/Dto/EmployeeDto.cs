namespace DNProjectAPI.Dto
{
    public class EmployeeDto
    {
        public Guid id { get; set; }
        public string Name { get; set; } = null!;

        public string Department { get; set; } = null!;

        public string Position { get; set; } = null!;

        public decimal Salary { get; set; } = 0.0m;

        public string Email { get; set; } = null!;

        public DateTime? DOB { get; set; }
    }
}
