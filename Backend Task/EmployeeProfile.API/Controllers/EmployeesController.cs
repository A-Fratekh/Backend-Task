
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

    [HttpGet("{id}")]

    public async Task<ActionResult<Employee>> Get(Guid id)
    {
        var result = await _mediator.Send(new GetEmployeeProfileQuery {EmployeeId=id});
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateEmployeeCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id = result }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateEmployeeCommand command)
    {
        if (id != command.Id)
            return BadRequest();

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id, DeleteEmployeeCommand command)
    {
        if (id != command.Id)
            return BadRequest();

        await _mediator.Send(command);
        return Ok();
    }
}
