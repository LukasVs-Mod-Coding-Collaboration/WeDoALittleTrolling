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
        public override bool InstancePerEntity => true;

        public const int extraUpdatesIceBoomerang = 1;
        public bool recentlyPassedIceBoomerangSoundDelay = false;
        public Entity parentEntity;
        public bool parentEntityExists;
        public NPC parentNPC;
        public bool parentNPCExists;
        public Player parentPlayer;
        public bool parentPlayerExists;
        public Item parentItem;
        public bool parentItemExists;
        
        public override void SetDefaults(Projectile projectile)
        {
            if(projectile.type == ProjectileID.IceBoomerang)
            {
                projectile.extraUpdates = extraUpdatesIceBoomerang;
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
                    if(!hasRecentlyPassedSoundDelay(ProjectileID.IceBoomerang))
                    {
                        int multiplier = (extraUpdatesIceBoomerang + 1);
                        projectile.soundDelay = (baseSoundDelay * multiplier);
                    }
                }
            }
            return base.PreAI(projectile);
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if(source is EntitySource_Parent parentSource)
            {
                parentEntity = parentSource.Entity;
                parentEntityExists = true;
            }
            else
            {
                parentEntity = null;
                parentEntityExists = false;
            }
            if(parentEntity is NPC parentEntity_npc)
            {
                parentNPC = parentEntity_npc;
                parentNPCExists = true;
            }
            else
            {
                parentNPC = null;
                parentNPCExists = false;
            }
            if(parentEntity is Player parentEntity_player)
            {
                parentPlayer = parentEntity_player;
                parentPlayerExists = true;
                if(parentEntity_player.HeldItem != null)
                {
                    parentItemExists = true;
                    parentItem = parentEntity_player.HeldItem;
                }
                else
                {
                    parentItem = null;
                    parentItemExists = false;
                }
            }
            else
            {
                parentPlayer = null;
                parentPlayerExists = false;
                parentItem = null;
                parentItemExists = false;
            }
            if
            (
                projectile.type == ProjectileID.EnchantedBeam ||
                projectile.type == ProjectileID.SwordBeam ||
                projectile.type == ProjectileID.IceSickle ||
                projectile.type == ProjectileID.DeathSickle
            )
            {
                if(TryGetParentPlayer(out Player player))
                {
                    if(player.HeldItem.prefix == ModContent.PrefixType<Colossal>())
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
                if(TryGetParentPlayer(out Player player))
                {
                    if(player.HeldItem.prefix == ModContent.PrefixType<Colossal>())
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
        
        public override void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
        {
            if(TryGetParentNPC(out NPC npc))
            {
                Random random = new Random();
                if(GlobalNPCs.InflictVenomDebuff1In1Group.Contains(npc.type))
                {
                    if(random.Next(0, 1) == 0) //1 in 1 Chance
                    {
                        target.AddBuff(BuffID.Venom, 240, false); //4s, X2 in Expert, X2.5 in Master
                    }
                }
                if(GlobalNPCs.InflictPoisonDebuff1In1Group.Contains(npc.type))
                {
                    if(random.Next(0, 1) == 0) //1 in 1 Chance
                    {
                        target.AddBuff(BuffID.Poisoned, 240, false); //4s, X2 in Expert, X2.5 in Master
                    }
                }
                if(GlobalNPCs.InflictBleedingDebuff1In1Group.Contains(npc.type))
                {
                    if(random.Next(0, 1) == 0) //1 in 1 Chance
                    {
                        target.AddBuff(BuffID.Bleeding, 960, false); //16s, X2 in Expert, X2.5 in Master
                    }
                }
                if(GlobalNPCs.InflictBleedingDebuff1In8Group.Contains(npc.type))
                {
                    if(random.Next(0, 8) == 0) //1 in 8 Chance
                    {
                        target.AddBuff(BuffID.Bleeding, 480, false); //8s, X2 in Expert, X2.5 in Master
                    }
                }
            }
            base.OnHitPlayer(projectile, target, info);
        }
        
        public override void ModifyHitPlayer(Projectile projectile, Player target, ref Player.HurtModifiers modifiers)
        {
            if(TryGetParentNPC(out NPC npc))
            {
                if(npc.HasBuff(ModContent.BuffType<SearingInferno>()))
                {
                    modifiers.SourceDamage *= SearingInferno.damageNerfMultiplier;
                }
                if(GlobalNPCs.NerfGroup25Percent.Contains(npc.type))
                {
                    modifiers.SourceDamage *= (float)0.75;
                }
                if(GlobalNPCs.NerfGroup35Percent.Contains(npc.type))
                {
                    modifiers.SourceDamage *= (float)0.65;
                }
                if(GlobalNPCs.NerfGroup50Percent.Contains(npc.type))
                {
                    modifiers.SourceDamage *= (float)0.5;
                }
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

        public bool hasRecentlyPassedSoundDelay(int projectileID)
        {
            if(projectileID == ProjectileID.IceBoomerang)
            {
                if(!recentlyPassedIceBoomerangSoundDelay)
                {
                    recentlyPassedIceBoomerangSoundDelay = true;
                    return false;
                }
                if(recentlyPassedIceBoomerangSoundDelay)
                {
                    recentlyPassedIceBoomerangSoundDelay = false;
                    return true;
                }
            }
            return false;
        }

        public bool TryGetParentEntity(out Entity entity)
        {
            entity = parentEntity;
            if(parentEntityExists)
            {
                return true;
            }
            return false;
        }

        public bool TryGetParentNPC(out NPC npc)
        {
            npc = parentNPC;
            if(parentNPCExists)
            {
                return true;
            }
            return false;
        }

        public bool TryGetParentPlayer(out Player player)
        {
            player = parentPlayer;
            if(parentPlayerExists)
            {
                return true;
            }
            return false;
        }

        public bool TryGetParentItem(out Item item)
        {
            item = parentItem;
            if(parentItemExists)
            {
                return true;
            }
            return false;
        }
    }
}
