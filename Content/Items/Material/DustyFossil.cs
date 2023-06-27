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

using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;

namespace WeDoALittleTrolling.Content.Items.Material
{
    internal class DustyFossil : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 16;

            Item.material = true;
            Item.consumable = false;
            Item.noUseGraphic = true;

            Item.value = Item.sellPrice(copper: 0);
            Item.maxStack = Item.CommonMaxStack;

            Item.rare = ItemRarityID.Blue;
        }
    }
}
