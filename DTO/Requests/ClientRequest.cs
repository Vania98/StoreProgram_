namespace StoreProgram_.DTO.Requests
{
    public class ClientRequest
    {
        public int ClientID { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string NumberPhone { get; set; } = string.Empty;
    }
}
//ClientRequest клас виглядає як DTO, призначений для зберігання та передачі інформації, пов’язаної з клієнтом, як-от ідентифікатор клієнта,
//ім’я та номер телефону