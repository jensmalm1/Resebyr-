using System;
using Data;
using Domain;

namespace TravelAgent
{
    class ProgramApp
    {
        private static void ChooseTask()
        {
            Console.WriteLine("Choose task:\n1. for adding customer\n2. for adding trip");
        }

        private static Customer GetCustomerFromUser()
        {
            

            Console.WriteLine("Type Name of customer");
            string name = Console.ReadLine();

            var customer = new Customer();
            customer.Name = name;
            return customer;

        }

        private static void AddNewCustomer(Customer customer)
        {

            using (var context = new TravelAgentContext())
            {
                context.Customers.Add(customer);
            }
        }


        static void Main(string[] args)
        {
            
            AddNewCustomer(GetCustomerFromUser());
            Console.WriteLine("Ny anmälning: ");


        }
    }
}
