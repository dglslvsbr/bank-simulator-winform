using BankSimulator.Components;

namespace BankSimulator.Views
{
    internal class Store : Panel
    {
        private readonly App _app = null!;
        private readonly Dictionary<PictureBox, NewButton> _storeItems = [];

        public Store(App app)
        {
            _app = app;
            _app.Controls.Add(this);

            InitializeComponents();
        }

        private void InitializeComponents()
        {
            ConfigurePanel();
            CreateStoreItems();
        }

        private void ConfigurePanel()
        {
            Width = _app.Width;
            Height = _app.Height;
        }

        private void CreateStoreItems()
        {
            _storeItems[new NewPictureBox(this, "Utils/Images/computer.png", 128, 128, 180, 50)] = new NewButton(this, "Comprar", Color.LightGreen, 100, 30, 200, 180, null);
            _storeItems[new NewPictureBox(this, "Utils/Images/bicicleta.png", 128, 110, 180, 220)] = new NewButton(this, "Comprar", Color.LightGreen, 100, 30, 200, 330, null);
            _storeItems[new NewPictureBox(this, "Utils/Images/carro.png", 128, 90, 180, 390)] = new NewButton(this, "Comprar", Color.LightGreen, 100, 30, 200, 480, null);
        }
    }
}