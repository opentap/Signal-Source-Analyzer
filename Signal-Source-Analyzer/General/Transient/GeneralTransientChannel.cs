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
    [Display("Transient Channel", Groups: new[] { "Signal Source Analyzer", "General", "Transient" }, Description: "Insert a description here")]
    public class GeneralTransientChannel : SSAXBaseStep
    {
        [Browsable(false)]
        public bool IsPropertyReadOnly { get; set; } = true;

        private Transient_SweepTypeEnum _SweepType;
        [EnabledIf("IsPropertyReadOnly", false, HideIfDisabled = false)]
        [Display("Sweep Type", Group: "Settings", Order: 20.01, Description: "Sets and read the transition mode, Wide-Narrow or Narrow-Narrow.")]
        public Transient_SweepTypeEnum SweepType
        {
            get
            {
                return _SweepType;
            }
            set
            {
                _SweepType = value;
                // Now lets assign this value to the GeneralTransientNewTrace
                foreach (var a in this.ChildTestSteps)
                {
                    if (a is GeneralTransientNewTrace)
                    {
                        (a as GeneralTransientNewTrace).SweepType = value;
                    }
                    if (a is GeneralTransientSingleTrace)
                    {
                        (a as GeneralTransientSingleTrace).SweepType = value;
                    }
                }
            }
        }


        public GeneralTransientChannel()
        {
            IsControlledByParent = false;

            // Traces
            GeneralTransientNewTrace TransientNewTrace = new GeneralTransientNewTrace { IsControlledByParent = true, Channel = this.Channel };
            GeneralTransientSweep TransientSweep = new GeneralTransientSweep { IsControlledByParent = true, Channel = this.Channel };

            this.ChildTestSteps.Add(TransientSweep);
            this.ChildTestSteps.Add(TransientNewTrace);
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
