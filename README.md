## Enkadia Synexsis Camera Test Platform - Windows UWP
A simple application to test cameras using the Synexsis camera module.

### Devices and software used for this project:
  * Raspberry Pi 3B (*__Windows Core IoT does not support Pi 3B+ or 4__*)
  * Windows Core IoT
  * Vaddio Roboshot Elite 12 camera
  
### Synexsis NuGet packages used - Available at the NuGet Repository (search Enkadia and check prerelease)
  * Enkadia.Synexsis.Extensions
  * Enkadia.Synexsis.Components.Cameras
  * Enkadia.Synexsis.ComponentFramework
  
### Additional Microsoft NuGet packages used
  * Microsoft.Extensions.DependencyInjection;

### Configuring your components
Synexsis builds your components by reading values from an `appsettings.json` file, located at the root of your program's runtime directory. Place your Synexsis Test License in the same folder.

```text
Place the appsettings.json file and license key in this folder.
This is an example for a release version running on a Raspberry Pi:

   ApplicationName\bin\ARM\Release\AppX

```

#### Troubleshooting
If the application fails to start, verify the license and appsettings.json files are in the correct folder.

### Creating the appsettings.json file
This appsettings file demonstrates the configuration information needed to support a Vaddio Roboshot camera.

#### Sample appsettings.json
```json
{
    "RoboshotCamera": {
		"IPAddress": "192.168.1.200",
		"Port": 23,
		"Username": "admin",
    "Password": "password"
	},
	"License": {
		"OfflineActivation": "true",
		"LicenseFileName": "MyLicense.skm"
	}
}
```
