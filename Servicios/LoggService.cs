﻿using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace TestApi.Servicios
{
    public interface ILoggService 
    {
        Task<string> ReadLog();
        void Truncate();
    }
    public class LoggService : ILoggService
    {
        private readonly IConfiguration configuration;
        private readonly string ruta;
        public LoggService(IConfiguration configuration)
        {
            this.configuration = configuration;
            ruta = configuration["LOG_FILE_PATH"];
        }

        public async Task<string> ReadLog()
        {
            string mensajeLogg = null;
            using (var sr = new StreamReader(ruta))
            {
                mensajeLogg = await sr.ReadToEndAsync();
            }
            return mensajeLogg;
        }

        public void Truncate()
        {
            var fs = new FileStream(ruta, FileMode.Truncate);
            fs.Close();
        }
    }
}
