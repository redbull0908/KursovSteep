using BLL.DTO;

namespace UI.ViewModel
{
    public class UserProfileViewModel
    {
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? Bithday { get; set; }
        public string? Sex { get; set; }
        public List<SubscribeDTO>? Subs {get;set;}
        public bool IsSubs()
        {
            if(Subs?.Count > 0)
                return true;
            return false;
        }
    }
}