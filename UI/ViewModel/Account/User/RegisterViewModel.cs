using System.ComponentModel.DataAnnotations;

namespace UI.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Поле не заполнено")]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [Display(Name = "ФИО")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public string Sex { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        public string PasswordConfirm { get; set; }
    }
}
