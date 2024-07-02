using OpenTap;
using OpenTap.Plugins.PNAX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signal_Source_Analyzer
{

    [Display("Sweep", Groups: new[] { "Signal Source Analyzer", "General", "Transient" }, Description: "Insert a description here")]
    public class GeneralTransientSweep : SSAXBaseStep
    {
        #region Settings
        [Browsable(false)]
        public bool IsPropertyReadOnly { get; set; } = true;


        [Display("Sweep Type", Group: "Settings", Order: 20.01, Description: "Sets and read the transition mode, Wide-Narrow or Narrow-Narrow.")]
        public Transient_SweepTypeEnum SweepType { get; set; }

        private bool _TimeCoupling;
        [Display("Time Coupling", Group: "Settings", Order: 20.02, Description: "Enable and disable the time coupling for two measurements (Wide-Narrow or Narrow1-Narrow2")]
        public bool TimeCoupling
        {
            get
            {
                return _TimeCoupling;
            }
            set
            {
                _TimeCoupling = value;
                if (_TimeCoupling)
                {
                    // Couple values
                    if (SweepType == Transient_SweepTypeEnum.WN)
                    {
                        _Narrow1TimeSpan = _Narrow2TimeSpan = _WideTimeSpan;
                        _Narrow1TimeOffset = _Narrow2TimeOffset = _WideTimeOffset;
                        _Narrow1Reference = _Narrow2Reference = _WideReference;
                    }
                    else
                    {
                        _Narrow2TimeSpan = _WideTimeSpan = _Narrow1TimeSpan;
                        _Narrow2TimeOffset = _WideTimeOffset = _Narrow1TimeOffset;
                        _Narrow2Reference = _WideReference = _Narrow1Reference;
                    }
                }
            }
        }

        [EnabledIf("IsPropertyReadOnly", false, HideIfDisabled = false)]
        [Display("Number Of Points", Group: "Settings", Order: 20.03, Description: "Sets the number of data points for the measurement.")]
        public int NumberOfPoints { get; set; }

        [EnabledIf("SweepType", Transient_SweepTypeEnum.WN, HideIfDisabled = true)]
        [Display("Frequency Range", Group: "Wide Mode", Order: 30.01, Description: "Set and read the frequency range for wide band.")]
        public Transient_FrequencyRangeEnum FrequencyRange { get; set; }

        private double _WideTimeSpan;
        [EnabledIf("SweepType", Transient_SweepTypeEnum.WN, HideIfDisabled = true)]
        [Display("Wide Time Span", Group: "Wide Mode", Order: 30.02, Description: "Set and read the time span of wide band.")]
        [Unit("S", UseEngineeringPrefix: true, StringFormat: "0.000")]
        public double WideTimeSpan
        {
            get 
            { 
                return _WideTimeSpan; 
            }
            set 
            { 
                _WideTimeSpan = value; 
                if (TimeCoupling)
                {
                    _Narrow1TimeSpan = _Narrow2TimeSpan = _WideTimeSpan;
                }
            }
        }

        private double _WideTimeOffset;
        [EnabledIf("SweepType", Transient_SweepTypeEnum.WN, HideIfDisabled = true)]
        [Display("Wide Time Offset", Group: "Wide Mode", Order: 30.03, Description: "Set and read the time offset for wide band.")]
        [Unit("S", UseEngineeringPrefix: true, StringFormat: "")]
        public double WideTimeOffset
        {
            get
            {
                return _WideTimeOffset;
            }
            set
            {
                _WideTimeOffset = value;
                if (TimeCoupling)
                {
                    _Narrow1TimeOffset = _Narrow2TimeOffset = _WideTimeOffset;
                }
            }
        }

        private Transient_ReferenceEnum _WideReference;
        [EnabledIf("SweepType", Transient_SweepTypeEnum.WN, HideIfDisabled = true)]
        [Display("Wide Reference", Group: "Wide Mode", Order: 30.04, Description: "Set and read the position at the reference point of wide band.")]
        public Transient_ReferenceEnum WideReference
        {
            get
            {
                return _WideReference;
            }
            set
            {
                _WideReference = value;
                if (TimeCoupling)
                {
                    _Narrow1Reference = _Narrow2Reference = _WideReference;
                }
            }
        }

        [EnabledIf("SweepType", Transient_SweepTypeEnum.WN, HideIfDisabled = true)]
        [Display("Wide VBW auto", Group: "Wide Mode", Order: 30.05, Description: "Sets and read the Video Band Width (VBW")]
        public bool WideVBWauto { get; set; }

        [EnabledIf("WideVBWauto", false, HideIfDisabled = true)]
        [EnabledIf("SweepType", Transient_SweepTypeEnum.WN, HideIfDisabled = true)]
        [Display("Wide VBW", Group: "Wide Mode", Order: 30.06, Description: "Sets and read the Video Band Width (VBW")]
        [Unit("Hz", UseEngineeringPrefix: true, StringFormat: "0.000000")]
        public double WideVBW { get; set; }

        [Display("Center Frequency", Group: "Narrow1", Order: 40.01, Description: "Set and read the center frequency for Narrow1 or Narrow2.")]
        [Unit("Hz", UseEngineeringPrefix: true, StringFormat: "0.000000000")]
        public double Narrow1CenterFrequency { get; set; }

        [Display("Frequency Span", Group: "Narrow1", Order: 40.02, Description: "Set and read the frequency span of narrow band 1/2.")]
        public Transient_FrequencySpanEnum Narrow1FrequencySpan { get; set; }

        private double _Narrow1TimeSpan;
        [Display("Narrow Time Span", Group: "Narrow1", Order: 40.03, Description: "Set and read the time span of the narrow band 1 or 2.")]
        [Unit("S", UseEngineeringPrefix: true, StringFormat: "0.000")]
        public double Narrow1TimeSpan
        {
            get
            {
                return _Narrow1TimeSpan;
            }
            set
            {
                _Narrow1TimeSpan = value;
                if (TimeCoupling)
                {
                    _WideTimeSpan = _Narrow2TimeSpan = _Narrow1TimeSpan;
                }
            }
        }

        private double _Narrow1TimeOffset;
        [Display("Narrow Time Offset", Group: "Narrow1", Order: 40.04, Description: "Set and read the offset time at the reference point for the narrow band 1 or 2.")]
        [Unit("S", UseEngineeringPrefix: true, StringFormat: "0")]
        public double Narrow1TimeOffset
        {
            get
            {
                return _Narrow1TimeOffset;
            }
            set
            {
                _Narrow1TimeOffset = value;
                if (TimeCoupling)
                {
                    _WideTimeOffset = _Narrow2TimeOffset = _Narrow1TimeOffset;
                }
            }
        }

        private Transient_ReferenceEnum _Narrow1Reference;
        [Display("Narrow Reference", Group: "Narrow1", Order: 40.05, Description: "Set and read the position at the reference point of the narrow band 1 or 2.")]
        public Transient_ReferenceEnum Narrow1Reference
        {
            get
            {
                return _Narrow1Reference;
            }
            set
            {
                _Narrow1Reference = value;
                if (TimeCoupling)
                {
                    _WideReference = _Narrow2Reference = _Narrow1Reference;
                }
            }
        }

        [Display("Narrow VBW auto", Group: "Narrow1", Order: 40.06, Description: "Sets and read the Video Band Width (VBW")]
        public bool Narrow1VBWauto { get; set; }

        [EnabledIf("Narrow1VBWauto", false, HideIfDisabled = true)]
        [Display("Narrow VBW", Group: "Narrow1", Order: 40.07, Description: "Sets and read the Video Band Width (VBW")]
        [Unit("Hz", UseEngineeringPrefix: true, StringFormat: "0.000000")]
        public double Narrow1VBW { get; set; }

        [EnabledIf("SweepType", Transient_SweepTypeEnum.NN, HideIfDisabled = true)]
        [Display("Center Frequency", Group: "Narrow2", Order: 50.01, Description: "Set and read the center frequency for Narrow1 or Narrow2.")]
        [Unit("Hz", UseEngineeringPrefix: true, StringFormat: "0.000000000")]
        public double Narrow2CenterFrequency { get; set; }

        [EnabledIf("SweepType", Transient_SweepTypeEnum.NN, HideIfDisabled = true)]
        [Display("Frequency Span", Group: "Narrow2", Order: 50.02, Description: "Set and read the frequency span of narrow band 1/2.")]
        public Transient_FrequencySpanEnum Narrow2FrequencySpan { get; set; }

        private double _Narrow2TimeSpan;
        [EnabledIf("SweepType", Transient_SweepTypeEnum.NN, HideIfDisabled = true)]
        [Display("Narrow Time Span", Group: "Narrow2", Order: 50.03, Description: "Set and read the time span of the narrow band 1 or 2.")]
        [Unit("S", UseEngineeringPrefix: true, StringFormat: "0.000")]
        public double Narrow2TimeSpan
        {
            get
            {
                return _Narrow2TimeSpan;
            }
            set
            {
                _Narrow2TimeSpan = value;
                if (TimeCoupling)
                {
                    _WideTimeSpan = _Narrow1TimeSpan = _Narrow2TimeSpan;
                }
            }
        }

        private double _Narrow2TimeOffset;
        [EnabledIf("SweepType", Transient_SweepTypeEnum.NN, HideIfDisabled = true)]
        [Display("Narrow Time Offset", Group: "Narrow2", Order: 50.04, Description: "Set and read the offset time at the reference point for the narrow band 1 or 2.")]
        [Unit("S", UseEngineeringPrefix: true, StringFormat: "0")]
        public double Narrow2TimeOffset
        {
            get
            {
                return _Narrow2TimeOffset;
            }
            set
            {
                _Narrow2TimeOffset = value;
                if (TimeCoupling)
                {
                    _WideTimeOffset = _Narrow1TimeOffset = _Narrow2TimeOffset;
                }
            }
        }

        private Transient_ReferenceEnum _Narrow2Reference;
        [EnabledIf("SweepType", Transient_SweepTypeEnum.NN, HideIfDisabled = true)]
        [Display("Narrow Reference", Group: "Narrow2", Order: 50.05, Description: "Set and read the position at the reference point of the narrow band 1 or 2.")]
        public Transient_ReferenceEnum Narrow2Reference
        {
            get
            {
                return _Narrow2Reference;
            }
            set
            {
                _Narrow2Reference = value;
                if (TimeCoupling)
                {
                    _WideReference = _Narrow1Reference = _Narrow2Reference;
                }
            }
        }


        [EnabledIf("SweepType", Transient_SweepTypeEnum.NN, HideIfDisabled = true)]
        [Display("Narrow VBW auto", Group: "Narrow2", Order: 50.06, Description: "Sets and read the Video Band Width (VBW")]
        public bool Narrow2VBWauto { get; set; }

        [EnabledIf("Narrow2VBWauto", false, HideIfDisabled = true)]
        [EnabledIf("SweepType", Transient_SweepTypeEnum.NN, HideIfDisabled = true)]
        [Display("Narrow VBW", Group: "Narrow2", Order: 50.07, Description: "Sets and read the Video Band Width (VBW")]
        [Unit("Hz", UseEngineeringPrefix: true, StringFormat: "0.000000")]
        public double Narrow2VBW { get; set; }


        #endregion

        public GeneralTransientSweep()
        {
            SweepType = Transient_SweepTypeEnum.WN;
            TimeCoupling = false;
            NumberOfPoints = 201;

            FrequencyRange = Transient_FrequencyRangeEnum.R8G;
            WideTimeSpan = 0.1;
            WideTimeOffset = 0;
            WideReference = Transient_ReferenceEnum.CENTer;
            WideVBW = 2000;
            WideVBWauto = true;

            Narrow1CenterFrequency = 1000000000;
            Narrow1FrequencySpan = Transient_FrequencySpanEnum.R30;
            Narrow1TimeSpan = 0.1;
            Narrow1TimeOffset = 0;
            Narrow1Reference = Transient_ReferenceEnum.CENTer;
            Narrow1VBW = 2000;
            Narrow1VBWauto = true;

            Narrow2CenterFrequency = 1000000000;
            Narrow2FrequencySpan = Transient_FrequencySpanEnum.R30;
            Narrow2TimeSpan = 0.1;
            Narrow2TimeOffset = 0;
            Narrow2Reference = Transient_ReferenceEnum.CENTer;
            Narrow2VBW = 2000;
            Narrow2VBWauto = true;

        }

        public override void Run()
        {
            SSAX.SetTransient_SweepType(Channel, SweepType);
            SSAX.SetTransient_TimeCoupling(Channel, TimeCoupling);
            //SSAX.SetTransient_NumberOfPoints(Channel, NumberOfPoints);
            NumberOfPoints = SSAX.GetTransient_NumberOfPoints(Channel);

            if (SweepType == Transient_SweepTypeEnum.WN)
            {
                SSAX.SetTransient_FrequencyRange(Channel, FrequencyRange);
                SSAX.SetTransient_WideTimeSpan(Channel, WideTimeSpan);

                double WideTimeSpanRead = SSAX.GetTransient_WideTimeSpan(Channel);
                if (WideTimeSpanRead != WideTimeSpan)
                {
                    Log.Info($"Wide Time Span setting {WideTimeSpan} read from instrument after setting: {WideTimeSpanRead}");
                    WideTimeSpan = WideTimeSpanRead;
                }
                SSAX.SetTransient_WideTimeOffset(Channel, WideTimeOffset);
                double WideTimeOffsetRead = SSAX.GetTransient_WideTimeOffset(Channel);
                if (WideTimeOffsetRead != WideTimeOffset)
                {
                    Log.Info($"Wide Time Offset setting {WideTimeOffset} read from instrument after setting: {WideTimeOffsetRead}");
                    WideTimeOffset = WideTimeOffsetRead;
                }

                SSAX.SetTransient_WideReference(Channel, WideReference);
                SSAX.SetTransient_WideVBWauto(Channel, WideVBWauto);
                if (WideVBWauto)
                {
                    //SSAX.SetTransient_WideVBW(Channel, WideVBW);
                    WideVBW = SSAX.GetTransient_WideVBW(Channel);
                    Log.Info($"Wide VBW: {WideVBW}");
                }
            }

            SSAX.SetTransient_CenterFrequency(Channel, 1, Narrow1CenterFrequency);
            SSAX.SetTransient_FrequencySpan(Channel, 1, Narrow1FrequencySpan);

            SSAX.SetTransient_NarrowTimeSpan(Channel, 1, Narrow1TimeSpan);
            double Narrow1TimeSpanRead = SSAX.GetTransient_NarrowTimeSpan(Channel,1);
            if (Narrow1TimeSpanRead != Narrow1TimeSpan)
            {
                Log.Info($"Narrow1 Time Span setting {Narrow1TimeSpan} read from instrument after setting: {Narrow1TimeSpanRead}");
                Narrow1TimeSpan = Narrow1TimeSpanRead;
            }
            SSAX.SetTransient_NarrowTimeOffset(Channel, 1, Narrow1TimeOffset);
            double Narrow1TimeOffsetRead = SSAX.GetTransient_NarrowTimeOffset(Channel, 1);
            if (Narrow1TimeOffsetRead != Narrow1TimeOffset)
            {
                Log.Info($"Narrow1 Time Offset setting {Narrow1TimeOffset} read from instrument after setting: {Narrow1TimeOffsetRead}");
                Narrow1TimeOffset = Narrow1TimeOffsetRead;
            }

            SSAX.SetTransient_NarrowReference(Channel, 1, Narrow1Reference);
            SSAX.SetTransient_NarrowVBWauto(Channel, 1, Narrow1VBWauto);
            if (WideVBWauto)
            {
                //SSAX.SetTransient_NarrowVBW(Channel, 1, Narrow1VBW);
                Narrow1VBW = SSAX.GetTransient_NarrowVBW(Channel, 1);
                Log.Info($"Narrow1 VBW: {Narrow1VBW}");
            }

            if (SweepType == Transient_SweepTypeEnum.NN)
            {
                SSAX.SetTransient_CenterFrequency(Channel, 2, Narrow2CenterFrequency);
                SSAX.SetTransient_FrequencySpan(Channel, 2, Narrow2FrequencySpan);
                SSAX.SetTransient_NarrowTimeSpan(Channel, 2, Narrow1TimeSpan);
                double Narrow2TimeSpanRead = SSAX.GetTransient_NarrowTimeSpan(Channel, 2);
                if (Narrow2TimeSpanRead != Narrow2TimeSpan)
                {
                    Log.Info($"Narrow2 Time Span setting {Narrow2TimeSpan} read from instrument after setting: {Narrow2TimeSpanRead}");
                    Narrow2TimeSpan = Narrow2TimeSpanRead;
                }
                SSAX.SetTransient_NarrowTimeOffset(Channel, 2, Narrow2TimeOffset);
                double Narrow2TimeOffsetRead = SSAX.GetTransient_NarrowTimeOffset(Channel, 2);
                if (Narrow2TimeOffsetRead != Narrow2TimeOffset)
                {
                    Log.Info($"Narrow2 Time Offset setting {Narrow2TimeOffset} read from instrument after setting: {Narrow2TimeOffsetRead}");
                    Narrow2TimeOffset = Narrow2TimeOffsetRead;
                }
                SSAX.SetTransient_NarrowReference(Channel, 2, Narrow2Reference);
                SSAX.SetTransient_NarrowVBWauto(Channel, 2, Narrow2VBWauto);
                if (WideVBWauto)
                {
                    //SSAX.SetTransient_NarrowVBW(Channel, 2, Narrow2VBW);
                    Narrow1VBW = SSAX.GetTransient_NarrowVBW(Channel, 2);
                    Log.Info($"Narrow2 VBW: {Narrow2VBW}");
                }
            }


            UpgradeVerdict(Verdict.Pass);
        }
    }
}
