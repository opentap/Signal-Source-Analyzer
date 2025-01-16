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
    public class IntegratedNoiseTable
    {
        [Display("Integrated Range Type", Group: "Trace", Order: 30.01, Description: "Sets and returns the integration range type of the selected integration range. RANGe[1-4] allows selection of the desired integration range.")]
        public PhaseNoise_IntegratedRangeTypeEnum IntegratedRangeType { get; set; }

        [EnabledIf("IntegratedRangeType", PhaseNoise_IntegratedRangeTypeEnum.CUSTom, PhaseNoise_IntegratedRangeTypeEnum.FULL, HideIfDisabled = true)]
        [Display("Start", Group: "Trace", Order: 30.02, Description: "Sets and returns the start frequency of the selected integration range. RANGe[1-4] allows selection of the desired integration range. The CALCulate:MEASure:PN:INTegral:RANGe:TYPE command must be set to CUSTom.")]
        [Unit("Hz", UseEngineeringPrefix: true, StringFormat: "")]
        public double Start { get; set; }

        [EnabledIf("IntegratedRangeType", PhaseNoise_IntegratedRangeTypeEnum.CUSTom, PhaseNoise_IntegratedRangeTypeEnum.FULL, HideIfDisabled = true)]
        [Display("Stop", Group: "Trace", Order: 30.03, Description: "Sets and returns the stop frequency of the selected integration range. RANGe[1-4] allows selection of the desired integration range. The CALCulate:MEASure:PN:INTegral:RANGe:TYPE command must be set to CUSTom.")]
        [Unit("Hz", UseEngineeringPrefix: true, StringFormat: "")]
        public double Stop { get; set; }

        [EnabledIf("IntegratedRangeType", PhaseNoise_IntegratedRangeTypeEnum.CUSTom, PhaseNoise_IntegratedRangeTypeEnum.FULL, HideIfDisabled = true)]
        [Display("Weighthing Filter", Group: "Trace", Order: 30.04, Description: "Sets and returns the weighting filter of the selected integration range. RANGe[1-4] allows selection of the desired weighting filter. This command selects a weighting filter whose values are applied to the calculation of residual effects. The file should be saved in the following directory: C:\\Program Data\\Keysight\\Network Analyzer\\WeightingFilter.")]
        public String WeighthingFilter { get; set; }

        public IntegratedNoiseTable()
        {
            IntegratedRangeType = PhaseNoise_IntegratedRangeTypeEnum.OFF;
            Start = 0.001;
            Stop = 1000000000;
            WeighthingFilter = "None";
        }

    }

    [AllowAsChildIn(typeof(GeneralPhaseNoiseSingleTrace))]
    [Display("Integrated Noise", Groups: new[] { "Signal Source Analyzer", "General", "Phase Noise" }, Description: "Insert a description here")]
    public class GeneralPhaseNoiseIntegratedNoise : SSAXBaseStep
    {
        #region Settings
        [Browsable(false)]
        public int mnum { get; set; }

        [Browsable(false)]
        public int wnum { get; set; }


        [Display("Show Integrated Noise Table", Group: "Settings", Order: 20.01, Description: "Enable or disable displaying the integrated noise table.")]
        public bool ShowIntegratedNoiseTable { get; set; }

        [Display("", Group: "Range 1", Order: 10)]
        [EmbedProperties(PrefixPropertyName = true)]
        public IntegratedNoiseTable IntegratedNoiseTableRange1 { get; set; } = new IntegratedNoiseTable();

        [Display("", Group: "Range 2", Order: 10)]
        [EmbedProperties(PrefixPropertyName = true)]
        public IntegratedNoiseTable IntegratedNoiseTableRange2 { get; set; } = new IntegratedNoiseTable();

        [Display("", Group: "Range 3", Order: 10)]
        [EmbedProperties(PrefixPropertyName = true)]
        public IntegratedNoiseTable IntegratedNoiseTableRange3 { get; set; } = new IntegratedNoiseTable();

        [Display("", Group: "Range 4", Order: 10)]
        [EmbedProperties(PrefixPropertyName = true)]
        public IntegratedNoiseTable IntegratedNoiseTableRange4 { get; set; } = new IntegratedNoiseTable();


        #endregion


        public GeneralPhaseNoiseIntegratedNoise()
        {
            ShowIntegratedNoiseTable = false;


            
        }

        public override void Run()
        {
            mnum = GetParent<SingleTraceBaseStep>().mnum;
            wnum = GetParent<SingleTraceBaseStep>().Window;
            int range = 1;

            RunChildSteps(); //If the step supports child steps.

            SSAX.SetPhaseNoise_ShowIntegratedNoiseTable(wnum, ShowIntegratedNoiseTable);

            SSAX.SetPhaseNoise_IntegratedRangeType(Channel, mnum, range, IntegratedNoiseTableRange1.IntegratedRangeType);
            SSAX.SetPhaseNoise_Start(Channel, mnum, range, IntegratedNoiseTableRange1.Start);
            SSAX.SetPhaseNoise_Stop(Channel, mnum, range, IntegratedNoiseTableRange1.Stop);
            if (IntegratedNoiseTableRange1.WeighthingFilter != "None")
            {
                SSAX.SetPhaseNoise_WeighthingFilter(Channel, mnum, range, IntegratedNoiseTableRange1.WeighthingFilter);
            }

            range = 2;
            SSAX.SetPhaseNoise_IntegratedRangeType(Channel, mnum, range, IntegratedNoiseTableRange2.IntegratedRangeType);
            SSAX.SetPhaseNoise_Start(Channel, mnum, range, IntegratedNoiseTableRange2.Start);
            SSAX.SetPhaseNoise_Stop(Channel, mnum, range, IntegratedNoiseTableRange2.Stop);
            if (IntegratedNoiseTableRange2.WeighthingFilter != "None")
            {
                SSAX.SetPhaseNoise_WeighthingFilter(Channel, mnum, range, IntegratedNoiseTableRange2.WeighthingFilter);
            }

            range = 3;
            SSAX.SetPhaseNoise_IntegratedRangeType(Channel, mnum, range, IntegratedNoiseTableRange3.IntegratedRangeType);
            SSAX.SetPhaseNoise_Start(Channel, mnum, range, IntegratedNoiseTableRange3.Start);
            SSAX.SetPhaseNoise_Stop(Channel, mnum, range, IntegratedNoiseTableRange3.Stop);
            if (IntegratedNoiseTableRange3.WeighthingFilter != "None")
            {
                SSAX.SetPhaseNoise_WeighthingFilter(Channel, mnum, range, IntegratedNoiseTableRange3.WeighthingFilter);
            }

            range = 4;
            SSAX.SetPhaseNoise_IntegratedRangeType(Channel, mnum, range, IntegratedNoiseTableRange4.IntegratedRangeType);
            SSAX.SetPhaseNoise_Start(Channel, mnum, range, IntegratedNoiseTableRange4.Start);
            SSAX.SetPhaseNoise_Stop(Channel, mnum, range, IntegratedNoiseTableRange4.Stop);
            if (IntegratedNoiseTableRange4.WeighthingFilter != "None")
            {
                SSAX.SetPhaseNoise_WeighthingFilter(Channel, mnum, range, IntegratedNoiseTableRange4.WeighthingFilter);
            }


            UpgradeVerdict(Verdict.Pass);
        }

        
    }
}
