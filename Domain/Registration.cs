using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Registration
    {
        public int CustomerId { get; set; }
        public int TravelId { get; set; }
        public Travel Travel { get; set; }
        public Customer Customer { get; set; }
        public bool IsPayed { get; set; }
    }
}