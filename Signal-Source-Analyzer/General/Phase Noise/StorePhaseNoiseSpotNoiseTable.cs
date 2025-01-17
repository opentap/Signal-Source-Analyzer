using OpenTap;
using OpenTap.Plugins.PNAX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;

namespace Signal_Source_Analyzer
{
    [Display("Store Spot Noise Table", Groups: new[] { "Signal Source Analyzer", "General", "Phase Noise" }, Description: "Insert a description here")]
    public class StorePhaseNoiseSpotNoiseTable : TestStep
    {
        #region Settings
        [Display("PNA", null, null, 0.1, false, null)]
        public SSAX SSAX { get; set; }

        [Display("Channel", "Choose which channel to grab data from.", "Measurements", 10.1, false, null)]
        public int Channel { get; set; }

        [Display("MNum", Groups: new[] { "Trace" }, Order: 11)]
        public List<int> mnum { get; set; }

        #endregion

        public StorePhaseNoiseSpotNoiseTable()
        {
            Channel = 1;
            mnum = new List<int>() { 1 };
        }

        public override void Run()
        {
            UpgradeVerdict(Verdict.NotSet);

            RunChildSteps(); //If the step supports child steps.

            List<string> ResultNames = new List<string>();

            // Get all measurements for current channel
            mnum = SSAX.GetChannelMeasurements(Channel);

            foreach (int measurement in mnum)
            {
                List<IConvertible> ResultValues = new List<IConvertible>();

                // Trace
                ResultNames.Add("Trace");
                ResultValues.Add(measurement);

                // Find if Decades is selected
                bool decades = SSAX.GetPhaseNoise_DecadeEdges(Channel, measurement);

                if (decades)
                {
                    // Get values
                    List<double> XValues = SSAX.GetPhaseNoise_DecadeX(Channel, measurement);
                    List<double> YValues = SSAX.GetPhaseNoise_DecadeY(Channel, measurement);

                    // make sure X and Y are the same length
                    if (XValues.Count != YValues.Count)
                    {
                        UpgradeVerdict(Verdict.Fail);
                        return;
                    }

                    // for all values
                    for (int i = 0; i < XValues.Count; i++)
                    {
                        // publish values
                        ResultNames.Add(XValues[i].ToString());
                        ResultValues.Add(YValues[i]);
                    }
                }

                // for user 1-6
                for (int n = 1; n <= 6; n++)
                {
                    // Find if user n is selected
                    bool user = SSAX.GetPhaseNoise_SpotFrequencyEnabled(Channel, measurement, n);

                    if (user)
                    {
                        // Get values
                        double XValue = SSAX.GetPhaseNoise_SpotFrequency(Channel, measurement, n);
                        double YValue = SSAX.GetPhaseNoise_SpotFrequencyValue(Channel, measurement, n);

                        // publish values
                        ResultNames.Add(XValue.ToString());
                        ResultValues.Add(YValue);
                    }
                }

                // publish values
                Results.Publish($"PN_SpotNoise_Channel_{Channel.ToString()}", ResultNames, ResultValues.ToArray());
            }

            UpgradeVerdict(Verdict.Pass);
        }
    }
}
