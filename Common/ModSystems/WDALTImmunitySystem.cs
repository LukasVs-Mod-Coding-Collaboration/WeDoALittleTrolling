/*
    WeDoALittleTrolling is a Terraria Mod made with tModLoader.
    Copyright (C) 2022-2025 LukasV-Coding

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
*/

using System.Linq;
using SteelSeries.GameSense;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Common.Configs;
using WeDoALittleTrolling.Common.Utilities;

namespace WeDoALittleTrolling.Common.ModSystems
{
    internal static class WDALTImmunitySystem
    {

        //This class is an advanced patch to block the crab cheese from working against any boss.

        public static readonly int[] BossNPCIDWhitelist =
        {
            NPCID.EaterofWorldsHead,
            NPCID.EaterofWorldsBody,
            NPCID.EaterofWorldsTail,
            NPCID.VileSpitEaterOfWorlds,
            NPCID.AncientCultistSquidhead,
            NPCID.AncientDoom,
            NPCID.AncientLight,
            NPCID.CultistDragonHead,
            NPCID.CultistDragonBody1,
            NPCID.CultistDragonBody2,
            NPCID.CultistDragonBody3,
            NPCID.CultistDragonBody4,
            NPCID.CultistDragonTail,
            NPCID.Probe,
            NPCID.WallofFleshEye,
            NPCID.TheHungry,
            NPCID.TheHungryII,
            NPCID.SkeletronHand,
            NPCID.TheDestroyerBody,
            NPCID.TheDestroyerTail,
            NPCID.PrimeCannon,
            NPCID.PrimeSaw,
            NPCID.PrimeVice,
            NPCID.PrimeLaser,
            NPCID.PlanterasHook,
            NPCID.PlanterasTentacle,
            NPCID.Spore,
            NPCID.GolemHead,
            NPCID.GolemFistLeft,
            NPCID.GolemFistRight,
            NPCID.GolemHeadFree,
            NPCID.DetonatingBubble,
            NPCID.Sharkron,
            NPCID.Sharkron2,
            NPCID.MoonLordFreeEye,
            NPCID.MoonLordLeechBlob,
            NPCID.DD2DarkMageT1,
            NPCID.DD2DarkMageT3,
            NPCID.DD2OgreT2,
            NPCID.DD2OgreT3,
            NPCID.DD2Betsy,
            NPCID.PirateShip,
            NPCID.PirateShipCannon,
            NPCID.MourningWood,
            NPCID.Pumpking,
            NPCID.PumpkingBlade,
            NPCID.Everscream,
            NPCID.IceQueen,
            NPCID.SantaNK1,
            NPCID.MartianSaucer,
            NPCID.MartianSaucerTurret,
            NPCID.MartianSaucerCannon,
            NPCID.MartianSaucerCore,
            NPCID.LunarTowerVortex,
            NPCID.LunarTowerStardust,
            NPCID.LunarTowerNebula,
            NPCID.LunarTowerSolar
        };
        
        public static int GetNewCooldownCounter(PlayerDeathReason reason, int cooldownCounter)
        {
            int newCooldownCounter = cooldownCounter;
            if (reason.SourceNPCIndex >= 0 && reason.SourceNPCIndex < Main.npc.Length)
            {
                if
                (
                    Main.npc[reason.SourceNPCIndex].boss ||
                    BossNPCIDWhitelist.Contains(Main.npc[reason.SourceNPCIndex].type)
                )
                {
                    newCooldownCounter = 1;
                }
            }
            if (reason.SourceProjectileLocalIndex >= 0 && reason.SourceProjectileLocalIndex < Main.projectile.Length)
            {
                if (Main.projectile[reason.SourceProjectileLocalIndex].TryGetGlobalProjectile<WDALTProjectileUtil>(out WDALTProjectileUtil util))
                {
                    if (util.TryGetParentNPC(out NPC npc))
                    {
                        if
                        (
                            npc.boss ||
                            BossNPCIDWhitelist.Contains(npc.type)
                        )
                        {
                            newCooldownCounter = 1;
                        }
                    }
                }
            }
            return newCooldownCounter;
        }
        
