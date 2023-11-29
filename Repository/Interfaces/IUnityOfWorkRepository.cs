namespace StoreProgram_.Repository.Interfaces
{
    public interface IUnityOfWorkRepository
    {
        IBasketRepository _basketRepository { get; }
        IClientRepository _clientRepository { get; }

        Task SaveChangesAsync();
    }
}
//Концепція Unit of Work дозволяє об'єднувати кілька операцій збереження у велику операцію, що забезпечує атомарність даних.
//Коли ви викликаєте SaveChangesAsync(), вся вибрана оперативна пам'ять відправляється в базу даних за одну транзакцію,
//що забезпечує консистентність даних.
//Атомарність даних гарантує, що транзакції виконуються надійно і безпечно, а будь-які непередбачені ситуації або помилки під час
//їх виконання не призводять до порушення цілісності бази даних.
