using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StoreProgram_.DTO.Requests;
using StoreProgram_.DTO.Responses;
using StoreProgram_.Model;
using StoreProgram_.Repository.Interfaces;
using StoreProgram_.Service.Interfaces;

namespace StoreProgram_.Service
{
    //Цей клас ClientService відповідає за операції з клієнтами, такі як отримання, додавання, оновлення та видалення. 
    public class ClientService : IClientService
    {
        private readonly IUnityOfWorkRepository _unityOfWork;
        
        private readonly IMapper _mapper;

        private readonly IClientRepository _clientRepository;

        private readonly IDistributedCache _cache;

        public ClientService(IMapper mapper,IUnityOfWorkRepository unityOfWork, IDistributedCache cache)
        {
            this._unityOfWork = unityOfWork;
            _clientRepository = this._unityOfWork._clientRepository;
            this._mapper = mapper;
            this._cache = cache;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _clientRepository.DeleteAsync(id);
            await _unityOfWork.SaveChangesAsync();

            await _cache.RemoveAsync($"Client_{id}");

            return true;
        }

        public async Task<IEnumerable<ClientResponse>> GetAll()
        {
            //var clients = await _clientRepository.GetAllAsync();
            //return clients.Select(clients => _mapper.Map<Client, ClientResponse>(clients));
            var cacheData = await _cache.GetStringAsync("AllClients");
            if (!string.IsNullOrEmpty(cacheData))
            {
                return JsonConvert.DeserializeObject<IEnumerable<ClientResponse>>(cacheData);
            }
            else
            {
                var clients = await _clientRepository.GetAllAsync();
                var clientResponses = clients.Select(client => _mapper.Map<Client, ClientResponse>(client));

                await _cache.SetStringAsync("AllClients", JsonConvert.SerializeObject(clientResponses), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30),
                    SlidingExpiration = TimeSpan.FromMinutes(10),

                });
                return clientResponses;
            }
        }

        public async Task<ClientResponse> GetAsync(int id)
        {
            //var client = await _clientRepository.GetAsync(id);
            //return _mapper.Map<Client, ClientResponse>(client);
            var cacheData = await _cache.GetStringAsync($"Client_{id}");
            if (!string.IsNullOrEmpty(cacheData))
            {
                return JsonConvert.DeserializeObject<ClientResponse>(cacheData);
            }
            else
            {
                var client = await _clientRepository.GetAsync(id);
                var clientResponses = _mapper.Map<Client, ClientResponse>(client);
                await _cache.SetStringAsync($"Client_{id}", JsonConvert.SerializeObject(clientResponses), new DistributedCacheEntryOptions
                {

                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30),
                    SlidingExpiration = TimeSpan.FromMinutes(10)

                });
                return clientResponses;
            }
        }

        public async Task<ClientResponse> InsertAsync(ClientRequest newClient)
        {
           var client = _mapper.Map<ClientRequest, Client>(newClient);
            await _clientRepository.AddAsync(client);
            await _unityOfWork.SaveChangesAsync();

            await _cache.RemoveAsync("AllClients");

            return _mapper.Map<Client, ClientResponse>(client);
        }

        public async Task<IEnumerable<ClientResponse>> UpdateAsync(int id, ClientRequest updateClient)
        {
            var client = _mapper.Map<ClientRequest,Client>(updateClient);
            await _clientRepository.ReplacAsync(id, client);
            await _unityOfWork.SaveChangesAsync();

            await _cache.RemoveAsync("AllClients");

            return await GetAll();
        }
    }
}
//Цей сервіс управляє операціями, пов'язаними з клієнтами, та використовує кешування для зберігання та отримання даних,
//що дозволяє прискорити доступ до інформації та зменшити навантаження на базу даних.