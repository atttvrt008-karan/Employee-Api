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
    // [Route("api/[controller]")]
    public class LoginsController : ControllerBase
    {
        
      
        // private List<Login> _logins= new List<Login>
        // {
        //     new Login {id=1,Email="karan@gmail.com",password="karan123"},
        // };
         private readonly DatabaseContext _context;
        //    private StudentsContext _image;
        public LoginsController(DatabaseContext context )         
{             _context = context; 
            
if (_context.Logins.Count() == 0)             
{                 
   _context.Logins.Add(new Login{id=1,Email="karan@gmail.com",password="karan123"});                _context.SaveChanges();             
}         
}
  [HttpGet]
    [Route("api/[controller]")]
        public ActionResult <List<Login>> GetAll()
        {
            return _context.Logins.OrderBy(id => id).ToList();
        }
  [HttpPut]
        [Route("api/[controller]/detail/")]
        public IActionResult GetById(Login login)
        {
              var product1 = _context.Logins.Where(b =>b.Email ==login.Email).Where(b =>b.password ==login.password);
            if (product1 == null)
                return NotFound();

            return Ok(product1);

           
        }
        [HttpPost]  
        [Route("api/[controller]/Create/")]  
//To Add new employee record  
  public IActionResult AddLogin(Login login)  
        {  
            var product1 = _context.Logins.Count(a=>a.Email==login.Email);
      
            try  
            { 
             if (_context.Logins.Count(a=>a.Email==login.Email)==0 ){ 
                 _context.Logins.Add(login);  
             _context.SaveChanges();  
             return Ok(product1);
                  }  
            } catch  
            {  
                throw;  
            } 
            return Ok(product1);
               
                
             
           
        }
    }
}
