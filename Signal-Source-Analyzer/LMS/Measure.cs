// Author: MyName
// Copyright:   Copyright 2021 Keysight Technologies
//              You have a royalty-free right to use, modify, reproduce and distribute
//              the sample application files (and/or any modified version) in any way
//              you find useful, provided that you agree that Keysight Technologies has no
//              warranty, obligations or liability for any sample application files.
using OpenTap;
using OpenTap.Plugins.PNAX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Signal_Source_Analyzer
{
    [Display("Trigger Channels", Groups: new[] { "Signal Source Analyzer" }, Description: "Triggers every channel")]
    public class Measure : TestStep
    {
        #region Settings
        [Display("SSA-X", Order: 0.1)]
        public SSAX SSAX { get; set; }

        [Display("Auto Select All Channels", Group: "Measurements", Order: 1)]
        public bool AutoSelectChannels { get; set; }

        [EnabledIf("AutoSelectChannels", false, HideIfDisabled = true)]
        [Display("Channels", Description: "Choose which channels to trigger.", "Measurements", Order: 2)]
        public List<int> channels { get; set; }
        // ToDo: Add property here for each parameter the end user should be able to change

        [Display("Sweep Mode", Group: "Settings", Order: 10)]
        public SweepModeEnumType sweepMode { get; set; }
        #endregion

        public Measure()
        {
            channels = new List<int>() { 1 };
            AutoSelectChannels = true;
            sweepMode = SweepModeEnumType.SING;
            // ToDo: Set default values for properties / settings.
        }

        public void AutoSelectChannelsAvailableOnInstrument()
        {
            if (AutoSelectChannels)
            {
                channels = SSAX.GetActiveChannels();
            }
        }

        public override void Run()
        {
            UpgradeVerdict(Verdict.NotSet);
            AutoSelectChannelsAvailableOnInstrument();

            try
            {
                List<int> activeChannels = SSAX.GetActiveChannels();
                channels = SSAX.ChannelListCheck(channels, activeChannels);
                // Trigger every channel
                foreach (var channel in channels)
                {
                    SSAX.SetSweepMode(channel, sweepMode);
                }
                //SSAX.WaitForOperationComplete();
            }
            catch (IndexOutOfRangeException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            UpgradeVerdict(Verdict.Pass);
        }
    }
}
