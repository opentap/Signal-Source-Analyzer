// Author: CMontes
// Copyright:   Copyright 2024 Keysight Technologies
//              You have a royalty-free right to use, modify, reproduce and distribute
//              the sample application files (and/or any modified version) in any way
//              you find useful, provided that you agree that Keysight Technologies has no
//              warranty, obligations or liability for any sample application files.
using OpenTap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Signal_Source_Analyzer
{
    public enum SpectrumAnalyzerTraceEnum
    {
        SA1,
        SA2
    }
    
    //[AllowAsChildIn(typeof(GeneralSpectrumAnalyzerNewTrace))]
    [Display("Spectrum Analyzer Single Trace", Groups: new[] { "Signal Source Analyzer", "General", "Spectrum Analyzer" }, Description: "Insert a description here")]
    public class GeneralSpectrumAnalyzerSingleTrace : SingleTraceBaseStep
    {

        private SpectrumAnalyzerTraceEnum _Meas;

        [EnabledIf(nameof(CustomTraceMeas), false, HideIfDisabled = true)]
        [Display("Meas", Groups: new[] { "Trace" }, Order: 11.1)]
        public SpectrumAnalyzerTraceEnum Meas
        {
            get
            {
                return _Meas;
            }
            set
            {
                _Meas = value;
                string scpi = Scpi.Format("{0}", value);
                measEnumName = scpi;    // value.ToString();
                UpdateTestStepName();
            }
        }

        public override void PrePlanRun()
        {
            base.PrePlanRun();
            AddNewTraceToSSAX();
        }

        public GeneralSpectrumAnalyzerSingleTrace()
        {
            Meas = SpectrumAnalyzerTraceEnum.SA1;
            measClass = "Spectrum Analyzer";
            EnableTraceSettings = true;
        }

    }
}
