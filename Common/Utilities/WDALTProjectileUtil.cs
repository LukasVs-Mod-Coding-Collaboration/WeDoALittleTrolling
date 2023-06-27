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

namespace WeDoALittleTrolling.Common.Utilities
{
    internal class WDALTProjectileUtil : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public readonly int extraUpdatesIceBoomerang = 1;
        public bool recentlyPassedIceBoomerangSoundDelay = false;
        public IEntitySource spawnSource;
        public Entity parentEntity;
        public bool parentEntityExists;
        public NPC parentNPC;
        public bool parentNPCExists;
        public Player parentPlayer;
        public bool parentPlayerExists;
        public Item parentHeldItem;
        public bool parentHeldItemExists;
        public int ammoItemId;
        public bool ammoItemIdExists;
        public Vector2 spawnPosition;
        public Vector2 spawnCenter;
        public int ticksAlive;

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            spawnCenter = projectile.Center;
            spawnPosition = projectile.position;
            ticksAlive = 0;
            spawnSource = source;
            if(source is EntitySource_ItemUse_WithAmmo itemSourceWithAmmo)
            {
                parentEntity = itemSourceWithAmmo.Entity;
                parentEntityExists = true;
                parentPlayer = itemSourceWithAmmo.Player;
                parentPlayerExists = true;
                parentHeldItem = itemSourceWithAmmo.Item;
                parentHeldItemExists = true;
                ammoItemId = itemSourceWithAmmo.AmmoItemIdUsed;
                ammoItemIdExists = true;
            }
            else
            {
                ammoItemId = 0;
                ammoItemIdExists = false;
                if (source is EntitySource_Parent parentSource)
                {
                    parentEntity = parentSource.Entity;
                    parentEntityExists = true;
                }
                else
                {
                    parentEntity = null;
                    parentEntityExists = false;
                }
                if (TryGetParentEntity(out Entity entity))
                {
                    ProcessParentSource(entity);
                }
            }
            base.OnSpawn(projectile, source);
        }

        public override bool ShouldUpdatePosition(Projectile projectile)
        {
            ticksAlive += 1;
            return base.ShouldUpdatePosition(projectile);
        }

        
        public void ProcessParentSource(Entity entity)
        {
            if (entity is NPC parentEntity_npc)
            {
                parentNPC = parentEntity_npc;
                parentNPCExists = true;
            }
            else
            {
                parentNPC = null;
                parentNPCExists = false;
            }
            if (entity is Player parentEntity_player)
            {
                parentPlayer = parentEntity_player;
                parentPlayerExists = true;
                if (parentEntity_player.HeldItem != null)
                {
                    parentHeldItemExists = true;
                    parentHeldItem = parentEntity_player.HeldItem;
                }
                else
                {
                    parentHeldItem = null;
                    parentHeldItemExists = false;
                }
            }
            else
            {
                parentPlayer = null;
                parentPlayerExists = false;
                parentHeldItem = null;
                parentHeldItemExists = false;
            }
            if (entity is Projectile parentEntity_projectile)
            {
                if(parentEntity_projectile.GetGlobalProjectile<WDALTProjectileUtil>().TryGetParentEntity(out Entity newEntity))
                {
                    ProcessParentSource(newEntity);
                }
            }
        }

        public override void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
        {
            if(TryGetParentNPC(out NPC npc))
            {
                GlobalNPCs.OnHitPlayerWithProjectile(projectile, npc, target, info);
            }
            base.OnHitPlayer(projectile, target, info);
        }
        
        public override void ModifyHitPlayer(Projectile projectile, Player target, ref Player.HurtModifiers modifiers)
        {
            if(TryGetParentNPC(out NPC npc))
            {
                GlobalNPCs.ModifyHitPlayerWithProjectile(projectile, npc, target, ref modifiers);
            }
            base.ModifyHitPlayer(projectile, target, ref modifiers);
        }

        public bool HasRecentlyPassedSoundDelay(int projectileID)
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

        public bool TryGetParentHeldItem(out Item item)
        {
            item = parentHeldItem;
            if(parentHeldItemExists)
            {
                return true;
            }
            return false;
        }

        public bool TryGetAmmoItemID(out int itemID)
        {
            itemID = ammoItemId;
            if(ammoItemIdExists)
            {
                return true;
            }
            return false;
        }
    }
}
