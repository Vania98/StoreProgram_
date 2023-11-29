namespace StoreProgram_.DTO.Responses
{
    public class Product
    {
        public int id { get; set; }
        public string productName { get; set; }
        public decimal price { get; set; }
        public string description { get; set; }
        public int sellerId { get; set; }
    }
}
//призначений для використання для представлення даних про продукти, передачі цієї інформації між різними частинами програми,
//такими як контролери, сервіси та інші компоненти. Він може використовуватись для створення відповідей на запити про дані продуктів.