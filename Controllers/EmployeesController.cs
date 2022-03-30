using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Api.Model;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Diagnostics;

namespace Api.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {


        // private List<Employee> _employees = new List<Employee>
        // {
        //     new Employee { employee_id=1, firstname = "Karan" ,lastname ="c", department ="IT" ,phoneno ="8012261027" ,address="chennai",image="123",salary=50000 },
        // };
        private readonly DatabaseContext _context;
        //    private StudentsContext _image;
        public EmployeesController(DatabaseContext context)
        {
            _context = context;
                      
            if (_context.Employees.Count() == 0)
            {
                _context.Employees.Add(new Employee { employee_id = 1, firstname = "Karan", lastname = "c", department = "IT", phoneno = "8012261027", address = "chennai", image = "123", salary = 50000 }); _context.SaveChanges();
            }
        }
        [HttpGet]
        [Route("api/[controller]/")]
        public IActionResult GetAll()
        {
            var product = _context.Employees .OrderBy(m => m.employee_id)
          .ToList();
            if (product == null)
                return NotFound();

            return Ok(product);


        }
        [HttpGet]
        [Route("api/[controller]/{rollno}")]
        public IActionResult GetById(int rollno)
        {
            var product = _context.Employees.Find(rollno);
            if (product == null)
                return NotFound();

            return Ok(product);


        }
        [HttpPost]
        [Route("api/[controller]/Create")]
        //To Add new employee record  
        public int AddEmployee(Employee employee)
        {
            try
            {

                _context.Employees.Add(employee);
                _context.SaveChanges();
                var employee_id = employee.employee_id;

                return employee_id;
            }
            catch
            {
                throw;
            }
        }
        [HttpPut]
        [Route("api/[controller]/Edit/{employee_id}")]

        //To Update the records of a particluar employee    
        public int UpdateEmployees(int employee_id, Employee employee)
        {
            try
            {


                var result = _context.Employees.SingleOrDefault(b => b.employee_id == employee_id);
                ;
                if (result != null)
                {
                    result.firstname = employee.firstname;
                    result.lastname = employee.lastname;
                    result.address = employee.address;
                    result.salary = employee.salary;
                    result.image = employee.image;
                    result.phoneno = employee.phoneno;
                    result.department =employee.department;
                    _context.SaveChanges();
                }
                return 1;
            }
            catch
            {
                throw;
            }
        }
        [HttpDelete]  
        [Route("api/[controller]/Delete/{employee_id}")] 
         //To Delete the record of a particular employee    
        public int DeleteEmployees(int employee_id)  
        {  
            try  
            {  
              Employee emp = _context.Employees.Where(b => b.employee_id == employee_id).First();
                 
               _context.Employees.Remove(emp);  
                _context.SaveChanges();  
               
                return 1;  
            }  
            catch  
            {  
                throw;  
            }  
        } 
      
    }
}