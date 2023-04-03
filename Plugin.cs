/*
    Copyright (C) 2023 rlen
    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.
    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.
    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.

    Contacts:
        Discord: rlen#8825
        E-Mail: contact@rlen.world
*/

using BepInEx;
using BepInEx.Configuration;
using System;

namespace ExperienceForever
{
    [BepInPlugin("org.rlenworld.plugins.experienceforever", "Experience Forever", "1.0.0.0")]
    [BepInProcess("EscapeFromTarkov.exe")]
    public class Plugin : BaseUnityPlugin
    {
        public static ConfigEntry<bool> config_enabled;
        public static ConfigEntry<float> config_skillBonus;
        public static ConfigEntry<bool> config_useAutoSkillAdjustment;
        public static ConfigEntry<float> config_metabolism;

        public static Plugin Instance { get; private set; }

        public static bool ConfigsAvailable {
            get {
                var result = true;
                result &= config_enabled != null;
                result &= config_skillBonus != null;
                result &= config_metabolism != null;

                return result;
            }
        }
        
        private void Awake()
        {
            config_enabled = Config.Bind(
                "General",
                "Enabled",
                true,
                "Enables/disables Experience Forever."
            );

            config_skillBonus = Config.Bind(
                "General",
                "Skill Point Multiplier",
                1.0f,
                "Sets your levelling speed multiplier to this value."
            );

            config_useAutoSkillAdjustment = Config.Bind(
                "Experimental",
                "Use Automatic Skill Adjustment",
                false,
                "Attempts to adjust skill gains based on how many points you earn throughout the course of the game."
            );

            config_metabolism = Config.Bind(
                "Specifics",
                "Metabolism Multiplier",
                0.1f,
                "XP rewards for Metabolism are very high, so this setting will allow you to adjust earnings to be more in line with other skills."
            );
            
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

            Instance = this;
        }

        private void Start() {
            new LevelBalancePatch().Enable();
            new EffectivenessPatch().Enable();
            new MetabolismPatch().Enable();
        }
    }
}
