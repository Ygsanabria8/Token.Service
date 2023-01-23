using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WaoVet.Token.Service.Domain;

namespace WaoVet.Token.Service.Infrastructure.DatabaseContext
{
    public class DataBaseContext : IdentityDbContext<UserModel>
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        {
        }

        public DbSet<UserModel> User { get; set; }

    }
}
