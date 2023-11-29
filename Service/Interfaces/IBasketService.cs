using StoreProgram_.DTO.Requests;
using StoreProgram_.DTO.Responses;
using StoreProgram_.Model;

namespace StoreProgram_.Service.Interfaces
{
    public interface IBasketService
    {
        Task<IEnumerable<BasketResponse>> GetAll();
        Task <BasketResponse> GetAsync(int id);
        Task <bool> DeleteAsync(int id);
        Task<IEnumerable<BasketResponse>> UpdateAsync(int id,BasketRequestcs basket);
        Task<BasketResponse> InsertAsync(BasketRequestcs basket);
        Task<IEnumerable<BasketWithClientInfoResponse>> GetBasketsWithClientInfo();
    }
}
//Цей інтерфейс визначає базовий набір операцій, які можуть бути виконані з кошиками.
