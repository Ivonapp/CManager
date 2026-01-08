using Castle.Core.Resource;
using CManager.Application.Helpers;
using CManager.Application.Services;
using CManager.Domain.Models;
using CManager.Infrastructure.Repos;
using Moq;
using System.Net;

namespace CManager.Tests.ServiceTests;


/*                    VISSA DELAR AV MIN KOD I DETTA AVSNITTET ÄR MED CHATGPT. (Dessa har jag kommenterat ut i koden med "CHATGPT" specifikt.)
                      Andra delar i koden som endast förklarar vad viss kod gör är INTE Chatgpt, utan förklaringar som jag gradvis har skrivit och skrivit om, ju mer jag förstått koden
                      medans jag byggt den lager för lager, där jag också har den som en repetition samt som en sorts "mall" för senare. Av hela kursen så var detta
                      det absolut svåraste och mest tidskrävande att greppa och förstå, där det också var svårt att finna info. Av den anledningen
                      skrev jag också mer förklaringar för att kunna använda det som mall etc. 

                      Chatgpt har använts som ett bollplank/en lärare som hjälpt mig få en bättre överblick och förståelse över något jag inte förstått,
                      där jag t ex inte lyckats få till en specifik del av en specifik kod och inte förstått hur eller varför.
                      När det åandra sidan är kod jag inte förstår eller behärskar, så kommer jag INTE att använda mig av den koden.
*/

public class CustomerServiceTest
{


    //DELETECUSTOMER 
    [Fact]
    public void DeleteCustomer_ShouldReturnTrue_WhenCustomerIsDeleted()
    {

        //ARRANGE
        var mockCustomerRepo = new Mock<ICustomerRepo>();
        var id = Guid.NewGuid(); //Ta in ID


        //                  mockCustomerRepo.setup =            (En metod som registrerar ett förväntat beteende på mocken. (säger: "när detta händer...")
        //                  r => r.DeleteCustomer(id)) =        (r = ICustomerRepo.) Ta ICustomerRepo och kör DeleteCustomer(id) som du hittar inuti ICustomerRepo.cs
        //                 .Returns(true): Säger:               "...skicka då tillbaka värdet true som svar."
        mockCustomerRepo.Setup(r => r.DeleteCustomer(id)).Returns(true);


        //                  Object innehåller (en låtsas) metod (DeleteCustomer) från mitt Interface "ICustomerRepo"
        //                  med andra ord: mockCustomerRepo.Object = ett objekt som SER UT som ICustomerRepo och går igenom DeleteCustomer där i
        var service = new CustomerService(mockCustomerRepo.Object);


        //ACT
        var result = service.DeleteCustomer(id);



        //ASSERT
        Assert.True(result); 
    }



    //GETCUSTOMERBYID
    [Fact]
    public void GetCustomerById_ShouldReturnCustomer_WhenCustomerExists()
    {


        //ARRANGE
        var mockCustomerRepo = new Mock<ICustomerRepo>();
        var id = Guid.NewGuid();

        var testCustomer = new CustomerModel
        {
            Id = Guid.NewGuid(),
            FirstName = "Test",
            LastName = "Testsson",
            Email = "Test@domain.com",
            PhoneNumber = "1234567890",
            Address = new AddressModel
            {
                StreetAddress = "Street",
                City = "City",
                PostalCode = "12345",
            }
        };

        mockCustomerRepo.Setup(r => r.GetCustomerById(id)).Returns(testCustomer);

        var service = new CustomerService(mockCustomerRepo.Object);

        //ACT
        var result = service.GetCustomerById(id);

        //ASSERT
        Assert.NotNull(result);                     // Undersöker att det returneras kund och inte null. Om null (tomt) så misslyckas testet. 
        Assert.Equal(id, result.Id);                //equal = "vågskå¨l" - jämför fejk ID med riktigt ID och ser OM dom matchar
        Assert.Equal("Test", result.FirstName);     //equal = "vågskål", jämför Testets namn med riktiga namnet och ser OM dom matchar
    }






    //CREATECUSTOMER (Testet bekräftar att en ny kund skapas och sparas korrekt.)

    //MED HJÄLP AV CHAT GPT
    // Jag har fått lite hjälp med strukturen av chatGPT för raden där jag skrivit in "var firstname ="test"" och det andra i det blocket, samt raden UNDER 
    // var result = service.CreateCustomer(firstName, lastName, email, phoneNumber, streetAddress, postalCode, city);
    // Jag höll på med denna delen rätt länge där jag försökte "minimera" koden och eventuellt se om jag kunde
    // skapa en lista med alla objekt där jag då kunde skippa alla extra rader, men det blev lite för "överkurs" för mig.

