using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelAgency;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod] public void CalculateDebtOfCustomer_UnpaidMiamiStockholm_DebtIs1()
        {
            var travels = new List<Travel>();
            var customer = new Customer();
            using (var context = new TravelAgencyContext())
            {
                travels = context.Travels.ToList();

                customer = new Customer
                {
                    Name = "Test Persson",
                    CustomerId = 100,
                    LastDebtAmount = 0,
                    LastDebtDate = new DateTime(2016 - 01 - 01),
                    NumberOfDebts = 0,
                    Registrations = null

                };
                context.Customers.Add(customer);
                context.SaveChanges();
            }

            //var registration = new Registration
            //{

            //    Customer = customer,
            //    Travel = travels.FirstOrDefault(x => x.Destination == "Miami"),
            //    IsPayed = false,
            //    CustomerId = 100,
            //    TravelId = 9
            //};






            //var registration2 = new Registration
            //{
            //    CustomerId = 100,
            //    Customer = customer,
            //    IsPayed = false,
            //    TravelId = 9,
                
            //};
            //customer.Registrations.Add(registration);
            //customer.Registrations.Add(registration2);
            
            
            double expected = 20000;
            var app= new App();
            app.RegistrerCustomerForTravel(customer.CustomerId,9);

            var actual = customer.TotalDebt;
            Assert.AreEqual(expected,actual);

            }
        }
    }

