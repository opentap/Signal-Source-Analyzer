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
    [AllowAsChildIn(typeof(SingleTraceBaseStep))]
    [Display("Multi Peak Search", Groups: new[] { "Signal Source Analyzer", "Trace" }, Description: "Insert a description here")]
    public class MultiPeakSearch : SSAXBaseStep
    {
        #region Settings
        [Display("Marker Number", Groups: new[] { "Trace" }, Order: 10)]
        public int mkr { get; set; }

        [Display("Peak Threshold", Groups: new[] { "Trace" }, Order: 11)]
        public double PeakThreshold { get; set; }

        [Display("Peak Excursion", Groups: new[] { "Trace" }, Order: 12)]
        public double PeakExcursion{ get; set; }

        [Display("Peak Polarity", Groups: new[] { "Trace" }, Order: 13)]
        public SAMultiPeakSearchPolarityEnumType PeakPolarity { get; set; }
        #endregion

        public MultiPeakSearch()
        {
            IsControlledByParent = true;
            PeakThreshold = -100;
            PeakExcursion = 3;
            PeakPolarity = SAMultiPeakSearchPolarityEnumType.POS;
        }

        public override void Run()
        {
            int mnum = GetParent<SingleTraceBaseStep>().mnum;

            SSAX.SetMultiPeakSearchThreshold(Channel, mnum, PeakThreshold);
            SSAX.SetMultiPeakSearchExcursion(Channel, mnum, PeakExcursion);
            SSAX.SetMultiPeakSearchPolarity(Channel, mnum, PeakPolarity);

            UpgradeVerdict(Verdict.Pass);
        }
    }
}
