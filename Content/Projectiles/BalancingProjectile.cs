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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Utilities;
using WeDoALittleTrolling.Common.Utilities;
using WeDoALittleTrolling.Common.ModSystems;
using WeDoALittleTrolling.Common.Configs;
//using WeDoALittleTrolling.Content.Buffs;

namespace WeDoALittleTrolling.Content.Projectiles
{
    internal class BalancingProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => false;

        public static UnifiedRandom random = new UnifiedRandom();

        public static readonly int[] InflictVenomDebuff1In1Group =
        {
        };
        public static readonly int[] InflictPoisonDebuff1In1Group =
        {
        };
        public static readonly int[] InflictBleedingDebuff1In1Group =
        {
        };
        public static readonly int[] InflictBleedingDebuff1In8Group =
        {
        };

        /*public static readonly int[] InflictVulnerable1In1Group =
        {
            ProjectileID.SeedPlantera,
            ProjectileID.HallowBossDeathAurora,
            ProjectileID.HallowBossLastingRainbow,
            ProjectileID.HallowBossRainbowStreak,
            ProjectileID.FairyQueenHymn,
            ProjectileID.FairyQueenLance,
            ProjectileID.MoonBoulder,
            ProjectileID.MoonLeech,
            ProjectileID.PhantasmalBolt,
            ProjectileID.PhantasmalEye,
            ProjectileID.PhantasmalSphere,
            ProjectileID.CultistBossIceMist,
            ProjectileID.CultistBossLightningOrb,
            ProjectileID.CultistBossLightningOrbArc,
            ProjectileID.CultistBossFireBall,
            ProjectileID.CultistBossFireBallClone,
            ProjectileID.DD2OgreSpit,
            ProjectileID.DD2BetsyFlameBreath,
            ProjectileID.FlamingWood,
            ProjectileID.GreekFire1,
            ProjectileID.GreekFire2,
            ProjectileID.GreekFire3,
            ProjectileID.FrostWave,
            ProjectileID.FrostShard,
            ProjectileID.PineNeedleHostile,
            ProjectileID.OrnamentHostile,
            ProjectileID.OrnamentHostileShrapnel,
            ProjectileID.SaucerMissile,
            ProjectileID.SaucerLaser,
            ProjectileID.SaucerScrap,
            ProjectileID.MartianWalkerLaser,
            ProjectileID.RayGunnerLaser,
            ProjectileID.BrainScramblerBolt,
            ProjectileID.MartianTurretBolt,
            ProjectileID.FrostBeam,
            ProjectileID.SandnadoHostile,
            ProjectileID.SandnadoHostileMark,
            ProjectileID.Sharknado,
            ProjectileID.SharknadoBolt,
            ProjectileID.Cthulunado,
            ProjectileID.EyeFire,
            ProjectileID.Fireball,
            ProjectileID.EyeBeam
        };
        public static readonly int[] InflictWreckedResistance1In1Group =
        {
            ProjectileID.SniperBullet,
            ProjectileID.HappyBomb,
            ProjectileID.Boulder,
            ProjectileID.BouncyBoulder,
            ProjectileID.RollingCactus,
            ProjectileID.MiniBoulder,
            ProjectileID.LifeCrystalBoulder,
            ProjectileID.MoonBoulder,
            ProjectileID.SaucerMissile,
            ProjectileID.RocketSkeleton,
            ProjectileID.DD2OgreStomp,
            ProjectileID.DD2OgreSmash,
            ProjectileID.DD2BetsyFireball,
            ProjectileID.BombSkeletronPrime,
            ProjectileID.ThornBall,
            ProjectileID.Missile,
            ProjectileID.Present,
            ProjectileID.Spike,
        };
        public static readonly int[] InflictDevastated1In1Group =
        {
            ProjectileID.UnholyTridentHostile,
            ProjectileID.AncientDoomProjectile,
            ProjectileID.SaucerDeathray,
            ProjectileID.HappyBomb,
            ProjectileID.SniperBullet,
            ProjectileID.FlamingScythe
        };*/

