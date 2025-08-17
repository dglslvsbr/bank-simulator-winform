using BankSimulator.Components;
using BankSimulator.Models;
using BankSimulator.Services;
using BankSimulator.Utils;
using Newtonsoft.Json;
using System.Globalization;

namespace BankSimulator.Views
{
    internal class Pix : Panel
    {
        private readonly App _app = null!;
        private readonly Dictionary<string, NewTextBox> _txtBoxes = [];
        private readonly Dictionary<string, NewButton> _buttons = [];

        public Pix(App app)
        {
            _app = app;
            _app.Controls.Add(this);

            InitializeComponents();
        }

        private void InitializeComponents()
        {
            ConfigurePanel();
            CreatePictureBox();
            CreateTextBoxes();
            CreateButtons();
            CreateEventHandlers();
        }

        private void ConfigurePanel()
        {
            Width = _app.Width;
            Height = _app.Height;
        }

        private void CreatePictureBox()
            => _ = new NewPictureBox(this, "Utils/Images/pix.png", 200, 180, 130, 0);

        private void CreateTextBoxes()
        {
            _txtBoxes["receiver"] = new NewTextBox(this, "Send to whom? (CPF)", 130, 230)
            {
                MaxLength = 11
            };
            _txtBoxes["value"] = new NewTextBox(this, "Quantity ($)", 130, 280)
            {
                MaxLength = 10
            };
        }

        private void CreateButtons()
        {
            _buttons["send"] = new NewButton(this, "Send Pix", Color.AliceBlue, 100, 30, 185, 320, null);
        }

        private void CreateEventHandlers()
        {
            _txtBoxes["receiver"].KeyDown += (s, e) =>
            {
                if (!(e.KeyCode == Keys.Back) && !char.IsDigit((char)e.KeyCode)) e.SuppressKeyPress = true;
            };

            _txtBoxes["value"].KeyDown += (s, e) =>
            {
                if (!(e.KeyCode == Keys.Back) && !char.IsDigit((char)e.KeyCode)) e.SuppressKeyPress = true;
            };

            _buttons["send"].Click += async (s, e) =>
            {
                if (await ProcessData.CheckPix(_txtBoxes))
                {
                    try
                    {
                        string cpf = _txtBoxes["receiver"].Text;

                        var receiver = await TransactionService.CheckCpf(cpf);

                        var clientDeserialize = JsonConvert.DeserializeObject<Client>(receiver);

                        if (clientDeserialize!.Name != null)
                        {
                            var transaction = new Transaction
                            {
                                ReceiverClient = clientDeserialize!.Id,
                                Value = double.Parse(_txtBoxes["value"].Text, CultureInfo.InvariantCulture),
                                Date = DateTime.Now,
                                ClientId = StoreToken.ExtractId()
                            };

                            string result = await TransactionService.Send(transaction);

                            MessageBox.Show(result, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else 
                            MessageBox.Show("The Pix key does not exist!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }
                    catch (HttpRequestException ex)
                    {
                        ApplicationLog.RegisterLog("An error occurred: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        ApplicationLog.RegisterLog("Generic Error: " + ex.Message);
                    }
                }
                else
                    MessageBox.Show("Check data and try again!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            };
        }
    }
}