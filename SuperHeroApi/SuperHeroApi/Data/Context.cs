namespace SuperHeroApi.Data
{
    public class Context : DbContext // 40:50
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<SuperHero> SuperHeroes { get; set; }
    }
}
