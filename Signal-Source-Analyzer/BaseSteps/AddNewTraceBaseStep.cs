﻿using OpenTap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signal_Source_Analyzer
{
    [Browsable(false)]
    public class AddNewTraceBaseStep : SingleTraceBaseStep
    {
        #region Settings
        [Browsable(false)]
        public bool EnableButton { get; set; } = true;

        [Browsable(true)]
        [EnabledIf("EnableButton", true, HideIfDisabled = false)]
        [Display("Add New Trace", Groups: new[] { "Trace" }, Order: 20)]
        [Layout(LayoutMode.FullRow)]
        public virtual void AddNewTraceButton()
        {
            AddNewTrace();
        }
        #endregion

        public override void UpdateTestStepName()
        {
            // No need to update Test Name on New Trace Test Step
            // only on Single Trace
        }

        public override void Run()
        {
            RunChildSteps(); //If the step supports child steps.

            // If no verdict is used, the verdict will default to NotSet.
            // You can change the verdict using UpgradeVerdict() as shown below.
            UpgradeVerdict(Verdict.Pass);
        }

        protected virtual void AddNewTrace()
        {
        }
    }
}
