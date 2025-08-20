using BankSimulator.Enums;
using BankSimulator.Views;

namespace BankSimulator.Components
{
    internal class NewItem : Panel
    {
        private readonly Store _store;
        private NewPictureBox _image = null!;

        private readonly int _locationX;
        private readonly int _locationY;
        private readonly ProductEnum _productName;
        private readonly double _itemPrice;

        public NewItem(Store store, string imagePath, ProductEnum productName, double itemPrice, int locationX, int locationY)
        {
            _store = store;
            _store.Controls.Add(this);
            _locationX = locationX;
            _locationY = locationY;
            _productName = productName;
            _itemPrice = itemPrice;

            ConfigurePanel();
            ImageItem(imagePath);
            ButtonItem();
        }

        private void ConfigurePanel()
        {
            Height = 200;
            Location = new(_locationX, _locationY);
        }

        private void ImageItem(string imagePath)
        {
            _image = new NewPictureBox(this, imagePath, 128, 128, 0, 0);
            Controls.Add(_image);

            Label label = new()
            {
                Text = "$ " + _itemPrice.ToString(),
                Font = new("Arial", 15, FontStyle.Bold),
                Location = new(_locationX - 100, _locationY + 50),
                Size = new(100, 20)
            };
            _store.Controls.Add(label);
            label.BringToFront();
        }

        private void ButtonItem()
        {
            var button = new NewButton(this, "Buy", Color.LightGreen, 100, 30, _image.Location.X + 15, _image.Location.Y + 130, null);
            Controls.Add(button);

            button.Click += (s, e) =>
            {
                _store._app.Screens["Payment"] = new Payment(_store._app, _productName, _itemPrice);
                _store._app.Screens["Payment"].BringToFront();
            };
        }
    }
}