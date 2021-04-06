using FreeParser.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeParser.Controllers
{

    public class Work
	{
        public bool isWork { get; set; }
	}

    [ApiController]
    [Route("api/[controller]")]
    public class ParserController : ControllerBase
	{
        private readonly HostedService _backgroundParsing;
        public ParserController(IEnumerable<IHostedService> hostedService)
        {
            _backgroundParsing = hostedService.Where(c => c.GetType().Name == nameof(HostedService)).FirstOrDefault() as HostedService;
        }

        [HttpPost]
        public IActionResult WorkService([FromBody] Work work)
        {
            Task result;

			if (work.isWork == false)
			{
               result = _backgroundParsing.StopAsync(new System.Threading.CancellationToken());
                
			}
			else
			{
                result = _backgroundParsing.StartAsync(new System.Threading.CancellationToken());
                
            }

            return Ok(result);

        }
    }
}
