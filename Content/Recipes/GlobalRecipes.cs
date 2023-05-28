using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Content.Prefixes;
using WeDoALittleTrolling.Content.Items;
using WeDoALittleTrolling.Content.Items.Accessories;

namespace WeDoALittleTrolling.Content.Recipes
{
    internal class GlobalRecipes : ModSystem
    {

        public override void AddRecipes()
        {
            /*
            Recipe WDALT_SharkToothNecklace = Recipe.Create(ItemID.SharkToothNecklace, 1);
            WDALT_SharkToothNecklace.AddTile(TileID.TinkerersWorkbench);
            WDALT_SharkToothNecklace.AddIngredient(ItemID.Shackle, 1);
            WDALT_SharkToothNecklace.AddRecipeGroup(RecipeGroupID.IronBar, 10);
            WDALT_SharkToothNecklace.Register();
            */

            Recipe WDALT_HardySaddleToHeartCrystal = Recipe.Create(ItemID.LifeCrystal, 1);
            WDALT_HardySaddleToHeartCrystal.AddIngredient(ItemID.HardySaddle, 1);
            WDALT_HardySaddleToHeartCrystal.Register();
            
        }

        public override void PostAddRecipes()
        {
            for(int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                //Platinum Armor

                if(recipe.TryGetResult(ItemID.PlatinumHelmet, out Item PlatinumHelmet))
                {
                    recipe.RemoveIngredient(ItemID.PlatinumBar);
                    recipe.AddIngredient(ItemID.PlatinumBar, 12);
                }
                if(recipe.TryGetResult(ItemID.PlatinumChainmail, out Item PlatinumChainmail))
                {
                    recipe.RemoveIngredient(ItemID.PlatinumBar);
                    recipe.AddIngredient(ItemID.PlatinumBar, 22);
                }
                if(recipe.TryGetResult(ItemID.PlatinumGreaves, out Item PlatinumGreaves))
                {
                    recipe.RemoveIngredient(ItemID.PlatinumBar);
                    recipe.AddIngredient(ItemID.PlatinumBar, 16);
                }

                //Gold Armor

                if(recipe.TryGetResult(ItemID.GoldHelmet, out Item GoldHelmet))
                {
                    recipe.RemoveIngredient(ItemID.GoldBar);
                    recipe.AddIngredient(ItemID.GoldBar, 12);
                }
                if(recipe.TryGetResult(ItemID.GoldChainmail, out Item GoldChainmail))
                {
                    recipe.RemoveIngredient(ItemID.GoldBar);
                    recipe.AddIngredient(ItemID.GoldBar, 22);
                }
                if(recipe.TryGetResult(ItemID.GoldGreaves, out Item GoldGreaves))
                {
                    recipe.RemoveIngredient(ItemID.GoldBar);
                    recipe.AddIngredient(ItemID.GoldBar, 16);
                }

                //Iron Armor

                if(recipe.TryGetResult(ItemID.IronHelmet, out Item IronHelmet))
                {
                    recipe.RemoveIngredient(ItemID.IronBar);
                    recipe.AddIngredient(ItemID.IronBar, 10);
                }
                if(recipe.TryGetResult(ItemID.IronChainmail, out Item IronChainmail))
                {
                    recipe.RemoveIngredient(ItemID.IronBar);
                    recipe.AddIngredient(ItemID.IronBar, 20);
                }
                if(recipe.TryGetResult(ItemID.IronGreaves, out Item IronGreaves))
                {
                    recipe.RemoveIngredient(ItemID.IronBar);
                    recipe.AddIngredient(ItemID.IronBar, 10);
                }

                //Lead Armor

                if(recipe.TryGetResult(ItemID.LeadHelmet, out Item LeadHelmet))
                {
                    recipe.RemoveIngredient(ItemID.LeadBar);
                    recipe.AddIngredient(ItemID.LeadBar, 10);
                }
                if(recipe.TryGetResult(ItemID.LeadChainmail, out Item LeadChainmail))
                {
                    recipe.RemoveIngredient(ItemID.LeadBar);
                    recipe.AddIngredient(ItemID.LeadBar, 20);
                }
                if(recipe.TryGetResult(ItemID.LeadGreaves, out Item LeadGreaves))
                {
                    recipe.RemoveIngredient(ItemID.LeadBar);
                    recipe.AddIngredient(ItemID.LeadBar, 10);
                }

                //Silver Armor

                if(recipe.TryGetResult(ItemID.SilverHelmet, out Item SilverHelmet))
                {
                    recipe.RemoveIngredient(ItemID.SilverBar);
                    recipe.AddIngredient(ItemID.SilverBar, 10);
                }
                if(recipe.TryGetResult(ItemID.SilverChainmail, out Item SilverChainmail))
                {
                    recipe.RemoveIngredient(ItemID.SilverBar);
                    recipe.AddIngredient(ItemID.SilverBar, 20);
                }
                if(recipe.TryGetResult(ItemID.SilverGreaves, out Item SilverGreaves))
                {
                    recipe.RemoveIngredient(ItemID.SilverBar);
                    recipe.AddIngredient(ItemID.SilverBar, 10);
                }

                //Tungsten Armor

                if(recipe.TryGetResult(ItemID.TungstenHelmet, out Item TungstenHelmet))
                {
                    recipe.RemoveIngredient(ItemID.TungstenBar);
                    recipe.AddIngredient(ItemID.TungstenBar, 10);
                }
                if(recipe.TryGetResult(ItemID.TungstenChainmail, out Item TungstenChainmail))
                {
                    recipe.RemoveIngredient(ItemID.TungstenBar);
                    recipe.AddIngredient(ItemID.TungstenBar, 20);
                }
                if(recipe.TryGetResult(ItemID.TungstenGreaves, out Item TungstenGreaves))
                {
                    recipe.RemoveIngredient(ItemID.TungstenBar);
                    recipe.AddIngredient(ItemID.TungstenBar, 10);
                }

                //Copper Armor

                if(recipe.TryGetResult(ItemID.CopperHelmet, out Item CopperHelmet))
                {
                    recipe.RemoveIngredient(ItemID.CopperBar);
                    recipe.AddIngredient(ItemID.CopperBar, 8);
                }
                if(recipe.TryGetResult(ItemID.CopperChainmail, out Item CopperChainmail))
                {
                    recipe.RemoveIngredient(ItemID.CopperBar);
                    recipe.AddIngredient(ItemID.CopperBar, 14);
                }
                if(recipe.TryGetResult(ItemID.CopperGreaves, out Item CopperGreaves))
                {
                    recipe.RemoveIngredient(ItemID.CopperBar);
                    recipe.AddIngredient(ItemID.CopperBar, 8);
                }

                //Tin Armor

                if(recipe.TryGetResult(ItemID.TinHelmet, out Item TinHelmet))
                {
                    recipe.RemoveIngredient(ItemID.TinBar);
                    recipe.AddIngredient(ItemID.TinBar, 8);
                }
                if(recipe.TryGetResult(ItemID.TinChainmail, out Item TinChainmail))
                {
                    recipe.RemoveIngredient(ItemID.TinBar);
                    recipe.AddIngredient(ItemID.TinBar, 14);
                }
                if(recipe.TryGetResult(ItemID.TinGreaves, out Item TinGreaves))
                {
                    recipe.RemoveIngredient(ItemID.TinBar);
                    recipe.AddIngredient(ItemID.TinBar, 8);
                }

                //Cobalt Armor

                if(recipe.TryGetResult(ItemID.CobaltHat, out Item CobaltHat))
                {
                    recipe.RemoveIngredient(ItemID.CobaltBar);
                    recipe.AddIngredient(ItemID.CobaltBar, 10);
                }
                if(recipe.TryGetResult(ItemID.CobaltHelmet, out Item CobaltHelmet))
                {
                    recipe.RemoveIngredient(ItemID.CobaltBar);
                    recipe.AddIngredient(ItemID.CobaltBar, 10);
                }
                if(recipe.TryGetResult(ItemID.CobaltMask, out Item CobaltMask))
                {
                    recipe.RemoveIngredient(ItemID.CobaltBar);
                    recipe.AddIngredient(ItemID.CobaltBar, 10);
                }
                if(recipe.TryGetResult(ItemID.CobaltBreastplate, out Item CobaltBreastplate))
                {
                    recipe.RemoveIngredient(ItemID.CobaltBar);
                    recipe.AddIngredient(ItemID.CobaltBar, 15);
                }
                if(recipe.TryGetResult(ItemID.CobaltLeggings, out Item CobaltLeggings))
                {
                    recipe.RemoveIngredient(ItemID.CobaltBar);
                    recipe.AddIngredient(ItemID.CobaltBar, 10);
                }

                //Palladium Armor

                if(recipe.TryGetResult(ItemID.PalladiumHelmet, out Item PalladiumHelmet))
                {
                    recipe.RemoveIngredient(ItemID.PalladiumBar);
                    recipe.AddIngredient(ItemID.PalladiumBar, 10);
                }
                if(recipe.TryGetResult(ItemID.PalladiumHeadgear, out Item PalladiumHeadgear))
                {
                    recipe.RemoveIngredient(ItemID.PalladiumBar);
                    recipe.AddIngredient(ItemID.PalladiumBar, 10);
                }
                if(recipe.TryGetResult(ItemID.PalladiumMask, out Item PalladiumMask))
                {
                    recipe.RemoveIngredient(ItemID.PalladiumBar);
                    recipe.AddIngredient(ItemID.PalladiumBar, 10);
                }
                if(recipe.TryGetResult(ItemID.PalladiumBreastplate, out Item PalladiumBreastplate))
                {
                    recipe.RemoveIngredient(ItemID.PalladiumBar);
                    recipe.AddIngredient(ItemID.PalladiumBar, 15);
                }
                if(recipe.TryGetResult(ItemID.PalladiumLeggings, out Item PalladiumLeggings))
                {
                    recipe.RemoveIngredient(ItemID.PalladiumBar);
                    recipe.AddIngredient(ItemID.PalladiumBar, 15);
                }

                //Mythril Armor

                if(recipe.TryGetResult(ItemID.MythrilHat, out Item MythrilHat))
                {
                    recipe.RemoveIngredient(ItemID.MythrilBar);
                    recipe.AddIngredient(ItemID.MythrilBar, 10);
                }
                if(recipe.TryGetResult(ItemID.MythrilHelmet, out Item MythrilHelmet))
                {
                    recipe.RemoveIngredient(ItemID.MythrilBar);
                    recipe.AddIngredient(ItemID.MythrilBar, 10);
                }
                if(recipe.TryGetResult(ItemID.MythrilHood, out Item MythrilMask))
                {
                    recipe.RemoveIngredient(ItemID.MythrilBar);
                    recipe.AddIngredient(ItemID.MythrilBar, 10);
                }
                if(recipe.TryGetResult(ItemID.MythrilChainmail, out Item MythrilChainmail))
                {
                    recipe.RemoveIngredient(ItemID.MythrilBar);
                    recipe.AddIngredient(ItemID.MythrilBar, 15);
                }
                if(recipe.TryGetResult(ItemID.MythrilGreaves, out Item MythrilGreaves))
                {
                    recipe.RemoveIngredient(ItemID.MythrilBar);
                    recipe.AddIngredient(ItemID.MythrilBar, 10);
                }

                //Orichalcum Armor

                if(recipe.TryGetResult(ItemID.OrichalcumHelmet, out Item OrichalcumHelmet))
                {
                    recipe.RemoveIngredient(ItemID.OrichalcumBar);
                    recipe.AddIngredient(ItemID.OrichalcumBar, 10);
                }
                if(recipe.TryGetResult(ItemID.OrichalcumHeadgear, out Item OrichalcumHeadgear))
                {
                    recipe.RemoveIngredient(ItemID.OrichalcumBar);
                    recipe.AddIngredient(ItemID.OrichalcumBar, 10);
                }
                if(recipe.TryGetResult(ItemID.OrichalcumMask, out Item OrichalcumMask))
                {
                    recipe.RemoveIngredient(ItemID.OrichalcumBar);
                    recipe.AddIngredient(ItemID.OrichalcumBar, 10);
                }
                if(recipe.TryGetResult(ItemID.OrichalcumBreastplate, out Item OrichalcumBreastplate))
                {
                    recipe.RemoveIngredient(ItemID.OrichalcumBar);
                    recipe.AddIngredient(ItemID.OrichalcumBar, 15);
                }
                if(recipe.TryGetResult(ItemID.OrichalcumLeggings, out Item OrichalcumLeggings))
                {
                    recipe.RemoveIngredient(ItemID.OrichalcumBar);
                    recipe.AddIngredient(ItemID.OrichalcumBar, 15);
                }

                //Adamantite Armor

                if(recipe.TryGetResult(ItemID.AdamantiteHelmet, out Item AdamantiteHelmet))
                {
                    recipe.RemoveIngredient(ItemID.AdamantiteBar);
                    recipe.AddIngredient(ItemID.AdamantiteBar, 10);
                }
                if(recipe.TryGetResult(ItemID.AdamantiteHeadgear, out Item AdamantiteHeadgear))
                {
                    recipe.RemoveIngredient(ItemID.AdamantiteBar);
                    recipe.AddIngredient(ItemID.AdamantiteBar, 10);
                }
                if(recipe.TryGetResult(ItemID.AdamantiteMask, out Item AdamantiteMask))
                {
                    recipe.RemoveIngredient(ItemID.AdamantiteBar);
                    recipe.AddIngredient(ItemID.AdamantiteBar, 10);
                }
                if(recipe.TryGetResult(ItemID.AdamantiteBreastplate, out Item AdamantiteBreastplate))
                {
                    recipe.RemoveIngredient(ItemID.AdamantiteBar);
                    recipe.AddIngredient(ItemID.AdamantiteBar, 15);
                }
                if(recipe.TryGetResult(ItemID.AdamantiteLeggings, out Item AdamantiteLeggings))
                {
                    recipe.RemoveIngredient(ItemID.AdamantiteBar);
                    recipe.AddIngredient(ItemID.AdamantiteBar, 15);
                }

                //Titanium Armor

                if(recipe.TryGetResult(ItemID.TitaniumHelmet, out Item TitaniumHelmet))
                {
                    recipe.RemoveIngredient(ItemID.TitaniumBar);
                    recipe.AddIngredient(ItemID.TitaniumBar, 15);
                }
                if(recipe.TryGetResult(ItemID.TitaniumHeadgear, out Item TitaniumHeadgear))
                {
                    recipe.RemoveIngredient(ItemID.TitaniumBar);
                    recipe.AddIngredient(ItemID.TitaniumBar, 15);
                }
                if(recipe.TryGetResult(ItemID.TitaniumMask, out Item TitaniumMask))
                {
                    recipe.RemoveIngredient(ItemID.TitaniumBar);
                    recipe.AddIngredient(ItemID.TitaniumBar, 15);
                }
                if(recipe.TryGetResult(ItemID.TitaniumBreastplate, out Item TitaniumBreastplate))
                {
                    recipe.RemoveIngredient(ItemID.TitaniumBar);
                    recipe.AddIngredient(ItemID.TitaniumBar, 15);
                }
                if(recipe.TryGetResult(ItemID.TitaniumLeggings, out Item TitaniumLeggings))
                {
                    recipe.RemoveIngredient(ItemID.TitaniumBar);
                    recipe.AddIngredient(ItemID.TitaniumBar, 15);
                }

                //Frost Armor

                if(recipe.TryGetResult(ItemID.FrostHelmet, out Item FrostHelmet))
                {
                    if(recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item AdamantiteBar_FrostHelmet))
                    {
                        recipe.RemoveIngredient(ItemID.AdamantiteBar);
                        recipe.AddIngredient(ItemID.AdamantiteBar, 10);
                    }
                    if(recipe.TryGetIngredient(ItemID.TitaniumBar, out Item TitaniumBar_FrostHelmet))
                    {
                        recipe.RemoveIngredient(ItemID.TitaniumBar);
                        recipe.AddIngredient(ItemID.TitaniumBar, 10);
                    }
                }
                if(recipe.TryGetResult(ItemID.FrostBreastplate, out Item FrostBreastplate))
                {
                    if(recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item AdamantiteBar_FrostBreastplate))
                    {
                        recipe.RemoveIngredient(ItemID.AdamantiteBar);
                        recipe.AddIngredient(ItemID.AdamantiteBar, 15);
                    }
                    if(recipe.TryGetIngredient(ItemID.TitaniumBar, out Item TitaniumBar_FrostBreastplate))
                    {
                        recipe.RemoveIngredient(ItemID.TitaniumBar);
                        recipe.AddIngredient(ItemID.TitaniumBar, 15);
                    }
                }
                if(recipe.TryGetResult(ItemID.FrostLeggings, out Item FrostLeggings))
                {
                    if(recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item AdamantiteBar_FrostLeggings))
                    {
                        recipe.RemoveIngredient(ItemID.AdamantiteBar);
                        recipe.AddIngredient(ItemID.AdamantiteBar, 10);
                    }
                    if(recipe.TryGetIngredient(ItemID.TitaniumBar, out Item TitaniumBar_FrostLeggings))
                    {
                        recipe.RemoveIngredient(ItemID.TitaniumBar);
                        recipe.AddIngredient(ItemID.TitaniumBar, 10);
                    }
                }

