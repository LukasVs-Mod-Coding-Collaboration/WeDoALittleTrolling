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
using Terraria.Localization;
using Terraria.GameContent.ItemDropRules;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Content.Prefixes;
using WeDoALittleTrolling.Content.Items;
using WeDoALittleTrolling.Content.Items.Material;
using WeDoALittleTrolling.Content.Items.Accessories;

namespace WeDoALittleTrolling.Content.ModSystems
{
    internal class WDALTModSystem : ModSystem
    {
        
        public override void AddRecipes()
        {
            Condition ShimmerCondition = new Condition("Shimmer", GetFalse);
            
            Recipe WDALT_Terraprisma = Recipe.Create(ItemID.EmpressBlade, 1);
            WDALT_Terraprisma.AddTile(TileID.LunarCraftingStation);
            WDALT_Terraprisma.AddIngredient(ItemID.RainbowWhip, 1);
            WDALT_Terraprisma.AddIngredient(ItemID.RainbowCrystalStaff, 1);
            WDALT_Terraprisma.Register();

            Recipe WDALT_HardySaddleToHeartCrystal = Recipe.Create(ItemID.LifeCrystal, 1);
            WDALT_HardySaddleToHeartCrystal.AddIngredient(ItemID.HardySaddle, 1);
            WDALT_HardySaddleToHeartCrystal.Register();

            Recipe WDALT_ShimmerBackRodOfDiscord = Recipe.Create(ItemID.RodOfHarmony, 1);
            WDALT_ShimmerBackRodOfDiscord.AddCondition(ShimmerCondition);
            WDALT_ShimmerBackRodOfDiscord.AddIngredient(ItemID.RodofDiscord, 1);
            WDALT_ShimmerBackRodOfDiscord.Register();
        }

        public bool GetFalse()
        {
            return false;
        }

