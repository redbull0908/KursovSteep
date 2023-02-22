using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using UI.Models;
using BLL.Services;
using BLL.Interfaces;
using AutoMapper;
using BLL.DTO;
using UI.ViewModel;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServ<ServiceDTO> _service;
        private readonly IServ<ServiceCategoryDTO> _category;
        private readonly IServ<DoctorDTO> _doc;

        public HomeController(IServ<ServiceDTO> service, IServ<ServiceCategoryDTO> category , IServ<DoctorDTO> doc)
        {
            _service = service;
            _category = category;
            _doc = doc;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetServiceCategory()
        {
            var category = _category.GetAll();
            var v = new ServiceCatalogViewModel() { CatalogCategory = _category.GetAll() ,
                Services = _service.GetAll().Where(x=> category.Any(c=>c.Id == x.ServiceCategoryId)).ToList()};
            return View(v);
        }

        [HttpGet]
        public IActionResult GetServices(string name)
        {
            var category = _category.GetAll().FirstOrDefault(x=>x.Name==name);
            var v = new ServiceCatalogViewModel()
            {
                CatalogCategory = _category.GetAll().Where(x => x.Name == name).ToList(),
                Services = _service.GetAll().Where(x => x.ServiceCategoryId == category.Id).ToList()
            };
            return View(v);
        }

        [HttpGet]
        public IActionResult Doctors()
        {
            var v = new DoctorCatalogViewModel()
            {
                Doctors = _doc.GetAll()
            };
            return View(v);
        }

    }
}