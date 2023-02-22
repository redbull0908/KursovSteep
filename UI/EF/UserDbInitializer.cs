using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Identity;
using UI.Models.Identity;

namespace UI.EF
{
    internal class UserDbInitializer
    {
        UserManager<User> userManager;
        RoleManager<IdentityRole> roleManager;
        IServ<DoctorDTO> _doc;

        public UserDbInitializer(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IServ<DoctorDTO> doc)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _doc = doc;
        }

        public async Task Unit()
        {
            var doctors = _doc.GetAll();

            if (await roleManager.FindByNameAsync("manage") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("manage"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            if (await roleManager.FindByNameAsync("doctor") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("doctor"));
            }
            string nik = "qwerty";
            string password = "&Aa1234";

            if (await userManager.FindByNameAsync(nik) == null)
            {
                User user = new()
                {
                    UserName = nik,
                    FullName = "Иванов Иван",
                    PhoneNumber = "+375299645732",
                    Sex = "Мужской",
                    Bithday = new(1990,3,22)
                };
                IdentityResult result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "user");

                }

            }

            nik = "manage";
            password = "&Man572";

            if (await userManager.FindByNameAsync(nik) == null)
            {
                User admin = new()
                {
                    UserName = nik,
                    FullName = "Регистратура",
                };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "manage");
                }

            }

            for (int i = 0; i < doctors.Count; i++)
            {
                nik = "doc"+i;
                password = "&Doc728"+i;

                if (await userManager.FindByNameAsync(nik) == null)
                {
                    User doc = new()
                    {
                        UserName = nik,
                        Id = doctors[i].Id.ToString()
                    };
                    IdentityResult result = await userManager.CreateAsync(doc, password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(doc, "doctor");
                    }

                }
            }
        }
    }
}
