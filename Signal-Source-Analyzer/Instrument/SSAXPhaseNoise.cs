using OpenTap;
using OpenTap.Plugins.PNAX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signal_Source_Analyzer
{
    public enum PhaseNoise_NoiseTypeEnum
    {
        [Scpi("PNOise")]
        [Display("Phase Noise")]
        PNOise,
        //[Scpi("RESidual")]
        //RESidual,
        [Scpi("BASeband")]
        [Display("Baseband Noise")]
        BASeband,
    }

    public enum PhaseNoise_SweepTypeEnum
    {
        [Scpi("LOGarithmic")]
        [Display("Log Frequency")]
        LOGarithmic,
        [Scpi("SEGMent")]
        [Display("Segment Sweep")]
        SEGMent,
    }

    public enum PhaseNoise_FastXCORRModeEnum
    {
        [Scpi("NORMal")]
        [Display("Normal")]
        NORMal,
        [Scpi("FAST")]
        [Display("Fast")]
        FAST,
    }

    public partial class SSAX : PNAX
    {
        public void SetPhaseNoise_NoiseType(int Channel, PhaseNoise_NoiseTypeEnum NoiseType)
        {
            string NoiseTypeSCPI = Scpi.Format("{0}", NoiseType);
            ScpiCommand($"SENSe{Channel}:PN:NTYPe {NoiseTypeSCPI}");
        }
        public PhaseNoise_NoiseTypeEnum GetPhaseNoise_NoiseType(int Channel)
        {
            return ScpiQuery<PhaseNoise_NoiseTypeEnum>($"SENSe{Channel}:PN:NTYPe?");
        }

        public void SetPhaseNoise_SweepType(int Channel, PhaseNoise_SweepTypeEnum SweepType)
        {
            string SweepTypeSCPI = Scpi.Format("{0}", SweepType);
            ScpiCommand($"SENSe{Channel}:PN:SWEep:TYPE {SweepTypeSCPI}");
        }
        public PhaseNoise_SweepTypeEnum GetPhaseNoise_SweepType(int Channel)
        {
            return ScpiQuery<PhaseNoise_SweepTypeEnum>($"SENSe{Channel}:PN:SWEep:TYPE?");
        }

        public void SetPhaseNoise_XCORRGainIndicator(int Channel, bool State)
        {
            string StateValue = State ? "1" : "0";
            ScpiCommand($"SENSe{Channel}:PN:XGINdicator:STATe {StateValue}");
        }
        public bool GetPhaseNoise_XCORRGainIndicator(int Channel)
        {
            return ScpiQuery<bool>($"SENSe{Channel}:PN:XGINdicator:STATe?");
        }

        public void SetPhaseNoise_StartOffset(int Channel, double value)
        {
            ScpiCommand($"SENSe{Channel}:FREQuency:STARt {value}");
        }
        public double GetPhaseNoise_StartOffset(int Channel)
        {
            return ScpiQuery<double>($"SENSe{Channel}:FREQuency:STARt?");
        }

        public void SetPhaseNoise_StopOffset(int Channel, double value)
        {
            ScpiCommand($"SENSe{Channel}:FREQuency:STOP {value}");
        }
        public double GetPhaseNoise_StopOffset(int Channel)
        {
            return ScpiQuery<double>($"SENSe{Channel}:FREQuency:STOP?");
        }

        public void SetPhaseNoise_RBWRatio(int Channel, double value)
        {
            ScpiCommand($"SENSe{Channel}:PN:BWIDth:RESolution:RATio {value}");
        }
        public double GetPhaseNoise_RBWRatio(int Channel)
        {
            return ScpiQuery<double>($"SENSe{Channel}:PN:BWIDth:RESolution:RATio?");
        }

        public void SetPhaseNoise_XCORRFactor(int Channel, int value)
        {
            ScpiCommand($"SENSe{Channel}:PN:CORRelation:COUNt {value}");
        }
        public int GetPhaseNoise_XCORRFactor(int Channel)
        {
            return ScpiQuery<int>($"SENSe{Channel}:PN:CORRelation:COUNt?");
        }

        public void SetPhaseNoise_FastXCORRMode(int Channel, PhaseNoise_FastXCORRModeEnum FastXCORRMode)
        {
            string FastXCORRModeSCPI = Scpi.Format("{0}", FastXCORRMode);
            ScpiCommand($"SENSe{Channel}:PN:CORRelation:MODE {FastXCORRModeSCPI}");
        }
        public PhaseNoise_FastXCORRModeEnum GetPhaseNoise_FastXCORRMode(int Channel)
        {
            return ScpiQuery<PhaseNoise_FastXCORRModeEnum>($"SENSe{Channel}:PN:CORRelation:MODE?");
        }

        public void SetPhaseNoise_SegmentBandwidth(int Channel, int Segment, double value)
        {
            ScpiCommand($"SENSe{Channel}:PN:SEGMent{Segment}:BWIDth:RESolution {value}");
        }
        public double GetPhaseNoise_SegmentBandwidth(int Channel, int Segment)
        {
            return ScpiQuery<double>($"SENSe{Channel}:PN:SEGMent{Segment}:BWIDth:RESolution?");
        }

        public void SetPhaseNoise_SegmentCorrelation(int Channel, int Segment, int value)
        {
            ScpiCommand($"SENSe{Channel}:PN:SEGMent{Segment}:CORRelation:COUNt {value}");
        }
        public int GetPhaseNoise_SegmentCorrelation(int Channel, int Segment)
        {
            return ScpiQuery<int>($"SENSe{Channel}:PN:SEGMent{Segment}:CORRelation:COUNt?");
        }

        public int GetPhaseNoise_SegmentCount(int Channel, int Segment)
        {
            return ScpiQuery<int>($"SENSe{Channel}:PN:SEGMent{Segment}:COUNt?");
        }

        public double GetPhaseNoise_SegmentFreqStart(int Channel, int Segment)
        {
            return ScpiQuery<double>($"SENSe{Channel}:PN:SEGMent{Segment}:FREQuency:START?");
        }

        public double GetPhaseNoise_SegmentFreqStop(int Channel, int Segment)
        {
            return ScpiQuery<double>($"SENSe{Channel}:PN:SEGMent{Segment}:FREQuency:STOP?");
        }

        public void SetPhaseNoise_CarrierFrequency(int Channel, double value)
        {
            ScpiCommand($"SENSe{Channel}:PN:SWEep:CARRier:FREQuency {value}");
        }
        public double GetPhaseNoise_CarrierFrequency(int Channel)
        {
            return ScpiQuery<double>($"SENSe{Channel}:PN:SWEep:CARRier:FREQuency?");
        }

        public void SetPhaseNoise_EnableSearch(int Channel, bool State)
        {
            string StateValue = State ? "1" : "0";
            ScpiCommand($"SENSe{Channel}:PN:ADJust:CONFigure:FREQuency:SEARch:STATe {StateValue}");
        }
        public bool GetPhaseNoise_EnableSearch(int Channel)
        {
            return ScpiQuery<bool>($"SENSe{Channel}:PN:ADJust:CONFigure:FREQuency:SEARch:STATe?");
        }

        public void SetPhaseNoise_EnablePulse(int Channel, bool State)
        {
            string StateValue = State ? "1" : "0";
            ScpiCommand($"SENSe{Channel}:PN:PULSe:STATe {StateValue}");
        }
        public bool GetPhaseNoise_EnablePulse(int Channel)
        {
            return ScpiQuery<bool>($"SENSe{Channel}:PN:PULSe:STATe?");
        }

        public void SetPhaseNoise_PulsePeriod(int Channel, double value)
        {
            ScpiCommand($"SENSe{Channel}:PN:PULSe:PERiod {value}");
        }
        public double GetPhaseNoise_PulsePeriod(int Channel)
        {
            return ScpiQuery<double>($"SENSe{Channel}:PN:PULSe:PERiod?");
        }
    }
}
