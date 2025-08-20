using BankSimulator.Components;
using BankSimulator.Enums;
using BankSimulator.Models;

namespace BankSimulator.Views
{
    internal class Store : Panel
    {
        public readonly App _app = null!;
        private readonly Dictionary<string, NewItem> _storeItems = [];

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
            _storeItems["computer"] = new(this, "Utils/Images/computer.png", ProductEnum.Computer, 5000.00, 180, 20);
            _storeItems["bicicleta"] = new(this, "Utils/Images/bicicleta.png", ProductEnum.Bicicleta, 2500.00, 180, 200);
            _storeItems["carro"] = new(this, "Utils/Images/carro.png", ProductEnum.Carro, 15600.00, 180, 380);
        }
    }
}