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

using System;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Utilities;
using WeDoALittleTrolling.Common.ModSystems;
using WeDoALittleTrolling.Common.Configs;
//using WeDoALittleTrolling.Content.Buffs;

namespace WeDoALittleTrolling.Content.NPCs
{
    internal class BalancingNPC : GlobalNPC
    {
        public override bool InstancePerEntity => false;
        public static UnifiedRandom random = new UnifiedRandom();
        public static readonly int[] BuffGroup100Percent =
        {
            NPCID.Pinky,
            NPCID.SkeletonSniper
        };
        public static readonly int[] BuffGroup25Percent =
        {
            NPCID.SnowFlinx,
            NPCID.IceBat,
            NPCID.Wolf
        };
        public static readonly int[] NerfGroup25Percent =
        {
            NPCID.Antlion,
            NPCID.WalkingAntlion,
            NPCID.GiantWalkingAntlion,
            NPCID.TombCrawlerHead,
            NPCID.JungleCreeper,
            NPCID.JungleCreeperWall,
            NPCID.BlackRecluse,
            NPCID.BlackRecluseWall,
            NPCID.Wraith,
            NPCID.SkeletonCommando,
            NPCID.TacticalSkeleton,
            NPCID.Eyezor,
            NPCID.Nailhead
        };
        public static readonly int[] NerfGroup35Percent =
        {
            NPCID.GiantTortoise,
            NPCID.GiantMossHornet,
            NPCID.BigMossHornet,
            NPCID.MossHornet,
            NPCID.LittleMossHornet,
            NPCID.TinyMossHornet,
            NPCID.PossessedArmor,
            NPCID.FireImp,
            NPCID.Hellbat,
            NPCID.IchorSticker
        };
        public static readonly int[] NerfGroup50Percent =
        {
        };
        public static readonly int[] KnockbackResistanceGroup =
        {
            NPCID.AngryTrapper,
            NPCID.Mothron,
            NPCID.PossessedArmor,
            NPCID.RockGolem,
            NPCID.Herpling
        };
        public static readonly int[] InflictVenomDebuff1In1Group =
        {
            NPCID.AngryTrapper,
            NPCID.Moth
        };
        public static readonly int[] InflictPoisonDebuff1In1Group =
        {
            NPCID.Snatcher,
            NPCID.ManEater
        };
        public static readonly int[] InflictBleedingDebuff1In1Group =
        {
            NPCID.Shark,
            NPCID.SandShark,
            NPCID.PrimeSaw
        };
        public static readonly int[] InflictBleedingDebuff1In8Group =
        {
            NPCID.Herpling,
            NPCID.Wolf,
            NPCID.PirateCorsair,
            NPCID.PirateGhost
        };
        public static readonly int[] InflictBrokenArmor1In1Group =
        {
            NPCID.PrimeSaw
        };
        public static readonly int[] InflictSlowness1In1Group =
        {
            NPCID.PrimeSaw
        };
        public static readonly int[] InflictCursed1In1Group =
        {
            NPCID.PrimeVice
        };
        /*public static readonly int[] InflictVulnerable1In1Group =
        {
            NPCID.SkeletronHand,
            NPCID.QueenSlimeBoss,
            NPCID.DukeFishron,
            NPCID.Plantera,
            NPCID.PlanterasHook,
            NPCID.PlanterasTentacle,
            NPCID.Spore,
            NPCID.HallowBoss,
            NPCID.CultistBoss,
            NPCID.DD2OgreT2,
            NPCID.DD2OgreT3,
            NPCID.DD2Betsy,
            NPCID.MourningWood,
            NPCID.Pumpking,
            NPCID.PumpkingBlade,
            NPCID.Everscream,
            NPCID.SantaNK1,
            NPCID.IceQueen,
            NPCID.MartianSaucer,
            NPCID.MartianSaucerCannon,
            NPCID.MartianSaucerCore,
            NPCID.MartianSaucerTurret,
            NPCID.Mimic,
            NPCID.IceMimic,
            NPCID.PresentMimic,
            NPCID.BigMimicCorruption,
            NPCID.BigMimicCrimson,
            NPCID.BigMimicHallow,
            NPCID.BigMimicJungle,
            NPCID.BloodNautilus,
            NPCID.BloodEelHead,
            NPCID.BloodEelBody,
            NPCID.BloodEelTail,
            NPCID.GoblinShark,
            NPCID.IceGolem,
            NPCID.SandElemental,
            NPCID.DetonatingBubble,
            NPCID.Golem,
            NPCID.GolemHead,
            NPCID.GolemHeadFree
        };
        public static readonly int[] InflictWreckedResistance1In1Group =
        {
            NPCID.SkeletonSniper,
            NPCID.SkeletonCommando,
            NPCID.Clown,
            NPCID.Plantera,
            NPCID.Poltergeist,
            NPCID.GolemFistLeft,
            NPCID.GolemFistRight
        };
        public static readonly int[] InflictDevastated1In1Group =
        {
            NPCID.CultistDragonHead,
            NPCID.AncientDoom,
            NPCID.TheDestroyer,
            NPCID.Mothron,
            NPCID.WyvernHead,
            NPCID.SolarCrawltipedeHead,
            NPCID.StardustJellyfishSmall
        };*/

