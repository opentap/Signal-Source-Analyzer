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
using System.Threading.Tasks;

namespace Signal_Source_Analyzer
{
    [Display("Trigger", Groups: new[] { "Signal Source Analyzer", "General", "Transient" }, Description: "Insert a description here")]
    public class GeneralTransientTrigger : SSAXBaseStep
    {
        #region Settings

        private List<String> _TransientTriggerTypeList;
        [Browsable(false)]
        [Display("Transient Trigger Type List", "Trigger Type for a Transient Channel", Group: "Transient Settings", Order: 60, Collapsed: true)]
        public List<String> TransientTriggerTypeList
        {
            get { return _TransientTriggerTypeList; }
            set
            {
                _TransientTriggerTypeList = value;
                OnPropertyChanged("TransientTriggerTypeList");
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

                // Update Select Trigger with the options to choose from
                if (_SweepType == Transient_SweepTypeEnum.WN)
                {
                    // Wide and Narrow1
                    TransientTriggerTypeList = new List<string>() { "Wide", "Narrow1" };
                    SelectTrigger = "Wide";
                }

                if (_SweepType == Transient_SweepTypeEnum.NN)
                {
                    // Narrow1 and Narrow2
                    TransientTriggerTypeList = new List<string>() { "Narrow1", "Narrow2" };
                    SelectTrigger = "Narrow1";
                }
            }
        }


        [Display("Enable Video Trigger", Group: "Settings", Order: 20.01, Description: "Enable and disable the video trigger for the Transient measurement.")]
        public bool EnableVideoTrigger { get; set; }

        [Browsable (false)]
        public Transient_TriggerEnum SelectedTransientTrigger { get; set; }

        private string _SelectTrigger;
        [Display("Select Trigger", Group: "Settings", Order: 20.02, Description: "Set and read the trigger source from Wide, Narrow1 or Narrow2.")]
        [AvailableValues(nameof(TransientTriggerTypeList))]
        public string SelectTrigger
        {
            get
            {
                return _SelectTrigger;
            }
            set
            {
                _SelectTrigger = value;
                if (value.Equals("Wide"))
                    SelectedTransientTrigger = Transient_TriggerEnum.WB;
                else if (value.Equals("Narrow1"))
                    SelectedTransientTrigger = Transient_TriggerEnum.NB1;
                else if (value.Equals("Narrow2"))
                    SelectedTransientTrigger = Transient_TriggerEnum.NB2;
                else
                    throw new Exception("Unknown Trigger Type!");
            }
        }


        [EnabledIf("SelectedTransientTrigger", Transient_TriggerEnum.WB, HideIfDisabled = true)]
        [Display("Target Frequency", Group: "Wide", Order: 30.01, Description: "Set and read the target frequency value which make a video trigger in Wideband.")]
        [Unit("Hz", UseEngineeringPrefix: true, StringFormat: "0.000000000")]
        public double TargetWideFrequency { get; set; }

        [EnabledIf("SelectedTransientTrigger", Transient_TriggerEnum.WB, HideIfDisabled = true)]
        [Display("Hysteresis", Group: "Wide", Order: 30.02, Description: "Set and read the hysteresis value for video trigger in Wideband.")]
        [Unit("Hz", UseEngineeringPrefix: true, StringFormat: "0.000")]
        public double HysteresisWideFrequency { get; set; }

        [EnabledIf("SelectedTransientTrigger", Transient_TriggerEnum.WB, HideIfDisabled = true)]
        [Display("Edge", Group: "Wide", Order: 30.03, Description: "Set and read the trigger edge from positive or negative for video trigger in Wideband.")]
        public Transient_SlopeEnum EdgeWide { get; set; }

        [EnabledIf("SelectedTransientTrigger", Transient_TriggerEnum.WB, HideIfDisabled = true)]
        [Display("Holdoff", Group: "Wide", Order: 30.04, Description: "Set and read the holdoff time for Video Trigger in Wide band.")]
        [Unit("S", UseEngineeringPrefix: true, StringFormat: "0.000")]
        public double HoldoffWide { get; set; }




