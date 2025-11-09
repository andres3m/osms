using Microsoft.AspNetCore.Mvc;
using OceanMonitoring.Infrastructure.Services;
using OceanMonitoring.Core.Models;


namespace OceanMonitoring.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SensorsController : ControllerBase
    {
        private readonly ISensorRepository _repo;


        public SensorsController(ISensorRepository repo) => _repo = repo;


        [HttpGet]
        public IActionResult GetAll() => Ok(_repo.GetLatestAll());


        [HttpGet("latest")]
        public IActionResult Latest() => Ok(_repo.GetLatestAll());


        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var s = _repo.GetLatest(id);
            if (s == null) return NotFound();
            return Ok(s);
        }
    }
}