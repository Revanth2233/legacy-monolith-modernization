namespace LegacyModern.DTOs;

public record CreateEmployeeDto(
    string FirstName, string LastName, string Email,
    string Department, decimal Salary, DateTime HireDate, int? ManagerId);

public record UpdateEmployeeDto(
    string? FirstName, string? LastName, string? Email,
    string? Department, decimal? Salary, bool? IsActive);

public record EmployeeResponseDto(
    int EmployeeId, string FirstName, string LastName,
    string Email, string Department, decimal Salary,
    DateTime HireDate, bool IsActive, string? ManagerName);

public record PagedResult<T>(IEnumerable<T> Items, int Total, int Page, int PageSize)
{
    public int TotalPages => (int)Math.Ceiling((double)Total / PageSize);
}
