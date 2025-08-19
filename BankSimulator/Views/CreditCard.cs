using BankSimulator.Components;
using BankSimulator.Models;
using BankSimulator.Utils;
using Newtonsoft.Json;

namespace BankSimulator.Views
{
    internal class CreditCard : Panel
    {
        private readonly App _app;
        private NewPictureBox _card = null!;

        public CreditCard(App app)
        {
            _app = app;
            _app.Controls.Add(this);

            InitializeComponents();
        }

        private void InitializeComponents()
        {
            ConfigurePanel();
            CreateCreditCard();
        }

        private void ConfigurePanel()
        {
            Width = _app.Width;
            Height = _app.Height;
        }

        private void CreateCreditCard()
        {
            _card = new(this, "Utils/Images/card.png", 300, 300, 90, 130);
            Controls.Add(_card);
        }

        public async Task ShowData()
        {
            try
            {
                var response = await HttpClientStatic.HttpClient.GetAsync($"Client/Get/{StoreToken.ExtractId()}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var client = JsonConvert.DeserializeObject<Client>(content);

                    if (client!.CreditCard is not null)
                    {
                        var card = client.CreditCard;

                        _card.Controls.Add(new Label()
                        {
                            Text = "Client: " + client.Name + " " + client.Surname,
                            ForeColor = Color.White,
                            Font = new("sans-serif bold", 12),
                            Location = new(10, 150),
                            BackColor = Color.Transparent,
                            Size = new Size(200, 20)
                        });
                        _card.Controls.Add(new Label()
                        {
                            Text = "Number: " + card!.Number,
                            ForeColor = Color.White,
                            Font = new("sans-serif bold", 12),
                            Location = new(10, 170),
                            BackColor = Color.Transparent,
                            Size = new Size(300, 20)
                        });
                        _card.Controls.Add(new Label()
                        {
                            Text = "Validate: " + card.Validate.Month + "/" + card.Validate.Year.ToString(),
                            ForeColor = Color.White,
                            Font = new("sans-serif bold", 12),
                            Location = new(10, 190),
                            BackColor = Color.Transparent,
                            Size = new Size(200, 20)
                        });
                        _card.Controls.Add(new Label()
                        {
                            Text = "CVV: " + card.CVV.ToString(),
                            ForeColor = Color.White,
                            Font = new("sans-serif bold", 12),
                            Location = new(10, 210),
                            BackColor = Color.Transparent,
                            Size = new Size(200, 20)
                        });
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                ApplicationLog.RegisterLog("Request Error: " + ex.Message);
            }
        }
    }
}