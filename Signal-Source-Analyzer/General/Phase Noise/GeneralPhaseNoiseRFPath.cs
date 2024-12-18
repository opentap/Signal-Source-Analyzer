using OpenTap;
using OpenTap.Plugins.PNAX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signal_Source_Analyzer
{

    [Display("RF Path", Groups: new[] { "Signal Source Analyzer", "General", "Phase Noise" }, Description: "Insert a description here")]
    public class GeneralPhaseNoiseRFPath : SSAXBaseStep
    {
        #region Settings
        [Browsable(false)]
        public bool IsPropertyReadOnly { get; set; } = true;

        private PhaseNoise_NoiseTypeEnum _NoiseType;
        [EnabledIf("IsPropertyReadOnly", false, HideIfDisabled = false)]
        [Display("Sweep Type", Order: 10.01, Description: "Sets and read the transition mode, Wide-Narrow or Narrow-Narrow.")]
        public PhaseNoise_NoiseTypeEnum NoiseType
        {
            get
            {
                return _NoiseType;
            }
            set
            {
                _NoiseType = value;
            }
        }

        [Display("Always Use Internal Reference", Order: 20.01, Description: "")]
        public bool AlwaysUseInternalReference { get; set; }

        [EnabledIf("NoiseType", PhaseNoise_NoiseTypeEnum.PNOise, HideIfDisabled = true)]
        [Display("RF Input", Group: "DUT", Order: 30.01, Description: "Set and read the input port.")]
        public SSAXPortsEnum RFInput { get; set; }


        [EnabledIf("NoiseType", PhaseNoise_NoiseTypeEnum.RESidual, HideIfDisabled = true)]
        [Display("Number of DUT", Group: "DUT", Order: 40.01, Description: "This setting does not change anything in the analyzer.")]
        public int NumberOfDut { get; set; }

        [EnabledIf("NoiseType", PhaseNoise_NoiseTypeEnum.RESidual, HideIfDisabled = true)]
        [Display("Source", Group: "DUT", Order: 40.02, Description: "This setting does not change anything in the analyzer.")]
        public PhaseNoiseSourceEnum Source { get; set; }

        [EnabledIf("NoiseType", PhaseNoise_NoiseTypeEnum.RESidual, HideIfDisabled = true)]
        [Display("Source Output", Groups: new string[] { "DUT", "Path 1" }, Order: 40.03, Description: "This setting does not change anything in the analyzer.")]
        public PhaseNoiseSourceOutEnum Path1Source { get; set; }

        [EnabledIf("NoiseType", PhaseNoise_NoiseTypeEnum.RESidual, HideIfDisabled = true)]
        [Display("RF Input Output", Groups: new string[] { "DUT", "Path 1" }, Order: 40.04, Description: "This setting does not change anything in the analyzer.")]
        public SSAXPortsEnum Path1Input { get; set; }

        [EnabledIf("NoiseType", PhaseNoise_NoiseTypeEnum.RESidual, HideIfDisabled = true)]
        [Display("Source Output", Groups: new string[] { "DUT", "Path 2" }, Order: 40.05, Description: "This setting does not change anything in the analyzer.")]
        public PhaseNoiseSourceOutEnum Path2Source { get; set; }

        [EnabledIf("NoiseType", PhaseNoise_NoiseTypeEnum.RESidual, HideIfDisabled = true)]
        [Display("RF Input Output", Groups: new string[] { "DUT", "Path 2" }, Order: 40.06, Description: "This setting does not change anything in the analyzer.")]
        public SSAXPortsEnum Path2Input { get; set; }



        [EnabledIf("NoiseType", PhaseNoise_NoiseTypeEnum.PNOise, PhaseNoise_NoiseTypeEnum.RESidual, HideIfDisabled = true)]
        [Display("Auto Range", Group: "Settings", Order: 50.01, Description: "")]
        public bool AutoRange1 { get; set; }

        [EnabledIf("NoiseType", PhaseNoise_NoiseTypeEnum.PNOise, PhaseNoise_NoiseTypeEnum.RESidual, HideIfDisabled = true)]
        [EnabledIf("AutoRange1", false, HideIfDisabled = false)]
        [Display("Max Input Level", Group: "Settings", Order: 50.02, Description: "")]
        [Unit("dBm", UseEngineeringPrefix: true, StringFormat: "0.00")]
        public double MaxInputLevel1 { get; set; }

        [EnabledIf("NoiseType", PhaseNoise_NoiseTypeEnum.PNOise, PhaseNoise_NoiseTypeEnum.RESidual, HideIfDisabled = true)]
        [Display("Enable Attenuator Setting", Group: "Settings", Order: 50.03, Description: "")]
        public bool EnableAttenuatorSetting1 { get; set; }

        [EnabledIf("NoiseType", PhaseNoise_NoiseTypeEnum.PNOise, PhaseNoise_NoiseTypeEnum.RESidual, HideIfDisabled = true)]
        [EnabledIf("EnableAttenuatorSetting1", true, HideIfDisabled = false)]
        [Display("Max Input Level", Group: "Settings", Order: 50.04, Description: "")]
        [Unit("dB", UseEngineeringPrefix: true, StringFormat: "0")]
        public double Attenuator1 { get; set; }


        [EnabledIf("NoiseType", PhaseNoise_NoiseTypeEnum.RESidual, HideIfDisabled = true)]
        [Display("Auto Range 2", Group: "Settings", Order: 50.05, Description: "")]
        public bool AutoRange2 { get; set; }

        [EnabledIf("NoiseType", PhaseNoise_NoiseTypeEnum.RESidual, HideIfDisabled = true)]
        [EnabledIf("AutoRange2", false, HideIfDisabled = false)]
        [Display("Max Input Level 2", Group: "Settings", Order: 50.06, Description: "")]
        [Unit("dBm", UseEngineeringPrefix: true, StringFormat: "0.00")]
        public double MaxInputLevel2 { get; set; }

        [EnabledIf("NoiseType", PhaseNoise_NoiseTypeEnum.RESidual, HideIfDisabled = true)]
        [Display("Enable Attenuator Setting 2", Group: "Settings", Order: 50.07, Description: "")]
        public bool EnableAttenuatorSetting2 { get; set; }

        [EnabledIf("NoiseType", PhaseNoise_NoiseTypeEnum.RESidual, HideIfDisabled = true)]
        [EnabledIf("EnableAttenuatorSetting2", true, HideIfDisabled = false)]
        [Display("Max Input Level 2", Group: "Settings", Order: 50.08, Description: "")]
        [Unit("dB", UseEngineeringPrefix: true, StringFormat: "0")]
        public double Attenuator2 { get; set; }







        [EnabledIf("NoiseType", PhaseNoise_NoiseTypeEnum.BASeband, HideIfDisabled = true)]
        [Display("Baseband Input", Group: "Settings", Order: 60.01, Description: "Sets and returns the setting for RF path input.")]
        public PhaseNoise_BasebandInputEnum BasebandInput { get; set; }

        [EnabledIf("NoiseType", PhaseNoise_NoiseTypeEnum.BASeband, HideIfDisabled = true)]
        [Display("Baseband Gain", Group: "Settings", Order: 60.02, Description: "Sets and returns the setting for RF path gain.")]
        [Unit("dB", UseEngineeringPrefix: true, StringFormat: "")]
        public double BasebandGain { get; set; }

        #endregion


        public GeneralPhaseNoiseRFPath()
        {
            NoiseType = PhaseNoise_NoiseTypeEnum.PNOise;

            AlwaysUseInternalReference = false;
            RFInput = SSAXPortsEnum.Port1;

            NumberOfDut = 1;
            Source = PhaseNoiseSourceEnum.INTernal;
            Path1Source = PhaseNoiseSourceOutEnum.Source1Out;
            Path1Input = SSAXPortsEnum.Port1;
            Path2Source = PhaseNoiseSourceOutEnum.Source2Out;
            Path2Input = SSAXPortsEnum.Port2;

            AutoRange1 = true;
            MaxInputLevel1 = 10;
            EnableAttenuatorSetting1 = false;
            Attenuator1 = 10;

            AutoRange2 = true;
            MaxInputLevel2 = 10;
            EnableAttenuatorSetting2 = false;
            Attenuator2 = 10;

            BasebandInput = PhaseNoise_BasebandInputEnum.ACLF;
            BasebandGain = 0;
        }

        public override void Run()
        {
            RunChildSteps(); //If the step supports child steps.

            SSAX.SetPhaseNoise_AlwaysUseInternalReference(Channel, AlwaysUseInternalReference);
            if (NoiseType == PhaseNoise_NoiseTypeEnum.PNOise)
            {
                SSAX.SetPhaseNoise_RFInput(Channel, RFInput);

                SSAX.SetPhaseNoise_AutoRange(Channel, SSAXPortsEnum.Port1, AutoRange1);
                SSAX.SetPhaseNoise_MaxInputLevel(Channel, SSAXPortsEnum.Port1, MaxInputLevel1);
                SSAX.SetPhaseNoise_EnableAttenuatorSetting(Channel, SSAXPortsEnum.Port1, EnableAttenuatorSetting1);
                SSAX.SetPhaseNoise_Attenuator(Channel, SSAXPortsEnum.Port1, Attenuator1);
            }
            else if (NoiseType == PhaseNoise_NoiseTypeEnum.RESidual)
            {
                SSAX.SetPhaseNoise_AutoRange(Channel, SSAXPortsEnum.Port1, AutoRange1);
                SSAX.SetPhaseNoise_MaxInputLevel(Channel, SSAXPortsEnum.Port1, MaxInputLevel1);
                SSAX.SetPhaseNoise_EnableAttenuatorSetting(Channel, SSAXPortsEnum.Port1, EnableAttenuatorSetting1);
                SSAX.SetPhaseNoise_Attenuator(Channel, SSAXPortsEnum.Port1, Attenuator1);

                SSAX.SetPhaseNoise_AutoRange(Channel, SSAXPortsEnum.Port2, AutoRange2);
                SSAX.SetPhaseNoise_MaxInputLevel(Channel, SSAXPortsEnum.Port2, MaxInputLevel2);
                SSAX.SetPhaseNoise_EnableAttenuatorSetting(Channel, SSAXPortsEnum.Port2, EnableAttenuatorSetting2);
                SSAX.SetPhaseNoise_Attenuator(Channel, SSAXPortsEnum.Port2, Attenuator2);
            }
            else if (NoiseType == PhaseNoise_NoiseTypeEnum.BASeband)
            {
                SSAX.SetPhaseNoise_BasebandInput(Channel, BasebandInput);
                SSAX.SetPhaseNoise_BasebandGain(Channel, BasebandGain);
            }


           

            UpgradeVerdict(Verdict.Pass);
        }

        
    }
}
