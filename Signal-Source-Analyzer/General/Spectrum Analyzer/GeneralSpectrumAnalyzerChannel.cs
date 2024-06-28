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
    [Display("Spectrum Analyzer Channel", Groups: new[] { "Signal Source Analyzer", "General", "Spectrum Analyzer" }, Description: "Insert a description here")]
    public class GeneralSpectrumAnalyzerChannel : SSAXBaseStep
    {
        public GeneralSpectrumAnalyzerChannel()
        {
            IsControlledByParent = false;

            // Traces
            GeneralSpectrumAnalyzerNewTrace SpectrumAnalyzerNewTrace = new GeneralSpectrumAnalyzerNewTrace { IsControlledByParent = true, Channel = this.Channel };
            GeneralSpectrumAnalyzerSetup SpectrumAnalyzerSetup = new GeneralSpectrumAnalyzerSetup { IsControlledByParent = true, Channel = this.Channel };

            this.ChildTestSteps.Add(SpectrumAnalyzerSetup);
            this.ChildTestSteps.Add(SpectrumAnalyzerNewTrace);
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
