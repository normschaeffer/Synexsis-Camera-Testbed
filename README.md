## Enkadia Synexsis Camera Test Platform - Windows UWP
A simple application to test cameras using the Synexsis camera module.

### Devices and software used for this project:
  * Raspberry Pi 3B (*__Windows Core IoT does not support Pi 3B+ or 4__*)
  * Raspberry Pi 7" Touch Display
  * Windows Core IoT
  * Vaddio Roboshot Elite 12 camera
  * Netgear 8 port switch
  
### Synexsis NuGet packages used - Available at the NuGet Repository (search Enkadia and check prerelease)
  * Enkadia.Synexsis.Components.Cameras
  * Enkadia.Synexsis.ComponentFramework
  
### Additional Microsoft NuGet packages used
  * Microsoft.Extensions.DependencyInjection;
  
### Windows Target environments
   * Minimum Windows 10, version 1803
   * Maximum Windows 10, version 1809
   
#### NOTE THIS TEST PLATFORM REQUIRES THE USE OF AN ENKADIA TEST LICENSE. TO REQUEST A LICENSE PLEASE SEE [enkadia.com](https://www.enkadia.com)
---
### Configuring your components
Synexsis builds your components by reading values from an `appsettings.json` file, located at the root of your program's runtime directory. Place your Synexsis Test License in the same folder.

```text
Place the appsettings.json file and license key in this folder.
This is an example for a release version running on a Raspberry Pi:

   ApplicationName\bin\ARM\Release\AppX

```
#### Troubleshooting
If the application fails to start, verify the license and appsettings.json files are in the correct folder.

---
### Creating the appsettings.json file
This sample appsettings file demonstrates the configuration information needed to support a Vaddio Roboshot camera.

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
