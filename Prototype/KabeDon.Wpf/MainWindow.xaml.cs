using System.Windows;

namespace KabeDon.Wpf
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            CloudiaImage.MouseDown += CloudiaImage_MouseDown;
        }

        private void CloudiaImage_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var position = e.MouseDevice.GetPosition(CloudiaImage);
            MessageBox.Show($"クリック {position.X}, {position.Y}");
        }
    }
}
