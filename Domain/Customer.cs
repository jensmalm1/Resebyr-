using System.Collections.Generic;

namespace Domain
{
   
        public class Customer
        {
            public List<Travel> Travels { get; set; }
            public int CustomerId { get; set; }
            public string Name { get; set; }
            public int NumberOfDebts { get; set; }
            public double TotalDebt { get; set; }


        }
    
}
