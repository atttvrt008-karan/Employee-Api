using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Api.Model;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Diagnostics;
using System;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout.Properties;

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
            var product = _context.Employees.OrderBy(m => m.employee_id)
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
                    result.department = employee.department;
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

        [HttpGet]
        [Route("api/[controller]/pdf/{employee_id}")]
        public IActionResult GetAll1(int employee_id)
        {
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(new FileStream("/myfiles/hello.pdf", FileMode.Create, FileAccess.Write)));
            Document document = new Document(pdfDocument);
            Paragraph newline = new Paragraph(new Text("\n"));
            Paragraph header = new Paragraph("Employee Details")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(20);
            LineSeparator ls = new LineSeparator(new SolidLine());


            //  int employee_id=1;
            var line = _context.Employees.SingleOrDefault(b => b.employee_id == employee_id);

            document.Add(header);
            document.Add(newline);
            document.Add(ls);
            document.Add(newline);
            document.Add(new Paragraph("Firstname : " + line.firstname));
            document.Add(new Paragraph("Lastname : " + line.lastname));
            document.Add(new Paragraph("Department : " + line.department));
            document.Add(new Paragraph("Address : " + line.address));
            document.Add(new Paragraph("Phoneno : " + line.phoneno));
            document.Add(new Paragraph( "Salary : " + line.salary.ToString()));


            int n = pdfDocument.GetNumberOfPages();
            for (int i = 1; i <= n; i++)
            {
                document.ShowTextAligned(new Paragraph(String
                   .Format("page" + i + " of " + n)),
                   559, 806, i, TextAlignment.RIGHT,
                   VerticalAlignment.TOP, 0);
            }
            document.Add(newline);
            Table table = new Table(2, false);
            Cell cell11 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("State"));
            Cell cell12 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Capital"));

            Cell cell21 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("New York"));
            Cell cell22 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Albany"));

            Cell cell31 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("New Jersey"));
            Cell cell32 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Trenton"));

            Cell cell41 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("California"));
            Cell cell42 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Sacramento"));

            table.AddCell(cell11);
            table.AddCell(cell12);
            table.AddCell(cell21);
            table.AddCell(cell22);
            table.AddCell(cell31);
            table.AddCell(cell32);
            table.AddCell(cell41);
            table.AddCell(cell42);


            document.Add(table);
            document.Add(newline);
            string base64 = line.image.Substring(line.image.IndexOf(',') + 1);
            base64 = base64.Trim('\0');
            byte[] chartData = Convert.FromBase64String(base64);

            Image img = new Image(ImageDataFactory
               // .Create(@"..\..\image.jpg"))
               .Create(@chartData))
               .SetTextAlignment(TextAlignment.CENTER);
            document.Add(img);
            document.Add(newline);


            Link link = new Link("click here",
                       PdfAction.CreateURI("https://www.google.com"));
            Paragraph hyperLink = new Paragraph("Please ")
               .Add(link.SetBold().SetUnderline()
               .SetItalic().SetFontColor(ColorConstants.BLUE))
               .Add(" to go www.google.com.");

            document.Add(newline);
            document.Add(hyperLink);

            document.Close();
            // Console.WriteLine("Awesome PDF just got created.");

            return Ok(pdfDocument);
        }

    }
}