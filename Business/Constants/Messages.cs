using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public class Messages
    {
        //Customer
        public static string CustomerCreated = "Customer is created!";
        public static string CustomerNotCreated = "Customer is not created!";
        public static string CustomerUpdated = "Customer is updated!";
        public static string CustomerNotUpdated = "Customer is not updated!";
        public static string CustomerDeleted = "Customer is deleted!";
        public static string CustomerNotDeleted = "Customer is not deleted!";
        public static string CustomersListed = "Customers is listed!";
        public static string CustomersNotListed = "Customer is not listed!";
        public static string CustomerAlreadyExist = "This customer already exist!";
        public static string CustomerValidated = "Customer validated!";
        public static string CustomerNotValidated = "Customer not validated!";
        //Address
        public static string AddressCreated = "Address is created!";
        public static string AddressNotCreated = "Address is not created!";
        public static string AddressUpdated = "Address is updated!";
        public static string AddressNotUpdated = "Address is not updated!";
        public static string AddressDeleted = "Address is deleted!";
        public static string AddressNotDeleted = "Addreses is not deleted!";
        public static string AddressesListed = "Addresses is listed!";
        public static string AddressesNotListed = "Address is not listed!";
        public static string AddressAlreadyExist = "This address already exist!";
        //BusinessRules
        public static string BusinessRulesNotComply = "This action does not comply with BusinessRules.";
    }
}
