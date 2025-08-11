namespace BankSimulator.Models
{
    internal class Recover
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime ExpireAt { get; set; }
        public bool Used { get; set; }
        public int ClientId { get; set; }
        public Client? Client { get; set; }
    }
}