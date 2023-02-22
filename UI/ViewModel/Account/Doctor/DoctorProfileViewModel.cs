using BLL.DTO;

namespace UI.ViewModel
{
    public class DoctorProfileViewModel
    {
        public string? FullName { get; set; }
        public string? Description { get; set; }
        public List<SubscribeDTO>? Subs { get; set; }
        public bool IsSubs()
        {
            if (Subs?.Count > 0)
                return true;
            return false;
        }
    }
}
