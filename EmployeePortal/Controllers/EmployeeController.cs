using EmployeePortal.Models.Domain;
using EmployeePortal.Models.DTO;
using EmployeePortal.Repositories.Implementation;
using EmployeePortal.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace EmployeePortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        //POST: {apibaseurl}/api/employees
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeRequestDto request)
        {
            //Convert DTO to Domain
            var employee = new Employee
            {
                Name = request.Name,
                Email = request.Email,
                Age = request.Age,
                Phone = request.Phone,
                Salary = request.Salary,
                Status = request.Status
            };

            employee = await employeeRepository.SaveAsync(employee);

            //Convert Domain Model back to DTO
            var response = new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Age = employee.Age,
                Phone = employee.Phone,
                Salary = employee.Salary,
                Status = employee.Status
            };
            return Ok(response);
        }

        //GET: {apibaseurl}/api/employees
        [HttpGet]
        public async Task<IActionResult> GetAllEmployee()
        {
            var employees = await employeeRepository.GetAllAsync();

            //Convert Domain model to DTO
            var response = new List<EmployeeDto>();
            foreach (var employee in employees)
            {
                response.Add(new EmployeeDto
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Age = employee.Age,
                    Phone = employee.Phone,
                    Salary = employee.Salary,
                    Status = employee.Status
                });
            }
            return Ok(response);
        }

        //PUT: {apibaseurl}/api/employees/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployeeById([FromRoute] Guid id, UpdateEmployeeRequestDto request)
        {
            //Convert DTO to Domain Model
            var employee = new Employee
            {
                Id = id,
                Name = request.Name,
                Email = request.Email,
                Age = request.Age,
                Phone = request.Phone,
                Salary = request.Salary,
                Status = request.Status
            };

            //Call Repository to Update Employee Domain Model
            var updatedEmployee = await employeeRepository.UpdateAsync(employee);
            if (updatedEmployee == null)
            {
                return NotFound();
            }

            // Convert Domain Model back to DTO
            var response = new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Age = employee.Age,
                Phone = employee.Phone,
                Salary = employee.Salary,
                Status = employee.Status
            };

            return Ok(response);
        }

        //DELETE: {apibaseurl}/api/employees/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromBody] Guid id)
        {
            var deletedEmployees = await employeeRepository.DeleteAsync(id);

            if(deletedEmployees == null)
            {
                return NotFound();
            }

            //Convert Domain model to DTO
            var response = new EmployeeDto
            {
                Id = deletedEmployees.Id,
                Name = deletedEmployees.Name,
                Email = deletedEmployees.Email,
                Age = deletedEmployees.Age,
                Phone = deletedEmployees.Phone,
                Salary = deletedEmployees.Salary,
                Status = deletedEmployees.Status
            };

            return Ok(response);
        }
    }
}
