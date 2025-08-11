namespace BankSimulator.Views
{
    internal class Main : Panel
    {
        private readonly App _app;

        public Main(App app)
        {
            _app = app;
            _app.Controls.Add(this);

            InitializeComponents();
        }

        private void InitializeComponents()
        {
            ConfigurePanel();
        }

        private void ConfigurePanel()
        {
            Width = _app.Width;
            Height = _app.Height;
        }
    }
}