        public override void SetDefaults(Projectile projectile)
        {
            if (WDALTModSystem.isCalamityModPresent && !ModContent.GetInstance<WDALTServerConfig>().DisableCalamityCompatibilityMode)
            {
                return;
            }
            if (!ModContent.GetInstance<WDALTServerConfig>().DisableRebalancing)
            {
                if (projectile.type == ProjectileID.FrostBlastFriendly)
                {
                    projectile.penetrate = 1;
                }
                if (projectile.type == ProjectileID.ThunderStaffShot)
                {
                    projectile.penetrate = 2;
                }
                if
                (
                    projectile.type == ProjectileID.Flare ||
                    projectile.type == ProjectileID.BlueFlare ||
                    projectile.type == ProjectileID.SpelunkerFlare ||
                    projectile.type == ProjectileID.CursedFlare ||
                    projectile.type == ProjectileID.RainbowFlare ||
                    projectile.type == ProjectileID.ShimmerFlare
                )
                {
                    projectile.timeLeft = 1800;
                }
                if (projectile.type == ProjectileID.CandyCorn)
                {
                    projectile.usesLocalNPCImmunity = true;
                    projectile.localNPCHitCooldown = 6;
                    projectile.usesIDStaticNPCImmunity = false;
                    projectile.idStaticNPCHitCooldown = -1;
                }
                if
                (
                    projectile.type == ProjectileID.FlamingJack ||
                    projectile.type == ProjectileID.DD2BallistraProj ||
                    projectile.type == ProjectileID.AmethystBolt ||
                    projectile.type == ProjectileID.TopazBolt ||
                    projectile.type == ProjectileID.SapphireBolt ||
                    projectile.type == ProjectileID.EmeraldBolt
                )
                {
                    projectile.extraUpdates = 1;
                }
                if
                (
                    projectile.type == ProjectileID.UnholyArrow ||
                    projectile.type == ProjectileID.JestersArrow ||
                    projectile.type == ProjectileID.HellfireArrow ||
                    projectile.type == ProjectileID.BoneArrowFromMerchant ||
                    projectile.type == ProjectileID.HallowStar ||
                    projectile.type == ProjectileID.Volcano
                )
                {
                    projectile.usesLocalNPCImmunity = true;
                    projectile.localNPCHitCooldown = 20;
                    projectile.usesIDStaticNPCImmunity = false;
                    projectile.idStaticNPCHitCooldown = -1;
                }
                if (projectile.type == ProjectileID.IceBoomerang)
                {
                    projectile.extraUpdates = WDALTProjectileUtil.extraUpdatesIceBoomerang;
                }
                if (projectile.type == ProjectileID.BookOfSkullsSkull)
                {
                    projectile.penetrate = -1;
                }
                if
                (
                    projectile.type == ProjectileID.DeathLaser ||
                    projectile.type == ProjectileID.FrostBlastFriendly ||
                    projectile.type == ProjectileID.InfernoFriendlyBolt ||
                    projectile.type == ProjectileID.LunarFlare ||
                    projectile.type == ProjectileID.Bubble
                )
                {
                    projectile.tileCollide = false;
                }
                if
                (
                    projectile.type == ProjectileID.FrostBlastFriendly ||
                    projectile.type == ProjectileID.PoisonFang ||
                    projectile.type == ProjectileID.VenomFang ||
                    projectile.type == ProjectileID.SkyFracture ||
                    projectile.type == ProjectileID.Meteor1 ||
                    projectile.type == ProjectileID.Meteor2 ||
                    projectile.type == ProjectileID.Meteor3 ||
                    projectile.type == ProjectileID.Blizzard ||
                    projectile.type == ProjectileID.InfernoFriendlyBolt ||
                    projectile.type == ProjectileID.FrostBoltStaff ||
                    projectile.type == ProjectileID.UnholyTridentFriendly ||
                    projectile.type == ProjectileID.BookStaffShot ||
                    projectile.type == ProjectileID.LunarFlare ||
                    projectile.type == ProjectileID.Bubble ||
                    projectile.type == ProjectileID.Landmine ||
                    projectile.type == ProjectileID.DiamondBolt ||
                    projectile.type == ProjectileID.RubyBolt ||
                    projectile.type == ProjectileID.AmberBolt ||
                    projectile.type == ProjectileID.ThunderStaffShot
                )
                {
                    projectile.usesLocalNPCImmunity = true;
                    projectile.localNPCHitCooldown = 10;
                    projectile.usesIDStaticNPCImmunity = false;
                    projectile.idStaticNPCHitCooldown = -1;
                }
                if
                (
                    projectile.type == ProjectileID.PoisonFang ||
                    projectile.type == ProjectileID.VenomFang
                )
                {
                    projectile.timeLeft = 80;
                }
                if
                (
                    projectile.type == ProjectileID.BlackBolt
                )
                {
                    projectile.timeLeft = 60;
                }
                if (WDALTModSystem.isThoriumModPresent && WDALTModSystem.MCIDIntegrity)
                {
                    if
                    (
                        projectile.type == WDALTModContentID.GetThoriumBossProjectileID(WDALTModContentID.ThoriumBossProjectile_LI_V1) ||
                        projectile.type == WDALTModContentID.GetThoriumBossProjectileID(WDALTModContentID.ThoriumBossProjectile_LI_V2) ||
                        projectile.type == WDALTModContentID.GetThoriumBossProjectileID(WDALTModContentID.ThoriumBossProjectile_LI_V3) ||
                        projectile.type == WDALTModContentID.GetThoriumBossProjectileID(WDALTModContentID.ThoriumBossProjectile_LI_V4) ||
                        projectile.type == WDALTModContentID.GetThoriumBossProjectileID(WDALTModContentID.ThoriumBossProjectile_LI_V5) ||
                        projectile.type == WDALTModContentID.GetThoriumBossProjectileID(WDALTModContentID.ThoriumBossProjectile_LI_V6) ||
                        projectile.type == WDALTModContentID.GetThoriumBossProjectileID(WDALTModContentID.ThoriumBossProjectile_LI_V7) ||
                        projectile.type == WDALTModContentID.GetThoriumBossProjectileID(WDALTModContentID.ThoriumBossProjectile_LI_V8) ||
                        projectile.type == WDALTModContentID.GetThoriumBossProjectileID(WDALTModContentID.ThoriumBossProjectile_LI_V9) ||
                        projectile.type == WDALTModContentID.GetThoriumBossProjectileID(WDALTModContentID.ThoriumBossProjectile_LI_V10) ||
                        projectile.type == WDALTModContentID.GetThoriumBossProjectileID(WDALTModContentID.ThoriumBossProjectile_LI_V11) ||
                        projectile.type == WDALTModContentID.GetThoriumBossProjectileID(WDALTModContentID.ThoriumBossProjectile_LI_V12)
                    )
                    {
                        if (Main.expertMode)
                        {
                            projectile.extraUpdates++;
                            if (projectile.type == WDALTModContentID.GetThoriumBossProjectileID(WDALTModContentID.ThoriumBossProjectile_LI_V12))
                            {
                                projectile.extraUpdates++;
                            }
                        }
                    }
                }
            }
            base.SetDefaults(projectile);
        }

