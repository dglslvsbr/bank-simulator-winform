using System.IdentityModel.Tokens.Jwt;

namespace BankSimulator.Utils
{
    static class StoreToken
    {
        private static string _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BankSimulator");

        public static void Stored(string token)
        {
            try
            {
                if (!Directory.Exists(_path))
                    Directory.CreateDirectory(_path);

                string tokenPath = Path.Combine(_path, "Token.txt");

                using var ToStore = new StreamWriter(tokenPath);

                ToStore.Write(token);
            }
            catch (IOException)
            {
                ApplicationLog.RegisterLog("It was not possible to store token");
            }
        }

        public static string Get()
        {
            try
            {
                string tokenPath = Path.Combine(_path, "Token.txt");

                using var reading = File.OpenText(tokenPath);

                string token = null!;

                if (!reading.EndOfStream)
                    token = reading.ReadLine()!;

                if (token is not null)
                    return token!;

                ApplicationLog.RegisterLog("It was not possible to obtain access token.");
                return null!;
            }
            catch (IOException)
            {
                ApplicationLog.RegisterLog("An error occurred in obtain access token");
                return null!;
            }
        }

        public static int ExtractId()
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();

                var token = File.ReadAllText(Path.Combine(_path, "Token.txt"));

                var jwtToken = handler.ReadJwtToken(token);

                var id = jwtToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)!.Value;

                return int.Parse(id);
            }
            catch (Exception)
            {
                ApplicationLog.RegisterLog("It was not possible to extract the client token ID.");
                return default;
            }
        }
    }
}