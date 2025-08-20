using BankSimulator.Components;
using BankSimulator.DTOs;
using BankSimulator.Enums;
using BankSimulator.Models;
using BankSimulator.Services;
using BankSimulator.Utils;
using Newtonsoft.Json;

namespace BankSimulator.Views
{
    internal class Payment : Panel
    {
        private readonly App _app;
        private readonly Dictionary<string, NewTextBox> _txtBoxes = [];
        private readonly Dictionary<string, NewButton> _buttons = [];
        private readonly ProductEnum _productName;
        private readonly double _itemPrice;

        public Payment(App app, ProductEnum productName, double itemPrice)
        {
            _app = app;
            _app.Controls.Add(this);
            _productName = productName;
            _itemPrice = itemPrice;

            InitializeComponents();
        }

        private void InitializeComponents()
        {
            ConfigurePanel();
            CreateTextBoxes();
            CreateButtons();
            CreateEventHandlers();
        }

        private void ConfigurePanel()
        {
            Width = _app.Width;
            Height = _app.Height;
        }

        private void CreateTextBoxes()
        {
            _txtBoxes["CPF"] = new(this, "CPF", 150, 100)
            {
                MaxLength = 11,
                Text = "12345678913"
            };
            _txtBoxes["Number"] = new(this, "Card number", 150, 150)
            {
                MaxLength = 16,
                Text = "7285584281467435"
            };
            _txtBoxes["Validate (m/yyyy)"] = new(this, "Card validity", 150, 200)
            {
                Enabled = false,
                Text = "01/2030"
            };
            _txtBoxes["CVV"] = new(this, "Card CVV", 150, 250)
            {
                MaxLength = 3,
                Text = "128"
            };
        }

        private void CreateButtons()
        {
            _buttons["Finalize"] = new(this, "Finalize", Color.LightGreen, 200, 30, 150, 300, null);
            _buttons["Cancel"] = new(this, "Cancel", Color.White, 200, 30, 150, 340, null);
        }

        private void CreateEventHandlers()
        {
            _txtBoxes["CPF"].KeyDown += (obj, e) =>
            {
                if (!(e.KeyCode == Keys.Back) && !char.IsDigit((char)e.KeyCode)) e.SuppressKeyPress = true;
            };

            _txtBoxes["Number"].KeyDown += (obj, e) =>
            {
                if (!(e.KeyCode == Keys.Back) && !char.IsDigit((char)e.KeyCode)) e.SuppressKeyPress = true;
            };

            _txtBoxes["CVV"].KeyDown += (obj, e) =>
            {
                if (!(e.KeyCode == Keys.Back) && !char.IsDigit((char)e.KeyCode)) e.SuppressKeyPress = true;
            };

            _buttons["Finalize"].Click += async (s, e) =>
            {
                if (await ProcessData.CheckPayment(_txtBoxes))
                {
                    var response = await HttpClientStatic.HttpClient.GetAsync($"Client/Get/{StoreToken.ExtractId()}");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();

                        var clientDeserialize = JsonConvert.DeserializeObject<Client>(content);

                        List<Invoice> invoices = [.. clientDeserialize!.Invoice!];

                        if (clientDeserialize is not null)
                        {
                            var invoiceItem = new InvoiceItemDTO()
                            {
                                Description = _productName.ToString(),
                                Price = _itemPrice,
                                InvoiceId = invoices[^1].Id
                            };

                            string result = await PaymentService.Pay(invoiceItem);

                            MessageBox.Show(result, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            Dispose();
                        }
                    }
                }
                else
                    MessageBox.Show("Payment failed, please try again.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            _buttons["Cancel"].Click += (s, e) =>
            {
                Dispose();
            };
        }
    }
}