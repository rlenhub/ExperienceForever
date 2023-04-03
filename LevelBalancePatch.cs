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
using EFT;
using Comfort.Common;
using System;
using System.Linq;
using System.Reflection;
using BepInEx;

namespace ExperienceForever {
    public class LevelBalancePatch : ModulePatch {
        // These values are all opinionated and will be adjusted based on community feedback.
        // If you don't like it, well, you have the source code.
        public static float GetAmountAdjustment(ESkillId skillId) {
            switch(skillId) {
            /// GENERAL IN-RAID SKILLS ///
                // Yeah I think I'm right on this one.
                case ESkillId.Perception:
                    return 2.0f;
                // How is it this slow?
                case ESkillId.Search:
                    return 2.0f;
                // Used like once or twice a raid, if even.
                case ESkillId.Surgery:
                    return 3.5f;
                // Unless you have a M4A1 that you threw into a pile of mud and duck shit, you won't level this up very quickly.
                case ESkillId.TroubleShooting:
                    return 4.0f;
                // Stress resistance is a fairly often but piddantly rewarded skill.
                case ESkillId.StressResistance:
                    return 4.0f;
                // Earns points quite often but not enough reward.
                case ESkillId.Health:
                    return 1.75f;
                // Doesn't get leveled very often so upgrade the amount.
                case ESkillId.MagDrills:
                    return 2.5f;
                // These get leveled fairly often but not enough points
                case ESkillId.AimDrills:
                case ESkillId.RecoilControl:
                    return 1.75f;
                // I got elite level on strength before I got past level 6...
                case ESkillId.Immunity:
                    return 3.0f;
                // Do I even need a comment for this one?
                case ESkillId.Vitality:
                    return 4.0f;
                // lmao who sneaks in SP-Tarkov?
                case ESkillId.CovertMovement:
                    return 10.0f;
            /// GENERAL HIDEOUT/TRADER SKILLS ///
                case ESkillId.Attention:
                    return 2.5f;
            /// ARMOR ///
                case ESkillId.HeavyVests:
                    return 3.0f;
                case ESkillId.LightVests:
                    return 2.5f;
            /// WEAPONS ///
                // Often used weapon types.
                case ESkillId.Assault:
                case ESkillId.LMG:
                case ESkillId.SMG:
                case ESkillId.HMG:
                case ESkillId.Shotgun:
                case ESkillId.Throwing:
                    return 3.75f;
                // Underutilized weapon types.
                case ESkillId.Sniper:
                case ESkillId.Pistol:
                case ESkillId.Revolver:
                    return 6.5f;
                // fucking MELEE
                case ESkillId.Melee:
                    return 12.5f;
                // Extremely high power but low utilization weapon types.
                case ESkillId.Launcher:
                case ESkillId.AttachedLauncher:
                    return 4.5f;
            /// OTHER ///
                // I have no idea what this is but I have 21 points for it so this is what I'm going with.
                case ESkillId.WeaponTreatment:
                    return 10.0f;
                default:
                    return 1.0f;
            }
        }
        
        protected override MethodBase GetTargetMethod() {
            return typeof(GClass1674).GetMethod("method_2", BindingFlags.NonPublic | BindingFlags.Instance);
        }
        
        [PatchPostfix]
        private static void PatchPostfix(ref GClass1674 __instance, ref float __result) {
            if (!Plugin.config_useAutoSkillAdjustment.Value)
                return;

            __result *= GetAmountAdjustment(__instance.Id);
        }
    }
}