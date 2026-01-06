using CManager.Application.Helpers;
using CManager.Application.Services;
using CManager.Domain.Models;
using CManager.Infrastructure.Repos;
using Moq;

namespace CManager.Tests.ServiceTests;

public class CustomerServiceTest
{
   


    //                  DELETECUSTOMER 
    /*                  KODEN SOM SKA BERÖRAS  
    public bool DeleteCustomer(Guid id)
    {
        return _customerRepo.DeleteCustomer(id);
    }
    */




    //                  DELETECUSTOMER 
    /*                  KODEN SOM SKRIVS OM */

    [Fact]

    public void DeleteCustomer_RetreiveCustomer_ReturnsTrue()
    {
        //                  ARRANGE - (mockar REPO)
        var mocknamn = new Mock<ICustomerRepo>();
        //skriva VAD den ska göra


        //                  ACT     - SERVICE
        var result = Service.RetrieveCustomer(id);


        //                  ASSERT
        Assert.True(result);
        // eller
        //Assert.False(result); */
    }

























}













//- CreateCustomer





//- GetAllCustomers (Måste testa både framgång och när hasError blir true)





//- UpdateCustomer




//- GetCustomerById