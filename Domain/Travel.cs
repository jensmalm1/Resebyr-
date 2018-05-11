using System;
using System.Collections.Generic;


namespace Domain
{
    public class Travel
    {
        public List<Registration> Registrations { get; set; }
        public int TravelId { get; set; }
        public string Destination { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
    }

}
