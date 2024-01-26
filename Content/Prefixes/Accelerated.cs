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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria;
using WeDoALittleTrolling.Common.ModPlayers;
using Terraria.ID;

namespace WeDoALittleTrolling.Content.Prefixes
{
    public class Accelerated : ModPrefix
    {
        public override PrefixCategory Category => PrefixCategory.Accessory;

        public override float RollChance(Item item)
        {
            if(!NPC.downedBoss3)
            {
                return 0f;
            }
            return 0.50f;
        }

        public override bool CanRoll(Item item)
        {
            return true;
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1.15f;
        }

        public static void RegisterHooks()
        {
            On_Player.WingAirLogicTweaks += On_Player_WingAirLogicTweaks;
        }

        public static void UnregisterHooks()
        {
            On_Player.WingAirLogicTweaks -= On_Player_WingAirLogicTweaks;
        }

        public static void On_Player_WingAirLogicTweaks(On_Player.orig_WingAirLogicTweaks orig, Player self)
        {
            orig.Invoke(self);
            if (self.GetModPlayer<WDALTPlayer>().acceleratedStack > 0)
            {
                float modifierAccelerated = 0.1f * self.accRunSpeed * self.GetModPlayer<WDALTPlayer>().acceleratedStack;
                self.accRunSpeed += modifierAccelerated;
            }
        }

        public override void ApplyAccessoryEffects(Player player)
        {
            player.GetModPlayer<WDALTPlayer>().acceleratedStack++;
        }

        public LocalizedText AdditionalTooltip => this.GetLocalization(nameof(AdditionalTooltip));

        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            yield return new TooltipLine(Mod, "PrefixAccessoryAcceleratedDescription", AdditionalTooltip.Value)
            {
                IsModifier = true,
            };
        }

        public override void SetStaticDefaults()
        {
            _ = AdditionalTooltip;
        }

    }

}
