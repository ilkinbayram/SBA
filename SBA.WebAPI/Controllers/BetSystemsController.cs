using Core.Entities.Dtos.SystemModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SBA.Business.Abstract;

namespace SBA.WebAPI.Controllers
{
    [Route("api/betsystems")]
    [ApiController]
    [BasicAuthentication]
    public class BetSystemsController : BaseWebApiController
    {
        private readonly IBetSystemService _systemService;

        public BetSystemsController(IBetSystemService systemService,
                                    IConfiguration configuration) : base(configuration)
        {
            _systemService = systemService;
        }

        [HttpGet("get/{id}")]
        public IActionResult Get(int id)
        {
            return Ok();
        }

        [HttpGet("get")]
        public IActionResult GetList()
        {
            return Ok();
        }

        [HttpPost("create")]
        public IActionResult Create(CreateSystemDto model)
        {
            var result = _systemService.Add(model);
            return Ok();
        }
    }
}
