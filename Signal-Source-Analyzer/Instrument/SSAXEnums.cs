using OpenTap;
using OpenTap.Plugins.PNAX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signal_Source_Analyzer
{
    public enum TransientFreqMeasurementFormatEnum
    {
        [Display("Frequency")]
        FREQuency,
        [Display("Δ Frequency")]
        DFRequency,
    }

    public enum VCOFreqMeasurementFormatEnum
    {
        [Display("Frequency")]
        FREQuency,
        [Display("Freq/V")]
        FSENsitivity,
        [Display("Δ Frequency")]
        DFRequency,
    }

    public enum PhaseMeasurementFormatEnum
    {
        [Display("Phase")]
        PHASe,
        [Display("Unwrapped Phase")]
        UPHase,
        [Display("Positive Phase")]
        PPHase,
    }

    public enum PowerMeasurementFormatEnum
    {
        [Display("Lin Mag")]
        MLINear,
        [Display("Log Mag")]
        MLOGarithmic,
    }

    public enum CurrentMeasurementFormatEnum
    {
        [Display("Real")]
        REAL,
    }

    public enum FreqMeasurementFormatUnitsEnum
    {
        [Display("Δ Hz")]
        HZ,
        [Display("%")]
        PERCentage,
        [Display("ppm")]
        PPM,
    }

    public enum PhaseMeasurementFormatUnitsEnum
    {
        [Display("Degree")]
        DEG,
        [Display("Radian")]
        RAD,
        [Display("Gradian")]
        GRAD,
    }

    public enum PowerLinMeasurementFormatUnitsEnum
    {
        [Display("W")]
        W,
        [Display("V")]
        V,
        [Display("A")]
        A,
    }

    public enum PowerLogMeasurementFormatUnitsEnum
    {
        [Display("dBm")]
        DBM,
        [Display("dBmV")]
        DBMV,
        [Display("dBuV")]
        DBUV,
        [Display("dBmA")]
        dBMA,
    }

    public enum DataFormatEnum
    {
        MLOG,
        MLIN,
        DFR,
        PHAS,
        UPH,
        PPH,
    }

    public enum PhaseNoise_AnalyzerInputEnum
    {
        [Scpi("a1")]
        [Display("a1")]
        a1,
        [Scpi("a2")]
        [Display("a2")]
        a2,
        [Scpi("a3")]
        [Display("a3")]
        a3,
        [Scpi("a4")]
        [Display("a4")]
        a4,
        [Scpi("b1")]
        [Display("b1")]
        b1,
        [Scpi("b2")]
        [Display("b2")]
        b2,
        [Scpi("b3")]
        [Display("b3")]
        b3,
        [Scpi("b4")]
        [Display("b4")]
        b4,
        [Scpi("b2/a1")]
        [Display("b2/a1")]
        b2a1,
        [Scpi("b1/a2")]
        [Display("b1/a2")]
        b1a2,
    }

    public enum PhaseNoise_BasebandInputEnum
    {
        [Scpi("ACLF")]
        [Display("AC(LF)")]
        ACLF,
        [Scpi("ACHF")]
        [Display("AC(HF)")]
        ACHF,
    }

    public enum PhaseNoise_RFPathConfigurationEnum
    {
        [Scpi("PulseTrigInput")]
        [Display("PulseTrigInput")]
        PulseTrigInput,
        [Scpi("IFGAINb1")]
        [Display("IFGAINb1")]
        IFGAINb1,
    }

    public enum PhaseNoise_TableSortOrderEnum
    {
        [Scpi("POWer ")]
        [Display("Power")]
        POWer,
        [Scpi("OFFSet ")]
        [Display("Offset")]
        OFFSet,
    }

    public enum PhaseNoise_IntegratedRangeTypeEnum
    {
        [Scpi("OFF")]
        [Display("Off")]
        OFF,
        [Scpi("FULL")]
        [Display("Full")]
        FULL,
        [Scpi("CUSTom")]
        [Display("Custom")]
        CUSTom,
    }


}
