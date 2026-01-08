using CManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

        namespace CManager.Application.Services;

            public interface ICustomerService
            {
            bool CreateCustomer(string firstName, string lastName, string email, string phoneNumber, string streetAddress, string postalCode, string city);

            IEnumerable<CustomerModel> GetAllCustomers(out bool hasError);//Kastar ut en parameter med "out bool hasError"



    //Jag skriver bara ut: 
    // bool DeleteCustomer(Guid id); 
    // bool UpdateCustomer(CustomerModel updatedCustomer);
    // CustomerModel GetCustomerById(Guid id);
    // Eftersom att jag här inte följer Interface segregation principle så som jag följer i Repository




            //                              DELETECUSTOMER
            //                              RENSAT

                            bool DeleteCustomer(Guid id);




            //                              UPDATECUSTOMER

                            bool UpdateCustomer(CustomerModel updatedCustomer);




            //                              HÄMTA SPECIFIK KUND
            //                              CHATGPT HJÄLPTE MIG NEDAN ***

                            CustomerModel GetCustomerById(Guid id);


}



