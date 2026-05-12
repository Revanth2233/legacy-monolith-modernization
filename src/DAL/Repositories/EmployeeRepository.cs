using LegacyModern.DAL.Interfaces;
using LegacyModern.Data;
using LegacyModern.DTOs;
using LegacyModern.Models;
using Microsoft.EntityFrameworkCore;

namespace LegacyModern.DAL.Repositories;

public class EmployeeRepository(AppDbContext db) : IEmployeeRepository
{
    public async Task<Employee?> GetByIdAsync(int id) =>
        await db.Employees.Include(e => e.Manager).FirstOrDefaultAsync(e => e.EmployeeId == id);

    public async Task<PagedResult<Employee>> GetAllAsync(int page, int pageSize, string? department = null)
    {
        var query = db.Employees.Include(e => e.Manager).Where(e => e.IsActive);
        if (!string.IsNullOrEmpty(department))
            query = query.Where(e => e.Department == department);

        var total = await query.CountAsync();
        var items = await query
            .OrderBy(e => e.LastName).ThenBy(e => e.FirstName)
            .Skip((page - 1) * pageSize).Take(pageSize)
            .ToListAsync();

        return new PagedResult<Employee>(items, total, page, pageSize);
    }

    public async Task<IEnumerable<Employee>> GetByDepartmentAsync(string department) =>
        await db.Employees.Where(e => e.Department == department && e.IsActive).ToListAsync();

    public async Task<Employee> AddAsync(Employee emp)
    {
        db.Employees.Add(emp);
        await db.SaveChangesAsync();
        return emp;
    }

    public async Task UpdateAsync(Employee emp)
    {
        db.Employees.Update(emp);
        await db.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var emp = await db.Employees.FindAsync(id);
        if (emp is null) return false;
        emp.IsActive = false; // Soft delete
        await db.SaveChangesAsync();
        return true;
    }

    public Task<bool> ExistsAsync(int id) => db.Employees.AnyAsync(e => e.EmployeeId == id);

    public async Task<IEnumerable<Employee>> GetDirectReportsAsync(int managerId) =>
        await db.Employees.Where(e => e.ManagerId == managerId && e.IsActive).ToListAsync();
}
