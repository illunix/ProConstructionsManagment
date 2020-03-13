using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProConstructionsManagment.Core.Entities;
using ProConstructionsManagment.Core.Enums;
using ProConstructionsManagment.Core.Interfaces;

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

        [HttpGet]
        [Route("employees/hired")]
        public async Task<IActionResult> GetEmployeesHired()
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

        [HttpGet]
        [Route("employees/hire")]
        public async Task<IActionResult> GetEmployeesForHire()
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

        [HttpGet]
        [Route("employees/{employeeId}")]
        public async Task<IActionResult> GetEmployeeById(Guid employeeId)
        {
            var result = await _asyncRepository.GetById(employeeId);

            return Ok(new
            {
                data = result
            });
        }

        [HttpPost]
        [Route("employee/add")]
        public async Task<IActionResult> AddEmployee([FromBody] Employee entity)
        {
            var result = await _asyncRepository.Add(entity);

            return Ok(result);
        }

        [HttpPost]
        [Route("employees/{employeeId}/update")]
        public async Task<IActionResult> UpdateEmployee([FromBody] Employee entity, Guid employeeId)
        {
            var result = await _asyncRepository.Update(entity, employeeId);

            return Ok(result);
        }
    }
}