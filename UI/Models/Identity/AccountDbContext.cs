using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UI.Models.Identity
{
    public class AccountDbContext : IdentityDbContext<User>
    {
        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)
        {

        }
        public new DbSet<User> Users { get; set; }
    }
}
