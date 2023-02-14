using Microsoft.EntityFrameworkCore;

namespace EntityRelationship.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Weapon> Weapon { get; set; }

        public DbSet<Skills> Skill { get; set; }
    }
}
