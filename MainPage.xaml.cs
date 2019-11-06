using System;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Enkadia.Synexsis.Extensions;
using Enkadia.Synexsis.Components.Cameras.Vaddio;
using Microsoft.Extensions.DependencyInjection;

namespace Synexsis_Camera_Testbed
{
    public sealed partial class MainPage : Page
    {
        //Add the Synexsis collection tools
        private IServiceCollection serviceCollection;
        private IServiceProvider serviceProvider;

        //Add the Roboshot camera, using the Roboshot Elite
        public RoboshotElite camera;

        //Add camera speed variables, if desired
        private int TiltSpeed = 12;
        private int PanSpeed = 9;
        private int ZoomSpeed = 8;

        public MainPage()
        {
            this.InitializeComponent();

            //Initialize the camera
            serviceCollection = new ServiceCollection();
            serviceCollection.AddSynexsis();
            serviceCollection.AddTransient<RoboshotElite>();
            serviceProvider = serviceCollection.BuildServiceProvider();
            camera = serviceProvider.GetService<RoboshotElite>();

            //Call function to get connected to the camera
            CameraConnect();
        }

        private async void CameraConnect()
        {
            if (BtnStandby.IsChecked == true)
            {
                await camera.StandbyOff();
                BtnStandby.IsChecked = false;
                BtnStandby.Content = "Standby Off";
            }
        }

        private async void BtnTiltUp_Holding(object sender, HoldingRoutedEventArgs e)
        {
            if (e.HoldingState == HoldingState.Started)
            {
                await camera.TiltUpAtSpeed(TiltSpeed);
            }
            else
            {
                await camera.Stop();
            }
        }

        private async void BtnTiltDown_Holding(object sender, HoldingRoutedEventArgs e)
        {
            if (e.HoldingState == HoldingState.Started)
            {
                await camera.TiltDownAtSpeed(TiltSpeed);
            }
            else
            {
                await camera.Stop();
            }
        }

        private async void BtnPanLeft_Holding(object sender, HoldingRoutedEventArgs e)
        {
            if (e.HoldingState == HoldingState.Started)
            {
                await camera.PanLeft();
            }
            else
            {
                await camera.Stop();
            }
        }

        private async void BtnPanRight_Holding(object sender, HoldingRoutedEventArgs e)
        {
            if (e.HoldingState == HoldingState.Started)
            {
                await camera.PanRight();
            }
            else
            {
                await camera.Stop();
            }
        }

        private async void BtnHome_Click(object sender, RoutedEventArgs e) => await camera.Home();

        private async void BtnStandby_OnChecked(object sender, RoutedEventArgs e)
        {
            await camera.StandbyOn();
            BtnStandby.IsChecked = true;
            BtnStandby.Content = "Standby On";
        }

        private async void BtnStandby_OnUnchecked(object sender, RoutedEventArgs e)
        {
            await camera.StandbyOff();
            BtnStandby.IsChecked = false;
            BtnStandby.Content = "Standby Off";
        }



    }
}

