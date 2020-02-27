using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProConstructionsManagment.Core.Enums;
using ProConstructionsManagment.Core.Interfaces;
using ProConstructionsManagment.Infrastructure.Data.Entities;

namespace ProConstructionsManagment.Web.Controllers
{
    [Route("api/v1/")]
    public class EmployeesController : Controller
    {
        private readonly IAsyncRepository<Employee, EmployeeStatus> _asyncRepository;

        public EmployeesController(IAsyncRepository<Employee, EmployeeStatus> asyncRepository)
        {
            _asyncRepository = asyncRepository;
        }

        [HttpGet]
        [Route("employees")]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var result = await _asyncRepository.GetAll();

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
                var result = await _asyncRepository.GetAllByStatus(EmployeeStatus.Hired);

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
                var result = await _asyncRepository.GetAllByStatus(EmployeeStatus.WaitingForHire);

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
                var result = await _asyncRepository.GetById(employeeId);

                return Ok(new
                {
                    data = result
                });
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("employee/add")]
        public async Task<IActionResult> AddEmployee([FromBody] Employee entity)
        {
            try
            {
                var result = await _asyncRepository.Add(entity);

                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("employees/{employeeId}/update")]
        public async Task<IActionResult> UpdateEmployee([FromBody] Employee entity, Guid employeeId)
        {
            try
            {
                var result = await _asyncRepository.Update(entity, employeeId);

                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}