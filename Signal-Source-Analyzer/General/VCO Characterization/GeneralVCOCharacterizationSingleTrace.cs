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
    public enum VCOCharacterizationTraceEnum
    {
        [Display("Frequency")]
        Freq,
        [Display("Power")]
        Power,
        [Display("CurrentVC")]
        CurrentVC,
        [Display("CurrentVS1")]
        CurrentVS1,
        [Display("CurrentVS2")]
        CurrentVS2,
    }

    [AllowAsChildIn(typeof(GeneralVCOCharacterizationNewTrace))]
    [Display("VCO Characterization Single Trace", Groups: new[] { "Signal Source Analyzer", "General", "VCO Characterization" }, Description: "Insert a description here")]
    public class GeneralVCOCharacterizationSingleTrace : SingleTraceBaseStep
    {

        private VCOCharacterizationTraceEnum _Meas;

        [EnabledIf(nameof(CustomTraceMeas), false, HideIfDisabled = true)]
        [Display("Meas", Groups: new[] { "Trace" }, Order: 11.1)]
        public VCOCharacterizationTraceEnum Meas
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

        public GeneralVCOCharacterizationSingleTrace()
        {
            Meas = VCOCharacterizationTraceEnum.Freq;
            measClass = "VCO Characterization";
        }

    }
}
