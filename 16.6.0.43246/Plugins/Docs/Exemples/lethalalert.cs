using SmartBot.Plugins.API;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using SmartBot.Database;

namespace SmartBot.Plugins
{
    [Serializable]
    public class bPluginDataContainer : PluginDataContainer
    {
        public bool Overlay { get; set; }

        public bPluginDataContainer()
        {
            Name = "LethalAlert";
            Overlay = true;
        }
    }

    public class LethalAlertPlugin : Plugin
    {
        private int _alertDuration = 5000;
        private DateTime _lethalFoundDate = DateTime.MinValue;
        private int _screenWidth = 0;
        private int _screenHeight = 0;

        private GuiElementText _textElement = null;

        private int PercToPixHeight(int percent)
        {
            return (int) (((float) _screenHeight/100.0f)*(float) percent);
        }

        public override void OnStarted()
        {
            int boxWidth = 265;
            int boxHeight = 30;

            int leftBorder = (_screenWidth - boxWidth)/2;

            _textElement = new GuiElementText("!!!! LETHAL DETECTED !!!!", leftBorder, PercToPixHeight(35), boxWidth,
                boxHeight, 22, 255, 255, 255);
        }

        public override void OnTick()
        {
            GUI.RemoveElement(_textElement);

            if (_lethalFoundDate.AddMilliseconds(_alertDuration) > DateTime.Now &&
                ((bPluginDataContainer) DataContainer).Overlay)
            {
                GUI.AddElement(_textElement);
            }
        }

        public override void OnLethal()
        {
            _lethalFoundDate = DateTime.Now;
            Bot.Log("[PLUGIN] -> !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Bot.Log("[PLUGIN] -> !!!!!!!!! LETHAL DETECTED !!!!!!!!");
            Bot.Log("[PLUGIN] -> !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            System.Media.SystemSounds.Exclamation.Play();
        }

        public override void OnGameResolutionUpdate(int width, int height)
        {
            _screenWidth = width;
            _screenHeight = height;
        }
    }
}