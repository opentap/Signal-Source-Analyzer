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
    public class SpotFrequencies
    {
        [Display("Spot Frequency Enabled", Group: "Settings", Order: 1, Description: "Enables and disables spot noise calculation for the specified user-defined offset frequency.")]
        public bool SpotFrequencyEnabled { get; set; }

        [Display("Spot Frequency", Group: "Settings", Order: 2, Description: "Sets and returns the offset frequency on which the spot noise is calculated. Up to six offset frequencies can be defined.")]
        [Unit("Hz", UseEngineeringPrefix: true, StringFormat: "")]
        public double SpotFrequency { get; set; }


        public SpotFrequencies()
        {
            SpotFrequency = 100;
            SpotFrequencyEnabled = false;
        }
    }

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

        [Display("Enable Spot Noise", Group: "Settings", Order: 20.02, Description: "Enables and disables spot noise calculation for the selected measurement number.")]
        public bool EnableSpotNoise { get; set; }

        [Display("Decade Edges", Group: "Settings", Order: 20.04, Description: "Enables and disables the spot noise calculation on every decade offset frequency.")]
        public bool DecadeEdges { get; set; }

        [EnabledIf("DecadeEdges", false, HideIfDisabled = true)]
        [Display("", Group: "Define Spot Frequencies", Order: 30)]
        [EmbedProperties(PrefixPropertyName = true)]
        public SpotFrequencies SpotFrequenciesUser1 { get; set; } = new SpotFrequencies();

        [EnabledIf("DecadeEdges", false, HideIfDisabled = true)]
        [Display("", Group: "Define Spot Frequencies", Order: 40)]
        [EmbedProperties(PrefixPropertyName = true)]
        public SpotFrequencies SpotFrequenciesUser2 { get; set; } = new SpotFrequencies();

        [EnabledIf("DecadeEdges", false, HideIfDisabled = true)]
        [Display("", Group: "Define Spot Frequencies", Order: 50)]
        [EmbedProperties(PrefixPropertyName = true)]
        public SpotFrequencies SpotFrequenciesUser3 { get; set; } = new SpotFrequencies();

        [EnabledIf("DecadeEdges", false, HideIfDisabled = true)]
        [Display("", Group: "Define Spot Frequencies", Order: 60)]
        [EmbedProperties(PrefixPropertyName = true)]
        public SpotFrequencies SpotFrequenciesUser4 { get; set; } = new SpotFrequencies();

        [EnabledIf("DecadeEdges", false, HideIfDisabled = true)]
        [Display("", Group: "Define Spot Frequencies", Order: 70)]
        [EmbedProperties(PrefixPropertyName = true)]
        public SpotFrequencies SpotFrequenciesUser5 { get; set; } = new SpotFrequencies();

        [EnabledIf("DecadeEdges", false, HideIfDisabled = true)]
        [Display("", Group: "Define Spot Frequencies", Order: 80)]
        [EmbedProperties(PrefixPropertyName = true)]
        public SpotFrequencies SpotFrequenciesUser6 { get; set; } = new SpotFrequencies();
        #endregion


        public GeneralPhaseNoiseSpotNoise()
        {
            ShowSpotNoiseTable = false;
            EnableSpotNoise = false;
            DecadeEdges = true;

            SpotFrequenciesUser1 = new SpotFrequencies();
            SpotFrequenciesUser1.SpotFrequency = 100;
            SpotFrequenciesUser2 = new SpotFrequencies();
            SpotFrequenciesUser2.SpotFrequency = 1e3;
            SpotFrequenciesUser3 = new SpotFrequencies();
            SpotFrequenciesUser3.SpotFrequency = 10e3;
            SpotFrequenciesUser4 = new SpotFrequencies();
            SpotFrequenciesUser4.SpotFrequency = 100e3;
            SpotFrequenciesUser5 = new SpotFrequencies();
            SpotFrequenciesUser5.SpotFrequency = 1e6;
            SpotFrequenciesUser6 = new SpotFrequencies();
            SpotFrequenciesUser6.SpotFrequency = 10e6;
        }

        public override void Run()
        {
            mnum = GetParent<SingleTraceBaseStep>().mnum;
            wnum = GetParent<SingleTraceBaseStep>().Window;

            RunChildSteps(); //If the step supports child steps.

            SSAX.SetPhaseNoise_ShowSpotNoiseTable(wnum, ShowSpotNoiseTable);
            SSAX.SetPhaseNoise_SpotNoiseEnable(Channel, mnum, EnableSpotNoise);
            SSAX.SetPhaseNoise_DecadeEdges(Channel, mnum, DecadeEdges);
            
            SSAX.SetPhaseNoise_SpotFrequencyEnabled(Channel, mnum, 1, SpotFrequenciesUser1.SpotFrequencyEnabled);
            if (SpotFrequenciesUser1.SpotFrequencyEnabled)
            {
                SSAX.SetPhaseNoise_SpotFrequency(Channel, mnum, 1, SpotFrequenciesUser1.SpotFrequency);
            }

            SSAX.SetPhaseNoise_SpotFrequencyEnabled(Channel, mnum, 2, SpotFrequenciesUser2.SpotFrequencyEnabled);
            if (SpotFrequenciesUser2.SpotFrequencyEnabled)
            {
                SSAX.SetPhaseNoise_SpotFrequency(Channel, mnum, 2, SpotFrequenciesUser2.SpotFrequency);
            }

            SSAX.SetPhaseNoise_SpotFrequencyEnabled(Channel, mnum, 3, SpotFrequenciesUser3.SpotFrequencyEnabled);
            if (SpotFrequenciesUser3.SpotFrequencyEnabled)
            {
                SSAX.SetPhaseNoise_SpotFrequency(Channel, mnum, 3, SpotFrequenciesUser3.SpotFrequency);
            }

            SSAX.SetPhaseNoise_SpotFrequencyEnabled(Channel, mnum, 4, SpotFrequenciesUser4.SpotFrequencyEnabled);
            if (SpotFrequenciesUser4.SpotFrequencyEnabled)
            {
                SSAX.SetPhaseNoise_SpotFrequency(Channel, mnum, 4, SpotFrequenciesUser4.SpotFrequency);
            }

            SSAX.SetPhaseNoise_SpotFrequencyEnabled(Channel, mnum, 5, SpotFrequenciesUser5.SpotFrequencyEnabled);
            if (SpotFrequenciesUser5.SpotFrequencyEnabled)
            {
                SSAX.SetPhaseNoise_SpotFrequency(Channel, mnum, 5, SpotFrequenciesUser5.SpotFrequency);
            }

            SSAX.SetPhaseNoise_SpotFrequencyEnabled(Channel, mnum, 6, SpotFrequenciesUser6.SpotFrequencyEnabled);
            if (SpotFrequenciesUser6.SpotFrequencyEnabled)
            {
                SSAX.SetPhaseNoise_SpotFrequency(Channel, mnum, 6, SpotFrequenciesUser6.SpotFrequency);
            }

            UpgradeVerdict(Verdict.Pass);
        }

        
    }
}
