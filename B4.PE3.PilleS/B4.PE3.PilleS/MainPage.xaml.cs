using B4.PE3.PilleS.Domain.Models;
using B4.PE3.PilleS.ViewModels;
using B4.PE3.PilleS.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace B4.PE3.PilleS
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnbtnTrackingClicked(object sender, EventArgs e)
        {
            MainContent.Content = new ContentView()
            {
                Content = new TrackingContentView()
            };
            btnTracking.IsEnabled = false;
            btnPosition.IsEnabled = true;
            btnMap.IsEnabled = true;
        }

        private void OnbtnMapClicked(object sender, EventArgs e)
        {
            MainContent.Content = new ContentView()
            {
                Content = new MapContentView()
            };
            btnMap.IsEnabled = false;
            btnPosition.IsEnabled = true;
            btnTracking.IsEnabled = true;
        }

        private void OnbtnPositionClicked(object sender, EventArgs e)
        {
            MainContent.Content = new ContentView()
            {
                Content = new PositionContentView()
            };
            btnPosition.IsEnabled = false;
            btnMap.IsEnabled = true;
            btnTracking.IsEnabled = true;
        }

    }
}
