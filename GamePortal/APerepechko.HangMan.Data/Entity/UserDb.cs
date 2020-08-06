using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APerepechko.HangMan.Data
{
    public class UserDb
    {
        public int UserId { get; set; }
        public virtual UserStatisticsDb UserStatisticsId { get; set; }        
        public string UserName { get; set; }
      
        public string FirstName { get; set; }
       
        public string LastName { get; set; }
         
        public string Password { get; set; }
     
        public string Email { get; set; }
       
        public DateTime DateOfBirth { get; set; }
        public string Avatar { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