    [Fact]
    public void CreateCustomer_ShouldReturnTrue_WhenCustomerIsSavedSuccessfully()
    {

        //ARRANGE
        var mockCustomerRepo = new Mock<ICustomerRepo>();
        mockCustomerRepo.Setup(r => r.GetAllCustomers()).Returns(new List<CustomerModel>());            //FRÅN EMILS KOD //Skickar tillbaka en tom lista när vi kallar på "GetAllCustomers" 
        mockCustomerRepo.Setup(r => r.SaveCustomers(It.IsAny<List<CustomerModel>>())).Returns(true);    //FRÅN EMILS KOD //Returnerar true när vi sparar kundlistan 

        var firstName = "Test";                                                 // informationen här fick jag hjälp av med ChatGpt då jag
        var lastName = "Testsson";                                              // inte riktigt förstod hur jag skulle skriva då jag försökte
        var email = "Test@domain.com";                                          // skriva koden som jag gjorde i GETCUSTOMERBYID vilket ju inte går.
        var phoneNumber = "1234567890";                                         // Dessa kund rader kommer sen att matas in i vår "result" del.
        var streetAddress = "Street";                                           
        var postalCode = "City";                                                
        var city = "12345";                                                     

        var service = new CustomerService(mockCustomerRepo.Object);

        //ACT
        var result = service.CreateCustomer(firstName, lastName, email, phoneNumber, streetAddress, postalCode, city);
        // DENNA OCKSÅ MED HJÄLP AV CHATGPT // Skickar in kundinformationen och sparar kund, result = true om det lyckas, false annars.

        //ASSERT
        Assert.True(result); //EJ chatgpt. Undersöker att CreateCustomer returnerade true, dvs att kunden sparades korrekt. Om false så blir testet misslyckat.
    }





    //UPDATECUSTOMER (Returnerar true när repositoryt lyckas spara ändringarna för en kund.)

    [Fact]
    public void UpdateCustomer_ShouldReturnTrue_WhenUpdateIsSuccessful()
    {
        //ARRANGE
        var mockCustomerRepo = new Mock<ICustomerRepo>();

        var customer = new CustomerModel();

        mockCustomerRepo.Setup(r => r.UpdateCustomer(customer)).Returns(true);

        var service = new CustomerService(mockCustomerRepo.Object); 

        //ACT
        var result = service.UpdateCustomer(customer);

        //ASSERT
        Assert.True(result);
    }





    //GETALLCUSTOMERS    (Kontrollerar att customerService kan hämta kunder via repo.)


    [Fact]
    public void GetAllCustomers_ShouldReturnCustomers_WhenSuccessful()
    {
        // ARRANGE
        var mockCustomerRepo = new Mock<ICustomerRepo>();       // Skapar ett fake (mock) repository som ersätter den RIKTIGA
        mockCustomerRepo.Setup(r => r.GetAllCustomers()).Returns(new List<CustomerModel>()); //FRÅN EMILS KOD //när vi kallar på "GetAllCustomers", returneras en tom lista  
        var service = new CustomerService(mockCustomerRepo.Object); // Skapar CustomerService och skickar in låtsas repot "mockCustomerRepo istället för ett riktigt repositoryt



        // ACT                                                   // MED HJÄLP AV CHATGPT
        var result = service.GetAllCustomers(out bool hasError); // Jag fick hjälp från ChatGPT med denna raden eftersom jag hela tiden fick fel i testet, och jag hade aldrig sett "out bool hasError."
                                                                 // Raden gör 2 saker. Den returnerar en lista (var result) och den kan returnera ett FALSE eller TRUE värde (out bool hasError.)
                                                                 // Om out bool hasError returnerar false så inebär det = att allt gick bra och det var INGA fel när kunderna hämtades
                                                                 // Om det åandra sidan returnerar TRUE så innebär det att något gick FEL när det skulle returneras en lista.


        // ASSERT                                               // MED HJÄLP AV CHATGPT
        Assert.False(hasError);                                 // Jag fick hjälp med även denna delen av chatGPT eftersom jag hade problem med att få testet att funka och inte visste
                                                                // hur jag skulle göra. 
                                                                // Denna delen KONTROLLERAR RESULTATET från ACT.
                                                                // Denna delen styr att testet bara ska bara passera om hasError är false (alltså från ACT out bool hasError.)
                                                                // OM det i ACT delen skulle skickas ut true, så kommer alltså testet bli RÖTT.
    }

}