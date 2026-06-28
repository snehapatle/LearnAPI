using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UseAppSettings.Models;

namespace UseAppSettings.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _context;
        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{deptId}")]//model bindings - fromroute
        public async Task<IActionResult> GetDatafromroute([FromRoute] int deptId)
        {
            int countData =await _context.Employee.CountAsync(x=> x.DeptId == deptId);
            return Ok(new
            {
                Message = "Db Connected Successfully",
                TotalEmps = countData
            });
        }

        [HttpGet]//model bindings - fromQuery
        public async Task<IActionResult> GetDatafromQuery([FromQuery] int deptId)
        {
            int countData = await _context.Employee.CountAsync(x => x.DeptId == deptId);
            return Ok(new
            {
                Message = "Db Connected Successfully",
                TotalEmps = countData
            });
        }

        [HttpPost("FromBody")]//model bindings - fromBody
        public async Task<IActionResult> GetDatafromBody([FromBody] Employee emp)
        {
            var empData = await _context.Employee.Where(x => x.Name == emp.Name).ToListAsync();
            return Ok(new
            {
                Message = "Db Connected Successfully",
                TotalEmps = empData
            });
        }

        [HttpPost("AddEmp")]
        public async Task<IActionResult> AddEmp(Employee emp)
        {
            await _context.Employee.AddAsync(emp);
           await  _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("FomForm")]//model bindings - fromForm
        public IActionResult Upload([FromForm] EmployeeForm emp)
        {
            return Ok();
        }

        [HttpGet("GetAllRecords")]
        public IActionResult GetAllRecords()
        {
            var list = new List<string>{
                "Sneha",
                "Preesha",
                "Prashant"
            };

            return Ok(list);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            if (id == 1)
            {
                return Ok("Sneha");
            }
            else if (id == 2)
            {
                return Ok("Preesha");
            }
            else
                return Ok("Prashant");
        }

        [HttpPost("Create")]
        public IActionResult Create(Employee emp)
        {
            return CreatedAtAction(nameof(Create), emp);//201
        }

        [HttpPut("Update")]
        public IActionResult Update(Employee emp)
        {
            return Ok();
        }

        //[HttpPatch]
        //public IActionResult PartialUpdate(Employee emp)
        //{
        //    return Ok();
        //}

        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            return BadRequest();
        }
    }
}
