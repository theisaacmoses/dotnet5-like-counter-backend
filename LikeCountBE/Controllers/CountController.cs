using LikeCountBE.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LikeCountBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountController : ControllerBase
    {

        private readonly ILogger<CountController> _logger;
        private readonly ICountService _service;


        public CountController(ILogger<CountController> logger, ICountService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet] 
        public int? Get()
        {
            return _service.GetCount();
        }

        [HttpGet]
        [Route("/test")]
        public string Test()
        {
            return "Working";
        }

        [HttpPost]
        [EnableCors("AllowOrigin")]
        public ActionResult Post([FromBody] int count)
        {
            return _service.UpdateCount(count) ? Ok() : new StatusCodeResult(500);
        }
    }
}
