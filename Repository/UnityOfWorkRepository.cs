using StoreProgram_.Repository.Interfaces;
using System.Data;

namespace StoreProgram_.Repository
{
    //Клас UnityOfWorkRepository є частиною патерна Unit of Work (одиниця роботи). Цей підхід використовується для керування транзакціями
    //та забезпечення консистентності даних у всіх операціях в межах однієї одиниці роботи.
    public class UnityOfWorkRepository : IUnityOfWorkRepository
    {
        //це шаблон, який об'єднує кілька операцій бази даних в одну транзакцію.Вона забезпечує контекст бази даних для виконання цих операцій
        //та забезпечує їх атомарність (всі операції успішні або жодна не виконується).
        protected readonly DataContext dataContext;
        public IBasketRepository _basketRepository { get; }
        public IClientRepository _clientRepository { get; }
        public UnityOfWorkRepository(
            DataContext dataContext,
            IBasketRepository basketRepository,
            IClientRepository clientRepository)
        {
            _basketRepository = basketRepository;
            _clientRepository = clientRepository;
            this.dataContext = dataContext;

        }
        //Клас UnityOfWorkRepository створений для спрощення управління транзакціями та забезпечення відправки змін до бази даних в одному блоку. 
        public async Task SaveChangesAsync()
        {
           await dataContext.SaveChangesAsync();
        }

    }
}
