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
        [Display("Sweep Type", Group: "Settings", Order: 20.1, Description: "Set and read the sweep type.")]
        public VCOCharacterization_SweepTypeEnum SweepType { get; set; }

        [Display("VControl Start", Group: "Settings", Order: 20.2, Description: "Sets and returns start DC value for the specified DC source. This function is used to set the voltage even if the VContol or VSupply1/2 parameter is non swept. When the VContol or VSupply1/2 is not swept, use the same voltage for both start and stop.")]
        public double VControlStart { get; set; }

        [Display("VControl Center", Group: "Settings", Order: 20.3, Description: "Sets and returns center DC value for the specified DC source. This function is used to set the voltage even if the VContol or VSupply1/2 parameter is non swept. When the VContol or VSupply1/2 is not swept, use 0 for span. This value is changed automatically")]
        public double VControlCenter { get; set; }

        [Display("VControl Stop", Group: "Settings", Order: 20.4, Description: "Sets and returns stop DC value for the specified DC source. This function is used to set the voltage even if the VContol or VSupply1/2 parameter is non swept. When the VContol or VSupply1/2 is not swept, use the same voltage for both start and stop.")]
        public double VControlStop { get; set; }

        [Display("VControl Span", Group: "Settings", Order: 20.5, Description: "Sets and returns span value for the specified DC source. This function is used to set the voltage even if the VContol or VSupply1/2 parameter is non swept. When the VContol or VSupply1/2 is not swept, use 0 for span.")]
        public double VControlSpan { get; set; }

        [Display("Number of Points", Group: "Settings", Order: 20.6, Description: "Sets the number of data points for the measurement.")]
        public int NumberofPoints { get; set; }

        [Display("Dwell Time", Group: "Settings", Order: 20.7, Description: "Sets the dwell time between each sweep point. \nDwell time is ONLY available with SENSe:SWEep:GENeration set to STEPped; It is Not available in ANALOG.\nSending dwell = 0 is the same as setting SENS:SWE:DWEL:AUTO ON. Sending a dwell time > 0 sets SENS:SWE:DWEL:AUTO OFF.")]
        public double DwellTime { get; set; }

        [Display("Frequency Band", Group: "Settings", Order: 20.8, Description: "Set and read the frequency band.")]
        public VCOCharacterization_FrequencyBandEnum FrequencyBand { get; set; }

        [Display("Initial Frequency", Group: "Settings", Order: 20.9, Description: "Set and read the initial frequency for the frequency band Max Freq > 8 GHz.")]
        [Unit("Hz", UseEngineeringPrefix: true, StringFormat: "0.000000000")]
        public double InitialFrequency { get; set; }

        [Display("Freq Resolution", Group: "Settings", Order: 20.10, Description: "Set and read the frequency resolution.")]
        public VCOCharacterization_FreqResolutionEnum FreqResolution { get; set; }



        #endregion

        public GeneralVCOCharacterizationSweep()
        {
            SweepType = VCOCharacterization_SweepTypeEnum.VControl;
            VControlStart = 5;
            VControlStop = 5;
            VControlStop = 0;
            VControlSpan = 5;
            NumberofPoints = 201;
            DwellTime = 0;
            FrequencyBand = VCOCharacterization_FrequencyBandEnum.R8G;
            InitialFrequency = 8000000000;
            FreqResolution = VCOCharacterization_FreqResolutionEnum.R10;


        }

        public override void Run()
        {
            SSAX.SetVCOCharacterization_SweepType(SweepType);
            SSAX.SetVCOCharacterization_VControlStart(Channel, SweepType, VControlStart);
            SSAX.SetVCOCharacterization_VControlCenter(Channel, SweepType, VControlCenter);
            SSAX.SetVCOCharacterization_VControlStop(Channel, SweepType, VControlStop);
            SSAX.SetVCOCharacterization_VControlSpan(Channel, SweepType, VControlSpan);
            SSAX.SetVCOCharacterization_NumberofPoints(Channel, NumberofPoints);
            SSAX.SetVCOCharacterization_DwellTime(Channel, DwellTime);
            SSAX.SetVCOCharacterization_FrequencyBand(FrequencyBand);
            SSAX.SetVCOCharacterization_InitialFrequency(InitialFrequency);
            SSAX.SetVCOCharacterization_FreqResolution(FreqResolution);


        }
    }
}
