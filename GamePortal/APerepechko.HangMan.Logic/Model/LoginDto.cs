using System.ComponentModel.DataAnnotations;

namespace GamePortal.Web.Api.Controllers.HangMan
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}