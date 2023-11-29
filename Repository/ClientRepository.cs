using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StoreProgram_.Model;
using StoreProgram_.Repository.Interfaces;
using System.Data;

namespace StoreProgram_.Repository
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        public ClientRepository(DataContext context) : base(context)
        {
        }
    }
    }
//Цей клас ClientRepository може бути використаний для виконання різних операцій з сутністю Client в базі даних,
//використовуючи методи, які вже визначені в загальному репозиторії GenericRepository<Client>.
