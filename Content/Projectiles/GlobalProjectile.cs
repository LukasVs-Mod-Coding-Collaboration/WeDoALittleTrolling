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
        
        public override void SetDefaults(Projectile projectile)
        {
            if(projectile.type == ProjectileID.IceBoomerang)
            {
                projectile.extraUpdates = projectile.GetGlobalProjectile<WDALTProjectileUtil>().extraUpdatesIceBoomerang;
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

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (projectile.type == ProjectileID.TrueNightsEdge)
            {
                projectile.damage *= 2;
                projectile.penetrate = 1;
            }
            if (projectile.GetGlobalProjectile<WDALTProjectileUtil>().TryGetParentHeldItem(out Item item))
            {
                if (item.prefix == ModContent.PrefixType<Colossal>())
                {
                    if (Colossal.CompatibleProjectileIDs.Contains(projectile.type) || Colossal.ShortswordCompatibleProjectileIDs.Contains(projectile.type))
                    {
                        projectile.scale *= 2;
                        if (Colossal.SpeedupProjectileIDs.Contains(projectile.type))
                        {
                            projectile.velocity *= 2;
                        }
                    }
                }
            }
            base.OnSpawn(projectile, source);
        }

        public override bool PreKill(Projectile projectile, int timeLeft)
        {
            if(projectile.type == ProjectileID.TrueNightsEdge)
            {
                for(int i = 0;i < 300*3;i++)
                {
                    Random rnd = new Random();
                    int rMax = projectile.width*3;
                    double r = rMax * Math.Sqrt(rnd.NextDouble());
                    double angle = rnd.NextDouble() * 2 * Math.PI;
                    int xOffset = (int)Math.Round(r * Math.Cos(angle));
                    int yOffset = (int)Math.Round(r * Math.Sin(angle));
                    Vector2 dustPosition = projectile.Center;
                    dustPosition.X += xOffset;
                    dustPosition.Y += yOffset;
                    int dustType = rnd.Next(0, 1);
                    switch (dustType)
                    {
                        case 0:
                            Dust newDust = Dust.NewDustPerfect(dustPosition, DustID.Terra);
                            newDust.noGravity = true;
                            newDust.noLightEmittence = true;
                            break;
                        default:
                            break;
                    }
                }
                
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
                target.AddBuff(BuffID.Poisoned, 240, false); //4s, X2 in Expert, X2.5 in Master
            }
            if(projectile.type == ProjectileID.DeathSickle)
            {
                target.AddBuff(BuffID.ShadowFlame, 240, false); //4s, X2 in Expert, X2.5 in Master
            }
            if(projectile.type == ProjectileID.IceSickle)
            {
                target.AddBuff(BuffID.Frostburn, 240, false); //4s, X2 in Expert, X2.5 in Master
            }
            if (projectile.type == ProjectileID.TrueExcalibur)
            {
                target.AddBuff(BuffID.Ichor, 240, false); //4s, X2 in Expert, X2.5 in Master
            }
            base.OnHitNPC(projectile, target, hit, damageDone);
        }

        public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
        {
            if(projectile.type == ProjectileID.FrostBlastFriendly)
            {
                Random rnd = new Random();
                if(rnd.Next(0, 100) < 50)
                {
                    modifiers.SetCrit();
                }
            }
            base.ModifyHitNPC(projectile, target, ref modifiers);
        }
    }
}
