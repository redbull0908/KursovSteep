using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using UI.EF;
using UI.Models.Identity;
using UI.ViewModel;

namespace UI.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        RoleManager<IdentityRole> _roleManager;
        private readonly IServ<ServiceDTO> _service;
        private readonly IServ<ServiceCategoryDTO> _category;
        private readonly IServ<DoctorDTO> _doc;
        private readonly IServ<ScheduleDTO> _shed;
        private readonly IServ<SubscribeDTO> _subscribe;


        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager,
            IServ<ServiceDTO> service,
            IServ<ServiceCategoryDTO> category,
            IServ<DoctorDTO> doc,
            IServ<ScheduleDTO> shed,
            IServ<SubscribeDTO> subscribe)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _service = service;
            _category = category;
            _doc = doc;
            _shed = shed;
            _subscribe = subscribe;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Initializer()
        {
            await new UserDbInitializer(_userManager, _roleManager, _doc).Unit();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm )
        {
            if (ModelState.IsValid)
            {

                switch (ValidUser(vm.FullName, vm.Date, vm.PhoneNumber))
                {
                    case 1:
                        ModelState.AddModelError(string.Empty, "Проверьте ФИО введены некорректно");
                        return View();
                    case 2:
                        ModelState.AddModelError(string.Empty, "Возраст пользователя должен быть старше 16 лет");
                        return View();
                    case 3:
                        ModelState.AddModelError(string.Empty, "Введите номер телефона Беларуских операторов в международном формате");
                        return View();
                }

                if (_userManager.Users.Any(x => x.UserName == vm.Login))
                    ModelState.AddModelError(string.Empty, "Пользователь с таким логином уже зарезестрирован !");

                var user = new User
                {
                    FullName = vm.FullName,
                    Bithday = vm.Date,
                    Sex = vm.Sex,
                    UserName = vm.Login,
                    PhoneNumber = vm.PhoneNumber,
                };
                var result = await _userManager.CreateAsync(user, vm.Password);
                if (result.Succeeded)
                {
                    result = _userManager.AddToRoleAsync(user, "user").Result;
                    if (User.IsInRole("manage"))
                    {
                        ViewBag.userName = user.UserName;
                        return RedirectToAction(nameof(UserSubscribe),"Account",new { name = user.UserName });
                    }
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction(nameof(Login));
                }

            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var vm = new LoginViewModel();
                return View(vm);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(vm.Login);
                if (user is not null)
                {
                    var flag = await _userManager.CheckPasswordAsync(user, vm.Password);
                    if (flag)
                    {
                        await _signInManager.SignInAsync(user, false);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            ModelState.AddModelError(string.Empty, "Неправильный логин и (или) пароль");
            return View(vm);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult UserProfile(string login)
        {
            var user = _userManager.FindByNameAsync(login).Result;
            var vm = new UserProfileViewModel
            {
                FullName = user.FullName,
                Bithday = user.Bithday,
                PhoneNumber = user.PhoneNumber,
                Sex = user.Sex,
                Subs = _subscribe.GetAll().Where(x => x.UserName == user.UserName && x.Date > DateTime.Now).ToList()
            };
            return View(vm);
        }

        [HttpGet]
        public IActionResult UserSubscribe(string name = "")
        {
            ViewBag.name = name;
            var selectedIndex = 1;
            SelectList category = new(_category.GetAll(), "Id", "Name", selectedIndex);
            ViewBag.category = category;
            SelectList services = new(_service.GetAll().Where(x => x.ServiceCategoryId == selectedIndex).ToList(), "ServiceCategoryId", "Name");
            ViewBag.services = services;
            SelectList doctors = new(_doc.GetAll().Where(x => x.ServiceCategoryId == selectedIndex).ToList(), "Id", "FullName");
            ViewBag.doctors = doctors;
            SelectList date = new(_shed.GetAll().Where(x => x.DoctorId == selectedIndex && x.Date.Date >= DateTime.Now.Date && int.Parse(DateTime.Now.ToString("HH")) > 18).ToList(), "DoctorId", "Date");
            ViewBag.date = date;
            SelectList times = new(GetTime(date.DataValueField, doctors.DataValueField));
            ViewBag.times = times;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserSubscribe([FromForm] string service , [FromForm] string doctors, [FromForm] string dateName, [FromForm] string time , [FromForm] string serviceName , [FromForm]string userName)
        {
            if(time == "Запишитесь на другой день !")
            {
                return RedirectToAction("UserSubscribe","Account");
            }
                var name = _doc.FindById(int.Parse(doctors)).FullName;
                var serv = _service.GetAll().FirstOrDefault(x => x.Name == serviceName).Name;
                User user = null;
                if (User.IsInRole("manage"))
                {
                    user = await _userManager.FindByNameAsync(userName);
                }
                else
                {
                    user = await _userManager.FindByNameAsync(User.Identity.Name);

                }

            var sub = new SubscribeDTO()
                {
                    DoctorName = name,
                    Date = GetTime(time, DateTime.Parse(dateName)),
                    ServiceName = serv,
                    Time = time,
                    UserName = user.UserName,
                    UserFullName = user.FullName
            };
            _subscribe.Add(sub);
            var id = _subscribe.GetAll().FirstOrDefault(x => x.UserName == sub.UserName && x.Date == sub.Date).Id;
            return RedirectToAction(nameof(Luck), "Account", new {name = id} );
        }

        public IActionResult SubService(int id)
        {
            return PartialView(_service.GetAll().Where(x => x.ServiceCategoryId == id).ToList());
        }
        public IActionResult SubDoctors(int id)
        {
            return PartialView(_doc.GetAll().Where(x => x.ServiceCategoryId == id).ToList());
        }
        public IActionResult SubDate(int id)
        {
            return PartialView(_shed.GetAll().Where(x => x.DoctorId == id).ToList());
        }
        public IActionResult SubTime([FromQuery]string str)
        {
            var arr = str?.Split("||");
            return PartialView(GetTime(arr[0], arr[1]));
        }
        public List<string>GetTime(string date, string docName)
        {
            var timel = new List<string>()
                {
                "8:00","8:10","8:20","8:30","8:40","8:50",
                "9:00","9:10","9:20","9:30","9:40","9:50",
                "10:00","10:10","10:20","10:30","10:40","10:50",
                "11:00","11:10","11:20","11:30","11:40","11:50",
                "12:00","12:10","12:20","12:30","12:40","12:50",
                "13:00","13:10","13:20","13:30","13:40","13:50",
                "14:00","14:10","14:20","14:30","14:40","14:50",
                "15:00","15:10","15:20","15:30","15:40","15:50",
                "16:00","16:10","16:20","16:30","16:40","16:50",
                "17:00","17:10","17:20","17:30","17:40","17:50",
                "18:00","18:10","18:20","18:30","18:40","18:50"
                };

            var sub = _subscribe.GetAll().Where(x => x.Date.ToShortDateString() == date.Split(' ')[0] && x.DoctorName == docName).ToList();
            foreach (var item in sub)
            {
                var result = timel.FirstOrDefault(item.Time);
                if (result!=null)
                timel.Remove(item.Time);
            }
            if(date.Split(' ')[0] == DateTime.Now.ToString().Split(' ')[0])
            {
                var timeNow = DateTime.Now.ToShortTimeString();
                var pr = int.Parse(timeNow.Split(':')[1]);
                var str = timeNow.Split(':');


                if (int.Parse(str[0]) >= 18 || (int.Parse(str[1]) >= 50 && int.Parse(str[0]) == 18))
                {
                    timel.Clear();
                    timel.Add("Запишитесь на другой день !");
                    return timel;
                }

                if (pr % 10 != 0)
                {
                    var number = (10 - pr % 10) + pr;
                    
                    if (number == 60)
                    {
                        str[1] = "00";
                        str[0] = (int.Parse(str[0]) + 1).ToString();
                    }
                    else
                    {
                        str[1] = number.ToString();
                    }
                    timeNow = string.Join(":", str);

                    var index = timel.IndexOf(timeNow);
                    if(index >=0)
                    timel.RemoveRange(0, index);
                }
                else
                {
                    timeNow = string.Join(":", str);
                    var index = timel.IndexOf(timeNow);
                    if (index >= 0)
                        timel.RemoveRange(0, index);
                }
            }

            return timel;
        }

        public DateTime GetTime(string time, DateTime date)
        {
            var timel = time?.Split(':');
            return new DateTime(date.Year, date.Month, date.Day, int.Parse(timel[0]), int.Parse(timel[1]), 0);
        }

        [HttpGet]
        public IActionResult ChangeUser()
        {
                return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> ChangeUser(ChangeUserViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
                if (user != null)
                {
                    switch (ValidUser(vm.FullName,vm.Date,vm.PhoneNumber))
                    {
                        case 1:
                            ModelState.AddModelError(string.Empty,"Проверьте ФИО введены некорректно");
                            return View();
                        case 2:
                            ModelState.AddModelError(string.Empty, "Возраст пользователя должен быть старше 16 лет");
                            return View();
                        case 3:
                            ModelState.AddModelError(string.Empty, "Введите номер телефона Беларуских операторов в международном формате");
                            return View();
                    }

                    user.FullName = vm.FullName;
                    user.PhoneNumber = vm.PhoneNumber;
                    user.Bithday = vm.Date;
                    user.Sex = vm.Sex;

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            
            return View();
        }

        private int ValidUser(string name , DateTime? date , string? phoneNumber)
        {
            var regex = new Regex("^[А-ЯЁ][а-яё]* [А-ЯЁ][а-яё]*$");
            var match = regex.Match(name);
            if (!match.Success)
                return 1;

            DateTime Today = DateTime.Today;
            // Возраст в годах
            int YearCount = 0;
            if(date != null)
            {
                if (Today.Month <= date?.Month && Today.Day < date?.Day)
                {
                    YearCount = Convert.ToInt32(Today.Year - date?.Year) - 1;
                }
                else
                {
                    YearCount = Convert.ToInt32(Today.Year - date?.Year);
                }
                if (YearCount < 16)
                    return 2;
            }
            if(phoneNumber != null)
            {
                regex = new Regex("^\\s*\\+?375((33\\d{7})|(29\\d{7})|(44\\d{7}|)|(25\\d{7}))\\s*$");
                if (!regex.Match(phoneNumber).Success)
                    return 3;
            }

            return 0;
        }

        [HttpGet]
        public IActionResult ChangePass()
        {
           return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePass(ChangePassViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
                if (user != null)
                {
                    IdentityResult result = await _userManager.ChangePasswordAsync(user, vm.OldPassword, vm.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Неверный пароль");
                    }
                }
            }
            
            return View();
        }

        public async Task<IActionResult> DocProfile()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if(user != null)
            {
                var doc = _doc.FindById(int.Parse(user.Id));

                var vm = new DoctorProfileViewModel
                {
                    FullName = doc.FullName,
                    Description = doc.Description,
                    Subs = _subscribe.GetAll().Where(x => x.DoctorName == doc.FullName && DateTime.Now <= x.Date && DateTime.Now.AddDays(14) >= x.Date).OrderBy(x => x.Date).ToList()
                };
                return View(vm);
            }

            return View();
        }

        [HttpGet]
        public IActionResult ChangeDoctor()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> ChangeDoctor(ChangeDoctorViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
                if (user != null)
                {
                    switch (ValidUser(vm.FullName, null, null))
                    {
                        case 1:
                            ModelState.AddModelError(string.Empty, "Проверьте ФИО введены некорректно");
                            return View();
                    }

                    user.FullName = vm.FullName;

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        var doc = _doc.FindById(int.Parse(user.Id));
                        doc.Description = vm.Description;
                        _doc.Update(doc);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult ManageProfile()
        {
            var vm = _subscribe.GetAll().Where(x=>x.Date>=DateTime.Now && x.Date.Date<=DateTime.Now.Date).ToList();
            return View(vm);
        }

        [HttpGet]
        public IActionResult SearchUser()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Luck(string name)
        {
            var vm = _subscribe.GetAll().FirstOrDefault(x => x.Id == int.Parse(name));
            return View(vm);
        }

        [HttpGet]
        public IActionResult FoundUser([FromQuery]string UserName)
        {

            var users = _userManager.Users.Where(u => u.FullName.ToLower() == UserName.ToLower()).ToList();

            if(users?.Count > 0)
            {
                ViewBag.users = users;
                return PartialView("UserFound200");
            }

            return PartialView("UserFound404");
        }
    }
}