using Microsoft.EntityFrameworkCore;

namespace TaskRequestDem.Model
{
    /// <summary>
    /// Представляет сущность клиента в системе.
    /// Содержит основные данные клиента, такие как идентификатор, имя, фамилия и номер телефона.
    /// </summary>
    public class Client
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