        public static void RegisterHooks()
        {
            if (ModContent.GetInstance<WDALTServerConfig>().DisableBossImmunityPatch)
            {
                return;
            }
            On_Player.Hurt_HurtInfo_bool += On_Player_Hurt_HurtInfo_bool;
            On_Player.Hurt_PlayerDeathReason_int_int_bool_bool_bool_int_bool_float += On_Player_Hurt_PlayerDeathReason_int_int_bool_bool_bool_int_bool_float;
            On_Player.Hurt_PlayerDeathReason_int_int_bool_bool_int_bool_float_float_float += On_Player_Hurt_PlayerDeathReason_int_int_bool_bool_int_bool_float_float_float;
            On_Player.Hurt_PlayerDeathReason_int_int_refHurtInfo_bool_bool_int_bool_float_float_float += On_Player_Hurt_PlayerDeathReason_int_int_refHurtInfo_bool_bool_int_bool_float_float_float;
        }

        public static void UnregisterHooks()
        {
            if (ModContent.GetInstance<WDALTServerConfig>().DisableBossImmunityPatch)
            {
                return;
            }
            On_Player.Hurt_HurtInfo_bool -= On_Player_Hurt_HurtInfo_bool;
            On_Player.Hurt_PlayerDeathReason_int_int_bool_bool_bool_int_bool_float -= On_Player_Hurt_PlayerDeathReason_int_int_bool_bool_bool_int_bool_float;
            On_Player.Hurt_PlayerDeathReason_int_int_bool_bool_int_bool_float_float_float -= On_Player_Hurt_PlayerDeathReason_int_int_bool_bool_int_bool_float_float_float;
            On_Player.Hurt_PlayerDeathReason_int_int_refHurtInfo_bool_bool_int_bool_float_float_float -= On_Player_Hurt_PlayerDeathReason_int_int_refHurtInfo_bool_bool_int_bool_float_float_float;
        }

        public static void On_Player_Hurt_HurtInfo_bool(On_Player.orig_Hurt_HurtInfo_bool orig, Player self, Player.HurtInfo info, bool quiet)
        {
            info.CooldownCounter = GetNewCooldownCounter(info.DamageSource, info.CooldownCounter);
            orig.Invoke(self, info, quiet);
        }

        public static double On_Player_Hurt_PlayerDeathReason_int_int_bool_bool_bool_int_bool_float(On_Player.orig_Hurt_PlayerDeathReason_int_int_bool_bool_bool_int_bool_float orig, Player self, PlayerDeathReason damageSource, int Damage, int hitDirection, bool pvp, bool quiet, bool Crit, int cooldownCounter, bool dodgeable, float armorPenetration)
        {
            cooldownCounter = GetNewCooldownCounter(damageSource, cooldownCounter);
            return orig.Invoke(self, damageSource, Damage, hitDirection, pvp, quiet, Crit, cooldownCounter, dodgeable, armorPenetration);
        }

        public static double On_Player_Hurt_PlayerDeathReason_int_int_bool_bool_int_bool_float_float_float(On_Player.orig_Hurt_PlayerDeathReason_int_int_bool_bool_int_bool_float_float_float orig, Player self, PlayerDeathReason damageSource, int Damage, int hitDirection, bool pvp, bool quiet, int cooldownCounter, bool dodgeable, float armorPenetration, float scalingArmorPenetration, float knockback)
        {
            cooldownCounter = GetNewCooldownCounter(damageSource, cooldownCounter);
            return orig.Invoke(self, damageSource, Damage, hitDirection, pvp, quiet, cooldownCounter, dodgeable, armorPenetration, scalingArmorPenetration, knockback);
        }

        public static double On_Player_Hurt_PlayerDeathReason_int_int_refHurtInfo_bool_bool_int_bool_float_float_float(On_Player.orig_Hurt_PlayerDeathReason_int_int_refHurtInfo_bool_bool_int_bool_float_float_float orig, Player self, PlayerDeathReason damageSource, int Damage, int hitDirection, out Player.HurtInfo info, bool pvp, bool quiet, int cooldownCounter, bool dodgeable, float armorPenetration, float scalingArmorPenetration, float knockback)
        {
            cooldownCounter = GetNewCooldownCounter(damageSource, cooldownCounter);
            return orig.Invoke(self, damageSource, Damage, hitDirection, out info, pvp, quiet, cooldownCounter, dodgeable, armorPenetration, scalingArmorPenetration, knockback);
        }
    }
}

