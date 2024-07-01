using OpenTap;
using OpenTap.Plugins.PNAX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signal_Source_Analyzer
{

    [Display("Sweep", Groups: new[] { "Signal Source Analyzer", "General", "Transient" }, Description: "Insert a description here")]
    public class GeneralTransientSweep : SSAXBaseStep
    {
        #region Settings

        [Display("Sweep Type", Group: "Settings", Order: 20.01, Description: "Sets and read the transition mode, Wide-Narrow or Narrow-Narrow.")]
        public Transient_SweepTypeEnum SweepType { get; set; }

        [Display("Time Coupling", Group: "Settings", Order: 20.02, Description: "Enable and disable the time coupling for two measurements (Wide-Narrow or Narrow1-Narrow2")]
        public bool TimeCoupling { get; set; }

        [Display("Number Of Points", Group: "Settings", Order: 20.03, Description: "Sets the number of data points for the measurement.")]
        public int NumberOfPoints { get; set; }

        [EnabledIf("SweepType", Transient_SweepTypeEnum.WN, HideIfDisabled = true)]
        [Display("Frequency Range", Group: "Wide Mode", Order: 30.01, Description: "Set and read the frequency range for wide band.")]
        public Transient_FrequencyRangeEnum FrequencyRange { get; set; }

        [EnabledIf("SweepType", Transient_SweepTypeEnum.WN, HideIfDisabled = true)]
        [Display("Wide Time Span", Group: "Wide Mode", Order: 30.02, Description: "Set and read the time span of wide band.")]
        [Unit("S", UseEngineeringPrefix: true, StringFormat: "0.000")]
        public double WideTimeSpan { get; set; }

        [EnabledIf("SweepType", Transient_SweepTypeEnum.WN, HideIfDisabled = true)]
        [Display("Wide Time Offset", Group: "Wide Mode", Order: 30.03, Description: "Set and read the time offset for wide band.")]
        [Unit("S", UseEngineeringPrefix: true, StringFormat: "")]
        public double WideTimeOffset { get; set; }

        [EnabledIf("SweepType", Transient_SweepTypeEnum.WN, HideIfDisabled = true)]
        [Display("Wide Reference", Group: "Wide Mode", Order: 30.04, Description: "Set and read the position at the reference point of wide band.")]
        public Transient_WideReferenceEnum WideReference { get; set; }

        [EnabledIf("SweepType", Transient_SweepTypeEnum.WN, HideIfDisabled = true)]
        [Display("Wide VBW", Group: "Wide Mode", Order: 30.05, Description: "Sets and read the Video Band Width (VBW")]
        [Unit("Hz", UseEngineeringPrefix: true, StringFormat: "0.000000")]
        public double WideVBW { get; set; }

        [EnabledIf("SweepType", Transient_SweepTypeEnum.WN, HideIfDisabled = true)]
        [Display("Wide VBW auto", Group: "Wide Mode", Order: 30.06, Description: "Sets and read the Video Band Width (VBW")]
        public bool WideVBWauto { get; set; }

        [Display("Center Frequency", Group: "Narrow1", Order: 40.01, Description: "Set and read the center frequency for Narrow1 or Narrow2.")]
        [Unit("Hz", UseEngineeringPrefix: true, StringFormat: "0.000000000")]
        public double Narrow1CenterFrequency { get; set; }

        [Display("Frequency Span", Group: "Narrow1", Order: 40.02, Description: "Set and read the frequency span of narrow band 1/2.")]
        public Transient_FrequencySpanEnum Narrow1FrequencySpan { get; set; }

        [Display("Narrow Time Span", Group: "Narrow1", Order: 40.03, Description: "Set and read the time span of the narrow band 1 or 2.")]
        [Unit("S", UseEngineeringPrefix: true, StringFormat: "0.000")]
        public double Narrow1TimeSpan { get; set; }

        [Display("Narrow Time Offset", Group: "Narrow1", Order: 40.04, Description: "Set and read the offset time at the reference point for the narrow band 1 or 2.")]
        [Unit("S", UseEngineeringPrefix: true, StringFormat: "0")]
        public double Narrow1TimeOffset { get; set; }

        [Display("Narrow Reference", Group: "Narrow1", Order: 40.05, Description: "Set and read the position at the reference point of the narrow band 1 or 2.")]
        public Transient_NarrowReferenceEnum Narrow1Reference { get; set; }

        [Display("Narrow VBW", Group: "Narrow1", Order: 40.06, Description: "Sets and read the Video Band Width (VBW")]
        [Unit("Hz", UseEngineeringPrefix: true, StringFormat: "0.000000")]
        public double Narrow1VBW { get; set; }

        [Display("Narrow VBW auto", Group: "Narrow1", Order: 40.07, Description: "Sets and read the Video Band Width (VBW")]
        public bool Narrow1VBWauto { get; set; }

        [EnabledIf("SweepType", Transient_SweepTypeEnum.NN, HideIfDisabled = true)]
        [Display("Center Frequency", Group: "Narrow2", Order: 50.01, Description: "Set and read the center frequency for Narrow1 or Narrow2.")]
        [Unit("Hz", UseEngineeringPrefix: true, StringFormat: "0.000000000")]
        public double Narrow2CenterFrequency { get; set; }

        [EnabledIf("SweepType", Transient_SweepTypeEnum.NN, HideIfDisabled = true)]
        [Display("Frequency Span", Group: "Narrow2", Order: 50.02, Description: "Set and read the frequency span of narrow band 1/2.")]
        public Transient_FrequencySpanEnum Narrow2FrequencySpan { get; set; }

        [EnabledIf("SweepType", Transient_SweepTypeEnum.NN, HideIfDisabled = true)]
        [Display("Narrow Time Span", Group: "Narrow2", Order: 50.03, Description: "Set and read the time span of the narrow band 1 or 2.")]
        [Unit("S", UseEngineeringPrefix: true, StringFormat: "0.000")]
        public double Narrow2TimeSpan { get; set; }

        [EnabledIf("SweepType", Transient_SweepTypeEnum.NN, HideIfDisabled = true)]
        [Display("Narrow Time Offset", Group: "Narrow2", Order: 50.04, Description: "Set and read the offset time at the reference point for the narrow band 1 or 2.")]
        [Unit("S", UseEngineeringPrefix: true, StringFormat: "0")]
        public double Narrow2TimeOffset { get; set; }

        [EnabledIf("SweepType", Transient_SweepTypeEnum.NN, HideIfDisabled = true)]
        [Display("Narrow Reference", Group: "Narrow2", Order: 50.05, Description: "Set and read the position at the reference point of the narrow band 1 or 2.")]
        public Transient_NarrowReferenceEnum Narrow2Reference { get; set; }

        [EnabledIf("SweepType", Transient_SweepTypeEnum.NN, HideIfDisabled = true)]
        [Display("Narrow VBW", Group: "Narrow2", Order: 50.06, Description: "Sets and read the Video Band Width (VBW")]
        [Unit("Hz", UseEngineeringPrefix: true, StringFormat: "0.000000")]
        public double Narrow2VBW { get; set; }

        [EnabledIf("SweepType", Transient_SweepTypeEnum.NN, HideIfDisabled = true)]
        [Display("Narrow VBW auto", Group: "Narrow2", Order: 50.07, Description: "Sets and read the Video Band Width (VBW")]
        public bool Narrow2VBWauto { get; set; }


        #endregion

        public GeneralTransientSweep()
        {
            SweepType = Transient_SweepTypeEnum.WN;
            TimeCoupling = false;
            NumberOfPoints = 201;

            FrequencyRange = Transient_FrequencyRangeEnum.R8G;
            WideTimeSpan = 0.1;
            WideTimeOffset = 0;
            WideReference = Transient_WideReferenceEnum.CENTer;
            WideVBW = 2000;
            WideVBWauto = true;

            Narrow1CenterFrequency = 1000000000;
            Narrow1FrequencySpan = Transient_FrequencySpanEnum.R30;
            Narrow1TimeSpan = 0.1;
            Narrow1TimeOffset = 0;
            Narrow1Reference = Transient_NarrowReferenceEnum.CENTer;
            Narrow1VBW = 2000;
            Narrow1VBWauto = true;

            Narrow2CenterFrequency = 1000000000;
            Narrow2FrequencySpan = Transient_FrequencySpanEnum.R30;
            Narrow2TimeSpan = 0.1;
            Narrow2TimeOffset = 0;
            Narrow2Reference = Transient_NarrowReferenceEnum.CENTer;
            Narrow2VBW = 2000;
            Narrow2VBWauto = true;

        }

        public override void Run()
        {
            SSAX.SetTransient_SweepType(Channel, SweepType);
            SSAX.SetTransient_TimeCoupling(Channel, TimeCoupling);
            SSAX.SetTransient_NumberOfPoints(Channel, NumberOfPoints);

            if (SweepType == Transient_SweepTypeEnum.WN)
            {
                SSAX.SetTransient_FrequencyRange(Channel, FrequencyRange);
                SSAX.SetTransient_WideTimeSpan(Channel, WideTimeSpan);
                SSAX.SetTransient_WideTimeOffset(Channel, WideTimeOffset);
                SSAX.SetTransient_WideReference(Channel, WideReference);
                SSAX.SetTransient_WideVBW(Channel, WideVBW);
                SSAX.SetTransient_WideVBWauto(Channel, WideVBWauto);
            }

            SSAX.SetTransient_CenterFrequency(Channel, 1, Narrow1CenterFrequency);
            SSAX.SetTransient_FrequencySpan(Channel, 1, Narrow1FrequencySpan);
            SSAX.SetTransient_NarrowTimeSpan(Channel, 1, Narrow1TimeSpan);
            SSAX.SetTransient_NarrowTimeOffset(Channel, 1, Narrow1TimeOffset);
            SSAX.SetTransient_NarrowReference(Channel, 1, Narrow1Reference);
            SSAX.SetTransient_NarrowVBW(Channel, 1, Narrow1VBW);
            SSAX.SetTransient_NarrowVBWauto(Channel, 1, Narrow1VBWauto);

            if (SweepType == Transient_SweepTypeEnum.NN)
            {
                SSAX.SetTransient_CenterFrequency(Channel, 2, Narrow2CenterFrequency);
                SSAX.SetTransient_FrequencySpan(Channel, 2, Narrow2FrequencySpan);
                SSAX.SetTransient_NarrowTimeSpan(Channel, 2, Narrow2TimeSpan);
                SSAX.SetTransient_NarrowTimeOffset(Channel, 2, Narrow2TimeOffset);
                SSAX.SetTransient_NarrowReference(Channel, 2, Narrow2Reference);
                SSAX.SetTransient_NarrowVBW(Channel, 2, Narrow2VBW);
                SSAX.SetTransient_NarrowVBWauto(Channel, 2, Narrow2VBWauto);
            }


            UpgradeVerdict(Verdict.Pass);
        }
    }
}
