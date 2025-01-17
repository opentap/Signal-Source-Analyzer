using OpenTap;
using OpenTap.Plugins.PNAX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;

namespace Signal_Source_Analyzer
{
    [Display("Store Integrated Noise Table", Groups: new[] { "Signal Source Analyzer", "General", "Phase Noise" }, Description: "Insert a description here")]
    public class StorePhaseNoiseIntegratedNoiseTable : TestStep
    {
        #region Settings
        [Display("PNA", null, null, 0.1, false, null)]
        public SSAX SSAX { get; set; }

        [Display("Channel", "Choose which channel to grab data from.", "Measurements", 10.1, false, null)]
        public int Channel { get; set; }

        [Display("MNum", Groups: new[] { "Trace" }, Order: 11)]
        public List<int> mnum { get; set; }

        #endregion

        public StorePhaseNoiseIntegratedNoiseTable()
        {
            Channel = 1;
            mnum = new List<int>() { 1 };
        }

        public override void Run()
        {
            UpgradeVerdict(Verdict.NotSet);

            RunChildSteps(); //If the step supports child steps.

            List<string> ResultNames = new List<string>();
            ResultNames.Add("Trace");
            ResultNames.Add("Start Offset");
            ResultNames.Add("Stop Offset");
            ResultNames.Add("Weighting");
            ResultNames.Add("Integ Noise");
            ResultNames.Add("Residual FM");
            ResultNames.Add("Residual AM");
            ResultNames.Add("Residual PM");
            ResultNames.Add("RMS Jitter");
            ResultNames.Add("RMS Radian");
            ResultNames.Add("RMS Degree");

            // Find Traces
            // Get all measurements for current channel
            mnum = SSAX.GetChannelMeasurements(Channel);


            // foreach trace
            foreach (int measurement in mnum)
            {
                // for range = 1 to 4
                for (int range = 1; range <= 4; range++)
                {
                    // find if range is different than off
                    PhaseNoise_IntegratedRangeTypeEnum IntegratedRangeTyp = SSAX.GetPhaseNoise_IntegratedRangeType(Channel, measurement, range);
                    if (IntegratedRangeTyp != PhaseNoise_IntegratedRangeTypeEnum.OFF)
                    {
                        List<IConvertible> ResultValues = new List<IConvertible>();

                        // trace
                        ResultValues.Add(measurement);

                        // get start
                        double start = SSAX.GetPhaseNoise_Start(Channel, measurement, range);
                        ResultValues.Add(start);
                        // get stop
                        double stop = SSAX.GetPhaseNoise_Stop(Channel, measurement, range);
                        ResultValues.Add(stop);
                        // get weighting
                        string weighting = SSAX.GetPhaseNoise_WeightingFilter(Channel, measurement, range);
                        ResultValues.Add(weighting);
                        // get Integ Noise
                        double integNoise = SSAX.GetPhaseNoise_IntegratedNoise(Channel, measurement, range, PhaseNoiseIntegratedNoiseDataEnum.IPN);
                        ResultValues.Add(integNoise);
                        // get RFM
                        double RFM = SSAX.GetPhaseNoise_IntegratedNoise(Channel, measurement, range, PhaseNoiseIntegratedNoiseDataEnum.RFM);
                        ResultValues.Add(RFM);
                        // get RAM
                        double RAM = SSAX.GetPhaseNoise_IntegratedNoise(Channel, measurement, range, PhaseNoiseIntegratedNoiseDataEnum.RAM);
                        ResultValues.Add(RAM);
                        // get RPM
                        double RPM = SSAX.GetPhaseNoise_IntegratedNoise(Channel, measurement, range, PhaseNoiseIntegratedNoiseDataEnum.RPM);
                        ResultValues.Add(RPM);
                        // get RMS Jitter
                        double RMSJitter = SSAX.GetPhaseNoise_IntegratedNoise(Channel, measurement, range, PhaseNoiseIntegratedNoiseDataEnum.RMSJ);
                        ResultValues.Add(RMSJitter);
                        // get RMS Radian
                        double RMSRadian = SSAX.GetPhaseNoise_IntegratedNoise(Channel, measurement, range, PhaseNoiseIntegratedNoiseDataEnum.RMSR);
                        ResultValues.Add(RMSRadian);
                        // get RMS Degree
                        double RMSDegree = SSAX.GetPhaseNoise_IntegratedNoise(Channel, measurement, range, PhaseNoiseIntegratedNoiseDataEnum.RMSD);
                        ResultValues.Add(RMSDegree);

                        // Publish
                        Results.Publish($"PN_IntegratedNoise_Channel_{Channel.ToString()}", ResultNames, ResultValues.ToArray());
                    }
                }

            }

            UpgradeVerdict(Verdict.Pass);
        }
    }
}
