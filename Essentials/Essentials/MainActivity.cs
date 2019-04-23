using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Xamarin.Essentials;
using System.Linq;
using System;

namespace Essentials
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            #region 1# connectivity
            var internet = FindViewById<TextView>(Resource.Id.internet);

            var profiles = Connectivity.ConnectionProfiles;
            if (profiles.Contains(ConnectionProfile.WiFi))
            {
                // Active Wi-Fi connection.
            }

            // Check internet connection
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                internet.Text = "connected";
                // Connection to internet is available
            }
            else if (current != NetworkAccess.Internet)
            {
                internet.Text = "no internet connection";
                // Connection to internet is not available
            }
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            #endregion
            #region 2# battery
            var level = Battery.ChargeLevel;
            var state = Battery.State;
            var source = Battery.PowerSource;
            var status = Battery.EnergySaverStatus;


            var batteryState = FindViewById<TextView>(Resource.Id.batteryState);
            var batterySource = FindViewById<TextView>(Resource.Id.batterySource);

            switch (state)
            {
                case BatteryState.Charging:
                    batteryState.Text = "Charging";
                    break;

                case BatteryState.Full:
                    batteryState.Text = "Full";
                    break;

                case BatteryState.Discharging:
                case BatteryState.NotCharging:
                    batteryState.Text = "NotCharging";
                    break;

                case BatteryState.NotPresent:
                    batteryState.Text = "NotPresent";
                    break;

                case BatteryState.Unknown:
                    batteryState.Text = "Unknow";
                    break;
            }

            switch (source)
            {
                case BatteryPowerSource.Battery:
                    batterySource.Text = "powered by battery";
                    break;

                case BatteryPowerSource.AC:
                    batterySource.Text = "powered by A/C unit";
                    break;

                case BatteryPowerSource.Usb:
                    batterySource.Text = "powered by usb cable";
                    break;

                case BatteryPowerSource.Wireless:
                    batterySource.Text = "powered by wireless charging";
                    break;

                case BatteryPowerSource.Unknown:
                    batterySource.Text = "Unknow power source";
                    break;
            }

            Battery.BatteryInfoChanged += Battery_BatteryInfoChanged;
            Battery.EnergySaverStatusChanged += OnEnergySaverStatusChanged;
            #endregion
            #region 3# DeviceInfo
            var device = DeviceInfo.Model;
            var manufacturer = DeviceInfo.Manufacturer;
            var deviceName = DeviceInfo.Name;
            var platform = DeviceInfo.Platform;
            var idiom = DeviceInfo.Idiom;

            var Model = FindViewById<TextView>(Resource.Id.Model).Text = device;
            var ManuFacturer = FindViewById<TextView>(Resource.Id.Manufacturer).Text = manufacturer;
            var DeviceName = FindViewById<TextView>(Resource.Id.Name).Text = deviceName;
            var Platform = FindViewById<TextView>(Resource.Id.Platform).Text = platform.ToString();
            var Idiom = FindViewById<TextView>(Resource.Id.idiom).Text = idiom.ToString();
            #endregion
            #region 4# AppInfo 
            var appName = AppInfo.Name;
            var packageName = AppInfo.PackageName;
            var version = AppInfo.VersionString;
            var build = AppInfo.BuildString;

            var AppName = FindViewById<TextView>(Resource.Id.appName).Text = appName;
            var PackageName = FindViewById<TextView>(Resource.Id.packageName).Text = packageName;
            var Version = FindViewById<TextView>(Resource.Id.versionString).Text = version;
            var Build = FindViewById<TextView>(Resource.Id.buildString).Text = build;
            #endregion
            #region 5# ScreenInfo

            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var DisplayInfo = FindViewById<TextView>(Resource.Id.MainInfo).Text = mainDisplayInfo.ToString();

            DeviceDisplay.MainDisplayInfoChanged += OnMainDisplayInfoChanged;
            #endregion
        }

        void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs args)
        {
            var acces = args.NetworkAccess;
            var profiles = args.ConnectionProfiles;
        }

        void Battery_BatteryInfoChanged(object sender, BatteryInfoChangedEventArgs e)
        {
            var level = e.ChargeLevel;
            var state = e.State;
            var source = e.PowerSource; 
        }

        void OnEnergySaverStatusChanged(object sender, EnergySaverStatusChangedEventArgs e)
        {
            var status = e.EnergySaverStatus;
        }

        void OnMainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            var displayInfo = e.DisplayInfo;
        }

    }
}