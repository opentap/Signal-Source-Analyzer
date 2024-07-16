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
        [Display("Sweep Type", Group: "Settings", Order: 10.01, Description: "Sets and read the transition mode, Wide-Narrow or Narrow-Narrow.")]
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

        [Browsable(false)]
        [Display("RF Input", Group: "Settings", Order: 20.001, Description: "Set and read the input port.")]
        public SSAXPortsEnum RFInput { get; set; }

        [EnabledIf("NoiseType", PhaseNoise_NoiseTypeEnum.PNOise, HideIfDisabled = true)]
        [Display("Enable Attenuator Setting", Group: "Settings", Order: 20.01, Description: "Set and query the state of auto calculate attenuation setting from INPut:LEVel:MAXimum. This setting is applicable when SENS:PN:POW:INP:LEV:AUTO is set at OFF.")]
        public bool EnableAttenuatorSetting { get; set; }

        [EnabledIf("NoiseType", PhaseNoise_NoiseTypeEnum.PNOise, HideIfDisabled = true)]
        [Display("Attenuator", Group: "Settings", Order: 20.02, Description: "Set and query manual attenuation setting. This setting is applied when SENS:PN:POW:INP:ATT:AUTO is set at OFF. This command overwrite the value of  SENS:PN:ATTenuation:VALue.")]
        [Unit("dBm", UseEngineeringPrefix: true, StringFormat: "0.00")]
        public double Attenuator { get; set; }

        [EnabledIf("NoiseType", PhaseNoise_NoiseTypeEnum.BASeband, HideIfDisabled = true)]
        [Display("Baseband Input", Group: "Settings", Order: 20.03, Description: "Sets and returns the setting for RF path input.")]
        public PhaseNoise_BasebandInputEnum BasebandInput { get; set; }

        [EnabledIf("NoiseType", PhaseNoise_NoiseTypeEnum.BASeband, HideIfDisabled = true)]
        [Display("Baseband Gain", Group: "Settings", Order: 20.04, Description: "Sets and returns the setting for RF path gain.")]
        [Unit("dB", UseEngineeringPrefix: true, StringFormat: "")]
        public double BasebandGain { get; set; }

        //[Display("Baseband Discharge", Group: "Settings", Order: 20.05, Description: "Executes the Dischage DC Block C.")]
        //public void BasebandDischarge { get; set; }

        [EnabledIf("NoiseType", PhaseNoise_NoiseTypeEnum.PNOise, HideIfDisabled = true)]
        [Display("Analyzer Input", Group: "Settings", Order: 20.06, Description: "Sets and returns the receiver for the phase noise measurement or receiver ratios for residual phase noise measurements.")]
        public PhaseNoise_AnalyzerInputEnum AnalyzerInput { get; set; }

        [EnabledIf("NoiseType", PhaseNoise_NoiseTypeEnum.PNOise, HideIfDisabled = true)]
        [Display("Receiver Attenuation", Group: "Settings", Order: 20.07, Description: "Sets the attenuation level for the specified test attenuator/port.")]
        [Unit("dBm", UseEngineeringPrefix: true, StringFormat: "0")]
        public double ReceiverAttenuation { get; set; }

        [EnabledIf("NoiseType", PhaseNoise_NoiseTypeEnum.PNOise, HideIfDisabled = true)]
        [Display("RF Path Configuration", Group: "Settings", Order: 20.08, Description: "Write or read the setting of a specified element in the current configuration.")]
        public string RFPathConfiguration { get; set; }




        #endregion


        public GeneralPhaseNoiseRFPath()
        {
            NoiseType = PhaseNoise_NoiseTypeEnum.PNOise;

            RFInput = SSAXPortsEnum.Port1;

            EnableAttenuatorSetting = true;
            Attenuator = 10;
            BasebandInput = PhaseNoise_BasebandInputEnum.ACLF;
            BasebandGain = 0;
            //BasebandDischarge = N / A;
            AnalyzerInput = PhaseNoise_AnalyzerInputEnum.b2;
            ReceiverAttenuation = 20;
            RFPathConfiguration = "Auto";

            
        }

        public override void Run()
        {
            RunChildSteps(); //If the step supports child steps.

            SSAX.SetPhaseNoise_EnableAttenuatorSetting(Channel, EnableAttenuatorSetting);
            SSAX.SetPhaseNoise_BasebandInput(Channel, BasebandInput);
            SSAX.SetPhaseNoise_BasebandGain(Channel, BasebandGain);
            //SSAX.SetPhaseNoise_BasebandDischarge(Channel);
            SSAX.SetPhaseNoise_AnalyzerInput(Channel, AnalyzerInput);
            SSAX.SetPhaseNoise_ReceiverAttenuation(Channel, RFInput, ReceiverAttenuation);
            SSAX.SetPhaseNoise_RFPathConfiguration(Channel, PhaseNoise_RFPathConfigurationEnum.IFGAINb1, RFPathConfiguration);

           

            UpgradeVerdict(Verdict.Pass);
        }

        
    }
}
