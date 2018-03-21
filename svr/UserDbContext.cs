using Microsoft.EntityFrameworkCore;

namespace image_storage {

    class UserDbContext : DbContext {

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) {
        }        

        static UserDbContext New() {
            var optionsBuilder = new DbContextOptionsBuilder<UserDbContext>();
            optionsBuilder.UseSqlite("Data Source=users.db");
            return new UserDbContext(optionsBuilder.Options);            
        }
    }
}