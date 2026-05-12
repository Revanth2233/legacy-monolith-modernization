using LegacyModern.BAL.Interfaces;
using LegacyModern.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LegacyModern.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController(IEmployeeService svc) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string? department = null)
        => Ok(await svc.GetAllAsync(page, pageSize, department));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
        => Ok(await svc.GetByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeDto dto)
    {
        var result = await svc.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.EmployeeId }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateEmployeeDto dto)
        => Ok(await svc.UpdateAsync(id, dto));

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await svc.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("{managerId:int}/direct-reports")]
    public async Task<IActionResult> GetDirectReports(int managerId)
        => Ok(await svc.GetDirectReportsAsync(managerId));
}
