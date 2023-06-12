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
            //Abre el archivo y lee el contenido
            using (var sr = new StreamReader(ruta))
            {
                mensajeLogg = await sr.ReadToEndAsync();
            }
            //Devuelve "No logs" si el archivo no tiene contenido que mostrar
            return mensajeLogg.Length > 0 ? mensajeLogg : "No logs";
        }

        public async Task<string> TruncateLog()
        {
            //Borra el contenido del archivo
            await File.WriteAllTextAsync(ruta, string.Empty);

            return "log truncado";
        }
    }
}
