using System.ComponentModel.DataAnnotations;

namespace component_composition.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Не вказаний Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не вказаний пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
