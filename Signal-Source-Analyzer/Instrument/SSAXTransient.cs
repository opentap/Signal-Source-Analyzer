using OpenTap;
using OpenTap.Plugins.PNAX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Signal_Source_Analyzer
{
    public enum Transient_SweepTypeEnum
    {
        [Scpi("WN")]
        [Display("Wide-Narrow")]
        WN,
        [Scpi("NN")]
        [Display("Narrow-Narrow")]
        NN,
    }

    public enum Transient_FrequencyRangeEnum
    {
        [Scpi("R4")]
        [Display("40 MHz – 160 MHz")]
        R4,
        [Scpi("R8")]
        [Display("80 MHz – 320 MHz")]
        R8,
        [Scpi("R16")]
        [Display("160 MHz – 640 MHz")]
        R16,
        [Scpi("R32")]
        [Display("320 MHz – 1.28 GHz")]
        R32,
        [Scpi("R64")]
        [Display("640 MHz – 2.56 GHz")]
        R64,
        [Scpi("R128")]
        [Display("1.28 GHz – 5.12 GHz")]
        R128,
        [Scpi("R256")]
        [Display("2.56 GHz – 8 GHz")]
        R256,
        [Scpi("R40M")]
        [Display("1 MHz – 40 MHz")]
        R40M,
        [Scpi("R8G")]
        [Display("256 MHz – 8 GHz")]
        R8G,
        [Scpi("RMAX")]
        [Display("Max.Freq. > 8 GHz")]
        RMAX,
    }

    public enum Transient_ReferenceEnum
    {
        [Scpi("LEFT")]
        [Display("Left")]
        LEFT,
        [Scpi("CENTer")]
        [Display("Center")]
        CENTer,
        [Scpi("RIGHt")]
        [Display("Right")]
        RIGHt,
    }

    public enum Transient_FrequencySpanEnum
    {
        [Scpi("R3K")]
        [Display("3.125 kHz")]
        R3K,
        [Scpi("R25K")]
        [Display("25 kHz")]
        R25K,
        [Scpi("R0_3")]
        [Display("312.5 kHz")]
        R0_3,
        [Scpi("R2_5")]
        [Display("2.5 MHz")]
        R2_5,
        [Scpi("R10")]
        [Display("10 MHz")]
        R10,
        [Scpi("R30")]
        [Display("30 MHz")]
        R30,
        [Scpi("R80")]
        [Display("80 MHz")]
        R80,
        [Scpi("R160")]
        [Display("160 MHz")]
        R160,
        [Scpi("R320")]
        [Display("320 MHz")]
        R320,
    }




    public partial class SSAX : PNAX
    {
        public void SetTransient_SweepType(int Channel, Transient_SweepTypeEnum SweepType)
        {
            string SweepTypeSCPI = Scpi.Format("{0}", SweepType);
            ScpiCommand($"SENSe{Channel}:TR:STYPe  {SweepTypeSCPI}");
        }

        public Transient_SweepTypeEnum GetTransient_SweepType(int Channel)
        {
            return ScpiQuery<Transient_SweepTypeEnum>($"SENSe:TR:STYpe?");
        }

        public void SetTransient_TimeCoupling(int Channel, bool State)
        {
            string StateValue = State ? "1" : "0";
            ScpiCommand($"SENSe{Channel}:TR:TIME:COUPling:STATe  {StateValue}");
        }

        public bool GetTransient_TimeCoupling(int Channel)
        {
            return ScpiQuery<bool>($"SENSe:TR:TIME:COUPling:STATe?");
        }

        public void SetTransient_NumberOfPoints(int Channel, int value)
        {
            ScpiCommand($"SENSe{Channel}:SWEep:POINts {value}");
        }

        public int GetTransient_NumberOfPoints(int Channel)
        {
            return ScpiQuery<int>($"SENSe{Channel}:SWEep:POINts?");
        }

        public void SetTransient_FrequencyRange(int Channel, Transient_FrequencyRangeEnum FrequencyRange)
        {
            string FrequencyRangeSCPI = Scpi.Format("{0}", FrequencyRange);
            ScpiCommand($"SENSe{Channel}:TR:WIDE:FREQuency:RANGe  {FrequencyRangeSCPI}");
        }

        public Transient_FrequencyRangeEnum GetTransient_FrequencyRange(int Channel)
        {
            return ScpiQuery<Transient_FrequencyRangeEnum>($"SENSe:TR:WIDE:FREQuency:RANGe?");
        }

        public void SetTransient_MaxFrequency(int Channel, double value)
        {
            ScpiCommand($"SENSe{Channel}:TR:WIDE:FREQuency:MAXimum  {value}");
        }

        public double GetTransient_MaxFrequency(int Channel)
        {
            return ScpiQuery<double>($"SENSe:TR:WIDE:FREQuency:MAXimum?");
        }

        public double GetTransient_MinFrequency(int Channel)
        {
            return ScpiQuery<double>($"SENSe:TR:WIDE:FREQuency:MINimum?");
        }

        public void SetTransient_WideTimeSpan(int Channel, double value)
        {
            ScpiCommand($"SENSe{Channel}:TR:WIDE:TIME:SPAN  {value}");
        }

        public double GetTransient_WideTimeSpan(int Channel)
        {
            return ScpiQuery<double>($"SENSe:TR:WIDE:TIME:SPAN?");
        }

        public void SetTransient_WideTimeOffset(int Channel, double value)
        {
            ScpiCommand($"SENSe{Channel}:TR:WIDE:TIME:OFFSet  {value}");
        }

        public double GetTransient_WideTimeOffset(int Channel)
        {
            return ScpiQuery<double>($"SENSe:TR:WIDE:TIME:OFFSet?");
        }

        public void SetTransient_WideReference(int Channel, Transient_ReferenceEnum WideReference)
        {
            string WideReferenceSCPI = Scpi.Format("{0}", WideReference);
            ScpiCommand($"SENSe{Channel}:TR:WIDE:TIME:REFerence  {WideReferenceSCPI}");
        }

        public Transient_ReferenceEnum GetTransient_WideReference(int Channel)
        {
            return ScpiQuery<Transient_ReferenceEnum>($"SENSe:TR:WIDE:TIME:REFerence?");
        }

        public void SetTransient_WideVBW(int Channel, double value)
        {
            ScpiCommand($"SENSe{Channel}:TR:WIDE:BANDwidth:VIDeo {value}");
        }

        public double GetTransient_WideVBW(int Channel)
        {
            return ScpiQuery<double>($"SENSe:TR:WIDE:BANDwidth:VIDeo?");
        }

        public void SetTransient_WideVBWauto(int Channel, bool State)
        {
            string StateValue = State ? "1" : "0";
            ScpiCommand($"SENSe{Channel}:TR:WIDE:BANDwidth:VIDeo:AUTO {StateValue}");
        }

        public bool GetTransient_WideVBWauto(int Channel)
        {
            return ScpiQuery<bool>($"SENSe:TR:WIDE:BANDwidth:VIDeo:AUTO?");
        }

        public void SetTransient_CenterFrequency(int Channel, int band, double value)
        {
            ScpiCommand($"SENSe{Channel}:TR:NARRow{band}:FREQuency:CENTer  {value}");
        }

        public double GetTransient_CenterFrequency(int Channel, int band)
        {
            return ScpiQuery<double>($"SENSe:TR:NARRow{band}:FREQuency:CENTer?");
        }

        public void SetTransient_FrequencySpan(int Channel, int band, Transient_FrequencySpanEnum FrequencySpan)
        {
            string FrequencySpanSCPI = Scpi.Format("{0}", FrequencySpan);
            ScpiCommand($"SENSe{Channel}:TR:NARRow{band}:FREQuency:RANGe  {FrequencySpanSCPI}");
        }

        public Transient_FrequencySpanEnum GetTransient_FrequencySpan(int Channel, int band)
        {
            return ScpiQuery<Transient_FrequencySpanEnum>($"SENSe:TR:NARRow{band}:FREQuency:RANGe?");
        }

        public void SetTransient_NarrowTimeSpan(int Channel, int band, double value)
        {
            ScpiCommand($"SENSe{Channel}:TR:NARRow{band}:TIME:SPAN  {value}");
        }

        public double GetTransient_NarrowTimeSpan(int Channel, int band)
        {
            return ScpiQuery<double>($"SENSe:TR:NARRow{band}:TIME:SPAN?");
        }

        public void SetTransient_NarrowTimeOffset(int Channel, int band, double value)
        {
            ScpiCommand($"SENSe{Channel}:TR:NARRow{band}:TIME:OFFSet  {value}");
        }

        public double GetTransient_NarrowTimeOffset(int Channel, int band)
        {
            return ScpiQuery<double>($"SENSe:TR:NARRow{band}:TIME:OFFSet?");
        }

        public void SetTransient_NarrowReference(int Channel, int band, Transient_ReferenceEnum NarrowReference)
        {
            string NarrowReferenceSCPI = Scpi.Format("{0}", NarrowReference);
            ScpiCommand($"SENSe{Channel}:TR:NARRow{band}:TIME:REFerence  {NarrowReferenceSCPI}");
        }

        public Transient_ReferenceEnum GetTransient_NarrowReference(int Channel, int band)
        {
            return ScpiQuery<Transient_ReferenceEnum>($"SENSe:TR:NARRow{band}:TIME:REFerence?");
        }

        public void SetTransient_NarrowVBW(int Channel, int band, double value)
        {
            ScpiCommand($"SENSe{Channel}:TR:NARRow{band}:BANDwidth:VIDeo:VALUe {value}");
        }

        public double GetTransient_NarrowVBW(int Channel, int band)
        {
            return ScpiQuery<double>($"SENSe:TR:NARRow{band}:BANDwidth:VIDeo:VALUe?");
        }

        public void SetTransient_NarrowVBWauto(int Channel, int band, bool State)
        {
            string StateValue = State ? "1" : "0";
            ScpiCommand($"SENSe{Channel}:TR:NARRow{band}:BANDwidth:VIDeo:AUTO {StateValue}");
        }

        public bool GetTransient_NarrowVBWauto(int Channel, int band)
        {
            return ScpiQuery<bool>($"SENSe:TR:NARRow{band}:BANDwidth:VIDeo:AUTO?");
        }



    }
}
