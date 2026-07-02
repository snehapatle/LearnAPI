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

        [HttpGet("LINQFIRST")]
        public IActionResult CheckFirst()
        {
            //var empFirst = _context.Employee.First();
            //var empLast = _context.Employee.OrderBy(x => x.ID).Last();
            //var empFirst = _context.Employee.FirstOrDefault(x => x.Name == "Sneha1");
            //var empSelect = _context.Employee.Select(x => x.Name == "sneha").ToList();
            //var empSelect1 = _context.Employee.Select(x => x.Name);
            //var emp = _context.Employee.Any();//true
            //var emp = _context.Employee.Any(x=> x.Name =="Preesha");//true
            //var emp = _context.Employee.SingleOrDefault();//error
            //var emp = _context.Employee.Single();//error
            //var emp = _context.Employee.SingleOrDefault(x => x.Name == "11");//null//
            // var emp = _context.Employee.Find(1);//returns data --array
            //var emp = _context.Employee.Find([1]);//returns data --array of objects 
            //var emp = _context.Employee.Where(x => x.ID == 1);//returns data - array of objects
            //var emp = _context.Employee.Where(x => x.Name.Contains("ha"));
            //var emp = _context.Employee.All(x => x.Name.Contains("ha"));
            // var emp = _context.Employee.Sum(x => x.Salary);
            //var emp = _context.Employee.Select(x=> x.Name).Distinct();
            //var emp = _context.Employee.AsEnumerable().Select(x => new
            //{
            //    x.Name,x.Salary
            //}).DistinctBy(x=>x.Name);
            //var emp = _context.Employee.Skip(2).Take(3);
            //var emp = _context.Employee.GroupBy(x => x.DeptId);

            //foreach (var grp in emp) {
            //    Console.WriteLine(grp.Key);
            //    foreach (var g in grp)
            //    {
            //        Console.WriteLine(g.Name + " " + g.Salary);
            //    }
            //}
            //var emp = _context.Employee.AsNoTracking().GroupBy(x => x.DeptId)
            //            .Select(x => new
            //            {
            //                DeptId = x.Key,
            //                Count = x.Count(),
            //                Emps = x.Sum(x=> x.Salary)
            //            }).ToList();
            //var emp = _context.Employee
            //            .Join(_context.Department,
            //            e => e.DeptId,
            //            d => d.DeptId,
            //            (e, d) => new
            //            {
            //                EmpName = e.Name,
            //                sal = e.Salary,
            //                DeptName = d.DeptName
            //            }).ToList();
            var emp = from e in _context.Employee
                      join d in _context.Department
                      on e.DeptId equals d.DeptId
                      into deptGroup
                      from d in deptGroup.DefaultIfEmpty()
                      select new
                      {
                          Emp = e.Name,
                          department = d != null ? d.DeptName : "no department"
                      };
            return Ok(new
            {
                data = emp
            });
        }

        [HttpGet("GetAllEmps")]
        public IActionResult GetAllEmps()
        {
            var emp = _context.Employee.ToList();
            return Ok(new
            {
                data = emp
            });
        }
    }
}
