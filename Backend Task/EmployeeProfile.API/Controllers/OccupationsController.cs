﻿using EmployeeProfile.Application.Commands.Departments;
using EmployeeProfile.Application.Commands.Grades;
using EmployeeProfile.Application.Commands.Occupations;
using EmployeeProfile.Application.Queries.Grades;
using EmployeeProfile.Application.Queries.Occcupations;
using EmployeeProfile.Application.Queries.Occupations;
using EmployeeProfile.Application.UnitOfWork;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeProfile.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OccupationsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;
    public OccupationsController(IMediator mediator, IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }
    [HttpGet]
    public async Task<ActionResult<List<Occupation>>> GetAll()
    {
        var result = await _mediator.Send(new GetAllOccupationsQuery());
        if (result == null) return NotFound();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Occupation>> Get(Guid id)
    {
        var result = await _mediator.Send(new GetOccupationQuery { Id = id });
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateOccupationCommand request)
    {
        var result = await _mediator.Send(request);
        _unitOfWork.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = result }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateOccupationCommand request)
    {
        if (id != request.Id)
            return BadRequest();

        await _mediator.Send(request);
        _unitOfWork.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id, DeleteOccupationcommand request)
    {
        if (id != request.Id)
            return BadRequest();

        await _mediator.Send(request);
        _unitOfWork.SaveChanges();

        return Ok();
    }


}
