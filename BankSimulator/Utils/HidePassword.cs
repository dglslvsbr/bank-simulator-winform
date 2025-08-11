using BankSimulator.Components;

namespace BankSimulator.Utils
{
    class HidePassword
    {
        public static void Change(NewTextBox newTextBox)
        {
            if (newTextBox.PasswordChar == '\0')
            {
                newTextBox.PasswordChar = '*';
            }
            else
            {
                newTextBox.PasswordChar = '\0';
            }
        }
    }
}