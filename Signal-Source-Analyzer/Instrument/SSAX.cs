using OpenTap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using OpenTap.Plugins.PNAX;

namespace Signal_Source_Analyzer
{
    [Display("SSA-X", Description: "Insert a description here", Group: "Signal Source Analyzer")]
    public partial class SSAX : PNAX
    {
        #region Settings

        #endregion


        public SSAX()
        {
            Name = "SSA-X";
            IoTimeout = 10000;
            isAlwaysPreset = true;
            ExternalDevices = true;
            IsQueryForErrors = true;
            IsWaitForOpc = true;

            EnableReferenceOscillatorSettings = false;
            VNAReferenceIn = VNAReferenceInEnumtype.Internal;
            VNAReferenceOut = VNAReferenceFreqEnumtype.Ten;

        }

        public override void Open()
        {
            base.Open(true);
        }

        public override void Close()
        {
            base.Close();
        }

        public new int AddNewTrace(int Channel, int Window, string Trace, string MeasClass, string Meas, ref int tnum, ref int mnum, ref string MeasName)
        {
            int traceid = GetNewWindowTraceID(Window);
            mnum = GetUniqueTraceId();

            // MeasName = Trace + mnum
            // i.e. for Trace = CH1_S11 
            //          mnum = 1
            //          then we get: CH1_S11_1
            // This is the format that the PNA uses
            MeasName = Trace + "_" + mnum.ToString();

            //ScpiCommand($"CALCulate{Channel}:CUST:DEFine \'{MeasName}\',\'{MeasClass}\',\'{Meas}\'");
            ScpiCommand($"CALCulate{Channel}:MEASure{mnum}:DEFine \"{Meas}:{MeasClass}\"");

            // Create a window if it doesn't exist already
            ScpiCommand($"DISPlay:WINDow{Window}:STATe ON");

            // Display the measurement
            //ScpiCommand($"DISPlay:WINDow{Window}:TRACe{traceid}:FEED \'{MeasName}\'");
            ScpiCommand($"DISPlay:MEASure{mnum}:FEED {Window}");

            // 
            ScpiCommand($"CALCulate{Channel}:PARameter:SELect \'{MeasName}\'");

            // Get Trace number
            tnum = ScpiQuery<int>($"CALCulate{Channel}:PARameter:TNUMber?");

            return tnum;
        }

        public void SetTraceFormat(int Channel, int mnum, string meas)
        {
            ScpiCommand($"CALCulate{Channel}:MEASure{mnum}:FORMat {meas}");
        }

        public void SetTraceFormat(int Channel, int mnum, TransientFreqMeasurementFormatEnum meas)
        {
            string FormatSCPI = Scpi.Format("{0}", meas);
            SetTraceFormat(Channel, mnum, FormatSCPI);
        }

        public void SetTraceFormat(int Channel, int mnum, PhaseMeasurementFormatEnum meas)
        {
            string FormatSCPI = Scpi.Format("{0}", meas);
            SetTraceFormat(Channel, mnum, FormatSCPI);
        }

        public void SetTraceFormat(int Channel, int mnum, PowerMeasurementFormatEnum meas)
        {
            string FormatSCPI = Scpi.Format("{0}", meas);
            SetTraceFormat(Channel, mnum, FormatSCPI);
        }

        public void SetTraceFormat(int Channel, int mnum, VCOFreqMeasurementFormatEnum meas)
        {
            string FormatSCPI = Scpi.Format("{0}", meas);
            SetTraceFormat(Channel, mnum, FormatSCPI);
        }

        public void SetTraceFormat(int Channel, int mnum, CurrentMeasurementFormatEnum meas)
        {
            string FormatSCPI = Scpi.Format("{0}", meas);
            SetTraceFormat(Channel, mnum, FormatSCPI);
        }

        public void SetTraceUnits(int Channel, int mnum, string dataFormat, FreqMeasurementFormatUnitsEnum units)
        {
            string UnitsSCPI = Scpi.Format("{0}", units);
            ScpiCommand($"CALCulate{Channel}:MEASure{mnum}:FORMat:UNIT {dataFormat}, {UnitsSCPI}");
        }

        public void SetTraceUnits(int Channel, int mnum, string dataFormat, PhaseMeasurementFormatUnitsEnum units)
        {
            string UnitsSCPI = Scpi.Format("{0}", units);
            ScpiCommand($"CALCulate{Channel}:MEASure{mnum}:FORMat:UNIT {dataFormat}, {UnitsSCPI}");
        }

        public void SetTraceUnits(int Channel, int mnum, string dataFormat, PowerLogMeasurementFormatUnitsEnum units)
        {
            string UnitsSCPI = Scpi.Format("{0}", units);
            ScpiCommand($"CALCulate{Channel}:MEASure{mnum}:FORMat:UNIT {dataFormat}, {UnitsSCPI}");
        }

        public void SetTraceUnits(int Channel, int mnum, string dataFormat, PowerLinMeasurementFormatUnitsEnum units)
        {
            string UnitsSCPI = Scpi.Format("{0}", units);
            ScpiCommand($"CALCulate{Channel}:MEASure{mnum}:FORMat:UNIT {dataFormat}, {UnitsSCPI}");
        }
    }
}
