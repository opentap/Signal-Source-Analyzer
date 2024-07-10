using OpenTap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.IO;
using OpenTap.Plugins.PNAX;

namespace Signal_Source_Analyzer
{
    [Display("Store Trace Data", Groups: new[] { "Signal Source Analyzer", "Load/Measure/Store" }, Description: "Stores trace data from all channels.")]
    public class StoreData : OpenTap.Plugins.PNAX.StoreData
    {
    }
}
