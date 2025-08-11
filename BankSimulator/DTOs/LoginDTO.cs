namespace BankSimulator.DTOs
{
    class LoginDTO(string email, string password)
    {
        public string Email { get; set; } = email;
        public string Password { get; set; } = password;
    }
}
