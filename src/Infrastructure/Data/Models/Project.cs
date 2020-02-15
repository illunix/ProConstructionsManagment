using System;
using ProConstructionsManagment.Infrastructure.Enums;

namespace ProConstructionsManagment.Infrastructure.Data.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string NIP { get; set; }
        public string Address { get; set; }
        public string SecondAddress { get; set; }
        public string JobDescription { get; set; }
        public ProjectStatus Status { get; set; } 
    }
}