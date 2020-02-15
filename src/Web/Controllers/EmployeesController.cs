using Microsoft.AspNetCore.Mvc;
using ProConstructionsManagment.Infrastructure.Data.Models;
using ProConstructionsManagment.Infrastructure.Data.Repositories;
using ProConstructionsManagment.Infrastructure.Enums;
using System;
using System.Threading.Tasks;

namespace ProConstructionsManagment.Web.Controllers
{
    [Route("api/v1/")]
    public class EmployeesController : Controller
    {
        private readonly IBaseRepository<Employee, EmployeeStatus> _employeesRepository;

        public EmployeesController(IBaseRepository<Employee, EmployeeStatus> employeesRepository)
        {
            _employeesRepository = employeesRepository;
        }

        [HttpGet]
        [Route("employees")]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var result = await _employeesRepository.GetAll();

                return Ok(new
                {
                    data = result,
                    summaries = new
                    {
                        count = result.Count
                    }
                });
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("employees/hired")]
        public async Task<IActionResult> GetEmployeesHired()
        {
            try
            {
                var result = await _employeesRepository.GetAllByStatus(EmployeeStatus.Hired);

                return Ok(new
                {
                    data = result,
                    summaries = new
                    {
                        count = result.Count
                    }
                });
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("employees/hire")]
        public async Task<IActionResult> GetEmployeesForHire()
        {
            try
            {
                var result = await _employeesRepository.GetAllByStatus(EmployeeStatus.WaitingForHire);

                return Ok(new
                {
                    data = result,
                    summaries = new
                    {
                        count = result.Count
                    }
                });
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("employees/{employeeId}")]
        public async Task<IActionResult> GetEmployeeById(Guid employeeId)
        {
            try
            {
                var result = await _employeesRepository.GetById(employeeId);

                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("employee/add")]
        public async Task<IActionResult> AddEmployee([FromBody] Employee model)
        {
            try
            {
                var result = await _employeesRepository.Add(model);

                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("employees/{employeeId}/update")]
        public async Task<IActionResult> UpdateEmployee([FromBody] Employee model, Guid employeeId)
        {
            try
            {
                var result = await _employeesRepository.Update(model, employeeId);

                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}