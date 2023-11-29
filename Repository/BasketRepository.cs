using Microsoft.Data.SqlClient;
using StoreProgram_.Model;
using StoreProgram_.Repository.Interfaces;
using System.Data;

namespace StoreProgram_.Repository
{
    public class BasketRepository : GenericRepository<Basket>, IBasketRepository
    {
       private readonly DataContext _dataContext;

        public BasketRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
        // Даний клас використовує Entity Framework Core для взаємодії з базою даних. Метод ToListAsync використовується для
        // асинхронного отримання списку кошиків з бази даних.
        public async Task<IEnumerable<Basket>> GetBasketsWithClientInfoAsync()
        {
            var basketsWithClientInfo = await _dataContext.Baskets
                .Include(basket => basket.Client)
                .ToListAsync();
            return basketsWithClientInfo;
        }
    }
}
// В цілому, цей клас BasketRepository реалізує специфічну функціональність, пов'язану з операціями з сутністю Basket,
// використовуючи Entity Framework Core для взаємодії з базою даних.
