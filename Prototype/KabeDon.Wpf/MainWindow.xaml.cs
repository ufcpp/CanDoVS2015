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

        private Rect Hat = new Rect(200, 146, 176, 94);
        private Rect Face = new Rect(203, 233, 153, 137);
        private Rect Forbidden = new Rect(142, 413, 182, 547);

        private void CloudiaImage_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var position = e.MouseDevice.GetPosition(CloudiaImage);

            if (Forbidden.Contains(position))
            {
                System.Diagnostics.Debug.WriteLine("禁止領域");
            }
            else if (Face.Contains(position))
            {
                MessageBox.Show("顔");
            }
            else if (Hat.Contains(position))
            {
                MessageBox.Show("帽子");
            }
            else
            {
                MessageBox.Show($"他の場所クリック {position.X}, {position.Y}");
            }
        }
    }
}
