using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProConstructionsManagment.Core.Entities;
using ProConstructionsManagment.Core.Enums;
using ProConstructionsManagment.Core.Interfaces;

namespace ProConstructionsManagment.Web.Controllers
{
    [Route("api/v1/")]
    public class ProjectsController : Controller
    {
        private readonly IAsyncRepository<Project, ProjectStatus> _asyncRepository;
        private readonly IAsyncRepository<ProjectCost> _repo;
        private readonly IProjectsRepository _projectsRepository;
        private readonly IAsyncRepository<ProjectRecruitment> _projectRecruitmentRepository;

        public ProjectsController(IAsyncRepository<Project, ProjectStatus> asyncRepository, IAsyncRepository<ProjectCost> repo, IProjectsRepository projectsRepository, IAsyncRepository<ProjectRecruitment> projectRecruitmentRepository)
        {
            _asyncRepository = asyncRepository;
            _repo = repo;
            _projectsRepository = projectsRepository;
            _projectRecruitmentRepository = projectRecruitmentRepository;
        }

        [HttpGet]
        [Route("projects")]
        public async Task<IActionResult> GetProjects()
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
        [Route("projects/recruitments")]
        public async Task<IActionResult> GetProjectsRecruitments()
        {
            var result = await _projectRecruitmentRepository.GetAll();

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
        [Route("projects/started")]
        public async Task<IActionResult> GetStartedProjects()
        {
            var result = await _asyncRepository.GetAllByStatus(ProjectStatus.Started);

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
        [Route("projects/start")]
        public async Task<IActionResult> GetProjectsForStart()
        {
            var result = await _asyncRepository.GetAllByStatus(ProjectStatus.WaitingToStart);

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
        [Route("projects/settlement")]
        public async Task<IActionResult> GetProjectsForSettlement()
        {
            var result = await _asyncRepository.GetAllByStatus(ProjectStatus.ForSettlement);

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
        [Route("projects/settled")]
        public async Task<IActionResult> GetSettledProjects()
        {
            var result = await _asyncRepository.GetAllByStatus(ProjectStatus.Settled);

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
        [Route("projects/ended")]
        public async Task<IActionResult> GetEndedProjects()
        {
            var result = await _asyncRepository.GetAllByStatus(ProjectStatus.Ended);

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
        [Route("projects/{projectId}")]
        public async Task<IActionResult> GetProjectById(Guid projectId)
        {
            var result = await _asyncRepository.GetById(projectId);

            return Ok(new
            {
                data = result
            });
        }

        [HttpGet]
        [Route("projects/{projectId}/recruitments")]
        public async Task<IActionResult> GetProjectRecruitments(Guid projectId)
        {
            var result = await _projectsRepository.GetProjectRecruitments(projectId);

            return Ok(new
            {
                data = result
            });
        }

        [HttpGet]
        [Route("projects/recruitments/{projectRecruitmentId}")]
        public async Task<IActionResult> GetProjectRecruitmentById(Guid projectRecruitmentId)
        {
            var result = await _projectsRepository.GetProjectRecruitment(projectRecruitmentId);

            return Ok(new
            {
                data = result
            });
        }

        [HttpPost]
        [Route("projects/{projectId}/recruitment/add")]
        public async Task<IActionResult> AddProjectRecruitment([FromBody] ProjectRecruitment entity, Guid projectId)
        {
            var data = new ProjectRecruitment
            {
                Id = entity.Id,
                ProjectId = projectId,
                PositionId = entity.PositionId,
                RequiredNumberOfEmployees = entity.RequiredNumberOfEmployees
            };

            var result = await _projectRecruitmentRepository.Add(data);

            return Ok(result);
        }

        [HttpPost]
        [Route("project/add")]
        public async Task<IActionResult> AddProject([FromBody] Project entity)
        {
            var result = await _asyncRepository.Add(entity);

            return Ok(result);
        }

        [HttpPost]
        [Route("projects/{projectId}/update")]
        public async Task<IActionResult> UpdateProject([FromBody] Project entity, Guid projectId)
        {
            var result = await _asyncRepository.Update(entity, projectId);

            return Ok(result);
        }

        [HttpPost]
        [Route("projects/{projectId}/recruitment/update")]
        public async Task<IActionResult> UpdateAddProjectRecruitment([FromBody] ProjectRecruitment entity, Guid projectId)
        {
            var result = await _projectRecruitmentRepository.Update(entity, projectId);

            return Ok(result);
        }

        [HttpGet]
        [Route("projects/{projectId}/costs")]
        public async Task<IActionResult> GetProjectCosts(Guid projectId)
        {
            var result = await _projectsRepository.GetAllProjectCosts(projectId);

            return Ok(new
            {
                data = result
            });
        }

        [HttpGet]
        [Route("projects/{projectCostId}/cost")]
        public async Task<IActionResult> GetProjectCostById(Guid projectCostId)
        {
            var result = await _projectsRepository.GetProjectCost(projectCostId);

            return Ok(new
            {
                data = result
            });
        }

        [HttpPost]
        [Route("projects/{projectId}/costs/add")]
        public async Task<IActionResult> AddProjectCost([FromBody] ProjectCost entity, Guid projectId)
        {
            var data = new ProjectCost
            {
                Id = entity.Id,
                ProjectId = projectId,
                GrossAmount = entity.GrossAmount,
                CostDescription = entity.CostDescription
            };

            var result = await _repo.Add(data);

            return Ok(result);
        }
    }
}