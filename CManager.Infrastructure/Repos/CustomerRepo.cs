using CManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace CManager.Infrastructure.Repos;

public class CustomerRepo : ICustomerRepo
{
    private readonly string _filePath;
    private readonly string _directoryPath;
    private readonly JsonSerializerOptions _jsonOptions;



    //Behöver gå igenom blocket undertill.
    public CustomerRepo(string directoryPath = "Data", string fileName = "list.json")
    {
        _directoryPath = directoryPath;
        _filePath = Path.Combine(_directoryPath, fileName);
        _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true, //denna gör så man kan skriva med både små och stora bokstäver som ToUpper/ToLower
        };
    }



    public List<CustomerModel> GetAllCustomers()
    {
        if (!File.Exists(_filePath))
        {
            return [];
        }

        try
        {
            var json = File.ReadAllText(_filePath);
            var customers = JsonSerializer.Deserialize<List<CustomerModel>>(json, _jsonOptions);
            return customers ?? []; //Denna delen säger: OM customers är null, returnera en tom lista.
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading file with customers: {ex.Message}");
            throw; //Den KASTAR vidare "felet" till GetAllCustomers
        }


    }

    public bool SaveCustomers(List<CustomerModel> customers)
    {
        if (customers == null)
            return false;

        try
        {
            var json = JsonSerializer.Serialize(customers, _jsonOptions);

            if (!Directory.Exists(_directoryPath))
                Directory.CreateDirectory(_directoryPath);

            File.WriteAllText(_filePath, json);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving file with customers: {ex.Message}");
            return false;
        }
    }

















    //                              UPDATECUSTOMER
    //                              NEDAN KOD ÄR PÅGÅENDE KOD, utgår från indexeringen som Emil skapade i CustomerService.cs > DeleteCustomer


    public bool UpdateCustomer(CustomerModel updatedCustomer)
    {
        try
        {
            
            var customers = GetAllCustomers(); //Hämtar ALLA kUnderna 
            
            var customer = customers.FirstOrDefault(c => c.Id == updatedCustomer.Id); // Hitta kunden med ID, Använde samma indexeringsmetod som i CustomerService.Cs > DeleteCustomer()

            if (customer == null)
                return false;

                // Uppdaterar och skriver över tidigare sm användaren skrivit in(customer) och skriver över det på (updatedCustomer) MEN uppdaterar ej Json filen än
                customer.FirstName = updatedCustomer.FirstName;
                customer.LastName = updatedCustomer.LastName;
                customer.Email = updatedCustomer.Email;
                customer.PhoneNumber = updatedCustomer.PhoneNumber;
                customer.Address.StreetAddress = updatedCustomer.Address.StreetAddress;
                customer.Address.PostalCode = updatedCustomer.Address.PostalCode;
                customer.Address.City = updatedCustomer.Address.City;

            
            return SaveCustomers(customers); // HÄR uppdateras det  till Json
        }

            catch (Exception ex) //OM det blir fel så hamnar det här och kastar exception
        {
            Console.WriteLine($"Not possible to make change. Please try again later... {ex.Message}");
            return false;
        }
    }

}
