using BankSimulator.Components;
using BankSimulator.DTOs;
using BankSimulator.Services;
using BankSimulator.Utils;
using System.Net.Http.Headers;

namespace BankSimulator.Views
{
    internal class Initial : Panel
    {
        private readonly App _app;
        private readonly Dictionary<string, NewTextBox> _txtBoxes = [];
        private readonly Dictionary<string, NewButton> _buttons = [];

        public Initial(App app)
        {
            _app = app;
            _app.Controls.Add(this);

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
            _txtBoxes["Email"] = new(this, "Email", 150, 250);
            _txtBoxes["Password"] = new(this, "Password", 150, 300)
            {
                // Hide password
                PasswordChar = '*'
            };
        }

        private void CreateButtons()
        {
            _buttons["HidePassword"] = new(this, null, Color.White, 23, 23, 353, 300, Image.FromFile("Utils/Images/eye.png"));
            _buttons["Login"] = new(this, "Login", Color.LightCyan, 100, 30, 150, 330, null);
            _buttons["Register"] = new(this, "Register", Color.LightGreen, 100, 30, 250, 330, null);
            _buttons["RecoverPassword"] = new(this, "Recover Password", Color.Orange, 200, 30, 150, 365, null);
        }

        private void CreateEventHandlers()
        {
            // Password visibility handler
            _buttons["HidePassword"].Click += (sender, args) => HidePassword.Change(_txtBoxes["Password"]);

            // Authenticate the client
            _buttons["Login"].Click += async (sender, args) =>
            {
                LoginDTO loginDto = new(_txtBoxes["Email"].Text, _txtBoxes["Password"].Text);

                if (ProcessData.CheckLoginData(loginDto))
                {
                    if (await AuthenticationService.CheckPassword(loginDto))
                    {
                        StoreToken.Stored(await AuthenticationService.Authenticate(loginDto));

                        HttpClientStatic.HttpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", StoreToken.Get());

                        _app.Screens["Main"].BringToFront();
                        _app.NewMenuStrip.EnableMenu();
                    }
                    else
                        MessageBox.Show("Email or password incorrect!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    _txtBoxes["Email"].BackColor = Color.LightPink;
                    _txtBoxes["Password"].BackColor = Color.LightPink;
                }
            };

            // Create account
            _buttons["Register"].Click += (sender, arg) =>
            {
                _app.Screens["Register"].BringToFront();
            };

            // Recover password
            _buttons["RecoverPassword"].Click += (sender, args) =>
            {
                _app.Screens["Recover"].BringToFront();
            };

            foreach (var obj in _txtBoxes)
            {
                obj.Value.KeyDown += (sender, args) => obj.Value.BackColor = default;
            }
        }
    }
}