using CManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CManager.Infrastructure.Repos;

        public interface ICustomerRepo : ICustomerReader, ICustomerUpdate, ICustomerDelete
        {
            List<CustomerModel> GetAllCustomers();
            bool SaveCustomers(List<CustomerModel> customers);
        }


//ANLEDINGEN till att jag i detta interfacet inte bara skriver: 

// * bool DeleteCustomer(Guid id);
// * CustomerModel GetCustomerById(Guid id);
// * bool UpdateCustomer(CustomerModel updatedCustomer);

// Är för att uppfylla kriteriet för Interface Segregation Policy för Repository interfacet.
// I min interface för Sercive följer jag INTE ISP. 
// Syftet med namnen (ICustomerDelete, ICustomerReader, ICustomerUpdate) var endast i syftet att förenkla för mig själv.


                    //              DELETECUSTOMER
                    //              RENSAT
                    public interface ICustomerDelete
                                {
                                    bool DeleteCustomer(Guid id);
                                }




                    //              HÄMTA SPECIFIK KUND
                    public interface ICustomerReader
                                {
                                    CustomerModel GetCustomerById(Guid id);
                                }




//              UPDATECUSTOMER ***
//              CHATGPT HJÄLPTE MIG NEDAN
                    public interface ICustomerUpdate
                                {
                                bool UpdateCustomer(CustomerModel updatedCustomer);

                                }












