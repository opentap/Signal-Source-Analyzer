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
        [Scpi("RESidual")]
        RESidual,
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

    public enum PhaseNoiseSourceEnum
    {
        [Scpi("EXTernal")]
        [Display("External")]
        EXTernal,
        [Scpi("INTernal")]
        [Display("Internal")]
        INTernal,
    }

    public enum PhaseNoiseSourceOutEnum
    {
        [Display("Source 1 Out")]
        Source1Out,
        [Display("Source 2 Out")]
        Source2Out,
    }

    public class ThresholdTable
    {
        [Display("Start Freq", Order: 1)]
        public double StartFreq { get; set; }
        [Display("Lower Threshold", Order: 2)]
        public double LowerThreshold { get; set; }
        [Display("Upper Threshold", Order: 3)]
        public double UpperThreshold { get; set; }

        public ThresholdTable()
        {
            StartFreq = 0;
            LowerThreshold = 0;
            UpperThreshold = 0;
        }
    }

    public class UserSpurTable
    {
        [Display("Spurious Freq", Order: 1)]
        public double SpuriousFrequency { get; set; }
    }

    public partial class SSAX : PNAX
    {
        #region Sweep
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
        #endregion

        #region RF Path

        public void SetPhaseNoise_RFInput(int Channel, SSAXPortsEnum RFInput)
        {
            string RFInputSCPI = Scpi.Format("{0}", RFInput);
            ScpiCommand($"SENSe{Channel}:PN:PORT {RFInputSCPI}");
        }

        public SSAXPortsEnum GetPhaseNoise_RFInput(int Channel)
        {
            return ScpiQuery<SSAXPortsEnum>($"SENSe{Channel}:PN:PORT?");
        }

        public void SetPhaseNoise_AutoRange(int Channel, SSAXPortsEnum port, bool State)
        {
            string PortSCPI = Scpi.Format("{0}", port);
            string StateValue = State ? "1" : "0";
            ScpiCommand($"SENSe{Channel}:PN:POWer:INPut{PortSCPI}:LEVel:AUTO {StateValue}");
        }

        public void SetPhaseNoise_MaxInputLevel(int Channel, SSAXPortsEnum port, double value)
        {
            string PortSCPI = Scpi.Format("{0}", port);
            ScpiCommand($"SENSe{Channel}:PN:POWer:INPut{PortSCPI}:LEVel:MAXimum {value}");
        }

        public void SetPhaseNoise_AlwaysUseInternalReference(int Channel, bool State)
        {
            string StateValue = State ? "1" : "0";
            ScpiCommand($"SENSe{Channel}:PN:ROSCillator:SOURce:FIXed:STATe {StateValue}");
        }

        public bool GetPhaseNoise_AlwaysUseInternalReference(int Channel)
        {
            return ScpiQuery<bool>($"SENSe{Channel}:PN:ROSCillator:SOURce:FIXed:STATe?");
        }

        public void SetPhaseNoise_EnableAttenuatorSetting(int Channel, SSAXPortsEnum port, bool State)
        {
            string PortSCPI = Scpi.Format("{0}", port);
            string StateValue = State ? "1" : "0";
            ScpiCommand($"SENSe{Channel}:PN:POWer:INPut{PortSCPI}:ATTenuation:AUTO {StateValue}");
        }

        public bool GetPhaseNoise_EnableAttenuatorSetting(int Channel, SSAXPortsEnum port)
        {
            string PortSCPI = Scpi.Format("{0}", port);
            return ScpiQuery<bool>($"SENSe{Channel}:PN:POWer:INPut{PortSCPI}:ATTenuation:AUTO?");
        }

        public void SetPhaseNoise_Attenuator(int Channel, SSAXPortsEnum port, double value)
        {
            string PortSCPI = Scpi.Format("{0}", port);
            ScpiCommand($"SENSe{Channel}:PN:POWer:INPut{PortSCPI}:ATTenuation {value}");
        }

        public void SetPhaseNoise_BasebandInput(int Channel, PhaseNoise_BasebandInputEnum BasebandInput)
        {
            string BasebandInputSCPI = Scpi.Format("{0}", BasebandInput);
            ScpiCommand($"SENSe{Channel}:PN:BASeband:INPut:COUPling {BasebandInputSCPI}");
        }

        public PhaseNoise_BasebandInputEnum GetPhaseNoise_BasebandInput(int Channel)
        {
            return ScpiQuery<PhaseNoise_BasebandInputEnum>($"SENSe{Channel}:PN:BASeband:INPut:COUPling?");
        }

        public void SetPhaseNoise_BasebandGain(int Channel, double value)
        {
            ScpiCommand($"SENSe{Channel}:PN:BASeband:GAIN {value}");
        }

        public double GetPhaseNoise_BasebandGain(int Channel)
        {
            return ScpiQuery<double>($"SENSe{Channel}:PN:BASeband:GAIN?");
        }

        public void SetPhaseNoise_BasebandDischarge(int Channel)
        {
            ScpiCommand($"SENSe{Channel}:PN:BASeband:BASeband:DISCharge");
        }

        //public void SetPhaseNoise_AnalyzerInput(int Channel, PhaseNoise_AnalyzerInputEnum AnalyzerInput)
        //{
        //    string AnalyzerInputSCPI = Scpi.Format("{0}", AnalyzerInput);
        //    ScpiCommand($"SENSe{Channel}:PN:RECeiver  {AnalyzerInputSCPI}");
        //}

        //public PhaseNoise_AnalyzerInputEnum GetPhaseNoise_AnalyzerInput(int Channel)
        //{
        //    return ScpiQuery<PhaseNoise_AnalyzerInputEnum>($"SENSe{Channel}:PN:RECeiver?");
        //}

        // SSAXPortsEnum
        public void SetPhaseNoise_ReceiverAttenuation(int Channel, SSAXPortsEnum port, double value)
        {
            string PortSCPI = Scpi.Format("{0}", port);
            ScpiCommand($"SOURce{Channel}:POWer{PortSCPI}:ATTenuation:RECeiver:TEST {value}");
        }

        public void SetPhaseNoise_ReceiverAttenuation(int Channel, int port, double value)
        {
            ScpiCommand($"SOURce{Channel}:POWer{port}:ATTenuation:RECeiver:TEST {value}");
        }

        public double GetPhaseNoise_ReceiverAttenuation(int Channel, int port)
        {
            return ScpiQuery<double>($"SOURce{Channel}:POWer{port}:ATTenuation:RECeiver:TEST?");
        }

        public void SetPhaseNoise_RFPathConfiguration(int Channel, PhaseNoise_RFPathConfigurationEnum RFPathConfiguration, string setting)
        {
            string RFPathConfigurationSCPI = Scpi.Format("{0}", RFPathConfiguration);
            ScpiCommand($"SENSe{Channel}:PATH:CONFig:ELEMent:STATe \"{RFPathConfigurationSCPI}\", \"{setting}\"");
        }

        public string GetPhaseNoise_RFPathConfiguration(int Channel, PhaseNoise_RFPathConfigurationEnum RFPathConfiguration)
        {
            string RFPathConfigurationSCPI = Scpi.Format("{0}", RFPathConfiguration);
            return ScpiQuery<string>($"SENSe{Channel}:PATH:CONFig:ELEMent:STATe? \"{RFPathConfigurationSCPI}\"");
        }
        #endregion

        #region Spurious
        public void SetPhaseNoise_ShowSpuriousTable(int wnum, bool State)
        {
            string StateValue = State ? "1" : "0";
            ScpiCommand($"DISPlay:WINDow{wnum}:TABLe:SPURious:ENABle {StateValue}");
        }

        public bool GetPhaseNoise_ShowSpuriousTable(int wnum)
        {
            return ScpiQuery<bool>($"DISPlay:WINDow{wnum}:TABLe:SPURious:ENABle?");
        }

        public void SetPhaseNoise_TableSortOrder(int Channel, int mnum, PhaseNoise_TableSortOrderEnum TableSortOrder)
        {
            string TableSortOrderSCPI = Scpi.Format("{0}", TableSortOrder);
            ScpiCommand($"CALCulate{Channel}:MEASure{mnum}:PN:SPURious:SORT {TableSortOrderSCPI}");
        }

        public PhaseNoise_TableSortOrderEnum GetPhaseNoise_TableSortOrder(int Channel, int mnum)
        {
            return ScpiQuery<PhaseNoise_TableSortOrderEnum>($"CALCulate{Channel}:MEASure{mnum}:PN:SPURious:SORT?");
        }

        public void SetPhaseNoise_EnableSpurAnalysis(int Channel, int mnum, bool State)
        {
            string StateValue = State ? "1" : "0";
            ScpiCommand($"CALCulate{Channel}:MEASure{mnum}:PN:SPURious:ANALysis:STATe {StateValue}");
        }

        public bool GetPhaseNoise_EnableSpurAnalysis(int Channel, int mnum)
        {
            return ScpiQuery<bool>($"CALCulate{Channel}:MEASure{mnum}:PN:SPURious:ANALysis:STATe?");
        }

        public void SetPhaseNoise_SpurSensibility(int Channel, int mnum, double value)
        {
            ScpiCommand($"CALCulate{Channel}:MEASure{mnum}:PN:SPURious:SENSibility {value}");
        }

        public double GetPhaseNoise_SpurSensibility(int Channel, int mnum)
        {
            return ScpiQuery<double>($"CALCulate{Channel}:MEASure{mnum}:PN:SPURious:SENSibility?");
        }

        public void SetPhaseNoise_MinSpurLevel(int Channel, int mnum, double value)
        {
            ScpiCommand($"CALCulate{Channel}:MEASure{mnum}:PN:SPURious:THReshold:LEVel:MINimum {value}");
        }

        public double GetPhaseNoise_MinSpurLevel(int Channel, int mnum)
        {
            return ScpiQuery<double>($"CALCulate{Channel}:MEASure{mnum}:PN:SPURious:THReshold:LEVel:MINimum?");
        }

        public void PhaseNoise_ThresholdTableDelete(int Channel, int mnum)
        {
            ScpiCommand($"CALCulate{Channel}:MEASure{mnum}:PN:SPURious:THReshold:TABle:DELete");
        }

        public void SetPhaseNoise_ThresholdTable(int Channel, int mnum, List<ThresholdTable> data)
        {
            string tableScpi = "";
            foreach(ThresholdTable t in data)
            {
                tableScpi += Scpi.Format("{0},{1},{2},", t.StartFreq, t.LowerThreshold, t.UpperThreshold);
            }
            tableScpi = tableScpi.Substring(0, tableScpi.Length - 1);

            ScpiCommand($"CALCulate{Channel}:MEASure{mnum}:PN:SPURious:THReshold:TABle:DATA {tableScpi}");
        }

        public string GetPhaseNoise_ThresholdTable(int Channel, int mnum)
        {
            return ScpiQuery<string>($"CALCulate{Channel}:MEASure{mnum}:PN:SPURious:THReshold:TABle:DATA?");
        }

        public void SetPhaseNoise_OmitDisplayedSpur(int Channel, int mnum, bool State)
        {
            string StateValue = State ? "1" : "0";
            ScpiCommand($"CALCulate{Channel}:MEASure{mnum}:PN:SPURious:OMISsion:STATe {StateValue}");
        }

        public bool GetPhaseNoise_OmitDisplayedSpur(int Channel, int mnum)
        {
            return ScpiQuery<bool>($"CALCulate{Channel}:MEASure{mnum}:PN:SPURious:OMISsion:STATe?");
        }

        public void SetPhaseNoise_UserSpurTable(int Channel, int mnum, List<UserSpurTable> data)
        {
            string tableScpi = string.Join(",", data.Select(d => d.SpuriousFrequency));

            ScpiCommand($"CALCulate{Channel}:MEASure{mnum}:PN:SPURious:OSSPur:DATA {tableScpi}");
        }

        public string GetPhaseNoise_UserSpurTable(int Channel, int mnum)
        {
            return ScpiQuery<string>($"CALCulate{Channel}:MEASure{mnum}:PN:SPURious:OSSPur:DATA?");
        }

        public void SetPhaseNoise_OmitUserSpecifiedSpurs(int Channel, int mnum, bool State)
        {
            string StateValue = State ? "1" : "0";
            ScpiCommand($"CALCulate{Channel}:MEASure{mnum}:PN:SPURious:OSSPur:STATe {StateValue}");
        }

        public bool GetPhaseNoise_OmitUserSpecifiedSpurs(int Channel, int mnum)
        {
            return ScpiQuery<bool>($"CALCulate{Channel}:MEASure{mnum}:PN:SPURious:OSSPur:STATe?");
        }
        #endregion

        #region Integrated Noise
        public void SetPhaseNoise_ShowIntegratedNoiseTable(int wnum, bool State)
        {
            string StateValue = State ? "1" : "0";
            ScpiCommand($"DISPlay:WINDow{wnum}:TABLe:INOise:ENABle {StateValue}");
        }

        public bool GetPhaseNoise_ShowIntegratedNoiseTable(int wnum)
        {
            return ScpiQuery<bool>($"DISPlay:WINDow{wnum}:TABLe:INOise:ENABle?");
        }

        public void SetPhaseNoise_IntegratedRangeType(int Channel, int mnum, int range, PhaseNoise_IntegratedRangeTypeEnum IntegratedRangeType)
        {
            string IntegratedRangeTypeSCPI = Scpi.Format("{0}", IntegratedRangeType);
            ScpiCommand($"CALCulate{Channel}:MEASure{mnum}:PN:INTegral:RANGe{range}:TYPE {IntegratedRangeTypeSCPI}");
        }

        public PhaseNoise_IntegratedRangeTypeEnum GetPhaseNoise_IntegratedRangeType(int Channel, int mnum, int range)
        {
            return ScpiQuery<PhaseNoise_IntegratedRangeTypeEnum>($"CALCulate{Channel}:MEASure{mnum}:PN:INTegral:RANGe{range}:TYPE?");
        }

        public void SetPhaseNoise_Start(int Channel, int mnum, int range, double value)
        {
            ScpiCommand($"CALCulate{Channel}:MEASure{mnum}:PN:INTegral:RANGe{range}:STARt {value}");
        }

        public double GetPhaseNoise_Start(int Channel, int mnum, int range)
        {
            return ScpiQuery<double>($"CALCulate{Channel}:MEASure{mnum}:PN:INTegral:RANGe{range}:STARt?");
        }

        public void SetPhaseNoise_Stop(int Channel, int mnum, int range, double value)
        {
            ScpiCommand($"CALCulate{Channel}:MEASure{mnum}:PN:INTegral:RANGe{range}:STOP {value}");
        }

        public double GetPhaseNoise_Stop(int Channel, int mnum, int range)
        {
            return ScpiQuery<double>($"CALCulate{Channel}:MEASure{mnum}:PN:INTegral:RANGe{range}:STOP?");
        }

        public void SetPhaseNoise_WeighthingFilter(int Channel, int mnum, int range, string FilterName)
        {
            ScpiCommand($"CALCulate{Channel}:MEASure{mnum}:PN:INTegral:RANGe{range}:WEIGhting \"{FilterName}\"");
        }

        public String GetPhaseNoise_WeighthingFilter(int Channel, int mnum, int range)
        {
            return ScpiQuery<String>($"CALCulate{Channel}:MEASure{mnum}:PN:INTegral:RANGe{range}:WEIGhting?");
        }
        #endregion

        #region Spot Noise
        public void SetPhaseNoise_ShowSpotNoiseTable(int wnum, bool State)
        {
            string StateValue = State ? "1" : "0";
            ScpiCommand($"DISPlay:WINDow{wnum}:TABLe:SNOise:ENABle {StateValue}");
        }

        public bool GetPhaseNoise_ShowSpotNoiseTable(int wnum)
        {
            return ScpiQuery<bool>($"DISPlay:WINDow{wnum}:TABLe:SNOise:ENABle?");
        }

        public void SetPhaseNoise_SpotFrequency(int Channel, int mnum, int user, double value)
        {
            ScpiCommand($"CALCulate{Channel}:MEASure{mnum}:PN:SNOise:USER{user}:X {value}");
        }

        public double GetPhaseNoise_SpotFrequency(int Channel, int mnum, int user)
        {
            return ScpiQuery<double>($"CALCulate{Channel}:MEASure{mnum}:PN:SNOiseUSER{user}:X?");
        }

        public void SetPhaseNoise_SpotFrequencyEnabled(int Channel, int mnum, int user, bool State)
        {
            string StateValue = State ? "1" : "0";
            ScpiCommand($"CALCulate{Channel}:MEASure{mnum}:PN:SNOise:USER{user}:STATe {StateValue}");
        }

        public bool GetPhaseNoise_SpotFrequencyEnabled(int Channel, int mnum, int user)
        {
            return ScpiQuery<bool>($"CALCulate{Channel}:MEASure{mnum}:PN:SNOiseUSER{user}:STATe?");
        }

        public void SetPhaseNoise_DecadeEdges(int Channel, int mnum, bool State)
        {
            string StateValue = State ? "1" : "0";
            ScpiCommand($"CALCulate{Channel}:MEASure{mnum}:PN:SNOise:DECades:STATe {StateValue}");
        }

        public bool GetPhaseNoise_DecadeEdges(int Channel, int mnum)
        {
            return ScpiQuery<bool>($"CALCulate{Channel}:MEASure{mnum}:PN:SNOise:DECades:STATe?");
        }
        #endregion

    }
}
