using CManager.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CManager.Presentation.ConsoleApp.Controllers;
public class MenuController
{

    private readonly ICustomerService _customerService;

    public MenuController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public void ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("*Manage Customers*" +
            "\n 1. Create a new customer" +
            "\n 2. View all customers" +
            "\n 3. View one customer" +
            "\n 4. Remove customer" +
            "\n 5. Exit" +
            "\n Choose option number: ");

            var CustomerInput = Console.ReadLine();

            switch (CustomerInput)
            {
                case "1":
                    CreateCustomer();
                    break;

                case "2":
                    ViewAllCustomers();
                    break;

                case "3":
                    ViewOneCustomer();
                    break;

                case "4":
                    RemoveCustomer();
                    break;

                case "5":
                    return;

                default:
                    OutputDialog("Invalid option");
                    break;


            }
        }
    }

    private void CreateCustomer()
    {
        Console.Clear();
        Console.WriteLine("Create Customer");

        Console.Write("First Name: ");
        var firstName = Console.ReadLine()!;

        Console.Write("Last Name: ");
        var lastName = Console.ReadLine()!;

        Console.Write("Email: ");
        var email = Console.ReadLine()!;

        Console.Write("Phonenumber: ");
        var phoneNumber = Console.ReadLine()!;

        Console.Write("Street Address: ");
        var streetAddress = Console.ReadLine()!;

        Console.Write("Postal Code: ");
        var postalCode = Console.ReadLine()!;

        Console.Write("City: ");
        var city = Console.ReadLine()!;

        var result = _customerService.CreateCustomer(firstName, lastName, email, phoneNumber, streetAddress, postalCode, city);
        if (result)
        {
            Console.WriteLine("Customer created");
            Console.WriteLine($"Name: {firstName} {lastName}");
        }
        else
        {
            Console.WriteLine("Something went wrong. Please try again.");
        }

        OutputDialog("Press any key to continue...");
    }


    private void ViewAllCustomers()
    {
        Console.Clear();
        Console.WriteLine("All Customers");

        var customers = _customerService.GetAllCustomers(out bool hasError);

        if (hasError)
        {
            Console.WriteLine("Something went wrong. Please try again later.");
        }

        if (!customers.Any()) /*Om listan är tom*/
        {
            Console.WriteLine("No customers found");
        }
        else
        {
            foreach(var customer in customers)
            {
                Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}" );
                Console.WriteLine($"Email: {customer.Email}");
                Console.WriteLine();

            }
        }

        OutputDialog("Press any key to continue...");
    }









    //ARBETAR MED NEDAN 
    private void ViewOneCustomer()
    {
        Console.Clear();

        var customers = _customerService.GetAllCustomers(out bool hasError).ToList();

        if (hasError)
        {
            Console.WriteLine("Something went wrong. Please try again later.");
        }

        if (!customers.Any()) /*Om listan är tom*/
        {
            Console.WriteLine("No customers found");
        }
        else
        {

            int number = 1;

            foreach (var customer in customers)
            {
                Console.WriteLine($"{number}. Name: {customer.FirstName} {customer.LastName}");
                Console.WriteLine($"Email: {customer.Email}");
                number++;
                Console.WriteLine();
            }
        }

        Console.WriteLine("Enter the number of the customer you wish to see: ");
        var CustomerInput = Console.ReadLine();

    }

    /*Note-to-self: Behöver fortsätta med kod som visar specifik kund kopplat till siffran användaren skriver in
     SAMT snygga till meny-valet "Enter the number of the customer you wish to see"*/








    private void RemoveCustomer()
    {
        Console.Clear();

        var customers = _customerService.GetAllCustomers(out bool hasError).ToList();

        if (hasError)
        {
            Console.WriteLine("Something went wrong. Please try again later.");
        }

        if (!customers.Any()) /*Om listan är tom*/
        {
            Console.WriteLine("No customers found");
        }
        else
        {

            int number = 1;

            foreach (var customer in customers)
            {
                Console.WriteLine($"{number}. Name: {customer.FirstName} {customer.LastName}");
                Console.WriteLine($"Email: {customer.Email}");
                number++;
                Console.WriteLine();
            }
        }

        Console.WriteLine("Enter the number of the customer you wish to remove: ");
        var CustomerInput = Console.ReadLine();

    }

    //Note-to-self: Behöver fortsätta med kod som raderar specifik kund kopplat till siffran användaren skriver in

































    private void OutputDialog(string message)
    {
        Console.WriteLine(message);
        Console.ReadKey();
    }



}
