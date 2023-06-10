using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace TestApi.Servicios
{
    public class LoggService : ILoggService
    {
        private readonly string ruta;
        public LoggService(IConfiguration configuration)
        {
           ruta = configuration["LOG_FILE_PATH"];
        }

        public async Task<string> ReadLog()
        {
            string mensajeLogg;

            using (var sr = new StreamReader(ruta))
            {
                mensajeLogg = await sr.ReadToEndAsync();
            }

            return mensajeLogg.Length > 0 ? mensajeLogg : "No logs";
        }

        public void TruncateLog()
        {
            var fs = new FileStream(ruta, FileMode.Truncate);
            fs.Close();
        }
    }
}
