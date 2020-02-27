using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProConstructionsManagment.Core.Interfaces;
using ProConstructionsManagment.Infrastructure.Data.Entities;

namespace ProConstructionsManagment.Web.Controllers
{
    [Route("api/v1/")]
    public class ClientsController : Controller
    {
        private readonly IAsyncRepository<Client> _asyncRepository;

        public ClientsController(IAsyncRepository<Client> asyncRepository)
        {
            _asyncRepository = asyncRepository;
        }

        [HttpGet]
        [Route("clients")]
        public async Task<IActionResult> GetClients()
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
        [Route("clients/{clientId}")]
        public async Task<IActionResult> GetEmployeeById(Guid clientId)
        {
            try
            {
                var result = await _asyncRepository.GetById(clientId);

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
        [Route("client/add")]
        public async Task<IActionResult> AddClient([FromBody] Client entity)
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
        [Route("clients/{clientId}/update")]
        public async Task<IActionResult> UpdateClient([FromBody] Client entity, Guid clientId)
        {
            try
            {
                var result = await _asyncRepository.Update(entity, clientId);

                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}