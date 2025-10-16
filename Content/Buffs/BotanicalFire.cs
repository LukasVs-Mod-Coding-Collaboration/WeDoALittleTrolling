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
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using WeDoALittleTrolling.Content.NPCs;
using ReLogic.Content;
using WeDoALittleTrolling.Common.ModPlayers;

namespace WeDoALittleTrolling.Content.Buffs
{
    public class BotanicalFire : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = false;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen += 4;
            player.statDefense += 4;
            player.manaRegenDelayBonus += 2f;
            player.manaRegenBonus += 4;
            player.maxRunSpeed += 0.8f;
            player.moveSpeed += 0.08f;
            player.GetCritChance(DamageClass.Generic) += 4f;
            for (int i = 0; i < (int)DamageClassLoader.DamageClassCount; i++)
            {
                DamageClass dClass = DamageClassLoader.GetDamageClass(i);
                if (dClass != null && !dClass.UseStandardCritCalcs)
                {
                    player.GetDamage(dClass) += 0.04f;
                }
            }
            player.fishingSkill += 8;
            player.endurance += 0.04f;
            base.Update(player, ref buffIndex);
        }
    }
}
