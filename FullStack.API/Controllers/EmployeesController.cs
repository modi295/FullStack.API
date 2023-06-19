using FullStack.API.Data;
using FullStack.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullStack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly FullStackDbContext _fullStackDbContext;
        public EmployeesController(FullStackDbContext fullStackDbContext)
        {
            _fullStackDbContext = fullStackDbContext;  

        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _fullStackDbContext.MyProperty.ToListAsync();
            return Ok(employees);

        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employeeRequest)
        {
            employeeRequest.Id= Guid.NewGuid();
            await _fullStackDbContext.MyProperty.AddAsync(employeeRequest);
            await _fullStackDbContext.SaveChangesAsync();
            return Ok(employeeRequest);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute]Guid id)
        {
            var employee = await _fullStackDbContext.MyProperty.FirstOrDefaultAsync(x => x.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee UpdateEmployeeRequest)
        {
            var employee= await _fullStackDbContext.MyProperty.FindAsync(id);

            if(employee == null)
            {
                return NotFound();
            }
            employee.Name = UpdateEmployeeRequest.Name;
            employee.Email = UpdateEmployeeRequest.Email;
            employee.Phone = UpdateEmployeeRequest.Phone;  
            employee.Salary = UpdateEmployeeRequest.Salary;
            employee.Department = UpdateEmployeeRequest.Department;

            await _fullStackDbContext.SaveChangesAsync();
            return Ok(employee);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _fullStackDbContext.MyProperty.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            _fullStackDbContext.MyProperty.Remove(employee);
            await _fullStackDbContext.SaveChangesAsync();

            return Ok(employee);
        }
    }

}
