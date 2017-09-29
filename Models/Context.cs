using Microsoft.EntityFrameworkCore;

namespace Match.Models
{
    public class Context : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Like> Likes { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Preference> Preferences { get; set; }
        public DbSet<LoveMatch> Matches { get; set; }
    }
}