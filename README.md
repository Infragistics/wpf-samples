# Examples for Infragistics Ultimate UI for WPF Controls

This repository contains the Samples Browser and Showcase Applications for all of the Infragistics for WPF controls listed in the [Infragistics documentation here](https://www.infragistics.com/help/wpf/controls-components-and-frameworks).

The Infragistics for WPF Samples Browser exists in the "Examples" folder in this repository, while each of the showcases exist in the "Applications" folder. Below you will find the steps necessary to run each of these.

# Initial Setup

In order to run the showcases or samples browser, you will first need to clone this repository. You can do that with the following command:

```
git clone https://github.com/Infragistics/wpf-samples.git
```

# Running Samples Browser

The Infragistics for WPF Samples Browser is a standalone application that contains samples for all of the WPF controls that Infragistics exposes. It exists in the "Examples" folder in this repository. In order to run it, you can follow these steps:

- Open the Examples/Infragistics.Samples.WPF.sln in Visual Studio.
- Restore the NuGet packages that are referenced in each project. The Trial NuGet packages for the Infragistics for WPF product are currently in use for each of the projects in the browser. Visual Studio will try to restore the packages on build. The Trial packages exist on the public NuGet feed, and so you will need to target it in order to restore them. The public NuGet feed is https://api.nuget.org/v3/index.json.
- When the build finishes, you can start the browser by simply starting the solution from within Visual Studio. An .exe for the browser will also be placed within the created Examples/Output folder for future runs of the browser.

# Running Showcase Applications

Each of the Infragistics for WPF Showcase Applications are individually runnable. Each one is within the Applications folder of this repository. The following lists each of the projects:    

- `AirplaneSeatingChart`
- `EarthQuake`
- `EquityTrading`
- `FinanceDashboard`
- `GeographicMapBrowser`
- `HospitalFloorPlan`
- `IGExcel`
- `IGOutlook`
- `IGWord`
- `MediaTimeline`

Each of the above has a .sln folder within the corresponding folder that you need to open in Visual Studio in order to run the application. Like the Samples Browser mentioned above, you will need to restore the Trial NuGet packages for the Infragistics for WPF product in order to run.

There is an additional folder in the "Examples" directory named `IGExtensions`. This is not a runnable project, but rather a class library with resources that each of the showcase applications use.    