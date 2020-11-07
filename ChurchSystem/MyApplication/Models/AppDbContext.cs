using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApplication.Models
{
    class AppDbContext : DbContext
    {
        public AppDbContext() : base("DefaultConnection")
        {
        }

        public DbSet<Town> Towns { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Collage> Collages { get; set; }
        public DbSet<People> Peoples { get; set; }
        public DbSet<Death> Deaths { get; set; }
        public DbSet<Lack> Lacks { get; set; }
        public DbSet<Confession> Confessions { get; set; }
        public DbSet<Graduate> Graduates { get; set; }
        public DbSet<Nursery> Nurseries { get; set; }
        public DbSet<OutStanding> OutStandings { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Sitting> Sittings { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
