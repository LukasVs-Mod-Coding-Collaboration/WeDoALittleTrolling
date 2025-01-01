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
using WeDoALittleTrolling.Common.Utilities;
using WeDoALittleTrolling.Common.ModSystems;
using WeDoALittleTrolling.Content.Buffs;
using WeDoALittleTrolling.Content.NPCs;
using WeDoALittleTrolling.Content.Prefixes;
using WeDoALittleTrolling.Content.Items;
using WeDoALittleTrolling.Content.Items.Weapons;
using WeDoALittleTrolling.Content.Items.Accessories;
using Microsoft.Xna.Framework;
using Terraria.Utilities;

namespace WeDoALittleTrolling.Content.Projectiles
{
    internal class GlobalProjectiles : GlobalProjectile
    {
        public override bool InstancePerEntity => false;

        public static UnifiedRandom random = new UnifiedRandom();

        public override void AI(Projectile projectile)
        {
            if (projectile.type == ProjectileID.Grenade && projectile.GetGlobalProjectile<WDALTProjectileUtil>().undeadMinerGrenade)
            {
                for (int i = 0; i < Main.player.Length; i++)
                {
                    if (Main.player[i].active && !Main.player[i].dead)
                    {
                        if (projectile.Hitbox.Intersects(Main.player[i].Hitbox))
                        {
                            projectile.Kill();
                        }
                    }
                }
            }
            if (projectile.type == ProjectileID.BoulderStaffOfEarth && projectile.GetGlobalProjectile<WDALTProjectileUtil>().hostileGolemBoulder)
            {
                if (projectile.GetGlobalProjectile<WDALTProjectileUtil>().ticksAlive < 30)
                {
                    projectile.tileCollide = false;
                }
                else
                {
                    projectile.tileCollide = true;
                }
            }
            if
            (
                (
                    projectile.type == ProjectileID.SeedPlantera ||
                    projectile.type == ProjectileID.PoisonSeedPlantera ||
                    projectile.type == ProjectileID.Fireball ||
                    projectile.type == ProjectileID.BrainScramblerBolt ||
                    projectile.type == ProjectileID.RayGunnerLaser
                ) &&
                (
                    !projectile.GetGlobalProjectile<WDALTProjectileUtil>().speedyPlanteraPoisonSeed
                )
            )
            {
                float lowest_distance = 0f;
                float correction_factor = 0f;
                switch (projectile.type)
                {
                    case ProjectileID.SeedPlantera:
                    case ProjectileID.PoisonSeedPlantera:
                        lowest_distance = 1024f;
                        correction_factor = 0.24f;
                        break;
                    case ProjectileID.Fireball:
                        lowest_distance = 1024f;
                        correction_factor = 0.16f;
                        break;
                    case ProjectileID.BrainScramblerBolt:
                        lowest_distance = 512f;
                        correction_factor = 0.64f;
                        break;
                    case ProjectileID.RayGunnerLaser:
                        lowest_distance = 512f;
                        correction_factor = 0.16f;
                        break;
                    default:
                        break;
                }
                Player target = null;
                for (int i = 0; i < Main.player.Length; i++)
                {
                    Player currentTarget = Main.player[i];
                    if (currentTarget.active)
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
                if (!success && projectile.GetGlobalProjectile<WDALTProjectileUtil>().colossalSolarWhip)
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
                projectile.tileCollide = true;
                projectile.light = 0.5f;
                projectile.netUpdate = true;
            }
            if
            (
                projectile.type == ProjectileID.GiantBee &&
                projectile.GetGlobalProjectile<WDALTProjectileUtil>().TryGetParentPlayer(out Player player) &&
                (
                    source is EntitySource_ItemUse ||
                    source is EntitySource_ItemUse_OnHurt ||
                    (source is EntitySource_Parent parentSource && parentSource.Entity is Projectile proj && proj.type == ProjectileID.BeeArrow)
                )
            )
            {
                if (player.strongBees && player.whoAmI == Main.myPlayer && random.NextBool(10))
                {
                    Projectile beenade = Projectile.NewProjectileDirect(source, projectile.Center, projectile.velocity, ProjectileID.Beenade, projectile.damage, projectile.knockBack, projectile.owner);
                    projectile.active = false;
                }
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
            if (projectile.type == ProjectileID.TrueNightsEdge)
            {
                for (int i = 0; i < 320; i++)
                {
                    int rMax = (int)Math.Round(projectile.width * 3 * projectile.scale);
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
                        int horizonalIncrease = (int)Math.Round((hitbox.Width * Colossal.scalingFactor) / (Colossal.scalingFactor * Colossal.sqrt));
                        int verticalIncrease = (int)Math.Round((hitbox.Height * Colossal.scalingFactor) / (Colossal.scalingFactor * Colossal.sqrt));
                        if (projectile.type == ProjectileID.Terragrim || projectile.type == ProjectileID.Arkhalis)
                        {
                            horizonalIncrease = (int)Math.Round(horizonalIncrease / Math.Sqrt(2));
                            verticalIncrease = (int)Math.Round(verticalIncrease / Math.Sqrt(2));
                        }
                        hitbox.Inflate(horizonalIncrease, verticalIncrease);
                    }
                    else if (Colossal.ShortswordCompatibleProjectileIDs.Contains(projectile.type))
                    {
                        int horizonalIncrease = (int)Math.Round((hitbox.Width * Colossal.scalingFactor) / (Colossal.sqrt));
                        int verticalIncrease = (int)Math.Round((hitbox.Height * Colossal.scalingFactor) / (Colossal.sqrt));
                        hitbox.Inflate(horizonalIncrease, verticalIncrease);
                    }
                }
            }
            base.ModifyDamageHitbox(projectile, ref hitbox);
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (projectile.type == ProjectileID.CoolWhipProj)
            {
                if (projectile.GetGlobalProjectile<WDALTProjectileUtil>().TryGetParentPlayer(out Player player))
                {
                    if (!player.HasBuff(BuffID.MoonLeech))
                    {
                        player.Heal(2);
                    }
                    if(!player.HasBuff(BuffID.Regeneration) || player.buffTime[player.FindBuffIndex(BuffID.Regeneration)] < 180)
                    {
                        player.AddBuff(BuffID.Regeneration, 180, true);
                    }
                }
            }
            if (projectile.type == ProjectileID.Arkhalis || projectile.type == ProjectileID.Terragrim)
            {
                if
                (
                    projectile.GetGlobalProjectile<WDALTProjectileUtil>().TryGetParentPlayer(out Player player) &&
                    !target.dontCountMe &&
                    target.CanBeChasedBy() &&
                    player.active &&
                    !player.dead
                )
                {
                    WDALTHitFreezeSystemNPC.FreezeNPCForTicks(target, 4);
                    WDALTHitFreezeSystemPlayer.FreezePlayerForTicks(player, 4);
                }
            }
            base.OnHitNPC(projectile, target, hit, damageDone);
        }

        public override void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
        {
            if (projectile.type == ProjectileID.Arkhalis || projectile.type == ProjectileID.Terragrim)
            {
                if
                (
                    projectile.GetGlobalProjectile<WDALTProjectileUtil>().TryGetParentPlayer(out Player player) &&
                    !target.active &&
                    target.dead &&
                    player.active &&
                    !player.dead
                )
                {
                    WDALTHitFreezeSystemPlayer.FreezePlayerForTicks(target, 4);
                    WDALTHitFreezeSystemPlayer.FreezePlayerForTicks(player, 4);
                }
            }
            if (projectile.type == ProjectileID.BoulderStaffOfEarth && projectile.GetGlobalProjectile<WDALTProjectileUtil>().hostileGolemBoulder)
            {
                target.AddBuff(ModContent.BuffType<SearingInferno>(), 240, true); //4s, X2 in Expert, X2.5 in Master
            }
            if (projectile.type == ProjectileID.BlackBolt)
            {
                target.AddBuff(ModContent.BuffType<OnyxBlaze>(), 240, true); //4s, X2 in Expert, X2.5 in Master
            }
            base.OnHitPlayer(projectile, target, info);
        }

        public override void ModifyHitPlayer(Projectile projectile, Player target, ref Player.HurtModifiers modifiers)
        {
            if (projectile.type == ProjectileID.Boulder && projectile.GetGlobalProjectile<WDALTProjectileUtil>().TryGetParentPlayer(out Player player))
            {
                modifiers.SourceDamage *= 0.5f;
                if (Main.expertMode)
                {
                    modifiers.SourceDamage *= 0.5f;
                }
                if (Main.masterMode)
                {
                    modifiers.SourceDamage *= (1f / 1.5f);
                }
            }
            if (projectile.type == ProjectileID.BoulderStaffOfEarth && projectile.GetGlobalProjectile<WDALTProjectileUtil>().hostileGolemBoulder)
            {
                modifiers.SourceDamage *= 0.5f;
            }
        }

        public override bool CanHitPlayer(Projectile projectile, Player target)
        {
            if
            (
                projectile.type == ProjectileID.Boulder &&
                projectile.GetGlobalProjectile<WDALTProjectileUtil>().TryGetParentPlayer(out Player player) &&
                projectile.GetGlobalProjectile<WDALTProjectileUtil>().ticksAlive < 30
            )
            {
                return false;
            }
            return base.CanHitPlayer(projectile, target);
        }
    }
}
