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
        public Item parentItem;
        public bool parentItemExists;

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            spawnSource = source;
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
            base.OnSpawn(projectile, source);
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
