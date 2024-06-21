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
    [AllowAsChildIn(typeof(SingleTraceBaseStep))]
    [Display("TraceFormat", Groups: new[] { "Signal Source Analyzer", "Trace"}, Description: "Insert a description here")]
    public class TraceFormat : SSAXBaseStep
    {
        #region Settings
        [Display("Format", Groups: new[] { "Trace" }, Order: 11.5)]
        public SSAX.MeasurementFormatEnum Format { get; set; }
        #endregion

        public TraceFormat()
        {
            IsControlledByParent = true;
            Format = SSAX.MeasurementFormatEnum.MLOGarithmic;
        }

        public override void Run()
        {
            //Channel = GetParent<GeneralSingleTraceBaseStep>().Channel;
            int mnum  = GetParent<SingleTraceBaseStep>().mnum;

            SSAX.SetTraceFormat(Channel, mnum, Format);

            UpgradeVerdict(Verdict.Pass);
        }
    }
}
