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
    [Display("Store Screen Shot", Groups: new[] { "Signal Source Analyzer", "Load/Measure/Store" }, Description: "Store screen shot")]
    public class StoreScreenShot : OpenTap.Plugins.PNAX.LMS.StoreScreenShot
    {
    }

}
