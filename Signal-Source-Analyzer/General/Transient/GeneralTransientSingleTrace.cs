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
    [AllowAsChildIn(typeof(GeneralTransientNewTrace))]
    [Display("Transient Single Trace", Groups: new[] { "Signal Source Analyzer", "General", "Transient" }, Description: "Insert a description here")]
    public class GeneralTransientSingleTrace : SingleTraceBaseStep
    {
        [Browsable(false)]
        public bool IsPropertyReadOnly { get; set; } = true;

        private Transient_SweepTypeEnum _SweepType;
        [EnabledIf("IsPropertyReadOnly", false, HideIfDisabled = false)]
        [Display("Sweep Type", Group: "Settings", Order: 10.01, Description: "Sets and read the transition mode, Wide-Narrow or Narrow-Narrow.")]
        public Transient_SweepTypeEnum SweepType
        {
            get
            {
                return _SweepType;
            }
            set
            {
                _SweepType = value;
                // Update Available traces to choose from
                // NB-NB to WB-NB
                //all NB2 renamed to WB
                if (_SweepType == Transient_SweepTypeEnum.WN)
                {
                    TransientTraceList = new List<string>() { "WB_Freq", "NB1_Freq", "NB1_Phase", "NB1_Power" };
                }

                // WB-NB to NB-NB
                // all WB renamed to NB2_Freq
                if (_SweepType == Transient_SweepTypeEnum.NN)
                {
                    TransientTraceList = new List<string>() { "NB1_Freq", "NB1_Phase", "NB1_Power", "NB2_Freq", "NB2_Phase", "NB2_Power" };
                }
            }
        }

        private List<String> _TransientTraceList;
        [Browsable(false)]
        [Display("Transient Trace List", "Traces that can be added as a new Trace on Transient Channel", Group: "Transient Settings", Order: 60, Collapsed: true)]
        public List<String> TransientTraceList
        {
            get { return _TransientTraceList; }
            set
            {
                _TransientTraceList = value;
                OnPropertyChanged("TransientTraceList");
            }
        }

        private string _Meas;
        [EnabledIf(nameof(CustomTraceMeas), false, HideIfDisabled = true)]
        [Display("Meas", Groups: new[] { "Trace" }, Order: 11.1)]
        [AvailableValues(nameof(TransientTraceList))]
        public string Meas
        {
            get
            {
                return _Meas;
            }
            set
            {
                _Meas = value;
                //string scpi = Scpi.Format("{0}", value);
                measEnumName = _Meas;   // scpi;    // value.ToString();
                UpdateTestStepName();
            }
        }

        public override void PrePlanRun()
        {
            base.PrePlanRun();

            // For Transient Channel
            // if its the first trace and if the sweep type is Narrow-Narrow, first we need to define a dummy trace
            int traceCount = SSAX.GetNewTraceID(Channel);

            if ((traceCount == 1) && (SweepType == Transient_SweepTypeEnum.NN))
            {
                // so we can initialize the channel
                SSAX.ScpiCommand($"CALCulate{Channel}:MEASure700:DEFine \"WB_Freq:Transient\"");
                // then change the sweep type to Narrow-Narrow
                SSAX.SetTransient_SweepType(Channel, SweepType);
                // delete the dummy trace
                SSAX.ScpiCommand($"CALCulate{Channel}:PARameter:DELete \"CH{Channel}_WB_Freq_700\"");
            }
            // proceed

            AddNewTraceToSSAX();
        }

        public GeneralTransientSingleTrace()
        {
            Meas = "WB_Freq";
            measClass = "Transient";
            TransientTraceList = new List<string>() { "WB_Freq", "NB1_Freq", "NB1_Phase", "NB1_Power" };
        }

    }
}
