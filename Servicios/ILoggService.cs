﻿using System.Threading.Tasks;

namespace TestApi.Servicios
{
    public interface ILoggService 
    {
        Task<string> ReadLog();
        Task<string> TruncateLog();
    }
}
