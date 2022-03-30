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

    public class AttendancesController : ControllerBase
    {


        // private List<Attendance> _attendances = new List<Attendance>
        // {
        //     new Attendance { id =1, employee_id=1, firstname = "Karan" ,attendance ="Present" },
        // };
        private readonly DatabaseContext _context;

        public AttendancesController(DatabaseContext context)
        {
            _context = context;

            if (_context.Attendances.Count() == 0)
            {
                _context.Attendances.Add(new Attendance { id = 1, employee_id = 1, firstname = "Karan", attendance = "Present" }); _context.SaveChanges();
            }
        }
        [HttpGet]
        [Route("api/[controller]/")]
        public IActionResult GetAll()
        {
            var product = _context.Attendances.OrderBy(m => m.employee_id)
          .ToList();
            if (product == null)
                return NotFound();

            return Ok(product);


        }
        [HttpGet]
        [Route("api/[controller]/{rollno}")]
        public IActionResult GetById(int rollno)
        {
            var product = _context.Attendances.Find(rollno);
            if (product == null)
                return NotFound();

            return Ok(product);


        }
        [HttpPost]
        [Route("api/[controller]/Create")]
        //To Add new employee record  
        public int AddAttendances(Attendance attendance)
        {
            try
            {

                _context.Attendances.Add(attendance);
                _context.SaveChanges();
                var id = attendance.id;

                return id;
            }
            catch
            {
                throw;
            }
        }
        [HttpPut]
        [Route("api/[controller]/Edit/{employee_id}")]

        //To Update the records of a particluar employee    
        public int UpdateAttendance(int employee_id, Attendance attendance)
        {
            try
            {


                var result = _context.Attendances.SingleOrDefault(b => b.employee_id == employee_id);
                ;
                if (result != null)
                {
                   
                    result.attendance =attendance.attendance;
                    _context.SaveChanges();
                }
                return 1;
            }
            catch
            {
                throw;
            }
        }
        [HttpGet]
        [Route("api/[controller]/count")]
        public List<Count> GetAllcount()

        {

            var query =

              (
                  from a in _context.Attendances
                  join e in _context.Employees
                 on a.employee_id equals e.employee_id
                  select new Data
                  {
                      firstname = e.firstname,
                      employee_id = e.employee_id,
                    //attendance = a.attendance,
                      
                      result = a.attendance == "Present" ? "Present" : a.attendance == "Casual Leave" ? "Casual Leave":a.attendance == "Sick Leave" ? "Sick Leave" : "Loss of Pay Leave"
                  }).ToList();
            var results = query.GroupBy(
            p => p.result)
            .Select(p => new Count
            {
                result = p.Key,
                count = p.Count()

            });

            return results.ToList();
        }

           [HttpGet]
        [Route("api/[controller]/find/{key}")]
        public List<Data>  GetAllfind(int key)

        {
           
 var query =

              (
                  from a in _context.Attendances
                  join e in _context.Employees
                 on a.employee_id equals e.employee_id
                  select new Data
                  {
                      firstname = e.firstname,
                      employee_id = e.employee_id,
                    //   attendance = a.attendance,
                      
                      result = a.attendance == "Present" ? "Present" : a.attendance == "Casual Leave" ? "Casual Leave":a.attendance == "Sick Leave" ? "Sick Leave" : "Loss of Pay Leave"
                  }).ToList();
   if(key == 0)
   {
     var results = query.Where(
    p => p.result=="Present");
    return results.ToList();
        }
        else if(key == 1)
        {
     var results = query.Where(
    p => p.result=="Casual Leave");
    return results.ToList();
        }
    else if(key ==2)
    {
      var results = query.Where(
    p => p.result=="Sick Leave");
    return results.ToList();
    }
     else if(key ==3)
    {
      var results = query.Where(
    p => p.result=="Loss of Pay Leave");
    return results.ToList();
    }
    return query;
    } 




    }
}