using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
namespace Api.Model


{
    // [Keyless]
    public class Data
    {

         public int  employee_id { get; set; }
        public string firstname { get; set; }
        //   public string attendance { get; set; }
         public string result { get; set; }
    }
}