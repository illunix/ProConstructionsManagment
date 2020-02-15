using Microsoft.AspNetCore.Mvc;
using ProConstructionsManagment.Infrastructure.Data.Models;
using ProConstructionsManagment.Infrastructure.Data.Repositories;
using ProConstructionsManagment.Infrastructure.Enums;
using System;
using System.Threading.Tasks;
using ProConstructionsManagment.Infrastructure.Data;

namespace ProConstructionsManagment.Web.Controllers
{
    [Route("api/v1/")]
    public class ProjectsController : Controller
    {
        private readonly IBaseRepository<Project, ProjectStatus> _projectsRepository;

        public ProjectsController(IBaseRepository<Project, ProjectStatus> projectsRepository)
        {
            _projectsRepository = projectsRepository;
        }

        [HttpGet]
        [Route("projects")]
        public async Task<IActionResult> GetProjects()
        {
            try
            {
                var result = await _projectsRepository.GetAll();

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
        [Route("projects/started")]
        public async Task<IActionResult> GetStartedProjects()
        {
            try
            {
                var result = await _projectsRepository.GetAllByStatus(ProjectStatus.Started);

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
        [Route("projects/start")]
        public async Task<IActionResult> GetProjectsForStart()
        {
            try
            {
                var result = await _projectsRepository.GetAllByStatus(ProjectStatus.WaitingForStart);

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
        [Route("projects/settlement")]
        public async Task<IActionResult> GetProjectsForSettlement()
        {
            try
            {
                var result = await _projectsRepository.GetAllByStatus(ProjectStatus.ForSettlement);

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
        [Route("projects/settled")]
        public async Task<IActionResult> GetSettledProjects()
        {
            try
            {
                var result = await _projectsRepository.GetAllByStatus(ProjectStatus.Settled);

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
        [Route("projects/{projectId}")]
        public async Task<IActionResult> GetProjectById(Guid projectId)
        {
            try
            {
                var result = await _projectsRepository.GetById(projectId);

                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("project/add")]
        public async Task<IActionResult> AddEmployee([FromBody] Project model)
        {
            try
            {
                var result = await _projectsRepository.Add(model);

                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("projects/{projectId}/update")]
        public async Task<IActionResult> UpdateEmployee([FromBody] Project model, Guid projectId)
        {
            try
            {
                var result = await _projectsRepository.Update(model, projectId);

                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}