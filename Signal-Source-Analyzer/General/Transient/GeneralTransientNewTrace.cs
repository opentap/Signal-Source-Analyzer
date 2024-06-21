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
        [Display("Meas", Groups: new[] { "Trace" }, Order: 11)]
        public TransientWideNarrowTraceEnum Meas { get; set; }
        #endregion

        public GeneralTransientNewTrace()
        {
            Meas = TransientWideNarrowTraceEnum.WB_Freq;
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
            ChildTestSteps.Add(new GeneralTransientSingleTrace() { SSAX = this.SSAX, Meas = this.Meas, Channel = this.Channel, IsControlledByParent = true, EnableTraceSettings = true });
        }

    }
}
