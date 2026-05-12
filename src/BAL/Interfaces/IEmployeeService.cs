using LegacyModern.DTOs;

namespace LegacyModern.BAL.Interfaces;

public interface IEmployeeService
{
    Task<EmployeeResponseDto> GetByIdAsync(int id);
    Task<PagedResult<EmployeeResponseDto>> GetAllAsync(int page, int pageSize, string? department = null);
    Task<EmployeeResponseDto> CreateAsync(CreateEmployeeDto dto);
    Task<EmployeeResponseDto> UpdateAsync(int id, UpdateEmployeeDto dto);
    Task DeleteAsync(int id);
    Task<IEnumerable<EmployeeResponseDto>> GetDirectReportsAsync(int managerId);
}
