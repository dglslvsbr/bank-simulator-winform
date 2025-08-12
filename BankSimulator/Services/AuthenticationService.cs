using BankSimulator.DTOs;
using BankSimulator.Utils;
using System.Text;
using System.Text.Json;

namespace BankSimulator.Services
{
    static class AuthenticationService
    {
        public static async Task<bool> CheckPassword(LoginDTO loginDTO)
        {
            try
            {
                var response = await HttpClientStatic.HttpClient.PostAsync("Auth/CheckPassword",
                    new StringContent(JsonSerializer.Serialize(loginDTO),
                    Encoding.UTF8, "application/json"));

                return response.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                ApplicationLog.RegisterLog("An error occurred: " + e.Message);
                return false;
            }
        }

        public static async Task<string> Authenticate(LoginDTO loginDto)
        {
            try
            {
                var response = await HttpClientStatic.HttpClient.PostAsync("Auth/Login",
                    new StringContent(JsonSerializer.Serialize(loginDto),
                    Encoding.UTF8, "application/json"));

                var content = await response.Content.ReadAsStringAsync();

                if (content is null)
                    return "Authentication failed, please try again.";

                return content;
            }
            catch (Exception e)
            {
                ApplicationLog.RegisterLog("An error occurred: " + e.Message);
                return null!;
            }
        }
    }
}