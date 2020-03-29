using Microsoft.AspNetCore.Mvc;
using ProConstructionsManagment.Core.Entities;
using ProConstructionsManagment.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProConstructionsManagment.Web.Controllers
{
    [Route("api/v1/")]
    public class PositionsController : Controller
    {
        private readonly IAsyncRepository<Position> _asyncRepository;

        public PositionsController(IAsyncRepository<Position> asyncRepository)
        {
            _asyncRepository = asyncRepository;
        }

        [HttpGet]
        [Route("positions")]
        public async Task<IActionResult> GetPositions()
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
        [Route("positions/{positionId}")]
        public async Task<IActionResult> GetPositionById(Guid positionId)
        {
            var result = await _asyncRepository.GetById(positionId);

            return Ok(new
            {
                data = result
            });
        }

        [HttpPost]
        [Route("position/add")]
        public async Task<IActionResult> AddPosition([FromBody] Position entity)
        {
            var result = await _asyncRepository.Add(entity);

            return Ok(result);
        }

        [HttpPost]
        [Route("positions/{positionId}/update")]
        public async Task<IActionResult> UpdatePosition([FromBody] Position entity, Guid positionId)
        {
            var result = await _asyncRepository.Update(entity, positionId);

            return Ok(result);
        }

        [HttpDelete]
        [Route("positions/{positionId}/remove")]
        public async Task<IActionResult> RemovePosition(Guid positionId)
        {
            var result = await _asyncRepository.Remove(positionId);

            return Ok(result);
        }
    }
}