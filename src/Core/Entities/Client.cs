using ProConstructionsManagment.Infrastructure.Data.Entities.Base;

namespace ProConstructionsManagment.Infrastructure.Data.Entities
{
    public class Client : BaseEntity
    {
        public string CompanyName { get; set; }
        public string NIP { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
        public string ContactName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string ContactEmail { get; set; }
    }
}