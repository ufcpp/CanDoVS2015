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

        private Rect Face = new Rect(203, 233, 153, 137);

        private void CloudiaImage_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var position = e.MouseDevice.GetPosition(CloudiaImage);

            if (Face.Contains(position))
            {
                MessageBox.Show($"顔");
            }
            else
            {
                MessageBox.Show($"他の場所クリック {position.X}, {position.Y}");
            }
        }
    }
}
