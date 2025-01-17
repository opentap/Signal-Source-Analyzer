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
    [Display("Store Spurious Table", Groups: new[] { "Signal Source Analyzer", "General", "Phase Noise" }, Description: "Insert a description here")]
    public class StorePhaseNoiseSpuriousTable : TestStep
    {
        #region Settings
        [Display("PNA", null, null, 0.1, false, null)]
        public SSAX SSAX { get; set; }

        [Display("Channel", "Choose which channel to grab data from.", "Measurements", 10.1, false, null)]
        public int Channel { get; set; }

        [Display("MNum", Groups: new[] { "Trace" }, Order: 11)]
        public List<int> mnum { get; set; }

        #endregion

        public StorePhaseNoiseSpuriousTable()
        {
            Channel = 1;
            mnum = new List<int>() { 1 };
        }

        public override void Run()
        {
            UpgradeVerdict(Verdict.NotSet);

            RunChildSteps(); //If the step supports child steps.


            List<string> ResultNames = new List<string>();
            ResultNames.Add("Trace");
            ResultNames.Add("Spur");
            ResultNames.Add("Offset");
            ResultNames.Add("Power");
            ResultNames.Add("Jitter");

            // Get all measurements for current channel
            mnum = SSAX.GetChannelMeasurements(Channel);

            foreach (int measurement in mnum)
            {

                // Query Spurious table from instrument
                List<DetectedSpur> detectedSpurs = SSAX.GetPhaseNoise_SpuriousData(Channel, measurement);

                // if empty, no spurious
                if (detectedSpurs.Count == 0)
                {
                    List<IConvertible> ResultValues = new List<IConvertible>();
                    ResultValues.Add("");
                    ResultValues.Add("");
                    ResultValues.Add("");
                    ResultValues.Add("");
                    ResultValues.Add("");
                    Results.Publish($"PN_Spurious_Channel_{Channel.ToString()}", ResultNames, ResultValues.ToArray());
                }
                else
                // else, parse table and add to resultnmae and resultvalues
                {
                    int spurCounter = 0;
                    foreach (DetectedSpur spur in detectedSpurs)
                    {
                        List<IConvertible> ResultValues = new List<IConvertible>();
                        ResultValues.Add(measurement);
                        ResultValues.Add(spurCounter + 1);
                        ResultValues.Add(spur.Frequency);
                        ResultValues.Add(spur.Power);
                        ResultValues.Add(spur.Jitter);
                        spurCounter++;
                        Results.Publish($"PN_Spurious_Channel_{Channel.ToString()}", ResultNames, ResultValues.ToArray());
                    }
                }

            }

            UpgradeVerdict(Verdict.Pass);
        }
    }
}
