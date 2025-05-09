using EmployeeProfile.Application.Commands.Departments;
using EmployeeProfile.Application.Commands.Grades;
using EmployeeProfile.Application.Queries.Departments;
using EmployeeProfile.Application.Queries.Grades;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeProfile.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GradesController : ControllerBase
{
    private readonly IMediator _mediator;

    public GradesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<Grade>>> GetAll()
    {
        var result = await _mediator.Send(new GetAllGradesQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Grade>> Get(Guid id)
    {
        var result = await _mediator.Send(new GetGradeQuery { Id = id });
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateGradeCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id = result }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateGradeCommand command)
    {
        if (id != command.Id)
            return BadRequest();

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id, DeleteGradeCommand command)
    {
        if (id != command.Id)
            return BadRequest();

        await _mediator.Send(command);
        return Ok();
    }
}
