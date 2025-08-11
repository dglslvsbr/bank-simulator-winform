using BankSimulator.Components;
using BankSimulator.Views;

namespace BankSimulator
{
    public partial class App : Form
    {
        public MainMenu NewMenuStrip = null!;
        public Dictionary<string, Panel> Screens = [];

        public App()
        {
            InitializeComponent();
            InitializeApp();
        }

        private void InitializeApp()
        {
            NewMenuStrip = new MainMenu(this);

            Screens["Initial"] = new Initial(this);
            Screens["Register"] = new Register(this);
            Screens["Recover"] = new Recover(this);
            Screens["Main"] = new Main(this);
            Screens["PersonData"] = new PersonData(this);

            Screens["Initial"].BringToFront();
        }
    }
}