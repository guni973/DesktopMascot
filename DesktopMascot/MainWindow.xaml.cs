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
using System.ComponentModel;
using System.Windows.Threading;

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

            informationWindow = new InformationWindow();
            informationWindow.Show();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5.0);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        DispatcherTimer timer;
        InformationWindow informationWindow;

        int count = 0;
        void timer_Tick(object sender, EventArgs e)
        {
            count++;
            if (count == 1)
            {
                informationWindow.DisplayMessage("ゆっくりしていってね！");
            }
            else
            {
                count = 0;
                informationWindow.DisplayMessage("ゆっくりした結果がこれだよ！");
            }
        }

        void WeatherInformation()
        {
            informationWindow.DisplayWeather(getLivedoorWeather());
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (informationWindow != null)
            {
                informationWindow.Close();
            }
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

            string Weather = (string)((jobj["forecasts"][0]["telop"] as JValue).Value);
            string dateLabel = (string)((jobj["forecasts"][0]["dateLabel"] as JValue).Value);
            string location = (string)((jobj["location"]["pref"] as JValue).Value);

            return $"{dateLabel}の{location}の天気は{Weather}です。";
        }

    }

}
