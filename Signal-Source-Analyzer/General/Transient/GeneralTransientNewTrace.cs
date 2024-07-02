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
    [AllowAsChildIn(typeof(GeneralTransientChannel))]
    [AllowChildrenOfType(typeof(GeneralTransientSingleTrace))]
    [Display("Transient New Trace", Groups: new[] { "Signal Source Analyzer", "General", "Transient" }, Description: "Insert a description here")]
    public class GeneralTransientNewTrace : AddNewTraceBaseStep
    {
        #region Settings
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

                // Now lets assign this value to the GeneralTransientNewTrace
                foreach (var a in this.ChildTestSteps)
                {
                    if (a is GeneralTransientSingleTrace)
                    {
                        (a as GeneralTransientSingleTrace).SweepType = value;
                    }
                }

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

                // Now lets update all available traces
                // NB-NB to WB-NB
                //all NB2 renamed to WB
                if (_SweepType == Transient_SweepTypeEnum.WN)
                {
                    foreach (var a in this.ChildTestSteps)
                    {
                        if (a is GeneralTransientSingleTrace)
                        {
                            if ((a as GeneralTransientSingleTrace).Meas.Contains("NB2"))
                            {
                                // Rename to WB
                                (a as GeneralTransientSingleTrace).Meas = "WB_Freq";
                            }
                        }
                    }
                }

                // WB-NB to NB-NB
                // all WB renamed to NB2_Freq
                if (_SweepType == Transient_SweepTypeEnum.NN)
                {
                    foreach (var a in this.ChildTestSteps)
                    {
                        if (a is GeneralTransientSingleTrace)
                        {
                            if ((a as GeneralTransientSingleTrace).Meas.Contains("WB_Freq"))
                            {
                                // Rename to WB
                                (a as GeneralTransientSingleTrace).Meas = "NB2_Freq";
                            }
                        }
                    }
                }

            }
        }

        [Display("Meas", Groups: new[] { "Trace" }, Order: 11)]
        [AvailableValues(nameof(TransientTraceList))]
        public string Meas { get; set; }
        #endregion

        public GeneralTransientNewTrace()
        {
            TransientTraceList = new List<string>() { "WB_Freq", "NB1_Freq", "NB1_Phase", "NB1_Power" };

            Meas = "WB_Freq";
            ChildTestSteps.Add(new GeneralTransientSingleTrace() { SSAX = this.SSAX, Meas = this.Meas, Channel = this.Channel, IsControlledByParent = true, EnableTraceSettings = true });
        }

        [Browsable(false)]
        public override List<(string, object)> GetMetaData()
        {
            List<(string, object)> retVal = new List<(string, object)>();

            return retVal;
        }

        // Called from Add New Trace Button
        protected override void AddNewTrace()
        {
            GeneralTransientSingleTrace generalTransientSingleTrace = new GeneralTransientSingleTrace() { SSAX = this.SSAX, Meas = this.Meas, Channel = this.Channel, IsControlledByParent = true, EnableTraceSettings = true, SweepType = this.SweepType };
            generalTransientSingleTrace.UpdateTestStepName();
            ChildTestSteps.Add(generalTransientSingleTrace);
        }

    }
}
