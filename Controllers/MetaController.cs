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

        public MetaController(ILoggService loggService)
        {
            this.loggService = loggService;
        }
        [HttpGet("log")]
        public async Task<ActionResult<string>> Get() 
        {
            string mensajeLog;
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
        public ActionResult Truncate()
        {
            try
            {
                loggService.Truncate();
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

            return Ok(new { code = StatusCodes.Status200OK, message = "log truncado" });
        }
    }
}

