using CManager.Domain.Models;
using CManager.Infrastructure.Repos;
using CManager.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CManager.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepo _customerRepo;
    public CustomerService(ICustomerRepo customerRepo)
    {
        _customerRepo = customerRepo;
    }

    //                 RENSAT
    //                 Skapar kunden
    //                 Använder GuidHelped för att ge kund unikt ID        (CustomerRepo)
    //                 och CustomerModel lägger till kunden UTAN att spara (CustomerRepo)
    public bool CreateCustomer(string firstName, string lastName, string email, string phoneNumber, string streetAddress, string postalcode, string city)
    {
        CustomerModel customerModel = new()
        {
            Id = GuidHelper.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber,
            Address = new AddressModel
            {
                StreetAddress = streetAddress,
                PostalCode = postalcode,
                City = city
            }
        };



        //                RENSAT // *KANSKE ÄNDRA DENNA KODEN SÅ SAVECUSTOMERS OCH GETALLCUSTOMERS ÄR SEPARAT UPPDELAT SÅ SOM I CUSTOMERREPO
        //                GETALLCUSTOMERS
        //                SAVECUSTOMERS

        try
        {
            var customers = _customerRepo.GetAllCustomers();
            customers.Add(customerModel);
            var result = _customerRepo.SaveCustomers(customers);
            return result;
        }
        catch (Exception)
        {
            return false;
        }
    }




        //                RENSAT
        //                GETALLCUSTOMERS
        public IEnumerable<CustomerModel> GetAllCustomers(out bool hasError)
        {
            hasError = false;

            try
            {
                var customers = _customerRepo.GetAllCustomers();
                return customers;
            }
            catch (Exception)
            {

                //Här Kommer throw hamna från customerrepo - getallcustomers
                hasError = true;
                return new List<CustomerModel>();
            }
        }



    //                    RENSAT
    //                    DELETECUSTOMER 
    //                    Resten av koden flyttad till CustomerRepo

    public bool DeleteCustomer(Guid id)
    {
        return _customerRepo.DeleteCustomer(id);
    }






    //                      UPDATECUSTOMER

    public bool UpdateCustomer(CustomerModel updatedCustomer)
    {
        try
        {
            return _customerRepo.UpdateCustomer(updatedCustomer);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Not possible to update the customer information. {ex.Message}");
            return false;
        }
    }



    //                       HÄMTA SPECIFIK KUND
    //                       CHATGPT HJÄLPTE MIG NEDAN ***

            public CustomerModel GetCustomerById(Guid id)
                {
                    return _customerRepo.GetCustomerById(id);
                }
        }



