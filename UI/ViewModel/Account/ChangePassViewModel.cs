using System.ComponentModel.DataAnnotations;

namespace UI.ViewModel
{
    public class ChangePassViewModel
    {
        [Required(ErrorMessage = "Поле не заполнено")]
        [DataType(DataType.Password)]
        [Display(Name = "Введите старый пароль")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Поле не заполнено")]
        [DataType(DataType.Password)]
        [Display(Name = "Введите новый пароль")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Поле не заполнено")]
        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        public string PasswordConfirm { get; set; }
    }
}
