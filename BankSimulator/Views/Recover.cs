using BankSimulator.Components;
using BankSimulator.DTOs;
using BankSimulator.Services;
using BankSimulator.Utils;

namespace BankSimulator.Views
{
    internal class Recover : Panel
    {
        private readonly App _app;
        private readonly Dictionary<string, NewTextBox> _txtBoxes = [];
        private readonly Dictionary<string, NewButton> _buttons = [];

        public Recover(App app)
        {
            _app = app;
            _app.Controls.Add(this);

            InitializeComponents();
        }

        private void InitializeComponents()
        {
            ConfiguraPanel();
            CreateTextBoxes();
            CreateButtons();
            CreateEventHandlers();
        }

        private void ConfiguraPanel()
        {
            Width = _app.Width;
            Height = _app.Height;
        }

        private void CreateTextBoxes()
        {
            _txtBoxes["Email"] = new(this, "Email", 150, 250);
        }

        private void CreateButtons()
        {
            _buttons["Send"] = new(this, "Send", Color.Orange, 100, 30, 150, 280, null);
            _buttons["Back"] = new(this, "Back", Color.White, 100, 30, 250, 280, null);
        }

        private void CreateEventHandlers()
        {
            // Try recover password
            _buttons["Send"].Click += async (obj, s) =>
            {
                if (!await EmailService.CheckEmail(_txtBoxes["Email"].Text))
                    MessageBox.Show("Not was possible send code for verification",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    MessageBox.Show("Code of verification enviaded!",
                                        "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _txtBoxes["Email"].Enabled = false;
                    _buttons["Send"].Enabled = false;
                    _buttons["Back"].Enabled = false;

                    _txtBoxes["Code"] = new NewTextBox(this, "Code", 300, 350)
                    {
                        MaxLength = 6,
                    };
                    _txtBoxes["Code"].Select();
                    _txtBoxes["Code"].KeyPress += (obj, e) =>
                    {
                        if (char.IsLetter(e.KeyChar)) e.Handled = true;
                    };

                    _buttons["Ok"] = new NewButton(this, "Ok", Color.AliceBlue, 100, 30, 350, 380, null);
                    _buttons["Ok"].Click += async (obj, e) =>
                    {
                        if (!ProcessData.CheckCode(_txtBoxes["Code"].Text))
                            MessageBox.Show("Invalid number!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        else
                        {
                            var recoverDto = new RecoverDTO(_txtBoxes["Email"].Text, int.Parse(_txtBoxes["Code"].Text));

                            if (!await RecoverService.CheckingCode(recoverDto))
                                MessageBox.Show("Invalid code for verification", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else
                            {
                                _txtBoxes["Code"].Enabled = false;
                                _buttons["Ok"].Enabled = false;

                                _txtBoxes["NewPassword"] = new NewTextBox(this, "New Password", 300, 450)
                                {
                                    PasswordChar = '*'
                                };
                                _txtBoxes["NewPassword"].Select();

                                _buttons["HidePassword"] = new NewButton(this, null, Color.White, 23, 23, 503, 450, Image.FromFile("Utils/Images/eye.png"));
                                _buttons["HidePassword"].Click += (obj, e) => HidePassword.Change(_txtBoxes["NewPassword"]);

                                _buttons["Change"] = new NewButton(this, "Change", Color.AliceBlue, 100, 30, 350, 480, null);
                                _buttons["Change"].Click += async (obj, e) =>
                                {
                                    var loginDto = new LoginDTO(_txtBoxes["Email"].Text, _txtBoxes["NewPassword"].Text);

                                    if (!await RecoverService.Change(loginDto))
                                        MessageBox.Show("The password not was changed with successful ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    else
                                    {
                                        MessageBox.Show("Password was changed with successful!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        _app.Screens["Initial"].BringToFront();
                                    }
                                };
                            }
                        }
                    };
                }
            };

            // Back the previous page
            _buttons["Back"].Click += (obj, s) =>
            {
                _app.Screens["Initial"].BringToFront();
            };
        }
    }
}