        public override bool PreAI(Projectile projectile)
        {
            if (WDALTModSystem.isCalamityModPresent && !ModContent.GetInstance<WDALTServerConfig>().DisableCalamityCompatibilityMode)
            {
                return base.PreAI(projectile);
            }
            if (!ModContent.GetInstance<WDALTServerConfig>().DisableRebalancing)
            {
                if (projectile.type == ProjectileID.IceBoomerang)
                {
                    int baseSoundDelay = 8;
                    if (projectile.soundDelay == (baseSoundDelay - 1))
                    {
                        if (!projectile.GetGlobalProjectile<WDALTProjectileUtil>().HasRecentlyPassedSoundDelay(ProjectileID.IceBoomerang))
                        {
                            int multiplier = (WDALTProjectileUtil.extraUpdatesIceBoomerang + 1);
                            projectile.soundDelay = (baseSoundDelay * multiplier);
                        }
                    }
                }
                if (projectile.type == ProjectileID.OrnamentFriendly && projectile.GetGlobalProjectile<WDALTProjectileUtil>().ticksAlive > 25)
                {
                    float lowest_distance = 512f; //Homing detection range
                    float correction_factor = 2.5f;
                    float speed = 10f;
                    NPC target = null;
                    for (int i = 0; i < Main.npc.Length; i++)
                    {
                        NPC currentTarget = Main.npc[i];
                        if
                        (
                            !currentTarget.dontTakeDamage &&
                            currentTarget.active &&
                            currentTarget.CanBeChasedBy() &&
                            !currentTarget.friendly &&
                            !currentTarget.CountsAsACritter &&
                            !currentTarget.isLikeATownNPC &&
                            currentTarget.type != NPCID.TargetDummy &&
                            Collision.CanHitLine
                            (
                                projectile.position,
                                projectile.width,
                                projectile.height,
                                currentTarget.position,
                                currentTarget.width,
                                currentTarget.height
                            )
                        )
                        {
                            Vector2 vectorToTarget = new Vector2(currentTarget.Center.X - projectile.Center.X, currentTarget.Center.Y - projectile.Center.Y);
                            if (vectorToTarget.Length() < lowest_distance)
                            {
                                lowest_distance = vectorToTarget.Length();
                                target = currentTarget;
                            }
                        }
                    }
                    if (target != null)
                    {
                        Vector2 vectorToTarget = new Vector2(target.Center.X - projectile.Center.X, target.Center.Y - projectile.Center.Y);
                        vectorToTarget = vectorToTarget.SafeNormalize(Vector2.Zero);
                        projectile.velocity = projectile.velocity + (vectorToTarget * correction_factor);
                        Vector2 normalizedVeloctiy = projectile.velocity;
                        normalizedVeloctiy = normalizedVeloctiy.SafeNormalize(Vector2.Zero);
                        projectile.velocity = normalizedVeloctiy * speed;
                        return false;
                    }
                }
            }
            return base.PreAI(projectile);
        }

