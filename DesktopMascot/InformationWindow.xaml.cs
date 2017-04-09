using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DesktopMascot
{
    /// <summary>
    /// InformationWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class InformationWindow : Window
    {
        public InformationWindow()
        {
            InitializeComponent();
        }

        public void DisplayMessage(string message)
        {
            Dispatcher.Invoke(() => this.murmur.Text = message);
        }

        public void ClearMessage()
        {
            DisplayMessage(string.Empty);
        }

        public void DisplayWeather(string weather)
        {
            Dispatcher.Invoke(() => this.weather.Text = weather);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }
    }
}
