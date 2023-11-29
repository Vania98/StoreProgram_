using AutoMapper;
using StoreProgram_.DTO.Requests;
using StoreProgram_.DTO.Responses;
using StoreProgram_.Model;

namespace StoreProgram_.Configurations
{
    public class AutoMapperProfile : Profile
    {
        //AutoMapper — це популярна бібліотека зіставлення об’єкта на об’єкт, яка використовується
        //в .NET для спрощення зіставлення між різними типами.
        public AutoMapperProfile()
        {
            CreateMapperClient();
            CreateMapperBasket();
        }
        //Цей метод визначає відображення для Client, ClientResponse, та ClientRequest об’єктів.
        //Він встановлює зіставлення між Clientсутністю та її відповідними DTO ( ClientResponseі ClientRequest) в обох напрямках.
        private void CreateMapperClient()
        {
            CreateMap<Client, ClientResponse>();
            CreateMap<ClientResponse, Client>();
            CreateMap<ClientRequest, Client>();
            CreateMap<Client, ClientRequest>();

        }
        //Цей метод визначає відображення для Basket, BasketResponse, BasketRequestcsта BasketWithClientInfoResponseоб’єктів.
        //Крім того, він використовує .ForMember()для явного зіставлення певних властивостей Basketсутності
        //з властивостями в BasketWithClientInfoResponse(наприклад, ClientId, ClientName, NumberPhone), щоб надати більш детальну інформацію.
        private void CreateMapperBasket()
        {
            CreateMap<Basket, BasketWithClientInfoResponse>()
            .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.Client.ClientID))
            .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.ClientName))
            .ForMember(dest => dest.NumberPhone, opt => opt.MapFrom(src => src.Client.NumberPhone));
            CreateMap<BasketWithClientInfoResponse, Basket>();
            CreateMap<Basket, BasketResponse>();
            CreateMap<BasketResponse, Basket>();
            CreateMap<BasketRequestcs, Basket>();
            CreateMap<Basket, BasketRequestcs>();
        }
    }
}
//AutoMapper - це бібліотека для .NET, яка автоматично мапує об'єкти одного класу на об'єкти іншого класу.
//Вона полегшує процес копіювання даних з одного об'єкта в інший без необхідності вручну виконувати цю роботу.
