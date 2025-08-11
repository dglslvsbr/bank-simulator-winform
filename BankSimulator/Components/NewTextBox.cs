namespace BankSimulator.Components;

internal class NewTextBox : TextBox
{
    public NewTextBox(Form form, string textLabel, int locationX, int locationY)
    {
        BorderStyle = BorderStyle.FixedSingle;
        Size = new(200, 0);
        Location = new(locationX, locationY);
        MaxLength = 30;

        NewLabel label = new(form, textLabel, Color.White, "Calibri",
            10, FontStyle.Regular, locationX, 15, Location.X + 75, Location.Y + 20);
        {
            ForeColor = Color.Black;
        };

        form.Controls.Add(this);
        form.Controls.Add(label);
    }

    public NewTextBox(Panel panel, string textLabel, int locationX, int locationY)
    {
        BorderStyle = BorderStyle.FixedSingle;
        Size = new(200, 0);
        Location = new(locationX, locationY);
        MaxLength = 30;

        NewLabel label = new(panel, textLabel, Color.White, "Calibri",
            10, FontStyle.Regular, locationX, 15, Location.X + 75, Location.Y - 20)
        {
           ForeColor = Color.Black
        };

        panel.Controls.Add(this);
        panel.Controls.Add(label);
    }
}