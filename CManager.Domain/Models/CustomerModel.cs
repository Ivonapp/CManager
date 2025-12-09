using System;
using System.Collections.Generic;
using System.Text;

namespace CManager.Domain.Models
{
    public class CustomerModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!; //Null här betyder att denna är required, och att vi GARANTERAR att den kommer fyllas i
        public string LastName { get; set; } = null!;  //Null här betyder att denna är required, och att vi GARANTERAR att den kommer fyllas i
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; } //Frågetecknet framför string säger att denna är frivillig att fylla i
        public AddressModel Address { get; set; } = null!; // Vi lägger in adressen under AdressModell för SOLID principen för "single responsibilities"

    }
}
