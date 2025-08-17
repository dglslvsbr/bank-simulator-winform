using BankSimulator.Components;
using BankSimulator.DTOs;
using BankSimulator.Models;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Globalization;

namespace BankSimulator.Utils
{
    static class ProcessData
    {
        public static bool CheckLoginData(LoginDTO loginDto)
        {
            if (string.IsNullOrWhiteSpace(loginDto.Email) ||
                string.IsNullOrWhiteSpace(loginDto.Password) ||
                !loginDto.Email.Contains('@'))
                return false;

            return true;
        }

        public static bool CheckRegisterData(Dictionary<string, NewTextBox> txtBoxes)
        {
            int count = 0;

            foreach (var obj in txtBoxes)
            {
                if (string.IsNullOrWhiteSpace(obj.Value.Text))
                {
                    obj.Value.BackColor = Color.LightPink;
                    count += 1;
                }
            }

            if (count == 0)
                return true;
            else
                return false;
        }

        public static bool CheckCode(string code)
        {
            bool isValid = int.TryParse(code, out _);
            return isValid;
        }

        public static async Task<bool> CheckPix(Dictionary<string, NewTextBox> txtBoxes)
        {
            string txtBoxCpf = txtBoxes["receiver"].Text;
            double txtBoxValue = int.TryParse(txtBoxes["value"].Text, out _) ? double.Parse(txtBoxes["value"].Text, CultureInfo.InvariantCulture) : 0;

            int currentClientId = StoreToken.ExtractId();

            var response = await HttpClientStatic.HttpClient
                .GetAsync($"Client/Get/{currentClientId}");

            if (!response.IsSuccessStatusCode)
                return false;

            var content = await response.Content.ReadAsStringAsync();

            var clientDeserialize = JsonConvert.DeserializeObject<Client>(content);

            if (clientDeserialize is null || clientDeserialize.CPF == txtBoxCpf ||
            txtBoxCpf.Length < 11 || txtBoxValue < 1)
                return false;

            return true;
        }
    }
}