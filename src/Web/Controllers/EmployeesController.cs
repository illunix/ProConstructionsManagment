using Microsoft.AspNetCore.Mvc;
using ProConstructionsManagment.Core.Entities;
using ProConstructionsManagment.Core.Enums;
using ProConstructionsManagment.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProConstructionsManagment.Web.Controllers
{
    [Route("api/v1/")]
    public class EmployeesController : Controller
    {
        private readonly IAsyncRepository<Employee, EmployeeStatus> _asyncRepository;
        private readonly IEmployeesService _employeesService;

        public EmployeesController(IAsyncRepository<Employee, EmployeeStatus> asyncRepository, IEmployeesService employeesService)
        {
            _asyncRepository = asyncRepository;
            _employeesService = employeesService;
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

        [HttpGet]
        [Route("employees/recruitment")]
        public async Task<IActionResult> GetAllEmployeesAbleToRecruit()
        {
            var result = await _employeesService.GetAllEmployeesAbleToRecruit();

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
        [Route("employees/positions/{positionId}")]
        public async Task<IActionResult> GetAllEmployeesByPosition(Guid positionId)
        {
            var result = await _employeesService.GetAllEmployeesByPosition(positionId);

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
        [Route("employees/recruitment/positions/{positionId}")]
        public async Task<IActionResult> GetAllEmployeesByPositionAbleToRecruit(Guid positionId)
        {
            var result = await _employeesService.GetAllEmployeesByPositionAbleToRecruit(positionId);

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
        [Route("employees/recruitment/projects/{projectId}/positions/{positionId}")]
        public async Task<IActionResult> GetAllEmployeesByPositionAbleToRecruit(Guid projectId, Guid positionId)
        {
            var result = await _employeesService.GetAllEmployeesByProjectIdAndPositionId(projectId, positionId);

            return Ok(new
            {
                data = result,
                summaries = new
                {
                    count = result.Count
                }
            });
        }
    }
}