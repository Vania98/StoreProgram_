using StoreProgram_.Model;

namespace StoreProgram_.Repository.Interfaces
{
    public interface IBasketRepository : IGenericRepository<Basket>
    {
        Task<IEnumerable<Basket>> GetBasketsWithClientInfoAsync();
    }
}
//підхід цього інтерфейсу полягає в створенні абстракції для доступу до даних, щоб відокремити логіку роботи
//з базою даних від рівня бізнес-логіки.