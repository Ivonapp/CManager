using CManager.Application.Services;
using CManager.Domain.Models;
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
                Console.WriteLine("======================");
                Console.WriteLine("   MANAGE CUSTOMERS");
                Console.WriteLine("======================");
                Console.WriteLine("");
                Console.WriteLine("[1] Create new customer" +
                "\n[2] View / update customers" +
                "\n[3] Delete customer" +
                "\n[4] Exit");

                Console.WriteLine("");
                Console.Write("Choose option number: ");

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
                        DeleteCustomer();
                        break;

                    case "4":
                        return;

                    default:
                        OutputDialog("Invalid option. Please try again.");
                        break;


                }
            }
        }
    }











    //                                             CREATECUSTOMER
    private void CreateCustomer()
    {
        Console.Clear();
        Console.WriteLine("======================");
        Console.WriteLine("   CREATE CUSTOMER");
        Console.WriteLine("======================");


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
            Console.Clear();
            Console.WriteLine($"Customer \"{firstName} {lastName}\" created successfully.");
        }
        else
        {
            Console.WriteLine("Something went wrong. Please try again.");
        }
        Console.WriteLine("");
        OutputDialog("Press any key to continue...");
    }








    //                                             VIEWALLCUSTOMERS > CHOOSE SPECIFIC CUSTOMER

    private void ViewAllCustomers()
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


            // VISAR NAMN OCH EMAIL
            while (true)
            {
                Console.Clear();
                Console.WriteLine("======================");
                Console.WriteLine("    ALL CUSTOMERS");
                Console.WriteLine("======================");
                Console.WriteLine("");

                for (int i = 0; i < customers.Count; i++)
                {
                    var customer = customers[i];

                    Console.WriteLine($"[{i + 1}] Name: {customer.FirstName} {customer.LastName}, Email: {customer.Email}");
                    
                }

                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine($"[1]-[{customers.Count}] View customer information  |  [0] Go back to menu");
                var CustomerInput = Console.ReadLine();



                //                                             VIEWONECUSTOMER CHOICE


                if (!int.TryParse(CustomerInput, out int userChoice))
                {
                    OutputDialog("Not a valid number! Press any key to try again...");
                    Console.Clear();
                    continue;

                }


                if (userChoice == 0)
                {
                    return;
                }


                if (userChoice > customers.Count)
                {
                    Console.WriteLine($"Number must be between 1 and {customers.Count}. Press any key to try again...");
                    Console.ReadKey();
                    continue;
                }

                    var index = userChoice - 1;
                    var selectedCustomer = customers[index];

                    Console.Clear();
                    Console.WriteLine("Customer Information:");
                    Console.WriteLine("");
                    Console.WriteLine($"Name: {selectedCustomer.FirstName} {selectedCustomer.LastName}");
                    Console.WriteLine($"Email: {selectedCustomer.Email}");
                    Console.WriteLine($"Phone: {selectedCustomer.PhoneNumber}");
                    Console.WriteLine($"Address: {selectedCustomer.Address.StreetAddress} {selectedCustomer.Address.PostalCode} {selectedCustomer.Address.City}");
                    Console.WriteLine($"ID: {selectedCustomer.Id}");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine($"[1] Update customer information  |  [0] Go back to menu");

                    var CustomerChoice = Console.ReadLine();


                if (!int.TryParse(CustomerChoice, out int userInput))
                {
                    OutputDialog("Not a valid number! Press any key to try again...");
                    Console.Clear();
                    continue;

                }


                if (userInput == 0)
                {
                    return;
                }








                //                                      UPDATECUSTOMER

                if (userInput == 1)
                {
                    Console.WriteLine("======================");
                    Console.WriteLine("   UPDATE CUSTOMER");
                    Console.WriteLine("======================");

                    //SAMLAR IN NY DATA FÖR KUNDEN

                    var updatedFirstName = InputHelper.ValidateInput("First name", ValidationType.Required);
                    var updatedLastName = InputHelper.ValidateInput("Last name", ValidationType.Required);
                    var updatedEmail = InputHelper.ValidateInput("Email", ValidationType.Email);
                    var updatedPhone = InputHelper.ValidateInput("PhoneNumber", ValidationType.Required);
                    var updatedStreet = InputHelper.ValidateInput("Address", ValidationType.Required);
                    var updatedPostal = InputHelper.ValidateInput("PostalCode", ValidationType.Required);
                    var updatedCity = InputHelper.ValidateInput("City", ValidationType.Required);

                    var updatedCustomer = new CustomerModel
                    {
                        Id = selectedCustomer.Id,
                        FirstName = updatedFirstName,
                        LastName = updatedLastName,
                        Email = updatedEmail,
                        PhoneNumber = updatedPhone,
                        Address = new AddressModel
                        {
                            StreetAddress = updatedStreet,
                            PostalCode = updatedPostal,
                            City = updatedCity
                        }
                    };

                    var result = _customerService.UpdateCustomer(updatedCustomer);
                    if (result)
                    {
                        OutputDialog("Customer updated successfully! Press any button to go back.");
                        return;
                    }

                    else
                    { 
                    OutputDialog("Something went wrong. Please try again.");
                    }

                }
            }     
        }
    }














    //                                             DELETECUSTOMER 

    private void DeleteCustomer()
    {
        Console.Clear();

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
                Console.WriteLine("======================");
                Console.WriteLine("   DELETE CUSTOMER");
                Console.WriteLine("======================");
                Console.WriteLine("");

                for (int i = 0; i < customers.Count; i++)
                {
                    var customer = customers[i];
                    
                    Console.WriteLine($"[{i + 1}] Name: {customer.FirstName} {customer.LastName}, Email: {customer.Email}");
                    
                }


                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine($"[1]-[{customers.Count}] Delete customer  |  [0] Go back to menu");
                var input = Console.ReadLine();





    //                                             DELETECUSTOMER CHOICE

                if (!int.TryParse(input, out int userChoice))
                {
                    OutputDialog("Not a valid number! Press any key to try again...");
                    Console.Clear();
                    continue;
                    
                }

                if (userChoice == 0)
                {
                    return;
                }


                if (userChoice > customers.Count)
                {
                    Console.WriteLine($"Number must be between 1 and {customers.Count}. Press any key to try again...");
                    Console.ReadKey();
                    continue;
                }


                var index = userChoice - 1;
                var selectedCustomer = customers[index];


                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Are you sure you want to delete:");
                    Console.WriteLine("");
                    Console.WriteLine($"{selectedCustomer.FirstName} {selectedCustomer.LastName}\n{selectedCustomer.Email}");
                    Console.WriteLine("");
                    Console.Write("(y/n): ");
                    var confirmation = Console.ReadLine()!.ToLower();
                    Console.Clear();

                    if (confirmation == "y")
                        
                    {
                        var result = _customerService.DeleteCustomer(selectedCustomer.Id);
                        if (result)
                        {
                            Console.WriteLine("");
                            OutputDialog($"The customer \"{selectedCustomer.FirstName} {selectedCustomer.LastName}\" has been removed successfully. Press any key to go back.");
                            return;
                        }
                        else
                        {
                            Console.WriteLine("");
                            OutputDialog("Something went wrong. Press any key to continue...");
                            return;
                        }
                    }
                    else if (confirmation == "n")
                    {
                        Console.WriteLine("");
                        Console.WriteLine($"The customer \"{selectedCustomer.FirstName} {selectedCustomer.LastName}\" has NOT been removed. Press any key to go back.");
                        Console.ReadKey();
                        return;
                    }
                    else
                    {
                        OutputDialog("Please enter 'y' for YES or 'n' for NO. Press any key to try again...");
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


