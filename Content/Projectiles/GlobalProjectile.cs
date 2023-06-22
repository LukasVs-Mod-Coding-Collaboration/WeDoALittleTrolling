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
using WeDoALittleTrolling.Content.Prefixes;
using WeDoALittleTrolling.Content.Items;
using WeDoALittleTrolling.Content.Items.Accessories;
using Microsoft.Xna.Framework;

namespace WeDoALittleTrolling.Content.Projectiles
{
    internal class GlobalProjectiles : GlobalProjectile
    {

        public override void SetDefaults(Projectile projectile)
        {
            if(projectile.type == ProjectileID.IceBoomerang)
            {
                projectile.extraUpdates = WDALTPlayerUtil.extraUpdatesIceBoomerang;
            }
            base.SetDefaults(projectile);
        }
        
        public override bool PreAI(Projectile projectile)
        {
            if(projectile.type == ProjectileID.IceBoomerang)
            {
                int baseSoundDelay = 8;
                if(projectile.TryGetOwner(out Player player))
                {
                    if(projectile.soundDelay == (baseSoundDelay - 1))
                    {
                        if(!player.GetModPlayer<WDALTPlayerUtil>().hasRecentlyPassedSoundDelay(ProjectileID.IceBoomerang))
                        {
                            int multiplier = (player.GetModPlayer<WDALTPlayerUtil>().getExtraUpdates(ProjectileID.IceBoomerang) + 1);
                            projectile.soundDelay = (baseSoundDelay * multiplier);
                        }
                    }
                }
            }
            return base.PreAI(projectile);
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if
            (
                projectile.type == ProjectileID.EnchantedBeam ||
                projectile.type == ProjectileID.SwordBeam ||
                projectile.type == ProjectileID.IceSickle ||
                projectile.type == ProjectileID.DeathSickle
            )
            {
                if(projectile.TryGetOwner(out Player player))
                {
                    if(player.GetModPlayer<WDALTPlayerUtil>().isPlayerHoldingItemWithPrefix(ModContent.PrefixType<Colossal>()))
                    {
                        projectile.scale *= 2;
                        projectile.velocity *= 2;
                    }
                }
            }
            base.OnSpawn(projectile, source);
        }

        public override void ModifyDamageHitbox(Projectile projectile, ref Rectangle hitbox)
        {
            if
            (
                projectile.type == ProjectileID.EnchantedBeam ||
                projectile.type == ProjectileID.SwordBeam ||
                projectile.type == ProjectileID.IceSickle ||
                projectile.type == ProjectileID.DeathSickle
            )
            {
                if(projectile.TryGetOwner(out Player player))
                {
                    if(player.GetModPlayer<WDALTPlayerUtil>().isPlayerHoldingItemWithPrefix(ModContent.PrefixType<Colossal>()))
                    {
                        int scalingFactor = 2;
                        int horizonalIncrease  = (hitbox.Width * scalingFactor) / (2*2);
                        int verticalIncrease = (hitbox.Height * scalingFactor) / (2*2);
                        hitbox.Inflate(horizonalIncrease, verticalIncrease);
                    }
                }
            }
            base.ModifyDamageHitbox(projectile, ref hitbox);
        }

        public override void ModifyHitPlayer(Projectile projectile, Player target, ref Player.HurtModifiers modifiers)
        {
            int[] NerfGroup25Percent =
            {
                ProjectileID.SandBallFalling,
                ProjectileID.Stinger
            };
            int[] NerfGroup50Percent =
            {
                ProjectileID.UnholyTridentHostile
            };
            if(NerfGroup25Percent.Contains(projectile.type))
            {
                modifiers.SourceDamage *= (float)0.75;
            }
            if(NerfGroup50Percent.Contains(projectile.type))
            {
                modifiers.SourceDamage *= (float)0.5;
            }
            base.ModifyHitPlayer(projectile, target, ref modifiers);
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
