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
    public enum TransientWideNarrowTraceEnum
    {
        [Display("Wide Band Frequency")]
        WB_Freq,
        [Display("Narrow Band 1 Frequency")]
        NB1_Freq,
        [Display("Narrow Band 1 Phase")]
        NB1_Phase,
        [Display("Narrow Band 1 Power")]
        NB1_Power,
    }

    public enum TransientNarrowNarrowTraceEnum
    {
        [Display("Narrow Band 1 Frequency")]
        NB1_Freq,
        [Display("Narrow Band 1 Phase")]
        NB1_Phase,
        [Display("Narrow Band 1 Power")]
        NB1_Power,
        [Display("Narrow Band 2 Frequency")]
        NB2_Freq,
        [Display("Narrow Band 2 Phase")]
        NB2_Phase,
        [Display("Narrow Band 2 Power")]
        NB2_Power,
    }

    [AllowAsChildIn(typeof(GeneralTransientNewTrace))]
    [Display("Transient Single Trace", Groups: new[] { "Signal Source Analyzer", "General", "Transient" }, Description: "Insert a description here")]
    public class GeneralTransientSingleTrace : SingleTraceBaseStep
    {

        private TransientWideNarrowTraceEnum _Meas;

        [EnabledIf(nameof(CustomTraceMeas), false, HideIfDisabled = true)]
        [Display("Meas", Groups: new[] { "Trace" }, Order: 11.1)]
        public TransientWideNarrowTraceEnum Meas
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

        public GeneralTransientSingleTrace()
        {
            Meas = TransientWideNarrowTraceEnum.WB_Freq;
            measClass = "Transient";
        }

    }
}
