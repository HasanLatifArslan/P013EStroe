using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;


namespace P013EStroe.MVCUI.Models
{
    public class AdminLoginViewModel
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "{0} Boş Geçilemez"), MaxLength(50)]
        public string Email { get; set; }
        [Display(Name = "Şifre"), DataType(DataType.Password), MaxLength(50), MinLength(3)]
        public string Password { get; set; }
        [ScaffoldColumn(false)]
        public string? ReturnUrl { get; set; }
    }
}
