namespace BankSimulator.Components
{
    internal class NewLabel : Label
    {
        public NewLabel(Form form, string text, Color colorLabel, string font,
            int fontSize, FontStyle fontStyle, int width, int height, int locationX, int locationY)
        {
            Text = text;
            ForeColor = colorLabel;
            Font = new(font, fontSize, fontStyle);
            Size = new(width, height);
            Location = new(locationX, locationY);

            form.Controls.Add(this);
        }

        public NewLabel(Panel panel, string text, Color colorLabel, string font,
            int fontSize, FontStyle fontStyle, int width, int height, int locationX, int locationY)
        {
            Text = text;
            ForeColor = colorLabel;
            Font = new(font, fontSize, fontStyle);
            Size = new(width, height);
            Location = new(locationX, locationY);

            panel.Controls.Add(this);
        }
    }
}