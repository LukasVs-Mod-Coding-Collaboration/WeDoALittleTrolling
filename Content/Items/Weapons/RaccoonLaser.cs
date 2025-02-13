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

using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using WeDoALittleTrolling.Content.Projectiles;
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;
using System;
using WeDoALittleTrolling.Common.ModSystems;

namespace WeDoALittleTrolling.Content.Items.Weapons
{
    internal class RaccoonLaser : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 28;
            Item.scale = 0.875f;
            Item.rare = ItemRarityID.Expert;

            Item.useTime = 120;
            Item.useAnimation = 120;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item84;

            Item.DamageType = DamageClass.Ranged;
            Item.damage = 4000;
            Item.knockBack = 5f;
            Item.noMelee = true;
            Item.crit = 0;
            Item.shoot = ModContent.ProjectileType<Beamlaser2>();
            Item.shootSpeed = 2.0f;
            Item.autoReuse = true;
       

        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            //float aimAngle = MathHelper.ToDegrees(velocity.ToRotation());
            //Depreciated
            float closestDistance = 0;
            bool hasTarget = false;
            NPC target = null;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if // Do we have a valid, hostile target here?
                    (
                    Main.npc[i] != null &&
                    !Main.npc[i].friendly &&
                    !Main.npc[i].CountsAsACritter &&
                    !Main.npc[i].isLikeATownNPC &&
                    !Main.npc[i].dontTakeDamage &&
                    Main.npc[i].active &&
                    Main.npc[i].CanBeChasedBy()
                    )
                {
                    Vector2 playerToThis = new Vector2(Main.npc[i].Center.X - player.Center.X, Main.npc[i].Center.Y - player.Center.Y); // Vector from player to potential target
                    Vector2 cursorToThis = new Vector2(Main.npc[i].Center.X - Main.MouseWorld.X, Main.npc[i].Center.Y - Main.MouseWorld.Y); // Vector from cursor to potential target
                    float trigonometricAngle = MathHelper.ToDegrees((float)Math.Acos(Vector2.Dot(playerToThis, velocity) / (playerToThis.Length() * velocity.Length()))); //Correction Angle in Degrees
                    if (Math.Abs(trigonometricAngle) < 30 ) // If where we're aiming deviates less than 15 degrees from where our npc is relative to us
                    {
                        if (!hasTarget) // We don't compare anything if we don't have anything to compare to yet
                        {
                            target = Main.npc[i];
                            hasTarget = true;
                            closestDistance = cursorToThis.Length();
                        }
                        else
                        {
                            if(cursorToThis.Length() < closestDistance) // If we do have a target, now's the time to compare which of them is closer to where we're aiming
                            {
                                target = Main.npc[i];
                                closestDistance = cursorToThis.Length();
                            }
                        }
                    }
                }
            }
            /*
            if (hasTarget)
            {
                Vector2 playerTo = new Vector2(target.Center.X - player.Center.X, target.Center.Y - player.Center.Y); // Vector from player to potential target
                float trigonometricAngle2 = MathHelper.ToDegrees((float)Math.Acos(Vector2.Dot(playerTo, velocity) / (playerTo.Length() * velocity.Length()))); //Correction Angle in Degrees
                player.chatOverhead.NewMessage("ToRotationAngle: " + Math.Abs(aimAngle - MathHelper.ToDegrees(playerTo.ToRotation())) + ", TrigonometricAngle: " + trigonometricAngle2, 60);
            }
            */
            if (hasTarget && Main.myPlayer == player.whoAmI)
            {
                Projectile targetShot = Projectile.NewProjectileDirect(player.GetSource_ItemUse(Item), target.Center, Vector2.Zero, ProjectileID.WoodenArrowFriendly, damage, knockback, player.whoAmI);
            }
            else if (!hasTarget && Main.myPlayer == player.whoAmI)
            {
                Projectile missedShot = Projectile.NewProjectileDirect(player.GetSource_ItemUse(Item), Main.MouseWorld, Vector2.Zero, ProjectileID.WoodenArrowFriendly, damage, knockback, player.whoAmI);
            }
            return false;
        }


        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10.0f, -4.5f);
        }

        public override void AddRecipes()
        {
            if(WDALTModSystem.isThoriumModPresent && WDALTModSystem.MCIDIntegrity)
            {
                CreateRecipe()
                    .AddTile(TileID.LunarCraftingStation)
                    .AddIngredient(ModContent.ItemType<D6A1_Artemis>(), 1)
                    .AddIngredient(WDALTModContentID.GetThoriumItemID(WDALTModContentID.ThoriumItem_Essence_SFF), 3)
                    .AddIngredient(WDALTModContentID.GetThoriumItemID(WDALTModContentID.ThoriumItem_Essence_OLD), 3)
                    .AddIngredient(WDALTModContentID.GetThoriumItemID(WDALTModContentID.ThoriumItem_Essence_AET), 3)
                    .AddIngredient(ItemID.LunarBar, 20)
                    .AddIngredient(WDALTModContentID.GetThoriumItemID(WDALTModContentID.ThoriumItem_Fragment_WD), 20)
                    .AddIngredient(ItemID.FragmentStardust, 20)
                    .AddIngredient(ItemID.FragmentVortex, 20)
                    .AddIngredient(ItemID.FragmentNebula, 20)
                    .AddIngredient(ItemID.FragmentSolar, 20)
                    .Register();
            }
        }

    }
}
