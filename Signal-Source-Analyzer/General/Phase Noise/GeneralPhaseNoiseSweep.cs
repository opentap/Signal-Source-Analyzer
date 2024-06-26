// Author: CMontes
// Copyright:   Copyright 2024 Keysight Technologies
//              You have a royalty-free right to use, modify, reproduce and distribute
//              the sample application files (and/or any modified version) in any way
//              you find useful, provided that you agree that Keysight Technologies has no
//              warranty, obligations or liability for any sample application files.
using Microsoft.SqlServer.Server;
using OpenTap;
using OpenTap.Plugins.PNAX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signal_Source_Analyzer
{
    [Display("Sweep", Groups: new[] { "Signal Source Analyzer", "General", "Phase Noise" }, Description: "Insert a description here")]
    public class GeneralPhaseNoiseSweep : SSAXBaseStep
    {
        #region Settings
        [Display("Noise Type", Group: "Settings", Order: 20.1, Description: "Sets and returns the noise type to phase or residual noise")]
        public PhaseNoise_NoiseTypeEnum NoiseType { get; set; }

        [Display("Sweep Type", Group: "Settings", Order: 20.2, Description: "Sets and returns the phase noise sweep type to logarithmic or segment")]
        public PhaseNoise_SweepTypeEnum SweepType { get; set; }

        [Display("X CORR Gain Indicator", Group: "Settings", Order: 20.3, Description: "This command turns the cross-correlation gain indicator on and off.")]
        public bool XCORRGainIndicator { get; set; }

        [EnabledIf("SweepType", PhaseNoise_SweepTypeEnum.LOGarithmic, HideIfDisabled = true)]
        [Display("Start Offset", Group: "Measurement", Order: 30.1, Description: "Sets the start frequency of the analyzer")]
        [Unit("kHz", UseEngineeringPrefix: true, StringFormat: "0")]
        public double StartOffset { get; set; }

        [EnabledIf("SweepType", PhaseNoise_SweepTypeEnum.LOGarithmic, HideIfDisabled = true)]
        [Display("Stop Offset", Group: "Measurement", Order: 30.2, Description: "Sets the stop frequency of the analyzer")]
        [Unit("MHz", UseEngineeringPrefix: true, StringFormat: "0")]
        public double StopOffset { get; set; }

        [EnabledIf("SweepType", PhaseNoise_SweepTypeEnum.LOGarithmic, HideIfDisabled = true)]
        [Display("RBW Ratio", Group: "Measurement", Order: 30.3, Description: "Sets and returns the resolution bandwidth ratio, which is the specified resolution bandwidth percentage of every half decade offset frequency")]
        [Unit("%", UseEngineeringPrefix: true, StringFormat: "00.0")]
        public double RBWRatio { get; set; }

        [Display("XCORR Factor", Group: "Measurement", Order: 30.4, Description: "Sets and returns the cross correlation count number. As the correlation number is increased, the noise floor is reduced.")]
        public int XCORRFactor { get; set; }

        [Display("Fast XCORR Mode", Group: "Measurement", Order: 30.5, Description: "Selects the correlation mode from normal or fast")]
        public PhaseNoise_FastXCORRModeEnum FastXCORRMode { get; set; }

        [EnabledIf("EnableSearch", true, HideIfDisabled = true)]
        [Display("Carrier Frequency", Group: "Source", Order: 40.1, Description: "Sets and returns the carrier frequency")]
        [Unit("GHz", UseEngineeringPrefix: true, StringFormat: "0.000000000")]
        public double CarrierFrequency { get; set; }

        [Display("Enable Search", Group: "Source", Order: 40.2, Description: "Enables and disables a broadband carrier search within the range specified using the SENSe:PN:ADJust:CONFigure:FREQuency:LIMit:LOW and SENSe:PN:ADJust:CONFigure:FREQuency:LIMit:HIGH commands.")]
        public bool EnableSearch { get; set; }

        [Display("Enable Pulse", Group: "Source", Order: 40.3, Description: "Sets and returns the state of pulse modulation")]
        public bool EnablePulse { get; set; }

        [EnabledIf("EnablePulse", true, HideIfDisabled = true)]
        [Display("Pulse Period", Group: "Source", Order: 40.4, Description: "Sets and returns the period of pulse modulation")]
        [Unit("Sec", UseEngineeringPrefix: true, StringFormat: "0.0000")]
        public double PulsePeriod { get; set; }
        #endregion


        public GeneralPhaseNoiseSweep()
        {
            NoiseType = PhaseNoise_NoiseTypeEnum.PNOise;
            SweepType = PhaseNoise_SweepTypeEnum.LOGarithmic;
            XCORRGainIndicator = false;

            StartOffset = 1;
            StopOffset = 10;
            RBWRatio = 10;
            XCORRFactor = 1;
            FastXCORRMode = PhaseNoise_FastXCORRModeEnum.NORMal;

            CarrierFrequency = 1;
            EnableSearch = true;
            EnablePulse = false;
            PulsePeriod = 0.0001;
        }

        public override void Run()
        {
            RunChildSteps(); //If the step supports child steps.

            SSAX.SetPhaseNoise_NoiseType(Channel, NoiseType);
            SSAX.SetPhaseNoise_SweepType(Channel, SweepType);
            SSAX.SetPhaseNoise_XCORRGainIndicator(Channel, XCORRGainIndicator);

            SSAX.SetPhaseNoise_StartOffset(Channel, StartOffset);
            SSAX.SetPhaseNoise_StopOffset(Channel, StopOffset);
            SSAX.SetPhaseNoise_RBWRatio(Channel, RBWRatio);
            SSAX.SetPhaseNoise_XCORRFactor(Channel, XCORRFactor);
            SSAX.SetPhaseNoise_FastXCORRMode(Channel, FastXCORRMode);
            //SSAX.SetPhaseNoise_SegmentBandwidth(Channel,Segment,SegmentBandwidth);
            //SSAX.SetPhaseNoise_SegmentCorrelation(Channel,Segment,SegmentCorrelation);

            SSAX.SetPhaseNoise_CarrierFrequency(Channel, CarrierFrequency);
            SSAX.SetPhaseNoise_EnableSearch(Channel, EnableSearch);
            SSAX.SetPhaseNoise_EnablePulse(Channel, EnablePulse);
            SSAX.SetPhaseNoise_PulsePeriod(Channel, PulsePeriod);

            UpgradeVerdict(Verdict.Pass);
        }
    }
}
