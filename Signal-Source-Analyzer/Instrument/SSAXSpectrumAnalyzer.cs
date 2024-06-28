using OpenTap;
using OpenTap.Plugins.PNAX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Signal_Source_Analyzer
{
    public partial class SSAX : PNAX
    {
        public double GetSAPortAttenuation(int Channel, int port)
        {
            return ScpiQuery<double>($"SOURce{Channel}:POWer{port}:ATTenuation:RECeiver:TEST?");
        }

        public void SetSAPortAttenuation(int Channel, int port, double att)
        {
            ScpiCommand($"SOURce{Channel}:POWer{port}:ATTenuation:RECeiver:TEST {att}");
        }
    }
}