        public override void PostAddRecipes()
        {
            for(int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                //Platinum Armor

                if(recipe.TryGetResult(ItemID.PlatinumHelmet, out Item PlatinumHelmet))
                {
                    if(recipe.TryGetIngredient(ItemID.PlatinumBar, out Item ingredient))
                    {
                        ingredient.stack = 12;
                    }
                }
                if(recipe.TryGetResult(ItemID.PlatinumChainmail, out Item PlatinumChainmail))
                {
                    if(recipe.TryGetIngredient(ItemID.PlatinumBar, out Item ingredient))
                    {
                        ingredient.stack = 22;
                    }
                }
                if(recipe.TryGetResult(ItemID.PlatinumGreaves, out Item PlatinumGreaves))
                {
                    if(recipe.TryGetIngredient(ItemID.PlatinumBar, out Item ingredient))
                    {
                        ingredient.stack = 16;
                    }
                }

                //Gold Armor

                if(recipe.TryGetResult(ItemID.GoldHelmet, out Item GoldHelmet))
                {
                    if(recipe.TryGetIngredient(ItemID.GoldBar, out Item ingredient))
                    {
                        ingredient.stack = 12;
                    }
                }
                if(recipe.TryGetResult(ItemID.GoldChainmail, out Item GoldChainmail))
                {
                    if(recipe.TryGetIngredient(ItemID.GoldBar, out Item ingredient))
                    {
                        ingredient.stack = 22;
                    }
                }
                if(recipe.TryGetResult(ItemID.GoldGreaves, out Item GoldGreaves))
                {
                    if(recipe.TryGetIngredient(ItemID.GoldBar, out Item ingredient))
                    {
                        ingredient.stack = 16;
                    }
                }

                //Iron Armor

                if(recipe.TryGetResult(ItemID.IronHelmet, out Item IronHelmet))
                {
                    if(recipe.TryGetIngredient(ItemID.IronBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.IronChainmail, out Item IronChainmail))
                {
                    if(recipe.TryGetIngredient(ItemID.IronBar, out Item ingredient))
                    {
                        ingredient.stack = 20;
                    }
                }
                if(recipe.TryGetResult(ItemID.IronGreaves, out Item IronGreaves))
                {
                    if(recipe.TryGetIngredient(ItemID.IronBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }

                //Lead Armor

                if(recipe.TryGetResult(ItemID.LeadHelmet, out Item LeadHelmet))
                {
                    if(recipe.TryGetIngredient(ItemID.LeadBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.LeadChainmail, out Item LeadChainmail))
                {
                    if(recipe.TryGetIngredient(ItemID.LeadBar, out Item ingredient))
                    {
                        ingredient.stack = 20;
                    }
                }
                if(recipe.TryGetResult(ItemID.LeadGreaves, out Item LeadGreaves))
                {
                    if(recipe.TryGetIngredient(ItemID.LeadBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }

                //Silver Armor

                if(recipe.TryGetResult(ItemID.SilverHelmet, out Item SilverHelmet))
                {
                    if(recipe.TryGetIngredient(ItemID.SilverBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.SilverChainmail, out Item SilverChainmail))
                {
                    if(recipe.TryGetIngredient(ItemID.SilverBar, out Item ingredient))
                    {
                        ingredient.stack = 20;
                    }
                }
                if(recipe.TryGetResult(ItemID.SilverGreaves, out Item SilverGreaves))
                {
                    if(recipe.TryGetIngredient(ItemID.SilverBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }

                //Tungsten Armor

                if(recipe.TryGetResult(ItemID.TungstenHelmet, out Item TungstenHelmet))
                {
                    if(recipe.TryGetIngredient(ItemID.TungstenBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.TungstenChainmail, out Item TungstenChainmail))
                {
                    if(recipe.TryGetIngredient(ItemID.TungstenBar, out Item ingredient))
                    {
                        ingredient.stack = 20;
                    }
                }
                if(recipe.TryGetResult(ItemID.TungstenGreaves, out Item TungstenGreaves))
                {
                    if(recipe.TryGetIngredient(ItemID.TungstenBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }

                //Copper Armor

                if(recipe.TryGetResult(ItemID.CopperHelmet, out Item CopperHelmet))
                {
                    if(recipe.TryGetIngredient(ItemID.CopperBar, out Item ingredient))
                    {
                        ingredient.stack = 8;
                    }
                }
                if(recipe.TryGetResult(ItemID.CopperChainmail, out Item CopperChainmail))
                {
                    if(recipe.TryGetIngredient(ItemID.CopperBar, out Item ingredient))
                    {
                        ingredient.stack = 14;
                    }
                }
                if(recipe.TryGetResult(ItemID.CopperGreaves, out Item CopperGreaves))
                {
                    if(recipe.TryGetIngredient(ItemID.CopperBar, out Item ingredient))
                    {
                        ingredient.stack = 8;
                    }
                }

                //Tin Armor

                if(recipe.TryGetResult(ItemID.TinHelmet, out Item TinHelmet))
                {
                    if(recipe.TryGetIngredient(ItemID.TinBar, out Item ingredient))
                    {
                        ingredient.stack = 8;
                    }
                }
                if(recipe.TryGetResult(ItemID.TinChainmail, out Item TinChainmail))
                {
                    if(recipe.TryGetIngredient(ItemID.TinBar, out Item ingredient))
                    {
                        ingredient.stack = 14;
                    }
                }
                if(recipe.TryGetResult(ItemID.TinGreaves, out Item TinGreaves))
                {
                    if(recipe.TryGetIngredient(ItemID.TinBar, out Item ingredient))
                    {
                        ingredient.stack = 8;
                    }
                }

                //Cobalt Armor

                if(recipe.TryGetResult(ItemID.CobaltHat, out Item CobaltHat))
                {
                    if(recipe.TryGetIngredient(ItemID.CobaltBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.CobaltHelmet, out Item CobaltHelmet))
                {
                    if(recipe.TryGetIngredient(ItemID.CobaltBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.CobaltMask, out Item CobaltMask))
                {
                    if(recipe.TryGetIngredient(ItemID.CobaltBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.CobaltBreastplate, out Item CobaltBreastplate))
                {
                    if(recipe.TryGetIngredient(ItemID.CobaltBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }
                if(recipe.TryGetResult(ItemID.CobaltLeggings, out Item CobaltLeggings))
                {
                    if(recipe.TryGetIngredient(ItemID.CobaltBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }

                //Palladium Armor

                if(recipe.TryGetResult(ItemID.PalladiumHelmet, out Item PalladiumHelmet))
                {
                    if(recipe.TryGetIngredient(ItemID.PalladiumBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.PalladiumHeadgear, out Item PalladiumHeadgear))
                {
                    if(recipe.TryGetIngredient(ItemID.PalladiumBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.PalladiumMask, out Item PalladiumMask))
                {
                    if(recipe.TryGetIngredient(ItemID.PalladiumBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.PalladiumBreastplate, out Item PalladiumBreastplate))
                {
                    if(recipe.TryGetIngredient(ItemID.PalladiumBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }
                if(recipe.TryGetResult(ItemID.PalladiumLeggings, out Item PalladiumLeggings))
                {
                    if(recipe.TryGetIngredient(ItemID.PalladiumBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }

                //Mythril Armor

                if(recipe.TryGetResult(ItemID.MythrilHat, out Item MythrilHat))
                {
                    if(recipe.TryGetIngredient(ItemID.MythrilBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.MythrilHelmet, out Item MythrilHelmet))
                {
                    if(recipe.TryGetIngredient(ItemID.MythrilBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.MythrilHood, out Item MythrilMask))
                {
                    if(recipe.TryGetIngredient(ItemID.MythrilBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.MythrilChainmail, out Item MythrilChainmail))
                {
                    if(recipe.TryGetIngredient(ItemID.MythrilBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }
                if(recipe.TryGetResult(ItemID.MythrilGreaves, out Item MythrilGreaves))
                {
                    if(recipe.TryGetIngredient(ItemID.MythrilBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }

                //Orichalcum Armor

                if(recipe.TryGetResult(ItemID.OrichalcumHelmet, out Item OrichalcumHelmet))
                {
                    if(recipe.TryGetIngredient(ItemID.OrichalcumBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.OrichalcumHeadgear, out Item OrichalcumHeadgear))
                {
                    if(recipe.TryGetIngredient(ItemID.OrichalcumBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.OrichalcumMask, out Item OrichalcumMask))
                {
                    if(recipe.TryGetIngredient(ItemID.OrichalcumBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.OrichalcumBreastplate, out Item OrichalcumBreastplate))
                {
                    if(recipe.TryGetIngredient(ItemID.OrichalcumBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }
                if(recipe.TryGetResult(ItemID.OrichalcumLeggings, out Item OrichalcumLeggings))
                {
                    if(recipe.TryGetIngredient(ItemID.OrichalcumBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }

                //Adamantite Armor

                if(recipe.TryGetResult(ItemID.AdamantiteHelmet, out Item AdamantiteHelmet))
                {
                    if(recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.AdamantiteHeadgear, out Item AdamantiteHeadgear))
                {
                    if(recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.AdamantiteMask, out Item AdamantiteMask))
                {
                    if(recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.AdamantiteBreastplate, out Item AdamantiteBreastplate))
                {
                    if(recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }
                if(recipe.TryGetResult(ItemID.AdamantiteLeggings, out Item AdamantiteLeggings))
                {
                    if(recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }

                //Titanium Armor

                if(recipe.TryGetResult(ItemID.TitaniumHelmet, out Item TitaniumHelmet))
                {
                    if(recipe.TryGetIngredient(ItemID.TitaniumBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }
                if(recipe.TryGetResult(ItemID.TitaniumHeadgear, out Item TitaniumHeadgear))
                {
                    if(recipe.TryGetIngredient(ItemID.TitaniumBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }
                if(recipe.TryGetResult(ItemID.TitaniumMask, out Item TitaniumMask))
                {
                    if(recipe.TryGetIngredient(ItemID.TitaniumBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }
                if(recipe.TryGetResult(ItemID.TitaniumBreastplate, out Item TitaniumBreastplate))
                {
                    if(recipe.TryGetIngredient(ItemID.TitaniumBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }
                if(recipe.TryGetResult(ItemID.TitaniumLeggings, out Item TitaniumLeggings))
                {
                    if(recipe.TryGetIngredient(ItemID.TitaniumBar, out Item ingredient))
                    {
                        ingredient.stack = 15;
                    }
                }

                //Frost Armor

                if(recipe.TryGetResult(ItemID.FrostHelmet, out Item FrostHelmet))
                {
                    if(recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item AdamantiteBar_FrostHelmet))
                    {
                        AdamantiteBar_FrostHelmet.stack = 10;
                    }
                    if(recipe.TryGetIngredient(ItemID.TitaniumBar, out Item TitaniumBar_FrostHelmet))
                    {
                        TitaniumBar_FrostHelmet.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.FrostBreastplate, out Item FrostBreastplate))
                {
                    if(recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item AdamantiteBar_FrostBreastplate))
                    {
                        AdamantiteBar_FrostBreastplate.stack = 15;
                    }
                    if(recipe.TryGetIngredient(ItemID.TitaniumBar, out Item TitaniumBar_FrostBreastplate))
                    {
                        TitaniumBar_FrostBreastplate.stack = 15;
                    }
                }
                if(recipe.TryGetResult(ItemID.FrostLeggings, out Item FrostLeggings))
                {
                    if(recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item AdamantiteBar_FrostLeggings))
                    {
                        AdamantiteBar_FrostLeggings.stack = 10;
                    }
                    if(recipe.TryGetIngredient(ItemID.TitaniumBar, out Item TitaniumBar_FrostLeggings))
                    {
                        TitaniumBar_FrostLeggings.stack = 10;
                    }
                }

                //Forbidden Armor

                if(recipe.TryGetResult(ItemID.AncientBattleArmorHat, out Item AncientBattleArmorHat))
                {
                    if(recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item AdamantiteBar_AncientBattleArmorHat))
                    {
                        AdamantiteBar_AncientBattleArmorHat.stack = 10;
                    }
                    if(recipe.TryGetIngredient(ItemID.TitaniumBar, out Item TitaniumBar_AncientBattleArmorHat))
                    {
                        TitaniumBar_AncientBattleArmorHat.stack = 10;
                    }
                }
                if(recipe.TryGetResult(ItemID.AncientBattleArmorShirt, out Item AncientBattleArmorShirt))
                {
                    if(recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item AdamantiteBar_AncientBattleArmorShirt))
                    {
                        AdamantiteBar_AncientBattleArmorShirt.stack = 15;
                    }
                    if(recipe.TryGetIngredient(ItemID.TitaniumBar, out Item TitaniumBar_AncientBattleArmorShirt))
                    {
                        TitaniumBar_AncientBattleArmorShirt.stack = 15;
                    }
                }
                if(recipe.TryGetResult(ItemID.AncientBattleArmorPants, out Item AncientBattleArmorPants))
                {
                    if(recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item AdamantiteBar_AncientBattleArmorPants))
                    {
                        AdamantiteBar_AncientBattleArmorPants.stack = 10;
                    }
                    if(recipe.TryGetIngredient(ItemID.TitaniumBar, out Item TitaniumBar_AncientBattleArmorPants))
                    {
                        TitaniumBar_AncientBattleArmorPants.stack = 10;
                    }
                }

                //Chlorophyte Bar

                if(recipe.TryGetResult(ItemID.ChlorophyteBar, out Item ChlorophyteBar))
                {
                    if(recipe.TryGetIngredient(ItemID.ChlorophyteOre, out Item ChlorophyteOre))
                    {
                        ChlorophyteOre.stack = 3;
                    }
                }

                //Chlorophyte Bullet

                if(recipe.TryGetResult(ItemID.ChlorophyteBullet, out Item ChlorophyteBullet))
                {
                    if(recipe.TryGetIngredient(ItemID.MusketBall, out Item MusketBall))
                    {
                        MusketBall.stack = 150;
                    }
                    ChlorophyteBullet.stack = 150;
                }

                //Spooky Armor
                if(recipe.TryGetResult(ItemID.SpookyHelmet, out Item SpookyHelmet))
                {
                    recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
                }
                if(recipe.TryGetResult(ItemID.SpookyBreastplate, out Item SpookyBreastplate))
                {
                    recipe.AddIngredient(ItemID.ChlorophyteBar, 18);
                }
                if(recipe.TryGetResult(ItemID.SpookyLeggings, out Item SpookyLeggings))
                {
                    recipe.AddIngredient(ItemID.ChlorophyteBar, 15);
                }

                //Progression Changes
                if(recipe.TryGetResult(ItemID.NightmarePickaxe, out Item NightmarePickaxe))
                {
                    recipe.AddIngredient(ItemID.FossilOre, 6);
                    recipe.AddIngredient(ItemID.FlinxFur, 6);
                }
                if(recipe.TryGetResult(ItemID.DeathbringerPickaxe, out Item DeathbringerPickaxe))
                {
                    recipe.AddIngredient(ItemID.FossilOre, 6);
                    recipe.AddIngredient(ItemID.FlinxFur, 6);
                }
                if(recipe.TryGetResult(ItemID.MythrilAnvil, out Item MythrilAnvil))
                {
                    recipe.AddIngredient(ModContent.ItemType<IcyFossil>(), 10);
                    recipe.AddIngredient(ModContent.ItemType<DustyFossil>(), 10);
                }
                if(recipe.TryGetResult(ItemID.OrichalcumAnvil, out Item OrichalcumAnvil))
                {
                    recipe.AddIngredient(ModContent.ItemType<IcyFossil>(), 10);
                    recipe.AddIngredient(ModContent.ItemType<DustyFossil>(), 10);
                }
            }
        }
    }
}
