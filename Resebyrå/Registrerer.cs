using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace TravelAgency
{

    public class Registrerer
    {
        double interestRate = 1.05;
        double interest = 0;
        Customer customer;
        Travel travel;

        private bool CheckIfCustomerHasTooManyDebts(Customer customer)
        {

            if (customer.NumberOfDebts >= 3)
            {

                return true;
            }

            return false;
        }

        private bool CheckIfCustomerHasDebt(Customer customer)
        {
            if (customer.NumberOfDebts > 0)
            {

                return true;
            }

            return false;
        }

        private bool AskIfCustomerWantsToPay()
        {
            bool payed;
            Console.WriteLine("Pay now? (yes/no)");
            if (Console.ReadLine() == "yes")
                payed = true;
            else
                payed = false;
            return payed;
        }

        static Customer GetCustomerFromDatabase(int customerId)
        {
            using (var context = new TravelAgencyContext())
            {
                return context.Customers.First(x => x.CustomerId == customerId);
            }
        }



        private Travel GetTravelFromDatabase(int travelId)
        {
            using (var context = new TravelAgencyContext())
            {
                return context.Travels.First(x => x.TravelId == travelId);
            }
        }

       

       


    }
}
