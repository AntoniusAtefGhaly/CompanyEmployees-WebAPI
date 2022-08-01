using Contracts;
using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyEmployees.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;
        private readonly RepositoryContext db;


        public WeatherForecastController(IRepositoryManager repositoryManager, ILoggerManager logger, RepositoryContext context)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
            db = context;
        }

        //        var x= HttpContext;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet]
        public ActionResult<IEnumerable<Employee>> Get()
        {

            var employees = _repositoryManager.Employee.FindAll(true);
            //var employees = db.Employees;
            return Ok(employees);
        }

        //[HttpGet]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}
        //[HttpGet]
        //public IEnumerable<string> Get1()
        //{
        //    _logger.LogInfo("Here is info message from our values controller."); 
        //    _logger.LogDebug("Here is debug message from our values controller."); 
        //    _logger.LogWarn("Here is warn message from our values controller."); 
        //    _logger.LogError("Here is an error message from our values controller.");
        //    return new string[] { "value1", "value2" }; 
        //}
    }
}




//C:\Program Files\Eclipse Adoptium\jre-8.0.332.9-hotspot\bin;
//C:\Windows\system32;
//C:\Windows;
//C:\Windows\System32\Wbem;
//C:\Windows\System32\WindowsPowerShell\v1 .0\; C:\Windows\System32\OpenSSH\;
//C:\Program Files(x86)\NVIDIA Corporation\PhysX\Common;
//C:\Program Files\dotnet\;
//C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\170\Tools\Binn\;
//C:\Program Files(x86)\Microsoft SQL Server\150\Tools\Binn\;
//C:\Program Files\Microsoft SQL Server\150\Tools\Binn\;
//C:\Program Files\Microsoft SQL Server\150\DTS\Binn\;
//C:\Program Files(x86)\Microsoft SQL Server\150\DTS\Binn\;
//C:\Program Files\Azure Data Studio\bin;
//C:\Program Files\Git\cmd; 
//C:\WINDOWS\system32;
//C:\WINDOWS; 
//C:\WINDOWS\System32\Wbem; 
//C:\WINDOWS\System32\WindowsPowerShell\v1 .0\;
//C:\WINDOWS\System32\OpenSSH\;
//C:\Program Files\nodejs\;
//C:\Program Files\Docker\Docker\resources\bin;
//C:\ProgramData\DockerDesktop\version - bin;
//C:\Users\Antonius\AppData\Local\Microsoft\WindowsApps;
//C:\Users\Antonius\AppData\Local\Programs\Microsoft VS Code\bin;
//C:\Users\Antonius\.dotnet\tools; 
//C:\Windows\Microsoft.NET\Framework64\v4.0.30319; 
//C:\Users\Antonius\AppData\Roaming\npm