﻿using MediatR;

namespace EmployeeProfile.Application.Commands.Departments;

public class UpdateDepartmentCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
