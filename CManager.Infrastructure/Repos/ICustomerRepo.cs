using CManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CManager.Infrastructure.Repos;

    public interface ICustomerRepo
    {
        /*KONTRAKT. Det ska gå att spara en lista av kunder till en fil +
        det ska gå att hämta ut listan också*/

        List<CustomerModel> GetAllCustomers();
    bool SaveCustomers(List<CustomerModel> customers);


                //UpdateCustomer
    bool UpdateCustomer(CustomerModel updatedCustomer);
}
