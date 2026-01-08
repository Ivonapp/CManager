using CManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace CManager.Infrastructure.Helpers
{
    //RENSAD KOD NEDAN
    //GÖR JSON LÄSBAR
    public class JsonFormatter
    {
        private readonly JsonSerializerOptions _jsonOptions;

        public JsonFormatter()
        {
            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,               //Mer lättläst
                PropertyNameCaseInsensitive = true, //gör så man kan skriva med både små och stora bokstäver som ToUpper/ToLower
            };
        }





        //RENSAD KOD NEDAN
        //GETALLCUSTOMERS
        //Omvandlar en Json-sträng till lista med kunder.
        //ELLER en tom lista om det misslyckas.
        //Jag är lite osäker på om jag vill ändra nedan kod från en IF struktur till try/catch istället. Låter det vara sålänge.
        public List<CustomerModel> Deserialize(string json)
        {
            var customers = JsonSerializer.Deserialize<List<CustomerModel>>(json, _jsonOptions);

            if (customers == null)
            {
                return new List<CustomerModel>();
            }

            return customers;
        }



        //RENSAD KOD NEDAN
        //SAVECUSTOMERS
        //Omvandlar en lista med kunder till Json-sträng.
        //Vid misslyckande skickas exception ut.
        public string SerializeCustomers(List<CustomerModel> customers)
        {
            try
            {
                return JsonSerializer.Serialize(customers, _jsonOptions);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong. {ex.Message}");
                throw;
                //eller så kan du testa att ha
                //return customers ?? [];
                //istället för throw som Emil visade.
            }
        }
    }
}
