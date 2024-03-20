using Microsoft.EntityFrameworkCore;

namespace LivrariaTech
{
    public class LivrariaBaseDados : DbContext
    {
        public LivrariaBaseDados(DbContextOptions options): base(options) { }
        public DbSet<AutorModel> Autor { get; set; } = null;

    }
}
