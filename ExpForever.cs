using Aki.Reflection.Patching;
using Aki.Reflection.Utils;
using Comfort.Common;
using System.Linq;
using System.Reflection;
using BepInEx;

namespace ExperienceForever {
    public class ExpForever : ModulePatch {
        public static bool Enabled = true;
        public static float Multiplier = 1.0f;
        
        protected override MethodBase GetTargetMethod() {
            return typeof(SkillsClass).GetMethod("GetEffectiveness", BindingFlags.Public | BindingFlags.Instance);
        }

        // [PatchPrefix]
        // private static void PatchPrefix(ref int skillPointsEarned) {
        //     BackendConfigSettingsClass instance = Singleton<BackendConfigSettingsClass>.Instance;
            
		//     int skillFreshPoints = instance.SkillFreshPoints;
        //     skillPointsEarned = 0;
        // }

        [PatchPostfix]
        private static void PatchPostfix(ref int skillPointsEarned, ref float __result) {
            __result = Enabled ? Multiplier : __result;
        }
    }
}