/*
    WeDoALittleTrolling is a Terraria Mod made with tModLoader.
    Copyright (C) 2022-2026 LukasV-Coding

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
using System;
using WeDoALittleTrolling.Common.ModPlayers;
using MonoMod.Cil;
using Mono.Cecil.Cil;

namespace WeDoALittleTrolling.Content.Items.Accessories
{
    internal class ManaExtractionCatalyst : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 34;

            Item.consumable = false;

            Item.value = Item.buyPrice(gold: 5);
            

            Item.rare = ItemRarityID.LightPurple;

            Item.accessory = true; //Item is an accessory
        }

        public static void RegisterHooks()
        {
            IL_Player.PickupItem += IL_Player_PickupItem;
        }

        public static void UnregisterHooks()
        {
            IL_Player.PickupItem -= IL_Player_PickupItem;
        }

        public static void IL_Player_PickupItem(ILContext intermediateLanguageContext)
        {
            bool successInjectCatalystHook = true;
            try
            {
                ILCursor cursor = new ILCursor(intermediateLanguageContext);
                cursor.GotoNext(i => i.MatchLdfld<Player>(nameof(Player.statMana))); //Go to the mana heal instruction.
                cursor.Index += 2; //Go after it now.
                cursor.Emit(OpCodes.Ldarg_0); //Push the current NPC index (variable i) onto the stack.
                cursor.EmitDelegate<Func<int, Player, int>> //Emit a function that accepts the current NPC index and returns the new value for "specialHitSetter".
                (
                    (
                        int amount,
                        Player player
                    ) =>
                    {
                        int ret = amount;
                        if (player.TryGetModPlayer<WDALTPlayer>(out WDALTPlayer plr) && plr.manaCatalyst)
                        {
                            if (player.statMana <= amount)
                            {
                                ret = (player.statMana - 1) * (-1);
                            }
                            else
                            {
                                ret = (amount) * (-1);
                            }
                        }
                        return ret;
                    }
                );
                cursor.GotoNext(i => i.MatchCall<Player>(nameof(Player.ManaEffect))); //Go to the mana effect call
                cursor.Remove(); //Remove vanilla mana effect
                cursor.EmitDelegate<Action<Player, int>> //Emit a function that accepts the current NPC index and returns the new value for "specialHitSetter".
                (
                    (
                        Player player,
                        int manaAmount
                    ) =>
                    {
                        if (player.TryGetModPlayer<WDALTPlayer>(out WDALTPlayer plr) && plr.manaCatalyst)
                        {
                            CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), Color.DarkViolet, manaAmount);
                        }
                        else
                        {
                            player.ManaEffect(manaAmount);
                        }
                    }
                );
                cursor.GotoNext(i => i.MatchLdfld<Player>(nameof(Player.statMana))); //Go to the mana check instruction.
                cursor.Index += 2; //Go after it now.
                cursor.GotoNext(i => i.MatchLdfld<Player>(nameof(Player.statMana))); //Go to the second mana heal instruction.
                cursor.Index += 2; //Go after it now.
                cursor.Emit(OpCodes.Ldarg_0); //Push the current NPC index (variable i) onto the stack.
                cursor.EmitDelegate<Func<int, Player, int>> //Emit a function that accepts the current NPC index and returns the new value for "specialHitSetter".
                (
                    (
                        int amount,
                        Player player
                    ) =>
                    {
                        int ret = amount;
                        if (player.TryGetModPlayer<WDALTPlayer>(out WDALTPlayer plr) && plr.manaCatalyst)
                        {
                            if (player.statMana <= amount)
                            {
                                ret = (player.statMana - 1) * (-1);
                            }
                            else
                            {
                                ret = (amount) * (-1);
                            }
                        }
                        return ret;
                    }
                );
                cursor.GotoNext(i => i.MatchCall<Player>(nameof(Player.ManaEffect))); //Go to the mana effect call
                cursor.Remove(); //Remove vanilla mana effect
                cursor.EmitDelegate<Action<Player, int>> //Emit a function that accepts the current NPC index and returns the new value for "specialHitSetter".
                (
                    (
                        Player player,
                        int manaAmount
                    ) =>
                    {
                        if (player.TryGetModPlayer<WDALTPlayer>(out WDALTPlayer plr) && plr.manaCatalyst)
                        {
                            CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), Color.DarkViolet, manaAmount);
                        }
                        else
                        {
                            player.ManaEffect(manaAmount);
                        }
                    }
                );
            }
            catch
            {
                MonoModHooks.DumpIL(ModContent.GetInstance<WeDoALittleTrolling>(), intermediateLanguageContext);
                WeDoALittleTrolling.logger.Fatal("WDALT: Failed to inject Catalyst Hook. Broken IL Code has been dumped to tModLoader-Logs/ILDumps/WeDoALittleTrolling.");
                successInjectCatalystHook = false;
            }
            if(successInjectCatalystHook)
            {
                WeDoALittleTrolling.logger.Debug("WDALT: Successfully injected Catalyst Hook via IL Editing.");
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if(player.statLifeMax2 > 50)
            {
                player.statLifeMax2 += -50; //Weird but working way to set Max down (originally 125)
            }
            else
            {
                player.statLifeMax2 = 1;
            }
            player.buffImmune[BuffID.ManaSickness] = true; //Immunity to Mana Sickness
            player.manaFlower = true;
            if(player.TryGetModPlayer<WDALTPlayer>(out WDALTPlayer plr))
            {
                plr.manaCatalyst = true;
            }

        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if
            (
                equippedItem.type == ModContent.ItemType<SorcerousMirror>() ||
                incomingItem.type == ModContent.ItemType<SorcerousMirror>()
            )
            {
                return false;
            }
            else
            {
                return base.CanAccessoryBeEquippedWith(equippedItem, incomingItem,player);
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe() //Recipe
              .AddTile(TileID.MythrilAnvil)
              .AddIngredient(ItemID.ManaFlower, 1)
              .AddIngredient(ItemID.SoulofMight, 5)
              .AddIngredient(ItemID.SoulofNight, 5)
              .Register();
        }
    }
}
