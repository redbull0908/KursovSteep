using System.ComponentModel.DataAnnotations;

namespace UI.ViewModel
{
    public class ChangeDoctorViewModel
    {
        [Required(ErrorMessage = "Поле не заполнено")]
        [Display(Name = "Полное имя")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [Display(Name = "Информация о враче")]
        public string Description { get; set; }
    }
}
