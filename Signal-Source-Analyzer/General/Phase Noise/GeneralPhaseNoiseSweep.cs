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
    public class PhaseNoiseSegmentDefinition
    {
        [Display("Start Offset", Order: 1)]
        [Unit("Hz", UseEngineeringPrefix: true)]
        public double StartOffset { get; set; }
        [Display("Stop Offset", Order: 2)]
        [Unit("Hz", UseEngineeringPrefix: true)]
        public double StopOffset { get; set; }
        [Display("RBW", Order: 3)]
        [Unit("Hz", UseEngineeringPrefix: true)]
        public double RBW { get; set; }
        [Display("XCORR", Order: 4)]
        public int Xcorr { get; set; }
        [Display("Total XCORR", Order: 5)]
        public int TotalXcorr { get; set; }
    }

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
        [Unit("Hz", UseEngineeringPrefix: true, StringFormat: "0")]
        public double StartOffset { get; set; }

        [EnabledIf("SweepType", PhaseNoise_SweepTypeEnum.LOGarithmic, HideIfDisabled = true)]
        [Display("Stop Offset", Group: "Measurement", Order: 30.2, Description: "Sets the stop frequency of the analyzer")]
        [Unit("Hz", UseEngineeringPrefix: true, StringFormat: "0")]
        public double StopOffset { get; set; }

        [EnabledIf("SweepType", PhaseNoise_SweepTypeEnum.LOGarithmic, HideIfDisabled = true)]
        [Display("RBW Ratio", Group: "Measurement", Order: 30.3, Description: "Sets and returns the resolution bandwidth ratio, which is the specified resolution bandwidth percentage of every half decade offset frequency")]
        [Unit("%", UseEngineeringPrefix: true, StringFormat: "00.0")]
        public double RBWRatio { get; set; }

        [Display("XCORR Factor", Group: "Measurement", Order: 30.4, Description: "Sets and returns the cross correlation count number. As the correlation number is increased, the noise floor is reduced.")]
        public int XCORRFactor { get; set; }

        [Display("Fast XCORR Mode", Group: "Measurement", Order: 30.5, Description: "Selects the correlation mode from normal or fast")]
        public PhaseNoise_FastXCORRModeEnum FastXCORRMode { get; set; }

        [EnabledIf("NoiseType", PhaseNoise_NoiseTypeEnum.PNOise, HideIfDisabled = true)]
        [EnabledIf("EnableSearch", false, HideIfDisabled = true)]
        [Display("Carrier Frequency", Group: "Source", Order: 40.2, Description: "Sets and returns the carrier frequency")]
        [Unit("Hz", UseEngineeringPrefix: true, StringFormat: "0.000000000")]
        public double CarrierFrequency { get; set; }

        [EnabledIf("NoiseType", PhaseNoise_NoiseTypeEnum.PNOise, HideIfDisabled = true)]
        [Display("Enable Search", Group: "Source", Order: 40.1, Description: "Enables and disables a broadband carrier search within the range specified using the SENSe:PN:ADJust:CONFigure:FREQuency:LIMit:LOW and SENSe:PN:ADJust:CONFigure:FREQuency:LIMit:HIGH commands.")]
        public bool EnableSearch { get; set; }

        [EnabledIf("NoiseType", PhaseNoise_NoiseTypeEnum.PNOise, HideIfDisabled = true)]
        [Display("Enable Pulse", Group: "Source", Order: 40.3, Description: "Sets and returns the state of pulse modulation")]
        public bool EnablePulse { get; set; }

        [EnabledIf("NoiseType", PhaseNoise_NoiseTypeEnum.PNOise, HideIfDisabled = true)]
        [EnabledIf("EnablePulse", true, HideIfDisabled = true)]
        [Display("Pulse Period", Group: "Source", Order: 40.4, Description: "Sets and returns the period of pulse modulation")]
        [Unit("Sec", UseEngineeringPrefix: true, StringFormat: "0.0000")]
        public double PulsePeriod { get; set; }

        [EnabledIf("NoiseType", PhaseNoise_NoiseTypeEnum.PNOise, HideIfDisabled = true)]
        [EnabledIf("SweepType", PhaseNoise_SweepTypeEnum.SEGMent, HideIfDisabled = true)]
        [Display("Segment Setup", Group: "Measurement", Order: 30.1, Description: "Segment Sweep Setup")]
        public List<PhaseNoiseSegmentDefinition> SegmentSweepSetup { get; set; }

        #endregion


        public GeneralPhaseNoiseSweep()
        {
            NoiseType = PhaseNoise_NoiseTypeEnum.PNOise;
            SweepType = PhaseNoise_SweepTypeEnum.LOGarithmic;
            XCORRGainIndicator = false;

            StartOffset = 1e3;
            StopOffset = 10e6;
            RBWRatio = 10;
            XCORRFactor = 1;
            FastXCORRMode = PhaseNoise_FastXCORRModeEnum.NORMal;

            CarrierFrequency = 1e9;
            EnableSearch = true;
            EnablePulse = false;
            PulsePeriod = 0.0001;

            SegmentSweepSetup = new List<PhaseNoiseSegmentDefinition>();
            SegmentSweepSetup.Add(new PhaseNoiseSegmentDefinition { StartOffset = 1e3, StopOffset = 3e3, RBW = 300, Xcorr = 1, TotalXcorr = 1 });
            SegmentSweepSetup.Add(new PhaseNoiseSegmentDefinition { StartOffset = 3e3, StopOffset = 10e3, RBW = 300, Xcorr = 1, TotalXcorr = 1 });
            SegmentSweepSetup.Add(new PhaseNoiseSegmentDefinition { StartOffset = 10e3, StopOffset = 30e3, RBW = 1e3, Xcorr = 13, TotalXcorr = 13 });
            SegmentSweepSetup.Add(new PhaseNoiseSegmentDefinition { StartOffset = 30e3, StopOffset = 100e3, RBW = 3e3, Xcorr = 29, TotalXcorr = 29 });
            SegmentSweepSetup.Add(new PhaseNoiseSegmentDefinition { StartOffset = 100e3, StopOffset = 300e3, RBW = 10e3, Xcorr = 125, TotalXcorr = 125 });
            SegmentSweepSetup.Add(new PhaseNoiseSegmentDefinition { StartOffset = 300e3, StopOffset = 1e6, RBW = 30e3, Xcorr = 509, TotalXcorr = 509 });
            SegmentSweepSetup.Add(new PhaseNoiseSegmentDefinition { StartOffset = 1e6, StopOffset = 3e6, RBW = 100e3, Xcorr = 509, TotalXcorr = 509 });
            SegmentSweepSetup.Add(new PhaseNoiseSegmentDefinition { StartOffset = 3e6, StopOffset = 10e6, RBW = 300e3, Xcorr = 509, TotalXcorr = 509 });

        }

        public override void Run()
        {
            RunChildSteps(); //If the step supports child steps.

            SSAX.SetPhaseNoise_NoiseType(Channel, NoiseType);
            SSAX.SetPhaseNoise_SweepType(Channel, SweepType);
            SSAX.SetPhaseNoise_XCORRGainIndicator(Channel, XCORRGainIndicator);

            if (SweepType == PhaseNoise_SweepTypeEnum.LOGarithmic)
            {
                SSAX.SetPhaseNoise_StartOffset(Channel, StartOffset);
                SSAX.SetPhaseNoise_StopOffset(Channel, StopOffset);
                SSAX.SetPhaseNoise_RBWRatio(Channel, RBWRatio);
                SSAX.SetPhaseNoise_XCORRFactor(Channel, XCORRFactor);
                SSAX.SetPhaseNoise_FastXCORRMode(Channel, FastXCORRMode);
            }
            else
            {
                // First set START and STOP frequencies
                SSAX.SetPhaseNoise_StartOffset(Channel, SegmentSweepSetup[0].StartOffset);
                SSAX.SetPhaseNoise_StopOffset(Channel, SegmentSweepSetup[SegmentSweepSetup.Count-1].StopOffset);

                // Get the number of segments
                int SegmentCount = SegmentSweepSetup.Count;

                for (int i = 1; i <= SegmentCount; i++)
                {
                    // Freq Start and Stop can't be set, these are read only
                    double segmentFreqStart = SSAX.GetPhaseNoise_SegmentFreqStart(Channel, i);
                    double segmentFreqStop = SSAX.GetPhaseNoise_SegmentFreqStop(Channel, i);
                    // Validate Start and Stop with the values on the list
                    if (segmentFreqStart != SegmentSweepSetup[i - 1].StartOffset)
                    {
                        Log.Warning($"Segment {i} Start Offset {SegmentSweepSetup[i - 1].StartOffset} does not match expected frequency of {segmentFreqStart}");
                    }
                    if (segmentFreqStop != SegmentSweepSetup[i - 1].StopOffset)
                    {
                        Log.Warning($"Segment {i} Stop Offset {SegmentSweepSetup[i - 1].StopOffset} does not match expected frequency of {segmentFreqStop}");
                    }

                    SSAX.SetPhaseNoise_SegmentBandwidth(Channel, i, SegmentSweepSetup[i - 1].RBW);
                    SSAX.SetPhaseNoise_SegmentCorrelation(Channel, i, SegmentSweepSetup[i - 1].Xcorr);

                    //Log.Info($"{segmentFreqStart}\t{segmentFreqStop}\t{segmentRBW}\t{segmentXCorr}");
                }
            }

            ViewSegmentTable();

            if (NoiseType == PhaseNoise_NoiseTypeEnum.PNOise)
            {
                SSAX.SetPhaseNoise_CarrierFrequency(Channel, CarrierFrequency);
                SSAX.SetPhaseNoise_EnableSearch(Channel, EnableSearch);
                SSAX.SetPhaseNoise_EnablePulse(Channel, EnablePulse);
                SSAX.SetPhaseNoise_PulsePeriod(Channel, PulsePeriod);
            }

            UpgradeVerdict(Verdict.Pass);
        }

        public void ViewSegmentTable()
        {
            int SegmentCount = SSAX.GetPhaseNoise_SegmentCount(Channel, 1);
            Log.Info("Segment Count: " + SegmentCount);
            Log.Info($"Start Offset\tStop Offset\tRBW\tXCORR");
            for (int i = 1; i <= SegmentCount; i++)
            {
                double segmentFreqStart = SSAX.GetPhaseNoise_SegmentFreqStart(Channel, i);
                double segmentFreqStop = SSAX.GetPhaseNoise_SegmentFreqStop(Channel, i);
                double segmentRBW = SSAX.GetPhaseNoise_SegmentBandwidth(Channel, i);
                int segmentXCorr = SSAX.GetPhaseNoise_SegmentCorrelation(Channel, i);

                Log.Info($"{segmentFreqStart}\t{segmentFreqStop}\t{segmentRBW}\t{segmentXCorr}");
            }
        }
    }
}
