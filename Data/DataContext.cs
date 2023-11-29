using Microsoft.EntityFrameworkCore;
using StoreProgram_.Model;

namespace StoreProgram_.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Basket> Baskets { get; set; }
    }
}
// Він представляє контекст бази даних і надає доступ до сутностей Clientі Basketяк DbSetвластивостей,
// дозволяючи взаємодіяти з основною базою даних через основні функції Entity Framework.