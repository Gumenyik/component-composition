using System.ComponentModel.DataAnnotations;

namespace component_composition.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не вказаний Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не вказане пріщвище")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Не вказане імя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не вкзаний пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль не вірний")]
        public string ConfirmPassword { get; set; }
    }
}
