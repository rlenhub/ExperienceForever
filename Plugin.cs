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

namespace ExperienceForever
{
    [BepInPlugin("org.rlenworld.plugins.experienceforever", "Experience Forever", "1.0.0.0")]
    [BepInProcess("EscapeFromTarkov.exe")]
    public class Plugin : BaseUnityPlugin
    {
        private ConfigEntry<bool> config_enabled;
        private ConfigEntry<float> config_skillBonus;
        
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
            
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }

        private void Start() {
            ExpForever.Enabled = config_enabled.Value;
            ExpForever.Multiplier = config_skillBonus.Value;
            new ExpForever().Enable();
        }

        private void Update() {
            ExpForever.Enabled = config_enabled.Value;
            ExpForever.Multiplier = System.Math.Max(0.0f, config_skillBonus.Value);
        }
    }
}
