using System;
using System.Linq;
using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace TravelAgency
{
    class ProgramApp
    {
        static void ChooseTask()
        {
            
           


     
                Console.Clear();
            DisplayCustomersAndTravels();
            Console.WriteLine("Choose task:\n\n1. Add new customer\n2. Add new trip \n3. Registrer trip to existing customer \n4. Show Customer by Customer-ID \n5. Show trips by TripID\n");
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
                    RegistrerCustomerForTravel(GetRegistrationCustomerFromUser(), GetRegistrationTravelFromUser());
                    break;
                case 4:
                    DisplayCustomer(GetCustomerIdFromUser());
                    break;
                case 5:
                    
                    break;

            }
        }

        static Customer GetCustomerFromUser()
        {
            Console.WriteLine("Type Name of customer");
            string name = Console.ReadLine();

            var customer = new Customer();
            customer.Name = name;
            return customer;
        }

        static int GetCustomerIdFromUser()
        {
            Console.WriteLine("Type Id of customer");
            int id = Int32.Parse(Console.ReadLine());

            return id;
        }

        static void AddNewCustomer(Customer customer)
        {

            using (var context = new TravelAgencyContext())
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

            using (var context = new TravelAgencyContext())
            {
                context.Travels.Add(travel);
                context.SaveChanges();
            }
        }

        static void DisplayCustomersAndTravels()
        {
            

            using (var context = new TravelAgencyContext())
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

            using (var context = new TravelAgencyContext())
            {
                
                var customer = context.Customers.First(x => x.CustomerId == customerId);



                Console.WriteLine($"Name: {customer.Name} \n" +
                                  $"Debt: {customer.TotalDebt}");
                Console.WriteLine(" Able to registrer trip?");
                if(customer.NumberOfDebts<3)
                    Console.WriteLine($"Yes, only {customer.NumberOfDebts} debts");
                Console.WriteLine($"No, 3 debts registrered already");

               // customer.Registations.Select(x => x.Travel) ForEach(x => Console.WriteLine($"{x.CustomerId,-20}{x.Name,-20}"));



            }

        }

        static int GetRegistrationTravelFromUser()
        {

            Console.WriteLine("Enter travelId");
            return Int32.Parse(Console.ReadLine());
        }

        static int GetRegistrationCustomerFromUser()
        {

            Console.WriteLine("Enter customerId");
            return Int32.Parse(Console.ReadLine());
        }




        static void RegistrerCustomerForTravel(int customerId, int travelId)
        {
            Customer customer;
            Travel travel;

            using (var context = new TravelAgencyContext())
            {
                customer = context.Customers.First(x => x.CustomerId == customerId);
                travel = context.Travels.First(x => x.TravelId == travelId);

            }

            Console.WriteLine("Payed?");
            bool payed = bool.Parse(Console.ReadLine());

            var registration = new Registration
            {
                Customer = customer,
                Travel = travel,
                IsPayed = payed

            };

            using (var context = new TravelAgencyContext())
            {
                if (!registration.IsPayed)
                {
                    context.Customers.First(x => x.CustomerId == customerId).TotalDebt = +travel.Price;
                    context.Customers.First(x => x.CustomerId == customerId).NumberOfDebts=+1;
                    context.SaveChanges();
                }

            }

            
        }

        static void CalculateDebtOfCustomer(int customerId)
        {
            Customer customer;
            using (var context = new TravelAgencyContext())
            {
                customer = context.Customers.First(x => x.CustomerId == customerId);
                context.
            }
        }



        static void Main(string[] args)
        {
            ChooseTask();
        }
    }
}
