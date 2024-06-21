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
    [AllowAnyChild]
    [Browsable(false)]
    public class SSAXBaseStep : TestStep
    {
        #region Settings

        [Browsable(false)]
        public bool IsControlledByParent { get; set; } = false;
        private SSAX _SSAX;
        [EnabledIf("IsControlledByParent", false, HideIfDisabled = false)]
        [Display("PNA", Order: 0.1)]
        public virtual SSAX SSAX
        {
            get { return _SSAX; }
            set
            {
                _SSAX = value;
                // Update traces
                foreach (var a in ChildTestSteps)
                {
                    if (a.GetType().IsSubclassOf(typeof(SSAXBaseStep)))
                    {
                        (a as SSAXBaseStep).SSAX = value;
                    }
                }
            }
        }

        private int _Channel;
        [EnabledIf("IsControlledByParent", false, HideIfDisabled = false)]
        [Display("Channel", Order: 0.11)]
        public virtual int Channel
        {
            get { return _Channel; }
            set
            {
                _Channel = value;

                // Update traces
                foreach (var a in ChildTestSteps)
                {
                    if (a.GetType().IsSubclassOf(typeof(SSAXBaseStep)))
                    {
                        (a as SSAXBaseStep).Channel = value;
                    }
                    if (a is SingleTraceBaseStep)
                    {
                        (a as SingleTraceBaseStep).UpdateTestStepName();
                    }
                }
            }
        }

        [Output]
        [Browsable(false)]
        [Display("MetaData", Groups: new[] { "MetaData" }, Order: 1000.0)]
        public List<(string, object)> MetaData { get; set; }
        #endregion

        public SSAXBaseStep()
        {
            // ToDo: Set default values for properties / settings.
            Channel = 1;
        }

        public override void Run()
        {
            // ToDo: Add test case code.
            RunChildSteps(); //If the step supports child steps.

            // If no verdict is used, the verdict will default to NotSet.
            // You can change the verdict using UpgradeVerdict() as shown below.
            // UpgradeVerdict(Verdict.Pass);
        }

        [Browsable(false)]
        public virtual List<(string, object)> GetMetaData()
        {
            throw new NotImplementedException();
        }

        public virtual void UpdateMetaData()
        {
            MetaData = new List<(string, object)> { ("Channel", Channel) };

            foreach (var ch in this.ChildTestSteps)
            {
                List<(string, object)> ret = (ch as SSAXBaseStep).GetMetaData();
                foreach (var it in ret)
                {
                    MetaData.Add(it);
                }
            }
        }

    }
}
