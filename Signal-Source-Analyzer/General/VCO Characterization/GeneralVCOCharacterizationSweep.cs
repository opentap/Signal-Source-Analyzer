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
    [Display("Sweep", Groups: new[] { "Signal Source Analyzer", "General", "VCO Characterization" }, Description: "Insert a description here")]
    public class GeneralVCOCharacterizationSweep : SSAXBaseStep
    {
        #region Settings
        [Display("Sweep Type", Group: "Settings", Order: 20.01, Description: "Set and read the sweep type.")]
        public VCOCharacterization_SweepTypeEnum SweepType { get; set; }

        [Display("Type", Group: "Settings", Order: 20.011)]
        public SweepSSCSTypeEnum IsStartStopCenterSpan { get; set; }

        [EnabledIf("IsStartStopCenterSpan", SweepSSCSTypeEnum.StartStop, HideIfDisabled = true)]
        [Unit("V", UseEngineeringPrefix: true)]
        [Display("VControl Start", Group: "Settings", Order: 20.02, Description: "Sets and returns start DC value for the specified DC source. This function is used to set the voltage even if the VContol or VSupply1/2 parameter is non swept. When the VContol or VSupply1/2 is not swept, use the same voltage for both start and stop.")]
        public double VControlStart { get; set; }

        [EnabledIf("IsStartStopCenterSpan", SweepSSCSTypeEnum.CenterSpan, HideIfDisabled = true)]
        [Unit("V", UseEngineeringPrefix: true)]
        [Display("VControl Center", Group: "Settings", Order: 20.03, Description: "Sets and returns center DC value for the specified DC source. This function is used to set the voltage even if the VContol or VSupply1/2 parameter is non swept. When the VContol or VSupply1/2 is not swept, use 0 for span. This value is changed automatically")]
        public double VControlCenter { get; set; }

        [EnabledIf("IsStartStopCenterSpan", SweepSSCSTypeEnum.StartStop, HideIfDisabled = true)]
        [Unit("V", UseEngineeringPrefix: true)]
        [Display("VControl Stop", Group: "Settings", Order: 20.04, Description: "Sets and returns stop DC value for the specified DC source. This function is used to set the voltage even if the VContol or VSupply1/2 parameter is non swept. When the VContol or VSupply1/2 is not swept, use the same voltage for both start and stop.")]
        public double VControlStop { get; set; }

        [EnabledIf("IsStartStopCenterSpan", SweepSSCSTypeEnum.CenterSpan, HideIfDisabled = true)]
        [Unit("V", UseEngineeringPrefix: true)]
        [Display("VControl Span", Group: "Settings", Order: 20.05, Description: "Sets and returns span value for the specified DC source. This function is used to set the voltage even if the VContol or VSupply1/2 parameter is non swept. When the VContol or VSupply1/2 is not swept, use 0 for span.")]
        public double VControlSpan { get; set; }

        [Display("Number of Points", Group: "Settings", Order: 20.06, Description: "Sets the number of data points for the measurement.")]
        public int NumberofPoints { get; set; }

        [Display("Dwell Time", Group: "Settings", Order: 20.07, Description: "Sets the dwell time between each sweep point. \nDwell time is ONLY available with SENSe:SWEep:GENeration set to STEPped; It is Not available in ANALOG.\nSending dwell = 0 is the same as setting SENS:SWE:DWEL:AUTO ON. Sending a dwell time > 0 sets SENS:SWE:DWEL:AUTO OFF.")]
        [Unit("Sec", UseEngineeringPrefix: true)]
        public double DwellTime { get; set; }

        [Display("Frequency Band", Group: "Settings", Order: 20.08, Description: "Set and read the frequency band.")]
        public VCOCharacterization_FrequencyBandEnum FrequencyBand { get; set; }

        [Display("Initial Frequency", Group: "Settings", Order: 20.09, Description: "Set and read the initial frequency for the frequency band Max Freq > 8 GHz.")]
        [Unit("Hz", UseEngineeringPrefix: true, StringFormat: "0.000000000")]
        public double InitialFrequency { get; set; }

        [Display("Freq Resolution", Group: "Settings", Order: 20.10, Description: "Set and read the frequency resolution.")]
        public VCOCharacterization_FreqResolutionEnum FreqResolution { get; set; }





        #endregion

        public GeneralVCOCharacterizationSweep()
        {
            SweepType = VCOCharacterization_SweepTypeEnum.VControl;
            IsStartStopCenterSpan = SweepSSCSTypeEnum.StartStop;
            VControlStart = 0.5;
            VControlStop = 1;
            VControlCenter = 0.75;
            VControlSpan = 0.5;
            NumberofPoints = 201;
            DwellTime = 0;
            FrequencyBand = VCOCharacterization_FrequencyBandEnum.R8G;
            InitialFrequency = 8000000000;
            FreqResolution = VCOCharacterization_FreqResolutionEnum.R64K;


        }

        public override void Run()
        {
            SSAX.SetVCOCharacterization_SweepType(Channel, SweepType);
            if (IsStartStopCenterSpan == SweepSSCSTypeEnum.StartStop)
            {
                SSAX.SetVCOCharacterization_VControlStart(Channel, SweepType, VControlStart);
                SSAX.SetVCOCharacterization_VControlStop(Channel, SweepType, VControlStop);
            }
            else
            {
                SSAX.SetVCOCharacterization_VControlCenter(Channel, SweepType, VControlCenter);
                SSAX.SetVCOCharacterization_VControlSpan(Channel, SweepType, VControlSpan);
            }
            SSAX.SetVCOCharacterization_NumberofPoints(Channel, NumberofPoints);
            SSAX.SetVCOCharacterization_DwellTime(Channel, DwellTime);
            SSAX.SetVCOCharacterization_FrequencyBand(Channel, FrequencyBand);
            SSAX.SetVCOCharacterization_InitialFrequency(Channel, InitialFrequency);
            SSAX.SetVCOCharacterization_FreqResolution(Channel, FreqResolution);

            UpgradeVerdict(Verdict.Pass);

        }
    }
}
