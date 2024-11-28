# Duplicare 7 XrmToolBox Migration Tool

## Overview

The Duplicare 7 XrmToolBox Migration Tool facilitates the transition from the older Plugin Assembly method to the new dependent assemblies within the Power Platform. This migration tool automatically transfers all unmanaged steps created by Duplicare to the new package, ensuring a smooth and efficient update process.

## Key Features

- **Automatic Migration**: Move all unmanaged steps created by Duplicare from the older assembly to the new package automatically.
- **API Key Input**: Optionally input your API key, a new feature introduced in this version.

## Documentation Links

For more information on the changes and features regarding dependent assemblies, please refer to the official documentation on the modern way to make plugins:
- [Build and package plug-in code](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/build-and-package)

TLDR; moving from the .dll plugins to toe .nupkg plugins requires SDK message steps to be pointed to the new assembly.

## Usage

This tool must be run within XrmToolBox. If you have an active Duplicare license and require assistance during the migration process, please contact your account manager. A member of the Data8 team will be happy to help you at a time that suits you.

## Source Code

The source code for the Duplicare 7 XrmToolBox Migration Tool is publicly available. You can review the code to understand the migration process and make modifications if desired.

## How to use

If you would like to use our released version, please download the latest .dll file from the releases and place it in the "Plugins" folder, wherever your XrmToolBox tool is saved.

## Note

If you are online and have an existing Duplicare integration, you will be required to follow this migration process to ensure continued functionality.

---

For further inquiries or support, please reach out to the Data8 team or your account manager.
