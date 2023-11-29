namespace StoreProgram_.DTO.Responses
{
    public class BasketResponse
    {
        public int BasketID { get; set; }
        public int ClientID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
//клас виглядає як DTO, призначений для зберігання та передачі інформації, пов’язаної з відповіддю кошика, що містить властивості,
//що представляють такі деталі, як ідентифікатор кошика, пов’язаний ідентифікатор клієнта, назва продукту, кількість і ціна. 
