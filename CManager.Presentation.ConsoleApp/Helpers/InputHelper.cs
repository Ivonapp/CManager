using System.Text.RegularExpressions;

namespace CManager.Presentation.ConsoleApp.Helpers;

public enum ValidationType
{
    Required,               //CreateCustomer
    NotRequired,            //UpdateCustomer
    Email,                  //CreateCustomer
    EmailOptional,          //UpdateCustomer
}

public static class InputHelper
{

    public static string? ValidateInput(string fieldName, ValidationType validationType)
    {
        while (true)
        {
            Console.Write($"{fieldName}: ");
            var input = Console.ReadLine()!;

            if (string.IsNullOrWhiteSpace(input))
            {
                if (validationType == ValidationType.NotRequired || validationType == ValidationType.EmailOptional) //UpdateCustomer, kan lämnas tomt
                    return null;      //UpdateCustomer, kan lämnas tomt - "null" så att inte den gamla informationen suddas ut om användaren ej fyller i nåt


                Console.WriteLine($"{fieldName} is required. Press any key to try again....");
                Console.ReadKey();
                continue;
            }


            var (isValid, errorMessage) = ValidateType(input, validationType);

            if (isValid)
                return input;

            Console.WriteLine($"{errorMessage}. Press any key to continue:");
            Console.ReadKey();
        }

    }







    private static (bool isValid, string errorMessage) ValidateType(string input, ValidationType type)
    {
        switch (type)
        {
            case ValidationType.Required:
                return (true, "");


            case ValidationType.Email:
                if (IsValidEmail(input))
                {
                    return (true, "");
                }
                else
                {
                    return (false, "Invalid email. Use name@example.com ");
                }

                //CustomerUpdate
                //Email är valfri men OM kund fyller i email så ska det föhlja regex
            case ValidationType.EmailOptional:
                if (string.IsNullOrWhiteSpace(input))
                { 
                    return (true, ""); // OM EMAIL LÄMNAS TOM
                }

                if (IsValidEmail(input))  
                {
                    return (true, ""); //OM KUND FYLLER I EMAIL SOM FÖLJER REGEX
                }

                else
                {
                    return (false, "Invalid email. Use name@example.com"); //oM EMAIL FYLLS I MEN INTE FÖLJER REGEX
                }


            default:
                return (true, "");
        }

    }





    private static bool IsValidEmail(string input)
    {
        var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(input, pattern);
    }

}
