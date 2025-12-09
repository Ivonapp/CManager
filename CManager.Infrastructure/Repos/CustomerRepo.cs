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
            Console.WriteLine($"Error saving file with customers: { ex.Message}");
            return false;
        }
    }
}
