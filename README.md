# Signal Source Analyzer

## Overview

The Signal Source Analyzer Plugin is an OpenTAP plugin designed for seamless control of your Keysight SSA-X, automating tests through the test automation editor. This plugin streamlines the process of generating different measurements, saving results, all without the need for writing tedious and error-prone scripts.

## Features
**TODO: what features to call out? The following features are for the Network Analyzer plugin...**
* Create Test plan in PathWave Test Automation Editor

    The NA Plugin simplifies the creation process of complex test scenarios without the need for manual scripting. By using PathWave Test Automation Editor, you can design test steps, plans, set parameters, connect DUTs, set result output, define conditions with ease, accommodating users with varying levels of technical expertise.

* Test Parameterization

    Effortlessly parameterize test configurations within the PathWave Test Automation editor. This feature allows dynamic adjustment of test scenarios on the fly, enhancing flexibility and promoting comprehensive test coverage without manual intervention.

* Data Acquisition and Measurement Output

    The plugin offers robust data acquisition capabilities. You can capture, log, and save data from your network analyzer, enhancing your ability to monitor and optimize performance.

* Spectrum Analyzer (SA) Mode
  
    Utilize Spectrum Analyzer (SA) mode for advanced frequency-domain analysis. The NA Plugin seamlessly integrates with your network analyzer to provide comprehensive SA capabilities allowing users to perform detailed frequency measurements effortlessly.

* Calibration

    Perform calibration tasks through PathWave Test Automation Editor without the need for intricate scripting. The plugin supports calibration routines, ensuring the accuracy and reliability of your network analyzer measurements.

* Channel/Trace Setup

    Effortlessly configure channels and define traces/markers within PathWave Test Automation editor. Tailor your test scenarios by specifying the desired channels and traces, allowing for precise and targeted measurements.

## Getting Started


### Build from source (Windows with Visual Studio installed):
```
git clone https://github.com/opentap/Signal-Source-Analyzer.git
cd Signal-Source-Analyzer
start Signal-Source-Analyzer.sln
```

## List of Supported Instruments
All active Keysight Signal Source Analyzers with recent firmware. You can check the compatibility in the table below. The main testing environment for this plugin is the **TODO:**
| Family | Supported Instruments |
| :---:      | :----                 |
| SSA-X        | E5055A, E5056A, E5057A, E5058A |

## Support
For any questions, issues, or assistance, please submit an Issue in this repo.

## License
The Signal Source Analyzer Plugin is released under the MIT License. Feel free to use, modify, and distribute it in accordance with the terms of the license.

