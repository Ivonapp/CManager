using Castle.Core.Resource;
using CManager.Application.Helpers;
using CManager.Application.Services;
using CManager.Domain.Models;
using CManager.Infrastructure.Repos;
using Moq;
using System.Net;

namespace CManager.Tests.ServiceTests;

public class CustomerServiceTest
{



    //                                  DELETECUSTOMER 

    /*                  KODEN SOM SKA BERÖRAS  
    public bool DeleteCustomer(Guid id)
    {
        return _customerRepo.DeleteCustomer(id);
    }

    */

    [Fact]
    public void DeleteCustomer_ShouldReturnTrue_WhenCustomerIsDeleted()
    {

        //                  ARRANGE - (mockar REPO)
        var mockCustomerRepo = new Mock<ICustomerRepo>();
        var id = Guid.NewGuid(); //Ta in ID


        //                  mockCustomerRepo.setup =            (En metod som registrerar ett förväntat beteende på mocken. (säger: "när detta händer...")
        //                  r => r.DeleteCustomer(id)) =        (r = ICustomerRepo.) Ta ICustomerRepo och kör DeleteCustomer(id) som du hittar inuti ICustomerRepo.cs
        //                 .Returns(true): Säger:               "...skicka då tillbaka värdet true som svar."
        mockCustomerRepo.Setup(r => r.DeleteCustomer(id)).Returns(true);


        //                  Object innehåller (en låtsas) metod (DeleteCustomer) från mitt Interface "ICustomerRepo"
        //                  med andra ord: mockCustomerRepo.Object = ett objekt som SER UT som ICustomerRepo och går igenom DeleteCustomer där i
        var service = new CustomerService(mockCustomerRepo.Object);


        //                  ACT     - SERVICE
        //                  *skriv vad som händer imr*
        var result = service.DeleteCustomer(id);



        //                  ASSERT
        //                  returnerar bara ja eller nej (eftersom originalkoden är bool)
        Assert.True(result); //true
        // eller
        //Assert.False(result); */
    }













    //-                             GetCustomerById
    /*public CustomerModel GetCustomerById(Guid id)
    {
        return _customerRepo.GetCustomerById(id);
    }*/


    [Fact]
    public void GetCustomerById_ShouldReturnCustomer_WhenCustomerExists()
    {


        //                  ARRANGE, setup returns()
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

        mockCustomerRepo.Setup(r => r.GetCustomerById(id)).Returns(testCustomer); // vad den returnerar

        var service = new CustomerService(mockCustomerRepo.Object); // service 

        //                  ACT
        //                  var result / använd NAMNET på METODEN i originalkoden
        var result = service.GetCustomerById(id);

        //                  ***ASSERT***
        //                  returnerar CUSTOMER eftersom syftet är att returnera en kund
        Assert.NotNull(result);
        Assert.Equal(id, result.Id);                //equal = "vågskå¨l" - jämför fejk ID med riktigt ID och ser OM dom matchar
        Assert.Equal("Test", result.FirstName);     //equal = "vågskål", jämför Testets namn med riktiga namnet och ser OM dom matchar
    }

















    //                                          - CreateCustomer

    //KODEN
    /*public bool CreateCustomer(string firstName, string lastName, string email, string phoneNumber, string streetAddress, string postalCode, string city)
    {
        try
        {
            var customers = _customerRepo.GetAllCustomers();
            customers.Add(customerModel);
            var result = _customerRepo.SaveCustomers(customers);
            return result;
        }
        catch (Exception)
        {
            return false;
        }
    }*/



    [Fact]
    public void CreateCustomer_ShouldReturnTrue_WhenCustomerIsSavedSuccessfully()
    {

        //ARRANGE (MOCK, Repository Interfacet)
        var mockCustomerRepo = new Mock<ICustomerRepo>();
        mockCustomerRepo.Setup(r => r.GetAllCustomers()).Returns(new List<CustomerModel>()); //FRÅN EMILS KOD
        mockCustomerRepo.Setup(r => r.SaveCustomers(It.IsAny<List<CustomerModel>>())).Returns(true); //FRÅN EMILS KOD

        var firstName = "Test";
        var lastName = "Testsson";
        var email = "Test@domain.com";
        var phoneNumber = "1234567890";
        var streetAddress = "Street";
        var postalCode = "City";
        var city = "12345";

        var service = new CustomerService(mockCustomerRepo.Object);

        //ACT (SERVICE, CustomerService)
        var result = service.CreateCustomer(firstName, lastName, email, phoneNumber, streetAddress, postalCode, city);

        //ASSERT
        Assert.True(result);
    }














    //                                  - UpdateCustomer

    //KODEN

    /*    public bool UpdateCustomer(CustomerModel updatedCustomer)
    {
        try
        {
            return _customerRepo.UpdateCustomer(updatedCustomer);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Not possible to update the customer information. {ex.Message}");
            return false;
        }
    }*/




    [Fact]
    public void UpdateCustomer_ShouldReturnTrue_WhenUpdateIsSuccessful()
    {

        //ARRANGE (MOCK, Repository Interfacet)
        var mockCustomerRepo = new Mock<ICustomerRepo>();

        var customer = new CustomerModel();

        mockCustomerRepo.Setup(r => r.UpdateCustomer(customer)).Returns(true);

        var service = new CustomerService(mockCustomerRepo.Object); 

        //ACT (SERVICE, CustomerService)
        var result = service.UpdateCustomer(customer);

        //ASSERT (Undersöker om resultatet blev rätt)
        Assert.True(result);
    }




















    //                                  - GetAllCustomers

    //KODEN

    /*public IEnumerable<CustomerModel> GetAllCustomers(out bool hasError)
        {
            hasError = false;

            try
            {
                var customers = _customerRepo.GetAllCustomers();
                return customers;
            }
            catch (Exception)
            {

                //Här Kommer throw hamna från customerrepo - getallcustomers
                hasError = true;
                return new List<CustomerModel>();
            }
        }*/




    //Kontrollerar att customerService kan hämta kunder via repo
    [Fact]
    public void GetAllCustomers_ShouldReturnCustomers_WhenSuccessful()
    {
        // ARRANGE
        var mockCustomerRepo = new Mock<ICustomerRepo>(); 

        var testCustomers = new List<CustomerModel> {new CustomerModel()}; 

        mockCustomerRepo.Setup(r => r.GetAllCustomers()).Returns(testCustomers);
        var service = new CustomerService(mockCustomerRepo.Object); 


        // ACT
        var result = service.GetAllCustomers(out bool hasError); 


        // ASSERT
        Assert.False(hasError); 
        // FALSE kontrolerrar att värdet som skickas in är falskt.
        // 
    }

}













//"HANS video - Skapa enhetstester för att testa och kvalitetssäkra din kod"
// Assert.equal
// Assert.Null(member.PhoneNumber);
// Assert.NotNull(member);