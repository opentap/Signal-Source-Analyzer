using OpenTap;
using OpenTap.Plugins.PNAX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signal_Source_Analyzer
{

    [AllowAsChildIn(typeof(GeneralPhaseNoiseSingleTrace))]
    [Display("Spurious", Groups: new[] { "Signal Source Analyzer", "General", "Phase Noise" }, Description: "Insert a description here")]
    public class GeneralPhaseNoiseSpurious : SSAXBaseStep
    {
        #region Settings
        [Browsable(false)]
        public int mnum { get; set; }

        [Browsable(false)]
        public int wnum { get; set; }


        [Display("Show Spurious Table", Group: "Settings", Order: 20.01, Description: "Enable or disable displaying the spurious table.")]
        public bool ShowSpuriousTable { get; set; }

        [Display("Table Sort Order", Group: "Settings", Order: 20.02, Description: "Sets and returns the spurious table sorting order.")]
        public PhaseNoise_TableSortOrderEnum TableSortOrder { get; set; }

        [Display("Enable Spur Analysis", Group: "Trace", Order: 30.01, Description: "Enables and disables spurious analysis.")]
        public bool EnableSpurAnalysis { get; set; }

        [Display("Spur Sensibility", Group: "Trace", Order: 30.02, Description: "Sets and returns the spurious sensibility number.")]
        public double SpurSensibility { get; set; }

        [Display("Min Spur Level", Group: "Trace", Order: 30.03, Description: "Sets and returns the minimum spurious threshold level.")]
        [Unit("dBc", UseEngineeringPrefix: true, StringFormat: "0.00")]
        public double MinSpurLevel { get; set; }

        [Display("Threshold Table", Group: "Trace", Order: 30.04, Description: "Sets and returns the spurious threshold table data.")]
        public string ThresholdTable { get; set; }

        [Display("Omit Displayed Spur", Group: "Trace", Order: 30.05, Description: "Enables and disables spur omission.")]
        public bool OmitDisplayedSpur { get; set; }

        [Display("User Spur Table", Group: "Trace", Order: 30.06, Description: "Sets and returns the User Spur Table data which defines spurs to omit.")]
        public Array UserSpurTable { get; set; }

        
        #endregion


        public GeneralPhaseNoiseSpurious()
        {
            ShowSpuriousTable = false;
            TableSortOrder = PhaseNoise_TableSortOrderEnum.POWer;

            EnableSpurAnalysis = false;
            SpurSensibility = 3;
            MinSpurLevel = -500;
            //ThresholdTable = ;
            OmitDisplayedSpur = false;
            //UserSpurTable = ;

            


        }

        public override void Run()
        {
            mnum = GetParent<SingleTraceBaseStep>().mnum;
            wnum = GetParent<SingleTraceBaseStep>().Window;

            RunChildSteps(); //If the step supports child steps.

            SSAX.SetPhaseNoise_ShowSpuriousTable(wnum, ShowSpuriousTable);
            SSAX.SetPhaseNoise_TableSortOrder(Channel, mnum, TableSortOrder);

            SSAX.SetPhaseNoise_EnableSpurAnalysis(Channel, mnum, EnableSpurAnalysis);
            SSAX.SetPhaseNoise_SpurSensibility(Channel, mnum, SpurSensibility);
            SSAX.SetPhaseNoise_MinSpurLevel(Channel, mnum, MinSpurLevel);
            //SSAX.SetPhaseNoise_ThresholdTable(Channel, mnum, ThresholdTable);
            SSAX.SetPhaseNoise_OmitDisplayedSpur(Channel, mnum, OmitDisplayedSpur);
            //SSAX.SetPhaseNoise_UserSpurTable(Channel, mnum, UserSpurTable);

            

            UpgradeVerdict(Verdict.Pass);
        }

        
    }
}
