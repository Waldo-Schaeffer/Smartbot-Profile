using SmartBot.Plugins.API;
using System;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.ComponentModel;

namespace SmartBot.Plugins
{
    [Serializable]
    public class bPluginDataContainer : PluginDataContainer
    {
        public int GoldAmountToKeep { get; set; }

        [ItemsSource(typeof(ProfileStringSource))]
        public string DefaultProfile { get; set; }

        [ItemsSource(typeof(MulliganStringSource))]
        public string DefaultMulligan { get; set; }

        [ItemsSource(typeof(ProfileStringSource))]
        public string ArenaProfile { get; set; }

        [ItemsSource(typeof(MulliganStringSource))]
        public string ArenaMulligan { get; set; }
        public Bot.Mode DefaultMode { get; set; }

        //Init vars
        public bPluginDataContainer()
        {
            Name = "ArenaSwitcher";
        }
    }

    public class bPlugin : Plugin
    {
        public override void OnStarted()
        {
            Init();
        }

        public override void OnArenaEnd()
        {
            if (ShouldSwitchToOldMode())
                SwitchToOldMode();
        }

        public override void OnGameEnd()
        {
            if (ShouldSwitchToArena())
                SwitchToArena();
        }

        /* --------------- ArenaSwitcher Methods -------------- */

        private int _goldToKeep;
        private int _arenaPrice;
        private Bot.Mode _oldMode;

        private string _defaultProfile = string.Empty;
        private string _arenaProfile = string.Empty;

        private string _defaultMulligan = string.Empty;
        private string _arenaMulligan = string.Empty;

        private void Init()
        {
            if (!DataContainer.Enabled)
                return;

            _defaultProfile = ((bPluginDataContainer)DataContainer).DefaultProfile;
            _arenaMulligan = ((bPluginDataContainer)DataContainer).ArenaMulligan;
            _arenaProfile = ((bPluginDataContainer)DataContainer).ArenaProfile;
            _defaultMulligan = ((bPluginDataContainer)DataContainer).DefaultMulligan;
            _goldToKeep = ((bPluginDataContainer)DataContainer).GoldAmountToKeep;
            _arenaPrice = 150;
            _oldMode = ((bPluginDataContainer)DataContainer).DefaultMode;

            Bot.Log("[PLUGIN] -> ArenaSwitcher : Initialized...");
            Bot.Log("[PLUGIN] -> ArenaSwitcher : Keep " + _goldToKeep + " gold");

            if (!string.IsNullOrEmpty(_defaultProfile) && !string.IsNullOrEmpty(_defaultMulligan))
                Bot.Log("[PLUGIN] -> ArenaSwitcher : Default settings -> " + _defaultProfile + " - " + _defaultMulligan);
            else
                Bot.Log("[PLUGIN] -> ArenaSwitcher : You need to set default deck and profile settings");

            if (!string.IsNullOrEmpty(_arenaProfile) && !string.IsNullOrEmpty(_arenaMulligan))
                Bot.Log("[PLUGIN] -> ArenaSwitcher : Arena settings -> " + _arenaProfile + " - " + _arenaMulligan);
            else
                Bot.Log("[PLUGIN] -> ArenaSwitcher : You need to set arena deck and profile settings");

            if ((Bot.CurrentMode() != Bot.Mode.Arena && Bot.CurrentMode() != Bot.Mode.ArenaAuto))
            {
                if (ShouldSwitchToArena())
                    SwitchToArena();
            }
        }

        private bool ShouldSwitchToArena()
        {
            return (SmartBot.Plugins.API.Statistics.Gold >= _goldToKeep + _arenaPrice && (Bot.CurrentMode() != Bot.Mode.Arena && Bot.CurrentMode() != Bot.Mode.ArenaAuto));
        }

        private bool ShouldSwitchToOldMode()
        {
            return (SmartBot.Plugins.API.Statistics.Gold < _goldToKeep + _arenaPrice && (Bot.CurrentMode() == Bot.Mode.Arena || Bot.CurrentMode() == Bot.Mode.ArenaAuto));
        }

        private void SwitchToArena()
        {
            if (Bot.CurrentMode() == Bot.Mode.ArenaAuto)
                return;

            Bot.Log("[PLUGIN] -> ArenaSwitcher : Switching to arena (" + _arenaProfile + " - " + _arenaMulligan + ") ...");
            Bot.ChangeMode(Bot.Mode.ArenaAuto);
            Bot.ChangeProfile(_arenaProfile);
            Bot.ChangeMulligan(_arenaMulligan);
        }

        private void SwitchToOldMode()
        {
            if (Bot.CurrentMode() == _oldMode)
                return;

            Bot.Log("[PLUGIN] -> ArenaSwitcher : Switching to " + _oldMode.ToString() + " (" + _defaultProfile + " - " + _defaultMulligan + ")...");
            Bot.ChangeMode(_oldMode);
            Bot.ChangeProfile(_defaultProfile);
            Bot.ChangeMulligan(_defaultMulligan);
        }
    }
}