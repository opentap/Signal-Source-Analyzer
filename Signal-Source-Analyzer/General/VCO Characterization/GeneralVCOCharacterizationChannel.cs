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
    [Display("VCO Characterization Channel", Groups: new[] { "Signal Source Analyzer", "General", "VCO Characterization" }, Description: "Insert a description here")]
    public class GeneralVCOCharacterizationChannel : SSAXBaseStep
    {
        public GeneralVCOCharacterizationChannel()
        {
            IsControlledByParent = false;

            // Traces
            GeneralVCOCharacterizationNewTrace VCOCharacterizationNewTrace = new GeneralVCOCharacterizationNewTrace { IsControlledByParent = true, Channel = this.Channel };
            GeneralVCOCharacterizationSweep VCOCharacterizationSweep = new GeneralVCOCharacterizationSweep { IsControlledByParent = true, Channel = this.Channel };
            GeneralVCOCharacterizationRFPath VCOCharacterizationRFPath = new GeneralVCOCharacterizationRFPath { IsControlledByParent = true, Channel = this.Channel };

            this.ChildTestSteps.Add(VCOCharacterizationSweep);
            this.ChildTestSteps.Add(VCOCharacterizationRFPath);
            this.ChildTestSteps.Add(VCOCharacterizationNewTrace);
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
