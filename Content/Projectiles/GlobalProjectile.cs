/*
    WeDoALittleTrolling is a Terraria Mod made with tModLoader.
    Copyright (C) 2022-2023 LukasV-Coding

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
using WeDoALittleTrolling.Common.Utilities;
using WeDoALittleTrolling.Content.Buffs;
using WeDoALittleTrolling.Content.NPCs;
using WeDoALittleTrolling.Content.Prefixes;
using WeDoALittleTrolling.Content.Items;
using WeDoALittleTrolling.Content.Items.Weapons;
using WeDoALittleTrolling.Content.Items.Accessories;
using Microsoft.Xna.Framework;

namespace WeDoALittleTrolling.Content.Projectiles
{
    internal class GlobalProjectiles : GlobalProjectile
    {
        public override bool InstancePerEntity => false;

        public static Random random = new Random();

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
        public static readonly int[] InflictSearingInferno1In1Group =
        {
            ProjectileID.Fireball,
            ProjectileID.EyeBeam
        };
        public static readonly int[] InflictWreckedResistance1In1Group =
        {
            ProjectileID.SeedPlantera,
            ProjectileID.PoisonSeedPlantera,
            ProjectileID.DeathLaser,
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
            ProjectileID.DD2OgreStomp,
            ProjectileID.DD2OgreSpit,
            ProjectileID.DD2OgreSmash,
            ProjectileID.DD2BetsyFireball,
            ProjectileID.DD2BetsyFlameBreath,
            ProjectileID.FlamingWood,
            ProjectileID.GreekFire1,
            ProjectileID.GreekFire2,
            ProjectileID.GreekFire3,
            ProjectileID.FlamingScythe,
            ProjectileID.FrostWave,
            ProjectileID.FrostShard,
            ProjectileID.Missile,
            ProjectileID.Present,
            ProjectileID.Spike,
            ProjectileID.PineNeedleHostile,
            ProjectileID.OrnamentHostile,
            ProjectileID.OrnamentHostileShrapnel,
            ProjectileID.SaucerMissile,
            ProjectileID.SaucerLaser,
            ProjectileID.SaucerScrap
        };
        public static readonly int[] InflictDevastated1In1Group =
        {
            ProjectileID.UnholyTridentHostile,
            ProjectileID.AncientDoomProjectile,
            ProjectileID.PhantasmalDeathray,
            ProjectileID.SaucerDeathray,
            ProjectileID.FairyQueenSunDance,
            ProjectileID.Sharknado,
            ProjectileID.Cthulunado,
            ProjectileID.BombSkeletronPrime,
            ProjectileID.ThornBall,
            ProjectileID.EyeFire
        };
        
        public override void SetDefaults(Projectile projectile)
        {
            if(projectile.type == ProjectileID.IceBoomerang)
            {
                projectile.extraUpdates = projectile.GetGlobalProjectile<WDALTProjectileUtil>().extraUpdatesIceBoomerang;
            }
            if(projectile.type == ProjectileID.BookOfSkullsSkull)
            {
                projectile.penetrate = -1;
            }
            if
            (
                projectile.type == ProjectileID.DeathLaser ||
                projectile.type == ProjectileID.PinkLaser ||
                projectile.type == ProjectileID.FrostBlastFriendly ||
                projectile.type == ProjectileID.PoisonFang ||
                projectile.type == ProjectileID.VenomFang ||
                projectile.type == ProjectileID.SkyFracture ||
                projectile.type == ProjectileID.InfernoFriendlyBolt ||
                projectile.type == ProjectileID.PineNeedleFriendly ||
                projectile.type == ProjectileID.ApprenticeStaffT3Shot
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
                projectile.type == ProjectileID.PineNeedleFriendly ||
                projectile.type == ProjectileID.ApprenticeStaffT3Shot
            )
            {
                projectile.usesLocalNPCImmunity = true;
            }
            if
            (
                projectile.type == ProjectileID.PoisonFang ||
                projectile.type == ProjectileID.VenomFang
            )
            {
                projectile.timeLeft = 80;
            }
            base.SetDefaults(projectile);
        }
        
        public override bool PreAI(Projectile projectile)
        {
            if(projectile.type == ProjectileID.IceBoomerang)
            {
                int baseSoundDelay = 8;
                if(projectile.soundDelay == (baseSoundDelay - 1))
                {
                    if(!projectile.GetGlobalProjectile<WDALTProjectileUtil>().HasRecentlyPassedSoundDelay(ProjectileID.IceBoomerang))
                    {
                        int multiplier = (projectile.GetGlobalProjectile<WDALTProjectileUtil>().extraUpdatesIceBoomerang + 1);
                        projectile.soundDelay = (baseSoundDelay * multiplier);
                    }
                }
            }
            return base.PreAI(projectile);
        }

        public override void AI(Projectile projectile)
        {
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
                projectile.type == ProjectileID.PineNeedleFriendly ||
                projectile.type == ProjectileID.ApprenticeStaffT3Shot
            )
            {
                float lowest_distance = 999; //Homing detection range
                NPC target = null;
                for(int i = 0; i < 200; i++)
                {
                    NPC currentTarget = Main.npc[i];
                    if
                    (
                        !currentTarget.dontTakeDamage &&
                        currentTarget.active &&
                        !currentTarget.friendly && 
                        !currentTarget.CountsAsACritter && 
                        !currentTarget.isLikeATownNPC && 
                        currentTarget.type != NPCID.TargetDummy
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
                if(target != null)
                {
                    Vector2 vectorToTarget = new Vector2(target.Center.X - projectile.Center.X, target.Center.Y - projectile.Center.Y);
                    vectorToTarget.Normalize();
                    float originalLength = projectile.velocity.Length();
                    projectile.velocity = projectile.velocity + (vectorToTarget * 3.5f);
                    Vector2 normalizedVeloctiy = projectile.velocity;
                    normalizedVeloctiy.Normalize();
                    projectile.velocity = normalizedVeloctiy * originalLength;
                }
            }
            base.AI(projectile);
        }

        public override void PostAI(Projectile projectile)
        {
            if (projectile.type == ProjectileID.TrueNightsEdge)
            {
                projectile.localAI[0] = 32f;
                projectile.localAI[1] = 0f;
                projectile.Center = projectile.GetGlobalProjectile<WDALTProjectileUtil>().spawnCenter + (projectile.velocity * projectile.GetGlobalProjectile<WDALTProjectileUtil>().ticksAlive);
            }
            if (projectile.type == ProjectileID.SolarWhipSword)
            {
                bool success = false;
                if (projectile.GetGlobalProjectile<WDALTProjectileUtil>().TryGetParentHeldItem(out Item item))
                {
                    if (item.prefix == ModContent.PrefixType<Colossal>())
                    {
                        projectile.ai[0] -= 0.5f;
                        success = true;
                    }
                }
                if(!success && projectile.GetGlobalProjectile<WDALTProjectileUtil>().colossalSolarWhip)
                {
                    projectile.ai[0] -= 0.5f;
                }
            }
            base.PostAI(projectile);
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (projectile.type == ProjectileID.TrueNightsEdge)
            {
                projectile.damage *= 2;
                projectile.tileCollide = true;
                projectile.light = 0.5f;
                projectile.netUpdate = true;
            }
            if (projectile.GetGlobalProjectile<WDALTProjectileUtil>().TryGetParentHeldItem(out Item item))
            {
                if (item.prefix == ModContent.PrefixType<Colossal>())
                {
                    if (Colossal.CompatibleProjectileIDs.Contains(projectile.type) || Colossal.ShortswordCompatibleProjectileIDs.Contains(projectile.type))
                    {
                        projectile.scale *= 2;
                    }
                    if (Colossal.SpeedupProjectileIDs.Contains(projectile.type))
                    {
                        projectile.velocity *= 2;
                    }
                    if (projectile.type == ProjectileID.SolarWhipSword)
                    {
                        projectile.extraUpdates = 1;
                        projectile.GetGlobalProjectile<WDALTProjectileUtil>().colossalSolarWhip = true;
                    }
                    projectile.netUpdate = true;
                }
            }
            base.OnSpawn(projectile, source);
        }

        public override bool PreKill(Projectile projectile, int timeLeft)
        {
            if(projectile.type == ProjectileID.TrueNightsEdge)
            {
                for(int i = 0;i < 320;i++)
                {
                    int rMax = (int)Math.Round(projectile.width*3*projectile.scale);
                    double r = rMax * Math.Sqrt(random.NextDouble());
                    double angle = random.NextDouble() * 2 * Math.PI;
                    int xOffset = (int)Math.Round(r * Math.Cos(angle));
                    int yOffset = (int)Math.Round(r * Math.Sin(angle));
                    Vector2 dustPosition = projectile.Center;
                    dustPosition.X += xOffset;
                    dustPosition.Y += yOffset;
                    int dustType = random.Next(0, 1);
                    switch (dustType)
                    {
                        case 0:
                            Dust newDust = Dust.NewDustPerfect(dustPosition, DustID.Terra, null, 0, default, projectile.scale);
                            newDust.noGravity = true;
                            break;
                        default:
                            break;
                    }
                }
                SoundEngine.PlaySound(SoundID.Item10, projectile.Center);
            }
            return base.PreKill(projectile, timeLeft);
        }

        public override void ModifyDamageHitbox(Projectile projectile, ref Rectangle hitbox)
        {

            if (projectile.GetGlobalProjectile<WDALTProjectileUtil>().TryGetParentHeldItem(out Item item))
            {
                if (item.prefix == ModContent.PrefixType<Colossal>())
                {
                    if (Colossal.CompatibleProjectileIDs.Contains(projectile.type))
                    {
                        int scalingFactor = 2;
                        int horizonalIncrease = (int)Math.Round((hitbox.Width * scalingFactor) / (2 * Math.Sqrt(2)));
                        int verticalIncrease = (int)Math.Round((hitbox.Height * scalingFactor) / (2 * Math.Sqrt(2)));
                        if(projectile.type == ProjectileID.Terragrim || projectile.type == ProjectileID.Arkhalis)
                        {
                            horizonalIncrease = (int)Math.Round(horizonalIncrease / Math.Sqrt(2));
                            verticalIncrease = (int)Math.Round(verticalIncrease / Math.Sqrt(2));
                        }
                        hitbox.Inflate(horizonalIncrease, verticalIncrease);
                    }
                    else if(Colossal.ShortswordCompatibleProjectileIDs.Contains(projectile.type))
                    {
                        int scalingFactor = 2;
                        int horizonalIncrease  = (int)Math.Round((hitbox.Width * scalingFactor) / (Math.Sqrt(2)));
                        int verticalIncrease = (int)Math.Round((hitbox.Height * scalingFactor) / (Math.Sqrt(2)));
                        hitbox.Inflate(horizonalIncrease, verticalIncrease);
                    }
                }
            }
            base.ModifyDamageHitbox(projectile, ref hitbox);
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if(projectile.type == ProjectileID.SporeCloud)
            {
                target.AddBuff(BuffID.Poisoned, 240, false);
            }
            if(projectile.type == ProjectileID.DeathSickle)
            {
                target.AddBuff(BuffID.ShadowFlame, 240, false);
            }
            if(projectile.type == ProjectileID.IceSickle)
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
            base.OnHitNPC(projectile, target, hit, damageDone);
        }

        public override void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
        {
            if(InflictVenomDebuff1In1Group.Contains(projectile.type))
            {
                if(random.Next(0, 1) == 0) //1 in 1 Chance
                {
                    target.AddBuff(BuffID.Venom, 240, true); //4s, X2 in Expert, X2.5 in Master
                }
            }
            if(InflictPoisonDebuff1In1Group.Contains(projectile.type))
            {
                if(random.Next(0, 1) == 0) //1 in 1 Chance
                {
                    target.AddBuff(BuffID.Poisoned, 240, true); //4s, X2 in Expert, X2.5 in Master
                }
            }
            if(InflictBleedingDebuff1In1Group.Contains(projectile.type))
            {
                if(random.Next(0, 1) == 0) //1 in 1 Chance
                {
                    target.AddBuff(BuffID.Bleeding, 960, true); //16s, X2 in Expert, X2.5 in Master
                }
            }
            if(InflictBleedingDebuff1In8Group.Contains(projectile.type))
            {
                if(random.Next(0, 8) == 0) //1 in 8 Chance
                {
                    target.AddBuff(BuffID.Bleeding, 480, true); //8s, X2 in Expert, X2.5 in Master
                }
            }
            if(InflictSearingInferno1In1Group.Contains(projectile.type))
            {
                if(random.Next(0, 1) == 0)
                {
                    target.AddBuff(ModContent.BuffType<SearingInferno>(), 240, true); //4s, X2 in Expert, X2.5 in Master
                }
            }
            if(InflictWreckedResistance1In1Group.Contains(projectile.type))
            {
                if(random.Next(0, 1) == 0)
                {
                    target.AddBuff(ModContent.BuffType<WreckedResistance>(), 3600, true); //1m, X2 in Expert, X2.5 in Master
                }
            }
            if(InflictDevastated1In1Group.Contains(projectile.type))
            {
                if(random.Next(0, 1) == 0)
                {
                    target.AddBuff(ModContent.BuffType<Devastated>(), 3600, true); //1m, X2 in Expert, X2.5 in Master
                }
            }
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
            base.OnHitPlayer(projectile, target, info);
        }

        public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
        {
            if
            (
                projectile.type == ProjectileID.FrostBlastFriendly ||
                projectile.type == ProjectileID.BabySpider ||
                projectile.type == ProjectileID.SpiderEgg ||
                projectile.type == ProjectileID.HoundiusShootiusFireball ||
                projectile.type == ProjectileID.MoonlordTurretLaser ||
                projectile.type == ProjectileID.RainbowCrystalExplosion
            )
            {
                if(random.Next(0, 100) < 30)
                {
                    modifiers.SetCrit();
                }
            }
            base.ModifyHitNPC(projectile, target, ref modifiers);
        }
    }
}