        [EnabledIf("SelectedTransientTrigger", Transient_TriggerEnum.NB1, HideIfDisabled = true)]
        [Display("Trigger type", Group: "Narrow1", Order: 40.01, Description: "Set and read the trigger type for Narrow1 or Narrow2.")]
        public Transient_TriggerTypeEnum Narrow1TriggerType { get; set; }

        [EnabledIf("Narrow1TriggerType", Transient_TriggerTypeEnum.FREQuency, HideIfDisabled = true)]
        [EnabledIf("SelectedTransientTrigger", Transient_TriggerEnum.NB1, HideIfDisabled = true)]
        [Display("Target Frequency", Group: "Narrow1", Order: 40.02, Description: "Set and read the target frequency value which make a video trigger in Narrow band")]
        [Unit("Hz", UseEngineeringPrefix: true, StringFormat: "0.000000000")]
        public double Narrow1TargetNarrowFrequency { get; set; }

        [EnabledIf("Narrow1TriggerType", Transient_TriggerTypeEnum.POWer, HideIfDisabled = true)]
        [EnabledIf("SelectedTransientTrigger", Transient_TriggerEnum.NB1, HideIfDisabled = true)]
        [Display("Target Power", Group: "Narrow1", Order: 40.03, Description: "Set and read the target power value which make a video trigger in Narrow band.")]
        [Unit("dBm", UseEngineeringPrefix: true, StringFormat: "")]
        public double Narrow1TargetNarrowPower { get; set; }

        [EnabledIf("Narrow1TriggerType", Transient_TriggerTypeEnum.FREQuency, HideIfDisabled = true)]
        [EnabledIf("SelectedTransientTrigger", Transient_TriggerEnum.NB1, HideIfDisabled = true)]
        [Display("Hysteresis", Group: "Narrow1", Order: 40.04, Description: "Set and read the hysteresis value (frequency")]
        [Unit("Hz", UseEngineeringPrefix: true, StringFormat: "0.000")]
        public double Narrow1HysteresisNarrowFrequency { get; set; }

        [EnabledIf("Narrow1TriggerType", Transient_TriggerTypeEnum.POWer, HideIfDisabled = true)]
        [EnabledIf("SelectedTransientTrigger", Transient_TriggerEnum.NB1, HideIfDisabled = true)]
        [Display("Hysteresis", Group: "Narrow1", Order: 40.05, Description: "Set and read the hysteresis value (Power")]
        [Unit("dBm", UseEngineeringPrefix: true, StringFormat: "0.000")]
        public double Narrow1HysteresisNarrowPower { get; set; }

        [EnabledIf("SelectedTransientTrigger", Transient_TriggerEnum.NB1, HideIfDisabled = true)]
        [Display("Edge", Group: "Narrow1", Order: 40.06, Description: "Set and read the trigger edge from positive or negative for video trigger in Narrow band.")]
        public Transient_SlopeEnum Narrow1EdgeNarrow { get; set; }

        [EnabledIf("SelectedTransientTrigger", Transient_TriggerEnum.NB1, HideIfDisabled = true)]
        [Display("Min Input Level", Group: "Narrow1", Order: 40.07, Description: "Set and read the minimum power value which disables the video trigger in Narrow band. The trigger is disabled if the power level is below this level. The relative value from the maximum input level should be specified.")]
        [Unit("dB", UseEngineeringPrefix: true, StringFormat: "0.00")]
        public double Narrow1MinInputLevel { get; set; }

        [EnabledIf("SelectedTransientTrigger", Transient_TriggerEnum.NB1, HideIfDisabled = true)]
        [Display("Holdoff", Group: "Narrow1", Order: 40.08, Description: "Set and read the holdoff time for Video Trigger in Narrow band.")]
        [Unit("S", UseEngineeringPrefix: true, StringFormat: "0.000")]
        public double Narrow1HoldoffNarrow { get; set; }





        [EnabledIf("SelectedTransientTrigger", Transient_TriggerEnum.NB2, HideIfDisabled = true)]
        [Display("Trigger type", Group: "Narrow2", Order: 40.01, Description: "Set and read the trigger type for Narrow1 or Narrow2.")]
        public Transient_TriggerTypeEnum Narrow2TriggerType { get; set; }

