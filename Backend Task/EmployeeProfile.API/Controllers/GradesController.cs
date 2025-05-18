using EmployeeProfile.Application.Commands.Departments;
using EmployeeProfile.Application.Commands.Grades;
using EmployeeProfile.Application.Queries.Departments;
using EmployeeProfile.Application.Queries.Grades;
using EmployeeProfile.Application.UnitOfWork;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Aggregates.GradeAggregate;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeProfile.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GradesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public GradesController(IMediator mediator, IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _unitOfWork = unitOfWork;
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
    public async Task<ActionResult<Guid>> Create(CreateGradeCommand request)
    {
        var result = await _mediator.Send(request);
        _unitOfWork.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = result }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateGradeCommand request)
    {
        if (id != request.Id)
            return BadRequest();

        await _mediator.Send(request);
        _unitOfWork.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id, DeleteGradeCommand request)
    {
        if (id != request.Id)
            return BadRequest();
        await _mediator.Send(request);
        _unitOfWork.SaveChanges();
        return Ok();
    }
}
