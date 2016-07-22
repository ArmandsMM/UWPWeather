using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPWeather
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string pair = "eth_usd";
            var position = await LocationManager.GetPosition();
            RootObject ticker = await OpenWeatherProxy.GetData(pair);
            resultTextBlock.Text = "Last price: " + ticker.ticker.last.ToString() + " and position is " + position.Coordinate.Latitude;


        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string pair = "eth_usd";
                var position = await LocationManager.GetPosition();
                RootObject ticker = await OpenWeatherProxy.GetData(pair);
                resultTextBlock.Text = "Last price: " + ticker.ticker.last.ToString() + " and position is " + position.Coordinate.Latitude;

                //Schedule update
                var uri = "https://btc-e.com/api/2/" + pair + "/ticker";
                var tileContent = new Uri(uri);
                var requestedInterval = PeriodicUpdateRecurrence.HalfHour;
                var updater = TileUpdateManager.CreateTileUpdaterForApplication();
                updater.StartPeriodicUpdate(tileContent, requestedInterval);
            }
            catch (Exception)
            {
                resultTextBlock.Text = "something went wrong";
            }

        }
    }
}
