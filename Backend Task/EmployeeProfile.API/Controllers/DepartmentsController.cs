using EmployeeProfile.Application.Commands.Departments;
using MediatR;
using EmployeeProfile.Application.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeProfile.Application.Queries.Departments;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using System.Linq.Expressions;
using EmployeeProfile.Application.UnitOfWork;

namespace EmployeeProfile.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUnitOfWork<Department> _unitOfWork;

    public DepartmentsController(IMediator mediator, IUnitOfWork<Department> unitOfWork)
    {
        _mediator = mediator;
        _unitOfWork = unitOfWork;
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
    public Task<ActionResult> Create(CreateDepartmentCommand request)
    {
        var result = _mediator.Send(request);
        _unitOfWork.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = result }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateDepartmentCommand request)
    {
        if (id != request.Id)
            return BadRequest();

        await _mediator.Send(request);
        _unitOfWork.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id, DeleteDepartmentCommand request)
    {
        if (id != request.Id)
            return BadRequest();

        await _mediator.Send(request);
        _unitOfWork.SaveChanges();
        return Ok();
    }
}
