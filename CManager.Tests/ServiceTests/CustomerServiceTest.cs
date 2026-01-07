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


    [Fact]

        public void DeleteCustomer_ReturnsTrue()
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
        //                  *skriv vad som händer imr*
            Assert.True(result);
            // eller
            //Assert.False(result); */
    }



}













//- CreateCustomer





//- GetAllCustomers (Måste testa både framgång och när hasError blir true)





//- UpdateCustomer




//- GetCustomerById