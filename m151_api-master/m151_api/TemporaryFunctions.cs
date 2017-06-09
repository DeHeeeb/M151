using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using m151_api.Classes;
using m151_api.Entities;

namespace m151_api
{
    public class TemporaryFunctions
    {
        public static bool credentialsAreValid()
        {
            return true;
        }

        public static bool tokenIsValid()
        {
            return true;
        }

        internal static Employee getEmployee()
        {
            return new Employee()
            {
                Number = 1,
                Organisation = new Organisation() {Id = 1, Name = "Spiel Gut"},
                Prename = "Hans",
                Surname = "Kunz"
            };
        }

        public static bool customerCouldBeSaved()
        {
            return true;
        }

        public static bool userIsAuthorized()
        {
            return true;
        }

        public static bool customerCouldBeUpdated()
        {
            return true;
        }

        public static Customer getCustomerByToken()
        {
            return new Customer()
            {
                Email = "joan.kuenzler@gmail.com",
                City = "St.Gallen",
                IsPremiumMember = true,
                Number = 1,
                PhoneNumber = "0712901214",
                Prename = "Joan",
                Surname = "Kuenzler",
                Street = "Neusteig",
                StreetNumber = "14",
                ZipCode = "9300"
            };
        }

       public static bool lendingCoudldBeSaved()
        {
            return true;
        }

        public static bool employeeCouldBeSaved()
        {
            return true;
        }

        public static bool employeeCouldBeUpdated()
        {
            return true;
        }
    }
}