using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Pipelines.Sockets.Unofficial.Arenas;
using StoreProgram_.DTO.Requests;
using StoreProgram_.DTO.Responses;
using StoreProgram_.Model;
using StoreProgram_.Repository.Interfaces;
using StoreProgram_.Service.Interfaces;

namespace StoreProgram_.Service
{
    public class BasketService : IBasketService, IConsumer<Product>
    {
        private readonly IUnityOfWorkRepository _unityOfWork;

        private readonly IMapper _mapper;

        private readonly IBasketRepository _basketRepository;

        private readonly IMemoryCache _memoryCache;

        public BasketService(IMapper mapper, IUnityOfWorkRepository unityOfWork, IClientRepository basketRepository, IMemoryCache memoryCache)
        {
            this._unityOfWork = unityOfWork;
            _basketRepository = this._unityOfWork._basketRepository;
            this._mapper = mapper;
            _memoryCache = memoryCache;
        }


        private async Task<IEnumerable<BasketResponse>> GetCachedBasketAsync()
        {
            var cacheKey = "BasketCacheKey";

            if(_memoryCache.TryGetValue(cacheKey, out IEnumerable<BasketResponse> cachedBasket))
            {
                return cachedBasket;
            }
            else
            {
                var baskets = await _basketRepository.GetAllAsync();

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30),
                    Size = 100
                };
                cachedBasket = baskets.Select(basket => _mapper.Map<Basket, BasketResponse>(basket));
                
                _memoryCache.Set(cacheKey, cachedBasket);

                return cachedBasket;

            }
        }
        //Цей сервіс управляє всіма операціями, пов'язаними з кошиками, включаючи їх отримання, додавання, видалення, оновлення та
        //обробку нових продуктів для додавання до кошиків.
        private void RemoveCacheBasket()
        {
            _memoryCache.Remove("BasketCacheKey");
        }



        public async Task<bool> DeleteAsync(int id)
        {
            RemoveCacheBasket();

            await _basketRepository.DeleteAsync(id);
            await _unityOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<BasketResponse>> GetAll()
        {
            //var baskets = await _basketRepository.GetAllAsync();
            //return baskets.Select(baskets => _mapper.Map<Basket,BasketResponse>(baskets));
            return await GetCachedBasketAsync();
        }

        public async Task<BasketResponse> GetAsync(int id)
        {
            //var basket = await _basketRepository.GetAsync(id);
            //return _mapper.Map<Basket, BasketResponse>(basket);
            var cacheBaskets = await GetCachedBasketAsync();
            return cacheBaskets.FirstOrDefault(basket => basket.BasketID == id);
        }

        public async Task<BasketResponse> InsertAsync(BasketRequestcs newBasket)
        {
            RemoveCacheBasket();

            var basket = _mapper.Map<BasketRequestcs, Basket>(newBasket);
            await _basketRepository.AddAsync(basket);
            await _unityOfWork.SaveChangesAsync();
            return _mapper.Map<Basket, BasketResponse>(basket);
        }

        public async Task<IEnumerable<BasketResponse>> UpdateAsync(int id, BasketRequestcs updateBasket)
        {
            RemoveCacheBasket();

            var basket = _mapper.Map<BasketRequestcs, Basket>(updateBasket);
            await _basketRepository.ReplacAsync(id, basket);
            await _unityOfWork.SaveChangesAsync();
            return await GetAll();
        }

        public async Task<IEnumerable<BasketWithClientInfoResponse>> GetBasketsWithClientInfo()
        {
            var basketsWithClientInfo = await _basketRepository.GetBasketsWithClientInfoAsync();
            return _mapper.Map<IEnumerable<BasketWithClientInfoResponse>>(basketsWithClientInfo);
        }

        public async Task Consume(ConsumeContext<Product> context)
        {
            var product = context.Message;

            var newBasket = new Basket
            {
                ClientID = 1,
                ProductName = product.productName,
                Quantity = 1,
                Price = product.price
            };
            await _basketRepository.AddAsync(newBasket);
            await _unityOfWork.SaveChangesAsync();
        }
    }
}
