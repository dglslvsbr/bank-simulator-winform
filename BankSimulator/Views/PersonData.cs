using BankSimulator.Components;
using BankSimulator.Models;
using BankSimulator.Utils;
using Newtonsoft.Json;

namespace BankSimulator.Views
{
    internal class PersonData : Panel
    {
        private readonly App _app;

        public PersonData(App app)
        {
            _app = app;
            _app.Controls.Add(this);

            InitializeComponents();
        }

        private void InitializeComponents()
        {
            ConfigurePanel();
            EnableMenu();
        }

        private void ConfigurePanel()
        {
            Width = _app.Width;
            Height = _app.Height;
        }

        private void EnableMenu()
        {
            _app.NewMenuStrip.EnableMenu();
        }

        public async Task MostrarDados()
        {
            var response = await HttpClientStatic.HttpClient.GetAsync($"Client/Get/{StoreToken.ExtractId()}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var client = JsonConvert.DeserializeObject<Client>(content);

                if (client is not null)
                {
                    _ = new NewLabel(this, "Data the client", Color.Black, "Calibri", 16, FontStyle.Bold, 200, 40, 196, 150);
                    _ = new NewLabel(this, $"Name: {client.Name}", Color.Black, "Calibri", 10, FontStyle.Bold, 200, 40, 200, 200);
                    _ = new NewLabel(this, $"Surname: {client.Surname}", Color.Black, "Calibri", 10, FontStyle.Bold, 200, 40, 200, 250);
                    _ = new NewLabel(this, $"Email: {client.Email}", Color.Black, "Calibri", 10, FontStyle.Bold, 250, 40, 200, 300);
                    _ = new NewLabel(this, $"CPF: {client.CPF}", Color.Black, "Calibri", 10, FontStyle.Bold, 200, 40, 200, 350);
                    _ = new NewLabel(this, $"Saldo: {client.Saldo}", Color.Black, "Calibri", 10, FontStyle.Bold, 200, 40, 200, 400);
                }
            }
            else MessageBox.Show("An error occurred in show data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}