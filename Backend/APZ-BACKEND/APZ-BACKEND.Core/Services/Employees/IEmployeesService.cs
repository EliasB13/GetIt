﻿using APZ_BACKEND.Core.Dtos.Employee;
using APZ_BACKEND.Core.Entities;
using APZ_BACKEND.Core.Services.Communication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APZ_BACKEND.Core.Services.Employees
{
	public interface IEmployeesService
	{
		Task<IEnumerable<EmployeeDto>> GetBusinessEmployees(int businessUserId);
		Task<GenericServiceResponse<EmployeeDto>> AddEmployee(int businessUserId, string login);
		Task<GenericServiceResponse<Employee>> DeleteEmployee(int employeeId);
	}
}
