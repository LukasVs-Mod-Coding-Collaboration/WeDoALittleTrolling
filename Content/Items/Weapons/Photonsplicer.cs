using WeDoALittleTrolling.Content.Projectiles;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace WeDoALittleTrolling.Content.Items.Weapons
{
    public class Photonsplicer : ModItem
    {
        public override void SetStaticDefaults() {
            ItemID.Sets.SkipsInitialUseSound[Item.type] = true; // This skips use animation-tied sound playback, so that we're able to make it be tied to use time instead in the UseItem() hook.
            ItemID.Sets.Spears[Item.type] = true; // This allows the game to recognize our new item as a spear.
        }

        public override void SetDefaults() {
            // Common Properties
            Item.rare = ItemRarityID.Pink; // Assign this item a rarity level of Pink
            Item.value = Item.sellPrice(silver: 10); // The number and type of coins item can be sold for to an NPC

            Item.width = 200;
            Item.height = 200;
            Item.scale = (float)1.0;

            // Use Properties
            Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
            Item.useAnimation = 24; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            Item.useTime = 24; // The length of the item's use time in ticks (60 ticks == 1 second.)
            Item.reuseDelay = 8;
            Item.UseSound = SoundID.Item71; // The sound that this item plays when used.
            Item.autoReuse = true; // Allows the player to hold click to automatically use the item again. Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

            // Weapon Properties
            Item.damage = 80;
            Item.knockBack = (float)10.0;
            Item.noUseGraphic = true; // When true, the item's sprite will not be visible while the item is in use. This is true because the spear projectile is what's shown so we do not want to show the spear sprite as well.
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true; // Allows the item's animation to do damage. This is important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.

            // Projectile Properties
            Item.shootSpeed = (float)3.7; // The speed of the projectile measured in pixels per frame.
            Item.shoot = ModContent.ProjectileType<PhotonsplicerProjectile>(); // The projectile that is fired from this weapon
        }

        public override bool CanUseItem(Player player) {
            // Ensures no more than one spear can be thrown out, use this when using autoReuse
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }

        public override bool? UseItem(Player player)
		{
            SoundEngine.PlaySound(Item.UseSound.Value, player.Center);
            return null;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes() {
            CreateRecipe()
                .Register();
        }
    }
}
