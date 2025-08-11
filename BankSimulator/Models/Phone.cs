namespace BankSimulator.Models
{
    internal class Phone
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public string Number { get; set; } = null!;
        public int ClientId { get; set; }
        public Client? Client { get; set; }
    }
}