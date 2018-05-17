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
                //todo ta bort magic numbers as cases.
                Console.WriteLine(
                    "Choose task:\n\n1. Add new customer\n2. Add new trip \n3. Registrer trip to existing customer \n4. Show Customer by Customer-ID \n5. Go to main menu \n6. Quit");
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
                        RegistrerTrip();
                        break;
                    case 4:
                        Console.Clear();
                        DisplayCustomersAndTravels();
                        DisplayCustomer(GetCustomerIdFromUser());
                        break;
                    case 5:
                        ChooseTask();
                        break;
                    case 6:
                        Console.WriteLine("Bye");
                        Environment.Exit(0);
                        break;
                }
            }
        }


        private void RegistrerTrip()
        {
            var registrerer = new Registrerer(GetCustomerIdFromUser(), GetRegistrationTravelIDFromUser());
            if (registrerer.AskIfCustomerWantsToPay())
            {
                Console.WriteLine($"Your have to pay a total of {registrerer.CalculateCost()}");
            }
            else
            {
                if (registrerer.CheckIfCustomerHasTooManyDebts())
                {
                    Console.WriteLine(
                        $"You already have too many debts and have to pay a total of {registrerer.CalculateCost()}");
                    registrerer.CustomerPayed();
                }
                else
                {
                    registrerer.CustomerAddedNewDebt();
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
                context.Customers.ToList().ForEach(x => Console.WriteLine($"{x.CustomerId,-20}{x.Name,-20}"));
                Console.WriteLine();


                Console.WriteLine($"{"Travel ID:",-20}{"Travel Destination:",-30}{"Date:",-20}{"Price:",-20}");
                context.Travels.ToList().ForEach(x =>
                    Console.WriteLine(
                        $"{x.TravelId,-20}{x.Destination,-30}{$"{x.Date.Year}-{x.Date.Month}",-20}{x.Price,-20}"));
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
            }
        }

        private int GetRegistrationTravelIDFromUser()
        {
            Console.WriteLine("Enter travelId");
            return Int32.Parse(Console.ReadLine());
        }

    }
}