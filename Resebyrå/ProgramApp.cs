using System;
using System.Linq;
using Data;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace TravelAgent
{
    class ProgramApp
    {
        static void ChooseTask()
        {
            Console.WriteLine("Choose task:\n1. for adding customer\n2. for adding trip");
        }

        static Customer GetCustomerFromUser()
        {
            Console.WriteLine("Type Name of customer");
            string name = Console.ReadLine();

            var customer = new Customer();
            customer.Name = name;
            return customer;
        }

        static void AddNewCustomer(Customer customer)
        {

            using (var context = new TravelAgentContext())
            {
                context.Customers.Add(customer);
                context.SaveChanges();
            }
        }

        static Travel GetTravelFromUser()
        {


            Console.WriteLine("Enter destination");
            string destination = Console.ReadLine();
            Console.WriteLine("Enter date (yyyy-mm-dd hh:mm");
            DateTime date =DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter price of trip");
            double price = double.Parse(Console.ReadLine());

            var travel= new Travel();
            travel.Destination = destination;
            travel.Date = date;
            travel.Price = price;

            return travel;

        }

        static void AddNewTravel(Travel travel)
        {

            using (var context = new TravelAgentContext())
            {
                context.Travels.Add(travel);
                context.SaveChanges();
            }
        }

        static void DisplayCustomersAndTravels()
        {
            

            using (var context = new TravelAgentContext())
            {
                Console.WriteLine($"{"Customer ID:",-20}{"Customer Name:",-20}");
                context.
                    Customers.
                    ToList().
                    ForEach(x => Console.WriteLine($"{x.CustomerId,-20}{x.Name,-20}"));
                Console.WriteLine();


                Console.WriteLine($"{"Travel ID:",-20}{"Travel Destination:",-20}");
                context.
                    Travels.
                    ToList().
                    ForEach(x => Console.WriteLine($"{x.TravelId,-20}{x.Destination,-20}"));
                Console.WriteLine();
            }


        }


        static void DisplayCustomer(int customerId)
        {

            using (var context = new TravelAgentContext())
            {

                var customer = context.Customers.First(x => x.CustomerId == customerId);
                    customer.NumberOfDebts;
                    
                customer.Participants.Select(x=>x.Travel) ForEach(x => Console.WriteLine($"{x.CustomerId,-20}{x.Name,-20}"));
                


            }

        }

        static void RegistrerTripForCustomer(int customerId)
        {
            using (var context = new TravelAgentContext())
            {
                var customer = context.Customers.First(x => x.CustomerId == customerId);

            }
        }


         static void RegistrerCustomerForTravel(Customer customer, Travel travel)
        {




        }


        static void Main(string[] args)
        {
            DisplayCustomersAndTravels();

            DisplayCustomer();

            AddNewCustomer(GetCustomerFromUser());

            AddNewTravel(GetTravelFromUser());

            

            Console.WriteLine("Ny anmälning: ");


        }
    }
}
