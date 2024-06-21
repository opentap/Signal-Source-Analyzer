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
    public enum PhaseNoiseTraceEnum
    {
        [Display("PN")]
        [Scpi("PN")]
        PN,
        [Display("AM")]
        [Scpi("AM")]
        AM,
        [Display("PN+AM")]
        [Scpi("PN+AM")]
        PN_AM,
        [Display("L(f)")]
        [Scpi("L(f)")]
        Lf,
        [Display("Sphi(f)")]
        [Scpi("Sphi(f)")]
        Sphif,
        [Display("Sv(f)")]
        [Scpi("Sv(f)")]
        Svf,
        [Display("Sy(f)")]
        [Scpi("Sy(f)")]
        Syf,
        [Display("AVAR")]
        [Scpi("AVAR")]
        AVAR,
        [Display("ADEV")]
        [Scpi("ADEV")]
        ADEV
    }

    [AllowAsChildIn(typeof(GeneralPhaseNoiseNewTrace))]
    [Display("Phase Noise Single Trace", Groups: new[] { "Signal Source Analyzer", "General",  "Phase Noise" }, Description: "Insert a description here")]
    public class GeneralPhaseNoiseSingleTrace : SingleTraceBaseStep
    {

        private PhaseNoiseTraceEnum _Meas;

        [EnabledIf(nameof(CustomTraceMeas), false, HideIfDisabled = true)]
        [Display("Meas", Groups: new[] { "Trace" }, Order: 11.1)]
        public PhaseNoiseTraceEnum Meas
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

        public GeneralPhaseNoiseSingleTrace()
        {
            Meas = PhaseNoiseTraceEnum.PN;
            measClass = "Phase Noise";
        }

    }
}
