using EmployeeProfile.Application.Commands.Departments;
using MediatR;
using EmployeeProfile.Application.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeProfile.Application.Queries.Departments;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using System.Linq.Expressions;

namespace EmployeeProfile.API.Controllers;

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
    public async Task<ActionResult<List<Department>>> GetAll()
    {
        var result = await _mediator.Send(new GetAllDepartmentsQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Department>> Get(Guid id)
    {
        var result = await _mediator.Send(new GetDepartmentByIdQuery { Id = id });
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateDepartmentCommand request)
    {
        var result = await _mediator.Send(request);
        return CreatedAtAction(nameof(Get), new { id = result }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateDepartmentCommand request)
    {
        if (id != request.Id)
            return BadRequest();

        await _mediator.Send(request);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id, DeleteDepartmentCommand request)
    {
        if (id != request.Id)
            return BadRequest();

        await _mediator.Send(request);
        return Ok();
    }
}
