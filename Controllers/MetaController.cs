using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using TestApi.DTOs;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("v1/meta")]
    public class MetaController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly string ruta;
        public MetaController(IConfiguration configuration)
        {
            this.configuration = configuration;
            ruta = configuration["LOG_FILE_PATH"];
        }
        [HttpGet("log")]
        public async Task<ActionResult<string>> Get() 
        {
            string mensajeLog = null;
            try
            {
                
                using (var sr = new StreamReader(ruta))
                {
                    mensajeLog = await sr.ReadToEndAsync();
                } 
                
            }
            catch
            {
                throw;
             
            }
            return mensajeLog;
        }
        [HttpGet("log/truncate")]
        public ActionResult<LogDTO> Truncate()
        {
            try
            {
                var fs = new FileStream(ruta, FileMode.Truncate);
                fs.Close();
                LogDTO log = new LogDTO
                {
                    Code = Ok().StatusCode,
                    Message = "log truncado"
                };
                return log;
            }
            catch 
            {
                throw;
            }
            
        }
    }
}
