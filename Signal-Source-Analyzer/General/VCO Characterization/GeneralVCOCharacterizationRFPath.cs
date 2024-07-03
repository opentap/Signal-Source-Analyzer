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
    public enum SSAXPortsEnum
    {
        [Display("Port 1")]
        [Scpi("1")]
        Port1 = 1,
        [Display("Port 2")]
        [Scpi("2")]
        Port2 = 2,
    }

    [Display("RF Path", Groups: new[] { "Signal Source Analyzer", "General", "VCO Characterization" }, Description: "Insert a description here")]
    public class GeneralVCOCharacterizationRFPath : SSAXBaseStep
    {
        #region Settings
        [Display("RF Input", Group: "Settings", Order: 20.01, Description: "Set and read the input port.")]
        public SSAXPortsEnum RFInput { get; set; }

        [Display("Max Input Level Auto", Group: "Settings", Order: 20.02, Description: "Enable and disable the auto leveling function at pre-measurement.")]
        public bool MaxInputLevelAuto { get; set; }

        [EnabledIf("MaxInputLevelAuto", false, HideIfDisabled = false)]
        [Display("Max Input Level", Group: "Settings", Order: 20.03, Description: "Set and read the maximum input level used to calculate attenuation setting in dBm.  This setting is not applicable when SENS:VCO:POW:INP:LEV:AUTO is ON")]
        [Unit("dBm", UseEngineeringPrefix: true, StringFormat: "0.00")]
        public double MaxInputLevel { get; set; }

        [Display("Enable Attenuator Setting", Group: "Settings", Order: 20.04, Description: "Enable and disable the auto calculate attenuation setting from INPut:LEVel:MAXimum. This setting is applicable when SENS:VCO:POW:INP:LEV:AUTO is set at OFF. Command only. This feature is not  available on the Soft Front Panel (GUI")]
        public bool EnableAttenuatorSetting { get; set; }

        [EnabledIf("EnableAttenuatorSetting", true, HideIfDisabled = false)]
        [Display("Attenuator", Group: "Settings", Order: 20.05, Description: "Set and read manual attenuation setting. This setting is applied when SENS:VCO:ATT:AUTO is set at OFF. Command only. This feature is not  available on the Soft Front Panel (GUI")]
        [Unit("dB", UseEngineeringPrefix: true)]
        public double Attenuator { get; set; }




        #endregion

        public GeneralVCOCharacterizationRFPath()
        {
            RFInput = SSAXPortsEnum.Port1;
            MaxInputLevel = 10;
            MaxInputLevelAuto = false;
            EnableAttenuatorSetting = false;
            Attenuator = 20;
        }

        public override void Run()
        {
            SSAX.SetVCOCharacterization_RFInput(Channel, RFInput);
            SSAX.SetVCOCharacterization_MaxInputLevel(Channel, MaxInputLevel);
            SSAX.SetVCOCharacterization_MaxInputLevelAuto(Channel, MaxInputLevelAuto);
            SSAX.SetVCOCharacterization_EnableAttenuatorSetting(Channel, !EnableAttenuatorSetting);
            SSAX.SetVCOCharacterization_Attenuator(Channel, Attenuator);

            UpgradeVerdict(Verdict.Pass);

        }
    }
}
