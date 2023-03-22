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
