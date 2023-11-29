using System.Data.SqlClient;
using StoreProgram_.Repository.Interfaces;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Text;
using Microsoft.Data.SqlClient;
using MassTransit;

namespace StoreProgram_.Repository
{
    //це шаблон, що дозволяє створити універсальний інтерфейс для взаємодії з будь-яким типом сутностей в базі даних.
    //Він надає основні CRUD (створення, читання, оновлення, видалення) операції для роботи з цими сутностями.
    public class GenericRepository<T>:IGenericRepository<T> where T : class
    {
        private readonly DataContext _context;
        public GenericRepository(DataContext context)
        {
            _context = context;
        }

        protected static string GetEntityNotFoundErrorMessage(int id) =>
            $"{typeof(T).Name} with id {id} not found.";


        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        //Цей клас забезпечує загальні методи для роботи з будь-якими сутностями у базі даних, що спрощує роботу
        //з ними та дозволяє уникнути дублювання коду для різних типів сутностей.
        public async Task<T> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id)
                ?? throw new Exception(GetEntityNotFoundErrorMessage(id));
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.FindAsync<T>(id);
            if(entity == null)
            {
                return false;
            }
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddAsync(T t)
        {
            _context.Set<T>().Add(t);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> ReplacAsync(int id,T t)
        {
            var entry = _context.Entry(t);
            if (entry.State == EntityState.Detached)
            {
                var existingEntity = await _context.Set<T>().FindAsync(id);
                if (existingEntity == null)
                    return false;
                _context.Entry(existingEntity).CurrentValues.SetValues(t);
            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