                //Forbidden Armor

                if(recipe.TryGetResult(ItemID.AncientBattleArmorHat, out Item AncientBattleArmorHat))
                {
                    if(recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item AdamantiteBar_AncientBattleArmorHat))
                    {
                        recipe.RemoveIngredient(ItemID.AdamantiteBar);
                        recipe.AddIngredient(ItemID.AdamantiteBar, 10);
                    }
                    if(recipe.TryGetIngredient(ItemID.TitaniumBar, out Item TitaniumBar_AncientBattleArmorHat))
                    {
                        recipe.RemoveIngredient(ItemID.TitaniumBar);
                        recipe.AddIngredient(ItemID.TitaniumBar, 10);
                    }
                }
                if(recipe.TryGetResult(ItemID.AncientBattleArmorShirt, out Item AncientBattleArmorShirt))
                {
                    if(recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item AdamantiteBar_AncientBattleArmorShirt))
                    {
                        recipe.RemoveIngredient(ItemID.AdamantiteBar);
                        recipe.AddIngredient(ItemID.AdamantiteBar, 15);
                    }
                    if(recipe.TryGetIngredient(ItemID.TitaniumBar, out Item TitaniumBar_AncientBattleArmorShirt))
                    {
                        recipe.RemoveIngredient(ItemID.TitaniumBar);
                        recipe.AddIngredient(ItemID.TitaniumBar, 15);
                    }
                }
                if(recipe.TryGetResult(ItemID.AncientBattleArmorPants, out Item AncientBattleArmorPants))
                {
                    if(recipe.TryGetIngredient(ItemID.AdamantiteBar, out Item AdamantiteBar_AncientBattleArmorPants))
                    {
                        recipe.RemoveIngredient(ItemID.AdamantiteBar);
                        recipe.AddIngredient(ItemID.AdamantiteBar, 10);
                    }
                    if(recipe.TryGetIngredient(ItemID.TitaniumBar, out Item TitaniumBar_AncientBattleArmorPants))
                    {
                        recipe.RemoveIngredient(ItemID.TitaniumBar);
                        recipe.AddIngredient(ItemID.TitaniumBar, 10);
                    }
                }

                //Chlorophyte Bar

                if(recipe.TryGetResult(ItemID.ChlorophyteBar, out Item ChlorophyteBar))
                {
                    recipe.RemoveIngredient(ItemID.ChlorophyteOre);
                    recipe.AddIngredient(ItemID.ChlorophyteOre, 3);
                }

                //Chlorophyte Bullet

                if(recipe.TryGetResult(ItemID.ChlorophyteBullet, out Item ChlorophyteBullet))
                {
                    recipe.RemoveIngredient(ItemID.MusketBall);
                    recipe.AddIngredient(ItemID.MusketBall, 150);
                    ChlorophyteBullet.stack = 150;
                }
            }
        }
    }
}

