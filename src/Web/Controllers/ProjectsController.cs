using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProConstructionsManagment.Core.Enums;
using ProConstructionsManagment.Core.Interfaces;
using ProConstructionsManagment.Infrastructure.Data.Entities;

namespace ProConstructionsManagment.Web.Controllers
{
    [Route("api/v1/")]
    public class ProjectsController : Controller
    {
        private readonly IAsyncRepository<Project, ProjectStatus> _projectsRepository;

        public ProjectsController(IAsyncRepository<Project, ProjectStatus> projectsRepository)
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
        [Route("projects/ended")]
        public async Task<IActionResult> GetEndedProjects()
        {
            try
            {
                var result = await _projectsRepository.GetAllByStatus(ProjectStatus.Ended);

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

                return Ok(new
                {
                    data = result,
                });
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("project/add")]
        public async Task<IActionResult> AddProject([FromBody]Project entity)
        {
            try
            {
                var result = await _projectsRepository.Add(entity);

                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("projects/{projectId}/update")]
        public async Task<IActionResult> UpdateProject([FromBody]Project entity, Guid projectId)
        {
            try
            {
                var result = await _projectsRepository.Update(entity, projectId);

                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}