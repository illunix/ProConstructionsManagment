using System;
using ProConstructionsManagment.Core.Enums;

namespace ProConstructionsManagment.Infrastructure.Data.Entities
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ContactName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string ContactEmail { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string PlaceOfPerformance { get; set; }
        public int PercentageOfImplementation { get; set; }
        public int RequiredNumberOfEmployees { get; set; }
        public int NumberOfEmployees { get; set; }
        public int NumerOfCandidates { get; set; }
        public int NumberOfHours { get; set; }
        public string DateOfPaymentDelivery { get; set; }
        public string DateOfPayment { get; set; }
        public bool PaymentProtocol { get; set; }
        public bool Agreement { get; set; }
        public ProjectPaymentStatus PaymentStatus { get; set; }
        public ProjectStatus Status { get; set; }
    }
}