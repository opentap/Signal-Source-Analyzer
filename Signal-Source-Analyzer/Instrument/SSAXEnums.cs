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

}
