using BankSimulator.Components;
using BankSimulator.Utils;
using BankSimulator.Services;

namespace BankSimulator.Views
{
    internal class Register : Panel
    {
        private readonly App _app;
        private readonly Dictionary<string, NewTextBox> _txtBoxes = [];
        private readonly Dictionary<string, NewButton> _buttons = [];

        public Register(App app)
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

            _ = new NewLabel(this, "The application was desenvolve for tests, no insert real data",
               Color.Black, "Arial", 10, FontStyle.Bold, 800, 300, 20, 540);
        }

        private void ConfigurePanel()
        {
            Width = _app.Width;
            Height = _app.Height;
        }

        private void CreateTextBoxes()
        {
            _txtBoxes["Name"] = new(this, "Name", 40, 120);
            _txtBoxes["Surname"] = new(this, "Surname", 250, 120);
            _txtBoxes["Email"] = new(this, "Email", 40, 180);
            _txtBoxes["Password"] = new(this, "Password", 250, 180)
            {
                PasswordChar = '*'
            };
            _txtBoxes["CPF"] = new(this, "CPF", 40, 240)
            {
                MaxLength = 11
            };
            _txtBoxes["Number"] = new(this, "Phone", 250, 240)
            {
                MaxLength = 11
            };
            _txtBoxes["Street"] = new(this, "Street", 40, 300);
            _txtBoxes["HouseNumber"] = new(this, "Number", 250, 300)
            {
                MaxLength = 3
            };
            _txtBoxes["District"] = new(this, "District", 40, 360);
            _txtBoxes["City"] = new(this, "City", 250, 360);
            _txtBoxes["State"] = new(this, "State", 40, 420);
        }

        private void CreateButtons()
        {
            _buttons["HidePassword"] = new NewButton(this, null, Color.White, 23, 23, 453, 180, Image.FromFile("Utils/Images/eye.png"));
            _buttons["CreateAccount"] = new(this, "Create Account", Color.LightGreen, 100, 30, 150, 470, null);
            _buttons["Back"] = new(this, "Back", Color.LightCyan, 100, 30, 250, 470, null);
        }

        private void CreateEventHandlers()
        {
            _txtBoxes["CPF"].KeyPress += (obj, e) =>
            {
                if (char.IsLetter(e.KeyChar)) e.Handled = true;
            };

            _txtBoxes["Number"].KeyPress += (obj, e) =>
            {
                if (char.IsLetter(e.KeyChar)) e.Handled = true;
            };

            _txtBoxes["HouseNumber"].KeyPress += (obj, e) =>
            {
                if (char.IsLetter(e.KeyChar)) e.Handled = true;
            };

            // Password visibility handler
            _buttons["HidePassword"].Click += (sender, args) => HidePassword.Change(_txtBoxes["Password"]);

            // Create the account
            _buttons["CreateAccount"].Click += async (sender, args) =>
            {
                if (ProcessData.CheckRegisterData(_txtBoxes))
                {
                    if (await RegisterService.CreateAccount(_txtBoxes))
                    {
                        MessageBox.Show("Account created successful!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _app.Screens["Initial"].BringToFront();
                    }
                    else
                        MessageBox.Show("An error occurred in server: Not has possible create the account.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else MessageBox.Show("Check data and try again.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            // Back the previous page
            _buttons["Back"].Click += (sender, args) =>
            {
                _app.Screens["Initial"].BringToFront();
            };

            foreach (var obj in _txtBoxes)
            {
                obj.Value.KeyDown += (sender, args) => obj.Value.BackColor = default;
                Controls.Add(obj.Value);
                obj.Value.BringToFront();
            }
        }
    }
}