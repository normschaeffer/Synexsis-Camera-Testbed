using System;
using System.Diagnostics;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Enkadia.Synexsis.ComponentFramework;
using Enkadia.Synexsis.ComponentFramework.Extensions;
using Enkadia.Synexsis.Components.Cameras.Vaddio;
using Microsoft.Extensions.DependencyInjection;


/// <summary>
/// Enkadia Synexsis Camera integration application
/// </summary>
namespace Synexsis_Camera_Testbed
{
    public sealed partial class MainPage : Page
    {
        //Add the Synexsis collection tools
        private IServiceCollection serviceCollection;
        private IServiceProvider serviceProvider;

        //Add the Roboshot camera, using the Roboshot Elite component
        public RoboshotElite camera;

        //Add camera speed variables, if desired
        private int TiltSpeed = 12;
        //private int PanSpeed = 9;
        private int ZoomSpeed = 8;

        //Create a preset array
        private readonly double[,] Preset =
        {
            {-21.06, 34, 2.59}, //Preset 1
            {-65.1, -19.79, 3.46}, //Preset 2
            { 50.33,13.75, 2.23} //Preset 3
        };
        
        public MainPage()
        {
            this.InitializeComponent();

            //Initialize the camera
            serviceCollection = new ServiceCollection();
            serviceCollection.AddSynexsis(); 
            serviceCollection.AddTransient<RoboshotElite>(); //set the device type to the Roboshot Elite module
            serviceProvider = serviceCollection.BuildServiceProvider();
            camera = serviceProvider.ResolveWith<RoboshotElite>("RoboshotCamera"); // parameter is name of section key in appsettings.json file

            //Call function to get connected to the camera
            CameraConnect();
        }

        private async void CameraConnect()
        {
            //return the camera state
            ComponentResponse standbyStatus = await camera.GetStandbyMode();

            //if camera is in standby, turn it on and update toggle button to reflect state
            if (standbyStatus.Response.Equals("On"))
            {
                await camera.StandbyOff();
                BtnStandby.IsChecked = false;
                BtnStandby.Content = "Standby Off";
            }

            //if application starts with camera standby off, update toggle button to reflect state
            if (standbyStatus.Response.Equals("Off"))
            {
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

        private async void BtnZoomIn_OnHolding(object sender, HoldingRoutedEventArgs e)
        {
            if (e.HoldingState == HoldingState.Started)
            {
                await camera.ZoomInAtSpeed(ZoomSpeed);
            }
            else
            {
                await camera.Stop();
            }
        }

        private async void BtnZoomOut_OnHolding(object sender, HoldingRoutedEventArgs e)
        {
            if (e.HoldingState == HoldingState.Started)
            {
                await camera.ZoomOutAtSpeed(ZoomSpeed);
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


        private void BtnPreset1_OnClick(object sender, RoutedEventArgs e)
        {
            camera.SetPanPosition(Preset[0, 0], 20);
            camera.SetTiltPosition(Preset[0, 1], 20);
            camera.SetZoomPosition(Preset[0, 2], 20);
        }

        private void BtnPreset2_OnClick(object sender, RoutedEventArgs e)
        {
            camera.SetPanPosition(Preset[1, 0], 20);
            camera.SetTiltPosition(Preset[1, 1], 20);
            camera.SetZoomPosition(Preset[1, 2], 20);
        }

        private void BtnPreset3_OnClick(object sender, RoutedEventArgs e)
        {
            camera.SetPanPosition(Preset[2, 0], 20);
            camera.SetTiltPosition(Preset[2, 1], 20);
            camera.SetZoomPosition(Preset[2, 2], 20);
        }
    }
}

