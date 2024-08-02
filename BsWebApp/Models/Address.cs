using System.Collections.Generic;

namespace BsWebApp.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string AddressStreet { get; set; }
        public string AddressStreetExtra { get; set; }
        public string AddressCity { get; set; }
        public string AddressZip { get; set; }
        public string AddressCountry { get; set; }
        public string AddressState { get; set; }
        public string Label { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
