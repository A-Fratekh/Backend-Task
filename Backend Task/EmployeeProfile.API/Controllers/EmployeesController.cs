
using EmployeeProfile.Application.Commands.Departments;
using EmployeeProfile.Application.Commands.Employees;
using EmployeeProfile.Application.Queries.Employees;
using EmployeeProfile.Domain.Aggregates.EmployeeAggregate;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeProfile.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    public async Task<ActionResult<List<Employee>>> GetAll()
    {
        var result = await _mediator.Send(new GetEmployeesListQuery());
        return Ok(result);
    }

    [HttpGet("{employeeNumber}")]

    public async Task<ActionResult<Employee>> Get(int employeeNumber)
    {
        var result = await _mediator.Send(new GetEmployeeProfileQuery {EmployeeNo= employeeNumber });
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateEmployeeCommand request)
    {
        var result = await _mediator.Send(request);
        return CreatedAtAction(nameof(Get), new { number = result }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateEmployeeCommand request)
    {
        if (id != request.Id)
            return BadRequest();

        await _mediator.Send(request);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id, DeleteEmployeeCommand request)
    {
        if (id != request.Id)
            return BadRequest();

        await _mediator.Send(request);
        return Ok();
    }
}