        public static readonly int[] ScarecrowGroup =
        {
            NPCID.Scarecrow1,
            NPCID.Scarecrow2,
            NPCID.Scarecrow3,
            NPCID.Scarecrow4,
            NPCID.Scarecrow5,
            NPCID.Scarecrow6,
            NPCID.Scarecrow7,
            NPCID.Scarecrow8,
            NPCID.Scarecrow9,
            NPCID.Scarecrow10
        };

        public override void SetDefaults(NPC npc)
        {
            if (WDALTModSystem.isCalamityModPresent && !ModContent.GetInstance<WDALTServerConfig>().DisableCalamityCompatibilityMode)
            {
                return;
            }
            if (!ModContent.GetInstance<WDALTServerConfig>().DisableRebalancing)
            {
                if (KnockbackResistanceGroup.Contains(npc.type))
                {
                    npc.knockBackResist = 0f;
                }
                if
                (
                    npc.type == NPCID.Derpling
                )
                {
                    npc.knockBackResist = 0.75f;
                }
                if
                (
                    npc.type == NPCID.Snatcher ||
                    npc.type == NPCID.ManEater ||
                    npc.type == NPCID.AngryTrapper
                )
                {
                    npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.25);
                }
                if
                (
                    npc.type == NPCID.Mothron ||
                    npc.type == NPCID.MothronEgg ||
                    npc.type == NPCID.MothronSpawn
                )
                {
                    npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.5);
                    npc.damage = (int)Math.Round(npc.damage * 1.25);
                }

                //Boss buffs

