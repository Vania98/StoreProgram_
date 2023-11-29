namespace StoreProgram_.DTO.Responses
{
    public class ClientResponse
    {
        public int ClientID { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string NumberPhone { get; set; } = string.Empty;
    }
}
//Цей клас ClientResponse може використовуватись для передачі даних про клієнта між різними частинами програми, такими як контролери,
//сервіси, або інші компоненти, і дозволяє структуровано обмінюватись інформацією про клієнта.