        public override void AI(Projectile projectile)
        {
            if (WDALTModSystem.isCalamityModPresent && !ModContent.GetInstance<WDALTServerConfig>().DisableCalamityCompatibilityMode)
            {
                return;
            }
            if (!ModContent.GetInstance<WDALTServerConfig>().DisableRebalancing)
            {
                if (projectile.type == ProjectileID.Bubble)
                {
                    projectile.velocity *= 1.03f;
                }
                if
                (
                    projectile.type == ProjectileID.FrostBlastFriendly ||
                    projectile.type == ProjectileID.PoisonFang ||
                    projectile.type == ProjectileID.VenomFang ||
                    projectile.type == ProjectileID.SkyFracture ||
                    projectile.type == ProjectileID.Meteor1 ||
                    projectile.type == ProjectileID.Meteor2 ||
                    projectile.type == ProjectileID.Meteor3 ||
                    projectile.type == ProjectileID.Blizzard ||
                    projectile.type == ProjectileID.InfernoFriendlyBolt ||
                    projectile.type == ProjectileID.FrostBoltStaff ||
                    projectile.type == ProjectileID.UnholyTridentFriendly ||
                    projectile.type == ProjectileID.BookStaffShot ||
                    projectile.type == ProjectileID.LunarFlare ||
                    (projectile.type == ProjectileID.DiamondBolt && projectile.penetrate >= projectile.maxPenetrate) ||
                    (projectile.type == ProjectileID.RubyBolt && projectile.penetrate >= projectile.maxPenetrate) ||
                    (projectile.type == ProjectileID.AmberBolt && projectile.penetrate >= projectile.maxPenetrate) ||
                    (projectile.type == ProjectileID.ThunderStaffShot && projectile.penetrate >= projectile.maxPenetrate)
                )
                {
                    float lowest_distance = 0f; //Homing detection range
                    float correction_factor = 0f;
                    switch (projectile.type)
                    {
                        case ProjectileID.FrostBlastFriendly:
                            lowest_distance = 1024f;
                            correction_factor = 0.875f;
                            break;
                        case ProjectileID.PoisonFang:
                        case ProjectileID.VenomFang:
                        case ProjectileID.DiamondBolt:
                        case ProjectileID.RubyBolt:
                        case ProjectileID.AmberBolt:
                        case ProjectileID.ThunderStaffShot:
                            lowest_distance = 320f;
                            correction_factor = 3.5f;
                            break;
                        case ProjectileID.SkyFracture:
                            lowest_distance = 320f;
                            correction_factor = 3.5f;
                            break;
                        case ProjectileID.Meteor1:
                        case ProjectileID.Meteor2:
                        case ProjectileID.Meteor3:
                            lowest_distance = 512f;
                            correction_factor = 0.84f;
                            break;
                        case ProjectileID.Blizzard:
                            lowest_distance = 512f;
                            correction_factor = 1.25f;
                            break;
                        case ProjectileID.InfernoFriendlyBolt:
                            lowest_distance = 240f;
                            correction_factor = 2.0f;
                            break;
                        case ProjectileID.FrostBoltStaff:
                            lowest_distance = 320f;
                            correction_factor = 4.0f;
                            break;
                        case ProjectileID.UnholyTridentFriendly:
                            lowest_distance = 320f;
                            correction_factor = 3.25f;
                            break;
                        case ProjectileID.BookStaffShot:
                            lowest_distance = 320f;
                            correction_factor = 1.375f;
                            break;
                        case ProjectileID.LunarFlare:
                            lowest_distance = 512f;
                            correction_factor = 1.67f;
                            break;
                        default:
                            break;
                    }
                    NPC target = null;
                    for (int i = 0; i < Main.npc.Length; i++)
                    {
                        NPC currentTarget = Main.npc[i];
                        if
                        (
                            !currentTarget.dontTakeDamage &&
                            currentTarget.active &&
                            currentTarget.CanBeChasedBy() &&
                            !currentTarget.friendly &&
                            !currentTarget.CountsAsACritter &&
                            !currentTarget.isLikeATownNPC &&
                            currentTarget.type != NPCID.TargetDummy &&
                            Collision.CanHitLine
                            (
                                projectile.position,
                                projectile.width,
                                projectile.height,
                                currentTarget.position,
                                currentTarget.width,
                                currentTarget.height
                            )
                        )
                        {
                            Vector2 vectorToTarget = new Vector2(currentTarget.Center.X - projectile.Center.X, currentTarget.Center.Y - projectile.Center.Y);
                            if (vectorToTarget.Length() < lowest_distance)
                            {
                                lowest_distance = vectorToTarget.Length();
                                target = currentTarget;
                            }
                        }
                    }
                    if (target != null)
                    {
                        Vector2 vectorToTarget = new Vector2(target.Center.X - projectile.Center.X, target.Center.Y - projectile.Center.Y);
                        vectorToTarget = vectorToTarget.SafeNormalize(Vector2.Zero);
                        float originalLength = projectile.velocity.Length();
                        projectile.velocity = projectile.velocity + (vectorToTarget * correction_factor);
                        Vector2 normalizedVeloctiy = projectile.velocity;
                        normalizedVeloctiy = normalizedVeloctiy.SafeNormalize(Vector2.Zero);
                        projectile.velocity = normalizedVeloctiy * originalLength;
                    }
                }
            }
            base.AI(projectile);
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (!ModContent.GetInstance<WDALTServerConfig>().DisableRebalancing)
            {
                if (projectile.type == ProjectileID.DeerclopsRangedProjectile)
                {
                    projectile.damage = (int)Math.Round(projectile.damage * 0.75);
                    projectile.netUpdate = true;
                }
                if (projectile.type == ProjectileID.OrnamentFriendly)
                {
                    projectile.damage = (int)Math.Round(projectile.damage * 0.5f);
                    projectile.netUpdate = true;
                }
            }
            base.OnSpawn(projectile, source);
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!ModContent.GetInstance<WDALTServerConfig>().DisableRebalancing)
            {
                if (projectile.type == ProjectileID.SporeCloud)
                {
                    target.AddBuff(BuffID.Poisoned, 240, false);
                }
                if (projectile.type == ProjectileID.LeadShortswordStab)
                {
                    target.AddBuff(BuffID.Poisoned, 1800, false);
                }
                if (projectile.type == ProjectileID.DeathSickle)
                {
                    target.AddBuff(BuffID.ShadowFlame, 240, false);
                }
                if (projectile.type == ProjectileID.IceSickle)
                {
                    target.AddBuff(BuffID.Frostburn, 240, false);
                }
                if (projectile.type == ProjectileID.TrueExcalibur)
                {
                    target.AddBuff(BuffID.Ichor, 240, false);
                }
                if (projectile.type == ProjectileID.NightsEdge)
                {
                    target.AddBuff(BuffID.ShadowFlame, 240, false);
                }
                if (projectile.type == ProjectileID.TrueNightsEdge)
                {
                    target.AddBuff(BuffID.CursedInferno, 240, false);
                }
                if (projectile.type == ProjectileID.HellfireArrow)
                {
                    target.AddBuff(BuffID.OnFire3, 240, false);
                }
            }
            base.OnHitNPC(projectile, target, hit, damageDone);
        }

        public override void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
        {
            if (!ModContent.GetInstance<WDALTServerConfig>().DisableRebalancing)
            {
                if (InflictVenomDebuff1In1Group.Contains(projectile.type))
                {
                    if (random.Next(0, 1) == 0) //1 in 1 Chance
                    {
                        target.AddBuff(BuffID.Venom, 240, true); //4s, X2 in Expert, X2.5 in Master
                    }
                }
                if (InflictPoisonDebuff1In1Group.Contains(projectile.type))
                {
                    if (random.Next(0, 1) == 0) //1 in 1 Chance
                    {
                        target.AddBuff(BuffID.Poisoned, 240, true); //4s, X2 in Expert, X2.5 in Master
                    }
                }
                if (InflictBleedingDebuff1In1Group.Contains(projectile.type))
                {
                    if (random.Next(0, 1) == 0) //1 in 1 Chance
                    {
                        target.AddBuff(BuffID.Bleeding, 960, true); //16s, X2 in Expert, X2.5 in Master
                    }
                }
                if (InflictBleedingDebuff1In8Group.Contains(projectile.type))
                {
                    if (random.Next(0, 8) == 0) //1 in 8 Chance
                    {
                        target.AddBuff(BuffID.Bleeding, 480, true); //8s, X2 in Expert, X2.5 in Master
                    }
                }
                /*if (InflictWreckedResistance1In1Group.Contains(projectile.type))
                {
                    if (random.Next(0, 1) == 0 && Main.masterMode)
                    {
                        target.AddBuff(ModContent.BuffType<WreckedResistance>(), 3600, true); //1m, X2 in Expert, X2.5 in Master
                    }
                }
                if (InflictVulnerable1In1Group.Contains(projectile.type))
                {
                    if (random.Next(0, 1) == 0 && Main.masterMode)
                    {
                        target.AddBuff(ModContent.BuffType<Vulnerable>(), 3600, true); //1m, X2 in Expert, X2.5 in Master
                    }
                }
                if (projectile.type == ProjectileID.PoisonSeedPlantera && Main.masterMode)
                {
                    target.AddBuff(ModContent.BuffType<Vulnerable>(), 3600, true); //1m, X2 in Expert, X2.5 in Master
                    target.AddBuff(ModContent.BuffType<WreckedResistance>(), 3600, true); //1m, X2 in Expert, X2.5 in Master
                }
                if
                (
                    projectile.type == ProjectileID.DeathLaser &&
                    projectile.GetGlobalProjectile<WDALTProjectileUtil>().TryGetParentNPC(out NPC attacker)
                )
                {
                    if ((attacker.type == NPCID.PrimeLaser || attacker.type == NPCID.SkeletronPrime || attacker.type == NPCID.Retinazer) && Main.masterMode)
                    {
                        target.AddBuff(ModContent.BuffType<Vulnerable>(), 3600, true); //1m, X2 in Expert, X2.5 in Master
                    }
                }
                if (InflictDevastated1In1Group.Contains(projectile.type))
                {
                    if (random.Next(0, 1) == 0 && Main.masterMode)
                    {
                        target.AddBuff(ModContent.BuffType<Devastated>(), 3600, true); //1m, X2 in Expert, X2.5 in Master
                        Devastated.DevastatePlayer(target);
                    }
                }
                if (projectile.type == ProjectileID.BlackBolt)
                {
                    if (Main.masterMode)
                    {
                        target.AddBuff(ModContent.BuffType<WreckedResistance>(), 3600, true); //1m, X2 in Expert, X2.5 in Master
                    }
                }
                if (WDALTModSystem.isThoriumModPresent && WDALTModSystem.MCIDIntegrity)
                {
                    if (WDALTModContentID.GetThoriumBossProjectileInflictVulnerable1in1Group().Contains(projectile.type))
                    {
                        if (random.Next(0, 1) == 0 && Main.masterMode)
                        {
                            target.AddBuff(ModContent.BuffType<Vulnerable>(), 3600, true); //1m, X2 in Expert, X2.5 in Master
                        }
                    }
                    if (WDALTModContentID.GetThoriumBossProjectileInflictDevastated1in1Group().Contains(projectile.type))
                    {
                        if (random.Next(0, 1) == 0 && Main.masterMode)
                        {
                            target.AddBuff(ModContent.BuffType<Devastated>(), 3600, true); //1m, X2 in Expert, X2.5 in Master
                            Devastated.DevastatePlayer(target);
                        }
                    }
                }
                if (WDALTModSystem.isSpiritModPresent && WDALTModSystem.MCIDIntegrity)
                {
                    if (WDALTModContentID.GetSpiritBossProjectileInflictVulnerable1in1Group().Contains(projectile.type))
                    {
                        if (random.Next(0, 1) == 0 && Main.masterMode)
                        {
                            target.AddBuff(ModContent.BuffType<Vulnerable>(), 3600, true); //1m, X2 in Expert, X2.5 in Master
                        }
                    }
                }
                if (WDALTModSystem.isSpookyModPresent && WDALTModSystem.MCIDIntegrity)
                {
                    if (WDALTModContentID.GetSpookyBossProjectileInflictVulnerable1in1Group().Contains(projectile.type))
                    {
                        if (random.Next(0, 1) == 0 && Main.masterMode)
                        {
                            target.AddBuff(ModContent.BuffType<Vulnerable>(), 3600, true); //1m, X2 in Expert, X2.5 in Master
                        }
                    }
                }*/
                if
                (
                    projectile.type == ProjectileID.DeerclopsIceSpike ||
                    projectile.type == ProjectileID.DeerclopsRangedProjectile ||
                    projectile.type == ProjectileID.InsanityShadowHostile
                )
                {
                    target.ClearBuff(BuffID.Frozen);
                    target.ClearBuff(BuffID.Slow);
                    target.buffImmune[BuffID.Frozen] = true;
                    target.buffImmune[BuffID.Slow] = true;
                }
            }
            base.OnHitPlayer(projectile, target, info);
        }

        public override void ModifyHitPlayer(Projectile projectile, Player target, ref Player.HurtModifiers modifiers)
        {
            if (!ModContent.GetInstance<WDALTServerConfig>().DisableRebalancing)
            {
                if (projectile.type == ProjectileID.PhantasmalDeathray && projectile.GetGlobalProjectile<WDALTProjectileUtil>().TryGetParentNPC(out NPC npc))
                {
                    if (npc.type == NPCID.MoonLordFreeEye)
                    {
                        modifiers.SourceDamage *= 0.75f;
                    }
                    if (npc.type == NPCID.MoonLordHead)
                    {
                        modifiers.SourceDamage *= (4f / 3f);
                    }
                }
            }
        }

        public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (!ModContent.GetInstance<WDALTServerConfig>().DisableRebalancing)
            {
                if (projectile.type == ProjectileID.CoolWhipProj)
                {
                    modifiers.SourceDamage *= 4f;
                }
            }
            base.ModifyHitNPC(projectile, target, ref modifiers);
        }
    }
}
