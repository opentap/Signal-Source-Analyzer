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
    [Display("RF Path", Groups: new[] { "Signal Source Analyzer", "General", "Transient" }, Description: "Insert a description here")]
    public class GeneralTransientRFPath : SSAXBaseStep
    {
        #region Settings

        [Display("RF Input", Group: "Settings", Order: 20.01, Description: "Set and read the input port for narrow band  1 and 2. Only one port can be selected in channel. Hence, the same value is set on Narrow 1,2 and Wide. The value is overwritten.")]
        public SSAXPortsEnum RFInput { get; set; }

        [Display("Max Input Level", Group: "Settings", Order: 20.03, Description: "Set and read the maximum power input level.")]
        [Unit("dBm", UseEngineeringPrefix: true, StringFormat: "0.00")]
        public double MaxInputLevel { get; set; }

        [Display("Enable Attenuator Setting", Group: "Settings", Order: 20.04, Description: "Set and read the on/off of receiver attenuation auto function")]
        public bool EnableAttenuatorSetting { get; set; }

        [EnabledIf("EnableAttenuatorSetting", true, HideIfDisabled = false)]
        [Unit("dB", UseEngineeringPrefix: true)]
        [Display("Attenuator", Group: "Settings", Order: 20.05, Description: "Set and read the receiver attenuation value")]
        public double Attenuator { get; set; }




        #endregion

        public GeneralTransientRFPath()
        {
            RFInput = SSAXPortsEnum.Port1;
            MaxInputLevel = 10;
            EnableAttenuatorSetting = false;
            Attenuator = 20;
        }

        public override void Run()
        {
            //SSAX.SetTransient_RFInputNarrow(Channel, 1, RFInput);
            //SSAX.SetTransient_RFInputNarrow(Channel, 2, RFInput);
            //SSAX.SetTransient_RFInputWide(Channel, RFInput);
            SSAX.SetTransient_MaxInputLevel(Channel, MaxInputLevel);
            SSAX.SetTransient_EnableAttenuatorSetting(Channel, !EnableAttenuatorSetting);
            SSAX.SetTransient_Attenuator(Channel, Attenuator);

            UpgradeVerdict(Verdict.Pass);
        }
    }
}
