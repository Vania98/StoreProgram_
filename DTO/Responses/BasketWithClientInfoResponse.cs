namespace StoreProgram_.DTO.Responses
{
    public class BasketWithClientInfoResponse
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string NumberPhone { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
//Загалом клас BasketWithClientInfoResponse використовується для передачі інформації про відповідь, яка містить дані про кошик з інформацією про клієнта,
//які часто використовуються для обміну даними між різними рівнями або компонентами програми.
