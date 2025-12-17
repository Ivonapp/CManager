using CManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CManager.Application.Services;

    public interface ICustomerService
    {
    bool CreateCustomer(string firstName, string lastName, string email, string phoneNumber, string streetAddress, string postalcode, string city);

    IEnumerable<CustomerModel> GetAllCustomers(out bool hasError);//Kastar ut en parameter med "out bool hasError"

    bool DeleteCustomer(Guid id);

}



