// Author: CMontes
// Copyright:   Copyright 2024 Keysight Technologies
//              You have a royalty-free right to use, modify, reproduce and distribute
//              the sample application files (and/or any modified version) in any way
//              you find useful, provided that you agree that Keysight Technologies has no
//              warranty, obligations or liability for any sample application files.
using OpenTap;
using OpenTap.Plugins.PNAX;
using Signal_Source_Analyzer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Signal_Source_Analyzer
{
    [Display("Phase Noise Channel", Groups: new[] { "Signal Source Analyzer", "General", "Phase Noise" }, Description: "Insert a description here")]
    public class GeneralPhaseNoiseChannel : SSAXBaseStep
    {
        [Browsable(false)]
        public bool IsPropertyReadOnly { get; set; } = true;

        private PhaseNoise_NoiseTypeEnum _NoiseType;
        [EnabledIf("IsPropertyReadOnly", false, HideIfDisabled = false)]
        [Display("Sweep Type", Group: "Settings", Order: 20.01, Description: "Sets and read the transition mode, Wide-Narrow or Narrow-Narrow.")]
        public PhaseNoise_NoiseTypeEnum NoiseType
        {
            get
            {
                return _NoiseType;
            }
            set
            {
                _NoiseType = value;
                // Now lets assign this value to the GeneralTransientNewTrace
                foreach (var a in this.ChildTestSteps)
                {
                    if (a is GeneralPhaseNoiseRFPath)
                    {
                        (a as GeneralPhaseNoiseRFPath).NoiseType = value;
                    }
                }
            }
        }

        public GeneralPhaseNoiseChannel()
        {
            IsControlledByParent = false;

            // Traces
            GeneralPhaseNoiseNewTrace PhaseNoiseNewTrace = new GeneralPhaseNoiseNewTrace { IsControlledByParent = true, Channel = this.Channel };
            GeneralPhaseNoiseSweep PhaseNoiseSweep = new GeneralPhaseNoiseSweep { IsControlledByParent = true, Channel = this.Channel };
            GeneralPhaseNoiseRFPath PhaseNoiseRFPath = new GeneralPhaseNoiseRFPath { IsControlledByParent = true, Channel = this.Channel };

            this.ChildTestSteps.Add(PhaseNoiseSweep);
            this.ChildTestSteps.Add(PhaseNoiseRFPath);
            this.ChildTestSteps.Add(PhaseNoiseNewTrace);
            ChildItemVisibility.SetVisibility(this, ChildItemVisibility.Visibility.Visible);
        }


        public override void Run()
        {
            //int dummy_trace_id = SSAX.GetNewTraceID(Channel);
            //// Define a dummy measurement so we can setup all channel parameters
            //// we will add the traces during the StandardSingleTrace or StandardNewTrace test steps
            //SSAX.ScpiCommand($"CALCulate{Channel}:MEASure{dummy_trace_id}:DEFine \"PN:Phase Noise\"");

            RunChildSteps(); //If the step supports child steps.

            // If no verdict is used, the verdict will default to NotSet.
            // You can change the verdict using UpgradeVerdict() as shown below.
            UpgradeVerdict(Verdict.Pass);

        }
    }
}
