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