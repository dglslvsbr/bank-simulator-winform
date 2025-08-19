using BankSimulator.Views;

namespace BankSimulator.Components
{
    public class MainMenu : MenuStrip
    {
        private readonly App _app = null!;

        public MainMenu(App form)
        {
            _app = form;
            _app.Controls.Add(this);

            AddItems();
            MenuStripEvents();
        }

        private void AddItems()
        {
            Items.Add("Home");
            Items.Add("Person Data");
            Items.Add("Credit Card");
            Items.Add("PIX");
            Items.Add("Transactions");
            Items.Add("Store");
            Items.Add("Close");
        }

        private void MenuStripEvents()
        {
            Items[0].Click += (sender, args) =>
            {
                _app.Screens["Main"].BringToFront();
                EnableMenu();
            };

            Items[1].Click += async (sender, args) =>
            {
                _app.Screens["PersonData"].BringToFront();
                EnableMenu();

                await ((PersonData)_app.Screens["PersonData"]).MostrarDados();
            };

            Items[2].Click += async (sender, args) =>
            {
                _app.Screens["CreditCard"].BringToFront();
                EnableMenu();

                await ((CreditCard)_app.Screens["CreditCard"]).ShowData();
            };

            Items[3].Click += (sender, args) =>
            {
                _app.Screens["Pix"].BringToFront();
                EnableMenu();
            };

            Items[4].Click += (sender, args) => MessageBox.Show("Ainda não está pronto!!");
            Items[5].Click += (sender, args) =>
            {
                _app.Screens["Store"].BringToFront();
                EnableMenu();
            };

            Items[6].Click += (sender, args) => _app.Close();
        }

        public void EnableMenu()
        {
            Visible = true;
            BringToFront();
        }

        public void DisableMenu()
        {
            Visible = false;
        }
    }
}