using BLL.DTO;

namespace UI.ViewModel
{
    public class ServiceCatalogViewModel
    {
        public List<ServiceCategoryDTO>? CatalogCategory { get; set; }
        public List<ServiceDTO>? Services { get; set; }
    }
}
