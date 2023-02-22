using System.ComponentModel.DataAnnotations;

namespace UI.ViewModel
{
    public class ChangeUserViewModel
    {
        [Required(ErrorMessage = "Поле не заполнено")]
        [Display(Name = "ФИО")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Поле не заполнено")]
        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Поле не заполнено")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string Sex { get; set; }
    }
}
