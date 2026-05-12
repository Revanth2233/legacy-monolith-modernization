using LegacyModern.DTOs;
using LegacyModern.Models;

namespace LegacyModern.DAL.Interfaces;

public interface IEmployeeRepository
{
    Task<Employee?> GetByIdAsync(int id);
    Task<PagedResult<Employee>> GetAllAsync(int page, int pageSize, string? department = null);
    Task<IEnumerable<Employee>> GetByDepartmentAsync(string department);
    Task<Employee> AddAsync(Employee employee);
    Task UpdateAsync(Employee employee);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<IEnumerable<Employee>> GetDirectReportsAsync(int managerId);
}
