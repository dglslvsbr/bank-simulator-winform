namespace BankSimulator.Models
{
    internal class Transaction
    {
        public int Id { get; set; }
        public int EmitterClient { get; set; }
        public int SenderClient { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
    }
}