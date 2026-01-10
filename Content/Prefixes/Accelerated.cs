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
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria;
using WeDoALittleTrolling.Common.ModPlayers;
using Terraria.ID;
using Terraria.DataStructures;
using WeDoALittleTrolling.Common.Configs;
using Humanizer;

namespace WeDoALittleTrolling.Content.Prefixes
{
    public class Accelerated : ModPrefix
    {
        public override PrefixCategory Category => PrefixCategory.Accessory;

        public override float RollChance(Item item)
        {
            if(!NPC.downedBoss3 && !Main.getGoodWorld)
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
            valueMult = 1.3f;
        }

        public static void RegisterHooks()
        {
            On_Player.WingAirLogicTweaks += On_Player_WingAirLogicTweaks;
            On_Player.HorizontalMovement += On_Player_HorizontalMovement;
        }

        public static void UnregisterHooks()
        {
            On_Player.WingAirLogicTweaks -= On_Player_WingAirLogicTweaks;
            On_Player.HorizontalMovement -= On_Player_HorizontalMovement;
        }

        public static void On_Player_HorizontalMovement(On_Player.orig_HorizontalMovement orig, Player self)
        {
            if (!ModContent.GetInstance<WDALTServerConfig>().DisableFishronSkipNerf && self.mount != null && self.mount.Active && self.mount.Type == MountID.CuteFishron)
            {
                float factor = 1f;
                if
                (
                    !NPC.downedMechBoss1 ||
                    !NPC.downedMechBoss2 ||
                    !NPC.downedMechBoss3
                )
                {
                    factor -= 0.15f;
                }
                if (!NPC.downedPlantBoss)
                {
                    factor -= 0.15f;
                }
                if (!NPC.downedGolemBoss)
                {
                    factor -= 0.15f;
                }
                if (factor < 0.95f)
                {
                    self.accRunSpeed *= factor;
                    self.maxRunSpeed *= factor;
                    self.runAcceleration  *= factor;
                }
            }
            orig.Invoke(self);
        }

        public static void On_Player_WingAirLogicTweaks(On_Player.orig_WingAirLogicTweaks orig, Player self)
        {
            orig.Invoke(self);
            if (self.GetModPlayer<WDALTPlayer>().acceleratedStack > 0)
            {
                float factor = 0.08f;
                float modifierAccelerated = factor * self.accRunSpeed * self.GetModPlayer<WDALTPlayer>().acceleratedStack;
                self.accRunSpeed += modifierAccelerated;
            }
            if (!ModContent.GetInstance<WDALTServerConfig>().DisableFishronSkipNerf && self.wingsLogic == ArmorIDs.Wing.FishronWings)
            {
                float factor = 1f;
                if
                (
                    !NPC.downedMechBoss1 ||
                    !NPC.downedMechBoss2 ||
                    !NPC.downedMechBoss3
                )
                {
                    factor -= 0.15f;
                }
                if (!NPC.downedPlantBoss)
                {
                    factor -= 0.15f;
                }
                if (!NPC.downedGolemBoss)
                {
                    factor -= 0.15f;
                }
                if (factor < 0.95f)
                {
                    self.accRunSpeed *= factor;
                }
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
