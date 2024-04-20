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

using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;
using WeDoALittleTrolling.Common.SkillTree;

namespace WeDoALittleTrolling.Content.Items
{
    internal class PermaSkillPointItemGive : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;

            Item.consumable = true; //Makes item Consumable

            Item.value = Item.buyPrice(silver: 10);
            Item.maxStack = 1;

            Item.autoReuse = false;
            Item.useTime = 25;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.UseSound = SoundID.Item2;

            Item.rare = ItemRarityID.Blue;
        }

        public override void UseAnimation(Player player)
        {
            if (Main.dedServ || Main.netMode == NetmodeID.Server || player.whoAmI != Main.myPlayer)
            {
                return;
            }
            if (!WDALTSkillTreeSystem.unlockedSkillPoint1)
            {
                WDALTSkillTreeSystem.unlockedSkillPoint1 = true;
                WDALTSkillTreeSystem.unlockedSkillPoints++;
            }
            base.UseAnimation(player);
        }
    }
}
