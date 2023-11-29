namespace StoreProgram_.DTO.Requests
{
    public class BasketRequestcs
    {
        public int BasketID { get; set; }
        public int ClientID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
//цей клас є об’єктом передачі даних (DTO), який використовується для захоплення та передачі інформації, пов’язаної із запитом кошика.
//BasketRequestcsклас виглядає як DTO, який визначає структуру запиту на створення або оновлення кошика,
//зберігаючи важливу інформацію про кошик, пов’язаного клієнта, продукт, кількість і ціну.
//DTO (Data Transfer Object) це об'єкт, який використовується для передачі даних між програмними компонентами