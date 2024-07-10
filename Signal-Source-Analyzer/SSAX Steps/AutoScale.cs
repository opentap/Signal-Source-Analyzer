// Author: MyName
// Copyright:   Copyright 2021 Keysight Technologies
//              You have a royalty-free right to use, modify, reproduce and distribute
//              the sample application files (and/or any modified version) in any way
//              you find useful, provided that you agree that Keysight Technologies has no
//              warranty, obligations or liability for any sample application files.
using OpenTap;
using OpenTap.Plugins.PNAX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Signal_Source_Analyzer
{
    [Display("Auto Scale", Groups: new[] { "Signal Source Analyzer" }, Description: "Triggers every channel")]
    public class AutoScale : OpenTap.Plugins.PNAX.AutoScale
    {
    }
}
