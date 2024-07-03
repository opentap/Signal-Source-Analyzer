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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signal_Source_Analyzer
{
    [Display("Trigger", Groups: new[] { "Signal Source Analyzer", "General", "Transient" }, Description: "Insert a description here")]
    public class GeneralTransientTrigger : SSAXBaseStep
    {
        #region Settings
        [Display("Enable Video Trigger", Group: "Settings", Order: 20.01, Description: "Enable and disable the video trigger for the Transient measurement.")]
        public bool EnableVideoTrigger { get; set; }

        [Display("Select Trigger", Group: "Settings", Order: 20.02, Description: "Set and read the trigger source from Wide, Narrow1 or Narrow2.")]
        public Transient_TriggerEnum SelectTrigger { get; set; }

        [Display("Trigger type", Group: "Settings", Order: 20.03, Description: "Set and read the trigger type for Narrow1 or Narrow2.")]
        public Transient_TriggerTypeEnum TriggerType { get; set; }

        [Display("Target Narrow Frequency", Group: "Settings", Order: 20.04, Description: "Set and read the target frequency value which make a video trigger in Narrow band")]
        [Unit("Hz", UseEngineeringPrefix: true, StringFormat: "0.000000000")]
        public double TargetNarrowFrequency { get; set; }

        [Display("Target Narrow Power", Group: "Settings", Order: 20.05, Description: "Set and read the target power value which make a video trigger in Narrow band.")]
        [Unit("dBm", UseEngineeringPrefix: true, StringFormat: "")]
        public double TargetNarrowPower { get; set; }

        [Display("Target Wide Frequency", Group: "Settings", Order: 20.06, Description: "Set and read the target frequency value which make a video trigger in Wideband.")]
        [Unit("Hz", UseEngineeringPrefix: true, StringFormat: "0.000000000")]
        public double TargetWideFrequency { get; set; }

        [Display("Hysteresis Narrow Frequency", Group: "Settings", Order: 20.07, Description: "Set and read the hysteresis value (frequency")]
        [Unit("Hz", UseEngineeringPrefix: true, StringFormat: "0.000")]
        public double HysteresisNarrowFrequency { get; set; }

        [Display("Hysteresis Narrow Power", Group: "Settings", Order: 20.08, Description: "Set and read the hysteresis value (Power")]
        [Unit("dBm", UseEngineeringPrefix: true, StringFormat: "0.000")]
        public double HysteresisNarrowPower { get; set; }

        [Display("Hysteresis Wide Frequency", Group: "Settings", Order: 20.09, Description: "Set and read the hysteresis value for video trigger in Wideband.")]
        [Unit("Hz", UseEngineeringPrefix: true, StringFormat: "0.000")]
        public double HysteresisWideFrequency { get; set; }

        [Display("Edge Narrow", Group: "Settings", Order: 20.10, Description: "Set and read the trigger edge from positive or negative for video trigger in Narrow band.")]
        public Transient_SlopeEnum EdgeNarrow { get; set; }

        [Display("Edge Wide", Group: "Settings", Order: 20.11, Description: "Set and read the trigger edge from positive or negative for video trigger in Wideband.")]
        public Transient_SlopeEnum EdgeWide { get; set; }

        [Display("Min Input Level", Group: "Settings", Order: 20.12, Description: "Set and read the minimum power value which disables the video trigger in Narrow band. The trigger is disabled if the power level is below this level. The relative value from the maximum input level should be specified.")]
        [Unit("dB", UseEngineeringPrefix: true, StringFormat: "0.00")]
        public double MinInputLevel { get; set; }

        [Display("Holdoff Narrow", Group: "Settings", Order: 20.13, Description: "Set and read the holdoff time for Video Trigger in Narrow band.")]
        [Unit("S", UseEngineeringPrefix: true, StringFormat: "0.000")]
        public double HoldoffNarrow { get; set; }

        [Display("Holdoff Wide", Group: "Settings", Order: 20.14, Description: "Set and read the holdoff time for Video Trigger in Wide band.")]
        [Unit("S", UseEngineeringPrefix: true, StringFormat: "0.000")]
        public double HoldoffWide { get; set; }






        #endregion

        public GeneralTransientTrigger()
        {
            EnableVideoTrigger = false;
            SelectTrigger = Transient_TriggerEnum.WB;
            TriggerType = Transient_TriggerTypeEnum.POWer;
            TargetNarrowFrequency = 1000000000;
            TargetNarrowPower = 0;
            TargetWideFrequency = 1000000000;
            HysteresisNarrowFrequency = 1000;
            HysteresisNarrowPower = 0.01;
            HysteresisWideFrequency = 1000;
            EdgeNarrow = Transient_SlopeEnum.POSitive;
            EdgeWide = Transient_SlopeEnum.POSitive;
            MinInputLevel = -20;
            HoldoffNarrow = 0.001;
            HoldoffWide = 0.001;


        }

        public override void Run()
        {
            int band = 1;
            SSAX.SetTransient_EnableVideoTrigger(Channel, EnableVideoTrigger);
            SSAX.SetTransient_SelectTrigger(Channel, SelectTrigger);
            SSAX.SetTransient_TriggerType(Channel, band, TriggerType);
            SSAX.SetTransient_TargetNarrowFrequency(Channel, band, TargetNarrowFrequency);
            SSAX.SetTransient_TargetNarrowPower(Channel, band, TargetNarrowPower);
            SSAX.SetTransient_TargetWideFrequency(Channel, TargetWideFrequency);
            SSAX.SetTransient_HysteresisNarrowFrequency(Channel, band, HysteresisNarrowFrequency);
            SSAX.SetTransient_HysteresisNarrowPower(Channel, band, HysteresisNarrowPower);
            SSAX.SetTransient_HysteresisWideFrequency(Channel, HysteresisWideFrequency);
            SSAX.SetTransient_EdgeNarrow(Channel, band, EdgeNarrow);
            SSAX.SetTransient_EdgeWide(Channel, EdgeWide);
            SSAX.SetTransient_MinInputLevel(Channel, band, MinInputLevel);
            SSAX.SetTransient_HoldoffNarrow(Channel, band, HoldoffNarrow);
            SSAX.SetTransient_HoldoffWide(Channel, HoldoffWide);



            UpgradeVerdict(Verdict.Pass);
        }
    }
}
