using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DeparmentAPI.Model
{
    public class UserContext:IdentityDbContext<ApplictionUser>
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options) { }
        public DbSet<FileModel> Files { get; set; }
        public DbSet<ApplictionUser> ApplictionUsers { get; set; }
        public DbSet<RMLUUsers> RMLUUsers { get; set; }
    }
}
