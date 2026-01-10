using CManager.Domain.Models;
using CManager.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace CManager.Infrastructure.Repos;


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



    /*              HÄMTA SPECIFIK KUND
                    CHATGPT HJÄLPTE MIG NEDAN med koden: " c => c.Id == id)!;"
                    vid tidpunkten då jag fick hjälp av chatGPT hade jag alltså inte riktigt förstått vad koden gör eller hur den funkar.
                    Nu vet jag att koden jämför en specifik kund med ett specifikt ID och jämför om det är samma kund. (Det kallas för lambda funktion.)
                        
                    För ytterligare förklaring: 
                    c = CustomerModel { Id = 123409, FirstName = "Ivar", LastName = "Svensson" }

                    Så om man nu ska "testa" funktionen, så kan man säga tex: 
                    customer => c.123409 == 123409 = TRUE
                    customer => c.123409 == 867463 = FALSE
                    Koden returnerar alltså true eller false beroende på om ID i detta fallet matchar.
     */

        public CustomerModel GetCustomerById(Guid id)
                {
                    var customers = GetAllCustomers();
                    return customers.FirstOrDefault(c => c.Id == id)!; //Hittar kund med rätt ID.
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
