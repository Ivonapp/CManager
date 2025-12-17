using CManager.Application.Services;
using CManager.Presentation.ConsoleApp.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CManager.Presentation.ConsoleApp.Controllers;

public class MenuController(ICustomerService customerService)
{
    private readonly ICustomerService _customerService = customerService;

    public void ShowMenu()
    {
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Manage Customers" +
                "\n [1] Create a new customer" +
                "\n [2] View all customers" +
                "\n [3] View one customer" +
                "\n [4] Delete customer" +
                "\n [5] Exit" +
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
                        DeleteCustomer();
                        break;

                    case "5":
                        return;

                    default:
                        OutputDialog("Invalid option");
                        break;


                }
            }
        }
    }

    //                                             CREATECUSTOMER
    private void CreateCustomer()
    {
        Console.Clear();
        Console.WriteLine("Create Customer");


        var firstName = InputHelper.ValidateInput("First name", ValidationType.Required);
        var lastName = InputHelper.ValidateInput("Last name", ValidationType.Required);
        var email = InputHelper.ValidateInput("Email", ValidationType.Email);
        var phoneNumber = InputHelper.ValidateInput("PhoneNumber", ValidationType.Required);
        var streetAddress = InputHelper.ValidateInput("Address", ValidationType.Required);
        var postalCode = InputHelper.ValidateInput("PostalCode", ValidationType.Required);
        var city = InputHelper.ValidateInput("City", ValidationType.Required);


        var result = _customerService.CreateCustomer(firstName, lastName, email, phoneNumber, streetAddress, postalCode, city);
        
        if (result)
        {
            Console.WriteLine("");
            Console.WriteLine("Customer created");
            Console.WriteLine($"Name: {firstName} {lastName}");
        }
        else
        {
            Console.WriteLine("Something went wrong. Please try again.");
        }

        OutputDialog("Press any key to continue...");
    }

    //                                             VIEWALLCUSTOMERS

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







   
    //                                             VIEWONECUSTOMER
    private void ViewOneCustomer()
    {
        Console.Clear();

        var customers = _customerService.GetAllCustomers(out bool hasError);

        if (hasError)
        {
            Console.WriteLine("Something went wrong. Please try again later.");
        }

        if (!customers.Any())
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




        //                                          ANVÄNDAREN VÄLJER EN KUND


            if (int.TryParse(CustomerInput, out int selected) &&
            selected >= 1 &&
            selected <= customers.Count())
            {

            
            var customer = customers.ElementAt(selected - 1);

            Console.Clear();
            Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}");
            Console.WriteLine($"Email: {customer.Email}");
            Console.WriteLine($"Phone: {customer.PhoneNumber}");
            Console.WriteLine($"Address: {customer.Address.StreetAddress} {customer.Address.PostalCode} {customer.Address.City}");
            Console.WriteLine($"ID: {customer.Id}");
            Console.WriteLine();
            
        }
        else
        {
            Console.WriteLine("Invalid selection. Please try again.");
            
        }
        OutputDialog("Press any key to continue...");
    }






























    //                                             DELETECUSTOMER 



    private void DeleteCustomer()
    {
        Console.Clear();
        Console.WriteLine("Delete Customer");

        var customers = _customerService.GetAllCustomers(out bool hasError).ToList();

        if (hasError)
        {
            Console.WriteLine("Something went wrong. Please try again later");
        }

        if (!customers.Any())
        {
            Console.WriteLine("No customers found");
        }
        else
        {
            while (true)
            {
                for (int i = 0; i < customers.Count; i++)
                {
                    var customer = customers[i];
                    Console.WriteLine($"[{i + 1}] {customer.FirstName} {customer.LastName} {customer.Email}");
                }

                Console.WriteLine("[0] Go back to menu");
                Console.Write("Enter customer number to delete: ");
                var input = Console.ReadLine();

                // siffra?
                // vi har valt något
                if (!int.TryParse(input, out int choice))
                {
                    OutputDialog("Not a valid number! Press any key to try again...");
                    continue;
                }

                // valt 0 för att gå tillbaka
                if (choice == 0)
                {
                    return;
                }

                // välja rätt customer
                if (choice > customers.Count)  // 0,1,2  => 1,2,3
                {
                    Console.WriteLine($"Number must be between 1 and {customers.Count}. Press any key to try again...");
                    Console.ReadKey();
                    continue;
                }

                //bekräftelse
                var index = choice - 1;
                var selectedCustomer = customers[index];

                Console.WriteLine("You have selected: ");
                Console.WriteLine($"Name: {selectedCustomer.FirstName} {selectedCustomer.LastName} {selectedCustomer.Email}");


                while (true)
                {
                    Console.Write("Are you sure you want to delete this customer? (y/n): ");
                    var confirmation = Console.ReadLine()!.ToLower();

                    if (confirmation == "y")
                        Console.Clear();
                    {
                        var result = _customerService.DeleteCustomer(selectedCustomer.Id);
                        if (result)
                        {
                            OutputDialog("Customer was removed, press any key to go back...");
                            return;
                        }
                        else
                        {
                            OutputDialog("Something went wrong. Please contact support... press any key to continue...");
                            return;
                        }
                    }
                    else if (confirmation == "n")
                    {
                        return;
                    }
                    else
                    {
                        OutputDialog("Please enter 'y' for yes or 'n' for no, press any key to try again...");
                        continue;
                    }
                }
            }
        }
        OutputDialog("Press any key to continue...");
    }

    private void OutputDialog(string message)
    {
        Console.WriteLine(message);
        Console.ReadKey();
    }
}




































    /*kod för att radera kund
     *  private void RemoveCustomer()
    {
        Console.Clear();

        var customers = _customerService.GetAllCustomers(out bool hasError).ToList();

        if (hasError)
        {
            Console.WriteLine("Something went wrong. Please try again later.");
        }

        if (!customers.Any())
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
    }*/


    //Note-to-self: Behöver fortsätta med kod som raderar specifik kund kopplat till siffran användaren skriver in




