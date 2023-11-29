using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreProgram_.DTO.Requests;
using StoreProgram_.DTO.Responses;
using StoreProgram_.Model;
using StoreProgram_.Service.Interfaces;
using System.Security.Cryptography.X509Certificates;

namespace StoreProgram_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }
        //Отримує всі кошики за допомогою injected _basketService
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<List<Basket>>> GetAll()
        {
            return Ok(await _basketService.GetAll());
        }
        //Отримує кошик за його ідентифікатором.
        [HttpGet("GetBasketById")]
        public async Task<ActionResult<IEnumerable<BasketResponse>>> GetByBasketId(int id)
        {
            return Ok(await _basketService.GetAsync(id));
        }
        //Створює новий кошик, приймаючи дані з тіла запиту ( BasketRequestcs) і передає його в _basketService.
        [HttpPost("CreateNewBasket")]
        public async Task<ActionResult<List<Basket>>> CreateBasket([FromBody] BasketRequestcs basket)
        {
            await _basketService.InsertAsync(basket);
            return Ok(await _basketService.GetAll());
        }
        //Оновлює існуючий кошик, ідентифікований ідентифікатором, беручи оновлену інформацію з тіла запиту ( BasketRequestcs).
        [HttpPut("UpadateBasket")]
        public async Task<ActionResult<List<Basket>>> UpdateBasket(int id, [FromBody]BasketRequestcs request)
        {
            var basket = await _basketService.UpdateAsync(id, request);
            return Ok(await _basketService.GetAll());
        }
        //Видаляє кошик, ідентифікований за його ідентифікатором.
        [HttpDelete("DeleteBasket")]
        public async Task<ActionResult<List<Basket>>> Delete(int id)
        {
            await _basketService.DeleteAsync(id);
            return Ok(await _basketService.GetAll());
        }
        //Отримує кошики разом із пов’язаною інформацією про клієнта.
        [HttpGet("GetJoinBasketWithClient")]
        public async Task<IActionResult> GetBasketWithClientInfo()
        {
            var basketWithClientInfo = await _basketService.GetBasketsWithClientInfo();
            return Ok(basketWithClientInfo);
        }
    }
}
//цей контролер служить інтерфейсом для обробки різноманітних HTTP-запитів, пов’язаних із керуванням кошиками в додатку магазину,
//використовуючи впроваджену службу ( ) IBasketServiceдля виконання основних операцій бізнес-логіки.
