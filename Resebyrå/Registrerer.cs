using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Migrations;

namespace TravelAgency
{

    public class Registrerer
    {
        const double interestRate = 1.05;
        double interest = 0;
        Customer customer;
        Travel travel;

        public Registrerer(int customerId, int travelId)
        {
            using (var context = new TravelAgencyContext())
            {
                customer = context.Customers.First(x => x.CustomerId == customerId);
                travel = context.Travels.First(x => x.TravelId == travelId);
            }
        }



        public bool CheckIfCustomerHasTooManyDebts()
        {
            if (customer.NumberOfDebts >= 3)
            {
                return true;
            }
            return false;
        }

        public bool CheckIfCustomerHasDebt()
        {
            if (customer.NumberOfDebts > 0)
            {
                return true;
            }
            return false;
        }

        public bool AskIfCustomerWantsToPay()
        {
            bool payed;
            Console.WriteLine("Pay now? (yes/no)");
            if (Console.ReadLine() == "yes")
                payed = true;
            else
                payed = false;
            return payed;
        }

        public Customer GetCustomerFromDatabase(int customerId)
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

        private double CalculateInterest()
        {
            using (var context = new TravelAgencyContext())
            {
                customer.LastDebtDate = context.Travels.OrderBy(x => x.Date).ToList().FirstOrDefault().Date;
                customer.LastDebtAmount = context.Travels.OrderBy(x => x.Date).ToList().FirstOrDefault().Price;
                context.SaveChanges();
            }
            double timeSpan = (travel.Date - customer.LastDebtDate).Days;
            interest = customer.LastDebtAmount * Math.Pow(interestRate, timeSpan)-customer.LastDebtAmount;

            return interest;
        }

        public double CalculateCost()
        {
            double returnCost = 0;
            if (CheckIfCustomerHasDebt())
            {
                returnCost += CalculateInterest();
            }

            returnCost += travel.Price;

            return returnCost;
        }

        public void CustomerPayed()
        {
            
            using (var context = new TravelAgencyContext())
            {
                context.Customers.First(x => x.CustomerId == customer.CustomerId).TotalDebt = 0;
                context.Customers.First(x => x.CustomerId == customer.CustomerId).NumberOfDebts = 0;
                context.SaveChanges();
            }
        }


        public void CustomerAddedNewDebt()
        {
            using (var context = new TravelAgencyContext())
            {

                {
                    context.Customers.First(x => x.CustomerId == customer.CustomerId).TotalDebt += +travel.Price;
                    context.Customers.First(x => x.CustomerId == customer.CustomerId).NumberOfDebts += 1;
                    
                    
                    customer.LastDebtDate = context.Travels.OrderBy(x => x.Date).ToList().FirstOrDefault().Date;
                    customer.LastDebtAmount = context.Travels.OrderBy(x => x.Date).ToList().FirstOrDefault().Price;

                    context.SaveChanges();
                }
            }

        }
    }
}
