using CManager.Domain.Models;
using CManager.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace CManager.Infrastructure.Repos;


//RENSAD KOD NEDAN
public class CustomerRepo : ICustomerRepo
{
    private readonly string _filePath;
    private readonly string _directoryPath;
    private readonly JsonFormatter _jsonFormatter;

    //Hämtar från klassen JsonFormatter.cs
    //Sparar ner data och till List.json
    public CustomerRepo(string directoryPath = "Data", string fileName = "list.json")
    {
        _directoryPath = directoryPath;
        _filePath = Path.Combine(_directoryPath, fileName);
        _jsonFormatter = new JsonFormatter();
    }







    //                  RENSAD KOD NEDAN
    //                  GETALLCUSTOMERS
    public List<CustomerModel> GetAllCustomers()
    {
        if (!File.Exists(_filePath))
        {
            return [];
        }

        try
        {
            var json = File.ReadAllText(_filePath);
            var customers = _jsonFormatter.Deserialize(json);
            return customers ?? []; //Denna delen säger: OM customers är null, returnera en tom lista.
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading file with customers: {ex.Message}");
            throw; //Den KASTAR vidare "felet" till GetAllCustomers
        }
    }


    //                  RENSAD KOD NEDAN
    //                  SAVECUSTOMERS
    public bool SaveCustomers(List<CustomerModel> customers)
    {
        if (customers == null)
            return false;

        try
        {
            var json = _jsonFormatter.SerializeCustomers(customers);

            if (!Directory.Exists(_directoryPath))
                { 
                Directory.CreateDirectory(_directoryPath);
                }

            File.WriteAllText(_filePath, json);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving file with customers: {ex.Message}");
            return false;
        }
    }







    //                 RENSAD
    //                 UPDATECUSTOMER
    //                 Utgår från indexeringen som Emil skapade i CustomerService.cs


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

            
            return SaveCustomers(customers); // HÄR uppdateras det till Json
        }

            catch (Exception ex) //OM det blir fel så hamnar det här och kastar exception
        {
            Console.WriteLine($"Not possible to make change. Please try again later... {ex.Message}");
            return false;
        }
    }








    
    //                  HÄMTA SPECIFIK KUND
    //                  CHATGPT HJÄLPTE MIG NEDAN ***

         public CustomerModel GetCustomerById(Guid id)
                {
                    var customers = GetAllCustomers();
                    return customers.FirstOrDefault(c => c.Id == id)!;
                }


   




    //                 DELETECUSTOMER 
    public bool DeleteCustomer(Guid id)
    {
        try
        {
            var customers = GetAllCustomers();
            var customer = customers.FirstOrDefault(c => c.Id == id);

            if (customer == null)
                return false;

            customers.Remove(customer);

            var result = SaveCustomers(customers);
            return result;

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting customer: {ex.Message}");
            return false;
        }
    }


}
