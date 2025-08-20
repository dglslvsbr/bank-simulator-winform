using BankSimulator.DTOs;
using BankSimulator.Utils;
using System.Text;
using System.Text.Json;

namespace BankSimulator.Services
{
    internal static class PaymentService
    {
        public static async Task<string> Pay(InvoiceItemDTO invoiceItemDTO)
        {
			try
			{
				var response = await HttpClientStatic.HttpClient.PostAsync("InvoiceItems/Create",
					new StringContent(JsonSerializer.Serialize(invoiceItemDTO),
					Encoding.UTF8, "application/json"));

				var content = await response.Content.ReadAsStringAsync();

				return content;
			}
			catch (HttpRequestException ex)
			{
				ApplicationLog.RegisterLog("Request Error: " + ex.Message);
				return default!;
			}
        }
    }
}