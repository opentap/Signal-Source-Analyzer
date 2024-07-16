using OpenTap;
using OpenTap.Plugins.PNAX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signal_Source_Analyzer
{

    [AllowAsChildIn(typeof(GeneralPhaseNoiseSingleTrace))]
    [Display("Spot Noise", Groups: new[] { "Signal Source Analyzer", "General", "Phase Noise" }, Description: "Insert a description here")]
    public class GeneralPhaseNoiseSpotNoise : SSAXBaseStep
    {
        #region Settings
        [Browsable(false)]
        public int mnum { get; set; }

        [Browsable(false)]
        public int wnum { get; set; }


        [Display("Show Spot Noise Table", Group: "Settings", Order: 20.01, Description: "Enable or disable displaying the spot noise table.")]
        public bool ShowSpotNoiseTable { get; set; }

        [Display("Spot Frequency", Group: "Settings", Order: 20.02, Description: "Sets and returns the offset frequency on which the spot noise is calculated. Up to six offset frequencies can be defined.")]
        [Unit("Hz", UseEngineeringPrefix: true, StringFormat: "")]
        public double SpotFrequency { get; set; }

        [Display("Spot Frequency Enabled", Group: "Settings", Order: 20.03, Description: "Enables and disables spot noise calculation for the specified user-defined offset frequency.")]
        public bool SpotFrequencyEnabled { get; set; }

        [Display("Decade Edges", Group: "Settings", Order: 20.04, Description: "Enables and disables the spot noise calculation on every decade offset frequency.")]
        public bool DecadeEdges { get; set; }
        #endregion


        public GeneralPhaseNoiseSpotNoise()
        {
            ShowSpotNoiseTable = false;
            SpotFrequency = 10;
            SpotFrequencyEnabled = true;
            DecadeEdges = true;
        }

        public override void Run()
        {
            mnum = GetParent<SingleTraceBaseStep>().mnum;
            wnum = GetParent<SingleTraceBaseStep>().Window;
            int user = 1;

            RunChildSteps(); //If the step supports child steps.

            SSAX.SetPhaseNoise_ShowSpotNoiseTable(wnum, ShowSpotNoiseTable);
            SSAX.SetPhaseNoise_SpotFrequency(Channel, mnum, user, SpotFrequency);
            SSAX.SetPhaseNoise_SpotFrequencyEnabled(Channel, mnum, user, SpotFrequencyEnabled);
            SSAX.SetPhaseNoise_DecadeEdges(Channel, mnum, DecadeEdges);



            UpgradeVerdict(Verdict.Pass);
        }

        
    }
}
