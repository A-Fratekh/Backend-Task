using EmployeeProfile.Application.Commands.Departments;
using MediatR;
using EmployeeProfile.Application.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeProfile.Application.Queries.Departments;

namespace EmployeeProfile.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DepartmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<DepartmentDTO>>> GetAll()
    {
        var result = await _mediator.Send(new GetAllDepartmentsQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DepartmentDTO>> Get(Guid id)
    {
        var result = await _mediator.Send(new GetDepartmentByIdQuery { Id = id });
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateDepartmentCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id = result }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateDepartmentCommand command)
    {
        if (id != command.Id)
            return BadRequest();

        await _mediator.Send(command);
        return NoContent();
    }
}
