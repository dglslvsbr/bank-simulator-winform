namespace BankSimulator.Utils
{
    static class CodeGenerator
    {
        public static int Random()
        {
            string numbers = null!;

            Random random = new();

            for (int i = 0; i < 6; i++)
            {
                numbers += random.Next(1, 9);
            }

            if (!string.IsNullOrEmpty(numbers))
            {
                return int.Parse(numbers);
            }
            return default;
        }
    }
}