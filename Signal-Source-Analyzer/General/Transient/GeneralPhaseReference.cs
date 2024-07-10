// Author: CMontes
// Copyright:   Copyright 2024 Keysight Technologies
//              You have a royalty-free right to use, modify, reproduce and distribute
//              the sample application files (and/or any modified version) in any way
//              you find useful, provided that you agree that Keysight Technologies has no
//              warranty, obligations or liability for any sample application files.
using OpenTap;
using OpenTap.Plugins.PNAX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Signal_Source_Analyzer
{
    public enum PhaseReferenceModeEnum
    {
        [Display("Measure Frequency at")]
        MeasureFrequencyAt,
        [Display("Set Frequency to")]
        SetFrequencyTo
    }

    [AllowAsChildIn(typeof(TestPlan))]
    [AllowAsChildIn(typeof(TestStep))]
    [AllowAsChildIn(typeof(TraceFormat))]
    [Display("Phase Reference", Groups: new[] { "Signal Source Analyzer", "General", "Transient"}, Description: "Insert a description here")]
    public class GeneralPhaseReference : SSAXBaseStep
    {
        #region Settings
        // Override for Channel so we can call ShowTraceSettings
        private int _Channel;
        [EnabledIf("IsControlledByParent", false, HideIfDisabled = false)]
        [Display("Channel", Order: 0.11)]
        public override int Channel
        {
            get
            {
                ShowTraceSettings();
                return _Channel;
            }
            set
            {
                ShowTraceSettings();
                _Channel = value;

                // Update traces
                foreach (var a in ChildTestSteps)
                {
                    if (a.GetType().IsSubclassOf(typeof(PNABaseStep)))
                    {
                        (a as PNABaseStep).Channel = value;
                    }
                    if (a is SingleTraceBaseStep)
                    {
                        (a as SingleTraceBaseStep).UpdateTestStepName();
                    }
                }
            }
        }

        [EnabledIf("IsControlledByParent", false, HideIfDisabled = true)]
        [Display("MNum (standalone)", Groups: new[] { "Trace" }, Order: 10)]
        public int mnum { get; set; }

        [Display("Mode", Group: "Phase Reference Frequency", Order: 20.00, Description: "Set and read the \"Measure frequency at\" setting in the phase reference frequency.")]
        public PhaseReferenceModeEnum PhaseMode { get; set; }

        [EnabledIf("PhaseMode", PhaseReferenceModeEnum.MeasureFrequencyAt, HideIfDisabled = true)]
        [Display("Measure Frequency At", Group: "Phase Reference Frequency", Order: 20.01, Description: "Set and read the \"Measure frequency at\" setting in the phase reference frequency.")]
        [Unit("S", UseEngineeringPrefix: true, StringFormat: "")]
        public double MeasureFrequencyAt { get; set; }

        [EnabledIf("PhaseMode", PhaseReferenceModeEnum.MeasureFrequencyAt, HideIfDisabled = true)]
        [Display("Using Span of", Group: "Phase Reference Frequency", Order: 20.02, Description: "Set and read the \"Using Span of\" setting in the phase reference frequency.")]
        [Unit("S", UseEngineeringPrefix: true, StringFormat: "")]
        public double PhaseReferenceFrequencySpan { get; set; }

        [EnabledIf("PhaseMode", PhaseReferenceModeEnum.SetFrequencyTo, HideIfDisabled = true)]
        [Display("Set Frequency To", Group: "Phase Reference Frequency", Order: 20.03, Description: "Set and read the \"Set Frequency to\" setting in the phase reference frequency.")]
        [Unit("Hz", UseEngineeringPrefix: true, StringFormat: "0.000000000")]
        public double SetFrequencyTo { get; set; }

        [Display("Set Phase to Zero At", Group: "Phase Zero Position", Order: 30.01, Description: "Set and read the \"Phase to Zero at\" setting in the phase zero position.")]
        [Unit("S", UseEngineeringPrefix: true, StringFormat: "0.000000")]
        public double SetPhasetoZeroAt { get; set; }

        [Display("Using Span of", Group: "Phase Zero Position", Order: 30.02, Description: "Set and read the \"Using Span of\" setting in the phase zero position.")]
        [Unit("S", UseEngineeringPrefix: false, StringFormat: "0.000000")]
        public double PhaseSpan { get; set; }





        #endregion

        private void ShowTraceSettings()
        {
            // What type of parent test we have?
            if (this.Parent != null)
            {
                Type parType = this.Parent.GetType();
                if (parType.Equals(typeof(TraceFormat)))
                {
                    // Parent is Trace format
                    // get mnum from parent
                    IsControlledByParent = true;
                }
                else
                {
                    // parent is test plan or any other test step i.e. sequence 
                    IsControlledByParent = false;
                }
            }
        }

        public GeneralPhaseReference()
        {
            PhaseMode = PhaseReferenceModeEnum.MeasureFrequencyAt;

            MeasureFrequencyAt = 0;
            PhaseReferenceFrequencySpan = 0;
            SetFrequencyTo = 1000000000;

            SetPhasetoZeroAt = 1000;
            PhaseSpan = 0;
        }

        public override void Run()
        {
            mnum = GetParent<TraceFormat>().mnum;

            if (PhaseMode == PhaseReferenceModeEnum.MeasureFrequencyAt)
            {
                SSAX.SetTransient_PhaseReferenceFrequencyAuto(Channel, mnum, true);
                SSAX.SetTransient_MeasureFrequencyAt(Channel, mnum, MeasureFrequencyAt);
                SSAX.SetTransient_PhaseReferenceFrequencySpan(Channel, mnum, PhaseReferenceFrequencySpan);
            }
            else if (PhaseMode == PhaseReferenceModeEnum.SetFrequencyTo)
            {
                SSAX.SetTransient_PhaseReferenceFrequencyAuto(Channel, mnum, false);
                SSAX.SetTransient_SetFrequencyTo(Channel, mnum, SetFrequencyTo);
            }

            SSAX.SetTransient_SetPhasetoZeroAt(Channel, mnum, SetPhasetoZeroAt);
            SSAX.SetTransient_PhaseSpan(Channel, mnum, PhaseSpan);


            UpgradeVerdict(Verdict.Pass);
        }
    }
}
