using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TestApi.Servicios;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("v1/meta")]
    public class MetaController : ControllerBase
    {
        private readonly ILoggService loggService;
        private string mensajeLog;

        public MetaController(ILoggService loggService)
        {
            this.loggService = loggService;
        }
        [HttpGet("log")]
        public async Task<ActionResult<string>> Get() 
        {
            try
            {
               mensajeLog = await loggService.ReadLog();                                   
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);           
            }
            return Ok(mensajeLog);
        }
        [HttpGet("log/truncate")]
        public async Task<ActionResult> Truncate()
        {
            try
            {
                mensajeLog = await loggService.TruncateLog();
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

            return Ok(new { code = StatusCodes.Status200OK, message = mensajeLog });
        }
    }
}

