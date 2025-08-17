using BankSimulator.Components;
using BankSimulator.Models;
using BankSimulator.Utils;
using System.Text;
using System.Text.Json;

namespace BankSimulator.Services
{
    static class RegisterService
    {
        public static async Task<bool> CreateAccount(Dictionary<string, NewTextBox> txtBoxes)
        {
            try
            {
                var client = new Client
                {
                    Name = txtBoxes["Name"].Text,
                    Surname = txtBoxes["Surname"].Text,
                    Email = txtBoxes["Email"].Text,
                    Password = BCrypt.Net.BCrypt.HashPassword(txtBoxes["Password"].Text),
                    CPF = txtBoxes["CPF"].Text,
                    Saldo = 10000.00,
                    Address = new Address
                    {
                        Street = txtBoxes["Street"].Text,
                        HouseNumber = txtBoxes["HouseNumber"].Text,
                        District = txtBoxes["District"].Text,
                        City = txtBoxes["City"].Text,
                        State = txtBoxes["State"].Text
                    },
                    Phone = [new Phone
                {
                    Type = "Celular",
                    Number = txtBoxes["Number"].Text
                }]
                };

                var clientSerialize = JsonSerializer.Serialize(client);

                var responseClient = await HttpClientStatic.HttpClient.PostAsync("Client/Create",
                    new StringContent(clientSerialize,
                    Encoding.UTF8, "application/json"));

                return responseClient.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                ApplicationLog.RegisterLog("Request Error: " + ex.Message);
                return false;
            }
        }
    }
}