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
    public enum VCOCharacterization_SweepTypeEnum
    {
        [Scpi("VControl")]
        [Display("VControl")]
        VControl,
        [Scpi("VSupply1")]
        [Display("VSupply1")]
        VSupply1,
        [Scpi("VSupply2")]
        [Display("VSupply2")]
        VSupply2,
    }

    public enum VCOCharacterization_FrequencyBandEnum
    {
        [Scpi("R40M")]
        [Display("1M - 40 MHz")]
        R40M,
        [Scpi("R1G5")]
        [Display("10 M - 1.5 GMHz")]
        R1G5,
        [Scpi("R8G")]
        [Display("250 M - 8 GHz")]
        R8G,
        [Scpi("RMAX")]
        [Display("Max Freq > 8 GHz")]
        RMAX,
    }

    public enum VCOCharacterization_FreqResolutionEnum
    {
        [Scpi("R01")]
        [Display("0.1 Hz")]
        R01,
        [Scpi("R1")]
        [Display("1 Hz")]
        R1,
        [Scpi("R10")]
        [Display("10 Hz")]
        R10,
        [Scpi("R1K")]
        [Display("1 kHz")]
        R1K,
        [Scpi("R64K")]
        [Display("64 kHz")]
        R64K,
    }


    public partial class SSAX : PNAX
    {
        public void SetVCOCharacterization_SweepType(VCOCharacterization_SweepTypeEnum SweepType)
        {
            string SweepTypeSCPI = Scpi.Format("{0}", SweepType);
            ScpiCommand($"SENSe:VCO:SWEep:SOURce '{SweepTypeSCPI}'");
        }

        public VCOCharacterization_SweepTypeEnum GetVCOCharacterization_SweepType()
        {
            return ScpiQuery<VCOCharacterization_SweepTypeEnum>($"SENSe:VCO:SWEep:SOURce?");
        }

        public void SetVCOCharacterization_VControlStart(int Channel, VCOCharacterization_SweepTypeEnum SweepType, double value)
        {
            string SweepTypeSCPI = Scpi.Format("{0}", SweepType);
            ScpiCommand($"SOURce{Channel}:DC:STARt {SweepTypeSCPI},{value}");
        }

        public double GetVCOCharacterization_VControlStart(int Channel, VCOCharacterization_SweepTypeEnum SweepType)
        {
            string SweepTypeSCPI = Scpi.Format("{0}", SweepType);
            return ScpiQuery<double>($"SOURce{Channel}:DC:START? {SweepTypeSCPI}");
        }

        public void SetVCOCharacterization_VControlCenter(int Channel, VCOCharacterization_SweepTypeEnum SweepType, double value)
        {
            string SweepTypeSCPI = Scpi.Format("{0}", SweepType);
            ScpiCommand($"SOURce{Channel}:DC:CENTer {SweepTypeSCPI},{value}");
        }

        public double GetVCOCharacterization_VControlCenter(int Channel, VCOCharacterization_SweepTypeEnum SweepType)
        {
            string SweepTypeSCPI = Scpi.Format("{0}", SweepType);
            return ScpiQuery<double>($"SOURce{Channel}:DC:CENTer? {SweepTypeSCPI}");
        }

        public void SetVCOCharacterization_VControlStop(int Channel, VCOCharacterization_SweepTypeEnum SweepType, double value)
        {
            string SweepTypeSCPI = Scpi.Format("{0}", SweepType);
            ScpiCommand($"SOURce{Channel}:DC:STOP {SweepTypeSCPI},{value}");
        }

        public double GetVCOCharacterization_VControlStop(int Channel, VCOCharacterization_SweepTypeEnum SweepType)
        {
            string SweepTypeSCPI = Scpi.Format("{0}", SweepType);
            return ScpiQuery<double>($"SOURce{Channel}:DC:STOP? {SweepTypeSCPI}");
        }

        public void SetVCOCharacterization_VControlSpan(int Channel, VCOCharacterization_SweepTypeEnum SweepType, double value)
        {
            string SweepTypeSCPI = Scpi.Format("{0}", SweepType);
            ScpiCommand($"SOURce{Channel}:DC:SPAN {SweepTypeSCPI},{value}");
        }

        public double GetVCOCharacterization_VControlSpan(int Channel, VCOCharacterization_SweepTypeEnum SweepType)
        {
            string SweepTypeSCPI = Scpi.Format("{0}", SweepType);
            return ScpiQuery<double>($"SOURce{Channel}:DC:SPAN? {SweepTypeSCPI}");
        }

        public void SetVCOCharacterization_NumberofPoints(int Channel, int value)
        {
            ScpiCommand($"SENSe{Channel}:SWEep:POINts {value}");
        }

        public int GetVCOCharacterization_NumberofPoints(int Channel)
        {
            return ScpiQuery<int>($"SENSe{Channel}:SWEep:POINts?");
        }

        public void SetVCOCharacterization_DwellTime(int Channel, double value)
        {
            ScpiCommand($"SENSe{Channel}:SWEep:DWELl {value}");
        }

        public double GetVCOCharacterization_DwellTime(int Channel)
        {
            return ScpiQuery<double>($"SENSe{Channel}:SWEep:DWELl?");
        }

        public void SetVCOCharacterization_FrequencyBand(VCOCharacterization_FrequencyBandEnum FrequencyBand)
        {
            string FrequencyBandSCPI = Scpi.Format("{0}", FrequencyBand);
            ScpiCommand($"SENSe:VCO:FREQuency:BAND {FrequencyBandSCPI}");
        }

        public VCOCharacterization_FrequencyBandEnum GetVCOCharacterization_FrequencyBand()
        {
            return ScpiQuery<VCOCharacterization_FrequencyBandEnum>($"SENSe:VCO:FREQuency:BAND?");
        }

        public void SetVCOCharacterization_InitialFrequency(double value)
        {
            ScpiCommand($"SENSe:VCO:FREQuency:INIT {value}");
        }

        public double GetVCOCharacterization_InitialFrequency()
        {
            return ScpiQuery<double>($"SENSe:VCO:FREQuency:INIT?");
        }

        public void SetVCOCharacterization_FreqResolution(VCOCharacterization_FreqResolutionEnum FreqResolution)
        {
            string FreqResolutionSCPI = Scpi.Format("{0}", FreqResolution);
            ScpiCommand($"SENSe:VCO:FREQuency:RESolution {FreqResolutionSCPI}");
        }

        public VCOCharacterization_FreqResolutionEnum GetVCOCharacterization_FreqResolution()
        {
            return ScpiQuery<VCOCharacterization_FreqResolutionEnum>($"SENSe:VCO:FREQuency:RESolution?");
        }


    }
}
