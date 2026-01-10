using CManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

        namespace CManager.Application.Services;

            public interface ICustomerService
            {
            bool CreateCustomer(string firstName, string lastName, string email, string phoneNumber, string streetAddress, string postalCode, string city);

            IEnumerable<CustomerModel> GetAllCustomers(out bool hasError);//Kastar ut en parameter med "out bool hasError"




            //                              DELETECUSTOMER

                            bool DeleteCustomer(Guid id);




            //                              UPDATECUSTOMER

                            bool UpdateCustomer(CustomerModel updatedCustomer);




    //                              HÄMTA SPECIFIK KUND
    //                              CHATGPT HJÄLPTE MIG NEDAN.
    //                              Metoden nedan ska hämta en kund baserat på ett ID
    //                              och den är kopplad till lambda funktionen.
    //                              (Detta förstår jag nu, men det var i början när man
    //                              inte riktigt förstod hur det funkade med alla olika filer,
    //                              hur allt kopplas ihop etc.)

                            CustomerModel GetCustomerById(Guid id);


}



