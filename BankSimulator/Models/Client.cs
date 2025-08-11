namespace BankSimulator.Models
{
    internal class Client
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string CPF { get; set; } = null!;
        public string Password { get; set; } = null!;
        public double? Saldo { get; set; }
        public Address? Address { get; set; }
        public ICollection<Phone>? Phone { get; set; }
        public Recover? Recover { get; set; }
        public CreditCard? CreditCard { get; set; }
        public ICollection<Invoice>? Invoice { get; set; }
        public Transaction? Transaction { get; set; }
    }
}