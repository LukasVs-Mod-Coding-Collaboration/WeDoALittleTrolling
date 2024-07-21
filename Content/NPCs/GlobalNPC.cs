/*
    WeDoALittleTrolling is a Terraria Mod made with tModLoader.
    Copyright (C) 2022-2024 LukasV-Coding

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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Common.Utilities;
using WeDoALittleTrolling.Common.ModPlayers;
using WeDoALittleTrolling.Content.Buffs;
using WeDoALittleTrolling.Content.Prefixes;
using WeDoALittleTrolling.Content.Items;
using WeDoALittleTrolling.Content.Items.Armor;
using WeDoALittleTrolling.Content.Items.Material;
using WeDoALittleTrolling.Content.Items.Accessories;
using Microsoft.Xna.Framework;
using Terraria.Utilities;
using WeDoALittleTrolling.Common.ModSystems;
using WeDoALittleTrolling.Content.Projectiles;
using WeDoALittleTrolling.Content.Items.Weapons;
using WeDoALittleTrolling.Content.Items.ProgressionCrystals;

namespace WeDoALittleTrolling.Content.NPCs
{
    internal class GlobalNPCs : GlobalNPC
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
            NPCID.Lihzahrd,
            NPCID.LihzahrdCrawler,
            NPCID.PossessedArmor,
            NPCID.RockGolem,
            NPCID.Wolf
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
        /*
        public static readonly int[] InflictSearingInferno1In1Group =
        {
            NPCID.Golem,
            NPCID.GolemHead,
            NPCID.GolemFistLeft,
            NPCID.GolemFistRight,
            NPCID.GolemHeadFree,
            NPCID.Lihzahrd,
            NPCID.LihzahrdCrawler,
            NPCID.FlyingSnake
        };
        */
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
        public static readonly int[] InflictVulnerable1In1Group =
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
        };

        public static readonly int[] ResistGloriousDemise50PercentGroup =
        {
            NPCID.TheDestroyer,
            NPCID.TheDestroyerBody,
            NPCID.TheDestroyerTail,
            NPCID.EaterofWorldsHead,
            NPCID.EaterofWorldsBody,
            NPCID.EaterofWorldsTail
        };

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
            if (npc.type == NPCID.Bee || npc.type == NPCID.BeeSmall)
            {
                Main.npcCatchable[npc.type] = true;
                npc.catchItem = ModContent.ItemType<Consumablebee>();
                //revenge. REVENGE!!
            }
            if (npc.type == NPCID.BoundTownSlimeOld)
            {
                Main.npcCatchable[npc.type] = true;
                npc.catchItem = ItemID.Gel;
            }
            if (WDALTModSystem.isCalamityModPresent)
            {
                base.SetDefaults(npc);
                return;
            }
            if (KnockbackResistanceGroup.Contains(npc.type))
            {
                npc.knockBackResist = 0f;
            }
            if
            (
                npc.type == NPCID.Lihzahrd ||
                npc.type == NPCID.LihzahrdCrawler ||
                npc.type == NPCID.FlyingSnake
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 2.0);
            }
            if
            (
                npc.type == NPCID.Snatcher ||
                npc.type == NPCID.ManEater ||
                npc.type == NPCID.AngryTrapper
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 2.0);
            }
            if
            (
                npc.type == NPCID.PossessedArmor
            )
            {
                npc.lifeMax *= 2;
                if (WDALTSeedSystem.TestyWorld)
                {
                    npc.lifeMax *= 4;
                }
            }
            if
            (
                npc.type == NPCID.Eyezor ||
                npc.type == NPCID.Frankenstein ||
                npc.type == NPCID.SwampThing ||
                npc.type == NPCID.VampireBat ||
                npc.type == NPCID.Vampire ||
                npc.type == NPCID.CreatureFromTheDeep ||
                npc.type == NPCID.Fritz ||
                npc.type == NPCID.ThePossessed ||
                npc.type == NPCID.Reaper ||
                npc.type == NPCID.Butcher ||
                npc.type == NPCID.DeadlySphere ||
                npc.type == NPCID.DrManFly ||
                npc.type == NPCID.Nailhead ||
                npc.type == NPCID.Psycho
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.5);
            }
            if
            (
                npc.type == NPCID.Mothron ||
                npc.type == NPCID.MothronEgg ||
                npc.type == NPCID.MothronSpawn
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 2.0);
                npc.damage = (int)Math.Round(npc.damage * 1.5);
            }

            //Boss buffs

            if (npc.type == NPCID.EyeofCthulhu)
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 2.0);
                if (Main.getGoodWorld)
                {
                    npc.damage = (int)Math.Round(npc.damage * Math.Sqrt(2.0));
                }
                else
                {
                    npc.damage = (int)Math.Round(npc.damage * 2.0);
                }
            }
            if (npc.type == NPCID.KingSlime)
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 2.0);
                if (Main.getGoodWorld)
                {
                    npc.damage = (int)Math.Round(npc.damage * Math.Sqrt(1.5));
                }
                else
                {
                    npc.damage = (int)Math.Round(npc.damage * 1.5);
                }
            }
            if (npc.type == NPCID.SkeletronHead)
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.5);
            }
            if (npc.type == NPCID.SkeletronHand)
            {
                npc.lifeMax *= 2;
                if (Main.getGoodWorld)
                {
                    npc.damage = (int)Math.Round(npc.damage * 0.5);
                }
            }
            if (npc.type == NPCID.BrainofCthulhu)
            {
                npc.lifeMax *= 2;
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
                    npc.lifeMax = (int)Math.Round(npc.lifeMax * Math.Sqrt(2.0));
                }
                else
                {
                    npc.lifeMax = (int)Math.Round(npc.lifeMax * 2.0);
                }
            }
            if (npc.type == NPCID.QueenBee)
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.5);
                if (Main.getGoodWorld)
                {
                    npc.damage = (int)Math.Round(npc.damage * Math.Sqrt(1.5));
                }
                else
                {
                    npc.damage = (int)Math.Round(npc.damage * 1.5);
                }
            }
            if
            (
                npc.type == NPCID.WallofFlesh ||
                npc.type == NPCID.WallofFleshEye ||
                npc.type == NPCID.TheHungry ||
                npc.type == NPCID.TheHungryII
            )
            {
                if (Main.getGoodWorld)
                {
                    npc.lifeMax = (int)Math.Round(npc.lifeMax * Math.Sqrt(1.75));
                }
                else
                {
                    npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.75);
                }
            }
            if
            (
                npc.type == NPCID.FireImp
            )
            {
                if (Main.getGoodWorld)
                {
                    npc.defense -= 8;
                }
            }
            if
            (
                npc.type == NPCID.Plantera
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 2.25);
            }
            if
            (
                npc.type == NPCID.PlanterasTentacle
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.75);
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
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 2.0);
            }
            if (npc.type == NPCID.HallowBoss)
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.75);
            }
            if (npc.type == NPCID.DukeFishron)
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.75);
                npc.damage = (int)Math.Round(npc.damage * 1.25);
            }
            if (npc.type == NPCID.CultistBoss)
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 2.0);
                npc.damage = (int)Math.Round(npc.damage * 1.75);
            }
            if
            (
                npc.type == NPCID.CultistDragonHead ||
                npc.type == NPCID.CultistDragonBody1 ||
                npc.type == NPCID.CultistDragonBody2 ||
                npc.type == NPCID.CultistDragonBody3 ||
                npc.type == NPCID.CultistDragonBody4 ||
                npc.type == NPCID.CultistDragonTail ||
                npc.type == NPCID.AncientCultistSquidhead ||
                npc.type == NPCID.AncientLight ||
                npc.type == NPCID.AncientDoom
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 2.0);
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
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 2.0);
            }
            if
            (
                npc.type == NPCID.SkeletronPrime ||
                npc.type == NPCID.PrimeCannon ||
                npc.type == NPCID.PrimeLaser ||
                npc.type == NPCID.PrimeSaw ||
                npc.type == NPCID.PrimeVice
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.5);
            }
            if
            (
                npc.type == NPCID.Retinazer ||
                npc.type == NPCID.Spazmatism
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.5);
            }
            if
            (
                npc.type == NPCID.TheDestroyer ||
                npc.type == NPCID.TheDestroyerBody ||
                npc.type == NPCID.TheDestroyerTail
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.75);
            }
            if
            (
                npc.type == NPCID.QueenSlimeBoss
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 2.0);
            }
            if
            (
                npc.type == NPCID.EaterofWorldsHead ||
                npc.type == NPCID.EaterofWorldsBody ||
                npc.type == NPCID.EaterofWorldsTail
            )
            {
                npc.lifeMax *= 2;
            }
            if (npc.type == NPCID.VileSpitEaterOfWorlds)
            {
                npc.dontTakeDamage = true;
            }
            if
            (
                npc.type == NPCID.DD2DarkMageT1 ||
                npc.type == NPCID.DD2DarkMageT3
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.75);
            }
            if
            (
                npc.type == NPCID.DD2OgreT2 ||
                npc.type == NPCID.DD2OgreT3
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.5);
            }
            if
            (
                npc.type == NPCID.DD2Betsy
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.75);
            }
            if
            (
                npc.type == NPCID.PirateShip ||
                npc.type == NPCID.PirateShipCannon
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.5);
            }
            if
            (
                npc.type == NPCID.MourningWood ||
                npc.type == NPCID.Splinterling ||
                npc.type == NPCID.Hellhound ||
                npc.type == NPCID.Poltergeist ||
                npc.type == NPCID.HeadlessHorseman ||
                ScarecrowGroup.Contains(npc.type)
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.5);
            }
            if
            (
                npc.type == NPCID.Pumpking ||
                npc.type == NPCID.PumpkingBlade
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.75);
                npc.damage = (int)Math.Round(npc.damage * 1.5);
            }
            if
            (
                npc.type == NPCID.Everscream
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.25);
            }
            if
            (
                npc.type == NPCID.SantaNK1
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.5);
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
                npc.type == NPCID.MartianSaucerTurret ||
                npc.type == NPCID.Scutlix ||
                npc.type == NPCID.ScutlixRider ||
                npc.type == NPCID.MartianWalker ||
                npc.type == NPCID.MartianDrone ||
                npc.type == NPCID.MartianTurret ||
                npc.type == NPCID.GigaZapper ||
                npc.type == NPCID.MartianEngineer ||
                npc.type == NPCID.MartianOfficer ||
                npc.type == NPCID.ForceBubble ||
                npc.type == NPCID.RayGunner ||
                npc.type == NPCID.GrayGrunt ||
                npc.type == NPCID.BrainScrambler
            )
            {
                npc.lifeMax = (int)Math.Round(npc.lifeMax * 2.0);
            }
            if (WDALTModSystem.isThoriumModPresent && WDALTModSystem.MCIDIntegrity)
            {
                //Buff Thorium Bosses Accordingly
                if (npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_GTB))
                {
                    npc.lifeMax *= 2;
                }
                if (npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_QJ))
                {
                    npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.75);
                }
                if (npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_VC))
                {
                    npc.lifeMax *= 2;
                }
                if (npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_GES))
                {
                    npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.75);
                }
                if (npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_BC))
                {
                    npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.5);
                }
                if (npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_SCS))
                {
                    npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.5);
                    npc.damage = (int)Math.Round(npc.damage * 1.5);
                }
                if (npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_BS_V1) || npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_BS_V2))
                {
                    npc.lifeMax = (int)Math.Round(npc.lifeMax * 2.0);
                }
                if (npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_FB_V1) || npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_FB_V2))
                {
                    npc.lifeMax = (int)Math.Round(npc.lifeMax * 2.0);
                }
                if (npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_LI_V1) || npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_LI_V2))
                {
                    npc.lifeMax = (int)Math.Round(npc.lifeMax * 2.25);
                }
                if
                (
                    npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_FO_V1) ||
                    npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_FO_V2) ||
                    npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_FO_V3)
                )
                {
                    npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.75);
                }
                if
                (
                    npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_OLD) ||
                    npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_SFF) ||
                    npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_DE)
                )
                {
                    npc.lifeMax *= 2;
                }
                if
                (
                    npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_AET)
                )
                {
                    npc.lifeMax *= 3;
                }
            }
            base.SetDefaults(npc);
        }

        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            if (npc.type == NPCID.VileSpitEaterOfWorlds)
            {
                npc.GetGlobalNPC<WDALTNPCUtil>().VileSpitTimeLeft = 300;
                npc.netUpdate = true;
            }
            //Decreasing damage during SetDefaults() is unsafe, do it in OnSpawn() instead.
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
            base.OnSpawn(npc, source);
        }

        public static void OnSpawnProjectile(NPC npc, Projectile projectile)
        {
            if (npc.HasBuff(ModContent.BuffType<SearingInferno>()))
            {
                projectile.damage = (int)Math.Round(projectile.damage * (1.0f - SearingInferno.damageNerfMultiplier));
                projectile.netUpdate = true;
            }
            if (WDALTModSystem.isCalamityModPresent)
            {
                return;
            }
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
            if (npc.type == NPCID.HallowBoss)
            {
                projectile.damage = (int)Math.Round(projectile.damage * 1.75);
            }
            if (npc.type == NPCID.CultistBoss && projectile.type != ProjectileID.CultistBossIceMist && projectile.type != ProjectileID.CultistBossLightningOrb)
            {
                projectile.damage = (int)Math.Round(projectile.damage * 1.75);
                projectile.netUpdate = true;
            }
            if (npc.type == NPCID.QueenBee)
            {
                projectile.damage = (int)Math.Round(projectile.damage * 1.5);
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
                    projectile.damage = (int)Math.Round(projectile.damage * 1.5);
                    projectile.netUpdate = true;
                }
            }
        }

        public override void ModifyShop(NPCShop shop)
        {
            if (shop.NpcType == NPCID.Stylist || shop.NpcType == NPCID.Pirate)
            {
                Condition LPCT1 = new Condition("At least 5 finished fishing quests", WDALTConditionFunctions.HasTier1FishingQuests);
                Condition LPCT2 = new Condition("At least 10 finished fishing quests", WDALTConditionFunctions.HasTier2FishingQuests);
                Condition LPCT3 = new Condition("At least 15 finished fishing quests", WDALTConditionFunctions.HasTier3FishingQuests);
                Condition DC = new Condition("Defeated Duke Fishron", WDALTConditionFunctions.DukeCrystal);
                NPCShop.Entry[] entry = new NPCShop.Entry[4];
                entry[0] = new NPCShop.Entry(ModContent.ItemType<LightPurpleCrystalTier1>(), LPCT1);
                entry[1] = new NPCShop.Entry(ModContent.ItemType<LightPurpleCrystalTier2>(), LPCT2);
                entry[2] = new NPCShop.Entry(ModContent.ItemType<LightPurpleCrystalTier3>(), LPCT3);
                entry[3] = new NPCShop.Entry(ItemID.FishronWings, DC);
                shop.Add(entry);
            }
            base.ModifyShop(shop);
        }

        public override bool? CanCollideWithPlayerMeleeAttack(NPC npc, Player player, Item item, Rectangle meleeAttackHitbox)
        {
            if (npc.type == NPCID.VileSpitEaterOfWorlds)
            {
                return false;
            }
            return base.CanCollideWithPlayerMeleeAttack(npc, player, item, meleeAttackHitbox);
        }

        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if
            (
                npc.type == NPCID.PossessedArmor || //Possessed Armors, Lihzards, Flying Snakes and Rock Golems have 50% DR%
                npc.type == NPCID.Lihzahrd ||
                npc.type == NPCID.LihzahrdCrawler ||
                npc.type == NPCID.FlyingSnake
            )
            {
                modifiers.SourceDamage *= 0.5f;
            }
            if (npc.type == NPCID.Gnome)
            {
                modifiers.SourceDamage *= 0.25f;
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
            base.ModifyIncomingHit(npc, ref modifiers);
        }

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if (projectile.type == ModContent.ProjectileType<GloriousDemiseProjectile>() && ResistGloriousDemise50PercentGroup.Contains(npc.type))
            {
                modifiers.FinalDamage *= 0.5f;
            }
            if (WDALTModSystem.isThoriumModPresent && WDALTModSystem.MCIDIntegrity)
            {
                if
                (
                    (
                        npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_SFF) ||
                        npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_AET) ||
                        npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_OLD) ||
                        npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_DE)
                    ) &&
                    (
                        projectile.type == ModContent.ProjectileType<PhantomStaffProjectile>() ||
                        projectile.type == ModContent.ProjectileType<PhantomStaffProjectileBullet>()
                    )
                )
                {
                    modifiers.FinalDamage *= 0.75f; //The Primordials only take 75% damage from the Luminte Phantom Staff.
                }
            }
            base.ModifyHitByProjectile(npc, projectile, ref modifiers);
        }

        public override bool PreAI(NPC npc)
        {
            if (npc.type == NPCID.VileSpitEaterOfWorlds)
            {
                if (npc.GetGlobalNPC<WDALTNPCUtil>().VileSpitTimeLeft <= 0)
                {
                    npc.active = false;
                }
                if (Collision.SolidCollision(npc.position, npc.width + (int)npc.velocity.Length() + 2, npc.height + (int)npc.velocity.Length() + 2) && npc.velocity.Length() > 0f)
                {
                    npc.ai[0] += 1f;
                    if (npc.ai[0] > 3f)
                    {
                        npc.ai[0] = 3f;
                    }
                    npc.position += npc.netOffset;
                    npc.rotation += 0.4f * (float)npc.direction;
                    npc.position -= npc.netOffset;
                    return false;
                }
            }
            if (npc.type == NPCID.CultistBoss)
            {
                WDALTBossAIUtil.BossAI_LunaticCultistExtras(npc);
            }
            if (npc.type == NPCID.Golem)
            {
                WDALTBossAIUtil.BossAI_GolemExtras(npc);
            }
            return base.PreAI(npc);
        }

        public override void AI(NPC npc)
        {
            if (npc.type == NPCID.VileSpitEaterOfWorlds)
            {
                npc.GetGlobalNPC<WDALTNPCUtil>().VileSpitTimeLeft--;
            }
            if
            (
                npc.type == NPCID.EaterofWorldsHead ||
                npc.type == NPCID.EaterofWorldsBody ||
                npc.type == NPCID.EaterofWorldsTail
            )
            {
                WDALTBossAIUtil.BossAI_EaterofWorldsExtras(npc, ref random);
            }
            if (npc.type == NPCID.TheDestroyerBody)
            {
                WDALTBossAIUtil.BossAI_TheDestroyerExtras(npc, ref random);
            }
            if (npc.type == NPCID.SkeletronPrime)
            {
                WDALTBossAIUtil.BossAI_SkeletronPrimeExtras(npc);
            }
            if (npc.type == NPCID.Plantera)
            {
                WDALTBossAIUtil.BossAI_PlanteraShotgun(npc, ref random);
            }
            if (npc.type == NPCID.UndeadMiner)
            {
                GlobalNPCs.UndeadMinerThrowGrenade(npc, ref random);
            }
            base.AI(npc);
        }

        public static void UndeadMinerThrowGrenade(NPC npc, ref UnifiedRandom random)
        {
            if 
            (
                npc.GetGlobalNPC<WDALTNPCUtil>().ticksAlive % 120 == 0 &&
                Main.netMode != NetmodeID.MultiplayerClient &&
                Main.player[npc.target] != null &&
                Main.player[npc.target].active &&
                !Main.player[npc.target].dead
            )
            {
                if
                (
                    Collision.CanHitLine
                    (
                        npc.position,
                        npc.width,
                        npc.height,
                        Main.player[npc.target].position,
                        Main.player[npc.target].width,
                        Main.player[npc.target].height
                    )
                )
                {
                    Vector2 vectorToTarget = Main.player[npc.target].Center - npc.Center;
                    vectorToTarget.Normalize();
                    vectorToTarget *= 6.0f;
                    vectorToTarget = vectorToTarget.RotatedBy((MathHelper.ToRadians(30) * (double)npc.direction) * (-1.0));
                    Projectile proj = Projectile.NewProjectileDirect(npc.GetSource_FromAI(), npc.Center, vectorToTarget, ProjectileID.Grenade, npc.damage, 8f);
                    proj.friendly = false;
                    proj.hostile = true;
                    proj.GetGlobalProjectile<WDALTProjectileUtil>().undeadMinerGrenade = true;
                    proj.netUpdate = true;
                }
            }
        }

        public override void PostAI(NPC npc)
        {
            if (Main.getGoodWorld)
            {
                if (npc.type == NPCID.BurningSphere && npc.dontTakeDamage)
                {
                    npc.dontTakeDamage = false;
                }
            }
            base.PostAI(npc);
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (WDALTModSystem.isThoriumModPresent && WDALTModSystem.MCIDIntegrity)
            {
                if
                (
                    npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_SFF) ||
                    npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_DE)
                )
                {
                    npc.buffImmune[ModContent.BuffType<SearingInferno>()] = true;
                }
                else
                {
                    npc.buffImmune[ModContent.BuffType<SearingInferno>()] = false;
                }
            }
            else
            {
                npc.buffImmune[ModContent.BuffType<SearingInferno>()] = false;
            }
            npc.buffImmune[ModContent.BuffType<SearingInferno>()] = false;
            base.UpdateLifeRegen(npc, ref damage);
        }

        public override void ResetEffects(NPC npc)
        {
            base.ResetEffects(npc);
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (npc.HasBuff(ModContent.BuffType<SearingInferno>()))
            {
                drawColor.R = 255;
                drawColor.G = 191;
                drawColor.B = 0;
                int xOffset = random.Next(-(npc.width / 2), (npc.width / 2));
                int yOffset = random.Next(-(npc.height / 2), (npc.height / 2));
                Vector2 dustPosition = npc.Center;
                dustPosition.X += xOffset;
                dustPosition.Y += yOffset;
                int dustType = random.Next(0, 2);
                switch (dustType)
                {
                    case 0:
                        Dust newDust1 = Dust.NewDustPerfect(dustPosition, DustID.SolarFlare);
                        newDust1.noGravity = true;
                        break;
                    case 1:
                        Dust newDust2 = Dust.NewDustPerfect(dustPosition, DustID.Ash);
                        newDust2.noGravity = true;
                        break;
                    default:
                        break;
                }

            }
            base.DrawEffects(npc, ref drawColor);
        }

        public static void ModifyHitPlayerWithProjectile(NPC npc, Player target, Projectile projectile, ref Player.HurtModifiers modifiers)
        {
            if (target.GetModPlayer<WDALTPlayer>().soulPoweredShield)
            {
                float distanceToTarget = Vector2.Distance(npc.Center, target.Center);
                if (distanceToTarget > 512f)
                {
                    distanceToTarget = 512f;
                }
                if (distanceToTarget < 48f)
                {
                    distanceToTarget = 48f;
                }
                float rangeOffsetFactor = ((512f - 48f) / (1f - 0.75f));
                float modifierSPS = Math.Abs(((distanceToTarget - 48f) / rangeOffsetFactor) + 0.75f);
                if (modifierSPS < 0.75f)
                {
                    modifierSPS = 0.75f;
                }
                modifiers.FinalDamage *= modifierSPS;
            }
        }

        public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
        {
            if (npc.HasBuff(ModContent.BuffType<SearingInferno>()))
            {
                modifiers.SourceDamage *= (1.0f - SearingInferno.damageNerfMultiplier);
            }
            if (target.GetModPlayer<WDALTPlayer>().soulPoweredShield)
            {
                float distanceToTarget = Vector2.Distance(npc.Center, target.Center);
                if (distanceToTarget > 512f)
                {
                    distanceToTarget = 512f;
                }
                float rangeOffsetFactor = ((512f - 48f) / (1f - 0.83f)); //3093.333f
                float modifierSPS = Math.Abs(((distanceToTarget - 48f) / rangeOffsetFactor) + 0.83f);
                if (modifierSPS < 0.83f)
                {
                    modifierSPS = 0.83f;
                }
                modifiers.FinalDamage *= modifierSPS;
            }
            base.ModifyHitPlayer(npc, target, ref modifiers);
        }

        public static void OnHitPlayerWithProjectile(NPC npc, Player target, Projectile projectile, Player.HurtInfo info)
        {
            if (target.GetModPlayer<WDALTPlayer>().searingSetBonus)
            {
                npc.AddBuff(ModContent.BuffType<SearingInferno>(), 600, false);
            }
        }

        public override void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
        {
            ApplyDebuffsToPlayerBasedOnNPC(npc.type, target);
            if (target.GetModPlayer<WDALTPlayer>().searingSetBonus)
            {
                npc.AddBuff(ModContent.BuffType<SearingInferno>(), 600, false);
            }
            if (npc.type == NPCID.Gnome)
            {
                target.GetModPlayer<WDALTPlayer>().gnomedDebuffTicksLeft = 18000;
                target.GetModPlayer<WDALTPlayer>().gnomedDebuff = true;
                target.AddBuff(ModContent.BuffType<Gnomed>(), 18000, true);
            }
            if (npc.type == NPCID.Deerclops)
            {
                target.ClearBuff(BuffID.Frozen);
                target.ClearBuff(BuffID.Slow);
                target.buffImmune[BuffID.Frozen] = true;
                target.buffImmune[BuffID.Slow] = true;
            }
            if (npc.type == NPCID.PrimeVice)
            {
                if (!target.HeldItem.IsAir)
                {
                    Item itemToDrop = target.HeldItem;
                    target.DropItem(target.GetSource_FromThis(), target.position, ref itemToDrop);
                    SoundEngine.PlaySound(SoundID.Item71, target.position);
                }
                else
                {
                    int j = -1;
                    if (!target.armor[0].IsAir)
                    {
                        j = 0;
                    }
                    else if (!target.armor[1].IsAir)
                    {
                        j = 1;
                    }
                    else if (!target.armor[2].IsAir)
                    {
                        j = 2;
                    }
                    if (j >= 0)
                    {
                        Item itemToDrop = target.armor[j];
                        target.DropItem(target.GetSource_FromThis(), target.position, ref itemToDrop);
                        SoundEngine.PlaySound(SoundID.Item71, target.position);
                    }
                }
            }
            if (npc.type == NPCID.PrimeSaw)
            {
                SoundEngine.PlaySound(SoundID.Item22, target.position);
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
            /*
            if (InflictSearingInferno1In1Group.Contains(npcType))
            {
                if (random.Next(0, 1) == 0)
                {
                    target.AddBuff(ModContent.BuffType<SearingInferno>(), 240, true); //4s, X2 in Expert, X2.5 in Master
                }
            }
            */
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
            if (InflictWreckedResistance1In1Group.Contains(npcType))
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
            }
            if (npcType == NPCID.PossessedArmor)
            {
                target.AddBuff(BuffID.Blackout, 900, true); //15s, X2 in Expert, X2.5 in Master
            }
            if (WDALTModSystem.isThoriumModPresent && WDALTModSystem.MCIDIntegrity)
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
        }

        public override void ModifyGlobalLoot(GlobalLoot globalLoot)
        {
            foreach (IItemDropRule rule in globalLoot.Get())
            {
                if (rule is ItemDropWithConditionRule conditionRule)
                {
                    if (conditionRule.condition is Conditions.SoulOfLight)
                    {
                        conditionRule.chanceNumerator = 2;
                        conditionRule.chanceDenominator = 5;
                    }
                    if (conditionRule.condition is Conditions.SoulOfNight)
                    {
                        conditionRule.chanceNumerator = 2;
                        conditionRule.chanceDenominator = 5;
                    }
                }
            }
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.GiantTortoise)
            {
                foreach (IItemDropRule rule in npcLoot.Get())
                {
                    if (rule is CommonDrop drop && drop.itemId == ItemID.TurtleShell)
                    {
                        drop.chanceNumerator = 1;
                        drop.chanceDenominator = 5;
                    }
                }
            }
            if (npc.type == NPCID.IceTortoise)
            {
                foreach (IItemDropRule rule in npcLoot.Get())
                {
                    if (rule is CommonDrop drop && drop.itemId == ItemID.FrozenTurtleShell)
                    {
                        drop.chanceNumerator = 1;
                        drop.chanceDenominator = 20;
                    }
                }
            }
            if (npc.type == NPCID.ChaosElemental)
            {
                int chanceNumerator = 1; // 4% chance
                int chanceDenominator = 100;
                TryModifyRodOfDiscordDropChance(npcLoot, chanceNumerator, chanceDenominator);
            }
            if (npc.type == NPCID.CultistBoss)
            {
                int dropAmountMin = 1;
                int dropAmountMax = 1;
                int chanceNumerator = 1;
                int chanceDenominator = 1;
                int itemID = ModContent.ItemType<ThrowOfTheLunatic>();
                CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                npcLoot.Add(drop);
            }
            /*
            if
            (
                npc.type == NPCID.Bee ||
                npc.type == NPCID.BeeSmall
            )
            {
                int dropAmountMin = 1;
                int dropAmountMax = 1;
                int chanceNumerator = 1; // 1 in
                int chanceDenominator = 3; // 3 chance
                int itemID = ModContent.ItemType<Consumablebee>();
                CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                npcLoot.Add(drop);
            }
            */
            if
            (
                npc.type == NPCID.RockGolem ||
                npc.type == NPCID.UndeadMiner
            )
            {
                int dropAmountMin = 1;
                int dropAmountMax = 1;
                int chanceNumerator = 1;
                int chanceDenominator = 24;
                if (npc.type == NPCID.UndeadMiner)
                {
                    chanceDenominator = 12;
                }
                int itemID = ModContent.ItemType<YellowCrystal>();
                CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                npcLoot.Add(drop);
            }

            if (WDALTModSystem.isConsolariaModPresent && WDALTModSystem.MCIDIntegrity)
            {
                if (npc.type == WDALTModContentID.GetConsolariaNPCID(WDALTModContentID.ConsolariaNPC_VM))
                {
                    int dropAmountMin = 1;
                    int dropAmountMax = 1;
                    int chanceNumerator = 1;
                    int chanceDenominator = 12;
                    int itemID = ModContent.ItemType<YellowCrystal>();
                    CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                    npcLoot.Add(drop);
                }
            }

            //Icy fossil drops

            if
            (
                npc.type == NPCID.PigronCorruption ||
                npc.type == NPCID.PigronCrimson ||
                npc.type == NPCID.PigronHallow
            )
            {
                int dropAmountMin = 1;
                int dropAmountMax = 1;
                int chanceNumerator = 20; // 20% chance
                int chanceDenominator = 100;
                int itemID = ModContent.ItemType<IcyFossil>();
                CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                npcLoot.Add(drop);
            }
            if
            (
                npc.type == NPCID.ArmoredViking ||
                npc.type == NPCID.IceTortoise ||
                npc.type == NPCID.IceElemental ||
                npc.type == NPCID.IcyMerman
            )
            {
                int dropAmountMin = 1;
                int dropAmountMax = 1;
                int chanceNumerator = 10; // 10% chance
                int chanceDenominator = 100;
                int itemID = ModContent.ItemType<IcyFossil>();
                CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                npcLoot.Add(drop);
            }
            if
            (
                npc.type == NPCID.IceMimic ||
                npc.type == NPCID.IceGolem
            )
            {
                int dropAmountMin = 1;
                int dropAmountMax = 1;
                int chanceNumerator = 40; // 40% chance
                int chanceDenominator = 100;
                int itemID = ModContent.ItemType<IcyFossil>();
                CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                npcLoot.Add(drop);
            }

            //Dusty fossil drops

            if
            (
                npc.type == NPCID.DesertGhoul ||
                npc.type == NPCID.DesertGhoulCorruption ||
                npc.type == NPCID.DesertGhoulCrimson ||
                npc.type == NPCID.DesertGhoulHallow
            )
            {
                int dropAmountMin = 1;
                int dropAmountMax = 1;
                int chanceNumerator = 20; // 20% chance
                int chanceDenominator = 100;
                int itemID = ModContent.ItemType<DustyFossil>();
                CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                npcLoot.Add(drop);
            }
            if
            (
                npc.type == NPCID.DesertBeast || //Basilisk
                npc.type == NPCID.DesertScorpionWalk || //Sand Poacher
                npc.type == NPCID.DesertScorpionWall || //Sand Poacher
                npc.type == NPCID.DesertLamiaDark ||
                npc.type == NPCID.DesertLamiaLight ||
                npc.type == NPCID.DuneSplicerHead
            )
            {
                int dropAmountMin = 1;
                int dropAmountMax = 1;
                int chanceNumerator = 10; // 10% chance
                int chanceDenominator = 100;
                int itemID = ModContent.ItemType<DustyFossil>();
                CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                npcLoot.Add(drop);
            }
            if
            (
                npc.type == NPCID.DesertDjinn || //Desert Spirit
                npc.type == NPCID.SandElemental
            )
            {
                int dropAmountMin = 1;
                int dropAmountMax = 1;
                int chanceNumerator = 40; // 40% chance
                int chanceDenominator = 100;
                int itemID = ModContent.ItemType<DustyFossil>();
                CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                npcLoot.Add(drop);
            }

            //Hellish fossil drops

            if
            (
                npc.type == NPCID.RedDevil ||
                npc.type == NPCID.DemonTaxCollector
            )
            {
                int dropAmountMin = 1;
                int dropAmountMax = 3;
                int chanceNumerator = 35; // 35% chance
                int chanceDenominator = 100;
                int itemID = ModContent.ItemType<HellishFossil>();
                CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                npcLoot.Add(drop);
            }
            if
            (
                npc.type == NPCID.Lavabat
            )
            {
                int dropAmountMin = 1;
                int dropAmountMax = 2;
                int chanceNumerator = 25; // 25% chance
                int chanceDenominator = 100;
                int itemID = ModContent.ItemType<HellishFossil>();
                CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                npcLoot.Add(drop);
            }

            //Mushroom fossil drops

            if
            (
                npc.type == NPCID.FungoFish
            )
            {
                int dropAmountMin = 1;
                int dropAmountMax = 2;
                int chanceNumerator = 25; // 25% chance
                int chanceDenominator = 100;
                int itemID = ModContent.ItemType<MushroomFossil>();
                CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                npcLoot.Add(drop);
            }
            if
            (
                npc.type == NPCID.GiantFungiBulb
            )
            {
                int dropAmountMin = 1;
                int dropAmountMax = 3;
                int chanceNumerator = 35; // 35% chance
                int chanceDenominator = 100;
                int itemID = ModContent.ItemType<MushroomFossil>();
                CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                npcLoot.Add(drop);
            }
            if
            (
                npc.type == NPCID.FungiSpore
            )
            {
                int dropAmountMin = 1;
                int dropAmountMax = 1;
                int chanceNumerator = 5; // 5% chance
                int chanceDenominator = 100;
                int itemID = ModContent.ItemType<MushroomFossil>();
                CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                npcLoot.Add(drop);
            }

            //Frost Crystal drops
            /*
            if
            (
                npc.type == NPCID.PresentMimic ||
                npc.type == NPCID.Nutcracker ||
                npc.type == NPCID.NutcrackerSpinning ||
                npc.type == NPCID.Yeti ||
                npc.type == NPCID.ElfCopter ||
                npc.type == NPCID.Krampus
            )
            {
                int dropAmountMin = 2;
                int dropAmountMax = 4;
                int chanceNumerator = 20; // 20% chance
                int chanceDenominator = 100;
                int itemID = ModContent.ItemType<FrostCrystal>();
                CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                npcLoot.Add(drop);
            }
            if
            (
                npc.type == NPCID.Flocko ||
                npc.type == NPCID.GingerbreadMan ||
                npc.type == NPCID.ZombieElf ||
                npc.type == NPCID.ZombieElfBeard ||
                npc.type == NPCID.ZombieElfGirl ||
                npc.type == NPCID.ElfArcher
            )
            {
                int dropAmountMin = 1;
                int dropAmountMax = 4;
                int chanceNumerator = 10; // 10% chance
                int chanceDenominator = 100;
                int itemID = ModContent.ItemType<FrostCrystal>();
                CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                npcLoot.Add(drop);
            }
            */
            if
            (
                npc.type == NPCID.WallCreeper ||
                npc.type == NPCID.WallCreeperWall
            )
            {
                int dropAmountMin = 1;
                int dropAmountMax = 3;
                int chanceNumeratorNormal = 1;
                int chanceDenominatorNormal = 2;
                int chanceNumeratorExpert = 9;
                int chanceDenominatorExpert = 10;
                int itemID = ModContent.ItemType<WallCreeperFang>();
                DropBasedOnExpertMode drop = new DropBasedOnExpertMode
                (
                    new CommonDrop(itemID, chanceDenominatorNormal, dropAmountMin, dropAmountMax, chanceNumeratorNormal),
                    new CommonDrop(itemID, chanceDenominatorExpert, dropAmountMin, dropAmountMax, chanceNumeratorExpert)
                );
                npcLoot.Add(drop);
            }
            if
            (
                npc.type == NPCID.WallCreeper ||
                npc.type == NPCID.WallCreeperWall ||
                npc.type == NPCID.BlackRecluse ||
                npc.type == NPCID.BlackRecluseWall
            )
            {
                int dropAmountMin = 5;
                int dropAmountMax = 15;
                int chanceNumeratorNormal = 1;
                int chanceDenominatorNormal = 2;
                int chanceNumeratorExpert = 9;
                int chanceDenominatorExpert = 10;
                int itemID = ItemID.Cobweb;
                DropBasedOnExpertMode drop = new DropBasedOnExpertMode
                (
                    new CommonDrop(itemID, chanceDenominatorNormal, dropAmountMin, dropAmountMax, chanceNumeratorNormal),
                    new CommonDrop(itemID, chanceDenominatorExpert, dropAmountMin, dropAmountMax, chanceNumeratorExpert)
                );
                npcLoot.Add(drop);
            }
            if (npc.type == NPCID.RockGolem)
            {
                int dropAmountMin = 1;
                int dropAmountMax = 3;
                int chanceNumerator = 1; // 100% chance
                int chanceDenominator = 1;
                int itemID = ItemID.Geode;
                CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                npcLoot.Add(drop);
                /*
                CommonDrop[] dropList =
                {
                    new CommonDrop(ItemID.Ruby, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator),
                    new CommonDrop(ItemID.Amber, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator),
                    new CommonDrop(ItemID.Topaz, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator),
                    new CommonDrop(ItemID.Emerald, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator),
                    new CommonDrop(ItemID.Diamond, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator),
                    new CommonDrop(ItemID.Sapphire, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator),
                    new CommonDrop(ItemID.Amethyst, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator),
                };
                foreach(CommonDrop drop in dropList)
                {
                    npcLoot.Add(drop);
                }
                */
            }
            base.ModifyNPCLoot(npc, npcLoot);
        }

        //Modify Rod of Discord drop chance. Are you kidding me, Re-Logic???!!!

        public static void TryModifyRodOfDiscordDropChance(NPCLoot npcLoot, int newChanceNumerator, int newChanceDenominator)
        {
            try
            {
                foreach (IItemDropRule rule in npcLoot.Get())
                {
                    if (rule is LeadingConditionRule leadingConditionRule)
                    {
                        try
                        {
                            foreach (IItemDropRuleChainAttempt chainedRuleAttempt in leadingConditionRule.ChainedRules)
                            {
                                try
                                {
                                    IItemDropRule chainedRule = chainedRuleAttempt.RuleToChain;
                                    if (chainedRule is CommonDrop drop)
                                    {
                                        if (drop.itemId == ItemID.RodofDiscord)
                                        {
                                            drop.chanceNumerator = newChanceNumerator;
                                            drop.chanceDenominator = newChanceDenominator;
                                        }
                                    }
                                    if (chainedRule is DropBasedOnExpertMode expertDropRule)
                                    {
                                        if (expertDropRule.ruleForNormalMode is CommonDrop normalDrop)
                                        {
                                            if (normalDrop.itemId == ItemID.RodofDiscord)
                                            {
                                                normalDrop.chanceNumerator = newChanceNumerator;
                                                normalDrop.chanceDenominator = newChanceDenominator;
                                            }
                                        }
                                        if (expertDropRule.ruleForExpertMode is CommonDrop expertDrop)
                                        {
                                            if (expertDrop.itemId == ItemID.RodofDiscord)
                                            {
                                                expertDrop.chanceNumerator = newChanceNumerator;
                                                expertDrop.chanceDenominator = newChanceDenominator;
                                            }
                                        }
                                    }
                                }
                                catch { }
                            }
                        }
                        catch { }
                    }
                }
            }
            catch { }
        }
    }
}
