using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
namespace Api.Model


{
    public class Attendance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int id { get; set; }
        public int employee_id { get; set; }
        public string firstname { get; set; }
              public string attendance { get; set; }


    }
}