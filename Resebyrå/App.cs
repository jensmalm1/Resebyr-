using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;

namespace TravelAgency
{
    public class App
    {
        public void ChooseTask()
        {
            DisplayCustomersAndTravels();
            bool keepMenu = true;
            while (keepMenu)
            {

                

                Console.WriteLine("Choose task:\n\n1. Add new customer\n2. Add new trip \n3. Registrer trip to existing customer \n4. Show Customer by Customer-ID \n9. Quit");
                int i = Int32.Parse(Console.ReadLine());
                switch (i)
                {
                    case 1:
                        AddNewCustomer(GetCustomerFromUser());
                        break;
                    case 2:
                        AddNewTravel(GetTravelFromUser());
                        break;
                    case 3:
                        RegistrerCustomerForTravel(GetRegistrationCustomerIDFromUser(), GetRegistrationTravelIDFromUser());
                        break;
                    case 4:
                        Console.Clear();
                        DisplayCustomersAndTravels();
                        DisplayCustomer(GetCustomerIdFromUser());
                        break;
                    case 5:
                        break;
                    case 9:
                        Console.WriteLine("Bye");
                        keepMenu = false;
                        break;

                        
                }
               
            }


        }

        private Customer GetCustomerFromUser()
        {
            Console.WriteLine("Type Name of customer");
            string name = Console.ReadLine();

            var customer = new Customer();
            customer.Name = name;
            return customer;
        }

        private int GetCustomerIdFromUser()
        {
            Console.WriteLine("Type Id of customer");
            int id = Int32.Parse(Console.ReadLine());

            return id;
        }

        private void AddNewCustomer(Customer customer)
        {

            using (var context = new TravelAgencyContext())
            {
                context.Customers.Add(customer);
                context.SaveChanges();
            }
        }

        private Travel GetTravelFromUser()
        {


            Console.WriteLine("Enter destination");
            string destination = Console.ReadLine();
            Console.WriteLine("Enter date (yyyy-mm-dd hh:mm");
            DateTime date = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter price of trip");
            double price = double.Parse(Console.ReadLine());

            var travel = new Travel();
            travel.Destination = destination;
            travel.Date = date;
            travel.Price = price;

            return travel;

        }

        private void AddNewTravel(Travel travel)
        {

            using (var context = new TravelAgencyContext())
            {
                context.Travels.Add(travel);
                context.SaveChanges();
            }
        }

        private void DisplayCustomersAndTravels()
        {
            using (var context = new TravelAgencyContext())
            {
                Console.WriteLine($"{"Customer ID:",-20}{"Customer Name:",-20}");
                context.
                    Customers.
                    ToList().
                    ForEach(x => Console.WriteLine($"{x.CustomerId,-20}{x.Name,-20}"));
                Console.WriteLine();


                Console.WriteLine($"{"Travel ID:",-20}{"Travel Destination:",-30}{"Date:",-20}{"Price:",-20}");
                context.
                    Travels.
                    ToList().
                    ForEach(x => Console.WriteLine($"{x.TravelId,-20}{x.Destination,-30}{$"{x.Date.Year}-{x.Date.Month}",-20}{x.Price,-20}"));
                Console.WriteLine();
            }
        }

        private void DisplayCustomer(int customerId)
        {

            using (var context = new TravelAgencyContext())
            {

                var customer = context.Customers.First(x => x.CustomerId == customerId);

                Console.WriteLine($"Name: {customer.Name} \n" +
                                  $"Debt: {customer.TotalDebt}");

                if (customer.NumberOfDebts < 3)
                    Console.WriteLine($"{customer.Name} has only {customer.NumberOfDebts} debts");
                else
                    Console.WriteLine($"{customer.Name} has 3 debts registrered and cannot registrer for another trip");

                // customer.Registations.Select(x => x.Travel) ForEach(x => Console.WriteLine($"{x.CustomerId,-20}{x.Name,-20}"));


            }

        }

        private int GetRegistrationTravelIDFromUser()
        {

            Console.WriteLine("Enter travelId");
            return Int32.Parse(Console.ReadLine());
        }

        private int GetRegistrationCustomerIDFromUser()
        {

            Console.WriteLine("Enter customerId");
            return Int32.Parse(Console.ReadLine());
        }


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



       public void RegistrerCustomerForTravel(int customerId, int travelId)
        {
            double interestRate = 1.05;
            double interest=0;
            Customer customer;
            Travel travel;

            using (var context = new TravelAgencyContext())
            {
                customer = context.Customers.First(x => x.CustomerId == customerId);
          
                travel = context.Travels.First(x => x.TravelId == travelId);

            }

            

            if (!CheckIfCustomerHasTooManyDebts(customer))
            {
                bool payed;
                if (AskIfCustomerWantsToPay())
                {
                    
                    payed = true;


                    var registration = new Registration
                    {
                        Customer = customer,
                        Travel = travel,
                        IsPayed = payed
                    };

                    if (CheckIfCustomerHasDebt(customer))
                    {
                        using (var context = new TravelAgencyContext())
                        {
                            customer.LastDebtDate = context.Travels.OrderBy(x => x.Date).ToList().FirstOrDefault().Date;
                            customer.LastDebtAmount = context.Travels.OrderBy(x => x.Date).ToList().FirstOrDefault().Price;
                            context.SaveChanges();
                        }


                        double timeSpan = (registration.Travel.Date-customer.LastDebtDate).Days;
                        interest = customer.LastDebtAmount * Math.Pow(interestRate, timeSpan);
                        Console.WriteLine(
                            $"{customer.Name} has to pay debts of {customer.TotalDebt} and {interest} as interest and travel price of {travel.Price} to a total of {customer.TotalDebt + travel.Price + interest} ");
                    }
                }
                else
                    payed = false;

                

                using (var context = new TravelAgencyContext())
                {
                    if (!payed)
                    {
                        context.Customers.First(x => x.CustomerId == customerId).TotalDebt += +travel.Price;
                        context.Customers.First(x => x.CustomerId == customerId).NumberOfDebts += 1;
                        context.SaveChanges();
                    }
                    else
                    {
                        context.Customers.First(x => x.CustomerId == customerId).TotalDebt = 0;
                        context.Customers.First(x => x.CustomerId == customerId).NumberOfDebts = 0;
                        context.SaveChanges();
                    }
                }
            }
            else
            {
                Console.WriteLine($"{customer.Name} has too many debts...");
                {

                    if (AskIfCustomerWantsToPay())
                    {
                        Console.WriteLine(
                            $"{customer.Name} has to pay debts of {customer.TotalDebt} and {interest} as interest and travel price of {travel.Price} to a total of {customer.TotalDebt + travel.Price+interest} ");
                        using (var context = new TravelAgencyContext())
                        {
                            context.Customers.First(x => x.CustomerId == customerId).TotalDebt = 0;
                            context.Customers.First(x => x.CustomerId == customerId).NumberOfDebts = 0;
                            context.SaveChanges();
                        }
                    }
                    else
                        Console.WriteLine("Unable to registrer trip until debts are payed...");
                }
            }
        }
    }
}
