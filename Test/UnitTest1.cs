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
        [TestMethod]
        public void CalculateCostForCustomer_NewCustomerRegistrerForMiami_20000()
        {
            var customer = new Customer
            {
                Name = "Test Persson",
                CustomerId = 100,
                LastDebtAmount = 0,
                LastDebtDate = new DateTime(2016 - 01 - 01),
                NumberOfDebts = 0,
                Registrations = null
            };


            var registrererTest = new Registrerer();
            var travels = registrererTest.GetTravelList();
            var travel = travels.FirstOrDefault(x => x.Destination == "Miami");
            var registrerer = new Registrerer(customer, travel);

            var actual = registrerer.CalculateCost();
            var expected = 20000;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateCostForCustomer_NewCustomerRegistrerForStockholm_5000()
        {
            var customer = new Customer
            {
                Name = "Test Persson",
                CustomerId = 101,
                LastDebtAmount = 0,
                LastDebtDate = new DateTime(2016 - 01 - 01),
                NumberOfDebts = 0,
                Registrations = null
            };

            var registrererTest = new Registrerer();
            var travels = registrererTest.GetTravelList();
            var travel = travels.FirstOrDefault(x => x.Destination == "Stockholm");
            var registrerer = new Registrerer(customer, travel);

            var actual = registrerer.CalculateCost();
            var expected = 5000;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckIfCustomerHasDebt_LisaLarssonHasNoDebt_False()
        {
            using (var context = new TravelAgencyContext())
            {
                var customer = context.Customers.First(x => x.Name == "Lisa Larsson");
                var registrer = new Registrerer(customer.CustomerId, 1);
                var actual = registrer.CheckIfCustomerHasDebt();
                var expected = false;
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void CheckIfCustomerHasDebt_LeeLikesDebtsHasThreeDebts_True()
        {
            using (var context = new TravelAgencyContext())
            {
                var customer = context.Customers.First(x => x.Name == "Lee Likes Debts");
                var registrer = new Registrerer(customer.CustomerId, 1);
                var actual = registrer.CheckIfCustomerHasDebt();
                var expected = true;
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void CheckIfCustomerHasTooManyDebts_LisaLarssonHasNoDebt_False()
        {
            using (var context = new TravelAgencyContext())
            {
                var customer = context.Customers.First(x => x.Name == "Lisa Larsson");
                var registrer = new Registrerer(customer.CustomerId, 1);
                var actual = registrer.CheckIfCustomerHasTooManyDebts();
                var expected = false;
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void CheckIfCustomerHasTooManyDebts_LeeLikesDebtsHasThreeDebts_True()
        {
            using (var context = new TravelAgencyContext())
            {
                var customer = context.Customers.First(x => x.Name == "Lee Likes Debts");
                var registrer = new Registrerer(customer.CustomerId, 1);
                var actual = registrer.CheckIfCustomerHasTooManyDebts();
                var expected = true;
                Assert.AreEqual(expected, actual);
            }
        }
    }
}