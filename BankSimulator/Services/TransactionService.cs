using BankSimulator.Models;
using BankSimulator.Utils;
using System.Text;
using System.Text.Json;

namespace BankSimulator.Services
{
    internal static class TransactionService
    {
        public static async Task<string> CheckCpf(string cpf)
        {
            try
            {
                var response = await HttpClientStatic.HttpClient.GetAsync($"Client/GetClientByCpf/{cpf}");

                var content = await response.Content.ReadAsStringAsync();

                return content;
            }
            catch (HttpRequestException e)
            {
                ApplicationLog.RegisterLog("Request Error: " + e.Message);
                return "An error occurred the check the receiver client";
            }
        }

        public static async Task<string> Send(Transaction transaction)
        {
            try
            {
                var response = await HttpClientStatic.HttpClient.PostAsync("Transaction/Create",
                new StringContent(JsonSerializer.Serialize(transaction),
                Encoding.UTF8, "application/json"));

                var content = await response.Content.ReadAsStringAsync();

                return content;
            }
            catch (HttpRequestException e)
            {
                ApplicationLog.RegisterLog("Request Error: " + e.Message);
                return "Error processing the transaction";
            }
        }
    }
}