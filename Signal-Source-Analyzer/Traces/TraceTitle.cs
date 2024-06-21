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
    [Display("TraceTitle", Groups: new[] { "Signal Source Analyzer", "Trace" }, Description: "Insert a description here")]
    public class TraceTitle : SSAXBaseStep
    {
        #region Settings
        [Display("Trace Title", Groups: new[] { "Trace" }, Order: 9)]
        public String Title { get; set; }

        #endregion

        public TraceTitle()
        {
            IsControlledByParent = true;
        }

        public override void Run()
        {
            int Window = GetParent<SingleTraceBaseStep>().Window;
            int tnum = GetParent<SingleTraceBaseStep>().tnum;

            if (Title != "")
            {
                SSAX.SetTraceTitle(Window, tnum, Title);
            }

            UpgradeVerdict(Verdict.Pass);
        }
    }
}
