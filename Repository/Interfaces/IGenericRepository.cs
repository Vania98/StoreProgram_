namespace StoreProgram_.Repository.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<bool> ReplacAsync(int id,T t);
        Task<bool> AddAsync(T t);

    }
}
//Цей інтерфейс визначає загальний контракт репозиторію, який може бути реалізований різними репозиторіями для виконання операцій CRUD
//(Створення, читання, оновлення, видалення) над об’єктами будь-якого типу класу T.