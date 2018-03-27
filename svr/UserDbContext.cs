using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace image_storage {

    public class UserDbContext : IdentityDbContext {
        
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) {
        }
    }
}