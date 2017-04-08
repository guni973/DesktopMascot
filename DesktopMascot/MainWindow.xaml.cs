using System;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

// getWeather
using System.Net;
using Newtonsoft.Json.Linq;



namespace DesktopMascot
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Quit_Clicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private string getLivedoorWeather()
        {
            string url = "http://weather.livedoor.com/forecast/webservice/json/v1";
            //東京都のID
            string cityname = "130010";

            string reqUrl = $"{url}?city={cityname}";
            var req = WebRequest.Create(reqUrl);
            var stream = req.GetResponse().GetResponseStream();
            var jsonSb = new StringBuilder();
            using (var sr = new StreamReader(stream))
            {
                jsonSb.Append(sr.ReadToEnd());
            }
            var jobj = JObject.Parse(jsonSb.ToString());
            File.WriteAllText(
                System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "tokyo_weather.txt"),
                jobj.ToString()
                );

            string todayWeather = (string)((jobj["forecasts"][0]["telop"] as JValue).Value);
            return "現在の東京の天気は" + todayWeather + "です。";
        }

    }

}
