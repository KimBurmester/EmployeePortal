using EmployeePortal.Models.Domain;

namespace EmployeePortal.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> SaveAsync(Employee employee);
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetById(Guid id);
        Task<Employee?> UpdateAsync(Employee employee);
        Task<Employee?> DeleteAsync(Guid id);
    }
}
