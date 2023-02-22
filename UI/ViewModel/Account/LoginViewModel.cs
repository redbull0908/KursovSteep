using System.ComponentModel.DataAnnotations;

namespace UI.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Не задано имя пользователя")]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Не введен пароль")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
