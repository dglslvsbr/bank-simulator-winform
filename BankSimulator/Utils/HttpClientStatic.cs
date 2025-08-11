namespace BankSimulator.Utils
{
    static class HttpClientStatic
    {
        private static HttpClient? _httpClient;

        public static HttpClient HttpClient
        {
            get
            {
                return _httpClient ??= new()
                {
                    BaseAddress = new Uri("https://localhost:7104/api/")
                };
            }
        }
    }
}