namespace BankSimulator.Models
{
    internal class Address
    {
        public int Id { get; set; }
        public string Street { get; set; } = null!;
        public string HouseNumber { get; set; } = null!;
        public string District { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public int ClientId { get; set; }
        public Client? Client { get; set; }
    }
}