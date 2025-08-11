using BankSimulator.DTOs;
using BankSimulator.Models;
using BankSimulator.Utils;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace BankSimulator.Services
{
    class EmailService
    {
        public static async Task<bool> CheckEmail(string email)
        {
            try
            {
                var response = await HttpClientStatic.HttpClient
                    .PostAsync("Recover/CheckEmail",
                    new StringContent(JsonConvert
                    .SerializeObject(new { Email = email }),
                    Encoding.UTF8, "application/json"));

                if (!response.IsSuccessStatusCode)
                    return false;

                var content = await response.Content.ReadAsStringAsync();

                var contentResult = JsonConvert.DeserializeObject<Client>(content);

                if (contentResult is null)
                    return false;

                // Generate code for recover of password
                int code = CodeGenerator.Random();

                var recoverDto = new RecoverDTO(contentResult.Email, code);

                if (!await RecoverService.Create(recoverDto, contentResult.Id))
                    return false;

                Send(contentResult.Email, code);
                return true;
            }
            catch (Exception e)
            {
                ApplicationLog.RegisterLog("An error occurred: " + e.Message);
                return false;
            }
        }

        public static void Send(string email, int code)
        {
            try
            {
                string rematente = "seu-email-aqui";
                string password = "sua-senha-aqui";

                MailMessage _mailMessage = new()
                {
                    From = new MailAddress(rematente)
                };
                _mailMessage.To.Add(email);
                _mailMessage.Subject = "Recover to Password";
                _mailMessage.Body = $"Code of recover: {code}";
                _mailMessage.IsBodyHtml = false;

                SmtpClient smtp = new("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential(rematente, password),
                    EnableSsl = true
                };

                smtp.Send(_mailMessage);
            }
            catch (SmtpException e)
            {
                ApplicationLog.RegisterLog($"An error occurred: {e.Message}");
            }
        }
    }
}