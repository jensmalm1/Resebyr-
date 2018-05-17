using System;
using System.Collections.Generic;

namespace Domain
{
    public class Customer
    {
        public List<Registration> Registrations { get; set; }
        public DateTime LastDebtDate { get; set; }
        public double LastDebtAmount { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public int NumberOfDebts { get; set; }
        public double TotalDebt { get; set; }
    }
}