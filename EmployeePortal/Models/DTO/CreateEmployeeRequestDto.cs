namespace EmployeePortal.Models.DTO
{
    public class CreateEmployeeRequestDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public double Salary { get; set; }
        public bool Status { get; set; }
    }
}