                if (npc.type == NPCID.EyeofCthulhu)
                {
                    npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.25);
                    if (Main.getGoodWorld)
                    {
                        npc.damage = (int)Math.Round(npc.damage * Math.Sqrt(1.5));
                    }
                    else
                    {
                        npc.damage = (int)Math.Round(npc.damage * 1.5);
                    }
                }
                if (npc.type == NPCID.KingSlime)
                {
                    npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.25);
                    if (Main.getGoodWorld)
                    {
                        npc.damage = (int)Math.Round(npc.damage * Math.Sqrt(1.25));
                    }
                    else
                    {
                        npc.damage = (int)Math.Round(npc.damage * 1.25);
                    }
                }
                if (npc.type == NPCID.BrainofCthulhu)
                {
                    if (Main.getGoodWorld)
                    {
                        npc.defense -= 10;
                        npc.knockBackResist = 1.25f;
                    }
                    else
                    {
                        npc.knockBackResist = 0f;
                    }
                }
                if (npc.type == NPCID.Creeper)
                {
                    if (Main.getGoodWorld)
                    {
                        npc.knockBackResist = 2.5f;
                    }
                }
                if (npc.type == NPCID.PrimeVice)
                {
                    npc.defense += 14;
                    npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.35);
                }
                if (npc.type == NPCID.PrimeSaw)
                {
                    npc.damage += 34;
                }
                if
                (
                    npc.type == NPCID.Plantera
                )
                {
                    npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.5);
                }
                if
                (
                    npc.type == NPCID.Golem ||
                    npc.type == NPCID.GolemHead ||
                    npc.type == NPCID.GolemFistLeft ||
                    npc.type == NPCID.GolemFistRight ||
                    npc.type == NPCID.GolemHeadFree
                )
                {
                    npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.25);
                }
                if (npc.type == NPCID.HallowBoss)
                {
                    npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.125);
                }
                if (npc.type == NPCID.CultistBoss)
                {
                    npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.125);
                }
                if
                (
                    npc.type == NPCID.MoonLordCore ||
                    npc.type == NPCID.MoonLordHead ||
                    npc.type == NPCID.MoonLordHand ||
                    npc.type == NPCID.MoonLordFreeEye ||
                    npc.type == NPCID.MoonLordLeechBlob
                )
                {
                    npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.25);
                }
                if (npc.type == NPCID.VileSpitEaterOfWorlds)
                {
                    npc.dontTakeDamage = true;
                }
                if
                (
                    npc.type == NPCID.Pumpking ||
                    npc.type == NPCID.PumpkingBlade
                )
                {
                    npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.25);
                    npc.damage = (int)Math.Round(npc.damage * 1.25);
                }
                if
                (
                    npc.type == NPCID.IceQueen
                )
                {
                    npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.25);
                }
                if
                (
                    npc.type == NPCID.MartianSaucer ||
                    npc.type == NPCID.MartianSaucerCannon ||
                    npc.type == NPCID.MartianSaucerCore ||
                    npc.type == NPCID.MartianSaucerTurret
                )
                {
                    npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.25);
                }
                if (WDALTModSystem.isThoriumModPresent && WDALTModSystem.MCIDIntegrity)
                {
                    //Buff Thorium Bosses Accordingly
                    if (npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_VC))
                    {
                        npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.35);
                    }
                    if (npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_SCS))
                    {
                        npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.25);
                        npc.damage = (int)Math.Round(npc.damage * 1.125);
                    }
                    if (npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_LI_V1) || npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_LI_V2))
                    {
                        npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.5);
                    }
                    if
                    (
                        npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_OLD) ||
                        npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_SFF) ||
                        npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_DE)
                    )
                    {
                        npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.25);
                    }
                    if
                    (
                        npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_AET)
                    )
                    {
                        npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.5);
                    }
                }
            }
            base.SetDefaults(npc);
        }

        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            //Decreasing damage during SetDefaults() is unsafe, do it in OnSpawn() instead.
            if (WDALTModSystem.isCalamityModPresent && !ModContent.GetInstance<WDALTServerConfig>().DisableCalamityCompatibilityMode)
            {
                return;
            }
            if (!ModContent.GetInstance<WDALTServerConfig>().DisableRebalancing)
            {
                if (BuffGroup100Percent.Contains(npc.type))
                {
                    npc.damage = (int)Math.Round(npc.damage * 2.0);
                    npc.netUpdate = true;
                }
                if (BuffGroup25Percent.Contains(npc.type))
                {
                    npc.damage = (int)Math.Round(npc.damage * 1.25);
                    npc.netUpdate = true;
                }
                if (NerfGroup25Percent.Contains(npc.type))
                {
                    npc.damage = (int)Math.Round(npc.damage * 0.75);
                    npc.netUpdate = true;
                }
                if (NerfGroup35Percent.Contains(npc.type))
                {
                    npc.damage = (int)Math.Round(npc.damage * 0.65);
                    npc.netUpdate = true;
                }
                if (NerfGroup50Percent.Contains(npc.type))
                {
                    npc.damage = (int)Math.Round(npc.damage * 0.5);
                    npc.netUpdate = true;
                }
            }
            base.OnSpawn(npc, source);
        }

        public static void OnSpawnProjectile(NPC npc, Projectile projectile)
        {
            if (WDALTModSystem.isCalamityModPresent && !ModContent.GetInstance<WDALTServerConfig>().DisableCalamityCompatibilityMode)
            {
                return;
            }
            if (!ModContent.GetInstance<WDALTServerConfig>().DisableRebalancing)
            {
                if (BuffGroup100Percent.Contains(npc.type))
                {
                    projectile.damage = (int)Math.Round(projectile.damage * 2.0);
                    projectile.netUpdate = true;
                }
                if (BuffGroup25Percent.Contains(npc.type))
                {
                    projectile.damage = (int)Math.Round(projectile.damage * 1.25);
                    projectile.netUpdate = true;
                }
                if (NerfGroup25Percent.Contains(npc.type))
                {
                    projectile.damage = (int)Math.Round(projectile.damage * 0.75);
                    projectile.netUpdate = true;
                }
                if (NerfGroup35Percent.Contains(npc.type))
                {
                    projectile.damage = (int)Math.Round(projectile.damage * 0.65);
                    projectile.netUpdate = true;
                }
                if (NerfGroup50Percent.Contains(npc.type))
                {
                    projectile.damage = (int)Math.Round(projectile.damage * 0.5);
                    projectile.netUpdate = true;
                }
                if (WDALTModSystem.isThoriumModPresent && WDALTModSystem.MCIDIntegrity)
                {
                    //Buff Thorium Bosses Accordingly
                    if
                    (
                        npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_SCS)
                    )
                    {
                        projectile.damage = (int)Math.Round(projectile.damage * 1.125);
                        projectile.netUpdate = true;
                    }
                }
            }
        }

        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if (!ModContent.GetInstance<WDALTServerConfig>().DisableRebalancing)
            {
                if
                (
                    npc.type == NPCID.PossessedArmor || //Possessed Armors, Lihzards, Flying Snakes and Rock Golems have 50% DR%
                    npc.type == NPCID.Lihzahrd ||
                    npc.type == NPCID.LihzahrdCrawler ||
                    npc.type == NPCID.FlyingSnake
                )
                {
                    modifiers.SourceDamage *= 0.75f;
                }
                if (npc.type == NPCID.Gnome) //I'm gnot a gnelf. I'm gnot a gnoblin. I'm a GNOME! Any you've been GNOOOOOOOMED! Gnomes have 90% DR%
                {
                    modifiers.SourceDamage *= 0.5f;
                }
                if (WDALTModSystem.isThoriumModPresent && WDALTModSystem.MCIDIntegrity)
                {
                    if
                    (
                        npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_AET) ||
                        npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_DE)
                    )
                    {
                        modifiers.FinalDamage *= 0.75f;
                    }
                }
            }
            base.ModifyIncomingHit(npc, ref modifiers);
        }

        public override void ResetEffects(NPC npc)
        {
            base.ResetEffects(npc);
        }

        public override void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
        {
            if (!ModContent.GetInstance<WDALTServerConfig>().DisableRebalancing)
            {
                ApplyDebuffsToPlayerBasedOnNPC(npc.type, target);
            }
        }

        public static void ApplyDebuffsToPlayerBasedOnNPC(int npcType, Player target)
        {
            if (InflictVenomDebuff1In1Group.Contains(npcType))
            {
                if (random.Next(0, 1) == 0) //1 in 1 Chance
                {
                    target.AddBuff(BuffID.Venom, 240, true); //4s, X2 in Expert, X2.5 in Master
                }
            }
            if (InflictPoisonDebuff1In1Group.Contains(npcType))
            {
                if (random.Next(0, 1) == 0) //1 in 1 Chance
                {
                    target.AddBuff(BuffID.Poisoned, 240, true); //4s, X2 in Expert, X2.5 in Master
                }
            }
            if (InflictBleedingDebuff1In1Group.Contains(npcType))
            {
                if (random.Next(0, 1) == 0) //1 in 1 Chance
                {
                    target.AddBuff(BuffID.Bleeding, 960, true); //16s, X2 in Expert, X2.5 in Master
                }
            }
            if (InflictBleedingDebuff1In8Group.Contains(npcType))
            {
                if (random.Next(0, 8) == 0) //1 in 8 Chance
                {
                    target.AddBuff(BuffID.Bleeding, 480, true); //8s, X2 in Expert, X2.5 in Master
                }
            }
            if (InflictBrokenArmor1In1Group.Contains(npcType))
            {
                if (random.Next(0, 1) == 0)
                {
                    target.AddBuff(BuffID.BrokenArmor, 960, true); //16s, X2 in Expert, X2.5 in Master
                }
            }
            if (InflictSlowness1In1Group.Contains(npcType))
            {
                if (random.Next(0, 1) == 0)
                {
                    target.AddBuff(BuffID.Slow, 960, true); //16s, X2 in Expert, X2.5 in Master
                }
            }
            if (InflictCursed1In1Group.Contains(npcType))
            {
                if (random.Next(0, 1) == 0)
                {
                    target.AddBuff(BuffID.Cursed, 240, true); //4s, X2 in Expert, X2.5 in Master
                }
            }
            /*if (InflictWreckedResistance1In1Group.Contains(npcType))
            {
                if (random.Next(0, 1) == 0 && Main.masterMode)
                {
                    target.AddBuff(ModContent.BuffType<WreckedResistance>(), 3600, true); //1m, X2 in Expert, X2.5 in Master
                }
            }
            if (InflictVulnerable1In1Group.Contains(npcType))
            {
                if (random.Next(0, 1) == 0 && Main.masterMode)
                {
                    target.AddBuff(ModContent.BuffType<Vulnerable>(), 3600, true); //1m, X2 in Expert, X2.5 in Master
                }
            }
            if (InflictDevastated1In1Group.Contains(npcType))
            {
                if (random.Next(0, 1) == 0 && Main.masterMode)
                {
                    target.AddBuff(ModContent.BuffType<Devastated>(), 3600, true); //1m, X2 in Expert, X2.5 in Master
                    Devastated.DevastatePlayer(target);
                }
            }*/
            if (npcType == NPCID.PossessedArmor)
            {
                target.AddBuff(BuffID.Blackout, 900, true); //15s, X2 in Expert, X2.5 in Master
            }
            /*if (WDALTModSystem.isThoriumModPresent && WDALTModSystem.MCIDIntegrity)
            {
                //Buff Thorium Bosses Accordingly
                if (WDALTModContentID.GetThoriumBossInflictVulnerable1in1Group().Contains(npcType) && Main.masterMode)
                {
                    target.AddBuff(ModContent.BuffType<Vulnerable>(), 3600, true); //1m, X2 in Expert, X2.5 in Master
                }
                if (WDALTModContentID.GetThoriumBossInflictDevastated1in1Group().Contains(npcType) && Main.masterMode)
                {
                    target.AddBuff(ModContent.BuffType<Devastated>(), 3600, true); //1m, X2 in Expert, X2.5 in Master
                    Devastated.DevastatePlayer(target);
                }
            }
            if (WDALTModSystem.isSpiritModPresent && WDALTModSystem.MCIDIntegrity)
            {
                //Buff Spirit Bosses Accordingly
                if (WDALTModContentID.GetSpiritBossInflictVulnerable1in1Group().Contains(npcType) && Main.masterMode)
                {
                    target.AddBuff(ModContent.BuffType<Vulnerable>(), 3600, true); //1m, X2 in Expert, X2.5 in Master
                }
                if (WDALTModContentID.GetSpiritBossInflictWreckedResistance1in1Group().Contains(npcType) && Main.masterMode)
                {
                    target.AddBuff(ModContent.BuffType<WreckedResistance>(), 3600, true); //1m, X2 in Expert, X2.5 in Master
                }
            }
            if (WDALTModSystem.isSpookyModPresent && WDALTModSystem.MCIDIntegrity)
            {
                //Buff Spooky Bosses Accordingly
                if (WDALTModContentID.GetSpookyBossInflictVulnerable1in1Group().Contains(npcType) && Main.masterMode)
                {
                    target.AddBuff(ModContent.BuffType<Vulnerable>(), 3600, true); //1m, X2 in Expert, X2.5 in Master
                }
                if (WDALTModContentID.GetSpookyBossInflictWreckedResistance1in1Group().Contains(npcType) && Main.masterMode)
                {
                    target.AddBuff(ModContent.BuffType<WreckedResistance>(), 3600, true); //1m, X2 in Expert, X2.5 in Master
                }
            }*/
        }
    }
}
