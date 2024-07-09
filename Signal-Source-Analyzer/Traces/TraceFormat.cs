// Author: CMontes
// Copyright:   Copyright 2024 Keysight Technologies
//              You have a royalty-free right to use, modify, reproduce and distribute
//              the sample application files (and/or any modified version) in any way
//              you find useful, provided that you agree that Keysight Technologies has no
//              warranty, obligations or liability for any sample application files.
using OpenTap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Signal_Source_Analyzer
{
    public enum TraceFormatTypeEnum
    {
        PhaseNoise,
        TransientFreq,
        TransientPhase,
        TransientPower,
        SpectrumAnalyzer,
        VCOCharacterizationFreq,
        VCOCharacterizationPower,
        VCOCharacterizationCurrent,
    }

    [AllowAsChildIn(typeof(SingleTraceBaseStep))]
    [Display("TraceFormat", Groups: new[] { "Signal Source Analyzer", "Trace"}, Description: "Insert a description here")]
    public class TraceFormat : SSAXBaseStep
    {
        #region Settings
        private string _Trace;
        [Browsable(false)]
        public string Trace
        {
            get
            {
                return _Trace;
            }
            set
            {
                _Trace = value;
                UpdateFormatOptions();
            }
        }

        private string _MeasClass;
        [Browsable(false)]
        public string MeasClass
        {
            get
            {
                return _MeasClass;
            }
            set
            {
                _MeasClass = value;
                UpdateFormatOptions();
            }
        }


        [Browsable(false)]
        public TraceFormatTypeEnum TraceFormatType { get; set; }

        [Browsable (false)]
        public int mnum {  get; set; }


        [EnabledIf("TraceFormatType", TraceFormatTypeEnum.SpectrumAnalyzer, HideIfDisabled = true)]
        [Display("Format", Groups: new[] { "Trace" }, Order: 20.01)]
        public SSAX.MeasurementFormatEnum SAMeasurementFormat { get; set; }

        [EnabledIf("TraceFormatType", TraceFormatTypeEnum.SpectrumAnalyzer, HideIfDisabled = true)]
        [EnabledIf("SAMeasurementFormat", SSAX.MeasurementFormatEnum.MLOGarithmic, HideIfDisabled = true)]
        [Display("Units", Groups: new[] { "Trace" }, Order: 20.02)]
        public PowerLogMeasurementFormatUnitsEnum SAFormatLogUnits { get; set; }

        [EnabledIf("TraceFormatType", TraceFormatTypeEnum.SpectrumAnalyzer, HideIfDisabled = true)]
        [EnabledIf("SAMeasurementFormat", SSAX.MeasurementFormatEnum.MLINear, HideIfDisabled = true)]
        [Display("Units", Groups: new[] { "Trace" }, Order: 20.02)]
        public PowerLinMeasurementFormatUnitsEnum SAFormatLinUnits { get; set; }




        [EnabledIf("TraceFormatType", TraceFormatTypeEnum.VCOCharacterizationFreq, HideIfDisabled = true)]
        [Display("Format", Groups: new[] { "Trace" }, Order: 20.01)]
        public VCOFreqMeasurementFormatEnum VcoFormat { get; set; }

        [EnabledIf("VcoFormat", VCOFreqMeasurementFormatEnum.DFRequency, HideIfDisabled = true)]
        [Display("Units", Groups: new[] { "Trace" }, Order: 20.02)]
        public FreqMeasurementFormatUnitsEnum VcoFormatDFRUnits { get; set; }





        [EnabledIf("TraceFormatType", TraceFormatTypeEnum.VCOCharacterizationPower, HideIfDisabled = true)]
        [Display("Format", Groups: new[] { "Trace" }, Order: 20.01)]
        public PowerMeasurementFormatEnum VcoFormatPower { get; set; }

        [EnabledIf("TraceFormatType", TraceFormatTypeEnum.VCOCharacterizationPower, HideIfDisabled = true)]
        [EnabledIf("VcoFormatPower", PowerMeasurementFormatEnum.MLOGarithmic, HideIfDisabled = true)]
        [Display("Units", Groups: new[] { "Trace" }, Order: 20.02)]
        public PowerLogMeasurementFormatUnitsEnum VcoFormatLogUnits { get; set; }

        [EnabledIf("TraceFormatType", TraceFormatTypeEnum.VCOCharacterizationPower, HideIfDisabled = true)]
        [EnabledIf("VcoFormatPower", PowerMeasurementFormatEnum.MLINear, HideIfDisabled = true)]
        [Display("Units", Groups: new[] { "Trace" }, Order: 20.02)]
        public PowerLinMeasurementFormatUnitsEnum VcoFormatLinUnits { get; set; }


        [EnabledIf("TraceFormatType", TraceFormatTypeEnum.VCOCharacterizationCurrent, HideIfDisabled = true)]
        [Display("Format", Groups: new[] { "Trace" }, Order: 20.01)]
        public CurrentMeasurementFormatEnum VcoFormatCurrent { get; set; }



        [EnabledIf("TraceFormatType", TraceFormatTypeEnum.TransientFreq, HideIfDisabled = true)]
        [Display("Format", Groups: new[] { "Trace" }, Order: 20.01)]
        public TransientFreqMeasurementFormatEnum TransientFormatFreq { get; set; }

        [EnabledIf("TraceFormatType", TraceFormatTypeEnum.TransientPhase, HideIfDisabled = true)]
        [Display("Format", Groups: new[] { "Trace" }, Order: 20.01)]
        public PhaseMeasurementFormatEnum TransientFormatPhase { get; set; }

        [EnabledIf("TraceFormatType", TraceFormatTypeEnum.TransientPower, HideIfDisabled = true)]
        [Display("Format", Groups: new[] { "Trace" }, Order: 20.01)]
        public PowerMeasurementFormatEnum TransientFormatPower { get; set; }

        [EnabledIf("TransientFormatFreq", TransientFreqMeasurementFormatEnum.DFRequency, HideIfDisabled = true)]
        [Display("Units", Groups: new[] { "Trace" }, Order: 20.02)]
        public FreqMeasurementFormatUnitsEnum TransientFormatDFRUnits { get; set; }

        [EnabledIf("TraceFormatType", TraceFormatTypeEnum.TransientPhase, HideIfDisabled = true)]
        [EnabledIf("TransientFormatPhase", PhaseMeasurementFormatEnum.PHASe, HideIfDisabled = true)]
        [Display("Units", Groups: new[] { "Trace" }, Order: 20.02)]
        public PhaseMeasurementFormatUnitsEnum TransientFormatPhaseUnits { get; set; }

        [EnabledIf("TraceFormatType", TraceFormatTypeEnum.TransientPhase, HideIfDisabled = true)]
        [EnabledIf("TransientFormatPhase", PhaseMeasurementFormatEnum.UPHase, HideIfDisabled = true)]
        [Display("Units", Groups: new[] { "Trace" }, Order: 20.02)]
        public PhaseMeasurementFormatUnitsEnum TransientFormatUnwrappedPhaseUnits { get; set; }

        [EnabledIf("TraceFormatType", TraceFormatTypeEnum.TransientPhase, HideIfDisabled = true)]
        [EnabledIf("TransientFormatPhase", PhaseMeasurementFormatEnum.PPHase, HideIfDisabled = true)]
        [Display("Units", Groups: new[] { "Trace" }, Order: 20.02)]
        public PhaseMeasurementFormatUnitsEnum TransientFormatPositivePhaseUnits { get; set; }

        [EnabledIf("TraceFormatType", TraceFormatTypeEnum.TransientPower, HideIfDisabled = true)]
        [EnabledIf("TransientFormatPower", PowerMeasurementFormatEnum.MLOGarithmic, HideIfDisabled = true)]
        [Display("Units", Groups: new[] { "Trace" }, Order: 20.02)]
        public PowerLogMeasurementFormatUnitsEnum TransientFormatLogUnits { get; set; }

        [EnabledIf("TraceFormatType", TraceFormatTypeEnum.TransientPower, HideIfDisabled = true)]
        [EnabledIf("TransientFormatPower", PowerMeasurementFormatEnum.MLINear, HideIfDisabled = true)]
        [Display("Units", Groups: new[] { "Trace" }, Order: 20.02)]
        public PowerLinMeasurementFormatUnitsEnum TransientFormatLinUnits { get; set; }


        #endregion
        [Browsable(true)]
        [EnabledIf("TraceFormatType", TraceFormatTypeEnum.TransientPhase, HideIfDisabled = true)]
        [Display("Add Phase Reference", Groups: new[] { "Trace" }, Order: 21.01)]
        public void AddPhaseReference()
        {
            ChildTestSteps.Add(new GeneralPhaseReference() { SSAX = SSAX, Channel = Channel, IsControlledByParent = true });
        }


        private void UpdateFormatOptions()
        {
            if (MeasClass == null) return;
            if (Trace == null) return;

            if (MeasClass.Equals("Transient"))
            {
                if (Trace.Contains("Freq"))
                {
                    TraceFormatType = TraceFormatTypeEnum.TransientFreq;
                }
                else if (Trace.Contains("Phase"))
                {
                    TraceFormatType = TraceFormatTypeEnum.TransientPhase;
                }
                else if (Trace.Contains("Power"))
                {
                    TraceFormatType = TraceFormatTypeEnum.TransientPower;
                }
                else
                {
                    throw new Exception("Unknown Trace Type");
                }
            }
            else if (MeasClass.Equals("Spectrum Analyzer"))
            {
                // LogMag
                //LinMag
                // Phase, Delay, Smith, Polar, SWR, Real, Imaginary, Unwrapped Phase, Positive Phase, Inverted Smith, Temeprature
                TraceFormatType = TraceFormatTypeEnum.SpectrumAnalyzer;

            }
            else if (MeasClass.Equals("VCO Characterization"))
            {
                if (Trace.Contains("Freq"))
                {
                    // FREQ
                    // FREQ/V
                    // Units
                    TraceFormatType = TraceFormatTypeEnum.VCOCharacterizationFreq;
                }
                else if (Trace.Contains("Power"))
                {
                    TraceFormatType = TraceFormatTypeEnum.VCOCharacterizationPower;
                }
                else if (Trace.Contains("Current"))
                {
                    TraceFormatType = TraceFormatTypeEnum.VCOCharacterizationCurrent;
                }
                else
                {
                    throw new Exception("Unknown Trace Type");
                }
            }
            else if (MeasClass.Equals("Phase Noise"))
            {
                // Nothing for Format
                TraceFormatType = TraceFormatTypeEnum.PhaseNoise;
            }
        }

        public TraceFormat()
        {
            IsControlledByParent = true;

            SAMeasurementFormat = SSAX.MeasurementFormatEnum.MLOGarithmic;
            SAFormatLogUnits = PowerLogMeasurementFormatUnitsEnum.DBM;
            SAFormatLinUnits = PowerLinMeasurementFormatUnitsEnum.W;

            VcoFormat = VCOFreqMeasurementFormatEnum.FREQuency;
            VcoFormatDFRUnits = FreqMeasurementFormatUnitsEnum.HZ;
            VcoFormatPower = PowerMeasurementFormatEnum.MLOGarithmic;
            VcoFormatLogUnits = PowerLogMeasurementFormatUnitsEnum.DBM;
            VcoFormatLinUnits = PowerLinMeasurementFormatUnitsEnum.W;
            VcoFormatCurrent = CurrentMeasurementFormatEnum.REAL;

            TransientFormatFreq = TransientFreqMeasurementFormatEnum.FREQuency;
            TransientFormatDFRUnits = FreqMeasurementFormatUnitsEnum.HZ;

            TransientFormatPhase = PhaseMeasurementFormatEnum.PHASe;
            TransientFormatPhaseUnits = PhaseMeasurementFormatUnitsEnum.DEG;
            TransientFormatUnwrappedPhaseUnits = PhaseMeasurementFormatUnitsEnum.DEG;
            TransientFormatPositivePhaseUnits = PhaseMeasurementFormatUnitsEnum.DEG;

            TransientFormatPower = PowerMeasurementFormatEnum.MLOGarithmic;
            TransientFormatLogUnits = PowerLogMeasurementFormatUnitsEnum.DBM;
            TransientFormatLinUnits = PowerLinMeasurementFormatUnitsEnum.W;

        }

        public override void Run()
        {
            //Channel = GetParent<GeneralSingleTraceBaseStep>().Channel;
            mnum  = GetParent<SingleTraceBaseStep>().mnum;

            if (TraceFormatType == TraceFormatTypeEnum.TransientFreq)
            {
                SSAX.SetTraceFormat(Channel, mnum, TransientFormatFreq);
                if (TransientFormatFreq == TransientFreqMeasurementFormatEnum.DFRequency)
                {
                    SSAX.SetTraceUnits(Channel, mnum, DataFormatEnum.DFR.ToString(), TransientFormatDFRUnits);
                }
            }
            else if (TraceFormatType == TraceFormatTypeEnum.TransientPhase)
            {
                SSAX.SetTraceFormat(Channel, mnum, TransientFormatPhase);
                switch (TransientFormatPhase)
                {
                    case PhaseMeasurementFormatEnum.PHASe:
                        SSAX.SetTraceUnits(Channel, mnum, DataFormatEnum.PHAS.ToString(), TransientFormatPhaseUnits);
                        break;
                    case PhaseMeasurementFormatEnum.UPHase:
                        SSAX.SetTraceUnits(Channel, mnum, DataFormatEnum.UPH.ToString(), TransientFormatUnwrappedPhaseUnits);
                        break;
                    case PhaseMeasurementFormatEnum.PPHase:
                        SSAX.SetTraceUnits(Channel, mnum, DataFormatEnum.PPH.ToString(), TransientFormatPositivePhaseUnits);
                        break;
                    default:
                        throw new Exception("unkonw format phase units");
                }
            }
            else if (TraceFormatType == TraceFormatTypeEnum.TransientPower)
            {
                SSAX.SetTraceFormat(Channel, mnum, TransientFormatPower);
                if (TransientFormatPower == PowerMeasurementFormatEnum.MLOGarithmic)
                {
                    SSAX.SetTraceUnits(Channel, mnum, DataFormatEnum.MLOG.ToString(), TransientFormatLogUnits);
                }
                else if (TransientFormatPower == PowerMeasurementFormatEnum.MLINear)
                {
                    SSAX.SetTraceUnits(Channel, mnum, DataFormatEnum.MLIN.ToString(), TransientFormatLinUnits);
                }
                else
                {
                    throw new Exception("unknown format power units");
                }
            }
            else if (TraceFormatType == TraceFormatTypeEnum.SpectrumAnalyzer)
            {
                SSAX.SetTraceFormat(Channel, mnum, SAMeasurementFormat);
                if (SAMeasurementFormat == SSAX.MeasurementFormatEnum.MLOGarithmic)
                {
                    SSAX.SetTraceUnits(Channel, mnum, DataFormatEnum.MLOG.ToString(), SAFormatLogUnits);
                }
                else if (SAMeasurementFormat == SSAX.MeasurementFormatEnum.MLINear)
                {
                    SSAX.SetTraceUnits(Channel, mnum, DataFormatEnum.MLIN.ToString(), SAFormatLinUnits);
                }
            }
            else if (TraceFormatType == TraceFormatTypeEnum.VCOCharacterizationFreq)
            {
                SSAX.SetTraceFormat(Channel, mnum, VcoFormat);
                if (VcoFormat == VCOFreqMeasurementFormatEnum.DFRequency)
                {
                    SSAX.SetTraceUnits(Channel, mnum, DataFormatEnum.DFR.ToString(), VcoFormatDFRUnits);
                }
            }
            else if (TraceFormatType == TraceFormatTypeEnum.VCOCharacterizationPower)
            {
                SSAX.SetTraceFormat(Channel, mnum, VcoFormatPower);
                if (VcoFormatPower == PowerMeasurementFormatEnum.MLOGarithmic)
                {
                    SSAX.SetTraceUnits(Channel, mnum, DataFormatEnum.MLOG.ToString(), VcoFormatLogUnits);
                }
                else if (VcoFormatPower == PowerMeasurementFormatEnum.MLINear)
                {
                    SSAX.SetTraceUnits(Channel, mnum, DataFormatEnum.MLIN.ToString(), VcoFormatLinUnits);
                }
            }
            else if (TraceFormatType == TraceFormatTypeEnum.VCOCharacterizationCurrent)
            {
                SSAX.SetTraceFormat(Channel, mnum, VcoFormatCurrent);
            }

            RunChildSteps(); //If the step supports child steps.

            UpgradeVerdict(Verdict.Pass);
        }
    }
}
