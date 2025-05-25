using EmployeePortal.Data;
using EmployeePortal.Models.Domain;
using EmployeePortal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortal.Repositories.Implementation
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeeRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Employee> SaveAsync(Employee employee)
        {
            await dbContext.Employees.AddAsync(employee);
            await dbContext.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee?> DeleteAsync(Guid id)
        {
            var existingEmployee = await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if(existingEmployee is null)
            {
                return null;
            }

            dbContext.Employees.Remove(existingEmployee);
            await dbContext.SaveChangesAsync();
            return existingEmployee;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await dbContext.Employees.ToListAsync();
        }

        public async Task<Employee?> GetById(Guid id)
        {
            return await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Employee?> UpdateAsync(Employee employee)
        {
            var existingEmployee = await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == employee.Id);

            if(existingEmployee != null)
            {
                dbContext.Entry(existingEmployee).CurrentValues.SetValues(employee);
                await dbContext.SaveChangesAsync();
                return employee;
            }
            return null;
        }
    }
}
