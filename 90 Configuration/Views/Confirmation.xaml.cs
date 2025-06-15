using System.Windows;
using System.Windows.Input;

namespace MyInventory.Views
{
    public partial class Confirmation : Window
    {
        public string DialogTitle { get; set; } = "Confirm";
        public string Message { get; set; } = "Are you sure?";
        public string YesButtonText { get; set; } = "Yes";
        public string NoButtonText { get; set; } = "No";
        public bool Result { get; private set; }

        public Confirmation(string title, string message, string yesText = "Yes", string noText = "No")
        {
            DialogTitle = title;
            Message = message;
            YesButtonText = yesText;
            NoButtonText = noText;

            DataContext = this;
            InitializeComponent();
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            Result = true;
            Close();
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            Result = false;
            Close();
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }
    }
}
