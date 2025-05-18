
using EmployeeProfile.Application.Commands.Departments;
using EmployeeProfile.Application.Commands.Employees;
using EmployeeProfile.Application.Queries.Employees;
using EmployeeProfile.Application.UnitOfWork;
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
    private readonly IUnitOfWork _unitOfWork;

    public EmployeesController(IMediator mediator, IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }
    [HttpGet]
    public async Task<ActionResult<List<Employee>>> GetAll()
    {
        var result = await _mediator.Send(new GetEmployeesListQuery());
        return Ok(result);
    }

    [HttpGet("{employeeNo}")]
    public async Task<ActionResult<Employee>> Get(int employeeNo)
    {
        var result = await _mediator.Send(new GetEmployeeProfileQuery {EmployeeNo= employeeNo });
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateEmployeeCommand request)
    {
        var result = await _mediator.Send(request);
        _unitOfWork.SaveChanges();
        return CreatedAtAction(nameof(Get), new { employeeNo = result }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int EmployeeNo, UpdateEmployeeCommand request)
    {
        if (EmployeeNo != request.EmployeeNo)
            return BadRequest();

        await _mediator.Send(request);
        _unitOfWork.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{employeeNo}")]
    public async Task<ActionResult> Delete(int employeeNo, DeleteEmployeeCommand request)
    {
        if (employeeNo != request.EmployeeNo)
            return BadRequest();
        await _mediator.Send(request);
        _unitOfWork.SaveChanges();
        return Ok();
    }
}