        [EnabledIf("Narrow2TriggerType", Transient_TriggerTypeEnum.FREQuency, HideIfDisabled = true)]
        [EnabledIf("SelectedTransientTrigger", Transient_TriggerEnum.NB2, HideIfDisabled = true)]
        [Display("Target Frequency", Group: "Narrow2", Order: 40.02, Description: "Set and read the target frequency value which make a video trigger in Narrow band")]
        [Unit("Hz", UseEngineeringPrefix: true, StringFormat: "0.000000000")]
        public double Narrow2TargetNarrowFrequency { get; set; }

        [EnabledIf("Narrow2TriggerType", Transient_TriggerTypeEnum.POWer, HideIfDisabled = true)]
        [EnabledIf("SelectedTransientTrigger", Transient_TriggerEnum.NB2, HideIfDisabled = true)]
        [Display("Target Power", Group: "Narrow2", Order: 40.03, Description: "Set and read the target power value which make a video trigger in Narrow band.")]
        [Unit("dBm", UseEngineeringPrefix: true, StringFormat: "")]
        public double Narrow2TargetNarrowPower { get; set; }

        [EnabledIf("Narrow2TriggerType", Transient_TriggerTypeEnum.FREQuency, HideIfDisabled = true)]
        [EnabledIf("SelectedTransientTrigger", Transient_TriggerEnum.NB2, HideIfDisabled = true)]
        [Display("Hysteresis", Group: "Narrow2", Order: 40.04, Description: "Set and read the hysteresis value (frequency")]
        [Unit("Hz", UseEngineeringPrefix: true, StringFormat: "0.000")]
        public double Narrow2HysteresisNarrowFrequency { get; set; }

        [EnabledIf("Narrow2TriggerType", Transient_TriggerTypeEnum.POWer, HideIfDisabled = true)]
        [EnabledIf("SelectedTransientTrigger", Transient_TriggerEnum.NB2, HideIfDisabled = true)]
        [Display("Hysteresis", Group: "Narrow2", Order: 40.05, Description: "Set and read the hysteresis value (Power")]
        [Unit("dBm", UseEngineeringPrefix: true, StringFormat: "0.000")]
        public double Narrow2HysteresisNarrowPower { get; set; }

        [EnabledIf("SelectedTransientTrigger", Transient_TriggerEnum.NB2, HideIfDisabled = true)]
        [Display("Edge", Group: "Narrow2", Order: 40.06, Description: "Set and read the trigger edge from positive or negative for video trigger in Narrow band.")]
        public Transient_SlopeEnum Narrow2EdgeNarrow { get; set; }

        [EnabledIf("SelectedTransientTrigger", Transient_TriggerEnum.NB2, HideIfDisabled = true)]
        [Display("Min Input Level", Group: "Narrow2", Order: 40.07, Description: "Set and read the minimum power value which disables the video trigger in Narrow band. The trigger is disabled if the power level is below this level. The relative value from the maximum input level should be specified.")]
        [Unit("dB", UseEngineeringPrefix: true, StringFormat: "0.00")]
        public double Narrow2MinInputLevel { get; set; }

        [EnabledIf("SelectedTransientTrigger", Transient_TriggerEnum.NB2, HideIfDisabled = true)]
        [Display("Holdoff", Group: "Narrow2", Order: 40.08, Description: "Set and read the holdoff time for Video Trigger in Narrow band.")]
        [Unit("S", UseEngineeringPrefix: true, StringFormat: "0.000")]
        public double Narrow2HoldoffNarrow { get; set; }




        #endregion

