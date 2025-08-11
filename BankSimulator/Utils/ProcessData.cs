using BankSimulator.Components;
using BankSimulator.DTOs;

namespace BankSimulator.Utils
{
    static class ProcessData
    {   
        public static bool CheckLoginData(LoginDTO loginDto)
        {
            if (string.IsNullOrWhiteSpace(loginDto.Email) ||
                string.IsNullOrWhiteSpace(loginDto.Password) ||
                !loginDto.Email.Contains('@'))
                return false;

            return true;
        }

        public static bool CheckRegisterData(Dictionary<string, NewTextBox> txtBoxes)
        {
            int count = 0;

            foreach (var obj in txtBoxes)
            {
                if (string.IsNullOrWhiteSpace(obj.Value.Text))
                {
                    obj.Value.BackColor = Color.LightPink;
                    count += 1;
                }
            }

            if (count == 0)
                return true;
            else
                return false;
        }

        public static bool CheckCode(string code)
        {
            bool isValid = int.TryParse(code, out _);
            return isValid;
        }
    }
}