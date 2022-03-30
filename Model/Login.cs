using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
namespace Api.Model


{
    // [Keyless]
    public class Login
    {
         [Key]
         [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int  id { get; set; }
        public string Email { get; set; }
         public string username { get; set; }
        public string password { get; set; }
    
    }
}