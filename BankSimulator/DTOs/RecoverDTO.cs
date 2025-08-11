namespace BankSimulator.DTOs
{
    class RecoverDTO(string email, int code)
    {
        public string Email { get; set; } = email;
        public int Code { get; set; } = code;
    }
}