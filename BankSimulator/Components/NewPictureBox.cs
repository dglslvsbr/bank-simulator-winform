namespace BankSimulator.Components
{
    internal class NewPictureBox : PictureBox
    {
        public NewPictureBox(Form form, string imagePath, int locationX, int locationY)
        {
            Image = Image.FromFile(imagePath);
            Location = new(locationX, locationY);

            form.Controls.Add(this);
        }

        public NewPictureBox(Panel panel, string imagePath, int width, int height, int locationX, int locationY)
        {
            Image = Image.FromFile(imagePath);
            Size = new(width, height);
            Location = new(locationX, locationY);

            panel.Controls.Add(this);
        }
    }
}