        public GeneralTransientTrigger()
        {
            TransientTriggerTypeList = new List<string>() { "Wide", "Narrow1" };

            EnableVideoTrigger = false;
            SelectTrigger = "Wide";

            TargetWideFrequency = 1000000000;
            HysteresisWideFrequency = 1000;
            EdgeWide = Transient_SlopeEnum.POSitive;
            HoldoffWide = 0.001;

            Narrow1TriggerType = Transient_TriggerTypeEnum.FREQuency;
            Narrow1TargetNarrowFrequency = 1000000000;
            Narrow1TargetNarrowPower = 0;
            Narrow1HysteresisNarrowFrequency = 1000;
            Narrow1HysteresisNarrowPower = 0.01;
            Narrow1EdgeNarrow = Transient_SlopeEnum.POSitive;
            Narrow1MinInputLevel = -20;
            Narrow1HoldoffNarrow = 0.001;

            Narrow2TriggerType = Transient_TriggerTypeEnum.FREQuency;
            Narrow2TargetNarrowFrequency = 1000000000;
            Narrow2TargetNarrowPower = 0;
            Narrow2HysteresisNarrowFrequency = 1000;
            Narrow2HysteresisNarrowPower = 0.01;
            Narrow2EdgeNarrow = Transient_SlopeEnum.POSitive;
            Narrow2MinInputLevel = -20;
            Narrow2HoldoffNarrow = 0.001;
        }

        public override void Run()
        {
            SSAX.SetTransient_EnableVideoTrigger(Channel, EnableVideoTrigger);
            SSAX.SetTransient_SelectTrigger(Channel, SelectedTransientTrigger);

            if (SelectedTransientTrigger == Transient_TriggerEnum.WB)
            {
                SSAX.SetTransient_TargetWideFrequency(Channel, TargetWideFrequency);
                SSAX.SetTransient_HysteresisWideFrequency(Channel, HysteresisWideFrequency);
                SSAX.SetTransient_EdgeWide(Channel, EdgeWide);
                SSAX.SetTransient_HoldoffWide(Channel, HoldoffWide);
            }
            else if (SelectedTransientTrigger == Transient_TriggerEnum.NB1)
            {
                int band = 1;
                SSAX.SetTransient_TriggerType(Channel, band, Narrow1TriggerType);
                if (Narrow1TriggerType == Transient_TriggerTypeEnum.FREQuency)
                {
                    SSAX.SetTransient_TargetNarrowFrequency(Channel, band, Narrow1TargetNarrowFrequency);
                    SSAX.SetTransient_HysteresisNarrowFrequency(Channel, band, Narrow1HysteresisNarrowFrequency);
                }
                else
                {
                    SSAX.SetTransient_TargetNarrowPower(Channel, band, Narrow1TargetNarrowPower);
                    SSAX.SetTransient_HysteresisNarrowPower(Channel, band, Narrow1HysteresisNarrowPower);
                }
                SSAX.SetTransient_EdgeNarrow(Channel, band, Narrow1EdgeNarrow);
                SSAX.SetTransient_MinInputLevel(Channel, band, Narrow1MinInputLevel);
                SSAX.SetTransient_HoldoffNarrow(Channel, band, Narrow1HoldoffNarrow);
            }
            else if (SelectedTransientTrigger == Transient_TriggerEnum.NB2)
            {
                int band = 2;
                SSAX.SetTransient_TriggerType(Channel, band, Narrow2TriggerType);
                if (Narrow2TriggerType == Transient_TriggerTypeEnum.FREQuency)
                {
                    SSAX.SetTransient_TargetNarrowFrequency(Channel, band, Narrow2TargetNarrowFrequency);
                    SSAX.SetTransient_HysteresisNarrowFrequency(Channel, band, Narrow2HysteresisNarrowFrequency);
                }
                else
                {
                    SSAX.SetTransient_TargetNarrowPower(Channel, band, Narrow2TargetNarrowPower);
                    SSAX.SetTransient_HysteresisNarrowPower(Channel, band, Narrow2HysteresisNarrowPower);
                }
                SSAX.SetTransient_EdgeNarrow(Channel, band, Narrow2EdgeNarrow);
                SSAX.SetTransient_MinInputLevel(Channel, band, Narrow2MinInputLevel);
                SSAX.SetTransient_HoldoffNarrow(Channel, band, Narrow2HoldoffNarrow);
            }
            else
            {
                throw new Exception("Unknown Trigger Type!");
            }

            UpgradeVerdict(Verdict.Pass);
        }
    }
}
