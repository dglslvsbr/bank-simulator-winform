namespace BankSimulator.Components
{
    internal class NewButton : Button
    {
        public NewButton(App app, string? buttonName, Color color,
            int width, int height, int locationX, int locationY, Image? image)
        {
            Text = buttonName;
            BackColor = color;
            Size = new(width, height);
            Location = new(locationX, locationY);
            Image = image;
            FlatStyle = FlatStyle.Flat;

            app.Controls.Add(this);
        }

        public NewButton(Panel panel, string? buttonName, Color color,
            int width, int height, int locationX, int locationY, Image? image)
        {
            Text = buttonName;
            BackColor = color;
            Size = new(width, height);
            Location = new(locationX, locationY);
            Image = image;
            FlatStyle = FlatStyle.Flat;

            panel.Controls.Add(this);
        }
    }
}
