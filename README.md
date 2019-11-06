## Enkadia Synexsis Camera Test Platform - Windows UWP
This repository contains an simple application to test cameras using the Synexsis camera module.

### Devices used:
  * Raspberry Pi 3B (Windows Core IoT does not support Pi 3B+ or 4)
  * Windows Core IoT
  * Vaddio Roboshot Elite 12 camera

### Configuring your components
Synexsis builds your components by reading values from an `appsettings.json` file, located at the root of your program's runtime directory. Place your Synexsis Test License in the same folder.

```text
Place the appsettings.json file and license key in this folder.
This is an example for a release version running on a Raspberry Pi:

   ApplicationName\bin\ARM\Release\AppX

```

This example demonstrates registering a projector which supports Vaddio Roboshot Cameras.

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
