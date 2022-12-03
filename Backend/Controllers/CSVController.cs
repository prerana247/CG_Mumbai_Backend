using Backend.Models;
using Backend.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CSVController : ControllerBase
    {

        private readonly ICSVService _csvService;


        public CSVController(ICSVService csvService)
        {
            _csvService = csvService;
        }

        [HttpPost("write-employee-csv")]
        public async Task<IActionResult> WriteEmployeeCSV([FromBody] List<User> employees)
        {
            _csvService.WriteCSV<User>(employees);

            return Ok();
        }

        [HttpPost("read-employees-csv")]
        public async Task<IActionResult> GetEmployeeCSV([FromForm] IFormFileCollection file)
        {
            var employees = _csvService.ReadCSV<User>(file[0].OpenReadStream());

            return Ok(employees);
        }

    }
}
