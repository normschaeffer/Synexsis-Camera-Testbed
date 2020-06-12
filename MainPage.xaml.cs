using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Enkadia.Synexsis.ComponentFramework.Extensions;
using Enkadia.Synexsis.Components.Cameras.Vaddio;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Synexsis_Camera_Testbed
{

    public sealed partial class MainPage
    {
        #region Declarations - includes a three-item array of presets for testing; add'l development needed for preset list

        public string CameraPositionInformation { get; set; }

        //Add the Roboshot camera, using the Roboshot Elite component
        public RoboshotElite Camera;

        //Add camera speed variables, if desired
        private int TiltSpeed = 12;
        private int PanSpeed = 15;
        private int ZoomSpeed = 8;

        //TODO: create a real preset list or pull presets from database
        private List<(int CameraPreset, double XPosition, int PanSpeed, double YPosition, int TiltSpeed, double ZPosition, int ZoomSpeed)> PresetList { get; set; }

        //Create a three item, two-dimensional preset array that contains the PTZ information
        private readonly double[,] _preset =
        {
            {-21.06, 34, 2.59}, //Preset 1
            {-65.1, -19.79, 3.46}, //Preset 2
            { 50.33, 13.75, 2.23} //Preset 3
        };

        private int cameraPreset { get; set; }
        private double PanPosition { get; set; }
        private double TiltPosition { get; set; }
        private double ZoomPosition { get; set; }

        #endregion 
        
        public MainPage()
        {
            this.InitializeComponent();

            //Initialize the camera
            IServiceCollection serviceCollection = new ServiceCollection(); //initialize the Synexsis tools
            serviceCollection.AddSynexsis(); 
            serviceCollection.AddTransient<RoboshotElite>(); //set the device type to the Roboshot Elite module
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            Camera = serviceProvider.ResolveWith<RoboshotElite>("RoboshotCamera"); // parameter is name of section key in appsettings.json file

            //Call function to get connected to the camera
            CameraConnect();
        }

        #region Camera Connect method
        private async void CameraConnect()
        {
            //return the camera state
            var standbyStatus = await Camera.GetStandbyMode();

            //if camera is in standby, turn it on and update toggle button to reflect state
            if (standbyStatus.Response.Equals("On"))
            {
                await Camera.StandbyOff();
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

        #endregion

        #region Tilt Functions - Hold and release on touch screens. For non-touch screens click and hold right mouse button to start, release to stop
        private async void BtnTiltUp_Holding(object sender, HoldingRoutedEventArgs e)
        {
            if (e.HoldingState == HoldingState.Started)
            {
                await Camera.TiltUpAtSpeed(TiltSpeed);
            }
            else
            {
                await Camera.Stop();
            }
        }

        private async void BtnTiltDown_Holding(object sender, HoldingRoutedEventArgs e)
        {
            if (e.HoldingState == HoldingState.Started)
            {
                await Camera.TiltDownAtSpeed(TiltSpeed);
            }
            else
            {
                await Camera.Stop();
            }
        }

        private async void BtnTiltUp_OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            await Camera.TiltUpAtSpeed(TiltSpeed);
        }

        private async void BtnTiltUp_OnPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            await Camera.Stop();
        }

        private async void BtnTiltDown_OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            await Camera.TiltDownAtSpeed(TiltSpeed);
        }

        private async void BtnTiltDown_OnPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            await Camera.Stop();
        }

        #endregion

        #region Pan Functions - Hold and release on touch screens. For non-touch screens click and hold right mouse button to start, release to stop

        private async void BtnPanLeft_Holding(object sender, HoldingRoutedEventArgs e)
        {
            if (e.HoldingState == HoldingState.Started)
            {
                await Camera.PanLeftAtSpeed(PanSpeed);
            }
            else
            {
                await Camera.Stop();
            }
        }

        private async void BtnPanRight_Holding(object sender, HoldingRoutedEventArgs e)
        {
            if (e.HoldingState == HoldingState.Started)
            {
                await Camera.PanRightAtSpeed(PanSpeed);
            }
            else
            {
                await Camera.Stop();
            }
        }

        private async void BtnPanLeft_OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            await Camera.PanLeftAtSpeed(PanSpeed);
        }

        private async void BtnPanLeft_OnPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            await Camera.Stop();
        }
        private async void BtnPanRight_OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            await Camera.PanRightAtSpeed(PanSpeed);
        }

        private async void BtnPanRight_OnPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            await Camera.Stop();
        }

        #endregion

        #region Zoom functions - Hold and release on touch screens. For non-touch screens click and hold right mouse button to start, release to stop

        private async void BtnZoomIn_OnHolding(object sender, HoldingRoutedEventArgs e)
        {
            if (e.HoldingState == HoldingState.Started)
            {
                await Camera.ZoomInAtSpeed(ZoomSpeed);
            }
            else
            {
                await Camera.Stop();
            }
        }

        private async void BtnZoomOut_OnHolding(object sender, HoldingRoutedEventArgs e)
        {
            if (e.HoldingState == HoldingState.Started)
            {
                await Camera.ZoomOutAtSpeed(ZoomSpeed);
            }
            else
            {
                await Camera.Stop();
            }
        }

        private async void BtnZoomIn_OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            await Camera.ZoomInAtSpeed(ZoomSpeed);
        }

        private async void BtnZoomIn_OnPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            await Camera.Stop();
        }

        private async void BtnZoomOut_OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            await Camera.ZoomInAtSpeed(ZoomSpeed);
        }
        private async void BtnZoomOut_OnPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            await Camera.Stop();
        }

        #endregion

        #region Home and Standby buttons
        private async void BtnHome_Click(object sender, RoutedEventArgs e) => await Camera.Home();

        private async void BtnStandby_OnChecked(object sender, RoutedEventArgs e)
        {
            await Camera.StandbyOn();
            BtnStandby.IsChecked = true;
            BtnStandby.Content = "Standby On";
        }

        private async void BtnStandby_OnUnchecked(object sender, RoutedEventArgs e)
        {
            await Camera.StandbyOff();
            BtnStandby.IsChecked = false;
            BtnStandby.Content = "Standby Off";
        }
        #endregion

        #region Go to Preset buttons
        
        private void BtnPreset1_OnClick(object sender, RoutedEventArgs e)
        {
            Camera.SetPanPosition(_preset[0, 0], 20);
            Camera.SetTiltPosition(_preset[0, 1], 20);
            Camera.SetZoomPosition(_preset[0, 2], 20);
        }

        private void BtnPreset2_OnClick(object sender, RoutedEventArgs e)
        {
            Camera.SetPanPosition(_preset[1, 0], 20);
            Camera.SetTiltPosition(_preset[1, 1], 20);
            Camera.SetZoomPosition(_preset[1, 2], 20);
        }

        private void BtnPreset3_OnClick(object sender, RoutedEventArgs e)
        {
            Camera.SetPanPosition(_preset[2, 0], 20);
            Camera.SetTiltPosition(_preset[2, 1], 20);
            Camera.SetZoomPosition(_preset[2, 2], 20);
        }

        private async void BtnGetCameraPosition_OnClick(object sender, RoutedEventArgs e)
        {
            var response = await GetCameraPreset();
            TxtBlkCameraPosition.Text = response;
        }

        #endregion

        #region AddCameraPreset - undeveloped

        //TODO: write AddCameraPreset function
        private void AddCameraPreset(int cameraPreset, double xPosition, int panSpeed, double yPosition,
            int tiltSpeed, double zPosition, int zoomSpeed)
        {
 
        }
        private void BtnSetCameraPosition_OnClick(object sender, RoutedEventArgs e)
        {
            AddCameraPreset(1, PanPosition, 20, TiltPosition, 15, ZoomPosition, 10);
        }

        #endregion

        #region GetCameraPreset - display position information to user
        public async Task<string> GetCameraPreset()
        {
            try
            {
                var sb = new StringBuilder();

                var pan = await Camera.GetPanPosition();
                PanPosition = Convert.ToDouble(pan.Response);

                var tilt = await Camera.GetTiltPosition();
                TiltPosition = Convert.ToDouble(tilt.Response);

                var zoom = await Camera.GetZoomPosition();
                ZoomPosition = Convert.ToDouble(zoom.Response);

                var focus = await Camera.GetFocusMode();
                var focusMode = focus.Response;

                sb.Append("P: ");
                sb.Append(PanPosition);
                sb.Append(", ");
                sb.Append("T: ");
                sb.Append(TiltPosition);
                sb.Append(", ");
                sb.Append("Z: ");
                sb.Append(ZoomPosition);
                sb.Append(", ");
                sb.Append("F: ");
                sb.Append(focusMode);
                CameraPositionInformation = sb.ToString();
                
                return CameraPositionInformation;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        #endregion

        #region Exit button - hidden - only used for testing and convenience - delete for production
      
        private async void BtnExit_OnClick(object sender, RoutedEventArgs e) //to use as exit or restart, make button visible in XAML and uncomment the click event
        {
            /*
            //CoreApplication.Exit();

            MessageDialog message = new MessageDialog("System Restarting") {Title = "Touch Panel Restarting"};

            Thread.Sleep(2000);

            AppRestartFailureReason result = await CoreApplication.RequestRestartAsync("System restarted");

            if (result == AppRestartFailureReason.NotInForeground || result == AppRestartFailureReason.Other)
            {
                //MessageDialog message = new MessageDialog("System Restarting");
                //message.Title = "Touch Panel Restarting";
            }
            */
        }
        #endregion

    }
}

