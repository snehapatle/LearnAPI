using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace UseUserSecrets.Controllers
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

        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            int countData =await _context.Employee.CountAsync();
            return Ok(new
            {
                Message = "Db Connected Successfully through User Secrets",
                TotalEmps = countData
            });
        }
    }
}
