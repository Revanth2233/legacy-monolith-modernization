using AutoMapper;
using LegacyModern.BAL.Interfaces;
using LegacyModern.DAL.Interfaces;
using LegacyModern.DTOs;
using LegacyModern.Models;

namespace LegacyModern.BAL.Services;

public class EmployeeService(
    IEmployeeRepository repo,
    IMapper mapper,
    ILogger<EmployeeService> logger) : IEmployeeService
{
    public async Task<EmployeeResponseDto> GetByIdAsync(int id)
    {
        var emp = await repo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Employee {id} not found.");
        return mapper.Map<EmployeeResponseDto>(emp);
    }

    public async Task<PagedResult<EmployeeResponseDto>> GetAllAsync(int page, int pageSize, string? department = null)
    {
        var paged = await repo.GetAllAsync(page, pageSize, department);
        var dtos = mapper.Map<IEnumerable<EmployeeResponseDto>>(paged.Items);
        return new PagedResult<EmployeeResponseDto>(dtos, paged.Total, paged.Page, paged.PageSize);
    }

    public async Task<EmployeeResponseDto> CreateAsync(CreateEmployeeDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Email))
            throw new ArgumentException("Email is required.");
        var emp = mapper.Map<Employee>(dto);
        var saved = await repo.AddAsync(emp);
        logger.LogInformation("Employee {Id} created: {Name}", saved.EmployeeId, $"{dto.FirstName} {dto.LastName}");
        return mapper.Map<EmployeeResponseDto>(saved);
    }

    public async Task<EmployeeResponseDto> UpdateAsync(int id, UpdateEmployeeDto dto)
    {
        var emp = await repo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Employee {id} not found.");

        if (dto.FirstName is not null) emp.FirstName = dto.FirstName;
        if (dto.LastName is not null) emp.LastName = dto.LastName;
        if (dto.Email is not null) emp.Email = dto.Email;
        if (dto.Department is not null) emp.Department = dto.Department;
        if (dto.Salary.HasValue) emp.Salary = dto.Salary.Value;
        if (dto.IsActive.HasValue) emp.IsActive = dto.IsActive.Value;

        await repo.UpdateAsync(emp);
        logger.LogInformation("Employee {Id} updated", id);
        return mapper.Map<EmployeeResponseDto>(emp);
    }

    public async Task DeleteAsync(int id)
    {
        if (!await repo.DeleteAsync(id))
            throw new KeyNotFoundException($"Employee {id} not found.");
        logger.LogInformation("Employee {Id} soft-deleted", id);
    }

    public async Task<IEnumerable<EmployeeResponseDto>> GetDirectReportsAsync(int managerId)
    {
        var reports = await repo.GetDirectReportsAsync(managerId);
        return mapper.Map<IEnumerable<EmployeeResponseDto>>(reports);
    }
}
